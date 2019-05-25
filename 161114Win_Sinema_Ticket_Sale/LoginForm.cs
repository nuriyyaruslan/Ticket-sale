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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        ConnectionClass conClass = new ConnectionClass();
        SqlDataAdapter adapt = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public void GetRows(string Query,ComboBox cmb)
        {
            adapt = new SqlDataAdapter(Query, conClass.GetConnection());
            adapt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                cmb.Items.Add(dr[0].ToString());
            }
            conClass.GetConnection().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
                this.Hide();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string DbCoulmnUserName = "";
            string DbColumnPassword = "";
            adapt = new SqlDataAdapter($"Select UserName,Password from Worker Where UserName='{txtUserName.Text}' and Password='{txtPassword.Text}'", conClass.GetConnection());
            adapt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                DbCoulmnUserName = dr[0].ToString();
                DbColumnPassword = dr[1].ToString();
            }
            if (txtUserName.Text == DbCoulmnUserName && txtPassword.Text == DbColumnPassword)
            {
                Movies Mv = new Movies();
                Mv.StartPosition = FormStartPosition.CenterScreen;
                Mv.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show($"Daxil etdiyiniz şifrə yalnışdır.\r\n Yeniden cehd edin ve ya şifrəni yeniləyin.");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPasswordForm FPForm = new ForgotPasswordForm();
            FPForm.StartPosition = FormStartPosition.CenterScreen;
            FPForm.ShowDialog();
        }
    }
}
