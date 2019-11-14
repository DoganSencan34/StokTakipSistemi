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
    public partial class KullaniciTanimlari : Form
    {
        public KullaniciTanimlari()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KullaniciEkle KlncEkle = new KullaniciEkle();
            KlncEkle.Show();
        }
        private DataTable table;
        private void KullaniciTanimlari_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "Select * from Kullanici";

                table = DataBase.select(sql);
                dataGridView1.DataSource = table;

                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa ans = new Anasayfa();
            ans.Show();
            this.Hide();
        }

        string yeniad="";
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            
                int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                pictureBox1.ImageLocation = "";


                if (row_index == null
                           || row_index < 0
                           || row_index >= table.Rows.Count) return;
                string resim1 = "Select * from Kullanici where KullaniciId=" + dataGridView1.Rows[row_index].Cells["KullaniciId"].Value.ToString();
                if (DataBase.select(resim1).Rows.Count > 0)
                {

                    try
                    {
                        DataTable table = DataBase.select(resim1);
                        if (table.Rows[0]["Resim"].ToString() != "")
                        {
                            pictureBox1.ImageLocation = Application.StartupPath + @"\resimler\" + table.Rows[0]["Resim"].ToString();
                        }
                        else
                        {
                            pictureBox1.ImageLocation = "";
                        }

                    }
                    catch (Exception)
                    {


                    }


                }
            //----
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
                    string kullaniciid = dataGridView1.Rows[row_index].Cells["KullaniciId"].Value.ToString();


                    KullaniciEkle kekle = new KullaniciEkle();
                    kekle.kid =kullaniciid;
                 
                    kekle.Show();
                    this.Hide();

                });

                menu.MenuItems.Add(item);
                item = new MenuItem("Sil");
                item.Click += new EventHandler(delegate (object s, EventArgs args)
                {
                    if (row_index == null
                        || row_index < 0
                        || row_index >= table.Rows.Count) return;
                    string kullaniciid = dataGridView1.Rows[row_index].Cells["KullaniciId"].Value.ToString();
                    if (MessageBox.Show("Silmek İstediğinize Eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string hedef = Application.StartupPath + @"\resimler\";
                        string path = hedef;
                        DirectoryInfo di = new DirectoryInfo(path);



                        string resimadi = "Select Resim from Kullanici where KullaniciId=" + kullaniciid;
                        DataTable tbl = DataBase.select(resimadi);

                        if (tbl.Rows[0]["Resim"].ToString() != "")
                        {
                            yeniad = tbl.Rows[0]["Resim"].ToString().Trim().Replace(" ", string.Empty);
                        }


                        string sil = "Delete from Kullanici where KullaniciId=" + kullaniciid;

                        DataBase.ExecSql(sil);

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
                        MessageBox.Show("Silindi");


                        string sql = "Select * from Kullanici";

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

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
