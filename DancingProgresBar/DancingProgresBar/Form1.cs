using System.Threading;
namespace DancingProgresBar
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;

        ManualResetEvent thread_1_stop = new ManualResetEvent(false);
        ManualResetEvent thread_1_pause = new ManualResetEvent(true);

        ManualResetEvent thread_2_stop = new ManualResetEvent(false);
        ManualResetEvent thread_2_pause = new ManualResetEvent(true);

        ManualResetEvent thread_3_stop = new ManualResetEvent(false);
        ManualResetEvent thread_3_pause = new ManualResetEvent(true);

        Random rand = new Random();

        struct Options
        {
            public int delay;
            public ProgressBar progresBar;
            public ManualResetEvent thread_stop;
            public ManualResetEvent thread_pause;
        }
        public Form1()
        {
            InitializeComponent();
            Text = "Танцюючі прогрес-бари";
            uiContext = SynchronizationContext.Current;
            checkBox1.Text = "Запустити 1-й потік";
            checkBox2.Text = "Призупинити 1-й потік";
            checkBox3.Text = "Запустити 2-й потік";
            checkBox4.Text = "Призупинити 2-й потік";
            checkBox5.Text = "Запустити 3-й потік";
            checkBox6.Text = "Призупинити 3-й потік";

            progressBar1.Style = ProgressBarStyle.Blocks;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
        }
        private void MyThread(object myParam)
        {
            try
            {
                Options op = (Options)myParam;
                int delay = op.delay;
                ProgressBar progresBar = op.progresBar;
                ManualResetEvent thread_stop = op.thread_stop;
                ManualResetEvent thread_pause = op.thread_pause;
                while (!thread_stop.WaitOne(0))
                {
                    thread_pause.WaitOne();
                    if (thread_stop.WaitOne(0)) break;
                    int randValue;
                    SendOrPostCallback del = (param) =>
                    {
                        randValue = rand.Next(1, 100);
                        progresBar.Value = randValue;
                    };
                    uiContext.Send(del, null);

                    Thread.Sleep(delay);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void progressBar1_Click(object sender, EventArgs e) { }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Options op = new Options { delay = 100 , progresBar = progressBar1,
                thread_stop = thread_1_stop, thread_pause = thread_1_pause
            };

            Thread th1 = new Thread(MyThread);
            th1.IsBackground = true;

            if (checkBox1.Checked)
            {
                thread_1_stop.Reset();
                checkBox1.Text = "Зупинити 1-й потік";
                th1.Start(op);
            }
            else if (!checkBox1.Checked)
            {
                thread_1_stop.Set();
                checkBox1.Text = "Запустити 1-й потік";
                checkBox2.Checked = false;
                progressBar1.Value = 0;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                thread_1_pause.Reset();
                checkBox2.Text = "Відновити 1-й потік";

            }
            else if (!checkBox2.Checked)
            {
                thread_1_pause.Set();
                checkBox2.Text = "Призупинити 1-й потік";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Options op = new Options { delay = 100, progresBar = progressBar2,
                thread_stop = thread_2_stop, thread_pause = thread_2_pause
            };
            Thread th2 = new Thread(MyThread);
            th2.IsBackground = true;

            if (checkBox3.Checked)
            {
                thread_2_stop.Reset();
                checkBox3.Text = "Зупинити 2-й потік";
                th2.Start(op);
            }
            else if (!checkBox3.Checked)
            {
                thread_2_stop.Set();
                checkBox3.Text = "Запустити 2-й потік";
                checkBox4.Checked = false;
                progressBar2.Value = 0;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                thread_2_pause.Reset();
                checkBox4.Text = "Відновити 2-й потік";

            }
            else if (!checkBox4.Checked)
            {
                thread_2_pause.Set();
                checkBox4.Text = "Призупинити 2-й потік";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Options op = new Options { delay = 100, progresBar = progressBar3,
                thread_stop = thread_3_stop, thread_pause = thread_3_pause
            };
            Thread th3 = new Thread(MyThread);
            th3.IsBackground = true;

            if (checkBox5.Checked)
            {
                thread_3_stop.Reset();
                checkBox5.Text = "Зупинити 3-й потік";
                th3.Start(op);
            }
            else if (!checkBox5.Checked)
            {
                thread_3_stop.Set();
                checkBox5.Text = "Запустити 3-й потік";
                checkBox6.Checked = false;
                progressBar3.Value = 0;

            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                thread_3_pause.Reset();
                checkBox6.Text = "Відновити 3-й потік";

            }
            else if (!checkBox6.Checked)
            {
                thread_3_pause.Set();
                checkBox6.Text = "Призупинити 3-й потік";
            }
        }
    }
}
