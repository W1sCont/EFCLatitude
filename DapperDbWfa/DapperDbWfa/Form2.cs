using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;


namespace DapperDbWfa
{
    public partial class Form2 : Form
    {
        public string ConnectionString { get; private set; }
        public Form2()
        {
            InitializeComponent();
            Text = "Підключення до сервера";
            label1.Text = "Введіть назву вашого локального сервера:";
            button1.Text = "OK";
            button2.Text = "Cancel";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Назва сервера не може бути порожньою!");
                    return;
                }
                ConnectionString = $@"Server={textBox1.Text.Trim()};Database=DapperDB;Integrated Security=SSPI;TrustServerCertificate=true;MultipleActiveResultSets=True;";
                using var connection = new SqlConnection(ConnectionString);
                try
                {
                    connection.Open();
                    // (localdb)\MSSQLLocalDB
                    MessageBox.Show("Підключення успішне!");
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch (Exception ex) { MessageBox.Show($"Помилка підключення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
