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
using System.Data.Sql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace kitaplik
{
    public partial class emanet : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
        SqlDataReader dr;
        SqlCommand cmd;

        public emanet(string ad_, string soyad_)
        {
            InitializeComponent();
            ad = ad_;
            soyad = soyad_;
        }
        string ad, soyad;

        private void button1_Click(object sender, EventArgs e)
        {
           
            //string TCno = textBox1.Text;
            string isbn=textBox2.Text;
            

            conn.Open();
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd = new SqlCommand("Select * from book where ISBN='"+isbn+"'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            { MessageBox.Show("hohoho");
                if(isbn == dr["ISBN"].ToString().TrimEnd().TrimStart())
                {
                    if (Convert.ToInt32(dr["adet"]) > 0 && comboBox1.Text=="EMANET VERILDI")
                    {
                        //dr.Close();
                        SqlCommand cmdstok = new SqlCommand("update book set(book_adet = book_adet - 1) where ISBN=@ISBN", conn);
                        cmdstok.ExecuteNonQuery();
                        SqlCommand command = new SqlCommand("insert into emanet (tc,ISBN,kitapadi,emanetdurumu)values(@tc,@ISBN,@kitapadi,@emanetdurumu)", conn);
                        command.Parameters.AddWithValue("@tc", textBox1.Text);
                        command.Parameters.AddWithValue("@ISBN", textBox2.Text);
                        command.Parameters.AddWithValue("@kitapadi", textBox3.Text);
                        command.Parameters.AddWithValue("@emanetdurumu", comboBox1.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("KAYIT BAŞARILI BİR ŞEKİLDE TAMAMLANDI", "SİSTEM");
                    }
                    else if(Convert.ToInt32(dr["adet"]) >= 0 && comboBox1.Text=="TESLIM ALINDI")
                    {
                        SqlCommand cmdstok = new SqlCommand("update book set(book_adet = book_adet + 1) where ISBN=@ISBN", conn);
                        cmdstok.ExecuteNonQuery();
                        SqlCommand command = new SqlCommand("insert into emanet (tc,ISBN,kitapadi,emanetdurumu)values(@tc,@ISBN,@kitapadi,@emanetdurumu)", conn);
                        command.Parameters.AddWithValue("@tc", textBox1.Text);
                        command.Parameters.AddWithValue("@ISBN", textBox2.Text);
                        command.Parameters.AddWithValue("@kitapadi", textBox3.Text);
                        command.Parameters.AddWithValue("@emanetdurumu", comboBox1.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("KAYIT BAŞARILI BİR ŞEKİLDE TAMAMLANDI", "SİSTEM");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(comboBox1.Text) && comboBox1.Text == "GECIKTI")
                        {
                        SqlCommand command = new SqlCommand("insert into emanet (tc,ISBN,kitapadi,emanetdurumu)values(@tc,@ISBN,@kitapadi,@emanetdurumu)", conn);
                        command.Parameters.AddWithValue("@tc", textBox1.Text);
                        command.Parameters.AddWithValue("@ISBN", textBox2.Text);
                        command.Parameters.AddWithValue("@kitapadi", textBox3.Text);
                        command.Parameters.AddWithValue("@emanetdurumu", comboBox1.Text);
                        command.ExecuteNonQuery();
                        MessageBox.Show("KAYIT BAŞARILI BİR ŞEKİLDE TAMAMLANDI", "SİSTEM");
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("ISBN No veya Stok Bilgileri Hatalı!", "SİSTEM");
                }
            }conn.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            user user = new user(ad.ToString(), soyad.ToString());
            user.Show();
            this.Hide();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           // e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void emanet_Load(object sender, EventArgs e)
        {
            label1.Text = ad + " " + soyad;
            button1.Text = "Kaydet";
            button2.Text = "Geri Dön";
        }
    }
}
