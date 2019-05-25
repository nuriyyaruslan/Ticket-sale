using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace _161114Win_Sinema_Ticket_Sale
{
    public class ConnectionClass
    {
        public SqlConnection GetConnection()
        {
            //SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\v11.0;Integrated Security=True;AttachDbFileName='|DataDirectory|\\Bs_Sinema.mdf'");
            
               SqlConnection connection = new SqlConnection("Data Source = DESKTOP-GR3DLML\\SQLEXPRESS; Initial Catalog = Bs_Sinema; Integrated Security = True");
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection;
        }
        //Delete Method 
        //cmd globaldir.
        SqlCommand cmd = new SqlCommand();
        public void DeleteMethod(string Query)
        {
                try
               {
                    cmd = new SqlCommand(Query, GetConnection());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
               catch (SqlException ex)
               {
                   MessageBox.Show(ex.Message);
               }
               finally
               {
                    GetConnection().Close();
               }         
        }
        //Update Method
        public void UpdateMethod(string Query)
        {
                try
                {
                    cmd = new SqlCommand(Query, GetConnection());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    GetConnection().Close();
                }
        }
        //Insert Method
        public void InsertMethod(string Query)
        {
                try
                {
                    cmd = new SqlCommand(Query, GetConnection());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    GetConnection().Close();
                }
        }       
    }
}
