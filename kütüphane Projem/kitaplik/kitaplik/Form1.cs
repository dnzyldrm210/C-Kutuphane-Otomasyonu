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

using System.Security.Cryptography;


namespace kitaplik
{
    public partial class Form1 : Form
    {
        SqlConnection conn =new SqlConnection("Data Source=DESKTOP-N457NO6;Initial Catalog=Kitaplik;Integrated Security=True");
        SqlDataReader dr;
        SqlCommand cmd;
        
        public Form1()
        {
            InitializeComponent();
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

            private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "User Name";
            label2.Text = "Password";
            button1.Text = "LOGİN";
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;
            
            conn.Open();
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd = new SqlCommand("Select * from Kullanicilar where email='" + email + "'", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (email == dr["email"].ToString().TrimEnd().TrimStart() && Md5(password) == dr["pass"].ToString().TrimEnd())
                {
                    if (Convert.ToInt32(dr["roller"]) == 1)
                    {
                        MessageBox.Show("Admin girişi başarılı!");
                        admin adminekran = new admin(dr["username"].ToString(), dr["surname"].ToString());
                        adminekran.Show();
                        this.Hide();
                    }
                    else if (email == dr["email"].ToString().TrimEnd().TrimStart() && Md5(password) == dr["pass"].ToString().TrimEnd())
                    {
                        if (Convert.ToInt32(dr["roller"]) == 0)
                        {
                            MessageBox.Show("Kullanıcı girişi başarılı!");
                            user user = new user(dr["username"].ToString(), dr["surname"].ToString());
                            user.Show();
                            this.Hide();
                        }
                    }                 
                    
                }

                 else 
                {
                    MessageBox.Show("Kullanıcı bilgileri hatalı:)");
                }

            }
            else {
                MessageBox.Show("Lütfen Bilgileri Doldurunuz!!!");
            }
            conn.Close();

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
