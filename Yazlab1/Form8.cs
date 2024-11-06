using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace Yazlab1
{
    public partial class TarifDetay : Form
    {
        private int selectedTarifID;
        private string databasePath = @"C:\Users\osman1\Desktop\Programlama\Yazlab\1\Yazlab\Yazlab1\TarifRehberiUygulamasi.db";

        public TarifDetay(int tarifID)
        {
            InitializeComponent();
            selectedTarifID = tarifID;
            this.StartPosition = FormStartPosition.CenterScreen;
            malzemelerPanel.AutoScroll = true;
            LoadTarifDetails();
        }

        private void LoadTarifDetails()
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
            {
                connection.Open();

                string query = "SELECT * FROM Tarifler WHERE TarifID = @TarifID";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TarifID", selectedTarifID);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tarifAdiLabel.Text = reader["TarifAdi"].ToString();
                            kategoriLabel.Text = reader["Kategori"].ToString();
                            hazirlamaSuresiLabel.Text = reader["HazirlamaSuresi"].ToString();
                            richTextBox1.Text = reader["Talimatlar"].ToString();
                            string resimYolu = reader["ResimYolu"].ToString();

                            try
                            {
                                pictureBox1.Image = Image.FromFile(resimYolu);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error loading image: {ex.Message}");
                            }
                        }
                    }
                }

                string malzemeQuery = @"
                                        SELECT m.MalzemeAdi, tm.MalzemeMiktar, m.MalzemeBirim, m.BirimFiyat
                                        FROM Malzemeler m
                                        JOIN TarifMalzeme tm ON m.MalzemeID = tm.MalzemeID
                                        WHERE tm.TarifID = @TarifID";

                using (SQLiteCommand malzemeCommand = new SQLiteCommand(malzemeQuery, connection))
                {
                    malzemeCommand.Parameters.AddWithValue("@TarifID", selectedTarifID);
                    using (SQLiteDataReader malzemeReader = malzemeCommand.ExecuteReader())
                    {
                        malzemelerPanel.Controls.Clear();

                        int cardCount = 0;
                        int cardWidth = 200;
                        int cardHeight = 50;
                        int padding = 10;
                        int columns = 2;
                        decimal toplamMaliyet = 0;

                        while (malzemeReader.Read())
                        {
                            string malzemeAdi = malzemeReader["MalzemeAdi"].ToString();
                            string miktar = malzemeReader["MalzemeMiktar"].ToString();
                            string birim = malzemeReader["MalzemeBirim"].ToString();
                            decimal birimFiyat = Convert.ToDecimal(malzemeReader["BirimFiyat"]);
                            decimal malzemeMiktar = Convert.ToDecimal(miktar);

                            decimal malzemeMaliyeti = malzemeMiktar * birimFiyat;
                            toplamMaliyet += malzemeMaliyeti;

                            Panel cardPanel = new Panel
                            {
                                Size = new Size(cardWidth, cardHeight),
                                BorderStyle = BorderStyle.FixedSingle
                            };

                            Label malzemeLabel = new Label
                            {
                                Text = $"{malzemeAdi} - {miktar} {birim}",
                                AutoSize = true,
                                Location = new Point(5, 15)
                            };

                            cardPanel.Controls.Add(malzemeLabel);

                            int row = cardCount / columns;
                            int col = cardCount % columns;

                            cardPanel.Location = new Point((cardWidth + padding) * col, (cardHeight + padding) * row);
                            malzemelerPanel.Controls.Add(cardPanel);

                            cardCount++;
                        }

                        maliyetLabel.Text = $"{toplamMaliyet:C}";
                    }
                }
            }
        }

        private void geriButton_Click(object sender, EventArgs e)
        {
            Tarifler tarifler = new Tarifler();
            tarifler.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
