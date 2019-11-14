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
    public partial class frmCikisFisiEkle : Form
    {
        public frmCikisFisiEkle()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUrunListesi urunListesi = new frmUrunListesi();
            if (urunListesi.ShowDialog() == DialogResult.OK)
            {

                int i = e.RowIndex;
                if (e.RowIndex == dataGridView1.NewRowIndex)
                {
                    i = dataGridView1.Rows.Add();
                }


                dataGridView1.Rows[i].Cells["UrunAdi"].Value = urunListesi.adi;
                dataGridView1.Rows[i].Cells["UrunKodu"].Value = urunListesi.kod;
                //dataGridView1.Rows[i].Cells["UrunAdet"].Value = urunListesi.adet;
                dataGridView1.Rows[i].Cells["UrunId"].Value = urunListesi.id;
                dataGridView1.Rows[i].Cells["UrunBirimi"].Value = urunListesi.birim;
               
            }
            urunListesi.cikisfisi = true;
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmMusteriListesi frmMusteriListesi = new frmMusteriListesi();
            frmMusteriListesi.cikisfisi = true;
            frmMusteriListesi.Show();
        }
        DataTable table;
        string haraketid;
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[0].Cells[1].Value == null)
            {
                MessageBox.Show("Giriş Fişi Ekleyebilmeniz İçin Ürün Eklemeniz Gerekmektedir.");
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["UrunAdi"].Value != null)
                {


                    if (dataGridView1.Rows[i].Cells["UrunAdet"].Value == null)
                    {
                        MessageBox.Show("Adet Girmeniz Gerekmektedir.");
                        return;
                    }

                }
            }
            string sql = "Insert Into FisBaslik(FisNo,Tarih,MusteriId,KullaniciRef,Aciklama,FisTipi) Values('" + FisNoTxt.Text + "','" 
                + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + MusteriIdLbl.Text + "','" + Form1.kullaniciid + "','" + AciklamaTxt.Text + "','2') SELECT SCOPE_IDENTITY();";
            string id = DataBase.execScalar(sql);

            //Fiş Haraket
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["UrunAdi"].Value != null)
                {
                    string komut = "Insert Into FisHaraket(FisId,UrunRef,Birim,Miktar,FisTipi) Values('" + id + "','" + dataGridView1.Rows[i].Cells["UrunId"].Value 
                        + "','" + dataGridView1.Rows[i].Cells["UrunBirimi"].Value + "','" + dataGridView1.Rows[i].Cells["UrunAdet"].Value + "','2')SELECT SCOPE_IDENTITY();";

                    haraketid = DataBase.execScalar(komut);

                    string cikar = "-" + Convert.ToInt32(dataGridView1.Rows[i].Cells["UrunAdet"].Value.ToString());

                    string stokekle = "INSERT INTO Stok(UrunRef,Birim,Miktar,Tarih,FisHaraketId) values('" 
                        + dataGridView1.Rows[i].Cells["UrunId"].Value + "','" + dataGridView1.Rows[i].Cells["UrunBirimi"].Value + "','" + cikar + "','" 
                        + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + haraketid + "')";

                    DataBase.ExecSql(stokekle);
                }

            }
            MessageBox.Show("Eklendi!");

            frmCikisFisi cikisfisi = new frmCikisFisi();
            cikisfisi.Show();
            this.Hide();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (fisid == "")
            {
                int[] selectedIndexes = new int[dataGridView1.SelectedRows.Count];
                if (dataGridView1.Rows.Count < 1) return;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu menu = new ContextMenu();
                    MenuItem item = new MenuItem("Sil");
                    int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                    if (row_index == null
                        || row_index < 0
                        || row_index >= dataGridView1.Rows.Count) return;

                    item.Click += new EventHandler(delegate (object s, EventArgs args)
                    {
                        if (MessageBox.Show("Silmek İstediğinize Eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            try
                            {
                                dataGridView1.Rows.RemoveAt(row_index);
                                dataGridView1.Refresh();
                            }
                            catch (Exception)
                            {

                                MessageBox.Show("Silinecek Satır Yok");
                            }
                        }

                    });


                    menu.MenuItems.Add(item);
                    menu.Show(dataGridView1, new Point(e.X, e.Y));
                }

            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        DataTable tblguncelefis;
        public string fisid = "";
        private void frmCikisFisiEkle_Load(object sender, EventArgs e)
        {
            if (fisid != "")
            {
                button1.Visible = false;
                btn_guncelle.Visible = true;

                string sorgu = @"
                    select* from FisBaslik fb, FisHaraket fh ,Urunler urn,Musteriler mst
                    where fb.FisId = fh.FisId and fh.UrunRef = urn.UrunRef and mst.MusteriRef=fb.MusteriId  and fh.FisTipi = 2 and fb.FisId =" + fisid;

                tblguncelefis = DataBase.select(sorgu);

                for (int i = 0; i < tblguncelefis.Rows.Count; i++)
                {
                    FisNoTxt.Text = tblguncelefis.Rows[i]["FisNo"].ToString();
                    AciklamaTxt.Text = tblguncelefis.Rows[i]["Aciklama"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(tblguncelefis.Rows[i]["Tarih"].ToString());
                    MusteriUnvaniTxt.Text = tblguncelefis.Rows[i]["MusteriAdi"].ToString();
                    MusteriKoduTxt.Text = tblguncelefis.Rows[i]["MusteriKodu"].ToString();
                    MusteriIdLbl.Text = tblguncelefis.Rows[i]["MusteriRef"].ToString();
                    dataGridView1.Rows.Add("", tblguncelefis.Rows[i]["UrunAdi"].ToString(), tblguncelefis.Rows[i]["UrunKodu"].ToString()
                        , tblguncelefis.Rows[i]["Miktar"].ToString(), tblguncelefis.Rows[i]["UrunRef"].ToString()
                        , tblguncelefis.Rows[i]["UrunBirimi"].ToString(), tblguncelefis.Rows[i]["FisHaraketId"].ToString());
                    
                }
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[0].Cells[1].Value == null)
            {
                MessageBox.Show("Giriş Fişi Güncelleyebilmeniz İçin Ürün Eklemeniz Gerekmektedir.");
                return;
            }
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["UrunAdi"].Value != null)
                {


                    if (dataGridView1.Rows[i].Cells["UrunAdet"].Value == null)
                    {
                        MessageBox.Show("Adet Girmeniz Gerekmektedir.");
                        return;
                    }

                }
            }
            string sql = "Update FisBaslik set FisNo='" + FisNoTxt.Text + "',GuncellemeTarihi='" + dateTimePicker1.Value.ToString("yyyy-MM-dd")
                + "',MusteriId='" + MusteriIdLbl.Text + "',Aciklama='" + AciklamaTxt.Text + "' where FisId=" + fisid;
            
            DataBase.ExecSql(sql);
            //Fiş Haraket
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["UrunAdi"].Value != null)
                {
                    string komut = "Update FisHaraket set UrunRef='" + dataGridView1.Rows[i].Cells["UrunId"].Value + "',Birim='" 
                        + dataGridView1.Rows[i].Cells["UrunBirimi"].Value + "',Miktar='" + dataGridView1.Rows[i].Cells["UrunAdet"].Value + "' where FisHaraketId=" + dataGridView1.Rows[i].Cells["FisHaraketId"].Value;

                    DataBase.ExecSql(komut);
                }

            }
            for (int k = 0; k < dataGridView1.Rows.Count; k++)
            {

                if (dataGridView1.Rows[k].Cells["UrunAdi"].Value != null)
                {

                    int topla = Convert.ToInt32(dataGridView1.Rows[k].Cells["UrunAdet"].Value.ToString());

                    string cikar = "-" + Convert.ToInt32(dataGridView1.Rows[k].Cells["UrunAdet"].Value.ToString());

                    string stokekle = "Update Stok set UrunRef='" + dataGridView1.Rows[k].Cells["UrunId"].Value + "',Birim='"
                        + dataGridView1.Rows[k].Cells["UrunBirimi"].Value + "',Miktar='" + cikar + "' where FisHaraketId=" + dataGridView1.Rows[k].Cells["FisHaraketId"].Value;
                    DataBase.ExecSql(stokekle);
                    
                }
            }
            MessageBox.Show("Güncellendi!");
            frmCikisFisi cikisfisi = new frmCikisFisi();
            cikisfisi.Show();

            this.Hide();
        }
    }
}
