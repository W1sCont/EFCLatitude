using ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WarehouseDbContext;

namespace Warehouse
{
    public partial class Form3 : Form
    {
        private readonly ClassDbContext _db;
        private readonly string _mode;
        private readonly int? _editId;

        public Form3(string mode)
        {
            InitializeComponent();
            _db = new ClassDbContext();
            _mode = mode;
            _editId = null;
            Text = (_mode == "Type") ? "Додати категорію" : "Додати постачальника";
            label1.Text = (_mode == "Type") ? "Введіть назву категорії" : "Введіть назву постачальника";
            button1.Text = "OK";
            button2.Text = "Скасувати";
        }
        public Form3(string mode, int? editId)
        {
            InitializeComponent();
            _db = new ClassDbContext();
            _mode = mode;
            _editId = editId;

            if (_mode == "Supplier")
            {
                Text = _editId == null ? "Додавання постачальника" : "Редагування постачальника";
                label1.Text = "Введіть назву постачальника";
                button1.Text = "OK";
                button2.Text = "Скасувати";
            }
            else if (_mode == "Type")
            {
                Text = _editId == null ? "Додавання категорії" : "Редагування категорії";
                label1.Text = "Введіть назву категорії";
                button1.Text = "OK";
                button2.Text = "Скасувати";
            }

            if (_editId != null)
            {
                if (_mode == "Type")
                {
                    var type = _db.Types.Find(_editId.Value);
                    if (type != null) textBox1.Text = type.Name;
                }
                else if (_mode == "Supplier")
                {
                    var supplier = _db.Suppliers.Find(_editId.Value);
                    if (supplier != null) textBox1.Text = supplier.Name;
                }
            }
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

                if (_editId == null)
                {
                    if (_mode == "Type")
                    {
                        bool exists = _db.Types.Any(t => t.Name.ToLower() == inputName.ToLower());
                        if (exists)
                        {
                            MessageBox.Show("Категорія з такою назвою вже існує! Дублювання заборонено.",
                                            "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        var newType = new ClassTypeOfGood { Name = textBox1.Text };
                        _db.Types.Add(newType);
                    }
                    else if (_mode == "Supplier")
                    {
                        bool exists = _db.Suppliers.Any(s => s.Name.ToLower() == inputName.ToLower());
                        if (exists)
                        {
                            MessageBox.Show("Постачальник з такою назвою вже існує! Дублювання заборонено.",
                                            "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        var newSupplier = new ClassSupplier { Name = textBox1.Text };
                        _db.Suppliers.Add(newSupplier);
                    }
                }
                else
                {
                    if (_mode == "Type")
                    {
                        var typeToEdit = _db.Types.Find(_editId.Value);
                        if (typeToEdit != null) typeToEdit.Name = textBox1.Text;
                    }
                    else if (_mode == "Supplier")
                    {
                        var supplierToEdit = _db.Suppliers.Find(_editId.Value);
                        if (supplierToEdit != null) supplierToEdit.Name = textBox1.Text;
                    }
                }

                _db.SaveChanges();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
