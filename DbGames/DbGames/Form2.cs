using ClassLibrary;
using GamesDbContext;

namespace DbGames
{
    public partial class Form2 : Form
    {
        private readonly GameDbContext _db;
        private int? _id;
        public Form2() : this(null) { }
        public Form2(int? id)
        {
            InitializeComponent();
            _db = new GameDbContext();
            _id = id;
            Text = _id != null ? "Форма редагування" : "Форма додавання";
            label1.Text = "Введіть назву";
            label2.Text = "Введіть стиль";
            label3.Text = "Виберіть студію";
            label4.Text = "Введіть дату";
            label5.Text = "Режим гри";
            label6.Text = "Кількість проданих копій";
            button1.Text = "OK";
            button2.Text = "Скасувати";
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            comboBox1.DataSource = _db.Studios.ToList();
            comboBox2.DataSource = Enum.GetValues(typeof(GameMode));

            if (_id  != null )
            {
                var editGame = _db.Games.Find(_id);
                if (editGame != null)
                {
                    textBox1.Text = editGame.Name;
                    textBox2.Text = editGame.Style;
                    comboBox1.SelectedValue = editGame.StudioId;
                    dateTimePicker1.Value = editGame.Date;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string inputName = textBox1.Text.Trim();
                string inputStyle = textBox2.Text.Trim();
                int? inputSold = (int?)numericUpDown1.Value;
                int? indexStudio = (int?)comboBox1.SelectedValue;
                if(indexStudio == null)
                {
                    MessageBox.Show("Виберіть студію!");
                    return;
                }
                var selectedStudio = _db.Studios.Find(indexStudio);
                if (string.IsNullOrWhiteSpace(inputName))
                {
                    MessageBox.Show("Введіть назву!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(inputStyle))
                {
                    MessageBox.Show("Введіть стиль!");
                    return;
                }

                if (_id == null)
                {
                    var newGame = new Game
                    {
                        Name = inputName,
                        Style = inputStyle,
                        GameMode = (GameMode)comboBox2.SelectedValue,
                        Sold = inputSold,
                        Date = dateTimePicker1.Value,
                        StudioId = indexStudio
                    };
                    _db.Games.Add(newGame);
                }
                else
                {
                    var editGame = _db.Games.Find(_id);
                    if (editGame != null)
                    {
                        editGame.Name = inputName;
                        editGame.Style = inputStyle;
                        editGame.GameMode = (GameMode)comboBox2.SelectedValue;
                        editGame.Sold = (int)numericUpDown1.Value;
                        editGame.Date = dateTimePicker1.Value;
                        editGame.StudioId = indexStudio;
                    }
                }

                _db.SaveChanges();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
