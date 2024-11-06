using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace Yazlab1
{
    public partial class TarifEklemeFormu : Form
    {
        private SQLiteConnection sqliteConnection;
        private string databasePath = @"C:\Users\osman1\Desktop\Programlama\Yazlab\1\Yazlab\Yazlab1\TarifRehberiUygulamasi.db";
        private List<List<string>> malzemeListesi;

        public TarifEklemeFormu()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeDatabase();
            this.Load += TarifEkleGuncelle_Load;
            malzemeListesi = new List<List<string>>();
        }

        private void InitializeDatabase()
        {
            string connectionString = $"Data Source={databasePath};Version=3;";
            sqliteConnection = new SQLiteConnection(connectionString);

            try
            {
                sqliteConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanına bağlanılamadı: {ex.Message}");
            }
        }

        private void TarifEkleGuncelle_Load(object sender, EventArgs e)
        {
            LoadMalzemeler();
        }

        private void LoadMalzemeler(string searchQuery = "")
        {
            string query = "SELECT MalzemeID, MalzemeAdi, MalzemeBirim FROM Malzemeler";

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += " WHERE MalzemeAdi LIKE @SearchQuery";
            }

            using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
            {
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    command.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                }

                try
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        int cardCount = 0;
                        int cardWidth = 150;
                        int cardHeight = 130;
                        int padding = 10;
                        int columns = 5;

                        panel1.Controls.Clear();
                        panel2.AutoScroll = true;

                        while (reader.Read())
                        {
                            string malzemeId = reader["MalzemeID"].ToString();
                            string malzemeAdi = reader["MalzemeAdi"].ToString();
                            string malzemeBirim = reader["MalzemeBirim"].ToString();

                            Panel cardPanel = new Panel();
                            cardPanel.Size = new Size(cardWidth, cardHeight);
                            cardPanel.BorderStyle = BorderStyle.FixedSingle;

                            Label nameLabel = new Label();
                            nameLabel.Text = $"M.adı: {malzemeAdi}";
                            nameLabel.Location = new Point(5, 5);
                            nameLabel.AutoSize = true;

                            Label birimLabel = new Label();
                            birimLabel.Text = $"Birim: {malzemeBirim}";
                            birimLabel.Location = new Point(5, 25);
                            birimLabel.AutoSize = true;

                            TextBox kullanilacakMiktarTextBox = new TextBox();
                            kullanilacakMiktarTextBox.Location = new Point(5, 65);
                            kullanilacakMiktarTextBox.Width = 60;

                            Button ekleButton = new Button();
                            ekleButton.Text = "Ekle";
                            ekleButton.BackColor = Color.White;
                            ekleButton.ForeColor = Color.Black;
                            ekleButton.Location = new Point(70, 65);
                            ekleButton.Click += (s, e) =>
                            {
                                List<string> malzeme = new List<string>();
                                if (int.TryParse(kullanilacakMiktarTextBox.Text, out int kullanilacakMiktar))
                                {
                                    malzeme.Add(malzemeId);
                                    malzeme.Add(kullanilacakMiktar.ToString());
                                    malzemeListesi.Add(malzeme);
                                    MessageBox.Show($"{malzemeAdi} için {kullanilacakMiktar} {malzemeBirim} kullanıldı.");
                                }
                                else
                                {
                                    MessageBox.Show("Geçerli bir miktar giriniz.");
                                }
                            };

                            cardPanel.Controls.Add(nameLabel);
                            cardPanel.Controls.Add(birimLabel);
                            cardPanel.Controls.Add(kullanilacakMiktarTextBox);
                            cardPanel.Controls.Add(ekleButton);

                            int row = cardCount / columns;
                            int col = cardCount % columns;

                            cardPanel.Location = new Point((cardWidth + padding) * col, (cardHeight + padding) * row);
                            panel1.Controls.Add(cardPanel);

                            cardCount++;
                        }

                        int totalRows = (int)Math.Ceiling((double)cardCount / columns);
                        panel1.Size = new Size((cardWidth + padding) * columns, (cardHeight + padding) * totalRows);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Malzemeler yüklenirken hata oluştu: {ex.Message}");
                }
            }
        }

        private bool TarifVarMi(string tarifAdi)
        {
            string query = "SELECT COUNT(*) FROM Tarifler WHERE TarifAdi = @TarifAdi";
            using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
            {
                command.Parameters.AddWithValue("@TarifAdi", tarifAdi);

                try
                {
                    long count = (long)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Tarif kontrolü sırasında bir hata oluştu: {ex.Message}");
                    return true;
                }
            }
        }

        private void SaveIngredients(long tarifId)
        {
            foreach (var malzeme in malzemeListesi)
            {
                string malzemeId = malzeme[0];
                string kullanilacakMiktar = malzeme[1];

                string insertMalzemeQuery = "INSERT INTO TarifMalzeme (TarifID, MalzemeID, MalzemeMiktar) VALUES (@TarifID, @MalzemeID, @MalzemeMiktar)";
                using (SQLiteCommand command = new SQLiteCommand(insertMalzemeQuery, sqliteConnection))
                {
                    command.Parameters.AddWithValue("@TarifID", tarifId);
                    command.Parameters.AddWithValue("@MalzemeID", malzemeId);
                    command.Parameters.AddWithValue("@MalzemeMiktar", kullanilacakMiktar);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Malzeme kaydetme sırasında bir hata oluştu: {ex.Message}");
                    }
                }
            }

            foreach (Control control in panel1.Controls)
            {
                if (control is Panel cardPanel)
                {
                    foreach (Control innerControl in cardPanel.Controls)
                    {
                        if (innerControl is TextBox textBox)
                        {
                            textBox.Clear();
                        }
                    }
                }
            }
        }

        private void ClearForm()
        {
            tarifAdiTextBox.Clear();
            anaYemekCheckBox.Checked = false;
            tatliCheckBox.Checked = false;
            corbaCheckBox.Checked = false;
            salataCheckBox.Checked = false;
            atistirmalikCheckBox.Checked = false;
            sureNumericUpDown.Value = sureNumericUpDown.Minimum;
            richTextBox1.Clear();
            malzemeListesi.Clear();
            urlTextBox.Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        private void corbaCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void anaMenuButton_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void malzemeOlusturButonu_Click_1(object sender, EventArgs e)
        {
            malzemeEkle malzemeEkleGuncelle = new malzemeEkle();
            malzemeEkleGuncelle.Show();
            LoadMalzemeler();
            this.Close();
        }

        private void kaydetButonu_Click_1(object sender, EventArgs e)
        {
            string kategori = null;

            if (anaYemekCheckBox.Checked)
                kategori = "Ana Yemek";
            else if (tatliCheckBox.Checked)
                kategori = "Tatlı";
            else if (corbaCheckBox.Checked)
                kategori = "Çorba";
            else if (salataCheckBox.Checked)
                kategori = "Salata";
            else if (atistirmalikCheckBox.Checked)
                kategori = "Atıştırmalık";

            string tarifAdi = tarifAdiTextBox.Text;
            string hazirlamaSuresi = sureNumericUpDown.Value.ToString();
            string talimatlar = richTextBox1.Text;
            string resimYolu = urlTextBox.Text;

            if (string.IsNullOrEmpty(tarifAdi) || string.IsNullOrEmpty(kategori) || string.IsNullOrEmpty(hazirlamaSuresi) || string.IsNullOrEmpty(talimatlar) || string.IsNullOrEmpty(resimYolu))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            if (malzemeListesi.Count == 0)
            {
                MessageBox.Show("Lütfen en az bir malzeme ekleyiniz.");
                return;
            }

            if (TarifVarMi(tarifAdi))
            {
                MessageBox.Show("Bu tarif zaten mevcut.");
                return;
            }

            long tarifId = 0;

            string insertTarifQuery = "INSERT INTO Tarifler (TarifAdi, Kategori, HazirlamaSuresi, Talimatlar, ResimYolu) VALUES (@TarifAdi, @Kategori, @HazirlamaSuresi, @Talimatlar, @ResimYolu)";
            using (SQLiteCommand command = new SQLiteCommand(insertTarifQuery, sqliteConnection))
            {
                command.Parameters.AddWithValue("@TarifAdi", tarifAdi);
                command.Parameters.AddWithValue("@Kategori", kategori);
                command.Parameters.AddWithValue("@HazirlamaSuresi", hazirlamaSuresi);
                command.Parameters.AddWithValue("@Talimatlar", talimatlar);
                command.Parameters.AddWithValue("@ResimYolu", resimYolu);

                try
                {
                    command.ExecuteNonQuery();
                    tarifId = sqliteConnection.LastInsertRowId;
                    SaveIngredients(tarifId);
                    MessageBox.Show("Tarif başarıyla kaydedildi.");
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Tarif kaydetme sırasında bir hata oluştu: {ex.Message}");
                }
            }
        }

        private void araButonu_Click_1(object sender, EventArgs e)
        {
            LoadMalzemeler(araTextBox.Text.Trim());
        }
    }
}
