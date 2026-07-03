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
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            try
            {
                MainClass server = new MainClass();
                Console.WriteLine("connect....");
                _ = server.Accept();
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
                byte[] bytes = new byte[65536];

                while (true)
                {
                    int bytesRec = await handler.ReceiveAsync(bytes);
                    if (bytesRec == 0)
                    {
                        break;
                    }
                    Serialization_Deserialization serializer = new Serialization_Deserialization();
                    MyCommand myCommand = serializer.DeserializeObj<MyCommand>(bytes, bytesRec);

                    if (myCommand == null) continue;

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
                            KillProcess(new MyProcess { ProcessId = myCommand.IdProcess });
                            myCommand.CommandResult = true;
                            res = serializer.SerializeObj<MyCommand>(myCommand);
                            handler.Send(res);
                            break;
                        default:
                            Console.WriteLine();
                            break;
                    }
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
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 49152);
                Socket sListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                sListener.Bind(ipEndPoint);

                sListener.Listen(10);
                while (true)
                {
                    Socket handler = await sListener.AcceptAsync();
                    _ = Receive(handler);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

