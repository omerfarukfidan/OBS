using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OBS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-31EMK53\\SQLEXPRESS;Initial Catalog=OBS;Persist Security Info=True;User ID=obsUser;Password=1234";


        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-31EMK53\\SQLEXPRESS;Initial Catalog=OBS;Persist Security Info=True;User ID=obsUser;Password=1234");
        private void Form1_Load(object sender, EventArgs e)
        {
            getir();
        }

        private void getir()
        {
            DataTable t = new DataTable();
            using (SqlConnection objConn = new SqlConnection(conString))
            {
                using (SqlCommand objCmd = new SqlCommand("BölümüGetir", objConn))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;


                    using (SqlDataAdapter da = new SqlDataAdapter(objCmd))
                    {
                        da.Fill(t);
                    }

                }
            }
            if (t.Rows.Count > 0)
                dataGridView1.DataSource = t;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
