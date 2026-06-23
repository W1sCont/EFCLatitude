using System.ComponentModel;
using System.Diagnostics;

namespace TaskManager
{
    public partial class Form1 : Form
    {
        BindingList<MyProcess> myProcess;
        public Form1()
        {
            InitializeComponent();
            myProcess = new BindingList<MyProcess>();

            Text = "Диспетчер завдань";
            button1.Text = "Оновити список";
            button2.Text = "Завершити процес";
            button3.Text = "Запустити процес";

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.RowHeadersVisible = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            myProcess.Clear();
            await Task.Run(() =>
            {
                try
                {
                    Process[] lp = Process.GetProcesses();
                    var sortedProcess = lp.OrderBy(i => i.ProcessName).ToList();

                    foreach (Process p in sortedProcess)
                    {
                        myProcess.Add(new MyProcess(p.ProcessName, p.Id));
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            });
            dataGridView1.DataSource = myProcess;
            dataGridView1.Columns["Name"].HeaderText = "Назва процесу";
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Виберіть процес!");
                return;
            }

            MyProcess curentProcess = dataGridView1.CurrentRow.DataBoundItem as MyProcess;

            if (curentProcess == null)
            {
                MessageBox.Show("Не існуючий процес!");
                return;
            }

            try
            {
                Process processToKill = Process.GetProcessById(curentProcess.Id);
                processToKill.Kill();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Не вказано шлях до програми!");
                return;
            }
            try
            {
                string? path = textBox1.Text;
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = path;
                    proc.Start();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
