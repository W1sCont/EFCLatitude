using ClassDbContext;
using Microsoft.EntityFrameworkCore;
namespace FluentAPI
{
    public partial class Form1 : Form
    {
        private readonly HrmDbContext _db;
        public Form1()
        {
            InitializeComponent();
            _db = new HrmDbContext();
            Text = "HRM";
            checkBox1.Text = "Список посад";
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            try
            {
                // _db.Database.EnsureDeleted();
                _db.Database.EnsureCreated();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка створення бази: {ex.Message}");
            }

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            using var db = new HrmDbContext();
            if (!checkBox1.Checked)
            {
                var result = db.Employees
                .Select(e => new
                {
                    e.Id,
                    Імя = e.Name,
                    Прізвище = e.Surname,
                    Вік = e.Age,
                    Посада = e.JobTitle_Id.Title
                })
                .ToList();
                dataGridView1.DataSource = result;
            }
            else
            {
                var result = db.Role
                .Select(r => new
                {
                    r.Id,
                    Посада = r.Title
                })
                .ToList();
                dataGridView1.DataSource = result;
            }

            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                var listRole = _db.Role.Select(i => i.Id);
                if (!listRole.Any())
                {
                    MessageBox.Show("Список посад порожній");
                    return;
                }
                else
                {
                    Form2 add = new Form2();
                    add.ShowDialog();
                }
            }
            else
            {
                Form3 add = new Form3();
                add.ShowDialog();
            }
            RefreshGrid();
        }

        private void addRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void removeRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Нічого не вибрано");
                return;
            }
            var confirmResult = MessageBox.Show("Ви впевнені, що хочете видалити саме це?",
                                               "Підтвердження видалення",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    if (!checkBox1.Checked)
                    {
                        int selectedId = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                        var selectedEmployee = _db.Employees.Find(selectedId);
                        _db.Employees.Remove(selectedEmployee);

                    }
                    else
                    {
                        int selectedId = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                        var selectedRole = _db.Role.Find(selectedId);
                        _db.Role.Remove(selectedRole);
                    }
                    _db.SaveChanges();
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при видаленні: {ex.Message}");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                var listRole = _db.Role.Select(i => i.Id);
                if (!listRole.Any())
                {
                    MessageBox.Show("Список посад порожній");
                    return;
                }
                else
                {
                    int selectedId = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                    Form2 add = new Form2(selectedId);
                    add.ShowDialog();
                }
            }
            else
            {
                int selectedId = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                Form3 add = new Form3(selectedId);
                add.ShowDialog();
            }
            RefreshGrid();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 find = new Form4();
            find.ShowDialog();
            RefreshGrid();
        }
    }
}
