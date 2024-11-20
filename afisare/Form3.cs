using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AfisareDateStiri
{
    public partial class Form3 : Form
    {
        public StiriLocaleEntities2 db = new StiriLocaleEntities2();
        private DataGridView dataGridView;

        int id_global = 0;
        public Form3(DataGridView dataGridView)
        {
            InitializeComponent();
            this.dataGridView = dataGridView;
            int indexSelectat1 = dataGridView.SelectedCells[0].RowIndex;
            int indexSelectat2 = dataGridView.CurrentCell.RowIndex;

            List<importStiri> lista = db.importStiris.ToList();
            importStiri isl = lista.ElementAt(indexSelectat1);
            id_global = isl.is_id;

            textBox1.Text = isl.is_titlu;
            textBox2.Text = isl.is_categorie;
            textBox3.Text = isl.is_autor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int indexSelectat1 = dataGridView.SelectedCells[0].RowIndex;
            int indexSelectat2 = dataGridView.CurrentCell.RowIndex;
            List<importStiri> lista = db.importStiris.ToList();
            int idInreg = lista.ElementAt(indexSelectat1).is_id;
            importStiri isl = db.importStiris.Where(c => c.is_id == idInreg).First();
            if (isl != null && (isl.is_id == id_global))
            {
                isl.is_titlu = textBox1.Text;
                isl.is_categorie = textBox2.Text;
                isl.is_autor = textBox3.Text;
                db.SaveChanges();
                MessageBox.Show("S-au actualizat datele");
                dataGridView.DataSource = db.importStiris.ToList();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("Selectie gresita");
            }
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int indexSelectat1 = dataGridView.SelectedCells[0].RowIndex;
                importStiri element = db.importStiris.ToList().ElementAt(indexSelectat1);
                db.importStiris.Remove(element);
                db.SaveChanges();
                MessageBox.Show("S-a sters elementul");
                dataGridView.DataSource = db.importStiris.ToList();
            }
            catch
            {
                MessageBox.Show("Nu s-a putut realiza stergerea");
            }
            this.Close();
        }
    }
}
