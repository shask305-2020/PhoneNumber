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


namespace PhoneNumber
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\toxa_\source\repos\PhoneNumber\Database.mdf;Integrated Security=True");
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("ContactAddOrEdit", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@mode", "Add");
                sqlCmd.Parameters.AddWithValue("@ContactID", 0);
                sqlCmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@MobileNumber", txtNumber.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Address", rtxtAddr.Text.Trim());
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Сохранение успешно");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Сообщение об ошибке");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        void FillDataGridView()
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter("ContactViewOrSearch", sqlConnection);
            sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlData.SelectCommand.Parameters.AddWithValue("@ContactName", txtSearch.Text.Trim());
            DataTable dataTable = new DataTable();
            sqlData.Fill(dataTable);
            dataGrid.DataSource = dataTable;
            sqlConnection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Сообщение об ошибке");
            }
        }
    }
}
