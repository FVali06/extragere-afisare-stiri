using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AfisareDateStiri
{
    public partial class Form2 : Form
    {
        public StiriLocaleEntities2 db = new StiriLocaleEntities2();
        private DataGridView dataGridView;

        public Form2(DataGridView dataGridView)
        {
            InitializeComponent();
            this.dataGridView = dataGridView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            importStiri isl = new importStiri();
            isl.is_titlu = textBox1.Text;
            isl.is_categorie = textBox2.Text;
            isl.is_autor = textBox3.Text;
            //isl.is_data = DateTime.Now;
            db.importStiris.Add(isl);
            db.SaveChanges();
            MessageBox.Show("Datele au fost salvate");
            dataGridView.DataSource = db.importStiris.ToList();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
