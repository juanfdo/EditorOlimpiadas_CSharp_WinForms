using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;
//using Finisar.SQLite;

namespace EditorOlimpiadas
{
    public partial class frmEditor : Form
    {
        public string ruta;
        private SQLiteConnection conexion_sqlite = null;

        const string strTblCategoria = "tblCategoria";
        const string strTblOlimpiada = "tblOlimpiada";
        const string strTblCuestionario = "tblCuestionario";
        const string strTblRespuestaErronea = "tblRepuestaErronea";

        //public SQLiteConnection conexion_sqlite { get; private set; }
        private void SetConnection(String filePath)
        {
            ArrayList tablas = new ArrayList();
            tablas.Add(strTblOlimpiada);
            tablas.Add(strTblCategoria);
            tablas.Add(strTblCuestionario);
            tablas.Add(strTblRespuestaErronea);

            String conString = String.Format("Data Source={0};Version=3;New=False;Compress=True;", filePath);
            try
            {
                conexion_sqlite = new SQLiteConnection(conString);
                llenarComboBox(this.cbOlimpiada, strTblOlimpiada);
                llenarComboBox(this.cbCategoria, strTblCategoria);
                //conexion_sqlite.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public frmEditor()
        {
            InitializeComponent();

        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscarBD = new OpenFileDialog();
            buscarBD.Filter = "Archivos sqlite (.sqlite)|*.sqlite|Archivos DB (.db)|*.db";
            if (buscarBD.ShowDialog() == DialogResult.OK)
            {
                //EditorOlimpiadas.CBD.sqConnectionString = "DataSource=" + buscarBD.FileName;
                //CBD.contadorRegistro();
                SetConnection(buscarBD.FileName);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registroToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtPregunta_TextChanged(object sender, EventArgs e)
        {

        }

        private void acercaDeTSM_Click(object sender, EventArgs e)
        {
            Form creditos = new frmcredits();
            creditos.Show();

        }

        private void btnNewDB_Click(object sender, EventArgs e)
        {
            //Crear una nueva Base de Datos
            CrearBD();
        }

        private void btnOpenDB_Click(object sender, EventArgs e)
        {
            //Crear una nueva Base de Datos
            //CrearBD();
            abrirToolStripMenuItem_Click(null, null);
        }

        private void CrearBD()
        {
            try
            {

                string folderPath = string.Empty;
                using (FolderBrowserDialog fdb = new FolderBrowserDialog())
                {
                    if (fdb.ShowDialog() == DialogResult.OK)
                    {
                        folderPath = fdb.SelectedPath;
                    }
                }

                if (folderPath != string.Empty)
                {
                    //Utilizamos estos tres objetos de SQLite
                    //SQLiteConnection conexion_sqlite;
                    SQLiteCommand cmd_sqlite;
                    string strQuery;
                    // SQLiteDataReader datareader_sqlite;
                    //Crear una nueva conexión de la base de datos
                    conexion_sqlite = new SQLiteConnection("Data Source=" + folderPath + "\\database.sqlite;Version=3;New=True;Compress=True;");
                    conexion_sqlite.Open();

                    cmd_sqlite = conexion_sqlite.CreateCommand();
                    strQuery = Properties.Resources.ScriptCreateTables;
                    cmd_sqlite.CommandText = strQuery;
                    cmd_sqlite.ExecuteNonQuery();

                    this.Text = folderPath;
                    MessageBox.Show("Base de datos creada exitosamente!");
                    conexion_sqlite.Close();
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }

        //private void btnOpenMedia_Click(object sender, EventArgs e)
        //{

        //}

        private void picEqP_Click(object sender, EventArgs e)
        {
            optEqP.Checked = true;
        }

        private void picTextP_Click(object sender, EventArgs e)
        {
            optEqTextP.Checked = true;
        }

        private void picEq1_Click(object sender, EventArgs e)
        {
            optEq1.Checked = true;
        }

        private void picText1_Click(object sender, EventArgs e)
        {
            optText1.Checked = true;
        }

        private void picEq2_Click(object sender, EventArgs e)
        {
            optEq2.Checked = true;
        }

        private void picText2_Click(object sender, EventArgs e)
        {
            optText2.Checked = true;
        }

        private void picEq3_Click(object sender, EventArgs e)
        {
            optEq3.Checked = true;
        }

        private void picText3_Click(object sender, EventArgs e)
        {
            optText3.Checked = true;
        }

        private void picEq4_Click(object sender, EventArgs e)
        {
            optEq4.Checked = true;
        }

        private void picText4_Click(object sender, EventArgs e)
        {
            optText4.Checked = true;
        }

        private void btnOpenMedia_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "Imágenes-Vídeo | *.JPG; *.PNG; *.GIF; *.AVI; *.WAV; *.MP3";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtMediaPath.Text = openFileDialog1.FileName;
                string extension = txtMediaPath.Text.ToUpper();
                String[] extension2 = extension.Split('.');
                MessageBox.Show(extension2[1]);
                int extIndex = extension2.Length - 1;
                if (extension2[extIndex] == "BMP" || extension2[extIndex] == "JPG" || extension2[extIndex] == "PNG")
                {
                    picPregunta.ImageLocation = openFileDialog1.FileName;
                }
                if (extension2[extIndex] == "AVI" || extension2[extIndex] == "MP4" || extension2[extIndex] == "?")
                {

                }
                if (extension2[extIndex] == "WAV" || extension2[1] == "MP3")
                {

                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SQLiteCommand sqlCmd = this.conexion_sqlite.CreateCommand();
        }

        private void btAgregarOlimpiada_Click(object sender, EventArgs e)
        {
            insertarNombre(strTblOlimpiada, tbAgregarOlimpiada.Text);
            llenarComboBox(cbOlimpiada, strTblOlimpiada);
            tbAgregarOlimpiada.Text = "";
        }
        private void btAgregarCategoria_Click(object sender, EventArgs e)
        {
            insertarNombre(strTblCategoria, tbAgregarCategoria.Text);
            llenarComboBox(cbCategoria, strTblCategoria);
            tbAgregarCategoria.Text = "";
        }

        private void insertarNombre(String strTabla, String strTexto)
        {
            SQLiteConnection cnxSqlite = null;
            try
            {
                String strQuery = String.Format("INSERT INTO {0}(txtNombre) VALUES('{1}');", strTabla, strTexto);
                cnxSqlite = conexion_sqlite.OpenAndReturn();
                SQLiteCommand cmdSqlite = cnxSqlite.CreateCommand();
                cmdSqlite.CommandText = strQuery;
                cmdSqlite.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cnxSqlite != null)
                    cnxSqlite.Close();
            }
        }
        private void llenarComboBox(ComboBox cb, string tabla)
        {
            SQLiteConnection cnxSqlite = null;
            try
            {
                cnxSqlite = conexion_sqlite.OpenAndReturn();
                SQLiteCommand cmdSqlite = cnxSqlite.CreateCommand();
                cmdSqlite.CommandText = String.Format("SELECT intID, txtNombre FROM {0}", tabla);
                SQLiteDataReader reader = cmdSqlite.ExecuteReader();
                List<Nombre> nombres = new List<Nombre>();
                Nombre nombre;

                while (reader.Read())
                {
                    nombre = new Nombre()
                    {
                        id = reader.GetInt32(0),
                        texto = reader.GetString(1)
                    };
                    nombres.Add(nombre);
                }
                cb.DataSource = nombres;
                cb.DisplayMember = "texto";
                cb.ValueMember = "id";
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cnxSqlite != null)
                    cnxSqlite.Close();
            }
        }
    }
    public class Nombre
    {
        public string texto { get; set; }
        public Int32 id { get; set; }
    }
}

/*
INSERT INTO tblOlimpiada(txtNombre) VALUES ('2018-2');
INSERT INTO tblCategoria(txtNombre) VALUES ('Cat1');

INSERT INTO tblCuestionario( 
	txtPregunta,
	txtVideo,
	txtEcuaciones,
	txtOtros,
	txtCorrecta,
	intIdCategoria,
	intIdOlimpiada
) VALUES (
	"TEXTO DE LA PREGUNTA",
	null,
	null,
	null,
	"Respuesta correcta",
	1,
	1
);
*/
