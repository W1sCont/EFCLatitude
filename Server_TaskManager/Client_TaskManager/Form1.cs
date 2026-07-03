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
        SynchronizationContext uiContext;
        Socket sock;
        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;
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
                    uiContext.Post(i => MessageBox.Show("Connected!"), null);
                }
                catch (Exception ex) { uiContext.Post(i => MessageBox.Show(ex.Message), null); }
            });
        }

        private async Task RefreshGridList()
        {
            await Task.Run(() =>
            {
                try
                {
                    MyCommand processList = new MyCommand() { NameOfCommand = "ListProcess" };
                    Serialization_Deserialization serialization = new Serialization_Deserialization();
                    sock.Send(serialization.SerializeObj(processList));
                    byte[] buffer = new byte[65536];
                    int bytesRec = sock.Receive(buffer);
                    List<MyProcess> list = serialization.DeserializeObj<List<MyProcess>>(buffer, bytesRec);
                    uiContext.Post(i => dataGridView1.DataSource = list, null);
                }
                catch (Exception ex) { uiContext.Post(i => MessageBox.Show(ex.Message), null); }
            });
        }

        private async Task CreateProcess()
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Назва процесу не може бути порожня!");
                return;
            }
            string path = textBox2.Text.Trim();
            await Task.Run(() =>
            {
                try
                {
                    MyCommand process = new MyCommand() { NameOfCommand = "CreateProcess", Path = path };
                    Serialization_Deserialization serialization = new Serialization_Deserialization();
                    sock.Send(serialization.SerializeObj(process));
                    byte[] buffer = new byte[65536];
                    int bytesRec = sock.Receive(buffer);
                    MyCommand result = serialization.DeserializeObj<MyCommand>(buffer, bytesRec);
                    if (result.CommandResult)
                    { uiContext.Post(i => MessageBox.Show("Процес успішно створено!"), null); }
                    else { uiContext.Post(i => MessageBox.Show("Помилка створення процесу!"), null); }
                }
                catch (Exception ex) { uiContext.Post(i => MessageBox.Show(ex.Message), null); }
            });
        }

        private async void KillProcess()
        {

            MyProcess? curentProcess = dataGridView1.CurrentRow.DataBoundItem as MyProcess;
            await Task.Run(() =>
            {
                try
                {
                    MyCommand res = new MyCommand() { NameOfCommand = "KillProcess", IdProcess = curentProcess.ProcessId };
                    Serialization_Deserialization serialization = new Serialization_Deserialization();
                    sock.Send(serialization.SerializeObj(res));
                    byte[] buffer = new byte[65536];
                    int bytesRec = sock.Receive(buffer);
                    MyCommand result = serialization.DeserializeObj<MyCommand>(buffer, bytesRec);
                    if (result.CommandResult)
                    { uiContext.Post(i => MessageBox.Show("Процес успішно видалено!"), null); }
                    else { uiContext.Post(i => MessageBox.Show("Помилка видалення процесу!"), null); }
                }
                catch (Exception ex) { uiContext.Post(i => MessageBox.Show(ex.Message), null); }
            });
        }

        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        // Зєднання
        private void button1_Click(object sender, EventArgs e) { Connect(); }
        
        // Оновити список
        private async void button2_Click(object sender, EventArgs e)
        {
            await RefreshGridList();
        }

        // Завершити процес
        private void button3_Click(object sender, EventArgs e)
        {
            KillProcess();
        }

        // Створити процес
        private async void button4_Click(object sender, EventArgs e)
        {
            await CreateProcess();
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
