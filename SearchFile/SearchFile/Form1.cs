using ClassLibrary;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Windows.Forms.Design.AxImporter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace SearchFile
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;

        ManualResetEvent thread_1_stop = new ManualResetEvent(false);
        ManualResetEvent thread_1_pause = new ManualResetEvent(true);
        bool togglePause = true;

        string inputMask;
        string inputText;
        string path;
        Regex regMask;
        Regex regText;
        DirectoryInfo dirinf;

        struct Options
        {
            public DirectoryInfo di;
            public Regex rMask;
            public Regex tMask;
            public ListView listView;
            public ManualResetEvent thread_stop;
            public ManualResetEvent thread_pause;
            public bool checkInfo;
        }

        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;
            Search sr = new Search();
            Text = "Пошук файлів";

            listView1.View = View.Details;
            listView1.Columns.Add("Name", 245);
            listView1.Columns.Add("Folder", 300);
            listView1.Columns.Add("Size", 150);
            listView1.Columns.Add("Modification date", 200);
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            label1.Text = "Файл";
            label2.Text = "Слово або фраза у файлі";
            label3.Text = "Диск";
            label4.Text = "Результати пошуку: кількість знайдених файлів";
            label5.Text = "";

            button1.Text = "Знайти";
            button2.Text = "Зупинити";
            button2.Enabled = false;
            button3.Text = "Призупинити";
            button3.Enabled = false;

            checkBox1.Text = "Підкаталоги";
            LogicalDriveList();
            comboBox1.SelectedIndex = 0;
        }

        private void LogicalDriveList()
        {
            string[] astrLogicalDrives = System.IO.Directory.GetLogicalDrives();
            comboBox1.Items.Clear();
            foreach (string disk in astrLogicalDrives)
                comboBox1.Items.Add(disk);
        }

        private void MyThread(object myParam)
        {
            try
            {
                Options op = (Options)myParam;
                ListView listView = op.listView;
                ManualResetEvent thread_stop = op.thread_stop;
                ManualResetEvent thread_pause = op.thread_pause;
                Regex regMask = op.rMask;
                Regex regText = op.tMask;
                DirectoryInfo dirinf = op.di;
                bool chek = op.checkInfo;


                ulong Count = 0;
                thread_pause.WaitOne();
                if (thread_stop.WaitOne(0)) return;
                Action<string, string, string, string> fileFoundHandler = (name, folder, size, date) =>
                {
                    uiContext.Post(state =>
                    {
                        ListViewItem item = new ListViewItem(name);
                        item.SubItems.Add(folder);
                        item.SubItems.Add(size);
                        item.SubItems.Add(date);
                        listView1.Items.Add(item);

                        label5.Text = listView1.Items.Count.ToString();
                    }, null);
                };
                Search.FindTextInFiles(regText, dirinf, regMask, chek, thread_stop, thread_pause, fileFoundHandler);
                uiContext.Post(status => { 
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                }, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string CheckMaskFile(string mask)
        {
            string Mask;
            Mask = mask.Replace(".", @"\.");
            Mask = mask.Replace("?", ".");
            Mask = mask.Replace("*", ".*");
            Mask = "^" + mask + "$";
            return Mask;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Введіть файл для пошуку!");
                    return;
                }

                listView1.Items.Clear();
                button1.Enabled = false;
                thread_1_stop.Reset();
                thread_1_pause.Set();
                togglePause = true;
                button3.Text = "Призупинити";
                button2.Enabled = true;
                button3.Enabled = true;

                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    inputText = textBox2.Text;
                    inputText = Regex.Escape(inputText);
                }

                inputMask = CheckMaskFile(textBox1.Text);
                regMask = new Regex(inputMask, RegexOptions.IgnoreCase);
                path = comboBox1.Text;

                if (textBox2 != null)
                {
                    string text = Regex.Escape(textBox2.Text);
                    regText = text.Length == 0 ? null : new Regex(text, RegexOptions.IgnoreCase);
                    dirinf = new DirectoryInfo(path);

                    Thread thr = new Thread(MyThread);
                    Options op = new Options
                    {
                        di = dirinf,
                        rMask = regMask,
                        tMask = regText,
                        listView = listView1,
                        thread_stop = thread_1_stop,
                        thread_pause = thread_1_pause,
                        checkInfo = checkBox1.Checked
                    };

                    thr.Start(op);
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thread_1_stop.Set();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (togglePause)
            {
                button3.Text = "Продовжити";
                thread_1_pause.Reset();
                togglePause = false;
            }
            else
            {
                button3.Text = "Призупинити";
                thread_1_pause.Set();
                togglePause = true;
            }
        }
    }
}
