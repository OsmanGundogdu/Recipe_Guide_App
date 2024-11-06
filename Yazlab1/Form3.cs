using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace Yazlab1
{
    public partial class TarifGuncellemeFormu : Form
    {
        private SQLiteConnection sqliteConnection;
        private string databasePath = @"C:\Users\osman1\Desktop\Programlama\Yazlab\1\Yazlab\Yazlab1\TarifRehberiUygulamasi.db";
        private List<List<string>> malzemeListesi;
        private int? tarifID;

        public TarifGuncellemeFormu(int tarifID, string tarifAdi, int hazirlamaSuresi, string kategori, string talimatlar, string resimYolu)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeDatabase();
            malzemeListesi = new List<List<string>>();
            this.tarifID = tarifID;

            tarifAdiTextBox.Text = tarifAdi;
            sureNumericUpDown.Value = hazirlamaSuresi;

            anaYemekCheckBox.Checked = kategori == "Ana Yemek";
            tatliCheckBox.Checked = kategori == "Tatlı";
            corbaCheckBox.Checked = kategori == "Çorba";
            salataCheckBox.Checked = kategori == "Salata";
            atistirmalikCheckBox.Checked = kategori == "Atıştırmalık";

            richTextBox1.Text = talimatlar;
            urlTextBox.Text = resimYolu;

            LoadMalzemeler(tarifID);
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

        private void LoadMalzemeler(int? tarifID = null, string aramaKelimesi = null)
        {
            string query = tarifID.HasValue
                ? "SELECT Malzemeler.MalzemeID, MalzemeAdi, MalzemeBirim, IFNULL(TarifMalzeme.MalzemeMiktar, 0) AS KullanilacakMiktar " +
                  "FROM Malzemeler LEFT JOIN TarifMalzeme ON Malzemeler.MalzemeID = TarifMalzeme.MalzemeID AND TarifMalzeme.TarifID = @TarifID " +
                  "WHERE MalzemeAdi LIKE @AramaKelimesi"
                : "SELECT MalzemeID, MalzemeAdi, MalzemeBirim FROM Malzemeler";

            using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
            {
                if (tarifID.HasValue)
                {
                    command.Parameters.AddWithValue("@TarifID", tarifID.Value);
                }

                command.Parameters.AddWithValue("@AramaKelimesi", "%" + (aramaKelimesi ?? "") + "%");

                try
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        panel1.Controls.Clear();
                        panel1.AutoScroll = true;
                        int cardCount = 0;
                        int cardWidth = 190, cardHeight = 130, padding = 10, columns = 4;

                        while (reader.Read())
                        {
                            string malzemeId = reader["MalzemeID"].ToString();
                            string malzemeAdi = reader["MalzemeAdi"].ToString();
                            string malzemeBirim = reader["MalzemeBirim"].ToString();
                            string kullanilacakMiktar = reader["KullanilacakMiktar"].ToString();

                            Panel cardPanel = new Panel { Size = new Size(cardWidth, cardHeight), BorderStyle = BorderStyle.FixedSingle };
                            Label nameLabel = new Label { Text = $"M.adı: {malzemeAdi}", Location = new Point(5, 5), AutoSize = true };
                            Label birimLabel = new Label { Text = $"Birim: {malzemeBirim}", Location = new Point(5, 25), AutoSize = true };
                            TextBox kullanilacakMiktarTextBox = new TextBox
                            {
                                Location = new Point(5, 65),
                                Width = 60,
                                Text = kullanilacakMiktar
                            };

                            Button guncelleButonu = new Button
                            {
                                Text = "Güncelle",
                                Location = new Point(70, 65),
                                Width = 85
                            };

                            guncelleButonu.Click += (s, e) =>
                            {
                                UpdateIngredient(malzemeId, kullanilacakMiktarTextBox.Text);
                            };

                            cardPanel.Controls.Add(nameLabel);
                            cardPanel.Controls.Add(birimLabel);
                            cardPanel.Controls.Add(kullanilacakMiktarTextBox);
                            cardPanel.Controls.Add(guncelleButonu);

                            cardPanel.Location = new Point((cardWidth + padding) * (cardCount % columns), (cardHeight + padding) * (cardCount / columns));
                            panel1.Controls.Add(cardPanel);
                            cardCount++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Malzemeler yüklenirken hata oluştu: {ex.Message}");
                }
            }
        }

        private void guncelleButonu_Click(object sender, EventArgs e)
        {
            string kategori = null;

            if (anaYemekCheckBox.Checked) kategori = "Ana Yemek";
            else if (tatliCheckBox.Checked) kategori = "Tatlı";
            else if (corbaCheckBox.Checked) kategori = "Çorba";
            else if (salataCheckBox.Checked) kategori = "Salata";
            else if (atistirmalikCheckBox.Checked) kategori = "Atıştırmalık"; 

            string tarifAdi = tarifAdiTextBox.Text;
            string hazirlamaSuresi = sureNumericUpDown.Value.ToString();
            string talimatlar = richTextBox1.Text;
            string resimYolu = urlTextBox.Text;

            if (string.IsNullOrEmpty(tarifAdi) || string.IsNullOrEmpty(kategori) || string.IsNullOrEmpty(hazirlamaSuresi) || string.IsNullOrEmpty(talimatlar) || string.IsNullOrEmpty(resimYolu))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.");
                return;
            }

            string query = tarifID.HasValue
                ? "UPDATE Tarifler SET TarifAdi = @TarifAdi, Kategori = @Kategori, HazirlamaSuresi = @HazirlamaSuresi, Talimatlar = @Talimatlar, ResimYolu = @ResimYolu WHERE TarifID = @TarifID"
                : "INSERT INTO Tarifler (TarifAdi, Kategori, HazirlamaSuresi, Talimatlar, ResimYolu) VALUES (@TarifAdi, @Kategori, @HazirlamaSuresi, @Talimatlar, @ResimYolu); SELECT last_insert_rowid();";

            using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
            {
                command.Parameters.AddWithValue("@TarifAdi", tarifAdi);
                command.Parameters.AddWithValue("@Kategori", kategori);
                command.Parameters.AddWithValue("@HazirlamaSuresi", hazirlamaSuresi);
                command.Parameters.AddWithValue("@Talimatlar", talimatlar);
                command.Parameters.AddWithValue("@ResimYolu", resimYolu);

                if (tarifID.HasValue)
                {
                    command.Parameters.AddWithValue("@TarifID", tarifID);
                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Tarif başarıyla güncellendi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Tarif güncelleme sırasında bir hata oluştu: {ex.Message}");
                    }
                }
                else
                {
                    try
                    {
                        long yeniTarifId = (long)command.ExecuteScalar();
                        SaveIngredients(yeniTarifId);
                        MessageBox.Show("Tarif başarıyla kaydedildi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Tarif kaydetme sırasında bir hata oluştu: {ex.Message}");
                    }
                }
            }
        }

        private void UpdateIngredient(string malzemeId, string kullanilacakMiktar)
        {
            string checkQuery = "SELECT COUNT(*) FROM Malzemeler WHERE MalzemeID = @MalzemeID";

            using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, sqliteConnection))
            {
                checkCommand.Parameters.AddWithValue("@MalzemeID", malzemeId);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count == 0)
                {
                    string insertQuery = "INSERT INTO Malzemeler (MalzemeID, MalzemeAdi, MalzemeBirim) VALUES (@MalzemeID, @MalzemeAdi, @MalzemeBirim)";
                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, sqliteConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@MalzemeID", malzemeId);
                        insertCommand.Parameters.AddWithValue("@MalzemeAdi", "Yeni Malzeme Adı");
                        insertCommand.Parameters.AddWithValue("@MalzemeBirim", "Yeni Birim");
                        insertCommand.ExecuteNonQuery();
                    }
                }

                string updateQuery = "INSERT INTO TarifMalzeme (TarifID, MalzemeID, MalzemeMiktar) VALUES (@TarifID, @MalzemeID, @MalzemeMiktar) " +
                                     "ON CONFLICT (TarifID, MalzemeID) DO UPDATE SET MalzemeMiktar = @MalzemeMiktar";
                using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, sqliteConnection))
                {
                    updateCommand.Parameters.AddWithValue("@TarifID", tarifID); 
                    updateCommand.Parameters.AddWithValue("@MalzemeID", malzemeId);
                    updateCommand.Parameters.AddWithValue("@MalzemeMiktar", kullanilacakMiktar);
                    updateCommand.ExecuteNonQuery();
                }

                if (string.IsNullOrEmpty(kullanilacakMiktar) || kullanilacakMiktar == "0")
                {
                    string deleteQuery = "DELETE FROM TarifMalzeme WHERE TarifID = @TarifID AND MalzemeID = @MalzemeID";
                    using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, sqliteConnection))
                    {
                        deleteCommand.Parameters.AddWithValue("@TarifID", tarifID);
                        deleteCommand.Parameters.AddWithValue("@MalzemeID", malzemeId);
                        deleteCommand.ExecuteNonQuery();
                    }
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

        private void malzemeOlusturButonu_Click(object sender, EventArgs e)
        {
            malzemeEkle malzemeEkleGuncelle = new malzemeEkle();
            malzemeEkleGuncelle.Show();
            LoadMalzemeler();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void corbaCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void anaMenuButton_Click_1(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void araButonu_Click(object sender, EventArgs e)
        {
            string aramaKelimesi = araTextBox.Text.Trim();
            LoadMalzemeler(tarifID, aramaKelimesi);
        }
    }
}
