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
using System.IO;
using System.Security.Cryptography;
using System.Net.NetworkInformation;

namespace OBS
{
    public partial class Form1 : Form
    {

        //static string GenerateRandomLetters(int length)
        //{
        //    const string letters = "ABCDE"; // Olası harfler

        //    Random random = new Random();
        //    char[] chars = new char[length];

        //    for (int i = 0; i < length; i++)
        //    {
        //        int index = random.Next(letters.Length);
        //        chars[i] = letters[index];
        //    }

        //    return new string(chars);
        //}
         Random random = new Random();
        const  string letters = "ABCDE "; // Olası harfler
        char GenerateRandomLetter()
        {
            int index = random.Next(letters.Length);
            return letters[index];
        }

        public Form1()
        {
            InitializeComponent();
        }


        public string conString = "Data Source=DESKTOP-31EMK53\\SQLEXPRESS;Initial Catalog=OBS;Persist Security Info=True;User ID=obsUser;Password=1234";
        public string storedProcedureName = "DBdenGetir"; // Update with your actual stored procedure name




        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-31EMK53\\SQLEXPRESS;Initial Catalog=OBS;Persist Security Info=True;User ID=obsUser;Password=1234");

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        string dosyadi = "sinav", dosyayolu;

      


        int ÖğrenciId;
       
        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters if your stored procedure requires them
                    // command.Parameters.AddWithValue("@ParameterName", parameterValue);

                    DataSet ds = new DataSet();
                    using (SqlDataAdapter DA = new SqlDataAdapter(command))
                        DA.Fill(ds);

                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        int SınavId = (int)r["SınavId"];
                        int SoruSayısı = (int)r["SoruSayısı"];
                        string cvp = "";
                        dosyayolu = ("C:\\Users\\omerf\\Desktop\\Cevaplar\\" + SınavId.ToString() + ".txt");                       
                        File.AppendAllText(dosyayolu, string.Format("{0:000000}\n", SınavId));//sınav idyi yazdırma dbden gelcek

                        foreach (DataRow r2 in ds.Tables[1].Select("SınavId=" + SınavId.ToString()))
                        {
                             ÖğrenciId = (int)r2["ÖğrenciId"];

                             cvp = "";
                            for (int j=1;j<=SoruSayısı;j++)
                                cvp += GenerateRandomLetter();

                            File.AppendAllText(dosyayolu, string.Format("{0:0000} {1}\n", ÖğrenciId, cvp)); //öğrenci id ve cevapları yazdırma dbden gelcek
                        }
                       
                    }
                }


            }
        }
    }
}

