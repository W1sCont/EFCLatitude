using ClassLibrary_MyCommand;
using ClassLibrary_MyProcess;
using ClassLibrary_Serialization;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace MyServer
{
    class MainClass
    {
        List<MyProcess> processList = new List<MyProcess>();
        static async Task Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            try
            {
                MainClass server = new MainClass();
                await server.Accept();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task Receive(Socket handler)
        {
            try
            {
                string data = null;
                byte[] bytes = new byte[1024];

                int bytesRec = await handler.ReceiveAsync(bytes);
                Serialization_Deserialization serializer = new Serialization_Deserialization();
                MyCommand myCommand = serializer.DeserializeObj<MyCommand>(bytes, bytesRec);

                data = myCommand.ToString();

                switch (myCommand.NameOfCommand)
                {
                    case "ListProcess":
                        await AsyncListProcess();
                        byte[] res = serializer.SerializeObj<List<MyProcess>>(processList);
                        handler.Send(res);
                        break;
                    case "CreateProcess":
                        await CreateProcess(myCommand.Path);
                        myCommand.CommandResult = true;
                        res = serializer.SerializeObj<MyCommand>(myCommand);
                        handler.Send(res);
                        break;
                    case "KillProcess":
                        KillProcess(new MyProcess { ProcessId = myCommand.IdProcess} );
                        myCommand.CommandResult = true;
                        res = serializer.SerializeObj<MyCommand>(myCommand);
                        handler.Send(res);
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Сервер: " + ex.Message);
            }
            finally
            {
                if (handler.Connected)
                {
                    handler.Shutdown(SocketShutdown.Both);
                }
                handler.Close();
            }
        }

        private async Task Accept()
        {
            await Task.Run(() =>
            {
                try
                {
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 49152);
                    Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    sListener.Bind(ipEndPoint);

                    sListener.Listen(1);
                    while (true)
                    {
                        Socket handler = sListener.Accept();
                        _ = Receive(handler);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        private async Task AsyncListProcess()
        {
            await Task.Run(() =>
            {
                try
                {
                    Process[] lp = Process.GetProcesses();
                    var sortedProcess = lp.OrderBy(i => i.ProcessName).ToList();

                    processList.Clear();

                    foreach (Process p in sortedProcess)
                    {
                        processList.Add(new MyProcess(p.ProcessName, p.Id));
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            });
        }

        private void KillProcess(MyProcess curentProcess)
        {
            if (curentProcess == null)
            {
                Console.WriteLine("Не існуючий процес!");
                return;
            }

            try
            {
                Process processToKill = Process.GetProcessById(curentProcess.ProcessId);
                processToKill.Kill();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private async Task CreateProcess(string path)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = path;
                proc.Start();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}

