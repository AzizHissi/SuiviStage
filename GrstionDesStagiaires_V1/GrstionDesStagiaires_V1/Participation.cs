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
    public partial class Participation : UserControl
    {
        private string ConnectionString = "Data Source=.;Initial Catalog=SuiviStage;Integrated Security=True";
        DataTable dt;
        BindingSource bd;
        public Participation()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Participation_Load(object sender, EventArgs e)
        {
            ChargerCombos();
            Charger_DataGrid();
        }

        private void Charger_DataGrid()
        {
            try
            {

                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt = new DataTable();
                    bd = new BindingSource();

                    using (SqlCommand Command = new SqlCommand("listeParticipation", cnx))
                    {

                        Command.CommandType = CommandType.StoredProcedure;

                        dt.Load(Command.ExecuteReader());

                       
                        dataGridView1.DataSource = dt;
                        bd.DataSource = dt;
                        comboBox1.DataBindings.Clear();
                        comboBox2.DataBindings.Clear();

                        comboBox1.DataBindings.Add("text", bd, dt.Columns[0].ColumnName);
                        comboBox2.DataBindings.Add("text", bd, dt.Columns[2].ColumnName);
                        bd.MoveFirst();
                        dataGridView1.Rows[bd.Position].Selected = true;

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ChargerCombos()
        {
            try
            {
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt = new DataTable();

                    using (SqlCommand Command = new SqlCommand("AllParticipation", cnx))
                    {
                        
                        Command.CommandType = CommandType.StoredProcedure;
                        dt.Load(Command.ExecuteReader());
                        var Employees = dt.AsEnumerable().Select(row => row["ID_Empoyee"].ToString()).Distinct();
                        foreach (var emp in Employees)
                        {
                            comboBox1.Items.Add(emp);
                        }
                        var Stages = dt.AsEnumerable().Select(row => row["ID_Stage"].ToString()).Distinct();
                        foreach (var stage in Stages)
                        {
                            comboBox2.Items.Add(stage);
                        }
                    
                        comboBox1.SelectedItem = 0;


                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            bd.MoveFirst();
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            dataGridView1.Rows[bd.Position].Selected = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bd.MovePrevious();
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            dataGridView1.Rows[bd.Position].Selected = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bd.MoveNext();
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            dataGridView1.Rows[bd.Position].Selected = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bd.MoveLast();
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            dataGridView1.Rows[bd.Position].Selected = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bd.Position = dataGridView1.CurrentRow.Index;
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            dataGridView1.Rows[bd.Position].Selected = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bd.Position = dataGridView1.CurrentRow.Index;
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            dataGridView1.Rows[bd.Position].Selected = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt = new DataTable();

                    using (SqlCommand Command = new SqlCommand("DeleteParticipation", cnx))
                    {
                        
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add(new SqlParameter("@ide", int.Parse(comboBox1.Text)));
                        Command.Parameters.Add(new SqlParameter("@ids", int.Parse(comboBox2.Text)));
                        if (Command.ExecuteNonQuery() != 0) MessageBox.Show("supprimer avec succès", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      else MessageBox.Show("Participation n'existe pas", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            ChargerCombos();
            Charger_DataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt = new DataTable();

                    using (SqlCommand Command = new SqlCommand("AddPartcipation", cnx))
                    {

                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add(new SqlParameter("@ide", int.Parse(comboBox1.Text)));
                        Command.Parameters.Add(new SqlParameter("@ids", int.Parse(comboBox2.Text)));
                        if (Command.ExecuteNonQuery() != 0) MessageBox.Show("Ajouter avec succès", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else MessageBox.Show("Participation existe deja", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
          ChargerCombos();
            Charger_DataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    }

