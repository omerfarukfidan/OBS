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
         Random random = new Random();
        const  string letters = "ABCDE "; 
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
        public string storedProcedureName = "DBdenGetir"; 

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-31EMK53\\SQLEXPRESS;Initial Catalog=OBS;Persist Security Info=True;User ID=obsUser;Password=1234");

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        string dosyadi = "sinav", dosyayolu;
        int ÖğrenciId;

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                foreach (string selectedFilePath in openFileDialog1.FileNames)
                {


                    string text = File.ReadAllText(selectedFilePath, Encoding.UTF8).Replace("\r\n", "\n");


                    DataTable ÖğrenciCevaplar = new DataTable();
                    ÖğrenciCevaplar.Columns.Add("SınavId", typeof(int));
                    ÖğrenciCevaplar.Columns.Add("ÖğrenciId", typeof(int));
                    ÖğrenciCevaplar.Columns.Add("SoruNo", typeof(int));
                    ÖğrenciCevaplar.Columns.Add("Cevap", typeof(string));

                    int SınavId = 0;
                    string s1 = text.Substring(0, text.IndexOf("\n"));


                    int.TryParse(s1, out SınavId);
                    text = text.Substring(text.IndexOf("\n") + 2);
                    foreach (string line in text.Split("\n".ToCharArray()))
                    {
                        if (line == "")
                            continue;

                        int ogrId = 0;
                        string cevaplar = "";

                        string s2 = line.Substring(0, line.IndexOf(" "));
                        int.TryParse(s2, out ogrId);
                        cevaplar = line.Substring(line.IndexOf(" ") + 1);


                        for (int i = 0; i < cevaplar.Length; i++)
                            ÖğrenciCevaplar.Rows.Add(SınavId, ogrId, i + 1, cevaplar.Substring(i, 1));


                    }

                    using (SqlConnection objConn = new SqlConnection(conString))
                    {
                        objConn.Open();
                        using (SqlCommand objCmd = new SqlCommand("insertpeople", objConn))
                        {
                            objCmd.CommandType = CommandType.StoredProcedure;
                            objCmd.Parameters.AddWithValue("tbl", SqlDbType.Structured).Value = ÖğrenciCevaplar;

                            objCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 yeniForm = new Form2(); // Yeni form nesnesi oluşturun
            yeniForm.ShowDialog(); // Yeni formu görüntüleyin (modal olarak)
        }

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

