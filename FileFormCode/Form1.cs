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
using System.Configuration;
using System.Xml.Linq;


namespace FileFormCode
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into Emp values(@name,@salary)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDecimal(txtEmpSalary.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update Emp set name=@name,salary=@salary where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDecimal(txtEmpSalary.Text));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpId.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Emp where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpId.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from emp where id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpId.Text));
                con.Open();
                dr= cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtEmpName.Text = dr["name"].ToString();
                        txtEmpSalary.Text = dr["salary"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from emp";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
}

