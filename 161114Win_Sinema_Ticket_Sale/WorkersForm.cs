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
    public partial class WorkersForm : Form
    {
        public WorkersForm()
        {
            InitializeComponent();
        }
        //Connection class is global.
        ConnectionClass ConClass = new ConnectionClass();
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bu səhifədən çıxmaq istədiyinizə əminsinizmi?", "Bildiriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                this.Close();
            else
                this.DialogResult = DialogResult.Cancel;
        }
        //Select Method for WorkersForm
        //SelectMethod -u can use for Search too:
        void SelectMethod(string Query)
        {
            Clear();
            SqlConnection connection = ConClass.GetConnection();
            try
            {
                SqlDataAdapter adapt = new SqlDataAdapter(Query, connection);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
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
                connection.Close();
            }
        }
        public void SelectForWorker()
        {
           SelectMethod("Select WorkerName as Ad,WorkerSurname as Soyad,Birthdate as DoğumTarixi,Email as ElektronAdres,Phone as Telefon,PersonalityNo as ŞəxsiyyətSeriaNo,WorkPost as Vəzifə,UserName as İstifadəçiAdı,Password as Şifrə,Gender as Cinsi from Worker");
        }
        private void WorkersForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "";
            SelectForWorker();
        }
        private void btnWkSelect_Click(object sender, EventArgs e)
        {
            SelectForWorker();
        }
        //Clear Method
         void Clear()
        {
            dateTimePicker1.CustomFormat = "";
            txtWkName.Clear();
            txtWkSurname.Clear();
            txtWkuserName.Clear();
            txtWkEmail.Clear();
            txtWkParol.Clear();
            txtWkPersonalityNo.Clear();
            comboBox1.SelectedIndex = -1;
            maskedTextBox1.Clear();
            txtWkName.Focus();           
        }
        //Insert Method for WorkersForm
        void InsertMethodForWorker()
        {   
            //errorprovider test
            if (String.IsNullOrEmpty(txtWkName.Text))
                errorProvider1.SetError(txtWkName, "Ad daxil edin.");
            if (String.IsNullOrEmpty(txtWkSurname.Text))
                errorProvider1.SetError(txtWkSurname, "Soyad daxil edin.");
            if (String.IsNullOrEmpty(txtWkPersonalityNo.Text))
                errorProvider1.SetError(txtWkPersonalityNo, "Şəxsiyyət vəsiqəsinin seria nömrəsini daxil edin.");
            if (String.IsNullOrEmpty(txtWkParol.Text))
                errorProvider1.SetError(txtWkParol, "Şifrə daxil edin.");
            if (String.IsNullOrEmpty(txtWkEmail.Text))
                errorProvider1.SetError(txtWkEmail, "Elektron adres daxil edin.");
            if (String.IsNullOrEmpty(comboBox1.Text))
                errorProvider1.SetError(comboBox1, "Vəzifə daxil edin");
            if (String.IsNullOrEmpty(maskedTextBox1.Text))
                errorProvider1.SetError(maskedTextBox1,"Telefon nömrəsi daxil edin");
            if (String.IsNullOrEmpty(txtWkuserName.Text))
                errorProvider1.SetError(txtWkuserName, "İstifadəçi adı daxil edin.");
            else
            {
                errorProvider1.Clear();
                if (MessageBox.Show("Yadda saxlamaq istədiyinizə əminsinizmi?", "Bildiriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (rdbMale.Checked)  //Insert Method from ConnectionClass
                        ConClass.InsertMethod($"Insert into Worker(WorkerName,WorkerSurname,Birthdate,Email,Phone,PersonalityNo,UserName,Password,WorkPost,Gender) values(N'{Accuracy(txtWkName.Text)}',N'{Accuracy(txtWkSurname.Text)}',N'{dateTimePicker1.Text}',N'{txtWkEmail.Text}',N'{maskedTextBox1.Text}',N'{txtWkPersonalityNo.Text}',N'{txtWkuserName.Text}',N'{txtWkParol.Text}',N'{comboBox1.Text}',N'{rdbMale.Text}')");
                    if (rdbFemale.Checked)
                        ConClass.InsertMethod($"Insert into Worker(WorkerName,WorkerSurname,Birthdate,Email,Phone,PersonalityNo,UserName,Password,WorkPost,Gender) values(N'{Accuracy(txtWkName.Text)}',N'{Accuracy(txtWkSurname.Text)}',N'{dateTimePicker1.Text}',N'{txtWkEmail.Text}',N'{maskedTextBox1.Text}',N'{txtWkPersonalityNo.Text}',N'{txtWkuserName.Text}',N'{txtWkParol.Text}',N'{comboBox1.Text}',N'{rdbFemale.Text}')");
                    SelectForWorker();
                    MessageBox.Show("Müvəffəqiyyətlə qeyd edildi.");
                    Clear();     
                }                  
            }
        }
        //in order to upper first letter of some text:l
        public string Accuracy(string value)
        {
           return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1).ToLower();
        }
        //Insert
        private void btnWkInsert_Click(object sender, EventArgs e)
        {
            InsertMethodForWorker();
        }
        //Update
        private void btnWkUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Dəyişikliyi yadda saxlamaq istədiyinizə əminsinizmi?", "Bildiriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ConClass.UpdateMethod($"Update Worker Set WorkerName=N'{Accuracy(txtWkName.Text)}',WorkerSurname=N'{Accuracy(txtWkSurname.Text)}',Birthdate=N'{dateTimePicker1.Text}',Email=N'{txtWkEmail.Text}',Phone=N'{maskedTextBox1.Text}',UserName=N'{txtWkuserName.Text}',WorkPost=N'{comboBox1.Text}',Password=N'{txtWkParol.Text}' Where PersonalityNo=N'{txtWkPersonalityNo.Text}'");
                MessageBox.Show("Dəyişiklik müvəffəqiyyətlə qeydə alındı");
                SelectForWorker();
                Clear();
            }
        }
        //Delete    
        private void btnWkDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmək istədiyinizə əminsinizmi?", "Bildiriş", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ConClass.DeleteMethod($"Delete from Worker where UserName='{txtWkuserName.Text}'");
                MessageBox.Show("Müvəffəqiyyətlə silindi");
                SelectForWorker();
                Clear();
            }          
        }
        private void DataGridViewWk_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnWkDelete.Visible = true;
                DataGridViewRow Row = dataGridView1.CurrentRow;
                txtWkName.Text = Row.Cells["Ad"].Value.ToString();
                txtWkSurname.Text = Row.Cells["Soyad"].Value.ToString();
                dateTimePicker1.Text = Row.Cells["DoğumTarixi"].Value.ToString();
                txtWkEmail.Text = Row.Cells["ElektronAdres"].Value.ToString();
                maskedTextBox1.Text = Row.Cells["Telefon"].Value.ToString();
                txtWkPersonalityNo.Text = Row.Cells["ŞəxsiyyətSeriaNo"].Value.ToString();
                comboBox1.Text = Row.Cells["Vəzifə"].Value.ToString();
                txtWkuserName.Text = Row.Cells["İstifadəçiAdı"].Value.ToString();
                txtWkParol.Text= Row.Cells["Şifrə"].Value.ToString();
                //if (Row.Cells["Cinsi"].Value.ToString() == "Qadın")
                //    rdbFemale.Checked = true;
                //else
                //    rdbMale.Checked = true;

            }
            catch (FormatException ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }   
       
        //Method select for every category
         void MethodCategoryType(string column)
        {
           SelectMethod($"Select WorkerName as Ad,WorkerSurname as Soyad,Birthdate as DoğumTarixi,Email as ElektronAdres,Phone as Telefon,PersonalityNo as ŞəxsiyyətSeriaNo,WorkPost as Vəzifə,UserName as İstifadəçiAdı,Password as Şifrə from Worker where {column} Like '%{txtWkSearch.Text}%'");
        }
        public void SearchMethod()
        {
            try
            {
                if (!String.IsNullOrEmpty(cmbWkCategory.Text) && !String.IsNullOrEmpty(txtWkSearch.Text))
                {
                    switch (cmbWkCategory.Text)
                    {
                        case "Ad":
                            MethodCategoryType("WorkerName");
                            break;
                        case "Soyad":
                            MethodCategoryType("WorkerSurname");
                            break;
                        case "Doğum Tarixi":
                            MethodCategoryType("Birthdate");
                            break;
                        case "Elektron Adres":
                            MethodCategoryType("Email");
                            break;
                        case "Telefon":
                            MethodCategoryType("Phone");
                            break;
                        case "Şəxsiyyət SeriaNo":
                            MethodCategoryType("PersonalityNo");
                            break;
                        case "Vəzifə":
                            MethodCategoryType("WorkPost");
                            break;
                        case "İstifadəçi adı":
                            MethodCategoryType("UserName");
                            break;
                        default:
                            MessageBox.Show("Axtarış uğursuzdur yeniden cəhd edin.");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Axtarış üçün gərəkli xanaları doldurun");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnWkSearch_Click(object sender, EventArgs e)
        {
            SearchMethod();
        }

        private void WorkersForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
                SearchMethod();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Clear();
        }
    }
}