using Dapper;
using Microsoft.Data.SqlClient;
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
    public partial class Form3 : Form
    {
        public int SelectedId { get; private set; }

        public Form3(List<CountryIdViewModel> ls)
        {
            InitializeComponent();

            Text = "Вибір локації";
            label1.Text = "Виберіть локацію";
            button1.Text = "OK";
            button2.Text = "Скасувати";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DataSource = ls;
            comboBox1.DisplayMember = "CountryName";
            comboBox1.ValueMember = "CountryID";
        }

        public Form3(List<CityIdViewModel> ls)
        {
            InitializeComponent();

            Text = "Вибір локації";
            label1.Text = "Виберіть локацію";
            button1.Text = "OK";
            button2.Text = "Скасувати";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DataSource = ls;
            comboBox1.DisplayMember = "CityName";
            comboBox1.ValueMember = "CityID";
        }

        public Form3(List<CountryPromotionViewModel> ls)
        {
            InitializeComponent();

            Text = "Вибір локації";
            label1.Text = "Виберіть локацію";
            button1.Text = "OK";
            button2.Text = "Скасувати";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DataSource = ls;
            comboBox1.DisplayMember = "CountryName";
            comboBox1.ValueMember = "CountryID";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedId = (int)comboBox1.SelectedValue;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
