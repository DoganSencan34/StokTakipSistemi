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
    public partial class frmGirisFisi : Form
    {
        public frmGirisFisi()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmGirisFisiEkle grsfisi = new frmGirisFisiEkle();
            grsfisi.Show();
            this.Hide();
        }

        DataTable girisfistable;
        private void frmGirisFisi_Load(object sender, EventArgs e)
        {
            string sorgu = @"select fb.FisId,fb.FisNo,fb.Tarih,urn.UrunAdi,fh.Miktar,fh.Birim,fh.FisHaraketId 
                                from FisBaslik fb, FisHaraket fh ,Urunler urn 
                                          where 
		                                  fb.FisId = fh.FisId 
		                                  and 
		                                  fh.UrunRef = urn.UrunRef 
		                                  and fh.FisTipi = 1";
            girisfistable = DataBase.select(sorgu);
            dataGridView1.DataSource = girisfistable;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Hide();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;

            int[] selectedIndexes = new int[dataGridView1.SelectedRows.Count];
            if (girisfistable.Rows.Count < 1) return;

            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem item = new MenuItem("Güncelle");


                if (row_index == null
                    || row_index < 0
                    || row_index >= girisfistable.Rows.Count) return;

                item.Click += new EventHandler(delegate (object s, EventArgs args)
                {
                    string fisid = dataGridView1.Rows[row_index].Cells["FisId"].Value.ToString();


                    frmGirisFisiEkle girsekle = new frmGirisFisiEkle();
                    girsekle.fisid = fisid;

                    girsekle.Show();
                    this.Hide();

                });

                menu.MenuItems.Add(item);

                item = new MenuItem("Sil");



                item.Click += new EventHandler(delegate (object s, EventArgs args)
                {


                    if (row_index == null
                        || row_index < 0
                        || row_index >= girisfistable.Rows.Count) return;
                    string fisidsil = dataGridView1.Rows[row_index].Cells["FisId"].Value.ToString();
                    string fisharaketid = dataGridView1.Rows[row_index].Cells["FisHaraketId"].Value.ToString();
                    if (MessageBox.Show("Silmek İstediğinize Eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        string adet = "select * from FisHaraket where FisTipi=1 and FisId=" + fisidsil;
                        DataTable kontroltable = DataBase.select(adet);
                        if (kontroltable.Rows.Count> 1)
                        {
                           
                                string fisharaketsil = "Delete from FisHaraket where FisHaraketId=" + fisharaketid;
                                DataBase.ExecSql(fisharaketsil);

                               string stoksil = "Delete from Stok where FisHaraketId=" + fisharaketid;
                               DataBase.ExecSql(stoksil);
                            
                            MessageBox.Show("Silindi");

                            string sorgu = @"select fb.FisId,fb.FisNo,fb.Tarih,urn.UrunAdi,fh.Miktar,fh.Birim,fh.FisHaraketId from FisBaslik fb, FisHaraket fh ,Urunler urn 
                            where fb.FisId = fh.FisId and fh.UrunRef = urn.UrunRef and fh.FisTipi = 1";
                            girisfistable = DataBase.select(sorgu);
                            dataGridView1.DataSource = girisfistable;
                            dataGridView1.Columns[0].Visible = false;
                            dataGridView1.Columns[6].Visible = false;

                        }
                        else
                        {
                            string fisbasliksil = "Delete from FisBaslik where FisId=" + fisidsil;

                            string fisharaketsil = "Delete from FisHaraket where FisHaraketId=" + fisharaketid;

                            string stoksil = "Delete from Stok where FisHaraketId=" + fisharaketid;
                            DataBase.ExecSql(fisbasliksil);
                            DataBase.ExecSql(fisharaketsil);
                            DataBase.ExecSql(stoksil);

                            MessageBox.Show("Silindi");

                            string sorgu = @"select fb.FisId,fb.FisNo,fb.Tarih,urn.UrunAdi,fh.Miktar,fh.Birim,fh.FisHaraketId from FisBaslik fb, FisHaraket fh ,Urunler urn 
                            where fb.FisId = fh.FisId and fh.UrunRef = urn.UrunRef and fh.FisTipi = 1";
                            girisfistable = DataBase.select(sorgu);
                            dataGridView1.DataSource = girisfistable;
                            dataGridView1.Columns[0].Visible = false;
                            dataGridView1.Columns[6].Visible = false;
                        }

                    }
                });

                menu.MenuItems.Add(item);


                menu.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }
        string ara = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
            {
                ara = @"select fb.FisId,fb.FisNo,fb.Tarih,urn.UrunAdi,fh.Miktar,fh.Birim,fh.FisHaraketId 
                                from FisBaslik fb, FisHaraket fh ,Urunler urn 
                                          where 
		                                  fb.FisId = fh.FisId 
		                                  and 
		                                  fh.UrunRef = urn.UrunRef 
		                                  and fh.FisTipi = 1 and Tarih BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' and fb.FisNo='"+textBox1.Text.Trim()+"'";
            }
            else
            {
                ara = @"select fb.FisId,fb.FisNo,fb.Tarih,urn.UrunAdi,fh.Miktar,fh.Birim,fh.FisHaraketId 
                                from FisBaslik fb, FisHaraket fh ,Urunler urn 
                                          where 
		                                  fb.FisId = fh.FisId 
		                                  and 
		                                  fh.UrunRef = urn.UrunRef 
		                                  and fh.FisTipi = 1 and Tarih BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
            }
       
            DataTable aratbl = DataBase.select(ara);
            dataGridView1.DataSource = aratbl;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;



        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = @"select fb.FisId,fb.FisNo,fb.Tarih,urn.UrunAdi,fh.Miktar,fh.Birim,fh.FisHaraketId 
                                from FisBaslik fb, FisHaraket fh ,Urunler urn 
                                          where 
		                                  fb.FisId = fh.FisId 
		                                  and 
		                                  fh.UrunRef = urn.UrunRef 
		                                  and fh.FisTipi = 1";
            girisfistable = DataBase.select(sorgu);
            dataGridView1.DataSource = girisfistable;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }
    }
}

