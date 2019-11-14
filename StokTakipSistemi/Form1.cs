using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            try
            {
                Thread t = new Thread(new ThreadStart(StartForm));
                t.Start();
                Thread.Sleep(2000);
                InitializeComponent();
                t.Abort();
            }
            catch (Exception)
            {
                return;
            }
        }
        public void StartForm()
        {
            try
            {
                Application.Run(new frmSplash());
            }
            catch (Exception)
            {

                return;
            }
        }

        

        

       

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (Application.OpenForms["Form1"] == null)
                {
                    Form1 yenipencere = new Form1();
                    yenipencere.ShowDialog();
                }
                else
                {
                    Application.OpenForms["Form1"].Activate();
                }
            }
            catch (Exception)
            {

                return;
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        public static string kullaniciadi="";
        public static string kullaniciid;
        public static string mail;
        public static string KullaniciId;
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string kullanici = "select * from Kullanici where KullaniciAdi='" + textBox1.Text + "' and Sifre='" + textBox2.Text + "'";
                    if (DataBase.select(kullanici).Rows.Count > 0)
                    {
                        DataTable table = DataBase.select(kullanici);
                        Anasayfa anasyf = new Anasayfa();
                        kullaniciadi = table.Rows[0]["Adi"].ToString() + " " + table.Rows[0]["Soyadi"].ToString();
                        mail = table.Rows[0]["Email"].ToString();
                        kullaniciid = table.Rows[0]["KullaniciId"].ToString();
                        anasyf.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Adı Veya Şifre Yanlış");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Geçersiz!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
        DataTable table;
        private void button1_Click_1(object sender, EventArgs e)
        {   try
            {
                string kullaniciadi = "select * from Kullanici where KullaniciAdi='" + textBox1.Text + "'";
                DataTable tbl = DataBase.select(kullaniciadi);
                if (tbl.Rows.Count>0)
                {

               
                    string sifreyenilemekontrol = "Select * from SifremiUnuttum where Kid='" + tbl.Rows[0]["KullaniciId"].ToString()+"' and Durum=1";
                    if (DataBase.select(sifreyenilemekontrol).Rows.Count>0)
                    {
                        MessageBox.Show("Daha Önceden Şifre Yenileme İsteğinde Bulunduğunuz İçin Şifre Yenileme Sayfasına Yönlendiriliyorsunuz");
                        frmSifreYenilemeKod sifreYenilemeKod = new frmSifreYenilemeKod();
                        sifreYenilemeKod.kullaniciid = tbl.Rows[0]["KullaniciId"].ToString();
                        sifreYenilemeKod.Show();
                        return;
                    }
                    string kullanici = "select * from Kullanici where KullaniciAdi='" + textBox1.Text + "' and Sifre='" + textBox2.Text + "'";
                    if (DataBase.select(kullanici).Rows.Count > 0)
                    {
                        table = DataBase.select(kullanici);
                        Anasayfa anasyf = new Anasayfa();
                        anasyf.kladi = table.Rows[0]["Adi"].ToString() + " " + table.Rows[0]["Soyadi"].ToString();
                        mail = table.Rows[0]["Email"].ToString();
                        kullaniciid = table.Rows[0]["KullaniciId"].ToString();
                        anasyf.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Adı Veya Şifre Yanlış");
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı Veya Şifre Yanlış");
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }
        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            frmSifreYenileme frmSifreYenileme = new frmSifreYenileme();
            frmSifreYenileme.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
