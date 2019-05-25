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
    public partial class CinemaForm : Form
    {
        public CinemaForm()
        {
            InitializeComponent();
        }    
        ConnectionClass conClass = new ConnectionClass();
        SqlDataAdapter adapt = new SqlDataAdapter();
        private void CinemaForm_Load(object sender, EventArgs e)
        {
            //Create one button  with coressponding post of every movie: 
            adapt = new SqlDataAdapter($"Select PicturePath,MovieName from Seans Join Movie on Movie.Id=Seans.MovId", conClass.GetConnection());
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Button btn = new Button();
                btn.Height = 209;
                btn.Width = 180;
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                DataRow dr = dt.Rows[i];
                btn.BackgroundImage=Image.FromFile(dr[0].ToString());
                Label lbl = new Label();
                lbl.Text = dr[1].ToString();             
                lbl.Visible = false;
                btn.Controls.Add(lbl);         
                btn.Click += Btn_Click;
                flowLayoutPanel1.Controls.Add(btn);        
            }                            
        }
        Button clicked;
        private void Btn_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException(); //throw exception when you forgot about this click event
            //that is use to remember!
            clicked = (Button)sender; //or you can write clicked=sender as Button;//the both of the same.
            foreach (Label lbl in clicked.Controls)
            {          
               if(MessageBox.Show($"*{lbl.Text}* adlı filmə bilet almaq istədiyinizə əminsinizmi?","******",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
                {     
                    TicketForm tk = new TicketForm();
                    tk.txtMovieName.Text = lbl.Text;
                    tk.txtMovieName.ReadOnly = true;
                    tk.Show();
                }
            }
        }
    }
}   