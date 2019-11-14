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
    public partial class frmUrunListesi : Form
    {

        public string adi;
        public string kod;
        public string id;
        public string birim;
        public string adet;
        public frmUrunListesi()
        {
            InitializeComponent();
        }
        DataTable table;
        DataTable dt = new DataTable();
        private void frmUrunListesi_Load(object sender, EventArgs e)
        {
            string sql = "Select * from Urunler";
            table = DataBase.select(sql);
            dataGridView1.DataSource = table;

            dataGridView1.Columns[0].Visible = false;
        }
        public bool cikisfisi = false;
        public static int UrunListesiId;
        public static DataTable tble;
        public static int indis = 0;
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            try
            {
                int id = Convert.ToInt32(dataGridView1["UrunRef", dataGridView1.CurrentCell.RowIndex].Value);
                string kod = dataGridView1["UrunKodu", dataGridView1.CurrentCell.RowIndex].Value.ToString();
                string adi = dataGridView1["UrunAdi", dataGridView1.CurrentCell.RowIndex].Value.ToString();
                string birim = dataGridView1["UrunBirimi", dataGridView1.CurrentCell.RowIndex].Value.ToString();
                this.adi = adi;
                this.kod = kod;
                this.id = id + "";
                this.birim = birim;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
                return;

            }
        }
    }
}
