using Dapper;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using ViewModels;

namespace DapperDbWfa
{
    public partial class Form1 : Form
    {
        private string _connectingString;
        public Form1(string connectionString)
        {
            InitializeComponent();
            Text = "";

            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            _connectingString = connectionString;

        }

        private void RefreshGrid()
        {

        }

        private void allCustomersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                // (localdb)\MSSQLLocalDB
                var result = connection.Query<CustomerViewModel>("SELECT FullName, BirthDate, Gender, Email FROM Customers").ToList();
                dataGridView1.DataSource = result;
                dataGridView1.Columns["FullName"].HeaderText = "ПІБ Клієнта";
                dataGridView1.Columns["BirthDate"].HeaderText = "Дата народження";
                dataGridView1.Columns["Gender"].HeaderText = "Гендер";
                dataGridView1.Columns["Email"].HeaderText = "Електронна пошта";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void allEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                // (localdb)\MSSQLLocalDB
                var result = connection.Query<EmailViewModel>("SELECT Email FROM dbo.Customers").ToList();
                dataGridView1.DataSource = result;
                dataGridView1.Columns["Email"].HeaderText = "Електронна пошта";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                // (localdb)\MSSQLLocalDB
                var result = connection.Query<CategoryViewModel>("SELECT CategoryName FROM dbo.Categories").ToList();
                dataGridView1.DataSource = result;
                dataGridView1.Columns["CategoryName"].HeaderText = "Назва категорії";

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void promocionalProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                // (localdb)\MSSQLLocalDB
                var sql = @"SELECT g.GoodName, 
                   p.DiscountPercent, 
                   c.CountryName, 
                   p.StartDate, 
                   p.EndDate
                FROM dbo.Promotions p
                INNER JOIN dbo.Goods g ON p.GoodID = g.GoodID
                INNER JOIN dbo.Countries c ON p.CountryID = c.CountryID";
                var result = connection.Query<PromotionalViewModel>(sql).ToList();
                dataGridView1.DataSource = result;
                dataGridView1.Columns["GoodName"].HeaderText = "Назва товару";
                dataGridView1.Columns["DiscountPercent"].HeaderText = "Знижка";
                dataGridView1.Columns["CountryName"].HeaderText = "Країна";
                dataGridView1.Columns["StartDate"].HeaderText = "Початок акції";
                dataGridView1.Columns["EndDate"].HeaderText = "Завершення акції";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
