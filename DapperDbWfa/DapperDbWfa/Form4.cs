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

namespace DapperDbWfa
{
    public partial class Form4 : Form
    {
        private int? _id;
        private string _connectingString;
        public Form4(string connectingString) : this(connectingString, null) { }
        public Form4(string connectingString, int? id)
        {
            InitializeComponent();
            _connectingString = connectingString;
            _id = id;
            List<char> gend = new List<char> { 'F', 'M' };

            using var connection = new SqlConnection(_connectingString);
            connection.Open();

            Text = _id == null ? "Додавання" : "Редагування";
            label1.Text = "Прізвище та Ім'я";
            label2.Text = "Дата народження";
            label3.Text = "Гендер";
            label4.Text = "Емейл";
            label5.Text = "Місто";
            label6.Text = "Країна";

            comboBox1.DataSource = gend;

            comboBox3.DataSource = connection.Query<CountryIdViewModel>("SELECT CountryID, CountryName FROM dbo.Countries").ToList();
            comboBox3.DisplayMember = "CountryName";
            comboBox3.ValueMember = "CountryID";

            var countryId = comboBox3.SelectedValue;
            comboBox2.DataSource = connection.Query<CityIdViewModel>("SELECT CityID, CityName FROM dbo.Cities WHERE CountryID = @CountryId",
                new { CountryId = countryId }).ToList();
            comboBox2.DisplayMember = "CityName";
            comboBox2.ValueMember = "CityID";

            button1.Text = "OK";
            button2.Text = "Скасувати";
            button3.Text = "+";
            button4.Text = "+";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using var connection = new SqlConnection(_connectingString);
            connection.Open();
            var countryId = comboBox3.SelectedValue;
            comboBox2.DataSource = connection.Query<CityIdViewModel>("SELECT CityID, CityName FROM dbo.Cities WHERE CountryID = @CountryId",
                new { CountryId = countryId }).ToList();
            comboBox2.DisplayMember = "CityName";
            comboBox2.ValueMember = "CityID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var connection = new SqlConnection(_connectingString);
            connection.Open();

            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Поле Прізвище та Ім'я не може бути порожнім!");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Поле Емейл не може бути порожнім!");
                return;
            }

            try
            {
                if (_id == null)
                {
                    string insertSql = @" INSERT INTO dbo.Customers (FullName, BirthDate, Gender, Email, CityID) 
                        VALUES (@FullName, @BirthDate, @Gender, @Email, @CityID)";

                    connection.Execute(insertSql, new
                    {
                        FullName = textBox1.Text.Trim(),
                        BirthDate = dateTimePicker1.Value,
                        Gender = comboBox1.Text,
                        Email = textBox2.Text.Trim(),
                        CityID = (int)comboBox2.SelectedValue
                    });

                    MessageBox.Show("Покупця успішно додано!");
                }
                else
                {
                    string updateSql = @"UPDATE dbo.Customers SET FullName = @FullName, BirthDate = @BirthDate,
                        Gender = @Gender, Email = @Email, CityID = @CityID 
                        WHERE CustomerID = @CustomerID";

                    connection.Execute(updateSql, new
                    {
                        FullName = textBox1.Text.Trim(),
                        BirthDate = dateTimePicker1.Value,
                        Gender = comboBox1.Text,
                        Email = textBox2.Text.Trim(),
                        CityID = (int)comboBox2.SelectedValue,
                        CustomerID = _id
                    });

                    MessageBox.Show("Дані покупця оновлено!");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка бази даних: {ex.Message}");
            }
        }

        
    }
}
