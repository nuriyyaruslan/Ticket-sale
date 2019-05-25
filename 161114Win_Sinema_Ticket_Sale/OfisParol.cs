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
    public partial class OfisParol : Form
    {
        public OfisParol()
        {
            InitializeComponent();
        }
        private void btnCancelParol_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void BtnOkParol_Click(object sender, EventArgs e)
        {
           if(txtParol.Text=="1")
            {
                this.Close();
                WorkersForm WrForm = new WorkersForm();
                WrForm.StartPosition = FormStartPosition.CenterScreen;
                WrForm.ShowDialog();                  
           }
           else
                MessageBox.Show("Parol yalnışdır,yenidən sınayın!");
        }
    }
}
