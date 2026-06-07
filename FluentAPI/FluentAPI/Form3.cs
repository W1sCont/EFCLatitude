using ClassDbContext;
using ClassLibrary;

namespace FluentAPI
{
    public partial class Form3 : Form
    {
        private readonly int? _id;

        public Form3() : this(null) { }
        public Form3(int? id)
        {
            InitializeComponent();
            _id = id;
            Text = _id == null ? "Додавання посади" : "Редагування посади";
            Load += FormAdd_Load;
        }
        private void FormAdd_Load(object? sender, EventArgs e)
        {
            using var db = new HrmDbContext();
            label1.Text = "Назва посади";
            button1.Text = "OK";
            button2.Text = "Скасувати";

            if (_id != null)
            {
                var role = db.Role.Find(_id.Value);
                if (role != null)
                {
                    textBox1.Text = role.Title;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var db = new HrmDbContext();
            string inputTitle = textBox1.Text.Trim();
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Введіть назву посади!");
                    return;
                }

                if (_id == null)
                {
                    var newRole = new JobTitle
                    {
                        Title = inputTitle
                    };
                    db.Role.Add(newRole);
                }
                else
                {
                    var editRole = db.Role.Find(_id.Value);
                    if (editRole != null)
                    {
                        editRole.Title = inputTitle;
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
