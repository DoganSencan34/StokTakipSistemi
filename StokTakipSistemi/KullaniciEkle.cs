using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipSistemi
{
    public partial class KullaniciEkle : Form
    {
        public KullaniciEkle()
        {
            InitializeComponent();
        }

        private void btnBase64String_Click(object sender, EventArgs e)
        {
           
        }


        string resimPath;


        string dosyaYolu;
        string dosyaAdi;
        private void button1_Click(object sender, EventArgs e)
        {


            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png ;*.jpeg;*.gif|  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu = dosya.FileName;
            textBox6.Text = dosyaYolu;
            pictureBox1.ImageLocation = dosyaYolu;



        }


        byte[] resim ;
        string byteFile;
        private void button2_Click(object sender, EventArgs e)
        {
            string sql="";



            Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!reg.IsMatch(textBox3.Text))
            {
                MessageBox.Show("Geçersiz Mail Adresi");
                return;
            }
            string kontrol = "Select * from Kullanici where KullaniciAdi='" + textBox4.Text+"'";
            if (DataBase.select(kontrol).Rows.Count>0)
            {
                MessageBox.Show("Kullanıcı Adı Mevcut.");
                return;
            }





            

            dosyaAdi = Path.GetFileName(dosyaYolu); //Dosya adını alma
            string kaynak = dosyaYolu;
            string hedef = Application.StartupPath + @"\resimler\";
            string yeniad = textBox4.Text + ".jpg"; //Benzersiz isim verme
            if (textBox6.Text != "")
            {

            
                if (kyet.Checked && uyet.Checked && uyet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Resim,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','"+yeniad+"','1','1','1')";
                }
                else if (kyet.Checked && uyet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Resim,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + yeniad + "','1','1','0')";
                }
                else if (kyet.Checked && myet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Resim,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + yeniad + "','1','0','1')";
                }
                else if (uyet.Checked && myet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Resim,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + yeniad + "','0','1','1')";
                }
                else if (kyet.Checked )
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Resim,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + yeniad + "','1','0','0')";
                }
                else if (uyet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Resim,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + yeniad + "','0','1','0')";
                }
                else if (myet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Resim,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + yeniad + "','0','0','1')";
                }
               


                File.Copy(kaynak, hedef + yeniad);

                DataBase.ExecSql(sql);

            }

            else
            {

                if (kyet.Checked && uyet.Checked && uyet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text +  "','1','1','1')";
                }
                else if (kyet.Checked && uyet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text +  "','1','1','0')";
                }
                else if (kyet.Checked && myet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','1','0','1')";
                }
                else if (uyet.Checked && myet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','0','1','1')";
                }
                else if (kyet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text +  "','1','0','0')";
                }
                else if (uyet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','0','1','0')";
                }
                else if (myet.Checked)
                {
                    sql = "INSERT INTO Kullanici(Adi,Soyadi,Email,KullaniciAdi,Sifre,Kyet,Uyet,Myet) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text +  "','0','0','1')";
                }


             

                DataBase.ExecSql(sql);





            }
            
            MessageBox.Show("Kaydedildi");


            
        }
        public string kid="";
        private void KullaniciEkle_Load(object sender, EventArgs e)
        {
            if (kid!="")
            {
                button2.Visible = false;
                btn_guncelle.Visible = true;
                string sql = "Select * from Kullanici where KullaniciId=" + kid;
                DataTable kullanicitable = DataBase.select(sql);

                textBox1.Text = kullanicitable.Rows[0]["Adi"].ToString();
                textBox2.Text = kullanicitable.Rows[0]["Soyadi"].ToString();
                textBox3.Text = kullanicitable.Rows[0]["Email"].ToString();
                textBox4.Text = kullanicitable.Rows[0]["KullaniciAdi"].ToString();
                textBox5.Text = kullanicitable.Rows[0]["Sifre"].ToString();
                textBox6.Text = kullanicitable.Rows[0]["Resim"].ToString();

                if (kullanicitable.Rows[0]["Kyet"].ToString()=="1")
                {
                    kyet.Checked=true;
                }
                if (kullanicitable.Rows[0]["Uyet"].ToString() == "1")
                {
                    uyet.Checked = true;
                }
                if (kullanicitable.Rows[0]["Myet"].ToString() == "1")
                {
                    myet.Checked = true;
                }

            }//Güncelleme veri yazdırma bitiş


        }
        string sql = "";
        private void btn_guncelle_Click(object sender, EventArgs e)
        {
          



            Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!reg.IsMatch(textBox3.Text))
            {
                MessageBox.Show("Geçersiz Mail Adresi");
                return;
            }
            string kontrol = "Select KullaniciAdi,Resim from Kullanici where KullaniciId='" + kid+"'";
            DataTable kntrltbl = DataBase.select(kontrol);

           

            if (textBox4.Text !=kntrltbl.Rows[0]["KullaniciAdi"].ToString())
            {
                string kullaniciadivarmi="Select * from Kullanici where KullaniciAdi='"+textBox4.Text+"'";
                if (DataBase.select(kullaniciadivarmi).Rows.Count>0)
                {
                    MessageBox.Show("Kullanıcı Adı Mevcut");
                    return;
                }
            }

            if (textBox6.Text != kntrltbl.Rows[0]["Resim"].ToString())
            {
                
                string path = Application.StartupPath + @"\resimler\";
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] rgFiles = di.GetFiles();
                foreach (FileInfo fi in rgFiles)
                {
                    //fi.Name bize dosyanın adını dönüyor.
                    //fi.FullName ise bize dosyasının dizin bilgisini döner.


                    if (fi.Name == kntrltbl.Rows[0]["Resim"].ToString())
                    {
                        File.Delete(path+kntrltbl.Rows[0]["Resim"].ToString());
                    }
                }


            }





            dosyaAdi = Path.GetFileName(dosyaYolu); //Dosya adını alma
            string kaynak = dosyaYolu;
            string hedef = Application.StartupPath + @"\resimler\";
            string yeniad = textBox4.Text + ".jpg"; //Benzersiz isim verme
            if (textBox6.Text != "")
            {


                if (kyet.Checked && uyet.Checked && uyet.Checked)
                {
                    sql="UPDATE Kullanici set Adi='"+textBox1.Text+"',Soyadi='"+textBox2.Text+"',Email='"+textBox3.Text+"',KullaniciAdi='"+textBox4.Text+"',Sifre='"+textBox5.Text+"',Resim='"+yeniad+"',Kyet=1,Uyet=1,Myet=1 where KullaniciId="+kid;
                    
                }
                else if (kyet.Checked && uyet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Resim='" + yeniad + "',Kyet=1,Uyet=1,Myet=0 where KullaniciId=" + kid;
                   
                }
                else if (kyet.Checked && myet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Resim='" + yeniad + "',Kyet=1,Uyet=0,Myet=1 where KullaniciId=" + kid;
                    
                }
                else if (uyet.Checked && myet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Resim='" + yeniad + "',Kyet=0,Uyet=1,Myet=1 where KullaniciId=" + kid;
                   
                }
                else if (kyet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Resim='" + yeniad + "',Kyet=1,Uyet=0,Myet=0 where KullaniciId=" + kid;
                    
                }
                else if (uyet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Resim='" + yeniad + "',Kyet=0,Uyet=1,Myet=0 where KullaniciId=" + kid;
                   
                }
                else if (myet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Resim='" + yeniad + "',Kyet=0,Uyet=0,Myet=1 where KullaniciId=" + kid;
                    
                }
                else
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Resim='" + yeniad + "',Kyet=0,Uyet=0,Myet=0 where KullaniciId=" + kid;
                }


                File.Copy(kaynak, hedef + yeniad);

                DataBase.ExecSql(sql);

            }

            else
            {

                if (kyet.Checked && uyet.Checked && uyet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Kyet=1,Uyet=1,Myet=1 where KullaniciId=" + kid;

                }
                else if (kyet.Checked && uyet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Kyet=1,Uyet=1,Myet=0 where KullaniciId=" + kid;

                }
                else if (kyet.Checked && myet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Kyet=1,Uyet=0,Myet=1 where KullaniciId=" + kid;

                }
                else if (uyet.Checked && myet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text +  "',Kyet=0,Uyet=1,Myet=1 where KullaniciId=" + kid;

                }
                else if (kyet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text +  "',Kyet=1,Uyet=0,Myet=0 where KullaniciId=" + kid;

                }
                else if (uyet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Kyet=0,Uyet=1,Myet=0 where KullaniciId=" + kid;

                }
                else if (myet.Checked)
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text +  "',Kyet=0,Uyet=0,Myet=1 where KullaniciId=" + kid;

                }
                else
                {
                    sql = "UPDATE Kullanici set Adi='" + textBox1.Text + "',Soyadi='" + textBox2.Text + "',Email='" + textBox3.Text + "',KullaniciAdi='" + textBox4.Text + "',Sifre='" + textBox5.Text + "',Kyet=0,Uyet=0,Myet=0 where KullaniciId=" + kid;

                }




                DataBase.ExecSql(sql);





            }

            MessageBox.Show("Güncellendi..");
            KullaniciTanimlari kullaniciTanimlari = new KullaniciTanimlari();
            kullaniciTanimlari.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            KullaniciTanimlari kullaniciTanimlari = new KullaniciTanimlari();
            kullaniciTanimlari.Show();
            this.Hide();
        }
    }
}
