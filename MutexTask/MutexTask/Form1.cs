using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace MutexTask
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;
        public Mutex mutex1;
        public Mutex mutex2;
        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;
            // {921182AF-832E-439F-A4F9-DE7974C84985}
            // {567FA6DE-8CAB-4C69-8022-1079A78B97C0}
            Text = "Mutex";
            button1.Text = "Start";
        }

        private async void FirstThread()
        {
            await Task.Run(() =>
            {
                try
                {
                    mutex1.WaitOne();
                    string path = "FirstThread.txt";
                    Random rnd = new Random();
                    using (StreamWriter sw = new StreamWriter(path))    
                    {
                        for (int i = 0; i < 100000; ++i)
                        {
                            sw.WriteLine(rnd.Next(1, 100));
                        }
                    }
                    uiContext.Post(i => MessageBox.Show("Перший потік виконав завдання"), null);
                }
                catch (Exception ex) { uiContext.Post(i => MessageBox.Show(ex.Message), null); }
                finally { mutex1.ReleaseMutex(); }
            });
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false; 

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
        private async void SecondThread()
        {
            await Task.Run(() =>
            {
                try
                {
                    mutex2.WaitOne();
                    mutex1.WaitOne();

                    string pathRead = "FirstThread.txt";
                    string pathWrite = "SecondThread.txt";

                    using (StreamReader reader = new StreamReader(pathRead))
                    using (StreamWriter writer = new StreamWriter(pathWrite))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (int.TryParse(line.Trim(), out int number))
                            {
                                if (IsPrime(number))
                                {
                                    writer.WriteLine(number);
                                }
                            }
                        }
                    }
                    uiContext.Post(i => MessageBox.Show("Другий потік виконав завдання"), null);
                }
                catch (Exception ex){ uiContext.Post(i => MessageBox.Show(ex.Message), null); }
                finally 
                {
                    mutex1.ReleaseMutex();
                    mutex2.ReleaseMutex(); 
                }
            });
        }

        private async void ThirdThread()
        {
            await Task.Run(() =>
            {
               try
                {
                    mutex2.WaitOne();
                    string pathRead = "SecondThread.txt";
                    string pathWrite = "ThirdThread.txt";

                    using (StreamReader reader = new StreamReader(pathRead))
                    using (StreamWriter writer = new StreamWriter(pathWrite))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.EndsWith("7"))
                            {
                                writer.WriteLine(line);
                            }
                        }
                    }
                    uiContext.Post(i => MessageBox.Show("Третій потік виконав завдання"), null);
                }
                catch (Exception ex) { uiContext.Post(i => MessageBox.Show(ex.Message), null); }
                finally
                {
                    mutex2.ReleaseMutex();
                    button1.Enabled = true;
                }
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            mutex1 = new Mutex(false, "{921182AF-832E-439F-A4F9-DE7974C84985}");
            mutex2 = new Mutex(false, "{567FA6DE-8CAB-4C69-8022-1079A78B97C0}");

            FirstThread();
            SecondThread();
            ThirdThread();
        }
    }
}
