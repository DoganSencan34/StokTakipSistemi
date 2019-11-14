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
   
    public partial class UrunEkle : Form
    {
        public int dt_index;
        public string urun_id="";


        public UrunEkle()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(UrunAditxt.Text=="")
            {
                MessageBox.Show("Ürün Adı  Boş Bırakmayın.");
            }
            else if (UrunBirimCb.SelectedIndex==0)
            {
                MessageBox.Show("Ürün Birimi  Boş Bırakmayın.");
            }
            else if ( BirimFiyattxt.Text == "" )
            {
                MessageBox.Show("Birim Fiyatı Boş Bırakmayın.");
            }
            else if ( MinimumAdetTxt.Text == "")
            {
                MessageBox.Show("Minumum Adet Boş Bırakmayın.");
            }
            else
            {
                string kontrol = "Select * from Urunler where UrunAdi='" + UrunAditxt.Text + "'";
                if (DataBase.select(kontrol).Rows.Count > 0)
                {
                    MessageBox.Show("Ürün Adı Mevcut.");
                    return;
                }
                string sql = "Insert Into Urunler(UrunAdi,UrunKodu,MinimumStok,UrunBirimi,BirimFiyat) VALUES('" + UrunAditxt.Text + "','" + UrunKodutxt.Text + "','" + MinimumAdetTxt.Text + "','" + UrunBirimCb.SelectedItem.ToString() + "','" + BirimFiyattxt.Text + "')SELECT SCOPE_IDENTITY();";
                string id = DataBase.execScalar(sql);
                if (ResimTxt.Text !="")
                {
                    dosyaAdi = Path.GetFileName(dosyaYolu); //Dosya adını alma
                    string kaynak = dosyaYolu;
                    string hedef = Application.StartupPath + @"\resimler\";
                    string yeniad = UrunAditxt.Text.Trim().Replace(" ",string.Empty) + ".jpg"; //Benzersiz isim verme

                    string resimekle= "Insert Into UrunResim(UrunId,ResimUrl) VALUES('"+id+"','"+yeniad+"')";
                    DataBase.ExecSql(resimekle);
                    File.Copy(kaynak, hedef + yeniad);
                }
                if (Resim2Txt.Text !="")
                {
                    dosyaAdi2 = Path.GetFileName(dosyaYolu2); //Dosya adını alma
                    string kaynak = dosyaYolu2;
                    string hedef = Application.StartupPath + @"\resimler\";
                    string yeniad = "2"+UrunAditxt.Text.Trim().Replace(" ", string.Empty) + ".jpg";
                    string resimekle = "Insert Into UrunResim(UrunId,ResimUrl) VALUES('" + id + "','" + yeniad + "')";
                    DataBase.ExecSql(resimekle);
                    File.Copy(kaynak, hedef + yeniad);
                }
                MessageBox.Show("Kaydedildi.");
                UrunTanimlari utanim = new UrunTanimlari();
                utanim.Show();
                this.Hide();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            UrunTanimlari utanim = new UrunTanimlari();
            utanim.Show();
            this.Hide();
        }

        DataTable sorgutable;
        DataTable table;
        private void UrunEkle_Load(object sender, EventArgs e)
        {
            if (urun_id !="")
            {
                button5.Visible = true;
                button3.Visible = false;
                string sorgu = "Select * from Urunler where UrunRef=" + urun_id;
                sorgutable=DataBase.select(sorgu);

                string resim1 = "Select * from UrunResim where UrunId=" + urun_id;
                if (DataBase.select(resim1).Rows.Count > 0)
                {
                    try
                    {
                        DataTable table = DataBase.select(resim1);
                        if (table.Rows[0]["ResimUrl"].ToString() != "")
                        {
                            pictureBox1.ImageLocation = Application.StartupPath + @"\resimler\" + table.Rows[0]["ResimUrl"].ToString();
                        }
                        if (DataBase.select(resim1).Rows.Count == 2)
                        {
                            if (table.Rows[1]["ResimUrl"].ToString() != "")
                            {
                                pictureBox2.ImageLocation = Application.StartupPath + @"\resimler\" + table.Rows[1]["ResimUrl"].ToString();
                              
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                UrunKodutxt.Text = sorgutable.Rows[0]["UrunKodu"].ToString();
                UrunAditxt.Text = sorgutable.Rows[0]["UrunAdi"].ToString();
                MinimumAdetTxt.Text = sorgutable.Rows[0]["MinimumStok"].ToString();
                UrunBirimCb.SelectedItem = sorgutable.Rows[0]["UrunBirimi"].ToString();
                BirimFiyattxt.Text = sorgutable.Rows[0]["BirimFiyat"].ToString();
            }
        }
        string sql = "";
        private void button5_Click(object sender, EventArgs e)
        {
            if (UrunAditxt.Text == "")
            {
                MessageBox.Show("Ürün Adı  Boş Bırakmayın.");
            }
            else if (UrunBirimCb.SelectedIndex == 0)
            {
                MessageBox.Show("Ürün Birimi  Boş Bırakmayın.");
            }
            else if (BirimFiyattxt.Text == "")
            {
                MessageBox.Show("Birim Fiyatı Boş Bırakmayın.");
            }
            else if (MinimumAdetTxt.Text == "")
            {
                MessageBox.Show("Minumum Adet Boş Bırakmayın.");
            }
            else
            {
                try
                {
                    string kontrol = "Select * from Urunler where UrunAdi='" + UrunAditxt.Text + "'";
                    string komut= @"UPDATE Urunler SET UrunAdi='" + UrunAditxt.Text + 
                        "',UrunKodu='" + UrunKodutxt.Text + 
                        "',MinimumStok='" + MinimumAdetTxt.Text + 
                        "',UrunBirimi='" + UrunBirimCb.SelectedItem.ToString() + 
                        "',BirimFiyat='" + BirimFiyattxt.Text + 
                        "' WHERE UrunRef=" + urun_id;

                     DataBase.ExecSql(komut);
                    if (ResimTxt.Text != "")
                    {
                                dosyaAdi = Path.GetFileName(dosyaYolu); //Dosya adını alma
                                string kaynak = dosyaYolu;
                                string hedef = Application.StartupPath + @"\resimler\";
                                string yeniad = UrunAditxt.Text.Trim().Replace(" ", string.Empty) + ".jpg"; //Benzersiz isim verme

                                string resimsorgu = "Select * from UrunResim Where ResimUrl='" + yeniad + "'";
                                //Resim Varsa yapılacaklar
                        
                                if (DataBase.select(resimsorgu).Rows.Count > 0)
                                {

                                    DataTable tblresimupdate = DataBase.select(resimsorgu);

                                        string path = hedef;
                                        DirectoryInfo di = new DirectoryInfo(path);
                                        FileInfo[] rgFiles = di.GetFiles();
                                        foreach (FileInfo fi in rgFiles)
                                        {
                                            //fi.Name bize dosyanın adını dönüyor.
                                            //fi.FullName ise bize dosyasının dizin bilgisini döner.


                                            if (fi.Name == yeniad)
                                            {
                                                File.Delete(hedef + yeniad);
                                            }
                                        }
                                    string resimguncelle = "Update  UrunResim set UrunId='" + urun_id + "',ResimUrl='" + yeniad + "' where ResimId=" + tblresimupdate.Rows[0]["ResimId"].ToString();
                                    DataBase.ExecSql(resimguncelle);
                                    File.Copy(kaynak, hedef + yeniad);
                                }

                                //Resim Yoksa Insert edilecek
                                else
                                {
                                    string resimeklee = "Insert Into UrunResim(UrunId,ResimUrl) VALUES('" + urun_id + "','" + yeniad + "')";
                                    DataBase.ExecSql(resimeklee);
                                    File.Copy(kaynak, hedef + yeniad);
                                }
                    }

                    if (Resim2Txt.Text != "")
                    {
                            dosyaAdi2 = Path.GetFileName(dosyaYolu2); //Dosya adını alma
                            string kaynak = dosyaYolu2;
                            string hedef = Application.StartupPath + @"\resimler\";
                            string yeniad = "2" + UrunAditxt.Text.Trim().Replace(" ", string.Empty) + ".jpg"; //Benzersiz isim verme
                            string resimsorgu = "Select * from UrunResim Where ResimUrl='" + yeniad+"'";
                            DataTable tbl = DataBase.select(resimsorgu);                 
                            if (DataBase.select(resimsorgu).Rows.Count>0)
                            {
                                    string path = hedef;
                                    DirectoryInfo di = new DirectoryInfo(path);
                                    FileInfo[] rgFiles = di.GetFiles();
                                    foreach (FileInfo fi in rgFiles)
                                    {
                                        if (fi.Name==yeniad)
                                        {
                                            File.Delete(hedef+yeniad);
                                        }
                                    }
                                string resimekle = "Update  UrunResim set UrunId='" + urun_id + "',ResimUrl='" + yeniad + "' where ResimId=" + tbl.Rows[1]["ResimId"].ToString();
                                DataBase.ExecSql(resimekle);
                                File.Copy(kaynak, hedef + yeniad);
                            }
                            else
                            {
                                string resimeklee = "Insert Into UrunResim(UrunId,ResimUrl) VALUES('" + urun_id + "','" + yeniad + "')";
                                DataBase.ExecSql(resimeklee);
                                File.Copy(kaynak, hedef + yeniad);
                            }
                        
                    }
                MessageBox.Show("Güncellendi.");
                UrunTanimlari utanim = new UrunTanimlari();
                utanim.Show();
                this.Hide();

                }
                catch (Exception hata)
                {

                    MessageBox.Show(hata.Message);
                }
            }

        }

        string dosyaYolu;
        string dosyaAdi;
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png ;*.jpeg;*.gif|  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu = dosya.FileName;
            ResimTxt.Text = dosyaYolu;
            pictureBox1.ImageLocation = dosyaYolu;
        }
        string dosyaYolu2;
        string dosyaAdi2;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png ;*.jpeg;*.gif|  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu2 = dosya.FileName;
            Resim2Txt.Text = dosyaYolu2;
            pictureBox2.ImageLocation = dosyaYolu2;
        }

        private void ResimTxt_TextChanged(object sender, EventArgs e)
        {
            if (ResimTxt.Text != "")
            {
                button1.Enabled = true;
            }
        }
    }
}
