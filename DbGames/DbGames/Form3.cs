using ClassLibrary;
using GamesDbContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DbGames
{
    public partial class Form3 : Form
    {
        private readonly GameDbContext _db;
        int? _id;
        public Form3() : this(null) { }
        public Form3(int? id)
        {
            InitializeComponent();
            _db = new GameDbContext();
            _id = id;
            Text = _id != null ? "Редагування студії" : "Додавання студії";
            label1.Text = "Введіть назву студії";
            button1.Text = "ОК";
            button2.Text = "Скачувати";
            if (id != null) 
            {
                var result = _db.Studios.Find(_id);
                if (result != null)
                {
                    textBox1.Text = result.Name;
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            string? inputName;
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Поле з назвою студії не може дути порожнім");
                return;
            }
            inputName = textBox1.Text.Trim();
            if (_id != null)
            {
                var result = _db.Studios.Find(_id);
                result?.Name = inputName;
            }
            else
            {
                Studio newStudio = new Studio { Name = inputName };
                _db.Studios.Add(newStudio);
            }
            _db.SaveChanges();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
