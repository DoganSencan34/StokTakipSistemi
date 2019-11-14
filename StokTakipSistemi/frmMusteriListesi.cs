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
    public partial class frmMusteriListesi : Form
    {
        public frmMusteriListesi()
        {
            InitializeComponent();
        }
        DataTable table;
        public string ara = "";
        private void frmMusteriListesi_Load(object sender, EventArgs e)
        {
            string sql = "Select * from Musteriler";
            table = DataBase.select(sql);
            dataGridView1.DataSource = table;

            dataGridView1.Columns[0].Visible = false;
        }
        public static int indis = 0;
        public bool cikisfisi = false;
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (cikisfisi==true)
                {
                    int MusteriRef = Convert.ToInt32(dataGridView1["MusteriRef", dataGridView1.CurrentCell.RowIndex].Value);
                    string kod = dataGridView1["MusteriKodu", dataGridView1.CurrentCell.RowIndex].Value.ToString();

                    string adi = dataGridView1["MusteriAdi", dataGridView1.CurrentCell.RowIndex].Value.ToString();


                    frmCikisFisiEkle frm = (frmCikisFisiEkle)Application.OpenForms["frmCikisFisiEkle"];
                    frm.MusteriUnvaniTxt.Text = adi;
                    frm.MusteriKoduTxt.Text = kod;
                    frm.MusteriIdLbl.Text = MusteriRef.ToString();
                    indis++;

                    cikisfisi = false;
                    this.Hide();
                }
                else
                {

              
                int MusteriRef = Convert.ToInt32(dataGridView1["MusteriRef", dataGridView1.CurrentCell.RowIndex].Value);
                string kod = dataGridView1["MusteriKodu", dataGridView1.CurrentCell.RowIndex].Value.ToString();

                string adi = dataGridView1["MusteriAdi", dataGridView1.CurrentCell.RowIndex].Value.ToString();


                frmGirisFisiEkle frm = (frmGirisFisiEkle)Application.OpenForms["frmGirisFisiEkle"];
                frm.MusteriUnvaniTxt.Text = adi;
                frm.MusteriKoduTxt.Text = kod;
                frm.MusteriIdLbl.Text = MusteriRef.ToString();
                indis++;
                this.Hide();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Geçersiz");
                return;

            }
        }

        private void UrunKoduAraTxt_TextChanged(object sender, EventArgs e)
        {
            if (UrunKoduAraTxt.Text.Trim() == "")
            {
                string sql = "Select * from Musteriler";
                table = DataBase.select(sql);
                dataGridView1.DataSource = table;

                dataGridView1.Columns[0].Visible = false;
            }
            else
            {


                ara = "Select * from Musteriler where MusteriKodu like '%" + UrunKoduAraTxt.Text + "%'";
                table = DataBase.select(ara);
                dataGridView1.DataSource = table;

                dataGridView1.Columns[0].Visible = false;

            }


        }

       

        private void MusteriAdiAraTxt_TextChanged(object sender, EventArgs e)
        {
            if (MusteriAdiAraTxt.Text.Trim() == "")
            {
                string sql = "Select * from Musteriler";
                table = DataBase.select(sql);
                dataGridView1.DataSource = table;

                dataGridView1.Columns[0].Visible = false;
            }
            else
            {


                ara = "Select * from Musteriler where MusteriAdi like '%" + MusteriAdiAraTxt.Text + "%'";
                table = DataBase.select(ara);
                dataGridView1.DataSource = table;

                dataGridView1.Columns[0].Visible = false;

            }




            
        }
    }
}

