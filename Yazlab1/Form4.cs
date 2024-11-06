using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Yazlab1
{
    public partial class malzemeEkle : Form
    {
        private SQLiteConnection sqliteConnection;
        private string databasePath = @"C:\Users\osman1\Desktop\Programlama\Yazlab\1\Yazlab\Yazlab1\TarifRehberiUygulamasi.db";

        public malzemeEkle()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeDatabase();

            birimComboBox.Items.AddRange(new object[] { "Kilogram", "Gram", "Litre", "Mililitre", "Adet", "Diş", "Yemek Kaşığı" });
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

        private void button1_Click(object sender, EventArgs e)
        {
            string malzemeAdi = malzemeAdiTextBox.Text;
            decimal toplamMiktar = miktarNumericUpDown.Value;
            string malzemeBirim = birimComboBox.SelectedItem?.ToString();
            decimal birimFiyat = birimFiyatNumericUpDown.Value;

            if (MalzemeVarMi(malzemeAdi))
            {
                MessageBox.Show("Bu isimde bir malzeme zaten mevcut, lütfen başka bir malzeme adı giriniz.");
                return;
            }

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Malzemeler (MalzemeAdi, ToplamMiktar, MalzemeBirim, BirimFiyat) " +
                                   "VALUES (@malzemeAdi, @toplamMiktar, @malzemeBirim, @birimFiyat)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@malzemeAdi", malzemeAdi);
                        command.Parameters.AddWithValue("@toplamMiktar", toplamMiktar);
                        command.Parameters.AddWithValue("@malzemeBirim", malzemeBirim);
                        command.Parameters.AddWithValue("@birimFiyat", birimFiyat);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Malzeme başarıyla eklendi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabanına ekleme hatası: {ex.Message}");
                }
            }

            malzemeAdiTextBox.Clear();
            miktarNumericUpDown.Value = 0;
            birimComboBox.SelectedIndex = -1;
            birimFiyatNumericUpDown.Value = 0;
        }

        private bool MalzemeVarMi(string malzemeAdi)
        {
            string query = "SELECT COUNT(*) FROM Malzemeler WHERE MalzemeAdi = @malzemeAdi";
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
            {
                try
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@malzemeAdi", malzemeAdi);
                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Malzeme kontrolü sırasında bir hata oluştu: {ex.Message}");
                    return true;
                }
            }
        }

        private void anaMenuButton_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void geriButton_Click(object sender, EventArgs e)
        {
            TarifEklemeFormu tarifEkle = new TarifEklemeFormu();
            tarifEkle.Show();
            this.Close();
        }
    }
}
