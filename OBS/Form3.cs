using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using OBS.Models;
using System.Data;

namespace OBS
{
    public partial class Form3 : Form
    {
        private Ogrenci ogrenci;
        public string conString = "Data Source=DESKTOP-31EMK53\\SQLEXPRESS;Initial Catalog=OBS;Persist Security Info=True;User ID=obsUser;Password=1234";

        public Form3(Ogrenci ogrenciFromForm2)
        {
            InitializeComponent();
            ogrenci = ogrenciFromForm2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ogrenci != null)
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string query = "SELECT Puan FROM vSınavSonucu WHERE ÖğrenciId = @OgrenciId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OgrenciId", ogrenci.OgrId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                string sonuclar = "";
                                while (reader.Read())
                                {
                                    string puan = reader["Puan"].ToString();
                                    sonuclar += puan + "\n";
                                }
                                MessageBox.Show("Öğrenci Sonuçları:\n" + sonuclar);
                            }
                            else
                            {
                                MessageBox.Show("Öğrenci sonucu bulunamadı.");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Öğrenci bilgisi alınamadı.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
