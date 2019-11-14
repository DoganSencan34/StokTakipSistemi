using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipSistemi
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
        public string kladi;
        public string sfrunuttum = "";
        private void Anasayfa_Load(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection("server=DESKTOP-86INSUI\\DGN; user id=dgn; password=asasas123123123; database=StokTakipSistemi;");
            bag.Open();
            string card = @"select TOP 5 urn.UrunAdi,SUM(fh.Miktar) as Adet from FisHaraket fh,Urunler urn where fh.UrunRef=urn.UrunRef  and fh.FisTipi=2
                                GROUP BY urn.UrunAdi
                                ORDER BY SUM(fh.Miktar) desc";
            SqlCommand cmd = new SqlCommand(card, bag);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Urunler"].Points.AddXY(dr[0].ToString(),dr[1]);
            }
            bag.Close();
            bag.Open();
            string card2 = @"select kl.Adi,COUNT(fb.KullaniciRef) as Haraket from FisBaslik fb,Kullanici kl where fb.KullaniciRef=kl.KullaniciId
                            GROUP BY kl.Adi
                            order by COUNT(fb.KullaniciRef) desc";
            SqlCommand cmd2 = new SqlCommand(card2, bag);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                chart2.Series["Kullanici"].Points.AddXY(dr2[0].ToString(), dr2[1]);
            }
            bag.Close();
                ToolTip toolTip = new ToolTip();
            if (sfrunuttum !="")
            {
                string deger = "Select * from Kullanici where KullaniciId=" + sfrunuttum;
                DataTable tbl = DataBase.select(deger);
                KullaniciAdiLbl.Text = tbl.Rows[0]["Adi"].ToString();
                KullaniciIdLabel.Text = tbl.Rows[0]["KullaniciId"].ToString();
                KullaniciMailLabel.Text= tbl.Rows[0]["Email"].ToString();
                sfrunuttum = "";
            }
            else
            {
                KullaniciAdiLbl.Text = kladi;
                KullaniciIdLabel.Text = Form1.kullaniciid;
                KullaniciMailLabel.Text = Form1.mail;
            }
            string yetki = "Select Kyet from Kullanici where KullaniciId='" + KullaniciIdLabel.Text + "'";
            DataTable tble = DataBase.select(yetki);
            if (tble.Rows[0]["Kyet"].ToString() == "0" || tble.Rows[0]["Kyet"].ToString() == "")
            {
                button7.Visible = false;

            }
            string sql = "Select Resim from Kullanici where KullaniciId='" + KullaniciIdLabel.Text + "'";
            try
            {
                if (DataBase.select(sql).Rows.Count>0)
                {
                    DataTable table = DataBase.select(sql);
                      pictureBox1.ImageLocation = Application.StartupPath + @"\resimler\"+table.Rows[0]["Resim"].ToString();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Yüklenemedi." + hata.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            KullaniciTanimlari ktanimlari = new KullaniciTanimlari();
            ktanimlari.Show();
            this.Hide();
        }
        public static string kullaniciid;
        private void button1_Click(object sender, EventArgs e)
        {
            UrunTanimlari urn = new UrunTanimlari();
            kullaniciid = KullaniciIdLabel.Text;
            urn.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmMusteriTanimlari frm = new frmMusteriTanimlari();
            kullaniciid = KullaniciIdLabel.Text;
            frm.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UrunTanimlari urn = new UrunTanimlari();
            kullaniciid = KullaniciIdLabel.Text;
            urn.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmMusteriTanimlari frm = new frmMusteriTanimlari();
            kullaniciid = KullaniciIdLabel.Text;
            frm.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmCikisFisi frmCikisFisi = new frmCikisFisi();
            frmCikisFisi.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            KullaniciTanimlari ktanimlari = new KullaniciTanimlari();
            ktanimlari.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            frmGirisFisi grsfis = new frmGirisFisi();
            grsfis.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            frmCikisFisi frmCikisFisi = new frmCikisFisi();
            frmCikisFisi.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            test test = new test();
            test.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Raporlar rpr = new Raporlar();
            rpr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Raporlar rpr = new Raporlar();
            rpr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmGirisFisi grsfis = new frmGirisFisi();
            grsfis.Show();
            this.Hide();
        }

        private void KullaniciMailLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
