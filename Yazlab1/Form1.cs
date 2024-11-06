using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yazlab1
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tarifler tarifler = new Tarifler();
            tarifler.Show();
            this.Hide();
        }

        private void tarifEkleButton_Click(object sender, EventArgs e)
        {
            TarifEklemeFormu tarifEkle = new TarifEklemeFormu();
            tarifEkle.Show();
            this.Hide();
        }

        private void tarifGuncelleButton_Click(object sender, EventArgs e)
        {
            TarifGuncelle tarifGuncelle = new TarifGuncelle();
            tarifGuncelle.Show();
            this.Hide();
            
        }

        private void tarifSilButton_Click(object sender, EventArgs e)
        {
            TarifSil tarifSil = new TarifSil();
            tarifSil.Show();
            this.Hide();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {

        }
    }
}
