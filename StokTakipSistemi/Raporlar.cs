using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel= Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;




namespace StokTakipSistemi
{
    public partial class Raporlar : Form
    {
        public Raporlar()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = @"Select urn.UrunKodu,urn.UrunAdi,urn.UrunBirimi,urn.BirimFiyat,urn.UrunBirimi,SUM(stk.Miktar) as Miktar from Stok stk
                ,Urunler urn where stk.UrunRef=urn.UrunRef
                GROUP BY urn.UrunKodu,urn.UrunAdi,urn.UrunBirimi,urn.BirimFiyat,urn.UrunBirimi";
            System.Data.DataTable table = DataBase.select(sorgu);
            dataGridView1.DataSource = table;
            try
            {
                Raporlar raporlar = new Raporlar();
                raporlar.Enabled = false;
                pictureBox1.Visible = true;

                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                object Missing = Type.Missing;
                Workbook workbook = excel.Workbooks.Add(Missing);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        myRange.Select();


                    }
                }
                raporlar.Enabled = true;
                pictureBox1.Visible = false;
            }
            catch (Exception)
            {

                MessageBox.Show("Başarısız!");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = @"select  urn.UrunKodu,urn.UrunAdi,SUM(fh.Miktar) as Adet from FisHaraket fh,Urunler urn where fh.UrunRef=urn.UrunRef  and fh.FisTipi=2
                                GROUP BY urn.UrunAdi,urn.UrunKodu
                                ORDER BY SUM(fh.Miktar) desc";
            System.Data.DataTable table = DataBase.select(sorgu);
            dataGridView1.DataSource = table;
            try
            {
                Raporlar raporlar = new Raporlar();
                raporlar.Enabled = false;
                pictureBox1.Visible = true;

                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                object Missing = Type.Missing;
                Workbook workbook = excel.Workbooks.Add(Missing);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        myRange.Select();


                    }
                }
                raporlar.Enabled = true;
                pictureBox1.Visible = false;
            }
            catch (Exception)
            {

                MessageBox.Show("Başarısız!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu = @"select mst.MusteriKodu,mst.MusteriAdi,SUM(fh.Miktar) as [Verilen Mal] from FisBaslik fb,FisHaraket  fh
                            ,Musteriler mst where fb.FisId=fh.FisId and  fb.MusteriId=mst.MusteriRef and fh.FisTipi=2
                                GROUP BY  mst.MusteriKodu,mst.MusteriAdi
                                ORDER BY SUM(fh.Miktar) desc";
            System.Data.DataTable table = DataBase.select(sorgu);
            dataGridView1.DataSource = table;
            try
            {
                Raporlar raporlar = new Raporlar();
                raporlar.Enabled = false;
                pictureBox1.Visible = true;

                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                object Missing = Type.Missing;
                Workbook workbook = excel.Workbooks.Add(Missing);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        myRange.Select();


                    }
                }
                raporlar.Enabled = true;
                pictureBox1.Visible = false;
            }
            catch (Exception)
            {

                MessageBox.Show("Başarısız!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = @"Select urn.UrunKodu,urn.UrunAdi,urn.UrunBirimi,urn.MinimumStok,SUM(stk.Miktar) as [Toplam Stok] from Stok stk,Urunler urn where stk.UrunRef=urn.UrunRef
                                GROUP BY urn.UrunKodu,urn.UrunAdi,urn.UrunBirimi,urn.MinimumStok";
            System.Data.DataTable table = DataBase.select(sorgu);
            dataGridView1.DataSource = table;
            try
            {
                Raporlar raporlar = new Raporlar();
                raporlar.Enabled = false;
                pictureBox1.Visible = true;
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                object Missing = Type.Missing;
                Workbook workbook = excel.Workbooks.Add(Missing);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                }
                StartRow++;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        myRange.Select();
                    }
                }
                raporlar.Enabled = true;
                pictureBox1.Visible = false;
            }
            catch (Exception)
            {

                MessageBox.Show("Başarısız!");
            }
        }

        private void Raporlar_Load(object sender, EventArgs e)
        {

        }
    }
}
