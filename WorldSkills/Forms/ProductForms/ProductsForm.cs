using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Windows.Forms;
using WorldSkills.CategoryForms;
using WorldSkills.Data;
using WorldSkills.Models;
using WorldSkills.ProductForms;

namespace WorldSkills
{
    public partial class ProductsForm : Form
    {
        ApplicationDBContext db;
        private AutorizationForm auForm;        
        public ProductsForm(AutorizationForm auForm)
        {
            InitializeComponent();
            this.auForm = auForm;
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            db = new ApplicationDBContext();
            db.Products.Load();

            dataGridView1.DataSource = db.Products.Local.ToBindingList();
        }

        // добавление
        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddProductForm prForm = new AddProductForm();
            DialogResult result = prForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Product product = new Product();
            product.Name = prForm.textBox1.Text;
            product.Category_Id = (int)prForm.comboBox1.SelectedValue;
            product.Price = (int)prForm.numericUpDown1.Value;
            product.IsAvailable = prForm.checkBox1.Checked;

            db.Products.Add(product);
            db.SaveChanges();

            MessageBox.Show("Новый объект добавлен");
        }

        // редактирование
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Product product = db.Products.Find(id);

                AddProductForm prForm = new AddProductForm();

                prForm.textBox1.Text = product.Name;              
                prForm.numericUpDown1.Value = product.Price;
                prForm.checkBox1.Checked = product.IsAvailable;

                DialogResult result = prForm.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                product.Name = prForm.textBox1.Text;
                product.Category_Id = (int)prForm.comboBox1.SelectedValue;
                product.Price = (int)prForm.numericUpDown1.Value;
                product.IsAvailable = prForm.checkBox1.Checked;

                db.SaveChanges();
                dataGridView1.Refresh(); // обновляем грид
                MessageBox.Show("Объект обновлен");

            }
        }

        // удаление
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }

        private void ProductsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            auForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CategorysForm catForm = new CategorysForm();
            catForm.ShowDialog();
        }
    }
}
