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
            if (ogrenci != null)
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string query = "SELECT YılSonuNotu FROM vAğırlıklandırılmışYılSonuOrtalamasıHesapla WHERE ÖğrenciId = @OgrenciId";
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
                                    string puan = reader["YılSonuNotu"].ToString();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (ogrenci != null)
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string query = "select DersAdi from Ders d join ÖğrenciDers ö on d.DersId = ö.DersId WHERE ö.ÖğrenciId = @OgrenciId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OgrenciId", ogrenci.OgrId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                string dersler = "";
                                while (reader.Read())
                                {
                                    string ders = reader["DersAdi"].ToString();
                                    dersler += ders + "\n";
                                }
                                MessageBox.Show("Öğrenci Dersleri:\n" + dersler);
                            }
                            else
                            {
                                MessageBox.Show("Öğrencinin aldığı ders bulunmadı.");
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (ogrenci != null)
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    string query = "select ÖgrAdı,BölümAdı,ÖgrMail from Öğrenci ö join Bölüm b on ö.BölümId = b.BölümId where ö.ÖğrenciId = @OgrenciId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OgrenciId", ogrenci.OgrId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                string bilgiler = "";
                                while (reader.Read())
                                {
                                    string Ad = reader["ÖgrAdı"].ToString();
                                    string BölümAdı = reader["BölümAdı"].ToString();
                                    string Mail = reader["ÖgrMail"].ToString();
                                    bilgiler += "Ad: " + Ad + "\n" + "Bölüm: " + BölümAdı + "\n" + "Mail Adresi: " +  Mail + "\n";
                                }
                                MessageBox.Show("Öğrenci Dersleri:\n" + bilgiler);
                            }
                            else
                            {
                                MessageBox.Show("Öğrencinin aldığı ders bulunmadı.");
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

    }
}
