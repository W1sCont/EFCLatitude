namespace Warehouse
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
            menuStrip1 = new MenuStrip();
            менюToolStripMenuItem = new ToolStripMenuItem();
            весьТоварToolStripMenuItem = new ToolStripMenuItem();
            зМаксимальноюКсюToolStripMenuItem = new ToolStripMenuItem();
            всіТовариToolStripMenuItem = new ToolStripMenuItem();
            зМаксКсюToolStripMenuItem = new ToolStripMenuItem();
            зМінКтюToolStripMenuItem = new ToolStripMenuItem();
            зМінСобівартістюToolStripMenuItem1 = new ToolStripMenuItem();
            зМаксСобівартістюToolStripMenuItem1 = new ToolStripMenuItem();
            найстарішийТоварToolStripMenuItem = new ToolStripMenuItem();
            додатиToolStripMenuItem = new ToolStripMenuItem();
            редагуватиToolStripMenuItem = new ToolStripMenuItem();
            видалитиToolStripMenuItem = new ToolStripMenuItem();
            типиToolStripMenuItem = new ToolStripMenuItem();
            заКатегоріямиToolStripMenuItem = new ToolStripMenuItem();
            середняКстьТоваруToolStripMenuItem1 = new ToolStripMenuItem();
            середняКстьТоваруToolStripMenuItem = new ToolStripMenuItem();
            додатиToolStripMenuItem1 = new ToolStripMenuItem();
            редагуватиToolStripMenuItem1 = new ToolStripMenuItem();
            видалитиToolStripMenuItem1 = new ToolStripMenuItem();
            постачальникиToolStripMenuItem = new ToolStripMenuItem();
            відобразитиToolStripMenuItem = new ToolStripMenuItem();
            всіПостачальникиToolStripMenuItem = new ToolStripMenuItem();
            товариВідПостачальникToolStripMenuItem = new ToolStripMenuItem();
            toolStripComboBox1 = new ToolStripComboBox();
            додатиToolStripMenuItem2 = new ToolStripMenuItem();
            редагуватиToolStripMenuItem2 = new ToolStripMenuItem();
            видалитиToolStripMenuItem2 = new ToolStripMenuItem();
            вихідToolStripMenuItem = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { менюToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(870, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            менюToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { весьТоварToolStripMenuItem, типиToolStripMenuItem, постачальникиToolStripMenuItem, вихідToolStripMenuItem });
            менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            менюToolStripMenuItem.Size = new Size(65, 24);
            менюToolStripMenuItem.Text = "Меню";
            менюToolStripMenuItem.Click += менюToolStripMenuItem_Click;
            // 
            // весьТоварToolStripMenuItem
            // 
            весьТоварToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { зМаксимальноюКсюToolStripMenuItem, додатиToolStripMenuItem, редагуватиToolStripMenuItem, видалитиToolStripMenuItem });
            весьТоварToolStripMenuItem.Name = "весьТоварToolStripMenuItem";
            весьТоварToolStripMenuItem.Size = new Size(224, 26);
            весьТоварToolStripMenuItem.Text = "Товари";
            // 
            // зМаксимальноюКсюToolStripMenuItem
            // 
            зМаксимальноюКсюToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { всіТовариToolStripMenuItem, зМаксКсюToolStripMenuItem, зМінКтюToolStripMenuItem, зМінСобівартістюToolStripMenuItem1, зМаксСобівартістюToolStripMenuItem1, найстарішийТоварToolStripMenuItem });
            зМаксимальноюКсюToolStripMenuItem.Name = "зМаксимальноюКсюToolStripMenuItem";
            зМаксимальноюКсюToolStripMenuItem.Size = new Size(179, 26);
            зМаксимальноюКсюToolStripMenuItem.Text = "Відобразити";
            // 
            // всіТовариToolStripMenuItem
            // 
            всіТовариToolStripMenuItem.Name = "всіТовариToolStripMenuItem";
            всіТовариToolStripMenuItem.Size = new Size(230, 26);
            всіТовариToolStripMenuItem.Text = "Всі товари";
            всіТовариToolStripMenuItem.Click += всіТовариToolStripMenuItem_Click;
            // 
            // зМаксКсюToolStripMenuItem
            // 
            зМаксКсюToolStripMenuItem.Name = "зМаксКсюToolStripMenuItem";
            зМаксКсюToolStripMenuItem.Size = new Size(230, 26);
            зМаксКсюToolStripMenuItem.Text = "З макс к-сю";
            зМаксКсюToolStripMenuItem.Click += зМаксКсюToolStripMenuItem_Click;
            // 
            // зМінКтюToolStripMenuItem
            // 
            зМінКтюToolStripMenuItem.Name = "зМінКтюToolStripMenuItem";
            зМінКтюToolStripMenuItem.Size = new Size(230, 26);
            зМінКтюToolStripMenuItem.Text = "З мін к-тю";
            зМінКтюToolStripMenuItem.Click += зМінКтюToolStripMenuItem_Click;
            // 
            // зМінСобівартістюToolStripMenuItem1
            // 
            зМінСобівартістюToolStripMenuItem1.Name = "зМінСобівартістюToolStripMenuItem1";
            зМінСобівартістюToolStripMenuItem1.Size = new Size(230, 26);
            зМінСобівартістюToolStripMenuItem1.Text = "З мін собівартістю";
            зМінСобівартістюToolStripMenuItem1.Click += зМінСобівартістюToolStripMenuItem1_Click;
            // 
            // зМаксСобівартістюToolStripMenuItem1
            // 
            зМаксСобівартістюToolStripMenuItem1.Name = "зМаксСобівартістюToolStripMenuItem1";
            зМаксСобівартістюToolStripMenuItem1.Size = new Size(230, 26);
            зМаксСобівартістюToolStripMenuItem1.Text = "З макс собівартістю";
            зМаксСобівартістюToolStripMenuItem1.Click += зМаксСобівартістюToolStripMenuItem1_Click;
            // 
            // найстарішийТоварToolStripMenuItem
            // 
            найстарішийТоварToolStripMenuItem.Name = "найстарішийТоварToolStripMenuItem";
            найстарішийТоварToolStripMenuItem.Size = new Size(230, 26);
            найстарішийТоварToolStripMenuItem.Text = "Найстаріший товар";
            найстарішийТоварToolStripMenuItem.Click += найстарішийТоварToolStripMenuItem_Click;
            // 
            // додатиToolStripMenuItem
            // 
            додатиToolStripMenuItem.Name = "додатиToolStripMenuItem";
            додатиToolStripMenuItem.Size = new Size(179, 26);
            додатиToolStripMenuItem.Text = "Додати";
            додатиToolStripMenuItem.Click += додатиToolStripMenuItem_Click;
            // 
            // редагуватиToolStripMenuItem
            // 
            редагуватиToolStripMenuItem.Name = "редагуватиToolStripMenuItem";
            редагуватиToolStripMenuItem.Size = new Size(179, 26);
            редагуватиToolStripMenuItem.Text = "Редагувати";
            редагуватиToolStripMenuItem.Click += редагуватиToolStripMenuItem_Click;
            // 
            // видалитиToolStripMenuItem
            // 
            видалитиToolStripMenuItem.Name = "видалитиToolStripMenuItem";
            видалитиToolStripMenuItem.Size = new Size(179, 26);
            видалитиToolStripMenuItem.Text = "Видалити";
            видалитиToolStripMenuItem.Click += видалитиToolStripMenuItem_Click;
            // 
            // типиToolStripMenuItem
            // 
            типиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { заКатегоріямиToolStripMenuItem, додатиToolStripMenuItem1, редагуватиToolStripMenuItem1, видалитиToolStripMenuItem1 });
            типиToolStripMenuItem.Name = "типиToolStripMenuItem";
            типиToolStripMenuItem.Size = new Size(224, 26);
            типиToolStripMenuItem.Text = "Категорії";
            // 
            // заКатегоріямиToolStripMenuItem
            // 
            заКатегоріямиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { середняКстьТоваруToolStripMenuItem1, середняКстьТоваруToolStripMenuItem });
            заКатегоріямиToolStripMenuItem.Name = "заКатегоріямиToolStripMenuItem";
            заКатегоріямиToolStripMenuItem.Size = new Size(179, 26);
            заКатегоріямиToolStripMenuItem.Text = "Відобразити";
            // 
            // середняКстьТоваруToolStripMenuItem1
            // 
            середняКстьТоваруToolStripMenuItem1.Name = "середняКстьТоваруToolStripMenuItem1";
            середняКстьТоваруToolStripMenuItem1.Size = new Size(240, 26);
            середняКстьТоваруToolStripMenuItem1.Text = "Всі категорії";
            середняКстьТоваруToolStripMenuItem1.Click += середняКстьТоваруToolStripMenuItem1_Click;
            // 
            // середняКстьТоваруToolStripMenuItem
            // 
            середняКстьТоваруToolStripMenuItem.Name = "середняКстьТоваруToolStripMenuItem";
            середняКстьТоваруToolStripMenuItem.Size = new Size(240, 26);
            середняКстьТоваруToolStripMenuItem.Text = "Середня к-сть товару";
            середняКстьТоваруToolStripMenuItem.Click += середняКстьТоваруToolStripMenuItem_Click;
            // 
            // додатиToolStripMenuItem1
            // 
            додатиToolStripMenuItem1.Name = "додатиToolStripMenuItem1";
            додатиToolStripMenuItem1.Size = new Size(179, 26);
            додатиToolStripMenuItem1.Text = "Додати";
            додатиToolStripMenuItem1.Click += додатиToolStripMenuItem1_Click;
            // 
            // редагуватиToolStripMenuItem1
            // 
            редагуватиToolStripMenuItem1.Name = "редагуватиToolStripMenuItem1";
            редагуватиToolStripMenuItem1.Size = new Size(179, 26);
            редагуватиToolStripMenuItem1.Text = "Редагувати";
            редагуватиToolStripMenuItem1.Click += редагуватиToolStripMenuItem1_Click;
            // 
            // видалитиToolStripMenuItem1
            // 
            видалитиToolStripMenuItem1.Name = "видалитиToolStripMenuItem1";
            видалитиToolStripMenuItem1.Size = new Size(179, 26);
            видалитиToolStripMenuItem1.Text = "Видалити";
            видалитиToolStripMenuItem1.Click += видалитиToolStripMenuItem1_Click;
            // 
            // постачальникиToolStripMenuItem
            // 
            постачальникиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { відобразитиToolStripMenuItem, додатиToolStripMenuItem2, редагуватиToolStripMenuItem2, видалитиToolStripMenuItem2 });
            постачальникиToolStripMenuItem.Name = "постачальникиToolStripMenuItem";
            постачальникиToolStripMenuItem.Size = new Size(224, 26);
            постачальникиToolStripMenuItem.Text = "Постачальники";
            // 
            // відобразитиToolStripMenuItem
            // 
            відобразитиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { всіПостачальникиToolStripMenuItem, товариВідПостачальникToolStripMenuItem });
            відобразитиToolStripMenuItem.Name = "відобразитиToolStripMenuItem";
            відобразитиToolStripMenuItem.Size = new Size(224, 26);
            відобразитиToolStripMenuItem.Text = "Відобразити";
            // 
            // всіПостачальникиToolStripMenuItem
            // 
            всіПостачальникиToolStripMenuItem.Name = "всіПостачальникиToolStripMenuItem";
            всіПостачальникиToolStripMenuItem.Size = new Size(267, 26);
            всіПостачальникиToolStripMenuItem.Text = "Всі постачальники";
            всіПостачальникиToolStripMenuItem.Click += всіПостачальникиToolStripMenuItem_Click;
            // 
            // товариВідПостачальникToolStripMenuItem
            // 
            товариВідПостачальникToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripComboBox1 });
            товариВідПостачальникToolStripMenuItem.Name = "товариВідПостачальникToolStripMenuItem";
            товариВідПостачальникToolStripMenuItem.Size = new Size(267, 26);
            товариВідПостачальникToolStripMenuItem.Text = "Товари від постачальник";
            товариВідПостачальникToolStripMenuItem.Click += товариВідПостачальникToolStripMenuItem_Click;
            // 
            // toolStripComboBox1
            // 
            toolStripComboBox1.Name = "toolStripComboBox1";
            toolStripComboBox1.Size = new Size(121, 28);
            toolStripComboBox1.SelectedIndexChanged += toolStripComboBox1_SelectedIndexChanged;
            toolStripComboBox1.Click += toolStripComboBox1_Click;
            toolStripComboBox1.SelectedChanged += toolStripComboBox1_SelectedChanged;
            // 
            // додатиToolStripMenuItem2
            // 
            додатиToolStripMenuItem2.Name = "додатиToolStripMenuItem2";
            додатиToolStripMenuItem2.Size = new Size(224, 26);
            додатиToolStripMenuItem2.Text = "Додати";
            додатиToolStripMenuItem2.Click += додатиToolStripMenuItem2_Click;
            // 
            // редагуватиToolStripMenuItem2
            // 
            редагуватиToolStripMenuItem2.Name = "редагуватиToolStripMenuItem2";
            редагуватиToolStripMenuItem2.Size = new Size(224, 26);
            редагуватиToolStripMenuItem2.Text = "Редагувати";
            редагуватиToolStripMenuItem2.Click += редагуватиToolStripMenuItem2_Click;
            // 
            // видалитиToolStripMenuItem2
            // 
            видалитиToolStripMenuItem2.Name = "видалитиToolStripMenuItem2";
            видалитиToolStripMenuItem2.Size = new Size(224, 26);
            видалитиToolStripMenuItem2.Text = "Видалити";
            видалитиToolStripMenuItem2.Click += видалитиToolStripMenuItem2_Click;
            // 
            // вихідToolStripMenuItem
            // 
            вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            вихідToolStripMenuItem.Size = new Size(224, 26);
            вихідToolStripMenuItem.Text = "Вихід";
            вихідToolStripMenuItem.Click += вихідToolStripMenuItem_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 28);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(870, 480);
            dataGridView1.TabIndex = 1;
            dataGridView1.Visible = false;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
            statusStrip1.Location = new Point(0, 424);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 26);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            statusStrip1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(151, 20);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(151, 20);
            toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new Size(151, 20);
            toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 508);
            Controls.Add(statusStrip1);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem менюToolStripMenuItem;
        private ToolStripMenuItem весьТоварToolStripMenuItem;
        private ToolStripMenuItem типиToolStripMenuItem;
        private ToolStripMenuItem постачальникиToolStripMenuItem;
        private ToolStripMenuItem зМаксимальноюКсюToolStripMenuItem;
        private ToolStripMenuItem заКатегоріямиToolStripMenuItem;
        private ToolStripMenuItem зМаксКсюToolStripMenuItem;
        private ToolStripMenuItem зМінКтюToolStripMenuItem;
        private ToolStripMenuItem зМінСобівартістюToolStripMenuItem1;
        private ToolStripMenuItem зМаксСобівартістюToolStripMenuItem1;
        private ToolStripMenuItem найстарішийТоварToolStripMenuItem;
        private ToolStripMenuItem додатиToolStripMenuItem;
        private ToolStripMenuItem редагуватиToolStripMenuItem;
        private ToolStripMenuItem видалитиToolStripMenuItem;
        private ToolStripMenuItem середняКстьТоваруToolStripMenuItem1;
        private ToolStripMenuItem додатиToolStripMenuItem1;
        private ToolStripMenuItem редагуватиToolStripMenuItem1;
        private ToolStripMenuItem видалитиToolStripMenuItem1;
        private ToolStripMenuItem відобразитиToolStripMenuItem;
        private ToolStripMenuItem додатиToolStripMenuItem2;
        private ToolStripMenuItem редагуватиToolStripMenuItem2;
        private ToolStripMenuItem видалитиToolStripMenuItem2;
        private ToolStripMenuItem всіПостачальникиToolStripMenuItem;
        private ToolStripMenuItem товариВідПостачальникToolStripMenuItem;
        private ToolStripMenuItem середняКстьТоваруToolStripMenuItem;
        private DataGridView dataGridView1;
        private ToolStripMenuItem всіТовариToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripMenuItem вихідToolStripMenuItem;
        private ToolStripComboBox toolStripComboBox1;
    }
}
