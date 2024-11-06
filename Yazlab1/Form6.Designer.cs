namespace Yazlab1
{
    partial class TarifSil
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
            this.scrollablePanel = new System.Windows.Forms.Panel();
            this.sortingComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.anaMenuButton = new System.Windows.Forms.Button();
            this.araTextBox = new System.Windows.Forms.TextBox();
            this.araButonu = new System.Windows.Forms.Button();
            this.scrollablePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollablePanel
            // 
            this.scrollablePanel.Controls.Add(this.sortingComboBox);
            this.scrollablePanel.Controls.Add(this.panel1);
            this.scrollablePanel.Controls.Add(this.anaMenuButton);
            this.scrollablePanel.Controls.Add(this.araTextBox);
            this.scrollablePanel.Controls.Add(this.araButonu);
            this.scrollablePanel.Location = new System.Drawing.Point(12, 12);
            this.scrollablePanel.Name = "scrollablePanel";
            this.scrollablePanel.Size = new System.Drawing.Size(879, 652);
            this.scrollablePanel.TabIndex = 8;
            // 
            // sortingComboBox
            // 
            this.sortingComboBox.BackColor = System.Drawing.Color.LightGray;
            this.sortingComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sortingComboBox.FormattingEnabled = true;
            this.sortingComboBox.Location = new System.Drawing.Point(574, 19);
            this.sortingComboBox.Name = "sortingComboBox";
            this.sortingComboBox.Size = new System.Drawing.Size(264, 24);
            this.sortingComboBox.TabIndex = 8;
            this.sortingComboBox.Text = "                        FİLTRELE";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(26, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 160);
            this.panel1.TabIndex = 3;
            // 
            // anaMenuButton
            // 
            this.anaMenuButton.BackColor = System.Drawing.SystemColors.Info;
            this.anaMenuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.anaMenuButton.Location = new System.Drawing.Point(26, 5);
            this.anaMenuButton.Name = "anaMenuButton";
            this.anaMenuButton.Size = new System.Drawing.Size(72, 49);
            this.anaMenuButton.TabIndex = 6;
            this.anaMenuButton.Text = "ANA MENÜ";
            this.anaMenuButton.UseMnemonic = false;
            this.anaMenuButton.UseVisualStyleBackColor = false;
            this.anaMenuButton.Click += new System.EventHandler(this.anaMenuButton_Click_1);
            // 
            // araTextBox
            // 
            this.araTextBox.BackColor = System.Drawing.Color.Silver;
            this.araTextBox.Location = new System.Drawing.Point(149, 23);
            this.araTextBox.Name = "araTextBox";
            this.araTextBox.Size = new System.Drawing.Size(271, 20);
            this.araTextBox.TabIndex = 0;
            // 
            // araButonu
            // 
            this.araButonu.BackColor = System.Drawing.Color.Silver;
            this.araButonu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.araButonu.Location = new System.Drawing.Point(439, 18);
            this.araButonu.Name = "araButonu";
            this.araButonu.Size = new System.Drawing.Size(74, 24);
            this.araButonu.TabIndex = 1;
            this.araButonu.Text = "ARA";
            this.araButonu.UseVisualStyleBackColor = false;
            // 
            // TarifSil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(897, 676);
            this.Controls.Add(this.scrollablePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TarifSil";
            this.Text = "Form6";
            this.scrollablePanel.ResumeLayout(false);
            this.scrollablePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel scrollablePanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button anaMenuButton;
        private System.Windows.Forms.TextBox araTextBox;
        private System.Windows.Forms.Button araButonu;
        private System.Windows.Forms.ComboBox sortingComboBox;
    }
}