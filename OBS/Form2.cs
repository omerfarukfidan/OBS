using OBS.Models;
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
    public partial class Form2 : Form
    {
        public string conString = "Data Source=DESKTOP-31EMK53\\SQLEXPRESS;Initial Catalog=OBS;Persist Security Info=True;User ID=obsUser;Password=1234";
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int ogrenciId))
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Öğrenci WHERE ÖğrenciId = @OgrenciId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OgrenciId", ogrenciId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Ogrenci ogrenci = new Ogrenci();
                                ogrenci.OgrId = ogrenciId;
                                ogrenci.OgrAd = reader["ÖgrAdı"].ToString(); // Öğrenci adını veritabanından al
                                MessageBox.Show(ogrenci.OgrAd+" isimli, " + ogrenci.OgrId+ " ID'li öğrenci veritabanında bulundu");

                                Form3 yeniForm = new Form3(ogrenci); // Ogrenci örneğini parametre olarak geçirin
                                yeniForm.ShowDialog(); // Yeni formu görüntüleyin (modal olarak)


                                // Ogrenci nesnesini kullanarak istediğiniz işlemleri yapabilirsiniz
                            }
                            else
                            {
                                MessageBox.Show("Öğrenci ID veritabanında bulunamadı.");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Geçerli bir öğrenci ID girin.");
            }
        }


    }
}
