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
    public partial class Form1 : Form
         
    {
        int btnclk = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Height = button3.Height;
            panel4.Top = button3.Top;
            employee1.BringToFront();
            btnclk = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Height = button4.Height;
            panel4.Top = button4.Top;
            stage1.BringToFront();
            btnclk = 2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel4.Height = button5.Height;
            panel4.Top = button5.Top;
            btnclk = 3;
            participation1.BringToFront();
            participation1.ChargerCombos();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel4.Height = button6.Height;
            panel4.Top = button6.Top;
            analyse1.BringToFront();
            btnclk = 4;
            analyse1.comboBox1.SelectedIndex = 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
          
            
                
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (btnclk == 1)
            {
                try
                {
                    int num = int.Parse(tbsearch.Text);
                    bool verif = false;
                    for (int i = 0; i < employee1.dt1.Rows.Count; i++)
                    {
                        if (num == Int32.Parse(employee1.dt1.Rows[i][0].ToString()))
                        {
                            employee1.BS.Position = i;
                            verif = true;
                            for (int j = 0; j < employee1.dt1.Rows.Count; j++)
                                employee1.dataGridView1.Rows[j].Selected = false;
                            employee1.dataGridView1.Rows[employee1.BS.Position].Selected = true;
                        }

                    }
                    if (!verif) MessageBox.Show("N'existe pas", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                tbsearch.Text = "";
            }
           else  if (btnclk == 2)
            {
                int pos = stage1.listSatge.FindIndex(x => x.id_stage == int.Parse(tbsearch.Text));
                if (pos != -1) stage1.Synchronosier_data(pos);
                else MessageBox.Show("N'existe pas", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            tbsearch.Text = "";
        }
        /*  */
    }
}
