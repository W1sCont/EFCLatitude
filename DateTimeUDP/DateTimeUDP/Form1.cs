using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DateTimeUDP
{
    public partial class Form1 : Form
    {
        private System.Threading.SynchronizationContext uiContext;
        private Socket? _clientSocket;
        private IPEndPoint? _serverEndPoint;
        private CancellationTokenSource? _cts;
        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;

            Text = "Сервер часу";
            button1.Text = "З'єднатись";
            button2.Text = "Роз'єднатись";
            label1.Text = "ІР-адрес";
            label2.Font = new Font("Consolas", 26, FontStyle.Bold);
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.ForeColor = Color.DarkSlateGray;
            label2.Text = "--:--:--";
        }

        private async void RequestTimeFromServer()
        {
            try
            {
                _serverEndPoint = new IPEndPoint(IPAddress.Parse(textBox1.Text), 49152);
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                _cts = new CancellationTokenSource();
                CancellationToken token = _cts.Token;

                byte[] arr = Encoding.UTF8.GetBytes("CONNECT");
                await _clientSocket.SendToAsync(arr, _serverEndPoint);

                while (true)
                {
                    arr = Encoding.UTF8.GetBytes("GET_TIME");
                    await _clientSocket.SendToAsync(arr, _serverEndPoint);

                    byte[] result = new byte[1024];
                    EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    var dateTimeResult = await _clientSocket.ReceiveFromAsync(result, SocketFlags.None, remoteEP, token);

                    string timeFromServer = Encoding.UTF8.GetString(result, 0, dateTimeResult.ReceivedBytes).Trim();

                    if (DateTime.TryParse(timeFromServer, out DateTime parsedTime))
                    {
                        label2.Text = parsedTime.ToString();
                    }

                    await Task.Delay(1000, token);
                }

            }
            catch (OperationCanceledException) { uiContext.Send(i => { label2.Text = "--:--:--"; }, null); }
            catch (Exception ex) { uiContext.Send(i => { MessageBox.Show("Помилка з'єднання: " + ex.Message); }, null); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RequestTimeFromServer();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (_clientSocket != null && _serverEndPoint != null)
            {
                byte[] arr = Encoding.UTF8.GetBytes("DISCONNECT");
                await _clientSocket.SendToAsync(arr, _serverEndPoint);
            }

            _cts?.Cancel();
            _clientSocket?.Close();
            _clientSocket = null;
            _cts = null;
        }

        
    }
}
