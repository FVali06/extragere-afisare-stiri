using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AfisareDateStiri
{
    public partial class Form1 : Form
    {
        public StiriLocaleEntities2 db = new StiriLocaleEntities2();
        public BindingList<importStiri> listaStiri;

        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.Add("ID Ascendent");
            comboBox1.Items.Add("ID Descendent");
            comboBox1.Items.Add("Alfabetic Ascendent");
            comboBox1.Items.Add("Alfabetic Descendent");
            comboBox1.SelectedIndex = 0;

            comboBox1.SelectedIndexChanged += comboBoxSortare_SelectedIndexChanged;
        }

        private void comboBoxSortare_SelectedIndexChanged(object sender, EventArgs e)
        {
            string optiuneSelectata = comboBox1.SelectedItem.ToString();

            var stiriFiltrate = (BindingList<importStiri>)dataGridView1.DataSource;  // Obține lista curentă de stiri

            if (optiuneSelectata == "ID Ascendent")
            {
                listaStiri = new BindingList<importStiri>(stiriFiltrate.OrderBy(stiri => stiri.is_id).ToList());
            }
            else if (optiuneSelectata == "ID Descendent")
            {
                listaStiri = new BindingList<importStiri>(stiriFiltrate.OrderByDescending(stiri => stiri.is_id).ToList());
            }
            else if (optiuneSelectata == "Alfabetic Ascendent")
            {
                listaStiri = new BindingList<importStiri>(stiriFiltrate.OrderBy(stiri => stiri.is_titlu).ToList());
            }
            else if (optiuneSelectata == "Alfabetic Descendent")
            {
                listaStiri = new BindingList<importStiri>(stiriFiltrate.OrderByDescending(stiri => stiri.is_titlu).ToList());
            }

            dataGridView1.DataSource = listaStiri;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listaStiri = new BindingList<importStiri>(db.importStiris.ToList());
            dataGridView1.DataSource = listaStiri;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FiltrareSiSortare();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(dataGridView1);
            if (f2.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.DataSource = db.importStiris.ToList();
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form3 f3 = new Form3(dataGridView1);
            if (f3.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.DataSource = db.importStiris.ToList();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            FiltrareSiSortare();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            FiltrareSiSortare();
        }

        private void FiltrareSiSortare()
        {
            var stiriFiltrate = db.importStiris.ToList(); // Obține toate stirile

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                stiriFiltrate = stiriFiltrate.Where(i => i.is_categorie.Contains(textBox1.Text)).ToList(); // Aplică filtrarea după categorie
            }

            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                stiriFiltrate = stiriFiltrate.Where(i => i.is_autor.Contains(textBox2.Text)).ToList(); // Aplică filtrarea după autor
            }

            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                stiriFiltrate = stiriFiltrate.Where(i => i.is_data.Contains(textBox3.Text)).ToList(); // Aplică filtrarea după data
            }

            comboBoxSortare_SelectedIndexChanged(null, EventArgs.Empty); // Aplică sortarea

            dataGridView1.DataSource = new BindingList<importStiri>(stiriFiltrate);
        }
    }
}