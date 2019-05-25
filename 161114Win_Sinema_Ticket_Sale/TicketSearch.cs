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
    public partial class TicketSearch : Form
    {
        public TicketSearch()
        {
            InitializeComponent();
        }
        ConnectionClass conClass = new ConnectionClass();
        SqlDataAdapter adapt = new SqlDataAdapter();
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void SaledMethod()
        {
            adapt = new SqlDataAdapter($"Select SeansTime,SeansDate,StolNo,ClientN,ClientS,SalonName,MovieName,Price from Ticket Join Seans on Seans.Id=Ticket.SeansId Join Salon on Ticket.SalonId=Salon.Id Join Movie on Ticket.MovieId=Movie.Id Join Sale on Ticket.SaleId=Sale.Id Where Ticket.Id='{textBox1.Text}' ", conClass.GetConnection());
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show($"* {textBox1.Text} * nömrəli bilet yoxdur");
            }
            else
            {
                TicketSaled tk = new TicketSaled();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    tk.lblSeans.Text = dr[0].ToString();
                    tk.lblDate.Text = dr[1].ToString();
                    tk.lblStol.Text = dr[2].ToString();
                    tk.lblNameSurname.Text = dr[3].ToString() + " " + dr[4].ToString();
                    tk.lblSalonNo.Text = dr[5].ToString();
                    tk.lblMovieName.Text = dr[6].ToString();
                    tk.lblPrice.Text = dr[7].ToString();
                    tk.lbltkNo.Text = tk.lblBiletNo.Text = textBox1.Text;
                }
                tk.ShowDialog();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaledMethod();
        }

        private void TicketSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
                SaledMethod();
        }
    }
}
