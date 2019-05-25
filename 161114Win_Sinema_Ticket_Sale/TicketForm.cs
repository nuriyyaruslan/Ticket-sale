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
    public partial class TicketForm : Form
    {
        public TicketForm()
        {
            InitializeComponent();
        }
        ConnectionClass conClass = new ConnectionClass();
        SqlDataAdapter adapt = new SqlDataAdapter();   
        public int SelectSalon(string Salon)
        {
            int SalonNo = 0;
            adapt = new SqlDataAdapter($"Select Row_Count from Salon Where SalonName='{Salon}'", conClass.GetConnection());
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[0];
                SalonNo = int.Parse(dr[0].ToString());
            }
            return SalonNo;
        }
        Movies mv = new Movies();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbSalon.Text)
            {
                case "Salon1":
                    Chair(SelectSalon("Salon1"));
                    break;
                case "Salon2":
                    Chair(SelectSalon("Salon2"));
                    break;
                case "Salon3":
                    Chair(SelectSalon("Salon3"));
                    break;
                default:
                    break;
            }
        }
        string satilmisStollar = "";
        void Chair(int sira)
        {
            yavaslat:
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button && ctrl.Text!="Bilet al" || ctrl is Label && ctrl.Text== "Pərdə") 
                {                                 
                    Button btn = ctrl as Button;
                    Label lbll = ctrl as Label;
                    this.Controls.Remove(ctrl);
                    goto yavaslat;                   
                }                                   
            }          
            int stolNo = 1;
            for (int i = sira; i>0 ; i--)
            {
                for (int j = 0; j < 10; j++)
                {                   
                    Button stol = new Button();
                    stol.Height = stol.Width = 55;
                    stol.Top = 5+ i * 50;
                    stol.Left = 45 + j * 55;
                    stol.Text = stolNo.ToString();                  
                    stol.BackgroundImageLayout = ImageLayout.Stretch;                 
                        stolNo++;
                    stol.Click += Stol_Click;
                    stol.MouseDown += Stol_MouseDown;
                    stol.ContextMenuStrip = contextMenuStrip1;
                    stol.BackgroundImage = new Bitmap(Properties.Resources.chair_gray);
                    string[] chairs = satilmisStollar.Split(';'); 
                    if(chairs.Contains(stol.Text))
                    {
                        stol.BackgroundImage = null;
                        stol.BackgroundImage = new Bitmap(Properties.Resources.chair_Red);
                        stol.Enabled = false;
                    }               
                    this.Controls.Add(stol);
                }    
            }
            Label lbl = new Label();
            lbl.AutoSize = false;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Width = 550;
            lbl.Height = 40;
            lbl.Top = sira*55 +25;
            lbl.Left = 45;
            lbl.Text = "Pərdə";
            lbl.BackColor = Color.Red;
            lbl.ForeColor = Color.Black;
            this.Controls.Add(lbl);
        }
        Button clickedButton;
        private void Stol_MouseDown(object sender, MouseEventArgs e)
        {
            clickedButton = sender as Button;
        }
        //saled chairs:
        int cost=0;
        private void Stol_Click(object sender, EventArgs e)
        {
                clickedButton = (Button)sender;
                clickedButton.BackgroundImage = null;
                clickedButton.BackgroundImage = new Bitmap(Properties.Resources.chair_Red);
                if (txtchoosedTable.Text.Contains(clickedButton.Text))
                {
                    MessageBox.Show("Bu stol satılmışdır");
                }
                else
                {
                    txtchoosedTable.Text += clickedButton.Text + ";";
                    cost += Convert.ToInt32(cmbCost.Text);
                    txtSumCost.Text = cost.ToString();
                }      
        }
        /*--------------------------------------------------------------*/     
        private void TicketForm_Load(object sender, EventArgs e)
        {
            txtchoosedTable.ReadOnly = true;
            txtSumCost.ReadOnly = true;
            int MId = 0;
            int SnsId = 0;
            int SalonId = 0;
            adapt = new SqlDataAdapter($"Select MovieName,SalonName,SeansDate,SeansTime from Seans Join Salon on Salon.Id=Seans.SalId Join Movie on Movie.Id=Seans.MovId Where MovieName='{txtMovieName.Text}'", conClass.GetConnection());
            DataTable dtable = new DataTable();
            adapt.Fill(dtable);
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                DataRow drow = dtable.Rows[i];
                cmbSalon.Items.Add(drow[1].ToString());
                cmbDate.Items.Add(drow[2].ToString());
                cmbSeans.Items.Add(drow[3]).ToString();
                MId = mv.SelectId($"Select Id from Movie Where MovieName='{txtMovieName.Text}'");
                SalonId = mv.SelectId($"Select Id from Salon Where SalonName='{drow[1].ToString()}'");
                SnsId = mv.SelectId($"Select Id from Seans Where MovId='{MId.ToString()}' and SeansDate='{drow[2].ToString()}' and SeansTime='{drow[3]}' and SalId='{SalonId}'");
                adapt = new SqlDataAdapter($"Select StolNo from Ticket Where Date='{drow[2].ToString()}' and Ticket.MovieId='{MId}' and Ticket.SalonId='{SalonId}' and Ticket.SeansId='{SnsId}'",conClass.GetConnection());
                DataTable dt = new DataTable();                  
                adapt.Fill(dt);
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    DataRow dr = dt.Rows[k];
                    satilmisStollar += dr[0] + ";";
                }
            }   
        }     
        private void iptalEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bu stolu iptal etmək istədiyinizə əmisinizmi?", "-",MessageBoxButtons.OKCancel,MessageBoxIcon.None)==DialogResult.OK)
            {
                clickedButton.BackgroundImage = new Bitmap(Properties.Resources.chair_gray);
                if (cost > 0)
                {
                    cost = cost - int.Parse(cmbCost.Text);
                    txtSumCost.Text = cost.ToString();                 
                }
                txtchoosedTable.Text=txtchoosedTable.Text.Replace(clickedButton.Text+";", "");
                MessageBox.Show(" Stol iptal edildi");
            }
        }

        private void btnBuyTicket_Click(object sender, EventArgs e)
        {
            if (txtClientName.Text != "" && txtClientSurname.Text != "" && txtMovieName.Text != "" && txtSumCost.Text != "" && txtchoosedTable.Text != "" && cmbCost.Text != "" && cmbSalon.Text != "" && cmbSeans.Text != "" && cmbDate.Text != "")
            {
                if (MessageBox.Show("Bilet almaq istediyinizə əminsinizmi?", "_", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                {
                    conClass.InsertMethod($"Insert into Client(ClientName,ClientSurname) values('{txtClientName.Text}','{txtClientSurname.Text}')");                        
                    int clntId = 0;
                    int filminId = 0;
                    int seansId = 0;
                    int salonId = 0;
                    int saleId = 0;
                    salonId = mv.SelectId($"Select Id from Salon Where SalonName='{cmbSalon.Text}'");   
                    filminId = mv.SelectId($"Select Id from Movie Where MovieName='{txtMovieName.Text}'");
                    seansId = mv.SelectId($"Select Id from Seans Where MovId='{filminId}' and SeansDate='{cmbDate.Text}' and SeansTime='{cmbSeans.Text}' and SalId='{salonId}'");
                    clntId = mv.SelectId($"Select Id from Client Where ClientName='{txtClientName.Text}' and ClientSurname='{txtClientSurname.Text}'");         
                    conClass.InsertMethod($"Insert into Sale(Date,ClientId,Price) values('{cmbDate.Text}','{clntId}','{txtSumCost.Text}')");
                    saleId = mv.SelectId($"Select Id from Sale Where Date='{cmbDate.Text}' and Price='{txtSumCost.Text}' and ClientId='{clntId}'");
                    conClass.InsertMethod($"Insert into Ticket(MovieId,SalonId,SeansId,Date,StolNo,SaleId,ClientN,ClientS) values('{filminId}','{salonId}','{seansId}','{cmbDate.Text}','{txtchoosedTable.Text}','{saleId}','{txtClientName.Text}','{txtClientSurname.Text}')");
                    MessageBox.Show("Bilet Alınmışdır");
                    int ticketId = 0;
                    ticketId = mv.SelectId($"Select Id from Ticket Where SaleId='{saleId}'");
                    TicketSaled tk = new TicketSaled();
                    tk.lblPrice.Text = txtSumCost.Text;
                    tk.lblMovieName.Text = txtMovieName.Text;
                    tk.lblNameSurname.Text = txtClientName.Text + " " + txtClientSurname.Text;
                    tk.lblSalonNo.Text = cmbSalon.Text;
                    tk.lblSeans.Text = cmbSeans.Text;
                    tk.lblStol.Text = txtchoosedTable.Text;
                    tk.lbltkNo.Text = tk.lblBiletNo.Text = ticketId.ToString();                         
                    tk.Show();
                }
            }
            else
            {
                MessageBox.Show("Gərəkli xanaları doldurun");
            }
        }
    }
}
