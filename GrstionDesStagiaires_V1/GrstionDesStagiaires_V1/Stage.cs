using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GrstionDesStagiaires_V1
{
    public partial class Stage : UserControl
    {
        private string ConnectionString = "Data Source=.;Initial Catalog=SuiviStage;Integrated Security=True";
        public List<CStage> listSatge = new List<CStage>();
        int Position = 0;
        public Stage()
        {
            InitializeComponent();
        }

        private void Stage_Load(object sender, EventArgs e)
        {
            ChargerDataGrid();

        }

        private void ChargerDataGrid()
        {
            try
            {

                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();

                    listSatge.Clear();
                    dataGridView1.Rows.Clear();
                    using (SqlCommand Command = new SqlCommand("AllStage", cnx))
                    {
                        SqlDataReader dr;
                        Command.CommandType = CommandType.StoredProcedure;
                        dr =  Command.ExecuteReader();
                        while (dr.Read())
                        {
                            dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4]);
                            listSatge.Add(new CStage(int.Parse(dr[0].ToString()), dr[1].ToString(), DateTime.Parse(dr[2].ToString()), DateTime.Parse(dr[3].ToString()), float.Parse(dr[4].ToString())));
                        }
                       
                     
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Position = 0;
            Synchronosier_data(Position);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Position = 0;
            Synchronosier_data(Position);

        }
        public void Synchronosier_data(int pos)
        {
            textBox1.Text = listSatge[pos].id_stage.ToString();
            textBox2.Text = listSatge[pos].nom_stage;
            dateTimePicker1.Value = listSatge[pos].date_debut;
            dateTimePicker2.Value = listSatge[pos].date_fin;
            textBox3.Text =listSatge[pos].frais.ToString();
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            dataGridView1.Rows[pos].Selected = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           if (Position!=0) Synchronosier_data(--Position);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (Position < listSatge.Count-1 ) Synchronosier_data(++Position);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Position = listSatge.Count-1;
            Synchronosier_data(Position);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Position = dataGridView1.CurrentRow.Index;
            Synchronosier_data(Position);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Position = dataGridView1.CurrentRow.Index;
            Synchronosier_data(Position);

        }
        public void VIDER(Control f)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            foreach (Control ct in f.Controls)
            {
                if (ct.GetType() == typeof(TextBox))
                {
                    ct.Text = "";
                }
                if (ct.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)ct).SelectedIndex = 0;
                }
                if (ct.Controls.Count != 0)
                {
                    VIDER(ct);
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            VIDER(this);
            
        }
        public bool Check(Control f)
        {
            bool verif = true;
            foreach (Control ct in f.Controls)
            {
                if (ct.Text == "") verif = false;
                if (ct.GetType() == typeof(PictureBox) && ((PictureBox)ct).ImageLocation == "") verif = false;

                if (ct.Controls.Count != 0)
                {
                    verif = Check(ct);
                }

            }
            return verif;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (Check(this))
            { 
            try
            {
                if (listSatge.FindIndex(x => x.id_stage == int.Parse(textBox1.Text)) == -1)
                {
                    using (SqlConnection cnx = new SqlConnection(ConnectionString))
                    {
                        cnx.Open();
                        using (SqlCommand cmd = new SqlCommand("AddStage", cnx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@id", int.Parse(textBox1.Text)));
                            cmd.Parameters.Add(new SqlParameter("@Nom", textBox2.Text));
                            cmd.Parameters.Add(new SqlParameter("@debut", dateTimePicker1.Value));
                            cmd.Parameters.Add(new SqlParameter("@fin", dateTimePicker2.Value));
                            cmd.Parameters.Add(new SqlParameter("@frais", float.Parse(textBox3.Text)));

                            if (cmd.ExecuteNonQuery() != 0) MessageBox.Show("Sauvegarder aver success", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmd.Parameters.Clear();
                            ChargerDataGrid();
                        }

                    }
                }
                else MessageBox.Show("Ce stage existe deja", "information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
            else MessageBox.Show("Veuillez remplir tous les champs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    if (listSatge.FindIndex(x => x.id_stage == int.Parse(textBox1.Text)) != -1)
                    {
                        using (SqlConnection cnx = new SqlConnection(ConnectionString))
                        {
                            cnx.Open();
                            using (SqlCommand cmd = new SqlCommand("DeleteSatge", cnx))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("@id", int.Parse(textBox1.Text)));


                                if (cmd.ExecuteNonQuery() != 0) MessageBox.Show("Supprimmer avec  success", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmd.Parameters.Clear();
                                ChargerDataGrid();
                            }

                        }
                    }
                    else MessageBox.Show("Ce stage n'existe pas", "information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else MessageBox.Show("Veuillez enter un id", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Check(this))
            {
                try
            {
                if (listSatge.FindIndex(x => x.id_stage == int.Parse(textBox1.Text)) != -1)
                {
                    using (SqlConnection cnx = new SqlConnection(ConnectionString))
                    {
                        cnx.Open();
                        using (SqlCommand cmd = new SqlCommand("updateStage", cnx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@id", int.Parse(textBox1.Text)));
                            cmd.Parameters.Add(new SqlParameter("@Nom", textBox2.Text));
                            cmd.Parameters.Add(new SqlParameter("@debut", dateTimePicker1.Value));
                            cmd.Parameters.Add(new SqlParameter("@fin", dateTimePicker2.Value));
                            cmd.Parameters.Add(new SqlParameter("@frais", float.Parse(textBox3.Text)));

                            if (cmd.ExecuteNonQuery() != 0) MessageBox.Show("Modifier avec success", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmd.Parameters.Clear();
                            ChargerDataGrid();
                        }

                    }
                }
                else MessageBox.Show("Ce stage n'existe pas", "information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            else MessageBox.Show("Veuillez remplir tous les champs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
