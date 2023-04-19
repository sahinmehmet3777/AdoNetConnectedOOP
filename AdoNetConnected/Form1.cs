using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.Common;
using static AdoNetConnected.Form1;

namespace AdoNetConnected
{
    
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Select();
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (çalışanlar em = new çalışanlar())
                {
                    em.registration(textBox1.Text, textBox2.Text);
                    MessageBox.Show("Record added!");
                }
                textBox1.Select();
                Form1_Load(this, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ReadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (çalışanlar employee = new çalışanlar())
                {
                    List<çalışanlar> emp = employee.get_all_Employees(this);
                    dataGridView1.DataSource = emp;
                }
                textBox1.Select();
                Form1_Load(this, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                List<çalışanlar> Empl = new List<çalışanlar>();

                using (SqlConnection cnn = new SqlConnection("Data Source =.; Initial Catalog = Northwind; Integrated Security = True"))
                {
                    string komutsql = "Select * from Employees";
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(komutsql, cnn))
                    {
                        using (SqlDataReader okuyucu = cmd.ExecuteReader())
                        {
                            while (okuyucu.Read())
                            {
                                çalışanlar emp = new çalışanlar();

                                emp.ID = Convert.ToInt32(okuyucu["EmployeeID"]);
                                emp.Adı = okuyucu["FirstName"].ToString();
                                emp.Soyadı = okuyucu["LastName"].ToString();

                                Empl.Add(emp);
                            }
                            dataGridView1.DataSource = Empl;
                            textBox1.Select();
                        }
                    }
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (çalışanlar employee = new çalışanlar())
                {
                    employee.UpdateData(Convert.ToInt32(textBox3.Text), this);
                }
                textBox1.Text = "";
                textBox2.Text = null;
                textBox3.Clear();
                Form1_Load(this, null);
                textBox1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        int ID1 = 0;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID1 = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
            //int id = (int)dataGridView1.CurrentRow.Cells[0].Value; kısa hali
            string columnName = this.dataGridView1.Columns[e.ColumnIndex].Name;
            if (columnName == "ID")
            {
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value.ToString();
            }
            textBox1.Select();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            çalışanlar em = new çalışanlar();
            em.DeleteData(Convert.ToInt32(textBox3.Text), this);
            textBox1.Select();
            Form1_Load(this, null);
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string komutsql = "Select * from Employees where EmployeeID =" + "'" + Convert.ToInt32(textBox3.Text) + "'";
                List<çalışanlar> Empl = new List<çalışanlar>();

                using (SqlConnection cnn = new SqlConnection("Data Source =.; Initial Catalog = Northwind; Integrated Security = True"))
                {
                    cnn.Open();
                    using (SqlCommand cmd = new SqlCommand(komutsql, cnn))
                    {
                        using (SqlDataReader okuyucu = cmd.ExecuteReader())
                        {
                            while (okuyucu.Read())
                            {
                                çalışanlar emp = new çalışanlar();

                                emp.ID = Convert.ToInt32(okuyucu["EmployeeID"]);
                                emp.Adı = okuyucu["FirstName"].ToString();
                                emp.Soyadı = okuyucu["LastName"].ToString();

                                Empl.Add(emp);
                            }
                            dataGridView1.DataSource = Empl;
                        }
                    }
                    cnn.Close();
                }
                textBox1.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
// 