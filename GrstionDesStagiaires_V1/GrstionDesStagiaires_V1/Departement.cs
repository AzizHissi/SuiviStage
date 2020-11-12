using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrstionDesStagiaires_V1
{
    public partial class Departement : Form
    {
        public Departement()
        {
            InitializeComponent();
        }

        private void departementBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.departementBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.suiviStageDataSet);
        
            


        }

        private void Departement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'suiviStageDataSet.Departement' table. You can move, or remove it, as needed.
            this.departementTableAdapter.Fill(this.suiviStageDataSet.Departement);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Departement_Leave(object sender, EventArgs e)
        {
          


        }
    }
}
