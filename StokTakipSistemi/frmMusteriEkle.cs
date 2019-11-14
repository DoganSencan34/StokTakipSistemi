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
    public partial class frmMusteriEkle : Form
    {
        public frmMusteriEkle()
        {
            InitializeComponent();
        }

        private void IptalBtn_Click(object sender, EventArgs e)
        {
            frmMusteriTanimlari mstrtnm = new frmMusteriTanimlari();
            mstrtnm.Show();
            this.Hide();
        }
        public static string id;
        private void KaydetBtn_Click(object sender, EventArgs e)
        {
            if (MusteriAdiTxt.Text.Trim()== "")
            {
                MessageBox.Show("Müşteri Adını Boş Bırakmayınız.");
            }
            else if (MusteriTipiCb.SelectedIndex  == -1 || MusteriTipiCb.SelectedIndex == 0)
            {
                MessageBox.Show("Müşteri Tipini Boş Bırakmayınız.");
            }
            else if (TelefonTxt.Text.Trim() == "")
            {
                MessageBox.Show("Telefon Numarasını Boş Bırakmayınız.");
            }
            else
            {
                id = Form1.kullaniciid;
                string sql = "Insert Into Musteriler(MusteriKodu,MusteriAdi,MusteriTipi,Telefon,Telefon2,Fax,Mail,Ekleyen) VALUES('" + MusteriKoduEkleTxt.Text + "','" + MusteriAdiTxt.Text + "','" + MusteriTipiCb.SelectedIndex + "','" + TelefonTxt.Text + "','" +Telefon2Txt.Text + "','"+FaxTxt.Text+"','"+MailTxt.Text+"','"+id+"')";
                DataBase.ExecSql(sql);
                MessageBox.Show("Kaydedildi.");

                frmMusteriTanimlari mtanim = new frmMusteriTanimlari();
                mtanim.Show();
                this.Hide();
            }
        }
       
        private void frmMusteriEkle_Load(object sender, EventArgs e)
        {
            id=Form1.kullaniciid;
            string sql = "Select Adi from Kullanici where KullaniciId=" + id;
            sql = DataBase.select(sql).Rows[0]["Adi"].ToString() ;
            EkleyenTxt.Text = sql;



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void MailLbl_Click(object sender, EventArgs e)
        {

        }

        private void EkleyenLbl_Click(object sender, EventArgs e)
        {

        }

        private void MusteriKoduTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void MusteriAdiTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void TelefonTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Telefon2Txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void FaxTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void MailTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void EkleyenTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void MusteriTipiCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
