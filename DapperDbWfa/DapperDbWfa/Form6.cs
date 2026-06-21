using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DapperDbWfa
{
    public partial class Form6 : Form
    {
        private string _connectingString;
        private int? _id;
        public Form6(string connectingString)
        {
            InitializeComponent();
            _connectingString = connectingString;
            Text = "Керування акціями";
            label1.Text = "Оберіть товар";
            label2.Text = "Оберіть країну";
            label3.Text = "Знижка, %";
            label4.Text = "Дата початку";
            label5.Text = "Дата завершення";

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            button1.Text = "Додати";
            button2.Text = "Редагувати";
            button3.Text = "Видалити";
            button4.Text = "Скасувати";

            using var connection = new SqlConnection(_connectingString);
            connection.Open();

            comboBox1.DataSource = connection.Query("SELECT GoodID, GoodName FROM dbo.Goods").ToList();
            comboBox1.DisplayMember = "GoodName";
            comboBox1.ValueMember = "GoodID";

            comboBox2.DataSource = connection.Query("SELECT CountryID, CountryName FROM dbo.Countries").ToList();
            comboBox2.DisplayMember = "CountryName";
            comboBox2.ValueMember = "CountryID";

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            string sql = @"SELECT p.PromotionID, p.GoodID, g.GoodName, g.Price,p.CountryID, c.CountryName, 
                        p.DiscountPercent, p.StartDate, p.EndDate 
                        FROM dbo.Promotions p
                        INNER JOIN dbo.Countries c ON p.CountryID = c.CountryID
                        INNER JOIN dbo.Goods g ON p.GoodID = g.GoodID";
            using var connection = new SqlConnection(_connectingString);
            connection.Open();

            dataGridView1.DataSource = connection.Query<PromotionIdViewModel>(sql).ToList();

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["PromotionID"].Visible = false;
                dataGridView1.Columns["GoodID"].Visible = false;
                dataGridView1.Columns["CountryID"].Visible = false;

                dataGridView1.Columns["GoodName"].HeaderText = "Товар";
                dataGridView1.Columns["Price"].HeaderText = "Ціна";
                dataGridView1.Columns["CountryName"].HeaderText = "Країна";
                dataGridView1.Columns["DiscountPercent"].HeaderText = "Знижка %";
                dataGridView1.Columns["StartDate"].HeaderText = "Початок";
                dataGridView1.Columns["EndDate"].HeaderText = "Кінець";
            }
        }

        private void ResetInputs()
        {
            _id = null;
            numericUpDown1.Value = 0;
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today.AddDays(7);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                _id = (int)row.Cells["PromotionID"].Value;

                comboBox1.SelectedValue = (int)row.Cells["GoodID"].Value;
                comboBox2.SelectedValue = (int)row.Cells["CountryID"].Value;

                numericUpDown1.Text = row.Cells["DiscountPercent"].Value?.ToString() ?? "0";
                dateTimePicker1.Value = (DateTime)row.Cells["StartDate"].Value;
                dateTimePicker2.Value = (DateTime)row.Cells["EndDate"].Value;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                MessageBox.Show("Дата завершення акції не може бути ранішою за дату її початку!",
                                "Помилка валідації дат",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();

                if (comboBox1.SelectedValue is int goodId && comboBox2.SelectedValue is int countryId)
                {
                    decimal discount = numericUpDown1.Value;

                    string sql = @"INSERT INTO dbo.Promotions (GoodID, CountryID, DiscountPercent, StartDate, EndDate) 
                            VALUES (@GoodId, @CountryId, @Discount, @Start, @End)";

                    connection.Execute(sql, new
                    {
                        GoodId = goodId,
                        CountryId = countryId,
                        Discount = (int)numericUpDown1.Value,
                        Start = dateTimePicker1.Value.Date,
                        End = dateTimePicker2.Value.Date
                    });

                    MessageBox.Show("Промоцію успішно додано!");
                    RefreshGrid();
                    ResetInputs();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();

                if (comboBox1.SelectedValue is int goodId && comboBox2.SelectedValue is int countryId)
                {
                    decimal discount = numericUpDown1.Value;

                    string sql = @"UPDATE dbo.Promotions 
                            SET GoodID = @GoodId, CountryID = @CountryId, DiscountPercent = @Discount, StartDate = @Start, EndDate = @End
                            WHERE PromotionID = @PromoId";

                    connection.Execute(sql, new
                    {
                        GoodId = goodId,
                        CountryId = countryId,
                        Discount = (int)numericUpDown1.Value,
                        Start = dateTimePicker1.Value.Date,
                        End = dateTimePicker2.Value.Date,
                        PromoId = _id
                    });

                    MessageBox.Show("Промоцію успішно змінено!");
                    RefreshGrid();
                    ResetInputs();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_id == null)
            {
                MessageBox.Show("Будь ласка, спочатку оберіть акцію у таблиці!");
                return;
            }

            var confirmResult = MessageBox.Show("Ви впевнені, що хочете видалити цю акцію?", "Підтвердження", MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes) return;

            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();

                if (comboBox1.SelectedValue is int countryId)
                {
                    string sql = "DELETE FROM dbo.Promotions WHERE PromotionID = @Id";
                    connection.Execute(sql, new { Id = _id });

                    MessageBox.Show("Промоцію успішно видалено!");
                    RefreshGrid();
                    ResetInputs();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
        }
    }
}
