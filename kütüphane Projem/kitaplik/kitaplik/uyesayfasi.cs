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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace kitaplik
{
    public partial class uyesayfasi : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
        DataSet ds;
        SqlDataAdapter da;
        public uyesayfasi(string ad_, string soyad_)
        {
            InitializeComponent();
            ad = ad_;
            soyad = soyad_;
        }
        string ad, soyad;

        void griddoldur()
        {
            conn = new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
            da = new SqlDataAdapter("Select tc,ad,soyad,email,telno From uyeler", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "uyeler");
            dataGridView1.DataSource = ds.Tables["uyeler"];
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            string kayit = "update uyeler set tc=@tc, ad=@ad, soyad=@soyad, email=@email, telno=@telno where ID=@ID";
            SqlCommand cmd = new SqlCommand(kayit, conn);
            cmd.Parameters.AddWithValue("@tc", textBox1.Text);
            cmd.Parameters.AddWithValue("@ad", textBox2.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox3.Text);
            cmd.Parameters.AddWithValue("@email", textBox4.Text);
            cmd.Parameters.AddWithValue("@telno", textBox5.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Uye Bilgileri Güncellendi.");
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {/*
            label8.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();*/
        }
        private void uyesayfasi_Load(object sender, EventArgs e)
        {
            button1.Text = "Ekle";
            button2.Text = "Güncelle";
            button3.Text = "User Sayfasına Dön";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            label1.Text = ad + " " + soyad;
            griddoldur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            user user = new user(ad.ToString(), soyad.ToString());
            user.Show();
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("LÜTFEN ALANLARIN HEPSİNİ DOLDURUN BOŞ BIRAKMAYIN");
            }
            else
            {
                conn.Open();
                SqlCommand command = new SqlCommand("Insert into uyeler (tc,ad,soyad,email,telno)values(@tc,@ad,@soyad,@email,@telno)", conn);
                command.Parameters.AddWithValue("@tc", textBox1.Text);
                command.Parameters.AddWithValue("@ad", textBox2.Text);
                command.Parameters.AddWithValue("@soyad", textBox3.Text);
                command.Parameters.AddWithValue("@email", textBox4.Text);
                command.Parameters.AddWithValue("@telno", textBox5.Text);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("KAYIT BAŞARILI BİR ŞEKİLDE TAMAMLANDI", "SİSTEM");
            }
        }
    }
}
