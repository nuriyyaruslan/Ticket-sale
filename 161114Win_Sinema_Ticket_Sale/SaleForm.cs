using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _161114Win_Sinema_Ticket_Sale
{
    public partial class SaleForm : Form
    {
        public SaleForm()
        {
            InitializeComponent();
        }
        ConnectionClass conClass = new ConnectionClass();
        SqlDataAdapter adapt = new SqlDataAdapter();
        public void SelectMethod(string Query)
        {
            adapt = new SqlDataAdapter(Query, conClass.GetConnection());
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public void SelectM()
        {
            SelectMethod($"Select MovieName as FilminAdı,Sum(Cast(Price as int)) as Ümumi_Gəlir,Count(SaleId) as BiletSayı from Ticket Join Movie on Ticket.MovieId like Movie.Id Join Sale on Ticket.SaleId like Sale.Id Group by MovieName");
        }
        private void SaleForm_Load(object sender, EventArgs e)
        {
            SelectM();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectMethod($"Select MovieName,Sum(Cast(Price as int)) as umumi_gelir,Count(SaleId) as satis_sayi from Ticket Join Movie on Ticket.MovieId like Movie.Id Join Sale on Ticket.SaleId like Sale.Id where MovieName = '{textBox1.Text}' Group by MovieName");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectM();
        }
    }
}
