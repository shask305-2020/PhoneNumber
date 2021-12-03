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
        int ContactID = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                if (button1.Text == "Сохранить")
                {
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
                else
                {
                    SqlCommand sqlCmd = new SqlCommand("ContactAddOrEdit", sqlConnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlCmd.Parameters.AddWithValue("@ContactID", ContactID);
                    sqlCmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@MobileNumber", txtNumber.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", rtxtAddr.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Запись обновлена успешно");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Сообщение об ошибке");
            }
            finally
            {
                sqlConnection.Close();
            }
            Reset();
            FillDataGridView();
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
            dataGrid.Columns[0].Visible = false;
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

        private void dataGrid_DoubleClick(object sender, EventArgs e)
        {
            
            if (dataGrid.CurrentRow.Index != -1)
            {
                ContactID = Convert.ToInt32(dataGrid.CurrentRow.Cells[0].Value);
                txtName.Text = dataGrid.CurrentRow.Cells[1].Value.ToString();
                txtNumber.Text = dataGrid.CurrentRow.Cells[2].Value.ToString();
                rtxtAddr.Text = dataGrid.CurrentRow.Cells[3].Value.ToString();
                button1.Text = "Обновить";
                btnDelete.Enabled = true;
            }
        }

        void Reset()
        {
            txtName.Text = txtNumber.Text = rtxtAddr.Text = "";
            txtSearch.Text = "";
            button1.Text = "Сохранить";
            ContactID = 0;
            btnDelete.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("ContactDelete", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ContactID", ContactID);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Запись успешно удалена");
                Reset();
                FillDataGridView();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Сообщение об ошибке");
            }
        }
    }
}
