using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipSistemi
{
    public partial class frmSifreYenilemeKod : Form
    {
        public frmSifreYenilemeKod()
        {
            InitializeComponent();
        }
        public string kullaniciid;
        private void frmSifreYenilemeKod_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "Select * from SifremiUnuttum where Kid="+kullaniciid;
            DataTable table = DataBase.select(sorgu);
            if (table.Rows[0]["Kod"].ToString()==SifreKoduTxt.Text)
            {
                if (YeniSifreTxt.Text != YeniSifreTekrarTxt.Text)
                {
                    MessageBox.Show("Şifreler Uyuşmuyor..");
                    return;
                }
                else
                {
                    string degistir = "Update Kullanici set Sifre='" + YeniSifreTxt.Text + "' where KullaniciId=" + kullaniciid;
                    DataBase.ExecSql(degistir);
                    MessageBox.Show("Değiştirildi");
                    string deger = "Select * from Kullanici where KullaniciId=" + kullaniciid;
                    DataTable tbl = DataBase.select(deger);
                    Anasayfa anasayfa = new Anasayfa();
                    anasayfa.sfrunuttum = tbl.Rows[0]["KullaniciId"].ToString();
                    string kodsil = "Update SifremiUnuttum set Guncelleme_Tarih='"+ DateTime.Today.ToString("yyyy-MM-dd") + "',Durum=0 where Kid=" + kullaniciid;
                    DataBase.ExecSql(kodsil);
                    anasayfa.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Girdiğiniz Kod Yanlış");
            }
        }
    }
}
