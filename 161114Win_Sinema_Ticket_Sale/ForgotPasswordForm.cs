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
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }
        ConnectionClass conClass = new ConnectionClass();
        private void btnCancel_Click(object sender, EventArgs e)
        {
                this.Hide();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yenilemek istediyinize eminsinizmi?", "Bildiris", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                conClass.UpdateMethod($"Update Worker Set Password ='{txtPassword.Text}' Where UserName='{txtUserName.Text}'");
                MessageBox.Show("Muveffeqiyyetle yenilendi");
            }
            else
                this.DialogResult = DialogResult.Cancel;
        }
    }
}
