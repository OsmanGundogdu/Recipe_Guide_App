using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace Yazlab1
{
    public partial class Tarifler : Form
    {
        private SQLiteConnection sqliteConnection;
        private string databasePath = @"C:\Users\osman1\Desktop\Programlama\Yazlab\1\Yazlab\Yazlab1\TarifRehberiUygulamasi.db";

        public Tarifler()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeDatabase();
            LoadIngredients();
            this.Load += Tarifler_Load;
            InitializeSortingComboBox();
        }

        private void LoadIngredients()
        {
            string query = "SELECT MalzemeID, MalzemeAdi FROM Malzemeler";

            using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int malzemeID = reader.GetInt32(0);
                        string malzemeAdi = reader.GetString(1);
                        malzemeCheckedListBox.Items.Add(new MalzemeItem(malzemeID, malzemeAdi));
                    }
                }
            }
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
            List<int> selectedMalzemeIDs = new List<int>();
            foreach (MalzemeItem item in malzemeCheckedListBox.CheckedItems)
            {
                selectedMalzemeIDs.Add(item.MalzemeID);
            }

            string searchText = araTextBox.Text.Trim();
            string sortQuery = GetSortQuery();
            string malzemeQuery = selectedMalzemeIDs.Count > 0
                ? $"AND TarifID IN (SELECT TarifID FROM TarifMalzeme WHERE MalzemeID IN ({string.Join(",", selectedMalzemeIDs)}))"
                : "";

            string query = string.IsNullOrWhiteSpace(searchText)
                ? $"SELECT TarifID, TarifAdi, ResimYolu FROM Tarifler WHERE 1=1 {malzemeQuery} {sortQuery}"
                : $"SELECT TarifID, TarifAdi, ResimYolu FROM Tarifler WHERE TarifAdi LIKE @searchText {malzemeQuery} {sortQuery}";

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
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
                                    Location = new Point(5, 5),
                                    SizeMode = PictureBoxSizeMode.StretchImage
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

                                Label nameLabel = new Label
                                {
                                    Text = tarifAdi,
                                    Location = new Point(5, 110),
                                    AutoSize = true
                                };

                                if (!isSufficient)
                                {
                                    decimal missingCost = CalculateMissingIngredientsCost(tarifID);
                                    Label costLabel = new Label
                                    {
                                        Text = $"Eksik Maliyet: {missingCost:C}",
                                        Location = new Point(5, 130),
                                        AutoSize = true,
                                        ForeColor = Color.Red
                                    };
                                    cardPanel.Controls.Add(costLabel);
                                }

                                Button detailButton = new Button
                                {
                                    Text = "Detay",
                                    Location = new Point(5, 150)
                                };
                                detailButton.Click += (s, e) =>
                                {
                                    OpenTarifDetay(tarifID);
                                };

                                cardPanel.Controls.Add(pictureBox);
                                cardPanel.Controls.Add(nameLabel);
                                cardPanel.Controls.Add(detailButton);

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

        private decimal CalculateMissingIngredientsCost(int tarifID)
        {
            decimal totalCost = 0;

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
            {
                connection.Open();

                string query = @"
                                SELECT SUM(m.BirimFiyat * (tm.MalzemeMiktar - m.ToplamMiktar)) AS MissingCost
                                FROM Malzemeler m
                                JOIN TarifMalzeme tm ON m.MalzemeID = tm.MalzemeID
                                WHERE tm.TarifID = @tarifID AND m.ToplamMiktar < tm.MalzemeMiktar";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tarifID", tarifID);
                    object result = command.ExecuteScalar();
                    totalCost = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }

            return totalCost;
        }

        private void LoadRecipeSuggestions(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                tarifOnerileriPanel.Controls.Clear();
                return;
            }

            string query = "SELECT TarifID, TarifAdi, ResimYolu FROM Tarifler WHERE EXISTS " +
                           "(SELECT 1 FROM TarifMalzeme WHERE TarifMalzeme.TarifID = Tarifler.TarifID " +
                           "AND MalzemeID IN (SELECT MalzemeID FROM Malzemeler WHERE MalzemeAdi LIKE @searchText));";

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
            {
                try
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            int cardCount = 0;
                            int cardWidth = 150;
                            int cardHeight = 200;
                            int padding = 10;
                            int columns = 5;

                            tarifOnerileriPanel.Controls.Clear();
                            panel2.AutoScroll = true;

                            while (reader.Read())
                            {
                                int tarifID = Convert.ToInt32(reader["TarifID"]);
                                string tarifAdi = reader["TarifAdi"].ToString();
                                string resimYolu = reader["ResimYolu"].ToString();
                                bool isSufficient = IsRecipeSufficient(tarifID);

                                Panel oneriTarifPanel = new Panel
                                {
                                    Size = new Size(cardWidth, cardHeight),
                                    BorderStyle = BorderStyle.FixedSingle,
                                    BackColor = isSufficient ? Color.LightGreen : Color.LightCoral
                                };

                                PictureBox pictureBox = new PictureBox
                                {
                                    Size = new Size(140, 100),
                                    Location = new Point(5, 5),
                                    SizeMode = PictureBoxSizeMode.StretchImage
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

                                Label nameLabel = new Label
                                {
                                    Text = tarifAdi,
                                    Location = new Point(5, 110),
                                    AutoSize = true
                                };

                                if (!isSufficient)
                                {
                                    decimal missingCost = CalculateMissingIngredientsCost(tarifID);
                                    Label costLabel = new Label
                                    {
                                        Text = $"Eksik Maliyet: {missingCost:C}",
                                        Location = new Point(5, 130),
                                        AutoSize = true,
                                        ForeColor = Color.Red
                                    };
                                    oneriTarifPanel.Controls.Add(costLabel);
                                }

                                Button detailButton = new Button
                                {
                                    Text = "Detay",
                                    Location = new Point(5, 150)
                                };
                                detailButton.Click += (s, e) =>
                                {
                                    OpenTarifDetay(tarifID);
                                };

                                oneriTarifPanel.Controls.Add(pictureBox);
                                oneriTarifPanel.Controls.Add(nameLabel);
                                oneriTarifPanel.Controls.Add(detailButton);

                                int row = cardCount / columns;
                                int col = cardCount % columns;

                                oneriTarifPanel.Location = new Point((cardWidth + padding) * col, (cardHeight + padding) * row);
                                tarifOnerileriPanel.Controls.Add(oneriTarifPanel);

                                cardCount++;
                            }

                            int totalRows = (int)Math.Ceiling((double)cardCount / columns);
                            tarifOnerileriPanel.Size = new Size((cardWidth + padding) * columns, (cardHeight + padding) * totalRows);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading recipe suggestions: {ex.Message}");
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

        private void OpenTarifDetay(int tarifID)
        {
            TarifDetay detayForm = new TarifDetay(tarifID);
            detayForm.Show();
            this.Close();
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

            if (!string.IsNullOrWhiteSpace(araTextBox.Text))
            {
                tarifOnerileriPanel.Controls.Clear();
                LoadRecipeSuggestions(araTextBox.Text);
            }
        }

        private void anaMenuButton_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }
    }

    public class MalzemeItem
    {
        public int MalzemeID { get; }
        public string MalzemeAdi { get; }

        public MalzemeItem(int malzemeID, string malzemeAdi)
        {
            MalzemeID = malzemeID;
            MalzemeAdi = malzemeAdi;
        }

        public override string ToString() => MalzemeAdi;
    }

}
