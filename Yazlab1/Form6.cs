using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yazlab1
{
    public partial class TarifSil : Form
    {
        private SQLiteConnection sqliteConnection;
        private string databasePath = @"C:\Users\osman1\Desktop\Programlama\Yazlab\1\Yazlab\Yazlab1\TarifRehberiUygulamasi.db";

        public TarifSil()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeDatabase();
            this.Load += Tarifler_Load;
            InitializeSortingComboBox();
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
                MessageBox.Show($"Failed to connect to SQLite database. Error: {ex.Message}");
            }
        }

        private void Tarifler_Load(object sender, EventArgs e)
        {
            LoadRecipes();
        }

        private void InitializeSortingComboBox()
        {
            sortingComboBox.Items.AddRange(new string[]
            {
                "En hızlıdan en yavaşa",
                "En yavaştan en hızlıya",
                "Maliyet (artan)",
                "Maliyet (azalan)",
                "Malzeme sayısı (azdan çoğa)",
                "Malzeme sayısı (çoktan aza)",
                "Kategoriye göre"
            });
            sortingComboBox.SelectedIndexChanged += SortingComboBox_SelectedIndexChanged;
        }

        private void SortingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            string searchText = araTextBox.Text.Trim();
            string sortQuery = GetSortQuery();

            string query = string.IsNullOrWhiteSpace(searchText)
                ? $"SELECT TarifID, TarifAdi, ResimYolu FROM Tarifler {sortQuery}"
                : $"SELECT TarifID, TarifAdi, ResimYolu FROM Tarifler WHERE TarifAdi LIKE @searchText {sortQuery}";

            string connectionString = $"Data Source={databasePath};Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(searchText))
                        {
                            command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                        }

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            int cardCount = 0;
                            int cardWidth = 150;
                            int cardHeight = 200;
                            int padding = 10;
                            int columns = 5;

                            panel1.Controls.Clear();
                            scrollablePanel.AutoScroll = true;

                            while (reader.Read())
                            {
                                int tarifID = Convert.ToInt32(reader["TarifID"]);
                                string tarifAdi = reader["TarifAdi"].ToString();
                                string resimYolu = reader["ResimYolu"].ToString();
                                bool isSufficient = IsRecipeSufficient(tarifID);

                                Panel cardPanel = new Panel
                                {
                                    Size = new Size(cardWidth, cardHeight),
                                    BorderStyle = BorderStyle.FixedSingle,
                                    BackColor = isSufficient ? Color.LightGreen : Color.LightCoral
                                };

                                PictureBox pictureBox = new PictureBox
                                {
                                    Size = new Size(140, 100),
                                    Location = new Point(5, 5)
                                };

                                try
                                {
                                    using (Image fullImage = Image.FromFile(resimYolu))
                                    {
                                        Image thumbnailImage = fullImage.GetThumbnailImage(140, 100, null, IntPtr.Zero);
                                        pictureBox.Image = thumbnailImage;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error loading image: {ex.Message}");
                                    continue;
                                }

                                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                                Label nameLabel = new Label
                                {
                                    Text = tarifAdi,
                                    Location = new Point(5, 110),
                                    AutoSize = true
                                };

                                Button deleteButton = new Button
                                {
                                    Text = "Sil",
                                    Location = new Point(5, 150)
                                };

                                deleteButton.Click += async (s, e) =>
                                {
                                    DialogResult dialogResult = MessageBox.Show($"{tarifAdi} silinecek, emin misiniz?", "Tarifi Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        using (var sqlConnection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
                                        {
                                            try
                                            {
                                                await sqlConnection.OpenAsync();
                                                using (SQLiteCommand deleteCommand = new SQLiteCommand("DELETE FROM Tarifler WHERE TarifID = @tarifID", sqlConnection))
                                                {
                                                    deleteCommand.Parameters.AddWithValue("@tarifID", tarifID);
                                                    deleteCommand.ExecuteNonQuery();

                                                    MessageBox.Show($"{tarifAdi} başarıyla silindi. İlgili malzemeler de silindi.", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    panel1.Controls.Remove(cardPanel);
                                                    LoadRecipes();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show($"Silme işlemi sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Silme işlemi iptal edildi.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                };

                                cardPanel.Controls.Add(pictureBox);
                                cardPanel.Controls.Add(nameLabel);
                                cardPanel.Controls.Add(deleteButton);

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading recipes: {ex.Message}");
                }
            }
        }

        private string GetSortQuery()
        {
            switch (sortingComboBox.SelectedItem?.ToString())
            {
                case "En hızlıdan en yavaşa":
                    return "ORDER BY HazirlamaSuresi ASC";
                case "En yavaştan en hızlıya":
                    return "ORDER BY HazirlamaSuresi DESC";
                case "Maliyet (artan)":
                    return @"
                        ORDER BY (
                            SELECT SUM(tm.MalzemeMiktar * m.BirimFiyat)
                            FROM TarifMalzeme tm
                            JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID
                            WHERE tm.TarifID = Tarifler.TarifID
                        ) ASC";
                case "Maliyet (azalan)":
                    return @"
                        ORDER BY (
                            SELECT SUM(tm.MalzemeMiktar * m.BirimFiyat)
                            FROM TarifMalzeme tm
                            JOIN Malzemeler m ON tm.MalzemeID = m.MalzemeID
                            WHERE tm.TarifID = Tarifler.TarifID
                        ) DESC";
                case "Malzeme sayısı (azdan çoğa)":
                    return "ORDER BY (SELECT COUNT(*) FROM TarifMalzeme WHERE TarifID = Tarifler.TarifID) ASC";
                case "Malzeme sayısı (çoktan aza)":
                    return "ORDER BY (SELECT COUNT(*) FROM TarifMalzeme WHERE TarifID = Tarifler.TarifID) DESC";
                case "Kategoriye göre":
                    return "ORDER BY Kategori";
                default:
                    return "";
            }
        }

        private bool IsRecipeSufficient(int tarifID)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
            {
                connection.Open();

                string query = @"
                                SELECT CASE WHEN COUNT(*) = SUM(CASE WHEN m.ToplamMiktar >= tm.MalzemeMiktar THEN 1 ELSE 0 END)
                                            THEN 1 ELSE 0 END AS IsSufficient
                                FROM Malzemeler m
                                JOIN TarifMalzeme tm ON m.MalzemeID = tm.MalzemeID
                                WHERE tm.TarifID = @tarifID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tarifID", tarifID);
                    int isSufficient = Convert.ToInt32(command.ExecuteScalar());

                    return isSufficient == 1;
                }
            }
        }

        private void araButonu_Click(object sender, EventArgs e)
        {
            LoadRecipes();
        }

        private void anaMenuButton_Click_1(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }
    }
}
