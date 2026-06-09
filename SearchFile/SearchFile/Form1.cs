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

        string inputMask;
        string inpetText;
        struct Options
        {
            public ListView listView;
            public ManualResetEvent thread_stop;
            public ManualResetEvent thread_pause;
        }
        public Form1()
        {
            InitializeComponent();
            Search sr = new Search();
            Text = "Пошук файлів";
            label1.Text = "Файл";
            label2.Text = "Слово або фраза у файлі";
            label3.Text = "Диск";
            label4.Text = "Результати пошуку: кількість знайдених файлів";
            label5.Text = "";
            button1.Text = "Знайти";
            button2.Text = "Зупинити";
            button3.Text = "Призупинити";
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
                while (!thread_stop.WaitOne(0))
                {
                    thread_pause.WaitOne();
                    if (thread_stop.WaitOne(0)) break;
                    SendOrPostCallback del = (param) =>
                    {
                        
                    };
                    uiContext.Send(del, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string CheckMask(string mask)
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
                if(textBox2.Text != null)
                {
                    inpetText = textBox2.Text;
                    inpetText = Regex.Escape(inpetText);
                }

                inputMask = CheckMask(textBox1.Text);

            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }
        }
    }
}
