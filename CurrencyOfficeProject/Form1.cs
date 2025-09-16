using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;

namespace CurrencyOfficeProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=DbCurrency;Integrated Security=True");

        public string tcno;

        private void Form1_Load(object sender, EventArgs e)
        {
            
            string bugun = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var xmldosya= new XmlDocument();
            xmldosya.Load(bugun);

            string dolaralis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
            lbldolaral.Text = dolaralis;

            string dolarsatis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
            lbldolarsat.Text = dolarsatis;

            string euroalis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
            lbleuroal.Text = euroalis;

            string eurosatis = xmldosya.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
            lbleurosat.Text = eurosatis;


            lbltc.Text = tcno;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDOVIZ where MUSTERITC=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1",tcno);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) 
            {
                
                //lbltc.Text = dr[3].ToString();
                lblname.Text = dr[1].ToString();
                lblsoyad.Text = dr[2].ToString();
                lbltl.Text = dr[5].ToString();
                lbldolar.Text = dr[6].ToString();
                lbleuro.Text = dr[7].ToString();


            }
            baglanti.Close();
        }

        private void btndolaral_Click(object sender, EventArgs e)
        {
            txtkur.Text=lbldolaral.Text;
        }

        private void btndolarsat_Click(object sender, EventArgs e)
        {
            txtkur.Text=lbldolarsat.Text;
        }

        private void btneuroal_Click(object sender, EventArgs e)
        {
            txtkur.Text = lbleuroal.Text;

        }

        private void btneurosat_Click(object sender, EventArgs e)
        {
            txtkur.Text = lbleurosat.Text;

        }

        private void btnsatisyap_Click(object sender, EventArgs e)
        {
            double kur, miktar, tutar;
            kur = Convert.ToDouble(txtkur.Text);
            miktar=Convert.ToDouble(txtmiktar.Text);
            tutar = kur * miktar;
            txttutar.Text = tutar.ToString();

        }

        private void txtkur_TextChanged(object sender, EventArgs e)
        {
            txtkur.Text = txtkur.Text.Replace(".", ",");
        }

        private void btnsatisyap2_Click(object sender, EventArgs e)
        {
            double kur = Convert.ToDouble(txtkur.Text);
            int miktar = Convert.ToInt32(txtmiktar.Text);
            int tutar =Convert.ToInt32( miktar/kur);
            txttutar.Text=tutar.ToString();
            double kalan;
            kalan = miktar % kur ;
            txtkalan.Text= kalan.ToString();
        }
    }
}
