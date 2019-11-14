using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipSistemi
{
    
    public partial class frmMusteriTanimlari : Form
    {
        public string kullaniciid;
        DataTable table;
        public frmMusteriTanimlari()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa anasyf = new Anasayfa();
            anasyf.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmMusteriEkle musteriekle = new frmMusteriEkle();
            musteriekle.Show();
            this.Hide();
        }

        private void frmMusteriTanimlari_Load(object sender, EventArgs e)
        {
            kullaniciid = Anasayfa.kullaniciid;
            string yetki = "Select Myet from Kullanici where KullaniciId=" + kullaniciid;
            table = DataBase.select(yetki);





            if (table.Rows[0]["Myet"].ToString() == "0" || table.Rows[0]["Myet"].ToString() == "")
            {

                button1.Enabled = false;

                string sql = @"SELECT 
	               m.[MusteriRef]
                  ,m.[MusteriKodu]
                  ,m.[MusteriAdi]
                  ,CASE m.[MusteriTipi] WHEN '1' THEN 'Alıcı' WHEN '2' THEN 'Satıcı' END  [MusteriTipi]
                  ,m.[Telefon]
                  ,m.[Telefon2]
                  ,m.[Fax]
                  ,m.[Mail]
                  ,k.Adi
              FROM [StokTakipSistemi].[dbo].[Musteriler] m , Kullanici k where m.Ekleyen=k.KullaniciId ";
                
                table = DataBase.select(sql);
                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Visible = false;

            }
            else
            {
                button1.Enabled = true;



                        string sql = @"SELECT 
	           m.[MusteriRef]
              ,m.[MusteriKodu]
              ,m.[MusteriAdi]
              ,CASE m.[MusteriTipi] WHEN '1' THEN 'Alıcı' WHEN '2' THEN 'Satıcı' END  [MusteriTipi]
              ,m.[Telefon]
              ,m.[Telefon2]
              ,m.[Fax]
              ,m.[Mail]
              ,k.Adi
          FROM [StokTakipSistemi].[dbo].[Musteriler] m , Kullanici k where m.Ekleyen=k.KullaniciId ";
               
                table = DataBase.select(sql);
                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Visible = false;




            }
























            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {






        }

        public string kid;
        DataTable tble;
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //Sağ Tık

             kid= Anasayfa.kullaniciid;
            string yetki = "Select Myet from Kullanici where KullaniciId=" + kid;
            tble = DataBase.select(yetki);





            if (tble.Rows[0]["Myet"].ToString() == "1")
            {

                int[] selectedIndexes = new int[dataGridView1.SelectedRows.Count];
                if (table.Rows.Count < 1) return;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu menu = new ContextMenu();
                    MenuItem item = new MenuItem("Güncelle");
                    int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                    //int column = dataGridView1.HitTest(e.X, e.Y).ColumnIndex;
                    if (row_index == null
                        || row_index < 0
                        || row_index >= table.Rows.Count) return;

                    item.Click += new EventHandler(delegate (object s, EventArgs args)
                    {
                      

                         string musteriid = dataGridView1.Rows[row_index].Cells["MusteriRef"].Value.ToString();


                        
                        frmMusteriGuncelle mstrguncelle = new frmMusteriGuncelle();
                        mstrguncelle.mstref = musteriid;
                        mstrguncelle.dt_index = row_index;
                        mstrguncelle.Show();
                        this.Hide();

                    });

                    menu.MenuItems.Add(item);



                    item = new MenuItem("Sil");



                    item.Click += new EventHandler(delegate (object s, EventArgs args)
                    {


                        if (row_index == null
                            || row_index < 0
                            || row_index >= table.Rows.Count) return;
                        string musteriid = dataGridView1.Rows[row_index].Cells["MusteriRef"].Value.ToString();
                        if (MessageBox.Show("Silmek İstediğinize Eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {



                            string sil = "Delete from Musteriler where MusteriRef=" + musteriid;

                            DataBase.ExecSql(sil);
                            MessageBox.Show("Silindi");


                            string sql = @"SELECT 
	                                   m.[MusteriRef]
                                      ,m.[MusteriKodu]
                                      ,m.[MusteriAdi]
                                      ,CASE m.[MusteriTipi] WHEN '1' THEN 'Alıcı' WHEN '2' THEN 'Satıcı' END  [MusteriTipi]
                                      ,m.[Telefon]
                                      ,m.[Telefon2]
                                      ,m.[Fax]
                                      ,m.[Mail]
                                      ,k.Adi
                                  FROM [StokTakipSistemi].[dbo].[Musteriler] m , Kullanici k where m.Ekleyen=k.KullaniciId ";
                           
                            table = DataBase.select(sql);
                            dataGridView1.DataSource = table;
                            dataGridView1.Columns[0].Visible = false;



                        }


                        else
                        {
                            return;
                        }


                    });

                    menu.MenuItems.Add(item);

                    menu.Show(dataGridView1, new Point(e.X, e.Y));
                }

            }
            else
            {
                return;
            }







        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string ara = "SELECT* FROM Musteriler WHERE MusteriKodu LIKE '%" + textBox1.Text.Trim() + "%'";
            DataTable aratbl = DataBase.select(ara);
            dataGridView1.DataSource = aratbl;
            if (textBox1.Text.Trim() == "")
            {
                string sql = @"SELECT 
	               m.[MusteriRef]
                  ,m.[MusteriKodu]
                  ,m.[MusteriAdi]
                  ,CASE m.[MusteriTipi] WHEN '1' THEN 'Alıcı' WHEN '2' THEN 'Satıcı' END  [MusteriTipi]
                  ,m.[Telefon]
                  ,m.[Telefon2]
                  ,m.[Fax]
                  ,m.[Mail]
                  ,k.Adi
              FROM [StokTakipSistemi].[dbo].[Musteriler] m , Kullanici k where m.Ekleyen=k.KullaniciId ";

                table = DataBase.select(sql);
                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Visible = false;
            }
    }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
