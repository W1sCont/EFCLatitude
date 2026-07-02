using ClassLibrary_MyCommand;
using ClassLibrary_MyProcess;
using ClassLibrary_Serialization;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Client_TaskManager
{
    public partial class Form1 : Form
    {
        Socket sock;
        public Form1()
        {
            InitializeComponent();
            Text = "Віддалений диспетчер завдань";
            button1.Text = "Зєднання";
            button2.Text = "Оновити список";
            button3.Text = "Завершити процес";
            button4.Text = "Створити процес";
            label1.Text = "IP-адрес";
            label2.Text = "Активні процеси";
            label3.Text = "Шлях до програми (процес)";
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.RowHeadersVisible = false;
        }
        private async void Connect()
        {
            await Task.Run(() =>
            {
                try
                {
                    IPAddress ipAddr = IPAddress.Parse(textBox1.Text);
                    IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 49152);

                    sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    sock.Connect(ipEndPoint);
                    byte[] msg = Encoding.Default.GetBytes(Dns.GetHostName());
                    int bytesSent = sock.Send(msg);
                    MessageBox.Show("Клієнт " + Dns.GetHostName() + " встановив з'єднання з " + sock.RemoteEndPoint.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Клієнт: " + ex.Message);
                }
            });
        }

        private async void Exchange()
        {
            await Task.Run(() =>
            {
                try
                {
                    string theMessage = textBox1.Text;
                    byte[] msg = Encoding.Default.GetBytes(theMessage);
                    int bytesSent = sock.Send(msg);
                    if (theMessage.IndexOf("<end>") > -1)
                    {
                        byte[] bytes = new byte[1024];
                        int bytesRec = sock.Receive(bytes);
                        MessageBox.Show("Сервер (" + sock.RemoteEndPoint.ToString() + ") відповів: " + Encoding.Default.GetString(bytes, 0, bytesRec) /*Конвертуємо масив байтів у рядок*/);
                        sock.Shutdown(SocketShutdown.Both);
                        sock.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Клієнт: " + ex.Message);
                }
            });
        }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        // Зєднання
        private void button1_Click(object sender, EventArgs e) { Connect(); }
        
        // Оновити список
        private void button2_Click(object sender, EventArgs e)
        {

        }

        // Завершити процес
        private void button3_Click(object sender, EventArgs e)
        {

        }

        // Створити процес
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (sock != null)
                {
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Клієнт: " + ex.Message); }
        }
    }
}
