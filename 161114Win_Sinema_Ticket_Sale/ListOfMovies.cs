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
    public partial class ListOfMovies : Form
    {
        public ListOfMovies()
        {
            InitializeComponent();
        }
        ConnectionClass conClass = new ConnectionClass();
        public void SelectMovieMethod(string Query)
        {
            try
            {
                SqlDataAdapter adat = new SqlDataAdapter(Query, conClass.GetConnection());
                DataSet ds = new DataSet();
                adat.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Nəticə Tapılmadı");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conClass.GetConnection().Close();
            }
        }
        private void ListOfMovies_Load(object sender, EventArgs e)
        {
            SelectMovieMethod("Select Picture As Afişa,About as Haqqında,MovieName as Ad,ThreeD as ÜçD,KindName as Növü,MovieTime as Vaxt,CountryName as Ölkə from Movie Join MovieKind on Movie.KindId=MovieKind.Id Join Country on Country.Id=Movie.CountyId");   
        }
        //Refresh Method to Clear and call SelectMethod again:
        public void RefreshMethod()
        {
            SelectMovieMethod("Select Picture As Afişa,About as Haqqında,MovieName as Ad,ThreeD as ÜçD,KindName as Növü,MovieTime as Vaxt,CountryName as Ölkə from Movie Join MovieKind on Movie.KindId=MovieKind.Id Join Country on Country.Id=Movie.CountyId");
            txtcountry.Clear();
            txtkind.Clear();
            txtname.Clear();
            txttime.Clear();
            richTextBoxMovie.Clear();
            pictureBoxMovie.Image = null;
            pictureBoxMovie.Invalidate();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnMovieDelete.Visible = true;
            DataGridViewRow Row = dataGridView1.CurrentRow;
            pictureBoxMovie.ImageLocation = Row.Cells["Afişa"].Value.ToString();
            richTextBoxMovie.Text = Row.Cells["Haqqında"].Value.ToString();
            txtname.Text = Row.Cells["Ad"].Value.ToString();
            txtkind.Text = Row.Cells["Növü"].Value.ToString();
            txttime.Text = Row.Cells["Vaxt"].Value.ToString()+ "  Dəqiqə";
            txtcountry.Text = Row.Cells["Ölkə"].Value.ToString();
            //show information
            lblİnfo.Visible = false;
            label9.Visible = label7.Visible = label5.Visible = label4.Visible = label3.Visible = txtcountry.Visible = txttime.Visible = txtkind.Visible = txtname.Visible = richTextBoxMovie.Visible = pictureBoxMovie.Visible = true;
            //input information of each movie to coresponding places:
          
        }      
        private void btnMovieDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmək istədiyinizə əminsinizmi?", "Bildiriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                conClass.DeleteMethod($"Delete from Movie Where MovieName='{txtname.Text}'");
                MessageBox.Show("Silindi");
                RefreshMethod();
            }
        }
        private void btnMovieUpdate_Click(object sender, EventArgs e)
        {
            if (richTextBoxMovie.Visible)
            {
                if (MessageBox.Show("Deyişikliyi yadda saxlamağa əminsinizmi?", "Bildiriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    conClass.UpdateMethod($"Update Movie Set About=N'{richTextBoxMovie.Text}' Where MovieName=N'{txtname.Text}'");
                    MessageBox.Show("Dəyişildi");
                }
                else
                    this.DialogResult = DialogResult.Cancel; 
            }
            else
                MessageBox.Show("İlk önce haqqında bölməsində deyişiklik edin.");
           
        }
        //visible method
        public void VisibleMethod()
        {
            RefreshMethod();
            lblİnfo.Visible = true;
            label9.Visible = label7.Visible = label5.Visible = label4.Visible = label3.Visible = txtcountry.Visible = txttime.Visible = richTextBoxMovie.Visible = pictureBoxMovie.Visible = txtname.Visible = txtkind.Visible = false;
        }
        public void SearchMethod()
        {
            if (String.IsNullOrEmpty(txtNameSearch.Text))
                errorProvider1.SetError(txtNameSearch, "Axtardığınız filmin adını daxil edin.");
            else
            {
                errorProvider1.Clear();
                VisibleMethod();
                SelectMovieMethod($"Select Picture As Afişa,About as Haqqında,MovieName as Ad,ThreeD as ÜçD,KindName as Növü,MovieTime as Vaxt,CountryName as Ölkə from Movie Join MovieKind on Movie.KindId=MovieKind.Id Join Country on Country.Id=Movie.CountyId Where MovieName Like '%{txtNameSearch.Text}%'");
            }
        }
        private void btnMovieSearch_Click(object sender, EventArgs e)
        {
            SearchMethod();  
        }
        private void btnMovie_Click(object sender, EventArgs e)
        {
            VisibleMethod();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ListOfMovies_KeyUp(object sender, KeyEventArgs e)
        {
            //f9 kliklendiyinde axtarsin:
            if (e.KeyCode == Keys.F9)
                SearchMethod();
        }
    }
}
