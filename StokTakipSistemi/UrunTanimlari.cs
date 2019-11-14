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

    public partial class UrunTanimlari : Form
    {
        DataTable table;
       
        public string kullaniciid;
        public UrunTanimlari()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UrunEkle uekle = new UrunEkle();
            uekle.Show();
            this.Hide();
        }
        DataTable tbl;
        private void UrunTanimlari_Load(object sender, EventArgs e)
        {
            kullaniciid = Anasayfa.kullaniciid;
            string yetki = "Select Uyet from Kullanici where KullaniciId=" + kullaniciid;
            tbl = DataBase.select(yetki);





            if (tbl.Rows[0]["Uyet"].ToString() =="0" || tbl.Rows[0]["Uyet"].ToString() == "")
            {

                button1.Enabled = false;

                string sql = "Select * from Urunler";
                SqlConnection bag = new SqlConnection(DataBase.ConStr);
                bag.Open();
                table = DataBase.select(sql);
                dataGridView1.DataSource = table;


                bag.Close();
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                button1.Enabled = true;



                string sql = "Select * from Urunler";
            SqlConnection bag = new SqlConnection(DataBase.ConStr);
            bag.Open();
            table = DataBase.select(sql);
            dataGridView1.DataSource = table;


            bag.Close();
            dataGridView1.Columns[0].Visible = false;
            }
        }


        string yeniad;
        string yeniad2;

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

            int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            pictureBox1.ImageLocation = "";
            pictureBox2.ImageLocation = "";

            if (row_index == null
                       || row_index < 0
                       || row_index >= table.Rows.Count) return;
            string resim1 = "Select * from UrunResim where UrunId=" + dataGridView1.Rows[row_index].Cells["UrunRef"].Value.ToString();
            if (DataBase.select(resim1).Rows.Count>0)
            {

                try
                {
                    DataTable table = DataBase.select(resim1);
                    if (table.Rows[0]["ResimUrl"].ToString() != "")
                    {
                        pictureBox1.ImageLocation = Application.StartupPath + @"\resimler\" + table.Rows[0]["ResimUrl"].ToString();
                    }
                    if (DataBase.select(resim1).Rows.Count==2)
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


            kullaniciid = Anasayfa.kullaniciid;
            string yetki = "Select Uyet from Kullanici where KullaniciId=" + kullaniciid;
            tbl = DataBase.select(yetki);





            if (tbl.Rows[0]["Uyet"].ToString() == "1")
            {
               
                int[] selectedIndexes = new int[dataGridView1.SelectedRows.Count];
                if (table.Rows.Count < 1) return;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu menu = new ContextMenu();
                    MenuItem item = new MenuItem("Güncelle");
                    

                    if (row_index == null
                        || row_index < 0
                        || row_index >= table.Rows.Count) return;

                    item.Click += new EventHandler(delegate (object s, EventArgs args)
                    {
                        string urun_id = dataGridView1.Rows[row_index].Cells["UrunRef"].Value.ToString();

                        MessageBox.Show(urun_id);
                        UrunEkle urunGuncelleme = new UrunEkle();
                        urunGuncelleme.urun_id = urun_id;
                        urunGuncelleme.dt_index = row_index;
                        urunGuncelleme.Show();
                        this.Hide();

                    });

                    menu.MenuItems.Add(item);



                    item = new MenuItem("Sil");



                    item.Click += new EventHandler(delegate (object s, EventArgs args)
                    {


                        if (row_index == null
                            || row_index < 0
                            || row_index >= table.Rows.Count) return;
                        string urun_id = dataGridView1.Rows[row_index].Cells["UrunRef"].Value.ToString();
                        if (MessageBox.Show("Silmek İstediğinize Eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {


                            string resimsorgu = "Select * from UrunResim Where UrunId='" + urun_id + "'";
                            DataTable tableresim = DataBase.select(resimsorgu);



                            
                            if (DataBase.select(resimsorgu).Rows.Count > 0)
                            {
                                string hedef = Application.StartupPath + @"\resimler\";
                                string path = hedef;
                                DirectoryInfo di = new DirectoryInfo(path);

                                if (tableresim.Rows[0]["ResimUrl"].ToString() != "")
                                {
                                     yeniad = tableresim.Rows[0]["ResimUrl"].ToString().Trim().Replace(" ", string.Empty);
                                }
                                if (DataBase.select(resim1).Rows.Count == 2)
                                {
                                    if (tableresim.Rows[1]["ResimUrl"].ToString() != "")
                                    {
                                         yeniad2 = tableresim.Rows[1]["ResimUrl"].ToString().Trim().Replace(" ", string.Empty) ;
                                    }

                                }
                                

                                FileInfo[] rgFiles = di.GetFiles();
                                foreach (FileInfo fi in rgFiles)
                                {
                                    //fi.Name bize dosyanın adını dönüyor.
                                    //fi.FullName ise bize dosyasının dizin bilgisini döner.


                                    if (fi.Name == yeniad)
                                    {
                                        File.Delete(hedef + yeniad);
                                    }

                                    if (fi.Name==yeniad2)
                                    {
                                        File.Delete(hedef + yeniad2);
                                    }

                                }

                            }

                            string sil = "Delete from UrunResim where UrunId=" + urun_id;

                            DataBase.ExecSql(sil);

                            string resimsil = "Delete from UrunResim where UrunId=" + urun_id;

                            DataBase.ExecSql(resimsil);

                            string UrunSil = "Delete from Urunler where UrunRef=" + urun_id;

                            DataBase.ExecSql(UrunSil);


                            MessageBox.Show("Silindi");


                            string sql = "Select * from Urunler";
                            SqlConnection bag = new SqlConnection(DataBase.ConStr);
                            bag.Open();
                            table = DataBase.select(sql);
                            dataGridView1.DataSource = table;


                            bag.Close();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox2.Text.Trim() != "")
            {
                ara = "SELECT* FROM Urunler WHERE UrunKodu LIKE '%" + textBox1.Text.Trim() + "%' and UrunAdi LIKE '%" + textBox2.Text.Trim() + "%'";
            }
            else
            {
                ara = "SELECT* FROM Urunler WHERE UrunKodu LIKE '%" + textBox1.Text.Trim() + "%'";
            }
           
            DataTable aratbl = DataBase.select(ara);
            dataGridView1.DataSource = aratbl;

            if (textBox1.Text.Trim() == "")
            {
                string sql = "Select * from Urunler";
                SqlConnection bag = new SqlConnection(DataBase.ConStr);
                bag.Open();
                table = DataBase.select(sql);
                dataGridView1.DataSource = table;


                bag.Close();
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa anasyf = new Anasayfa();
            anasyf.Show();
            this.Hide();
        }


        string ara = "";
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() !="")
            {
                 ara = "SELECT* FROM Urunler WHERE UrunKodu LIKE '%" + textBox1.Text.Trim() + "%' and UrunAdi LIKE '%" + textBox2.Text.Trim() + "%'";
            }
            else
            {
                ara = "SELECT* FROM Urunler WHERE UrunAdi LIKE '%" + textBox2.Text.Trim() + "%'";
            }
            DataTable aratbl = DataBase.select(ara);
            dataGridView1.DataSource = aratbl;

            if (textBox1.Text.Trim() == "")
            {
                string sql = "Select * from Urunler";
                SqlConnection bag = new SqlConnection(DataBase.ConStr);
                bag.Open();
                table = DataBase.select(sql);
                dataGridView1.DataSource = table;


                bag.Close();
                dataGridView1.Columns[0].Visible = false;
            }
        }
    }
    }

