using ClassDbContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FluentAPI
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            Text = "Пошук";
            groupBox1.Text = "Критерій";
            radioButton1.Text = "Імя";
            radioButton2.Text = "Прізвище";
            radioButton3.Text = "Посада";
            label1.Text = "Введіть слово";
            button1.Text = "OK";
            button2.Text = "Скасувати";
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Поле пошуку не може бути порожнім");
                return;
            }
            using var db = new HrmDbContext();
            if (radioButton1.Checked)
            {
                var result = db.Employees
                .Where(i => i.Name.ToLower() == input.ToLower())
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
            else if(radioButton2.Checked)
            {
                var result = db.Employees
                    .Where(i => i.Surname.ToLower() == input.ToLower())
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
            else if (radioButton3.Checked)
            {
                var result = db.Role
                    .Where(i => i.Title.ToLower() == input.ToLower())
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
    }
}
