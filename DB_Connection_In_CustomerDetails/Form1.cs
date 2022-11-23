using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Connection_In_CustomerDetails
{
    public partial class Form1 : Form
    {
        string connetionString = "";
        SqlConnection sqlCon;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        string query = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connetionString = "server =LAPTOP-4UV87UTN;Database=Customer;Trusted_Connection=true;";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                query = "Insert into Customerinfo values(" + txtCustomerId.Text + ", '" + txtCustomerName.Text + "',' " + txtCustomerAdress.Text + "','" + txtGender.Text + "')";
                sqlCon = new SqlConnection(connetionString);
                cmd = new SqlCommand(query, sqlCon);
                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                MessageBox.Show("Customer Information Saved Successfully");
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                query = "Update CustomerInfo Set CustomerName= '" + txtCustomerName.Text + "', CustomerAddress =' " + txtCustomerAdress.Text + " ',CustomerGender = '" + txtGender.Text + "' Where CustomerId= " + txtCustomerId.Text + "";



                sqlCon = new SqlConnection(connetionString);
                cmd = new SqlCommand(query, sqlCon);
                sqlCon.Open();
                int n = cmd.ExecuteNonQuery();
                if (n == 0)
                {
                    MessageBox.Show("Customer information not found");
                }
                else
                    MessageBox.Show("Customer Information updated  Successfully");
                sqlCon.Close();
               ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void ClearFields()
        //{
        //    throw new NotImplementedException();
        //}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = "Delete from Customer  Where CustomerId= " + txtCustomerId.Text + "";



                sqlCon = new SqlConnection(connetionString);
                cmd = new SqlCommand(query, sqlCon);
                sqlCon.Open();
                int n = cmd.ExecuteNonQuery();
                if (n == 0)
                {
                    MessageBox.Show("Customer information not found");
                }

                if (n == 1)
                {
                    MessageBox.Show("customer  Information Deleted Successfully");
                }
                sqlCon.Close();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                query = "Select * from CustomerInfo Where CustomerId= " + txtCustomerId.Text + "";



                sqlCon = new SqlConnection(connetionString);
                adpt = new SqlDataAdapter(query, sqlCon);
                sqlCon.Open();
                DataSet ds = new DataSet();
                adpt.Fill(ds, "CustomerInfo");
                sqlCon.Close();



                if (ds != null && ds.Tables["CustomerInfo"].Rows.Count > 0)
                {
                    txtCustomerName.Text = ds.Tables["CustomerInfo"].Rows[0]["CustomerName"].ToString();
                    txtCustomerAdress.Text = ds.Tables["CustomerInfo"].Rows[0]["CustomerAddress"].ToString();
                    txtGender.Text = ds.Tables["CustomerInfo"].Rows[0]["CustomerGender"].ToString();
                }
                else
                {
                    MessageBox.Show("No Customer Information Found");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        
           {
                ClearFields();
           }
        private void ClearFields()
        {
            txtCustomerId.Text = "";
            txtCustomerName.Text = "";
            txtCustomerAdress.Text = "";
            txtGender.Text = "";
        }
    }
}

