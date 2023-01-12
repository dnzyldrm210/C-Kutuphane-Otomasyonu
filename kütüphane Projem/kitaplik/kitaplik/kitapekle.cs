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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kitaplik
{
    public partial class kitapekle : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
      
        public kitapekle(string ad_, string soyad_)
        {
            InitializeComponent();
            ad = ad_;
            soyad = soyad_;
        }
        string ad, soyad;

      
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("LÜTFEN ALANLARIN HEPSİNİ DOLDURUN BOŞ BIRAKMAYIN");
            }
            else
            {
                        conn.Open();
                        SqlCommand command = new SqlCommand("Insert into book (kitapadi,yazari,kitapturu,baskiyili,ISBN,sayfasayisi,kitapdili,yayinevi,aciklama,adet)values(@kitapadi,@yazari,@kitapturu,@baskiyili,@ISBN,@sayfasayisi,@kitapdili,@yayinevi,@aciklama,@adet)", conn);
                        command.Parameters.AddWithValue("@kitapadi", textBox1.Text);
                        command.Parameters.AddWithValue("@yazari", textBox2.Text);
                        command.Parameters.AddWithValue("@kitapturu", textBox3.Text);
                        command.Parameters.AddWithValue("@baskiyili", textBox4.Text);
                        command.Parameters.AddWithValue("@ISBN", textBox5.Text);
                        command.Parameters.AddWithValue("@sayfasayisi", textBox6.Text);
                        command.Parameters.AddWithValue("@kitapdili", textBox7.Text);
                        command.Parameters.AddWithValue("@yayinevi", textBox8.Text);
                        command.Parameters.AddWithValue("@aciklama", textBox9.Text);
                        command.Parameters.AddWithValue("@adet", textBox10.Text);
                        command.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("KAYIT BAŞARILI BİR ŞEKİLDE TAMAMLANDI", "SİSTEM");
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            user user = new user(ad.ToString(), soyad.ToString());
            user.Show();
            this.Hide();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void kitap_Load(object sender, EventArgs e)
        {
            label1.Text = ad + " " + soyad;
            button1.Text = "KAYDET";
            button2.Text = "Geri Git";
        }
    }
}
