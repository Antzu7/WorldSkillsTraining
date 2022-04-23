using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;
using WorldSkills.Data;
using WorldSkills.Models;

namespace WorldSkills
{
    public partial class RegistrationForm : Form
    {
        ApplicationDBContext db;
        private AutorizationForm auForm;
        public RegistrationForm(AutorizationForm auForm)
        {
            InitializeComponent();
            this.auForm = auForm;
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
        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {          
            auForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            auForm.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (errorLabel != null)
                Controls.Remove(errorLabel);

            string password = textBox2.Text;
            string repeatPassword = textBox3.Text;

            if (password != repeatPassword)
            {
                Point point = new Point() { X = 320, Y = 529 };
                Spawn_Label("Пароли не совпадают", point, 20);
            }           
            else
            {
                //Генерация соли
                byte[] salt1;
                new RNGCryptoServiceProvider().GetBytes(salt1 = new byte[16]);

                //Получение хэша
                var pbkdf2_1 = new Rfc2898DeriveBytes(password, salt1, 10000);
                byte[] hash1 = pbkdf2_1.GetBytes(20);

                //Объединение байтов соли и пароля
                byte[] hashBytes1 = new byte[36];
                Array.Copy(salt1, 0, hashBytes1, 0, 16);
                Array.Copy(hash1, 0, hashBytes1, 16, 20);

                //Конвертация объединенных соли+хэша в строку для хранения
                string savedPasswordHash = Convert.ToBase64String(hashBytes1);

                User user = new User();
                user.Full_name = textBox1.Text;
                user.Email = textBox4.Text;
                user.Phone_number = textBox5.Text;
                user.Age = (int)numericUpDown1.Value;
                user.Password = savedPasswordHash;

                db.Users.Add(user);
                db.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно");
            }
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            db = new ApplicationDBContext();
            db.Users.Load();
        }
    }
}
