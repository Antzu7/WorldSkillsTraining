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

namespace WorldSkills.ProductForms
{
    public partial class AddProductForm : Form
    {
        ApplicationDBContext db;
        public AddProductForm()
        {
            InitializeComponent();
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            db = new ApplicationDBContext();
            db.Categories.Load();

            comboBox1.DataSource = db.Categories.Local.ToBindingList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
        }
    }
}
