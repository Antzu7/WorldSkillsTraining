using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldSkills.Data;
using WorldSkills.Models;

namespace WorldSkills.Forms.OrderForms
{
    public partial class OrdersForm : Form
    {
        ApplicationDBContext db;
        public OrdersForm()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            AddOrderForm orForm = new AddOrderForm();

            List<Product> products = db.Products.ToList();
            orForm.listBox1.DataSource = products;
            orForm.listBox1.DisplayMember = "Name";
            orForm.listBox1.ValueMember = "Id";

            orForm.comboBox2.DataSource = db.Users.Local.ToBindingList();
            orForm.comboBox2.DisplayMember = "Full_name";
            orForm.comboBox2.ValueMember = "Id";
            
            DialogResult result = orForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;

            Order order = new Order();
            order.UserId = (int)orForm.comboBox2.SelectedValue;
            order.Date = orForm.dateTimePicker1.Value;

            products.Clear();
            foreach (var item in orForm.listBox1.SelectedItems)
            {
                products.Add((Product)item);
            }
            order.Products = products;
            db.Orders.Add(order);
            db.SaveChanges();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            db = new ApplicationDBContext();
            db.Products.Load();
            db.Users.Load();
            db.Orders.Load();

            dataGridView1.DataSource = db.Orders.Local.ToBindingList();
        }
    }
}
