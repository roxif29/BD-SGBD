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

namespace Laborator1SGBD
{

    public partial class Form1 : Form
    {
        public string text;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonParinte_Click(object sender, EventArgs e)
        {
         //Aici deschidem conexiunea 
            SqlConnection cs = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
            cs.Open();
            //Selectam id-ul si numele jocului din tabela Jocuri
            SqlCommand selCmd = new SqlCommand("SELECT Jid,NumeJ FROM Jocuri", cs);
            SqlDataReader reader = selCmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Jid";
            dataGridView1.Columns[1].Name = "NumeJ";

            //Adaugare date in DGV
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    DataGridViewRow tempRow = new DataGridViewRow();

                    DataGridViewCell cellID = new DataGridViewTextBoxCell();
                    cellID.Value = reader.GetInt32(0);
                    tempRow.Cells.Add(cellID);

                    DataGridViewCell cellJoc = new DataGridViewTextBoxCell();
                    cellJoc.Value = reader.GetString(1);
                    tempRow.Cells.Add(cellJoc);

                    dataGridView1.Rows.Add(tempRow);
                }
            }

            reader.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            int rid = 0;

            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                rid = Convert.ToInt32(selectedRow.Cells[0].Value);

            }

            // Afisarea datelor unui fiu atunci cand este selectata o inregistrare din parinte. Cand apasam pe celula NumeJ a parintelui
            //vor aparea recenziile jocului aferent
            SqlConnection cs = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
            cs.Open();
            string commandText = "SELECT Rid, Text FROM Recenzii WHERE Rid = @Rid ";
            SqlCommand selCmd = new SqlCommand(commandText, cs);
            selCmd.Parameters.Add("@Rid", SqlDbType.Int);
            selCmd.Parameters["@Rid"].Value = rid;

            SqlDataReader reader = selCmd.ExecuteReader();

            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnCount = 2;
            dataGridView2.Columns[0].Name = "Rid";
            dataGridView2.Columns[1].Name = "Text";


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataGridViewRow tempRow = new DataGridViewRow();

                    DataGridViewCell cellID = new DataGridViewTextBoxCell();
                    cellID.Value = reader.GetInt32(0);
                    tempRow.Cells.Add(cellID);

                    DataGridViewCell cellNameRecenzie= new DataGridViewTextBoxCell();
                    cellNameRecenzie.Value = reader.GetString(1);
                    tempRow.Cells.Add(cellNameRecenzie);

                    dataGridView2.Rows.Add(tempRow);
                }
            }


            reader.Close();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int rid = 0;
        
            if (dataGridView2.SelectedCells.Count > 0)
            {
             
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];

                rid = Convert.ToInt32(selectedRow.Cells[0].Value);

            }
           


            if (e.Control is TextBox) 
            {
                DataGridViewTextBoxEditingControl cell = (DataGridViewTextBoxEditingControl)e.Control;

                if (cell != null)
                {
                    //Stergerea unei recenzii se va realiza stergand textul recenziei si apasand butonul de update
                    cell.KeyDown += delegate (object s, KeyEventArgs ekp)
                    {
                        if (ekp.KeyData == Keys.Delete)
                        {
                            dataGridView2.Rows.RemoveAt(cell.EditingControlRowIndex);
                            SqlConnection cs = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
                            cs.Open();
                           
                            string sqlQuery = String.Format("delete from Recenzii where Rid = {0}", rid);

                            SqlCommand command = new SqlCommand(sqlQuery, cs);

                            int rowsDeletedCount = command.ExecuteNonQuery();
                            if (rowsDeletedCount != 0)
                                MessageBox.Show("Sters");
                            
                            command.Dispose();

                            
                        }
                    };
                }
            }


        }



        private void buttonAdauga_Click(object sender, EventArgs e)
        {

            //La adaugare se va lua din tabela Recenzii id-ul maxim existent (pt recenzii nu jocuri!) si se va incrementa, iar apoi se va adauga o 
            //recenzie avand id-ul (id maxim din tabela Recenzii+1)
            string text = "";
            Add otherForm = new Add(this);
            otherForm.ShowDialog();

            int jid = 0;
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                jid = Convert.ToInt32(selectedRow.Cells[0].Value);

            }
            int rid = 0;


            SqlConnection cs = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
            cs.Open();
            string query = "SELECT MAX(Rid) FROM Recenzii";
            SqlCommand comSelect = new SqlCommand(query, cs);
            SqlDataReader reader = comSelect.ExecuteReader();
            while (reader.Read())
                rid = Convert.ToInt32(reader[0]);

            rid++; 
            reader.Close();

            DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[0].Clone();
            row.Cells[0].Value = rid;
            row.Cells[1].Value = otherForm.TextBox1.Text;
            dataGridView2.Rows.Add(row);
            text = otherForm.TextBox1.Text;
            
            otherForm.Close();

            string sqlQuery = String.Format("SET IDENTITY_INSERT Recenzii ON;" +
                                            "Insert into Recenzii (Rid, Text, Jid) Values('{0}', '{1}', {2});" +
                                            "SET IDENTITY_INSERT Recenzii OFF;"
                                            , rid, text, jid);
            
            SqlConnection cons = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
            cons.Open();

            SqlCommand command = new SqlCommand(sqlQuery, cons);
            int rowsAddedCount = command.ExecuteNonQuery();
            if (rowsAddedCount != 0)
                MessageBox.Show("Adaugat");
            command.Dispose();

        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            //Pt update se va schimba textul recenziei si se va apasa butonul update si va aparea un messagebox in 
            //care vei fi intrebat daca esti sigur ca vrei sa updatezi datele
            SqlConnection cs = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
            int rid = 0;
            string text = "";

            if (cs.State != ConnectionState.Open)
            {
                cs.Open();
            }

            if (dataGridView2.SelectedCells.Count > 0)
            {
               
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];

                rid = Convert.ToInt32(selectedRow.Cells[0].Value);
                text = Convert.ToString(selectedRow.Cells[1].Value);

            }

            for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
            {
                DialogResult result;
                result = MessageBox.Show("Esti sigur ca doresti sa modifici datele?", "Confirmare", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
         
                    SqlCommand command = new SqlCommand("UPDATE Recenzii SET Text = @text Where Rid = @rid", cs);

                    command.Parameters.AddWithValue("@text", text);
                    command.Parameters.AddWithValue("@rid", rid);
                    MessageBox.Show("Updatat");
                    command.ExecuteNonQuery();
                    
                }
            }


        }


    }
}