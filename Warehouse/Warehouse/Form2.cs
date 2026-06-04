using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ClassLibrary;
using WarehouseDbContext;

namespace Warehouse
{
    public partial class Form2 : Form
    {
        private readonly ClassDbContext _db;
        private readonly int? _goodId;

        public Form2() : this(null)
        {
        }
        public Form2(int? goodId)
        {
            InitializeComponent();
            _db = new ClassDbContext();
            _goodId = goodId;
            Text = _goodId == null ? "Додавання товару" : "Редагування товару";
            this.Load += FormAddProduct_Load;
        }
        private void FormAddProduct_Load(object? sender, EventArgs e)
        {
            // label
            label1.Text = "Введіть назву товару";
            label2.Text = "Введіть кількість";
            label3.Text = "Введіть собівартість";
            label4.Text = "Введіть дату достави";
            label5.Text = "Виберіть тип";
            label6.Text = "Виберіть постачальника";
            button1.Text = "ОК";
            button2.Text = "Скасувати";

            // Type
            var typesList = _db.Types.Select(t => new { Id = (int?)t.Id, Name = t.Name }).ToList();
            typesList.Insert(0, new { Id = (int?)null, Name = "Не обрано" });

            comboBox1.DataSource = typesList;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";

            // Supplier
            var suppliersList = _db.Suppliers.Select(s => new { Id = (int?)s.Id, Name = s.Name }).ToList();
            suppliersList.Insert(0, new { Id = (int?)null, Name = "Не обрано" });

            comboBox2.DataSource = suppliersList;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";

            if (_goodId != null)
            {
                var good = _db.Goods.Find(_goodId.Value);
                if (good != null)
                {
                    textBox1.Text = good.Name;
                    numericUpDown1.Value = good.Count;
                    numericUpDown2.Value = good.NettoPrice;
                    dateTimePicker1.Value = good.DateOfDelivery;
                    comboBox1.SelectedValue = typesList;
                    comboBox2.SelectedValue = suppliersList;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputName = textBox1.Text.Trim();
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Введіть назву!");
                    return;
                }
                if (numericUpDown2.Value == 0)
                {
                    MessageBox.Show("Введіть вартість!");
                    return;
                }

                int? selectedTypeId = (int?)comboBox1.SelectedValue;
                int? selectedSupplierId = (int?)comboBox2.SelectedValue;

                if (_goodId == null)
                {
                    var existingGood = _db.Goods.FirstOrDefault(g =>
                        g.Name.ToLower() == inputName.ToLower() &&
                        g.SupplierId == selectedSupplierId);
                    if (existingGood != null)
                    {
                        var result = MessageBox.Show(
                            $"Товар '{inputName}' вже є в базі!\n\n" +
                            $"[ТАК] — Просто додати кількість (+{(int)numericUpDown1.Value} шт.) до існуючого?\n" +
                            $"[НІ] — Повернутися назад.\n",
                            "Знайдено дублікат",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            existingGood.Count += (int)numericUpDown1.Value;
                            _db.SaveChanges();

                            DialogResult = DialogResult.OK;
                            return;
                        }
                        else if (result == DialogResult.No)
                        {
                            return;
                        }
                    }
                
                    var newGood = new ClassGoods
                    {
                        Name = textBox1.Text,
                        Count = (int)numericUpDown1.Value,
                        NettoPrice = numericUpDown2.Value,
                        DateOfDelivery = dateTimePicker1.Value,
                        TypeOfGoodId = selectedTypeId,
                        SupplierId = selectedSupplierId
                    };
                    _db.Goods.Add(newGood);
                }
                else
                {
                    var editGood = _db.Goods.Find(_goodId.Value);
                    if (editGood != null)
                    {
                        editGood.Name = textBox1.Text;
                        editGood.Count = (int)numericUpDown1.Value;
                        editGood.NettoPrice = (int)numericUpDown2.Value;
                        editGood.DateOfDelivery = dateTimePicker1.Value;
                        editGood.TypeOfGoodId = selectedTypeId;
                        editGood.SupplierId = selectedSupplierId;
                    }
                }

                _db.SaveChanges();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }
    }
}
