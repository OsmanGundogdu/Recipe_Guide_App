namespace Yazlab1
{
    partial class Tarifler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.araTextBox = new System.Windows.Forms.TextBox();
            this.araButonu = new System.Windows.Forms.Button();
            this.anaMenuButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scrollablePanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.malzemeCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.sortingComboBox = new System.Windows.Forms.ComboBox();
            this.tarifOnerileriPanel = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.scrollablePanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // araTextBox
            // 
            this.araTextBox.BackColor = System.Drawing.Color.Silver;
            this.araTextBox.Location = new System.Drawing.Point(363, 52);
            this.araTextBox.Name = "araTextBox";
            this.araTextBox.Size = new System.Drawing.Size(219, 20);
            this.araTextBox.TabIndex = 0;
            // 
            // araButonu
            // 
            this.araButonu.BackColor = System.Drawing.Color.Silver;
            this.araButonu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.araButonu.Location = new System.Drawing.Point(419, 102);
            this.araButonu.Name = "araButonu";
            this.araButonu.Size = new System.Drawing.Size(103, 44);
            this.araButonu.TabIndex = 1;
            this.araButonu.Text = "ARA";
            this.araButonu.UseVisualStyleBackColor = false;
            this.araButonu.Click += new System.EventHandler(this.araButonu_Click);
            // 
            // anaMenuButton
            // 
            this.anaMenuButton.BackColor = System.Drawing.SystemColors.Info;
            this.anaMenuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.anaMenuButton.Location = new System.Drawing.Point(0, 0);
            this.anaMenuButton.Name = "anaMenuButton";
            this.anaMenuButton.Size = new System.Drawing.Size(63, 40);
            this.anaMenuButton.TabIndex = 6;
            this.anaMenuButton.Text = "ANA MENÜ";
            this.anaMenuButton.UseMnemonic = false;
            this.anaMenuButton.UseVisualStyleBackColor = false;
            this.anaMenuButton.Click += new System.EventHandler(this.anaMenuButton_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(27, 199);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 160);
            this.panel1.TabIndex = 3;
            // 
            // scrollablePanel
            // 
            this.scrollablePanel.Controls.Add(this.label3);
            this.scrollablePanel.Controls.Add(this.label1);
            this.scrollablePanel.Controls.Add(this.malzemeCheckedListBox);
            this.scrollablePanel.Controls.Add(this.sortingComboBox);
            this.scrollablePanel.Controls.Add(this.panel1);
            this.scrollablePanel.Controls.Add(this.anaMenuButton);
            this.scrollablePanel.Controls.Add(this.araTextBox);
            this.scrollablePanel.Controls.Add(this.araButonu);
            this.scrollablePanel.Location = new System.Drawing.Point(12, 12);
            this.scrollablePanel.Name = "scrollablePanel";
            this.scrollablePanel.Size = new System.Drawing.Size(879, 532);
            this.scrollablePanel.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(362, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "TARİF ADINA GÖRE ARA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(101, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "MALZEMEYE GÖRE ARA";
            // 
            // malzemeCheckedListBox
            // 
            this.malzemeCheckedListBox.FormattingEnabled = true;
            this.malzemeCheckedListBox.Location = new System.Drawing.Point(80, 52);
            this.malzemeCheckedListBox.Name = "malzemeCheckedListBox";
            this.malzemeCheckedListBox.Size = new System.Drawing.Size(257, 94);
            this.malzemeCheckedListBox.TabIndex = 8;
            // 
            // sortingComboBox
            // 
            this.sortingComboBox.BackColor = System.Drawing.Color.LightGray;
            this.sortingComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sortingComboBox.FormattingEnabled = true;
            this.sortingComboBox.Location = new System.Drawing.Point(611, 50);
            this.sortingComboBox.Name = "sortingComboBox";
            this.sortingComboBox.Size = new System.Drawing.Size(242, 24);
            this.sortingComboBox.TabIndex = 7;
            this.sortingComboBox.Text = "                    FİLTRELE";
            // 
            // tarifOnerileriPanel
            // 
            this.tarifOnerileriPanel.Location = new System.Drawing.Point(27, 17);
            this.tarifOnerileriPanel.Name = "tarifOnerileriPanel";
            this.tarifOnerileriPanel.Size = new System.Drawing.Size(257, 128);
            this.tarifOnerileriPanel.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(12, 550);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(879, 20);
            this.textBox1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(35, 575);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "TARİF ÖNERİLERİ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tarifOnerileriPanel);
            this.panel2.Location = new System.Drawing.Point(12, 594);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(879, 286);
            this.panel2.TabIndex = 10;
            // 
            // Tarifler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(896, 882);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.scrollablePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Tarifler";
            this.Text = "Form2";
            this.scrollablePanel.ResumeLayout(false);
            this.scrollablePanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox araTextBox;
        private System.Windows.Forms.Button araButonu;
        private System.Windows.Forms.Button anaMenuButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel scrollablePanel;
        private System.Windows.Forms.ComboBox sortingComboBox;
        private System.Windows.Forms.Panel tarifOnerileriPanel;
        private System.Windows.Forms.CheckedListBox malzemeCheckedListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
    }
}