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
    public partial class Movies : Form
    {
        public Movies()
        {
            InitializeComponent();
        }     
        private void Movies_Load(object sender, EventArgs e)
        {
            LoginForm Lg = new LoginForm();
            Lg.GetRows("Select CountryName from Country", cmbCountry);
            LoginForm Lgg = new LoginForm();
            Lgg.GetRows("Select KindName from MovieKind", cmbKind);
        }

        ConnectionClass conClass = new ConnectionClass();
        public int SelectId(string Query)
        {
            int id0 = 0;
            SqlDataAdapter adapt = new SqlDataAdapter(Query, conClass.GetConnection());
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            for(int i=0;i<dt.Rows.Count;i++)
            {
                DataRow dr = dt.Rows[0];
                id0 = int.Parse(dr[0].ToString());
            }  
            return id0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
                if (!String.IsNullOrEmpty(textBox1.Text) && richTextBox1.Text != "" && nudTime.Text != "" && nudMinute.Text!= "" && txtMovieName.Text != "" && cmbD.Text != "" && cmbKind.Text != "" && cmbCountry.Text != "")
                {
                    if (MessageBox.Show("Məlumatlar qeydə alınsın?", "Bildiriş", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                            int CntId = 0;
                            int MvkId = 0;
                            CntId = SelectId($"Select Id from Country Where CountryName =N'{cmbCountry.Text}'");
                            MvkId = SelectId($"Select Id from MovieKind Where KindName=N'{cmbKind.Text}'");
                            conClass.InsertMethod($"Insert into Movie (MovieName,ThreeD,KindId,MovieTime,CountyId,Picture,About) values(N'{txtMovieName.Text}',N'{cmbD.Text}','{MvkId}',N'{nudTime.Value.ToString() + " saat"} {nudMinute.Value.ToString() + " Dəqiqə"}','{CntId}',N'{pictureBox1.ImageLocation}',N'{richTextBox1.Text}')");
                            MessageBox.Show("Film əlavə olundu.");
                        richTextBox1.Clear();
                        nudTime.Text = "";
                        txtMovieName.Clear();
                        cmbCountry.SelectedIndex = -1;
                        cmbD.SelectedIndex = -1;
                        cmbKind.SelectedIndex = -1;
                        textBox1.Clear();
                        pictureBox1.Image = null;       
                    }
                    else
                        this.DialogResult = DialogResult.Cancel;
                }
                else
                    MessageBox.Show("Müvafiq xanaları doldurun");
                 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeansForm Sf = new SeansForm();
            Sf.ShowDialog();
        }

        private void btnListOfMovie_Click(object sender, EventArgs e)
        { 
            ListOfMovies lm = new ListOfMovies();
            lm.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Picture Folder|*.jpg";
            file.ShowDialog();
            textBox1.Text = file.FileName;
            pictureBox1.ImageLocation = file.FileName;
        }     
    }
}
