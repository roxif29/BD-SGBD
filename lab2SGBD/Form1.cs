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
using System.Configuration;            
namespace Laborator1SGBD
{
/// <summary>
/// AM O PROBLEMA LA CONEXIUNI!!!!!!!!!!
/// </summary>
    public partial class Form1 : Form
    {
        public string text;
        public string ChildTableName =ConfigurationManager.AppSettings["ChildTableName"];
        public string ChildColumnNames =
        ConfigurationManager.AppSettings["ChildColumnNames"];
        public string ColumnNamesInsertParameters =
        ConfigurationManager.AppSettings["ColumnNamesInsertParameters"];
        public List<string> ParentColumnNamesList = new List<string>(ConfigurationManager.AppSettings["ParentColumnNames"].Split(','));
        public List<string> ChildColumnNamesList = new List<string>(ConfigurationManager.AppSettings["ChildColumnNames"].Split(','));



        public List<string> ChildUpdateList = new List<string>(ConfigurationManager.AppSettings["ColumnNamesInsertParametersChild"].Split(','));
        
        //string nrColP = ConfigurationManager.AppSettings["ParentNumberOfColumns"];
        public int nrColP = Int32.Parse(ConfigurationManager.AppSettings["ParentNumberOfColumns"]);
        public int nrColC = Int32.Parse(ConfigurationManager.AppSettings["ChildNumberOfColumns"]);

        public string pkParent = ConfigurationManager.AppSettings["pkP"];
        public string pkChild = ConfigurationManager.AppSettings["pkC"];


        public string delQuery = ConfigurationManager.AppSettings["DeleteQuery"];
        public string upQuery = ConfigurationManager.AppSettings["UpdateQuery"];
        public string addQuery = ConfigurationManager.AppSettings["InsertQuery"];


        public string selMax = ConfigurationManager.AppSettings["selectMaxKey"];

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonParinte_Click(object sender, EventArgs e)
        {
            //Aici deschidem conexiunea 
            SqlConnection cs = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
            //string con = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            //SqlConnection cs = new SqlConnection(con);
       
            cs.Open();
            //Selectam id-ul si numele jocului din tabela Jocuri
            //SqlCommand selCmd = new SqlCommand("SELECT Jid,NumeJ FROM Jocuri", cs);
            string select = ConfigurationManager.AppSettings["selectP"];            SqlCommand selCmd = new SqlCommand(select, cs);
            SqlDataReader reader = selCmd.ExecuteReader();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = nrColP;
            for(int i=0;i<nrColP;i++)
            {
                dataGridView1.Columns[i].Name = ParentColumnNamesList[i];
            }
           

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
            
            int pkCh = 0;

            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                pkCh = Convert.ToInt32(selectedRow.Cells[0].Value);

            }

            // Afisarea datelor unui fiu atunci cand este selectata o inregistrare din parinte. Cand apasam pe celula NumeJ a parintelui
            //vor aparea Recenzii1le jocului aferent
            SqlConnection cs = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
            //string con = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            //SqlConnection cs = new SqlConnection(con);
            cs.Open();
            //string commandText = "SELECT Rid, Text FROM Recenzii1 WHERE Rid = @Rid ";
            string commandText = ConfigurationManager.AppSettings["selectDGV2"];
            SqlCommand selCmd = new SqlCommand(commandText, cs);
            /*selCmd.Parameters.Add("@Rid", SqlDbType.Int);
            selCmd.Parameters["@Rid"].Value = rid;*/

            selCmd.Parameters.Add(pkChild, SqlDbType.Int);
            selCmd.Parameters[pkChild].Value = pkCh;//pkCh=primary key child
            SqlDataReader reader = selCmd.ExecuteReader();

            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnCount = nrColC;// nr col child
            for (int i = 0; i < nrColP; i++)
            {
                dataGridView2.Columns[i].Name = ChildColumnNamesList[i];
            }
           /* dataGridView2.ColumnCount = 2;
            dataGridView2.Columns[0].Name = "Rid";
            dataGridView2.Columns[1].Name = "Text";*/


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
            int pkCh = 0;//pk child
        
            if (dataGridView2.SelectedCells.Count > 0)
            {
             
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];

                pkCh = Convert.ToInt32(selectedRow.Cells[0].Value);

            }
           


            if (e.Control is TextBox) 
            {
                DataGridViewTextBoxEditingControl cell = (DataGridViewTextBoxEditingControl)e.Control;

                if (cell != null)
                {
                    //Stergerea unei recenzii se va realiza stergand textul recenziei si apasand butonul de 

                    cell.KeyDown += delegate (object s, KeyEventArgs ekp)
                    {
                        if (ekp.KeyData == Keys.Delete)
                        {
                            dataGridView2.Rows.RemoveAt(cell.EditingControlRowIndex);
                            SqlConnection cs = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
                            //string con = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
                            //SqlConnection cs = new SqlConnection(con);
                            cs.Open();

                            string sqlQuery = String.Format(delQuery, pkCh);
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

            int pkPar = 0;//pk parent
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                pkPar = Convert.ToInt32(selectedRow.Cells[0].Value);

            }
            int pkCh = 0;


            SqlConnection cs = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
            //string con = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            //SqlConnection cs = new SqlConnection(con);
            cs.Open();
            string query = selMax;//aici se selecteaza cheia maxima
            SqlCommand comSelect = new SqlCommand(query, cs);
            SqlDataReader reader = comSelect.ExecuteReader();
            while (reader.Read())
                pkCh = Convert.ToInt32(reader[0]);

            pkCh++; 
            reader.Close();

            DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[0].Clone();
            row.Cells[0].Value = pkCh;
            row.Cells[1].Value = otherForm.TextBox1.Text;
            dataGridView2.Rows.Add(row);
            text = otherForm.TextBox1.Text;
            
            otherForm.Close();

            SqlDataAdapter da = new SqlDataAdapter();
            string quote = "\"";
            string sqlQuery = String.Format("SET IDENTITY_INSERT "+quote +  ChildTableName  + quote+ " ON;" +
                                            addQuery, pkCh, text, pkPar);
            
            SqlConnection cons = new SqlConnection("Data Source=DESKTOP-94DIQFF\\SQLEXPRESS; Initial Catalog=Steam;Integrated Security=True;");
             //string conn = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            //SqlConnection cons = new SqlConnection(conn);
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
            //string con = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            //SqlConnection cs = new SqlConnection(con);
            int pkCh = 0;
            string coloanaInformatii = "";

            if (cs.State != ConnectionState.Open)
            {
                cs.Open();
            }

            if (dataGridView2.SelectedCells.Count > 0)
            {
               
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];

                pkCh = Convert.ToInt32(selectedRow.Cells[0].Value);
                coloanaInformatii = Convert.ToString(selectedRow.Cells[1].Value);

            }

            for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
            {
                DialogResult result;
                result = MessageBox.Show("Esti sigur ca doresti sa modifici datele?", "Confirmare", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
         
                    SqlCommand command = new SqlCommand(upQuery, cs);
                    //tr sa iau parametrii aia pt nr de col si numele col si sa inlocuiesc aici intr un for

                    // command.Parameters.AddWithValue("@text", text);
                    //command.Parameters.AddWithValue("@rid", rid);
                    command.Parameters.AddWithValue(ChildUpdateList[0], coloanaInformatii);
                    command.Parameters.AddWithValue(ChildUpdateList[1], pkCh);
              
                   
                    MessageBox.Show("Updatat");
                    command.ExecuteNonQuery();
                    
                }
            }


        }


    }
}