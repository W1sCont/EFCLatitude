using GamesDbContext;
using Microsoft.EntityFrameworkCore;
namespace DbGames
{
    public partial class Form1 : Form
    {
        private readonly GameDbContext _db;
        public Form1()
        {
            InitializeComponent();
            _db = new GameDbContext();
            Text = "Ігротека";
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            checkBox1.Text = "Режим студій";
            RefreshGrid();
        }
        private void RefreshGrid()
        {
            if (!checkBox1.Checked)
            {
                var gamesList = _db.Games.Include(i => i.Studio).Select(i => new
                {
                    i.Id,
                    Назва = i.Name,
                    Стиль = i.Style,
                    Режим = i.GameMode,
                    Продано = i.Sold,
                    Дата_релізу = i.Date.ToShortDateString(),
                    Назва_студії = i.Studio.Name
                }).ToList();
                dataGridView1.DataSource = gamesList;
            }
            else if (checkBox1.Checked)
            {
                var studioList = _db.Studios.Select(i => new
                {
                    i.Id,
                    Назва = i.Name
                }).ToList();
                dataGridView1.DataSource = studioList;
            }
            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                try
                {
                    Form2 add = new Form2();
                    add.ShowDialog();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (checkBox1.Checked)
            {
                Form3 add = new Form3();
                add.ShowDialog();
            }
            RefreshGrid();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Text = "Режим ігор";
            }
            else if (!checkBox1.Checked)
            {
                checkBox1.Text = "Режим студій";
            }
            RefreshGrid();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Нічого не вибрано");
                return;
            }

            if (!checkBox1.Checked)
            {
                try
                {
                    int? editId = (int?)dataGridView1.CurrentRow?.Cells["Id"].Value;

                    Form2 add = new Form2(editId);
                    add.ShowDialog();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (checkBox1.Checked)
            {
                int? editId = (int?)dataGridView1.CurrentRow?.Cells["Id"].Value;

                Form3 add = new Form3(editId);
                add.ShowDialog();
            }
            RefreshGrid();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Нічого не вибрано");
                return;
            }
            var confirmResult = MessageBox.Show("Видалення безповоротне, ви впевнені?",
                                               "Підтвердження видалення",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                if (!checkBox1.Checked)
                {
                    try
                    {
                        int? removeId = (int?)dataGridView1.CurrentRow?.Cells["Id"].Value;
                        var removedObj = _db.Games.Find(removeId);
                        if (removedObj != null)
                        {
                            _db.Games.Remove(removedObj);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (checkBox1.Checked)
                {
                    try
                    {
                        int? removeId = (int?)dataGridView1.CurrentRow?.Cells["Id"].Value;
                        var removedObj = _db.Studios.Find(removeId);
                        if (removedObj != null)
                        {
                            _db.Studios.Remove(removedObj);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            _db.SaveChanges();
            RefreshGrid();
        }
    }
}
