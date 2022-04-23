using System;
using System.Data.Entity;
using System.Windows.Forms;
using WorldSkills.Data;
using WorldSkills.Models;

namespace WorldSkills.CategoryForms
{
    public partial class CategorysForm : Form
    {
        ApplicationDBContext db;
        public CategorysForm()
        {
            InitializeComponent();
        }

        private void CategorysForm_Load(object sender, EventArgs e)
        {
            db = new ApplicationDBContext();
            db.Categories.Load();

            dataGridView1.DataSource = db.Categories.Local.ToBindingList();
        }

        //добавление
        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddCategoryForm caForm = new AddCategoryForm();
            DialogResult result = caForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            Category category = new Category();
            category.Name = caForm.textBox1.Text;

            db.Categories.Add(category);
            db.SaveChanges();

            MessageBox.Show("Новый объект добавлен");
        }

        //редактирование
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Category category = db.Categories.Find(id);

                AddCategoryForm caForm = new AddCategoryForm();

                caForm.textBox1.Text = category.Name;

                DialogResult result = caForm.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;

                category.Name = caForm.textBox1.Text;

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

                Category category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();

                MessageBox.Show("Объект удален");
            }
        }
    }
}
