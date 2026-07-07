using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DateTimeUDP
{
    public class MainClass
    {
        static async Task Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            try
            {
                await WaitClientQuery();
            }
            catch (Exception ex) { Console.WriteLine("Відправник: " + ex.Message); }      
        }

        private async static Task WaitClientQuery()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 49152);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.Bind(ipEndPoint);

                while (true)
                {
                    if (token.IsCancellationRequested) { return; }

                    EndPoint remote = new IPEndPoint(IPAddress.Any, 0);
                    
                    byte[] arr = new byte[1024];
                    var result = await socket.ReceiveFromAsync(arr, SocketFlags.None, remote, token);
                    string? clientIP = (result.RemoteEndPoint).ToString();
                    string command = Encoding.UTF8.GetString(arr, 0, result.ReceivedBytes).Trim();

                    if (command == "CONNECT")
                    {
                        Console.WriteLine("Підключився: " + clientIP + " Дата та час: " + DateTime.UtcNow.ToString());
                    }
                    else if (command == "DISCONNECT")
                    {
                        Console.WriteLine("Відключився: " + clientIP + " Дата та час: " + DateTime.UtcNow.ToString());
                    }
                    else if (command == "GET_TIME")
                    {
                        byte[] curentTime = Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString());
                        await socket.SendToAsync(curentTime, SocketFlags.None, result.RemoteEndPoint, token);
                    }
                }

            }
            catch (Exception ex) { Console.WriteLine("Отримувач: " + ex.Message); }
        }
    }
}
