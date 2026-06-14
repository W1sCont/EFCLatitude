using ClassLibrary;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.Design.AxImporter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
namespace SearchFileTPL
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;

        CancellationTokenSource cancelToken;
        ManualResetEvent thread_1_pause = new ManualResetEvent(true);
        bool togglePause = true;

        string inputMask;
        string? inputText;
        string path;
        Regex regMask;
        Regex? regText;
        DirectoryInfo dirinf;

        public Form1()
        {
            InitializeComponent();
            
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

                cancelToken = new CancellationTokenSource();
                CancellationToken token = cancelToken.Token;

                listView1.Items.Clear();
                label5.Text = "0";

                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;

                button3.Text = "Призупинити";

                thread_1_pause.Set();
                togglePause = true;

                inputMask = CheckMaskFile(textBox1.Text);
                regMask = new Regex(inputMask, RegexOptions.IgnoreCase);
                
                path = comboBox1.Text;
                dirinf = new DirectoryInfo(path);

                bool chek = checkBox1.Checked;

                regText = string.IsNullOrWhiteSpace(textBox2.Text) ? null
                    : new Regex(Regex.Escape(textBox2.Text.Trim()), RegexOptions.IgnoreCase);

                Action<string, string, string, string> fileFoundHandler = (name, folder, size, date) =>
                {
                    Task.Factory.StartNew(() =>
                    {
                        ListViewItem item = new ListViewItem(name);
                        item.SubItems.Add(folder);
                        item.SubItems.Add(size);
                        item.SubItems.Add(date);
                        listView1.Items.Add(item);

                        label5.Text = listView1.Items.Count.ToString();
                    });
                };

                Task tsk = Task.Factory.StartNew(() =>
                    Search.FindTextInFiles(regText, dirinf, regMask, chek, thread_1_pause, token, fileFoundHandler), token);
                
                tsk.ContinueWith(status => {
                    button1.Enabled = true;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    if (status.IsCanceled || status.Exception?.InnerException is OperationCanceledException)
                    {
                        MessageBox.Show("Пошук зупинено користувачем.");
                    }
                    else if (status.IsFaulted)
                    {
                        MessageBox.Show($"Виникла помилка: {status.Exception.InnerException?.Message}");
                    }
                    else
                    {
                        MessageBox.Show("Пошук завершено успішно!");
                    }
                });
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancelToken.Cancel();
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
