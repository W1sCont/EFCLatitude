using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Drawing.Imaging;

namespace screenSharingServer
{
    public partial class Form1 : Form
    {
        TcpClient tcpClient;
        NetworkStream netstream;
        public SynchronizationContext uiContext;
        CancellationTokenSource cts;
        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;

            Text = "Screen sharing";
            label1.Text = "IP-адрес";
            button1.Text = "З'єднання";
            button2.Text = "Скасувати";
        }

        private async void Connect()
        {
            cts = new CancellationTokenSource();
            string IP = null;

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Не вказано адрес для підкючення!");
                return;
            }
            IP = textBox1.Text;
            await Task.Run(async () =>
            {
                try
                {
                    tcpClient = new TcpClient();
                    await tcpClient.ConnectAsync(IP, 49152);
                    netstream = tcpClient.GetStream();
                    byte[] msg = Encoding.Default.GetBytes(Dns.GetHostName());
                    await netstream.WriteAsync(msg, 0, msg.Length);
                    uiContext.Send(i => MessageBox.Show("З'єднання втсановлено!"), null);
                    SendScreen();
                }
                catch (Exception ex) { uiContext.Send(i => MessageBox.Show("Клієнт: " + ex.Message), null); }
            });

        }

        private async void SendScreen()
        {
            Rectangle size = Screen.PrimaryScreen.Bounds;
            CancellationToken token = cts.Token;
            await Task.Run(async () =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested) return;
                    using Bitmap btmp = new Bitmap(size.Width, size.Height);
                    using Graphics graf = Graphics.FromImage(btmp);
                    using MemoryStream ms = new MemoryStream();

                    graf.CopyFromScreen(0, 0, 0, 0, size.Size);
                    btmp.Save(ms, ImageFormat.Jpeg);
                    byte[] buffer = ms.ToArray();
                    await netstream.WriteAsync(BitConverter.GetBytes(buffer.Length), 0, 4);
                    await netstream.WriteAsync(buffer, 0, buffer.Length);
                    await Task.Delay(100);
                }
            }, token);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            netstream?.Close();
            tcpClient.Close();

            MessageBox.Show("З'єднання розірвано");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (netstream != null)
            {
                string theReply = "Я завершую обробку повідомлень";
                byte[] msg = Encoding.Default.GetBytes(theReply);
                netstream.Write(msg, 0, msg.Length);
            }
            netstream?.Close();
            tcpClient?.Close();
        }
    }
}
