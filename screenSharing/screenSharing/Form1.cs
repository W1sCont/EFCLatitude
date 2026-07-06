using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;

namespace screenSharing
{
    public partial class Form1 : Form
    {
        TcpClient tcpClient;
        NetworkStream netstream;
        public SynchronizationContext uiContext;
        public Form1()
        {
            InitializeComponent();
            Text = "Підлючення клієнта!";
            uiContext = SynchronizationContext.Current;
            Accept();
        }

        private async void Accept()
        {
            await Task.Run(async () =>
            {
                try
                {
                    TcpListener listener = new TcpListener(
                    IPAddress.Any, 49152);
                    listener.Start();

                    tcpClient = await listener.AcceptTcpClientAsync();
                    Receive(tcpClient);
                }
                catch (Exception ex) { uiContext.Send(i => MessageBox.Show("Сервер: " + ex.Message), null); }
            });
        }

        private async void Receive(TcpClient tcpClient)
        {
            await Task.Run(async () =>
            {
                try
                {
                    netstream = tcpClient.GetStream();
                    string client = null;
                    byte[] arr = new byte[tcpClient.ReceiveBufferSize];

                    int len = await netstream.ReadAsync(arr, 0, tcpClient.ReceiveBufferSize);
                    client = Encoding.Default.GetString(arr, 0, len);
                    uiContext.Send(i => { Text = "Підключено клієнта: " + client; }, null);

                    while (true)
                    {

                        byte[] sizeBuffer = new byte[4];
                        int sizeRead = await netstream.ReadAsync(sizeBuffer, 0, 4);
                        if (sizeRead == 0)
                        {
                            netstream.Close();
                            tcpClient.Close();
                            return;
                        }
                        int imageSize = BitConverter.ToInt32(sizeBuffer, 0);
                        byte[] imageBuffer = new byte[imageSize];

                        int totalBytesRead = 0;

                        while (totalBytesRead < imageSize)
                        {
                            int bytesRemaining = imageSize - totalBytesRead;
                            int bytesRead = await netstream.ReadAsync(imageBuffer, totalBytesRead, bytesRemaining);

                            if (bytesRead == 0)
                            {
                                throw new Exception("Клієнт розірвав з'єднання в процесі передачі кадру!");
                            }
                            totalBytesRead += bytesRead;
                        }

                        uiContext.Send(i => {
                            pictureBox1.Image?.Dispose();
                            using (MemoryStream ms = new MemoryStream(imageBuffer))
                            {
                                pictureBox1.Image = new Bitmap(ms);
                            }
                        }, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сервер: " + ex.Message);
                    netstream?.Close();
                    tcpClient?.Close();
                }
            });
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
