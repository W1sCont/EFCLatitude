namespace DapperDbWfa
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            menuStrip1 = new MenuStrip();
            menuToolStripMenuItem = new ToolStripMenuItem();
            addToolStripMenuItem = new ToolStripMenuItem();
            customerToolStripMenuItem = new ToolStripMenuItem();
            countryToolStripMenuItem = new ToolStripMenuItem();
            cityToolStripMenuItem = new ToolStripMenuItem();
            categoryToolStripMenuItem = new ToolStripMenuItem();
            promotionToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            allCustomersToolStripMenuItem1 = new ToolStripMenuItem();
            fromCityToolStripMenuItem = new ToolStripMenuItem();
            fromCountryToolStripMenuItem = new ToolStripMenuItem();
            allEmailToolStripMenuItem = new ToolStripMenuItem();
            categoriesToolStripMenuItem = new ToolStripMenuItem();
            promocionalProductToolStripMenuItem = new ToolStripMenuItem();
            forCountyToolStripMenuItem = new ToolStripMenuItem();
            citysToolStripMenuItem1 = new ToolStripMenuItem();
            countrysToolStripMenuItem1 = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 31);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(776, 407);
            dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            menuToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addToolStripMenuItem, exitToolStripMenuItem });
            menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            menuToolStripMenuItem.Size = new Size(60, 24);
            menuToolStripMenuItem.Text = "Menu";
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { customerToolStripMenuItem, countryToolStripMenuItem, cityToolStripMenuItem, categoryToolStripMenuItem, promotionToolStripMenuItem });
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(224, 26);
            addToolStripMenuItem.Text = "Add/Edit/Remove";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // customerToolStripMenuItem
            // 
            customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            customerToolStripMenuItem.Size = new Size(224, 26);
            customerToolStripMenuItem.Text = "Customer";
            customerToolStripMenuItem.Click += customerToolStripMenuItem_Click;
            // 
            // countryToolStripMenuItem
            // 
            countryToolStripMenuItem.Name = "countryToolStripMenuItem";
            countryToolStripMenuItem.Size = new Size(224, 26);
            countryToolStripMenuItem.Text = "Country";
            countryToolStripMenuItem.Click += countryToolStripMenuItem_Click;
            // 
            // cityToolStripMenuItem
            // 
            cityToolStripMenuItem.Name = "cityToolStripMenuItem";
            cityToolStripMenuItem.Size = new Size(224, 26);
            cityToolStripMenuItem.Text = "City";
            cityToolStripMenuItem.Click += cityToolStripMenuItem_Click;
            // 
            // categoryToolStripMenuItem
            // 
            categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
            categoryToolStripMenuItem.Size = new Size(224, 26);
            categoryToolStripMenuItem.Text = "Category";
            categoryToolStripMenuItem.Click += categoryToolStripMenuItem_Click;
            // 
            // promotionToolStripMenuItem
            // 
            promotionToolStripMenuItem.Name = "promotionToolStripMenuItem";
            promotionToolStripMenuItem.Size = new Size(224, 26);
            promotionToolStripMenuItem.Text = "Promotion";
            promotionToolStripMenuItem.Click += promotionToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { allCustomersToolStripMenuItem1, allEmailToolStripMenuItem, categoriesToolStripMenuItem, promocionalProductToolStripMenuItem, citysToolStripMenuItem1, countrysToolStripMenuItem1 });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(55, 24);
            viewToolStripMenuItem.Text = "View";
            // 
            // allCustomersToolStripMenuItem1
            // 
            allCustomersToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { fromCityToolStripMenuItem, fromCountryToolStripMenuItem });
            allCustomersToolStripMenuItem1.Name = "allCustomersToolStripMenuItem1";
            allCustomersToolStripMenuItem1.Size = new Size(232, 26);
            allCustomersToolStripMenuItem1.Text = "All customers";
            allCustomersToolStripMenuItem1.Click += allCustomersToolStripMenuItem1_Click;
            // 
            // fromCityToolStripMenuItem
            // 
            fromCityToolStripMenuItem.Name = "fromCityToolStripMenuItem";
            fromCityToolStripMenuItem.Size = new Size(181, 26);
            fromCityToolStripMenuItem.Text = "From City";
            fromCityToolStripMenuItem.Click += fromCityToolStripMenuItem_Click;
            // 
            // fromCountryToolStripMenuItem
            // 
            fromCountryToolStripMenuItem.Name = "fromCountryToolStripMenuItem";
            fromCountryToolStripMenuItem.Size = new Size(181, 26);
            fromCountryToolStripMenuItem.Text = "From Country";
            fromCountryToolStripMenuItem.Click += fromCountryToolStripMenuItem_Click;
            // 
            // allEmailToolStripMenuItem
            // 
            allEmailToolStripMenuItem.Name = "allEmailToolStripMenuItem";
            allEmailToolStripMenuItem.Size = new Size(232, 26);
            allEmailToolStripMenuItem.Text = "All email";
            allEmailToolStripMenuItem.Click += allEmailToolStripMenuItem_Click;
            // 
            // categoriesToolStripMenuItem
            // 
            categoriesToolStripMenuItem.Name = "categoriesToolStripMenuItem";
            categoriesToolStripMenuItem.Size = new Size(232, 26);
            categoriesToolStripMenuItem.Text = "Categories";
            categoriesToolStripMenuItem.Click += categoriesToolStripMenuItem_Click;
            // 
            // promocionalProductToolStripMenuItem
            // 
            promocionalProductToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { forCountyToolStripMenuItem });
            promocionalProductToolStripMenuItem.Name = "promocionalProductToolStripMenuItem";
            promocionalProductToolStripMenuItem.Size = new Size(232, 26);
            promocionalProductToolStripMenuItem.Text = "Promocional product";
            promocionalProductToolStripMenuItem.Click += promocionalProductToolStripMenuItem_Click;
            // 
            // forCountyToolStripMenuItem
            // 
            forCountyToolStripMenuItem.Name = "forCountyToolStripMenuItem";
            forCountyToolStripMenuItem.Size = new Size(161, 26);
            forCountyToolStripMenuItem.Text = "For county";
            forCountyToolStripMenuItem.Click += forCountyToolStripMenuItem_Click;
            // 
            // citysToolStripMenuItem1
            // 
            citysToolStripMenuItem1.Name = "citysToolStripMenuItem1";
            citysToolStripMenuItem1.Size = new Size(232, 26);
            citysToolStripMenuItem1.Text = "Citys";
            citysToolStripMenuItem1.Click += citysToolStripMenuItem1_Click;
            // 
            // countrysToolStripMenuItem1
            // 
            countrysToolStripMenuItem1.Name = "countrysToolStripMenuItem1";
            countrysToolStripMenuItem1.Size = new Size(232, 26);
            countrysToolStripMenuItem1.Text = "Countrys";
            countrysToolStripMenuItem1.Click += countrysToolStripMenuItem1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem allCustomersToolStripMenuItem1;
        private ToolStripMenuItem allEmailToolStripMenuItem;
        private ToolStripMenuItem categoriesToolStripMenuItem;
        private ToolStripMenuItem promocionalProductToolStripMenuItem;
        private ToolStripMenuItem citysToolStripMenuItem1;
        private ToolStripMenuItem countrysToolStripMenuItem1;
        private ToolStripMenuItem fromCityToolStripMenuItem;
        private ToolStripMenuItem fromCountryToolStripMenuItem;
        private ToolStripMenuItem forCountyToolStripMenuItem;
        private ToolStripMenuItem customerToolStripMenuItem;
        private ToolStripMenuItem countryToolStripMenuItem;
        private ToolStripMenuItem cityToolStripMenuItem;
        private ToolStripMenuItem categoryToolStripMenuItem;
        private ToolStripMenuItem promotionToolStripMenuItem;
    }
}
