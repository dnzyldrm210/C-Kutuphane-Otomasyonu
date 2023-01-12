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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography;

namespace kitaplik
{
    public partial class admin : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
        DataSet ds;
        SqlDataAdapter da;

        public admin(string ad_,string soyad_)
        {
            InitializeComponent();
            ad=ad_; 
            soyad=soyad_;   
        }

        private string Md5(string text)
        {
            //MD5 şifreşleme//
            MD5 MD5Encrypting = new MD5CryptoServiceProvider();
            byte[] bytes = MD5Encrypting.ComputeHash(Encoding.UTF8.GetBytes(text.ToCharArray()));

            StringBuilder builder = new StringBuilder();


            foreach (var item in bytes)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }

        string ad,soyad;   

        void griddoldur()
        {
            conn = new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
            da = new SqlDataAdapter("Select * From kullanicilar", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "kullanicilar");
            dataGridView1.DataSource = ds.Tables["kullanicilar"];
            conn.Close();
        }
        void griddoldur2()
        {
            conn = new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
            SqlDataAdapter da2 = new SqlDataAdapter("Select username, surname, email, telno, adress, cinsiyet From kullanicilar", conn);
            DataSet ds2 = new DataSet();
            conn.Open();
            da2.Fill(ds2, "kullanicilar");
            dataGridView2.DataSource = ds2.Tables["kullanicilar"];
            conn.Close();
        }

        private void admin_Load(object sender, EventArgs e)
        {



            label1.Text = ad + " " + soyad;
            groupBox1.Text = "kullanıcı ekle";
            groupBox2.Text = "kullanıcı güncelle";
            groupBox3.Text = "kullanıcıları listele";
            button1.Text = "kullanıcı ekle";
            button2.Text = "kullanıcı güncelle - sil";
            button3.Text = "kullanıcıları listele";
            radioButton1.Text = "Kadın";
            radioButton2.Text = "Erkek";
            button4.Text = "KAYDET";
            button5.Text = "TEMİZLE";
            button6.Text = "GÜNCELLE";
            button7.Text = "Kitap sayfası";
            button8.Text = "SİL!!!";
            button9.Text = "ÇIKIŞ";
            label3.Text = "İsim";
            label4.Text = "Soyisim";
            label5.Text = "E-mail";
            label6.Text = "Şifre";
            label7.Text = "Tekrar Şifre";
            label8.Text = "Cinsiyet";
            label9.Text = "Telefon";
            label10.Text = "Adres";

            griddoldur();
            griddoldur2();
        }
        int kontrolemail(string email)
        {
            conn.Open();
            SqlCommand kontrol = new SqlCommand("select count(email) from kullanicilar where email='" + email + "'", conn);

            int don = 0;
            don = Convert.ToInt32(kontrol.ExecuteScalar());
            conn.Close();
            return don;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox1.Visible = false;
            groupBox3.Visible = false;

            griddoldur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            griddoldur2();

        }


        //bu kısım database üzerinde aynı emailden var mı yok mu diye kontrol ediyor varsa hata verir.
       /* int kontrolemail(string email)
        {
            conn.Open();
            SqlCommand kontrol = new SqlCommand("select count(email) from kullanicilar where email='" + email + "'", conn);

            int don = 0;
            don = Convert.ToInt32(kontrol.ExecuteScalar());
            conn.Close();
            return don;
        }*/
        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("LÜTFEN ALANLARIN HEPSİNİ DOLDURUN BOŞ BIRAKMAYIN");
            }
            else
            {
                if (kontrolemail(textBox3.Text) == 0)
                {
                    if (radioButton1.Checked == true)
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand("Insert into Kullanicilar (username,surname,email,pass,telno,adress,cinsiyet)values(@username,@surname,@email,@pass,@telno,@adress,@cinsiyet)", conn);
                        command.Parameters.AddWithValue("@username", textBox1.Text);
                        command.Parameters.AddWithValue("@surname", textBox2.Text);
                        command.Parameters.AddWithValue("@email", textBox3.Text);
                        command.Parameters.AddWithValue("@pass", Md5(textBox4.Text));
                        command.Parameters.AddWithValue("@telno", textBox6.Text);
                        command.Parameters.AddWithValue("@adress", textBox12.Text);
                        command.Parameters.AddWithValue("@cinsiyet", radioButton1.Text);
                        command.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("KAYIT BAŞARILI BİR ŞEKİLDE TAMAMLANDI", "SİSTEM");
                    }

                    else
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand("Insert into Kullanicilar (username,surname,email,pass,telno,adress,cinsiyet)values(@username,@surname,@email,@pass,@telno,@adress,@cinsiyet)", conn);
                        command.Parameters.AddWithValue("@username", textBox1.Text);
                        command.Parameters.AddWithValue("@surname", textBox2.Text);
                        command.Parameters.AddWithValue("@email", textBox3.Text);
                        command.Parameters.AddWithValue("@pass", Md5(textBox4.Text));
                        command.Parameters.AddWithValue("@telno", textBox6.Text);
                        command.Parameters.AddWithValue("@adress", textBox12.Text);
                        command.Parameters.AddWithValue("@cinsiyet", radioButton2.Text);
                        command.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("KAYIT BAŞARILI BİR ŞEKİLDE TAMAMLANDI", "SİSTEM");
                    }
                }
                else
                {
                    MessageBox.Show(textBox3.Text + " BU MAİL ADRESİ KULLANIMDA LÜTFEN FARKLI BİR MAİL ADRESİ DENEYİNİZ!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    

        private void textbox1_enter(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox1_leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textbox2_enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textbox2_leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textbox3_leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textbox3_enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textbox4_leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }

        }

        private void textbox4_enter(object sender, EventArgs e)
        {
            { 
                textBox4.Text = "";
                textBox4.ForeColor = Color.Black;
            }
        }

        private void textbox5_enter(object sender, EventArgs e)
        {
            if (textBox5.Text == " ")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textbox5_leave(object sender, EventArgs e)
        {

            if (textBox5.Text == "")
            {
                textBox5.Text = " ";
                textBox5.ForeColor = Color.Black;
            }
        }

        private void textbox6_leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = " ";
                textBox5.ForeColor = Color.Black;
            }

        }
        private void texbox6_enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.Black;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox12.Text = "";
            radioButton1.Checked=false;
            radioButton2.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
                conn.Open();
                string kayit = "update Kullanicilar set username=@username,surname=@surname,pass=@pass,telno=@telno,adress=@adress where ID=@ID";
                SqlCommand cmd = new SqlCommand(kayit, conn);
                cmd.Parameters.AddWithValue("@username", textBox7.Text);
                cmd.Parameters.AddWithValue("@surname", textBox8.Text);
                cmd.Parameters.AddWithValue("@pass", textBox10.Text);
                cmd.Parameters.AddWithValue("@telno", textBox11.Text);
                cmd.Parameters.AddWithValue("@adress", textBox13.Text);
                cmd.Parameters.AddWithValue("@ID", lblid.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Kullanıcı Bilgileri Güncellendi.");              
        }

        private void button7_Click(object sender, EventArgs e)
        {

            user user = new user(ad.ToString(), soyad.ToString());
            user.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand command = new SqlCommand("delete from kullanicilar where email=@email", conn);
            command.Parameters.AddWithValue("email", dataGridView1.CurrentRow.Cells["email"].Value.ToString());
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Kullanıcı Bilgisi Silindi");
            ds.Tables["kullanicilar"].Clear();
            griddoldur();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            lblid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox11.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox13.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }
    }
    }

