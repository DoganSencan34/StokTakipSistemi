using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipSistemi
{
    public partial class frmSifreYenileme : Form
    {
        public frmSifreYenileme()
        {
            InitializeComponent();
        }
        private string CreatePassword(int length)
        {
            string chars= "ABCDEFGHIJKLMNOPRSTUVYZWXQ1234567890"; 
            StringBuilder Password = new StringBuilder();
            Random Rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                Password.Append(chars[Rnd.Next(0, chars.Length)]);
            }

            return Password.ToString();
        }
        char karakter;
        int sayi;
        private void button1_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!reg.IsMatch(emailtxt.Text))
            {
                MessageBox.Show("Geçersiz Mail Adresi");
                return;
            }
            string sifre = CreatePassword(6);
            string emailsorgu = "select * from Kullanici where Email='" + emailtxt.Text+"'";
            if (DataBase.select(emailsorgu).Rows.Count>0)
            {
                        DataTable table = DataBase.select(emailsorgu);
                        MailMessage ePosta = new MailMessage();
                        ePosta.From = new MailAddress("stoktakipsistemleri@gmail.com");
                        ePosta.To.Add(emailtxt.Text);
                        ePosta.Subject = "parola yenile";
                        ePosta.Body =sifre;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Credentials = new System.Net.NetworkCredential("stoktakipsistemleri@gmail.com", "asasas123123123");
                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        object userState = ePosta;
                        bool kontrol = true;
                        try
                        {
                            smtp.SendAsync(ePosta, (object)ePosta);
                            string kodekle = "INSERT INTO SifremiUnuttum(Mail,Kod,Kid,Olusturulan_Tarih,Durum) Values('"
                        + emailtxt.Text + "','" + sifre + "','" + table.Rows[0]["KullaniciId"].ToString() +"','"+ DateTime.Today.ToString("yyyy-MM-dd") + "','1')";
                            DataBase.ExecSql(kodekle);
                            string sorgu = "Select KullaniciId from Kullanici where Email='" + emailtxt.Text.Trim()+"'";
                            DataTable tbl = DataBase.select(sorgu);
                            frmSifreYenilemeKod frmSifreYenilemeKod = new frmSifreYenilemeKod();
                            frmSifreYenilemeKod.kullaniciid = tbl.Rows[0]["KullaniciId"].ToString();
                            frmSifreYenilemeKod.Show();
                            this.Hide();
                        }
                        catch (SmtpException ex)
                        {
                            kontrol = false;
                            System.Windows.Forms.MessageBox.Show(ex.Message, "Mail Gönderme Hatasi");
                        }
            }
            else
            {
                MessageBox.Show("Mail Adresine Kayıtlı Kullanıcı Yoktur..");
            }
        }

        private void frmSifreYenileme_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
