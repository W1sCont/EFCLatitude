using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DapperDbWfa
{
    public partial class Form5 : Form
    {
        private int? _id;
        private string _connectingString;
        private readonly string _mode;
        public Form5(string connectingString, string mode)
        {
            InitializeComponent();
            _connectingString = connectingString;
            _mode = mode;

            using var connection = new SqlConnection(_connectingString);
            connection.Open();

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

            if (_mode == "City")
            {
                Text = "Керування містами";
                label1.Text = "Оберіть країну";
                label2.Text = "Введіть нову назву міста";
                comboBox1.Visible = true;
                textBox1.Visible = true;

                comboBox1.DataSource = connection.Query<CountryIdViewModel>("SELECT CountryID, CountryName FROM dbo.Countries").ToList();
                comboBox1.DisplayMember = "CountryName";
                comboBox1.ValueMember = "CountryID";

                int countryId = (int)comboBox1.SelectedValue;
                RefreshGrid(countryId);
            }
            else if (_mode == "Country")
            {
                Text = "Керування країнами";
                label1.Text = " ";
                label2.Text = "Введіть нову назву країни";
                comboBox1.Visible = false;
                textBox1.Visible = true;
                dataGridView1.DataSource = connection.Query<CountryIdViewModel>("SELECT CountryID, CountryName FROM dbo.Countries").ToList();
                dataGridView1.Columns["CountryID"].Visible = false;
                dataGridView1.Columns["CountryName"].HeaderText = "Назва країн";
            }
            else if (_mode == "Category")
            {
                Text = "Керування категоріями";
                label1.Text = " ";
                label2.Text = "Введіть нову назву категорії";
                comboBox1.Visible = false;
                textBox1.Visible = true;
                dataGridView1.DataSource = connection.Query<CategoryIdViewModel>("SELECT CategoryID, CategoryName FROM dbo.Categories").ToList();
                dataGridView1.Columns["CategoryID"].Visible = false;
                dataGridView1.Columns["CategoryName"].HeaderText = "Назва категорії"; 
            }
        }

        private void RefreshGrid(int? countryId = null)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();

                if (_mode == "Country")
                {
                    dataGridView1.DataSource = connection.Query<CountryIdViewModel>("SELECT CountryID, CountryName FROM dbo.Countries").ToList();
                    if (dataGridView1.Columns.Count > 0)
                    {
                        if (dataGridView1.Columns["CountryID"] != null) dataGridView1.Columns["CountryID"].Visible = false;
                        if (dataGridView1.Columns["CountryName"] != null) dataGridView1.Columns["CountryName"].HeaderText = "Назва країни";
                    }
                }
                else if (_mode == "City")
                {
                    var data = connection.Query<CityIdViewModel>("SELECT CityID, CityName FROM dbo.Cities WHERE CountryID = @CountryId", new { CountryId = countryId }).ToList();
                    dataGridView1.DataSource = data;

                    if (dataGridView1.Columns.Count > 0)
                    {
                        if (dataGridView1.Columns["CityID"] != null) dataGridView1.Columns["CityID"].Visible = false;
                        if (dataGridView1.Columns["CityName"] != null) dataGridView1.Columns["CityName"].HeaderText = "Назва міста";
                    }
                }
                else if (_mode == "Category")
                {
                    var data = connection.Query<CategoryIdViewModel>("SELECT CategoryID, CategoryName FROM dbo.Categories").ToList();
                    dataGridView1.DataSource = data;

                    if (dataGridView1.Columns.Count > 0)
                    {
                        if (dataGridView1.Columns["CategoryID"] != null) dataGridView1.Columns["CategoryID"].Visible = false;
                        if (dataGridView1.Columns["CategoryName"] != null) dataGridView1.Columns["CategoryName"].HeaderText = "Назва категорії";
                    }
                }  
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int countryId = (int)comboBox1.SelectedValue;
            RefreshGrid(countryId);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                if (_mode == "Country")
                {
                    _id = (int)row.Cells["CountryID"].Value;
                    textBox1.Text = row.Cells["CountryName"].Value.ToString();
                }
                else if (_mode == "City")
                {
                    _id = (int)row.Cells["CityID"].Value;
                    textBox1.Text = row.Cells["CityName"].Value.ToString();
                }
                else if (_mode == "Category")
                {
                    _id = (int)row.Cells["CategoryID"].Value;
                    textBox1.Text = row.Cells["CategoryName"].Value.ToString();
                }
                else if (_mode == "Promotion")
                {
                    _id = (int)row.Cells["PromotionID"].Value;
                    textBox1.Text = row.Cells["DiscountPercent"].Value?.ToString() ?? "0";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Введіть назву!");
                return;
            }

            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();

                if (_mode == "Country")
                {
                    string countryName = textBox1.Text.Trim();
                    connection.Execute("INSERT INTO dbo.Countries (CountryName) VALUES (@Name)", new { Name = countryName });
                    MessageBox.Show($"Країну '{countryName}' успішно додано!");
                    RefreshGrid();
                }
                else if (_mode == "City")
                {
                    if (comboBox1.SelectedValue is int countryId)
                    {
                        string cityName = textBox1.Text.Trim();
                        connection.Execute("INSERT INTO dbo.Cities (CityName, CountryID) VALUES (@CityName, @CountryId)",
                            new { CityName = cityName, CountryId = countryId });

                        MessageBox.Show($"Місто '{cityName}' успішно додано!");
                        RefreshGrid(countryId);
                    }
                }
                else if (_mode == "Category")
                {
                    try
                    {
                        connection.Execute("INSERT INTO dbo.Categories (CategoryName) VALUES (@Name)", new { Name = textBox1.Text.Trim() });
                        MessageBox.Show("Категорію успішно додано!");
                        RefreshGrid();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }

                textBox1.Clear();
                _id = null;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (_id == null)
            {
                MessageBox.Show("Будь ласка, спочатку оберіть рядок у таблиці!");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;

            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();

                if (_mode == "Country")
                {
                    string newName = textBox1.Text.Trim();
                    connection.Execute("UPDATE dbo.Countries SET CountryName = @Name WHERE CountryID = @Id",
                        new { Name = newName, Id = _id });

                    MessageBox.Show("Назву країни успішно змінено!");
                    RefreshGrid();
                }
                else if (_mode == "City")
                {
                    if (comboBox1.SelectedValue is int countryId)
                    {
                        string newName = textBox1.Text.Trim();
                        connection.Execute("UPDATE dbo.Cities SET CityName = @CityName, CountryID = @CountryId WHERE CityID = @CityId",
                            new { CityName = newName, CountryId = countryId, CityId = _id });

                        MessageBox.Show("Назву міста успішно змінено!");
                        RefreshGrid(countryId);
                    }
                }
                else if (_mode == "Category")
                {
                    string newName = textBox1.Text.Trim();
                  
                    connection.Execute("UPDATE dbo.Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryId",
                        new { CategoryName = newName, CategoryId = _id });

                    MessageBox.Show("Назву категорії успішно змінено!");
                    RefreshGrid();
                }
               
                textBox1.Clear();
                _id = null;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_id == null)
            {
                MessageBox.Show("Будь ласка, спочатку оберіть рядок у таблиці!");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;

            var confirmResult = MessageBox.Show("Ви впевнені, що хочете видалити цей запис? Цю дію не можна буде скасувати.",
                "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes) return;

            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();

                if (_mode == "Country")
                {
                    string sql = "DELETE FROM dbo.Countries WHERE CountryID = @Id";
                    connection.Execute(sql, new { Id = _id });
                    MessageBox.Show("Країну успішно видалено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                }
                else if (_mode == "City")
                {
                    if (comboBox1.SelectedValue is int countryId)
                    {
                        string sql = "DELETE FROM dbo.Cities WHERE CityID = @Id";
                        connection.Execute(sql, new { Id = _id });

                        MessageBox.Show("Місто успішно видалено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshGrid(countryId);
                    }         
                }
                else if (_mode == "Category")
                {
                    string sql = "DELETE FROM dbo.Categories WHERE CategoryID = @Id";
                    connection.Execute(sql, new { Id = _id });

                    MessageBox.Show("Категорію успішно видалено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            textBox1.Clear();
            _id = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
