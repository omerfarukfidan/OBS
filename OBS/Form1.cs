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
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

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
        private void LogToFile(string message)
        {
            string logFilePath = @"C:\Users\omerf\Desktop\log.txt";

            try
            {
                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    writer.Write(message+"\t");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Log dosyasına yazma hatası: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Multiselect = true;

            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                Stopwatch sw = new Stopwatch();
                sw.Start();
                DataTable ÖğrenciCevaplar = new DataTable();
                ÖğrenciCevaplar.Columns.Add("SınavId", typeof(int));
                ÖğrenciCevaplar.Columns.Add("ÖğrenciId", typeof(int));
                ÖğrenciCevaplar.Columns.Add("SoruNo", typeof(int));
                ÖğrenciCevaplar.Columns.Add("Cevap", typeof(string));
                //foreach (string selectedFilePath in openFileDialog1.FileNames)

                    string folderPath = @"C:\Users\omerf\Desktop\Cevaplar";


foreach(string file in Directory.GetFiles(folderPath, "*.txt", SearchOption.AllDirectories))
                {

                    string text = File.ReadAllText(file, Encoding.UTF8).Replace("\r\n", "\n");


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
                sw.Stop();
                this.Text = sw.ElapsedMilliseconds.ToString();
                string logMessage = " Sonuclari Yorumla Butonu: " + sw.ElapsedMilliseconds.ToString() + " ms";
                LogToFile(logMessage);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 yeniForm = new Form2(); // Yeni form nesnesi oluşturun
            yeniForm.ShowDialog(); // Yeni formu görüntüleyin (modal olarak)
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Users\omerf\Desktop\Cevaplar";

            for (int numberOfStudents = 10; numberOfStudents < 200; numberOfStudents += 10)
            {
                //GC.Collect();
                //Thread.Sleep(2000);
                // Klasör yolunu belirtin
                Stopwatch sw = new Stopwatch();
                sw.Start();
                try
                {
                    // Klasör içindeki tüm dosyaları al
                    string[] files = Directory.GetFiles(folderPath);

                    // Her dosyayı sil
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    //MessageBox.Show("Tüm dosyalar başarıyla silindi.");
                }
                catch
                {
                    //MessageBox.Show("Hata oluştu: " + ex.Message);
                }

                //if (int.TryParse(textBox1.Text, out int numberOfStudents))
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(conString))
                        {
                            connection.Open();

                            using (SqlCommand command = new SqlCommand("InsertRandomStudentsAndStudentCourses", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@NumberOfStudents", numberOfStudents);
                                command.ExecuteNonQuery();
                                //MessageBox.Show("Öğrenciler başarıyla eklenmiştir.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata oluştu: " + ex.Message);
                    }
                }
                //else
                //{
                //    MessageBox.Show("Lütfen geçerli bir sayı girin.");
                //}

                sw.Stop();
                this.Text = sw.ElapsedMilliseconds.ToString();
                string logMessage = "\n"+numberOfStudents.ToString() + " Ogrenci icin" + " Insert Butonu: " + sw.ElapsedMilliseconds.ToString() + " ms";
                LogToFile(logMessage);

                // Önce button3_Click'i çağır
                button3_Click(sender, e);

                // Sonra button1_Click'i çağır
                button1_Click(sender, e);
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
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
                sw.Stop();
                this.Text = sw.ElapsedMilliseconds.ToString();
                string logMessage = " DB'den Getir Butonu: " + sw.ElapsedMilliseconds.ToString() + " ms";
                LogToFile(logMessage);

            }
        }
    }
}

