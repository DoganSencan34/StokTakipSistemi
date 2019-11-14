using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipSistemi
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }
        string dosyaYolu;
        string dosyaAdi;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png |  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu = dosya.FileName;
            pictureBox1.ImageLocation = dosyaYolu;



        }

        private void button2_Click(object sender, EventArgs e)
        {

            dosyaAdi = Path.GetFileName(dosyaYolu); //Dosya adını alma
            string kaynak = dosyaYolu;
            string hedef = Application.StartupPath + @"\resimler\";
            string yeniad = "Deneme" + ".png"; //Benzersiz isim verme
            File.Copy(kaynak, hedef + yeniad);
        }
    }
}
