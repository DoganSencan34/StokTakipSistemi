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
    public partial class frmMusteriGuncelle : Form
    {
        public frmMusteriGuncelle()
        {
            InitializeComponent();
        }

        public int dt_index;
        public string mstref = "";
        DataTable table;
        private void frmMusteriGuncelle_Load(object sender, EventArgs e)
        {
            if (mstref !=null)
            {
                string sql = "Select * from Musteriler where MusteriRef="+mstref;
                table=DataBase.select(sql);
                MusteriKoduTxt.Text = table.Rows[0]["MusteriKodu"].ToString();
                MusteriAdiTxt.Text = table.Rows[0]["MusteriAdi"].ToString();
                MusteriTipiCb.SelectedIndex = Convert.ToInt32(table.Rows[0]["MusteriTipi"]);
                TelefonTxt.Text = table.Rows[0]["Telefon"].ToString();
                Telefon2Txt.Text = table.Rows[0]["Telefon2"].ToString();
                FaxTxt.Text = table.Rows[0]["Fax"].ToString();
                MailTxt.Text = table.Rows[0]["Mail"].ToString();

                string id = Form1.kullaniciid;
                sql = "Select Adi from Kullanici where KullaniciId=" + id;
                sql = DataBase.select(sql).Rows[0]["Adi"].ToString();
                EkleyenTxt.Text = sql;

                
            }
        }

        private void IptalBtn_Click(object sender, EventArgs e)
        {
            frmMusteriTanimlari mstrtnm = new frmMusteriTanimlari();
            mstrtnm.Show();
            this.Hide();
        }

        private void KaydetBtn_Click(object sender, EventArgs e)
        {
            string komut = @"UPDATE Musteriler SET MusteriKodu='" + MusteriKoduTxt.Text +
                        "',MusteriAdi='" + MusteriAdiTxt.Text +
                        "',MusteriTipi='" + MusteriTipiCb.SelectedIndex +
                        "',Telefon='" + TelefonTxt.Text +
                        "',Telefon2='" + Telefon2Txt.Text +
                        "',Fax='"+FaxTxt.Text+"',Mail='"+MailTxt.Text+
                        "' WHERE MusteriRef=" + mstref;

            DataBase.ExecSql(komut);
            MessageBox.Show("Güncellendi.");

            frmMusteriTanimlari Mtanim = new frmMusteriTanimlari();
            Mtanim.Show();
            this.Hide();
        }
    }
}
