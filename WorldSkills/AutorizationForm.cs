using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using WorldSkills.Data;
using WorldSkills.Models;
using WorldSkills.ProductForms;

namespace WorldSkills
{
    public partial class AutorizationForm : Form
    {
        ApplicationDBContext db;
        public AutorizationForm()
        {
            InitializeComponent();
        }

        Label errorLabel = new Label();
        private void Spawn_Label(string text, Point point, int tabIndex)
        {
            errorLabel = new Label()
            {
                Text = text,
                Location = point,
                TabIndex = tabIndex,
                ForeColor = Color.Red,
                Size = new Size(135, 15),
            };
            Controls.Add(errorLabel);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationForm regForm = new RegistrationForm(this);           
            regForm.Show();
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (errorLabel != null)
                Controls.Remove(errorLabel);

            string email = textBox1.Text;
            string password = textBox2.Text;

            User user = db.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                string savedPasswordHash = user.Password;
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);

                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                    { 
                        Point p = new Point() { X = 12, Y = 261 };
                        Spawn_Label("Неправильный пароль", p, 8);
                        return;
                    }
                ProductsForm prForm = new ProductsForm(this);
                prForm.Show();
                Hide();
            }
            else
            { 
                Point point = new Point() { X = 12, Y = 171};
                Spawn_Label("Email не найден", point, 7);
            }
        }
        
        private void AutorizationForm_Load(object sender, EventArgs e)
        {
            db = new ApplicationDBContext();
            db.Users.Load();
        }
    }
}
