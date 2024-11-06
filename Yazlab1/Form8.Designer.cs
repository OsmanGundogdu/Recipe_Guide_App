namespace Yazlab1
{
    partial class TarifDetay
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tarifAdiLabel = new System.Windows.Forms.Label();
            this.kategoriLabel = new System.Windows.Forms.Label();
            this.hazirlamaSuresiLabel = new System.Windows.Forms.Label();
            this.malzemelerPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.geriButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.maliyetLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(27, 175);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(297, 181);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tarifAdiLabel
            // 
            this.tarifAdiLabel.AutoSize = true;
            this.tarifAdiLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tarifAdiLabel.Location = new System.Drawing.Point(35, 112);
            this.tarifAdiLabel.Name = "tarifAdiLabel";
            this.tarifAdiLabel.Size = new System.Drawing.Size(57, 25);
            this.tarifAdiLabel.TabIndex = 1;
            this.tarifAdiLabel.Text = "vgfu";
            // 
            // kategoriLabel
            // 
            this.kategoriLabel.AutoSize = true;
            this.kategoriLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kategoriLabel.Location = new System.Drawing.Point(237, 386);
            this.kategoriLabel.Name = "kategoriLabel";
            this.kategoriLabel.Size = new System.Drawing.Size(50, 16);
            this.kategoriLabel.TabIndex = 2;
            this.kategoriLabel.Text = "label1";
            // 
            // hazirlamaSuresiLabel
            // 
            this.hazirlamaSuresiLabel.AutoSize = true;
            this.hazirlamaSuresiLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.hazirlamaSuresiLabel.Location = new System.Drawing.Point(237, 427);
            this.hazirlamaSuresiLabel.Name = "hazirlamaSuresiLabel";
            this.hazirlamaSuresiLabel.Size = new System.Drawing.Size(50, 16);
            this.hazirlamaSuresiLabel.TabIndex = 3;
            this.hazirlamaSuresiLabel.Text = "label2";
            // 
            // malzemelerPanel
            // 
            this.malzemelerPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.malzemelerPanel.Location = new System.Drawing.Point(386, 386);
            this.malzemelerPanel.Name = "malzemelerPanel";
            this.malzemelerPanel.Size = new System.Drawing.Size(554, 244);
            this.malzemelerPanel.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.maliyetLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.geriButton);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.kategoriLabel);
            this.panel1.Controls.Add(this.malzemelerPanel);
            this.panel1.Controls.Add(this.tarifAdiLabel);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.hazirlamaSuresiLabel);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(950, 633);
            this.panel1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(37, 427);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 16);
            this.label2.TabIndex = 29;
            this.label2.Text = "Hazırlama Süresi: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(37, 386);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Kategori: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // geriButton
            // 
            this.geriButton.BackColor = System.Drawing.SystemColors.Info;
            this.geriButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.geriButton.Location = new System.Drawing.Point(3, 3);
            this.geriButton.Name = "geriButton";
            this.geriButton.Size = new System.Drawing.Size(91, 31);
            this.geriButton.TabIndex = 27;
            this.geriButton.Text = "GERİ";
            this.geriButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.geriButton.UseVisualStyleBackColor = false;
            this.geriButton.Click += new System.EventHandler(this.geriButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richTextBox1.Location = new System.Drawing.Point(386, 112);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(554, 241);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(37, 462);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "Maliyet:";
            // 
            // maliyetLabel
            // 
            this.maliyetLabel.AutoSize = true;
            this.maliyetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.maliyetLabel.Location = new System.Drawing.Point(240, 462);
            this.maliyetLabel.Name = "maliyetLabel";
            this.maliyetLabel.Size = new System.Drawing.Size(50, 16);
            this.maliyetLabel.TabIndex = 31;
            this.maliyetLabel.Text = "label4";
            // 
            // TarifDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(965, 658);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TarifDetay";
            this.Text = "Form8";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label tarifAdiLabel;
        private System.Windows.Forms.Label kategoriLabel;
        private System.Windows.Forms.Label hazirlamaSuresiLabel;
        private System.Windows.Forms.Panel malzemelerPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button geriButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label maliyetLabel;
        private System.Windows.Forms.Label label3;
    }
}