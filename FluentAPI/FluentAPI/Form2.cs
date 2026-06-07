using ClassDbContext;
using ClassLibrary;
using System.Data;

namespace FluentAPI
{
    public partial class Form2 : Form
    {
        private readonly int? _id;

        public Form2() : this(null) { }
        public Form2(int? id)
        {
            InitializeComponent();
            _id = id;
            Text = _id == null ? "Додавання співробітника" : "Редагування співробітника";
            Load += FormAdd_Load;
        }

        private void FormAdd_Load(object? sender, EventArgs e)
        {
            using var db = new HrmDbContext();
            label1.Text = "Ім'я";
            label2.Text = "Прізвище";
            label3.Text = "Вік";
            label4.Text = "Посада";
            button1.Text = "OK";
            button2.Text = "Скасувати";

            var roleList = db.Role.Select(i => new { Id = i.Id, Title = i.Title }).ToList();

            comboBox1.DataSource = roleList;
            comboBox1.DisplayMember = "Title";
            comboBox1.ValueMember = "Id";
            numericUpDown1.Minimum = 18;
            numericUpDown1.Maximum = 65;
            numericUpDown1.Value = 18;

            if (_id != null)
            {
                var emp = db.Employees.Find(_id.Value);
                if (emp != null)
                {
                    textBox1.Text = emp.Name;
                    textBox2.Text = emp.Surname;
                    numericUpDown1.Value = (int)emp.Age;
                    comboBox1.SelectedValue = emp.JobTitleId;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var db = new HrmDbContext();
            string inputName = textBox1.Text.Trim();
            string inputSurname = textBox2.Text.Trim();
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Введіть Ім'я!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Введіть Прізвище!");
                    return;
                }
                if (numericUpDown1.Value == 0)
                {
                    MessageBox.Show("Введіть вік!");
                    return;
                }
                if (comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Оберіть посаду!");
                    return;
                }

                int selectedRoleId = (int)comboBox1.SelectedValue;

                if (_id == null)
                {
                    var newEmployee = new Employee
                    {
                        Name = inputName,
                        Surname = inputSurname,
                        Age = (int)numericUpDown1.Value,
                        JobTitleId = selectedRoleId
                    };
                    db.Employees.Add(newEmployee);
                }
                else
                {
                    var editEmployee = db.Employees.Find(_id.Value);
                    if (editEmployee != null)
                    {
                        editEmployee.Name = inputName;
                        editEmployee.Surname = inputSurname;
                        editEmployee.Age = (int)numericUpDown1.Value;
                        editEmployee.JobTitleId = selectedRoleId;
                    }
                }

                db.SaveChanges();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
