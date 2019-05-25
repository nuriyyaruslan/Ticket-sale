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
    public partial class SeansForm : Form
    {
        public SeansForm()
        {
            InitializeComponent();
        }

        ConnectionClass conClass = new ConnectionClass();
        SqlDataAdapter adapt = new SqlDataAdapter();   
        public string getPictureMethod(string Query)
        {
            adapt = new SqlDataAdapter(Query,conClass.GetConnection());
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            DataRow path = dt.Rows[0];
            return path[0].ToString();
        } 
        private void cmbMovieName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if you choose moviename then you can see picture of movie on picturebox1
            if (cmbMovieName.Text != " ")
            {
                pictureBox1.ImageLocation = getPictureMethod($"Select Picture from Movie Where MovieName=N'{cmbMovieName.Text}'");
            }
            else
                MessageBox.Show("Film adı seçin");
        }
        public void SelectMethod(string Query)
        {
            try
            {
                adapt = new SqlDataAdapter(Query, conClass.GetConnection());
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                if(ds.Tables[0].Rows.Count==0)
                {
                    MessageBox.Show("Nəticə Tapılmadı");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(" Qiqqət! Xəta var!\r\n " + ex.Message);
            }
            finally
            {
                conClass.GetConnection().Close();
            }
        }
        //this method play refresh role as if
        public void SelectMethodForSeans()
        {
            SelectMethod($"Select MovieName as FilminAdı,SeansTime as SeansSaatı,SeansDate SeansTarixi,SalonName SalonAdı,Picture as Afişa from Seans Join Movie on Seans.MovId=Movie.Id join Salon on Seans.SalId=Salon.Id");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SelectMethodForSeans(); //in order to select all information from coressponding table....
            Clear();
        }
        //Clear Method
        public void Clear()
        {
            cmbSalon.SelectedIndex = -1;
            cmbSeansTime.SelectedIndex = -1;
        }
        //Add Seans....
        Movies mv = new Movies();
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbMovieName.Text != "" || cmbSalon.Text != "" || cmbSeansTime.Text != "")
            {
                string vaxt= "";
                string gun = "";
                string salon = "";
                adapt = new SqlDataAdapter($"Select SeansDate,SeansTime,SalonName from Seans Join Salon on Seans.SalId=Salon.Id", conClass.GetConnection());
                DataTable dtable = new DataTable();
                adapt.Fill(dtable);
                for (int f = 0; f < dtable.Rows.Count; f++)
                {
                    DataRow dr = dtable.Rows[f];
                    vaxt = dr[0].ToString() + "|";
                    gun = dr[1].ToString() + "|";
                    salon = dr[2].ToString() + "|";
                }
                if(!vaxt.Contains(cmbSeansTime.Text) && !salon.Contains(cmbSalon.Text) && !gun.Contains(dateTimePickerSeans.Text))
                {
                    if (MessageBox.Show($"{cmbMovieName.Text} adlı filmi {dateTimePickerSeans.Text} tarixinde {cmbSalon.Text} -də {cmbSeansTime.Text} tarixinde yayımlamaq istədiyinizə əminsinizmi?", "Bildiriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        int MOVId = 0;
                        int SALId = 0;
                        string picPath = "";
                        MOVId = mv.SelectId($"Select Id from Movie Where MovieName=N'{cmbMovieName.Text}'");
                        SALId = mv.SelectId($"Select Id from Salon Where SalonName=N'{cmbSalon.Text}'");
                        picPath = getPictureMethod($"Select Picture from Movie Where MovieName=N'{cmbMovieName.Text}'");
                        conClass.InsertMethod($"Insert into Seans (SeansTime,SeansDate,MovId,SalId,PicturePath) values(N'{cmbSeansTime.Text}',N'{dateTimePickerSeans.Text}','{MOVId}','{SALId}',N'{picPath}')");
                        MessageBox.Show("Yadda saxlanıldı");
                        Clear();
                        SelectMethodForSeans();
                    }
                    else
                        this.DialogResult = DialogResult.Cancel;
                }
               else
                {
                    MessageBox.Show("Bu seans doludur");
                }  
            }
            else
                MessageBox.Show("Müvafiq xanaları doldurun");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDelete.Visible = true;
            DataGridViewRow Row = dataGridView1.CurrentRow;
            cmbMovieName.Text = Row.Cells["FilminAdı"].Value.ToString();
            dateTimePickerSeans.Text = Row.Cells["SeansTarixi"].Value.ToString();
            cmbSeansTime.Text = Row.Cells["SeansSaatı"].Value.ToString();
            cmbSalon.Text = Row.Cells["SalonAdı"].Value.ToString();
        }    
        public void SearchMethod()
        {
            if (String.IsNullOrEmpty(cmbMovieName.Text))
                MessageBox.Show("Axtardığınız filmin adını müvafik xanadan seçin.");
            else
            {
                SelectMethod($"Select MovieName as FilminAdı, SeansTime as SeansSaatı, SeansDate SeansTarixi, SalonName SalonAdı,Picture as Afişa from Seans Join Movie on Seans.MovId = Movie.Id join Salon on Seans.SalId = Salon.Id Where MovieName='{cmbMovieName.Text}'");
            }
        }  
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMethod();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmbMovieName.Text) || cmbSalon.Text=="" || cmbSeansTime.Text=="" || dateTimePickerSeans.Text=="")
                MessageBox.Show("Silmək istədiyiniz film seasının məlumatlarını müvafik xanalara daxil edin.");
            else
            {
                if (MessageBox.Show($"{cmbMovieName.Text} adlı {dateTimePickerSeans.Text} tarixində {cmbSeansTime.Text} -də {cmbSalon.Text} -də \r\n yayımlanacaq  film seansını silmək istədiyinizə əminsinizmi?", "Bildiriş", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Movies mv = new Movies();
                    int slnId = 0;
                    slnId = mv.SelectId($"Select Id from Salon Where SalonName='{cmbSalon.Text}'");
                    conClass.DeleteMethod($"Delete from Seans Where SeansTime='{cmbSeansTime.Text}'and SeansDate='{dateTimePickerSeans.Text}'and SalId='{slnId}'");
                    MessageBox.Show("Seansın silinməsi müvəffəqiyyətlə tamamlandı");
                    SelectMethodForSeans();
                    Clear();                 
                }
                else
                    this.DialogResult = DialogResult.No;
            }
        }

        private void SeansForm_Load(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            SelectMethodForSeans();
            LoginForm Lg = new LoginForm();
            Lg.GetRows("Select MovieName from Movie", cmbMovieName);
            LoginForm Lg2 = new LoginForm();
            Lg2.GetRows("Select SalonName from Salon", cmbSalon);
        }

        private void SeansForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
                SearchMethod();
        }
    }
}