using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        List<Label> errorLabels = new List<Label>();
        private void Spawn_Label(string text, Point point)
        {           
            
            errorLabel = new Label()
            {
                Text = text,
                Location = point, 
                //TabIndex = index,
                ForeColor = Color.Red,
                Size = new Size(313, 15),
            };
            errorLabels.Add(errorLabel);
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
            if (errorLabels != null)
            {
                for (int i = 0; i < errorLabels.Count; i++)
                {
                    Controls.Remove(errorLabels[i]);
                }          
            }
                  

            string password = textBox2.Text;
            string repeatPassword = textBox3.Text;
            string name = textBox1.Text;
            string email = textBox4.Text;
            string phoneNumber = textBox5.Text;
            int age = (int)numericUpDown1.Value;

            if (password != repeatPassword)
            {
                Point point = new Point() { X = 320, Y = 529 };
                Spawn_Label("Пароли не совпадают", point);
            }           
            else
            {              
                createUser(password,name,email,phoneNumber,age);                 
            }
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            db = new ApplicationDBContext();
            db.Users.Load();
        }

        private void createUser(string password, string name, string email, string phoneNumber, int age)
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
            user.Password = password;
            user.Full_name = name;
            user.Email = email;
            user.Phone_number = phoneNumber;
            user.Age = age;

            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(user, context, results, true))
            {                
                foreach (var error in results)
                {
                    if (error.ErrorMessage == "Введите Email")
                    {
                        Point p = new Point() { X = 12, Y = 263 };
                        Spawn_Label(error.ErrorMessage, p);
                    }
                    if (error.ErrorMessage == "Введите пароль")
                    {
                        Point p = new Point() { X = 12, Y = 533 };
                        Spawn_Label(error.ErrorMessage, p);
                    }
                    if (error.ErrorMessage == "Пароль должен содержать как минимум 6 символов")
                    {
                        Point p = new Point() { X = 12, Y = 533 };
                        Spawn_Label(error.ErrorMessage, p);
                    }
                    if (error.ErrorMessage == "Введите полное имя")
                    {
                        Point p = new Point() { X = 12, Y = 173 };
                        Spawn_Label(error.ErrorMessage, p);
                    }
                    if (error.ErrorMessage == "Введите возраст")
                    {
                        Point p = new Point() { X = 12, Y = 439 };
                        Spawn_Label(error.ErrorMessage, p);
                    }
                    if (error.ErrorMessage == "Введите номер телефона")
                    {
                        Point p = new Point() { X = 12, Y = 349 };
                        Spawn_Label(error.ErrorMessage, p);
                    }
                    if (error.ErrorMessage == "Не валидный номер телефона")
                    {
                        Point p = new Point() { X = 12, Y = 349 };
                        Spawn_Label(error.ErrorMessage, p);
                    }                    
                }
            }
            else
            {
                user.Password = savedPasswordHash;
                db.Users.Add(user);
                db.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно");

                auForm.Show();
                Hide();
            }
        }
    }
}
