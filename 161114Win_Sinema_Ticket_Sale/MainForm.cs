using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _161114Win_Sinema_Ticket_Sale
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            LoginForm LF = new LoginForm();
            LF.StartPosition = FormStartPosition.CenterScreen;
            LF.ShowDialog();
        }

        private void başOfisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OfisParol OP = new OfisParol();
            OP.StartPosition = FormStartPosition.CenterScreen;
            OP.ShowDialog();        
        }

        private void btnCinema_Click(object sender, EventArgs e)
        {
            CinemaForm Cinema = new CinemaForm();
            Cinema.ShowDialog();
        }

        private void btnTicktQuery_Click(object sender, EventArgs e)
        {
            TicketSearch tks = new TicketSearch();
            tks.ShowDialog();
        }

        private void btnTicketDelete_Click(object sender, EventArgs e)
        {
            DeleteTicketForm dtf = new DeleteTicketForm();
            dtf.ShowDialog();
        }
        //Acording to open double times therefore we have to create method to do this
        public void openAboutForm()
        {
            AboutCinemaTicketSaleProject AboutForm = new AboutCinemaTicketSaleProject();
            AboutForm.StartPosition = FormStartPosition.CenterScreen;
            AboutForm.ShowDialog();
        }
        private void proqramHaqqındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openAboutForm();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openAboutForm();         
        }

        private void çıxışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bu proqramdan çıxmaq istədiyinizə əminsinizmi?", "Bildiriş", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void satışGöstəriciləriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleForm SF = new SaleForm();
            SF.ShowDialog();
            SF.TopMost = true;
        }
    }
}
