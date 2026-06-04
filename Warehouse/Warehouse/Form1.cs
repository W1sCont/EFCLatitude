using Microsoft.EntityFrameworkCore;
using WarehouseDbContext;

namespace Warehouse
{
    public partial class Form1 : Form
    {
        private readonly ClassDbContext db;
        public enum CurrentViewMode
        {
            Goods,
            Types,
            Suppliers,
            Stats
        }
        private CurrentViewMode _currentMode = CurrentViewMode.Goods;
        public Form1()
        {
            InitializeComponent();
            db = new ClassDbContext();

            Text = "Склад";
            // grid config
            dataGridView1.Visible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Alt
            this.KeyPreview = true;

            //status strip
            toolStripStatusLabel1.Text = "[Alt + A] Додати товар";
            toolStripStatusLabel2.Text = "[Alt + E] Редагувати виділений";
            toolStripStatusLabel3.Text = "[Alt + D] Видалити виділений";


            try
            {
                // db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                PopulateSupplierFilterMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка створення бази: {ex.Message}");
            }
        }

        // update grid
        private void RefreshCurrentGrid()
        {
            switch (_currentMode)
            {
                case CurrentViewMode.Goods:
                    ShowAllGoods();
                    UpdateStatusLabels(CurrentViewMode.Goods);
                    break;
                case CurrentViewMode.Types:
                    ShowAllTypes();
                    UpdateStatusLabels(CurrentViewMode.Types);
                    break;
                case CurrentViewMode.Suppliers:
                    ShowAllSuppliers();
                    UpdateStatusLabels(CurrentViewMode.Suppliers);
                    break;
            }
        }
        private void ShowAllGoods()
        {
            var result = db.Goods
                .Select(g => new
                {
                    g.Id,
                    Назва = g.Name,
                    Кількість = g.Count,
                    Ціна = g.NettoPrice,
                    Дата = g.DateOfDelivery,
                    Категорія = g.TypeOfGood != null ? g.TypeOfGood.Name : "Немає",
                    Постачальник = g.Supplier != null ? g.Supplier.Name : "Немає"
                })
                .ToList();
            dataGridView1.DataSource = result;
            dataGridView1.Visible = true;

            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
        }
        private void ShowAllTypes()
        {
            var result = db.Types
                .Select(t => new { t.Id, Категорія = t.Name })
                .ToList();
            dataGridView1.DataSource = result;
            dataGridView1.Visible = true;

            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
        }
        private void ShowAllSuppliers()
        {
            var result = db.Suppliers
                .Select(s => new { s.Id, Постачальник = s.Name })
                .ToList();
            dataGridView1.DataSource = result;
            dataGridView1.Visible = true;

            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
        }

        private void UpdateStatusLabels(CurrentViewMode mode)
        {
            statusStrip1.Visible = true;
            toolStripStatusLabel1.Visible = true;
            toolStripStatusLabel2.Visible = true;
            toolStripStatusLabel3.Visible = true;

            switch (mode)
            {
                case CurrentViewMode.Goods:
                    toolStripStatusLabel1.Text = "[Alt + A] Додати товар";
                    toolStripStatusLabel2.Text = "[Alt + E] Редагувати виділений";
                    toolStripStatusLabel3.Text = "[Alt + D] Видалити виділений";
                    break;
                case CurrentViewMode.Types:
                    toolStripStatusLabel1.Text = "[Alt + A] Додати категорію";
                    toolStripStatusLabel2.Text = "[Alt + E] Редагувати категорію";
                    toolStripStatusLabel3.Text = "[Alt + D] Видалити категорію (і товари!)";
                    break;
                case CurrentViewMode.Suppliers:
                    toolStripStatusLabel1.Text = "[Alt + A] Додати постачальника";
                    toolStripStatusLabel2.Text = "[Alt + E] Редагувати постачальника";
                    toolStripStatusLabel3.Text = "[Alt + D] Видалити постачальника (і товари!)";
                    break;
                case CurrentViewMode.Stats:
                    toolStripStatusLabel1.Visible = false;
                    toolStripStatusLabel2.Visible = false;
                    toolStripStatusLabel3.Visible = true;
                    toolStripStatusLabel3.Text = "Режим перегляду звіту (Редагування недоступне)";
                    break;
            }
        }
        // 
        // key and func
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        if (_currentMode == CurrentViewMode.Goods) додатиToolStripMenuItem_Click(sender, e);
                        else if (_currentMode == CurrentViewMode.Types) додатиToolStripMenuItem1_Click(sender, e);
                        else if (_currentMode == CurrentViewMode.Suppliers) додатиToolStripMenuItem2_Click(sender, e);
                        e.Handled = true;
                        break;

                    case Keys.E:
                        EditSelectedProduct();
                        e.Handled = true;
                        break;

                    case Keys.D:
                        DeleteSelectedProduct();
                        e.Handled = true;
                        break;
                }
            }
        }
        private void EditSelectedProduct()
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть рядок у таблиці для редагування!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dataGridView1.Columns["Id"] == null)
            {
                MessageBox.Show("Редагування неможливе для поточної вибірки даних.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridView1.CurrentRow != null)
            {
                try
                {
                    if (dataGridView1.CurrentRow == null)
                    {
                        MessageBox.Show("Будь ласка, виділіть рядок у таблиці для редагування!");
                        return;
                    }

                    if (dataGridView1.Columns["Id"] == null)
                    {
                        MessageBox.Show("Редагування неможливе для поточної вибірки.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    int selectedId = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

                    switch (_currentMode)
                    {
                        case CurrentViewMode.Goods:
                            Form2 editGoodForm = new Form2(selectedId);
                            if (editGoodForm.ShowDialog() == DialogResult.OK)
                            {
                                RefreshCurrentGrid();
                            }
                            break;

                        case CurrentViewMode.Types:
                            Form3 editTypeForm = new Form3("Type", selectedId);
                            if (editTypeForm.ShowDialog() == DialogResult.OK)
                            {
                                RefreshCurrentGrid();
                            }
                            break;

                        case CurrentViewMode.Suppliers:
                            Form3 editSupplierForm = new Form3("Supplier", selectedId);
                            if (editSupplierForm.ShowDialog() == DialogResult.OK)
                            {
                                RefreshCurrentGrid();
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не вдалося редагувати рядок. Перевірте, чи виведена таблиця з ID. Помилка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виділіть рядок у таблиці для редагування!");
            }
        }
        private void DeleteSelectedProduct()
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (dataGridView1.CurrentRow == null) return;
                if (dataGridView1.Columns["Id"] == null)
                {
                    MessageBox.Show("Видалення неможливе для цієї вибірки даних.");
                    return;
                }
                var confirmResult = MessageBox.Show("Ви впевнені, що хочете видалити саме це?",
                                                     "Підтвердження видалення",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        int selectedId = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

                        switch (_currentMode)
                        {
                            case CurrentViewMode.Goods:
                                var goodToDelete = db.Goods.Find(selectedId);
                                if (goodToDelete != null)
                                {
                                    db.Goods.Remove(goodToDelete);
                                    db.SaveChanges();
                                    RefreshCurrentGrid();
                                    MessageBox.Show("Видалення виконано успішно!");
                                }
                                break;
                            case CurrentViewMode.Types:
                                var typeToDelete = db.Types.Find(selectedId);
                                if (typeToDelete != null)
                                {
                                    db.Types.Remove(typeToDelete);
                                    db.SaveChanges();
                                    RefreshCurrentGrid();
                                    MessageBox.Show("Видалення виконано успішно!");
                                }
                                break;
                            case CurrentViewMode.Suppliers:
                                var supplierToDelete = db.Suppliers.Find(selectedId);
                                if (supplierToDelete != null)
                                {
                                    db.Suppliers.Remove(supplierToDelete);
                                    db.SaveChanges();
                                    RefreshCurrentGrid();
                                    MessageBox.Show("Видалення виконано успішно!");
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при видаленні: {ex.Message}");
                    }
                }
            }
        }

        private void PopulateSupplierFilterMenu()
        {
            try
            {
                toolStripComboBox1.Items.Clear();

                toolStripComboBox1.Items.Add(new SupplierComboItem { Id = -1, Name = "-- Всі постачальники --" });

                var suppliers = db.Suppliers
                    .Select(s => new SupplierComboItem { Id = s.Id, Name = s.Name })
                    .ToList();

                foreach (var supplier in suppliers)
                {
                    toolStripComboBox1.Items.Add(supplier);
                }

                if (toolStripComboBox1.Items.Count > 0)
                {
                    toolStripComboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка оновлення меню постачальників: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FilterGoodsBySelectedSupplier()
        {
            var selectedItem = toolStripComboBox1.SelectedItem as SupplierComboItem;
            if (selectedItem == null) return;

            if (selectedItem.Id == -1)
            {
                _currentMode = CurrentViewMode.Goods;
                dataGridView1.Visible = true;
                RefreshCurrentGrid();
                return;
            }

            _currentMode = CurrentViewMode.Stats;

            statusStrip1.Visible = true;
            toolStripStatusLabel1.Visible = false;
            toolStripStatusLabel2.Visible = false;
            toolStripStatusLabel3.Visible = true;
            toolStripStatusLabel3.Text = $"Товари від постачальника: {selectedItem.Name}";

            var filteredResult = db.Goods
                .Where(g => g.SupplierId == selectedItem.Id)
                .Select(g => new
                {
                    g.Id,
                    Назва = g.Name,
                    Кількість = g.Count,
                    Ціна = g.NettoPrice,
                    Дата = g.DateOfDelivery,
                    Категорія = g.TypeOfGood != null ? g.TypeOfGood.Name : "Немає"
                }).ToList();

            dataGridView1.DataSource = filteredResult;
            dataGridView1.Visible = true;

            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
        }
        //

        // add product
        private void додатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 addGood = new Form2();
            addGood.ShowDialog();
            _currentMode = CurrentViewMode.Goods;
            RefreshCurrentGrid();
        }

        // add supplier
        private void додатиToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form3 addSupplier = new Form3("Supplier");
            addSupplier.ShowDialog();
            _currentMode = CurrentViewMode.Suppliers;
            RefreshCurrentGrid();
            PopulateSupplierFilterMenu();
        }

        // add type
        private void додатиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 addType = new Form3("Type");
            addType.ShowDialog();
            _currentMode = CurrentViewMode.Types;
            RefreshCurrentGrid();
        }

        // show
        private void всіТовариToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentMode = CurrentViewMode.Goods;
            ShowAllGoods();
            UpdateStatusLabels(_currentMode);
        }

        // всі категорії
        private void середняКстьТоваруToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _currentMode = CurrentViewMode.Types;
            ShowAllTypes();
            UpdateStatusLabels(_currentMode);
        }

        private void всіПостачальникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentMode = CurrentViewMode.Suppliers;
            ShowAllSuppliers();
            UpdateStatusLabels(_currentMode);
        }

        // menu
        private void менюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // dataGridView1.Visible = false;
        }
        private void найстарішийТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var oldest = db.Goods.OrderBy(g => g.DateOfDelivery)
                .Select(g => new { g.Name, g.DateOfDelivery, g.Count })
                .Take(1)
                .ToList();

            dataGridView1.Visible = true;
            dataGridView1.DataSource = oldest;
            _currentMode = CurrentViewMode.Stats;
            UpdateStatusLabels(_currentMode);
        }
        private void середняКстьТоваруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var avrGoods = db.Goods.GroupBy(g => g.TypeOfGood.Name)
                .Select(group => new
                {
                    Category = group.Key ?? "Без категорії",
                    AverageCount = Math.Round(group.Average(g => g.Count), 2)
                }
                ).ToList();
            dataGridView1.Visible = true;
            dataGridView1.DataSource = avrGoods;
            _currentMode = CurrentViewMode.Stats;
            UpdateStatusLabels(_currentMode);
        }
        private void зМаксКсюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int maxCount = db.Goods.Max(g => g.Count);

            dataGridView1.DataSource = db.Goods
                .Where(g => g.Count == maxCount)
                .Select(g => new { g.Name, g.Count })
                .ToList();
            dataGridView1.Visible = true;
            _currentMode = CurrentViewMode.Stats;
            UpdateStatusLabels(_currentMode);
        }

        private void зМінКтюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int minCount = db.Goods.Min(g => g.Count);

            dataGridView1.DataSource = db.Goods
                .Where(g => g.Count == minCount)
                .Select(g => new { g.Name, g.Count })
                .ToList();
            dataGridView1.Visible = true;
            _currentMode = CurrentViewMode.Stats;
            UpdateStatusLabels(_currentMode);
        }

        private void зМінСобівартістюToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            decimal minNettoPrice = db.Goods.Min(g => g.NettoPrice);

            dataGridView1.DataSource = db.Goods
                .Where(g => g.NettoPrice == minNettoPrice)
                .Select(g => new { g.Name, g.NettoPrice })
                .ToList();
            dataGridView1.Visible = true;
            _currentMode = CurrentViewMode.Stats;
            UpdateStatusLabels(_currentMode);
        }

        private void зМаксСобівартістюToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            decimal maxNettoPrice = db.Goods.Max(g => g.NettoPrice);

            dataGridView1.DataSource = db.Goods
                .Where(g => g.NettoPrice == maxNettoPrice)
                .Select(g => new { g.Name, g.NettoPrice })
                .ToList();
            dataGridView1.Visible = true;
            _currentMode = CurrentViewMode.Stats;
            UpdateStatusLabels(_currentMode);
        }
        private void видалитиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _currentMode = CurrentViewMode.Types;
            RefreshCurrentGrid();
        }

        private void видалитиToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _currentMode = CurrentViewMode.Suppliers;
            RefreshCurrentGrid();
            PopulateSupplierFilterMenu();
        }

        private void видалитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentMode = CurrentViewMode.Goods;
            RefreshCurrentGrid();
        }

        private void редагуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentMode = CurrentViewMode.Goods;
            RefreshCurrentGrid();
            UpdateStatusLabels(_currentMode);
        }

        private void редагуватиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _currentMode = CurrentViewMode.Types;
            RefreshCurrentGrid();
            UpdateStatusLabels(_currentMode);
        }

        private void редагуватиToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _currentMode = CurrentViewMode.Suppliers;
            RefreshCurrentGrid();
            UpdateStatusLabels(_currentMode);
            PopulateSupplierFilterMenu();
        }

        private void товариВідПостачальникToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void toolStripComboBox1_SelectedChanged(object sender, EventArgs e)
        {

        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            постачальникиToolStripMenuItem.DropDown.Close();
            FilterGoodsBySelectedSupplier();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }
    }
    public class SupplierComboItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
