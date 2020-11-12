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
    public partial class Analyse : UserControl
    {
        private string ConnectionString = "Data Source=.;Initial Catalog=SuiviStage;Integrated Security=True";
        DataTable dt;
        public Analyse()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Analyse_Load(object sender, EventArgs e)
        {
            ChargerCombos();
            



        }

        public void ChargerCombos()
        {
            try
            {

                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt = new DataTable();

                    using (SqlCommand Command = new SqlCommand("ListeDepart", cnx))
                    {
                        SqlDataReader dr;
                        Command.CommandType = CommandType.StoredProcedure;
                        dr = Command.ExecuteReader();
                        while (dr.Read())
                        {
                            comboBox1.Items.Add(dr[0].ToString());
                            comboBox2.Items.Add(dr[1].ToString());
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
        void Charger_DataGrid1()
        {
            try
            {

                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt = new DataTable();

                    using (SqlCommand Command = new SqlCommand("StageRealise", cnx))
                    {
                        
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add(new SqlParameter("@id", int.Parse(comboBox1.Text)));
                        dt.Load(Command.ExecuteReader());
                        
                        Command.Parameters.Clear();
                        dataGridView1.DataSource = dt;
                     
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void CalculFrais()
        {
            try
            {

                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt = new DataTable();

                    using (SqlCommand Command = new SqlCommand("FraisGeneral", cnx))
                    {

                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add(new SqlParameter("@id", int.Parse(comboBox1.Text)));
                        textBox1.Text=Command.ExecuteScalar().ToString();

                        
                     

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Charger_DataGrid1();
            Charger_DataGrid2(0);
            CalculFrais();
            comboBox2.SelectedIndex = comboBox1.SelectedIndex;
           

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox2.SelectedIndex;
            Charger_DataGrid1();
            Charger_DataGrid2(0);
            CalculFrais();


        }
        void Charger_DataGrid2(int pos)
        {
            int id;
            if (dataGridView1.Rows.Count > 0) id = int.Parse(dataGridView1.Rows[pos].Cells["ID_stage"].Value.ToString());
            else id = -1;
            try
            {

                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt = new DataTable();

                    using (SqlCommand Command = new SqlCommand("ListeEmployee", cnx))
                    {

                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add(new SqlParameter("@id_s", id));
                        Command.Parameters.Add(new SqlParameter("@id_d", int.Parse(comboBox1.Text)));
                        dt.Load(Command.ExecuteReader());

                        Command.Parameters.Clear();
                        dataGridView2.DataSource = dt;
                        dataGridView2.Columns[6].Visible = false;
                        
                        float result=0;
                        foreach (DataGridViewRow  row in dataGridView2.Rows) result += float.Parse(row.Cells["Salaire"].Value.ToString());
                        textBox2.Text = result+"";

                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int pos = dataGridView1.CurrentRow.Index;
            Charger_DataGrid2(pos);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int pos = dataGridView1.CurrentRow.Index;
            Charger_DataGrid2(pos);
        }
    }
}
