using System.IO;
namespace CopyFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Копіювання файлу";
            label1.Text = "Джерело";
            label2.Text = "Приймач";
            button1.Text = "Файл...";
            button2.Text = "Папка...";
            button3.Text = "Копіювати";
        }

        private void MyThread(string inputPath, string outputPath, IProgress<int> progress)
        {
            try
            {
                string input = inputPath;
                string output = outputPath;

                using var streamRead = File.OpenRead(input);
                using var streamWrite = File.OpenWrite(output);

                byte[] buffer = new byte[4096];
                int bytesRead;
                long totalByte = 0;
                int percent;
                while ((bytesRead = streamRead.Read(buffer, 0, buffer.Length)) > 0)
                {
                    streamWrite.Write(buffer, 0, bytesRead);
                    totalByte += bytesRead;
                    percent = (int)((totalByte * 100) / streamRead.Length);

                    progress.Report(percent);
                }
            }
            finally { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Поле не може бути порожнім");
                    return;
                }
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Поле не може бути порожнім");
                    return;
                }

                button3.Enabled = false;

                var progress = new Progress<int>(value =>
                {
                    progressBar1.Value = value;
                });

                string inputPath = textBox1.Text;
                string fileName = Path.GetFileName(inputPath);
                string outputPath = Path.Combine(textBox2.Text, fileName);

                Task tsk = Task.Run(() => MyThread(inputPath, outputPath, progress));

                tsk.ContinueWith(t =>
                {
                    MessageBox.Show("Копіювання завершено");
                    button3.Enabled = true;
                    progressBar1.Value = 0;
                });
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
