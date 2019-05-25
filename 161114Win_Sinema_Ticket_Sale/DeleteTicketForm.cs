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
    public partial class DeleteTicketForm : Form
    {
        public DeleteTicketForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        ConnectionClass conClass = new ConnectionClass();
        private void button2_Click(object sender, EventArgs e)
        {
            //yoxlasin eger silmek istediyimiz Id ye uygun bilet varsa o zaman silsin eks halda mesaj
            //cixartsinki bu adli bilet yoxdur:
            //bunun ucun Ticket bazasindan id lerin hamisi cekirik ve dt ye yigiriq.
            string biletler = "";
            SqlDataAdapter adapt = new SqlDataAdapter($"Select Id from Ticket", conClass.GetConnection());
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow drow = dt.Rows[i];
                biletler += drow[0] + ";";
            }
            string[] tickets = biletler.Split(';');
            if( tickets.Contains(textBox1.Text))
            {
                if (MessageBox.Show($"{textBox1.Text} nömrəli bilet silinsin?", "Bildiriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    conClass.DeleteMethod($"Delete from Ticket Where Ticket.Id='{textBox1.Text}'");
                    MessageBox.Show($"{textBox1.Text} nömrəli bilet silindi");
                }
                else
                    this.DialogResult = DialogResult.Cancel;
            }
            else
                MessageBox.Show($"*{textBox1.Text}* nömrəli bilet yoxdur");          
        }
    }
}