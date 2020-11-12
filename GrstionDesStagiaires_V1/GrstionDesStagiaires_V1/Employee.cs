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
using System.IO;
using Microsoft.VisualBasic;

namespace GrstionDesStagiaires_V1
{
    public partial class Employee : UserControl
    {
        private string ConnectionString = "Data Source=.;Initial Catalog=SuiviStage;Integrated Security=True";
        public DataTable dt1 = new DataTable();
        DataTable dt = new DataTable();
        public BindingSource BS;
        Byte[] img;

        public Employee()
        {
            InitializeComponent();
        }
        private void Employee_Load(object sender, EventArgs e)
        {
            ChargerComboBox();

            Synchroniser_Data();
            Charger_photo();






        }

        public void ChargerComboBox()
        {
            
            dt = new DataTable();
            try
            {

                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt.Clear();

                    using (SqlCommand Command = new SqlCommand("ListeDepart", cnx))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        dt.Load(Command.ExecuteReader());
                        Command.Parameters.Clear();
                        comboBox1.DataSource = dt;
                        comboBox1.DisplayMember = "Nom_Departement";
                        comboBox1.ValueMember = "ID_Departemment";
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        public void Charger_photo()
        {
           

            try
            {
                if (!(dt1.Rows[BS.Position][6] is System.DBNull))
                {
                    img = (Byte[])dt1.Rows[BS.Position][6];

                    MemoryStream MS = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(MS);
                }
                else
                    pictureBox1.Image = null;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void Synchroniser_Data()
        {
            BS = new BindingSource();

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    dt1.Clear();
                    using (SqlCommand cmd = new SqlCommand("ListeEmploye", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        dt1.Load(cmd.ExecuteReader());
                        dataGridView1.DataSource = dt1;
                        BS.DataSource = dt1;

                    }
                }
                textBox1.DataBindings.Clear();
                textBox2.DataBindings.Clear();
                textBox3.DataBindings.Clear();
                comboBox1.DataBindings.Clear();
                comboBox2.DataBindings.Clear();
                comboBox3.DataBindings.Clear();

                textBox1.DataBindings.Add("Text", BS, dt1.Columns[0].ColumnName);
                comboBox1.DataBindings.Add("Text", BS, dt1.Columns[1].ColumnName);
                textBox2.DataBindings.Add("Text", BS, dt1.Columns[2].ColumnName);
                textBox3.DataBindings.Add("Text", BS, dt1.Columns[3].ColumnName);
                comboBox2.DataBindings.Add("Text", BS, dt1.Columns[5].ColumnName);
                comboBox3.DataBindings.Add("Text", BS, dt1.Columns[4].ColumnName);
                BS.MoveFirst();
                dataGridView1.Rows[BS.Position].Selected = true;
                dataGridView1.Columns[6].Visible = false;
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BS.Position = dataGridView1.CurrentRow.Index;
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            dataGridView1.Rows[BS.Position].Selected = true;
            Charger_photo();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BS.MoveFirst();
            DatagridSelectedRow(BS.Position);
            Charger_photo();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BS.MovePrevious();
            DatagridSelectedRow(BS.Position);
            Charger_photo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BS.MoveNext();
            DatagridSelectedRow(BS.Position);
            Charger_photo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BS.MoveLast();
            DatagridSelectedRow(BS.Position);
            Charger_photo();


        }

        public void DatagridSelectedRow(int pos)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows) row.Selected = false;
            dataGridView1.Rows[pos].Selected = true;
            Charger_photo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChargerImage();

        }

        private void ChargerImage()
        {
            OpenFileDialog FileIma = new OpenFileDialog();
            FileIma.Title = "Choisir une photo ";
            FileIma.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|JPEG Files (*.jpeg)|*.jpeg";
            if (FileIma.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = FileIma.FileName.ToString();
                button6.Text = "Modifier";

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (pictureBox1.ImageLocation != null  || pictureBox1.Image==null)
                if (MessageBox.Show("Voulez vous vraimant suprimer image", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {

                    pictureBox1.ImageLocation = "";
                    button6.Text = "Ajouter";
                }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ChargerImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VIDER(this);
            pictureBox1.ImageLocation = "";
            pictureBox1.Image = null;
            
        }
        public void VIDER(Control f)
        {
            pictureBox1.ImageLocation = "";
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
                DatagridSelectedRow(0);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Check(this))
            {
                try
                {
                    FileStream fs = new FileStream(pictureBox1.ImageLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img = br.ReadBytes((int)fs.Length);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                try
                {
                    using (SqlConnection cnx = new SqlConnection(ConnectionString))
                    {
                        cnx.Open();
                        using (SqlCommand cmd = new SqlCommand("Sauvegarde", cnx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@id_e", int.Parse(textBox1.Text)));
                            cmd.Parameters.Add(new SqlParameter("@id_t", int.Parse(comboBox1.SelectedValue.ToString())));
                            cmd.Parameters.Add(new SqlParameter("@nom", textBox2.Text));
                            cmd.Parameters.Add(new SqlParameter("@Prenom", textBox3.Text));
                            cmd.Parameters.Add(new SqlParameter("@grade", comboBox3.Text));
                            cmd.Parameters.Add(new SqlParameter("@sexe", comboBox2.Text));
                            cmd.Parameters.Add(new SqlParameter("@photo", img));

                            cmd.ExecuteNonQuery();

                        }

                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Synchroniser_Data();
            }
            else MessageBox.Show("Veuillez remplir tous les champs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            { 
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionString))
                {
                    cnx.Open();
                    using (SqlCommand cmd = new SqlCommand("SuprimeE", cnx))
                    {
                        if (MessageBox.Show("Voulez-vous vraiment supprimer ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@id_e", Int32.Parse(textBox1.Text)));
                            cmd.ExecuteNonQuery();

                        }



                    }

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Synchroniser_Data();
            }
            else MessageBox.Show("Veuillez enter un id", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void button9_Click(object sender, EventArgs e)
        {
            if (Check(this))
            { 
            if (MessageBox.Show("Voulez-vous vraiment Modifier ?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (pictureBox1.ImageLocation != null)
                {
                    try
                    {
                        FileStream fs = new FileStream(pictureBox1.ImageLocation, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }



                try
                {
                    using (SqlConnection cnx = new SqlConnection(ConnectionString))
                    {
                        cnx.Open();
                        using (SqlCommand cmd = new SqlCommand("ModifieE", cnx))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@id_e", Int32.Parse(textBox1.Text)));
                            cmd.Parameters.Add(new SqlParameter("@id_t", Int32.Parse(comboBox1.SelectedValue.ToString())));
                            cmd.Parameters.Add(new SqlParameter("@nom", textBox2.Text));
                            cmd.Parameters.Add(new SqlParameter("@Prenom", textBox3.Text));
                            cmd.Parameters.Add(new SqlParameter("@grade", comboBox3.Text));
                            cmd.Parameters.Add(new SqlParameter("@sexe", comboBox2.Text));
                            cmd.Parameters.Add(new SqlParameter("@photo", img));

                            cmd.ExecuteNonQuery();

                        }

                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Synchroniser_Data();
                Charger_photo();

            }
            }
            else MessageBox.Show("Veuillez remplir tous les champs", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int num = Int32.Parse(Interaction.InputBox("entrer l'id : ", "Gestion des stagiaire", "", -1, -1));

            //    for (int i = 0; i < dt1.Rows.Count; i++)
            //    {
            //        if (num == Int32.Parse(dt1.Rows[i][0].ToString()))
            //        {
            //            BS.Position = i;

            //            for (int j = 0; j < dt1.Rows.Count; j++)
            //                dataGridView1.Rows[j].Selected = false;
            //            dataGridView1.Rows[BS.Position].Selected = true;
            //        }

            //    }


            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Departement d1 = new Departement();
            d1.ShowDialog();
           
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            ChargerComboBox();
        }

        private void groupBox1_Enter_2(object sender, EventArgs e)
        {

        }
    }
}


