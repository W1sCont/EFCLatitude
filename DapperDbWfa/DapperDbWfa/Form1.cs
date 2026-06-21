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
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            _connectingString = connectionString;
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

        private void RefreshGrid()
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                var result = connection.Query<CustomerViewModel>("SELECT FullName, BirthDate, Gender, Email FROM Customers").ToList();
                dataGridView1.DataSource = result;
                dataGridView1.Columns["FullName"].HeaderText = "ПІБ Клієнта";
                dataGridView1.Columns["BirthDate"].HeaderText = "Дата народження";
                dataGridView1.Columns["Gender"].HeaderText = "Гендер";
                dataGridView1.Columns["Email"].HeaderText = "Електронна пошта";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void allCustomersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void allEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
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
                dataGridView1.Columns["DiscountPercent"].HeaderText = "Знижка %";
                dataGridView1.Columns["CountryName"].HeaderText = "Країна";
                dataGridView1.Columns["StartDate"].HeaderText = "Початок акції";
                dataGridView1.Columns["EndDate"].HeaderText = "Завершення акції";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void citysToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                var sql = @"SELECT CityName FROM dbo.Cities";
                var result = connection.Query<CitiesViewModedl>(sql).ToList();
                dataGridView1.DataSource = result;
                dataGridView1.Columns["CityName"].HeaderText = "Назва міста";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void countrysToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                var sql = @"SELECT CountryName FROM dbo.Countries";
                var result = connection.Query<CountriesViewModel>(sql).ToList();
                dataGridView1.DataSource = result;
                dataGridView1.Columns["CountryName"].HeaderText = "Назва країн";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void fromCityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                int cityId;
                var resultCity = connection.Query<CityIdViewModel>("SELECT CityID, CityName FROM dbo.Cities").ToList();
                string sql = @"SELECT FullName, BirthDate, Gender, Email 
                    FROM dbo.Customers
                    WHERE CityID = @TargetCityId";
                Form3 dw = new Form3(resultCity);
                if (dw.ShowDialog() == DialogResult.OK)
                {
                    cityId = dw.SelectedId;
                    var result = connection.Query<CustomerViewModel>(sql, new { TargetCityId = cityId }).ToList();
                    dataGridView1.DataSource = result;
                    dataGridView1.Columns["FullName"].HeaderText = "ПІБ Клієнта";
                    dataGridView1.Columns["BirthDate"].HeaderText = "Дата народження";
                    dataGridView1.Columns["Gender"].HeaderText = "Гендер";
                    dataGridView1.Columns["Email"].HeaderText = "Електронна пошта";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void fromCountryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                int countryId;
                var resultCountry = connection.Query<CountryIdViewModel>("SELECT CountryID, CountryName FROM dbo.Countries").ToList();
                string sql = @"SELECT c.FullName, c.BirthDate, c.Gender, c.Email 
                    FROM dbo.Customers c 
                    INNER JOIN dbo.Cities city ON c.CityID = city.CityID
                    INNER JOIN dbo.Countries countr ON city.CountryID = countr.CountryID
                    WHERE countr.CountryID = @TargetCountryId";
                Form3 dw = new Form3(resultCountry);
                if (dw.ShowDialog() == DialogResult.OK)
                {
                    countryId = dw.SelectedId;
                    var result = connection.Query<CustomerViewModel>(sql, new { TargetCountryId = countryId }).ToList();
                    dataGridView1.DataSource = result;
                    dataGridView1.Columns["FullName"].HeaderText = "ПІБ Клієнта";
                    dataGridView1.Columns["BirthDate"].HeaderText = "Дата народження";
                    dataGridView1.Columns["Gender"].HeaderText = "Гендер";
                    dataGridView1.Columns["Email"].HeaderText = "Електронна пошта";
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void forCountyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var connection = new SqlConnection(_connectingString);
                connection.Open();
                int countryId;
                var resultCountry = connection.Query<CountryIdViewModel>("SELECT CountryID, CountryName FROM dbo.Countries").ToList();
                string sql = @"SELECT g.GoodName, p.DiscountPercent,  p.StartDate, p.EndDate
                    FROM dbo.Promotions p
                    INNER JOIN dbo.Goods g ON p.GoodID = g.GoodID
                    WHERE p.CountryID = @TargetCountryId";
                Form3 dw = new Form3(resultCountry);
                if (dw.ShowDialog() == DialogResult.OK)
                {
                    countryId = dw.SelectedId;
                    var result = connection.Query<CountryPromotionViewModel>(sql, new { TargetCountryId = countryId }).ToList();
                    dataGridView1.DataSource = result;
                    dataGridView1.Columns["GoodName"].HeaderText = "Назва товару";
                    dataGridView1.Columns["DiscountPercent"].HeaderText = "Знижка (%)";
                    dataGridView1.Columns["StartDate"].HeaderText = "Початок акції";
                    dataGridView1.Columns["EndDate"].HeaderText = "Завершення акції";
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 addCustomer = new Form4(_connectingString);
            if (addCustomer.ShowDialog() == DialogResult.OK)
            {
                RefreshGrid();
            }
        }

        private void countryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 addCountry = new Form5(_connectingString, "Country");
            if (addCountry.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void countryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form5 editCountry = new Form5(_connectingString, "Country");
            if (editCountry.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void cityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 addCity = new Form5(_connectingString, "City");
            if (addCity.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void cityToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form5 editCity = new Form5(_connectingString, "City");
            if (editCity.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 category = new Form5(_connectingString, "Category");
            if (category.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void promotionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 promotion = new Form6(_connectingString);
            if (promotion.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
