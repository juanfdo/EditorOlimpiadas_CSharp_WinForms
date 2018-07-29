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
        const string strTblRespuestaErronea = "tblRespuestaErronea";
        List<Control> activarDesactivarControles = null;
        List<ToolStripItem> activarDesactivarToolStripItems = null;

        //Int32 cuestionId = -1;
        private Int32 __CUESTION_ID_ = -1;
        public Int32 cuestionId
        {
            set {
                __CUESTION_ID_ = value;
                txtPreguntaId.Text = "" + __CUESTION_ID_;
            }
            get { return __CUESTION_ID_; }
        }
        //public SQLiteConnection conexion_sqlite { get; private set; }
        private void SetConnection(String filePath)
        {
            /*
            ArrayList tablas = new ArrayList();
            tablas.Add(strTblOlimpiada);
            tablas.Add(strTblCategoria);
            tablas.Add(strTblCuestionario);
            tablas.Add(strTblRespuestaErronea);
            */
            

            String conString = String.Format("Data Source={0};Version=3;New=False;Compress=True;", filePath);
            try
            {
                conexion_sqlite = new SQLiteConnection(conString);
                llenarComboBox(this.cbOlimpiada, strTblOlimpiada);
                llenarComboBox(this.cbCategoria, strTblCategoria);
                ActivarEdicion();
                cargarPrimeraPregunta();
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

            if (activarDesactivarControles == null)
            {
                activarDesactivarControles = new List<Control>();
                activarDesactivarControles.Add(this.txtPregunta);
                activarDesactivarControles.Add(this.txtOp1);
                activarDesactivarControles.Add(this.txtOp2);
                activarDesactivarControles.Add(this.txtOp3);
                activarDesactivarControles.Add(this.txtOp4);
                activarDesactivarControles.Add(this.tbAgregarCategoria);
                activarDesactivarControles.Add(this.tbAgregarOlimpiada);
                activarDesactivarControles.Add(this.btnOpenMedia);
                activarDesactivarControles.Add(this.rBtnOpcion1);
                activarDesactivarControles.Add(this.rBtnOpcion2);
                activarDesactivarControles.Add(this.rBtnOpcion3);
                activarDesactivarControles.Add(this.rBtnOpcion4);
            }
            if (activarDesactivarToolStripItems == null)
            {
                activarDesactivarToolStripItems = new List<ToolStripItem>();
                activarDesactivarToolStripItems.Add(this.btnCloseDB);
                activarDesactivarToolStripItems.Add(this.btnDelQ);
                activarDesactivarToolStripItems.Add(this.btnBack);
                activarDesactivarToolStripItems.Add(this.btnFirst);
                activarDesactivarToolStripItems.Add(this.btnLast);
                activarDesactivarToolStripItems.Add(this.btnNewQ);
                activarDesactivarToolStripItems.Add(this.btnNext);
                activarDesactivarToolStripItems.Add(this.btnSave);
            }
            DesActivarEdicion();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscarBD = new OpenFileDialog();
            buscarBD.Filter = "Archivos sqlite (.sqlite)|*.sqlite|Archivos DB (.db)|*.db";
            if (buscarBD.ShowDialog() == DialogResult.OK)
            {
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
            SQLiteCommand cmd_sqlite = null;
            string strQuery;
            string folderPath = string.Empty;

            try
            {
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
                    cmd_sqlite = null;

                    ActivarEdicion();
                }
            }
            catch (SQLiteException ex)
            {
                if (cmd_sqlite != null)
                {
                    cmd_sqlite.CommandText = "ROLLBACK;";
                    cmd_sqlite.ExecuteNonQuery();
                }
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

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
        public class ConjuntoRBtnTBox
        {
            public RadioButton rBtn;
            public TextBox tBox;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SQLiteConnection cnxSqlite = null;
            string strRespuestaOK = null;
            Int32 intCat = 0, intOlimpiada = 0;
            List<string> strRespuestaErr = new List<string>();
            SQLiteDataReader reader = null;
            SQLiteCommand sqlCmd = null;
            string strInsertarCuestionario = null;
            string strPregunta;

            ConjuntoRBtnTBox[] conjunto = {
                new ConjuntoRBtnTBox(){ rBtn = rBtnOpcion1, tBox = txtOp1 },
                new ConjuntoRBtnTBox(){ rBtn = rBtnOpcion2, tBox = txtOp2 },
                new ConjuntoRBtnTBox(){ rBtn = rBtnOpcion3, tBox = txtOp3 },
                new ConjuntoRBtnTBox(){ rBtn = rBtnOpcion4, tBox = txtOp4 }
            };

            try
            {
                cnxSqlite = this.conexion_sqlite.OpenAndReturn();
                sqlCmd = this.conexion_sqlite.CreateCommand();

                //TODO: Validar el tipo de pregunta: Texto, Video, Ecuaciones, otros
                strPregunta = this.txtPregunta.Text;
                if (strPregunta == null)
                    return;//TODO: trow exception
                if (strPregunta.Length <= 0)
                    return;//TODO: trow exception

                if (cbCategoria.SelectedIndex > -1)
                {
                    intCat = ((Int32)cbCategoria.SelectedValue);
                }
                else
                {
                    throw new Exception("No se ha seleccionado una categoría");
                }
                if (cbOlimpiada.SelectedIndex > -1)
                {
                    intOlimpiada = ((Int32)cbOlimpiada.SelectedValue);
                }
                else
                {
                    throw new Exception("No se ha seleccionado una olimpiada");
                }

                foreach (ConjuntoRBtnTBox elm in conjunto)
                {
                    if (elm.rBtn.Checked)
                    {
                        strRespuestaOK = elm.tBox.Text;
                    }
                    else
                    {
                        strRespuestaErr.Add(elm.tBox.Text);
                    }
                }

                if (cuestionId != -1)
                {
                    //strInsertarCuestionario = Properties.Resources.;
                    strInsertarCuestionario = Properties.Resources.ScriptUpdatePregunta;
                    strInsertarCuestionario = String.Format(strInsertarCuestionario, strPregunta, strRespuestaOK, intCat, intOlimpiada, strRespuestaErr.ElementAt(0), strRespuestaErr.ElementAt(1), strRespuestaErr.ElementAt(2),cuestionId);
                }
                else
                {
                    strInsertarCuestionario = Properties.Resources.ScriptInsertaPregunta;
                    strInsertarCuestionario = String.Format(strInsertarCuestionario, strPregunta, strRespuestaOK, intCat, intOlimpiada, strRespuestaErr.ElementAt(0), strRespuestaErr.ElementAt(1), strRespuestaErr.ElementAt(2));
                }


                sqlCmd.CommandText = strInsertarCuestionario;
                reader = sqlCmd.ExecuteReader();
                cuestionId = -1;
                while (reader.Read())
                {
                    cuestionId = reader.GetInt32(0);
                }
            }
            catch (SQLiteException ex)
            {
                if (sqlCmd != null)
                {
                    sqlCmd.CommandText = "ROLLBACK;";
                    sqlCmd.ExecuteNonQuery();
                }
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //TODO: Establecer procedimiento de finallización
                if (reader != null)
                    reader.Close();
                if (cnxSqlite != null)
                    cnxSqlite.Close();
            }
        }

        private void btAgregarOlimpiada_Click(object sender, EventArgs e)
        {
            string txtOlimpiada = tbAgregarOlimpiada.Text;
            insertarNombre(strTblOlimpiada, txtOlimpiada);
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
                String strQuery = String.Format("INSERT INTO {0} (txtNombre) VALUES('{1}');", strTabla, strTexto);
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
        public class Nombre
        {
            public string texto { get; set; }
            public Int32 id { get; set; }
        }
        private void DesActivarEdicion()
        {
            foreach (Control tmp in activarDesactivarControles)
            {
                tmp.Enabled = false;
            }
            foreach (ToolStripItem tmp in activarDesactivarToolStripItems)
            {
                tmp.Enabled = false;
            }
        }
        private void ActivarEdicion()
        {
            foreach (Control tmp in activarDesactivarControles)
            {
                tmp.Enabled = true;
            }
            foreach (ToolStripItem tmp in activarDesactivarToolStripItems)
            {
                tmp.Enabled = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            cargarSiguientePregunta();
        }

        private void cargarUltimaPregunta()
        {
            string query = "SELECT * FROM viewCuestionario ORDER BY IntId DESC LIMIT 1;";
            cargarPregunta(query);
        }

        private void cargarPrimeraPregunta()
        {
            string query = "SELECT * FROM viewCuestionario ORDER BY IntId ASC LIMIT 1;";
            cargarPregunta(query);
        }

        private void cargarSiguientePregunta()
        {
            string query = "SELECT * FROM viewCuestionario WHERE IntId > {0} ORDER BY IntId ASC LIMIT 1;";
            query = String.Format(query, cuestionId);
            cargarPregunta(query);
        }

        private void cargarAnteriorPregunta()
        {
            string query = "SELECT * FROM viewCuestionario WHERE IntId < {0} ORDER BY IntId DESC LIMIT 1;";
            query = String.Format(query, cuestionId);
            cargarPregunta(query);
        }

        private void cargarPregunta(string query)
        {
            SQLiteConnection cnxSqlite = null;
            SQLiteCommand cmdSqlite = null;
            try
            {
                cnxSqlite = conexion_sqlite.OpenAndReturn();
                cmdSqlite = cnxSqlite.CreateCommand();
                cmdSqlite.CommandText = query;
                SQLiteDataReader reader = cmdSqlite.ExecuteReader();
                if (reader.Read())
                {
                    cuestionId = reader.GetInt32(0);
                    this.txtPregunta.Text = reader.GetString(4);
                    this.rBtnOpcion1.Checked = true;
                    this.txtOp1.Text = reader.GetString(1);
                    this.txtOp2.Text = reader.GetString(6);
                    this.txtOp3.Text = reader.GetString(7);
                    this.txtOp4.Text = reader.GetString(8);
                }
                else
                {
                    //TODO: No hay registros en la Base de datos
                }
                reader.Close();
            }
            catch (SQLiteException ex)
            {
                if(cmdSqlite != null)
                {
                    cmdSqlite.CommandText = "ROLLBACK;";
                    cmdSqlite.ExecuteNonQuery();
                }
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //reader.Close();
                if (cnxSqlite != null)
                    cnxSqlite.Close();

            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            cargarUltimaPregunta();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            cargarAnteriorPregunta();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            cargarPrimeraPregunta();
        }

        private void btnDelQ_Click(object sender, EventArgs e)
        {
            borrarPreguntaActual();
        }
        private void borrarPreguntaActual()
        {
            String query = "BEGIN TRANSACTION;" +
                "DELETE FROM " + strTblRespuestaErronea + " WHERE IntId = {0};" +
                "DELETE FROM " + strTblCuestionario + " WHERE IntId = {0};" +
                "COMMIT;" +
                "SELECT * FROM viewCuestionario WHERE IntId < {0} ORDER BY IntId DESC LIMIT 1;";
            query = String.Format(query, cuestionId);
            cargarPregunta(query);
        }

        private void btnNewQ_Click(object sender, EventArgs e)
        {
            limpiarFormulario();
        }
        private void limpiarFormulario()
        {
            cuestionId = -1;
            this.txtPregunta.Text = "";
            this.txtOp1.Text = "";
            this.txtOp2.Text = "";
            this.txtOp3.Text = "";
            this.txtOp4.Text = "";
            this.cbCategoria.DataSource = null;
            this.cbOlimpiada.DataSource = null;
        }

        private void btnCloseDB_Click(object sender, EventArgs e)
        {
            this.conexion_sqlite = null;
            limpiarFormulario();
            DesActivarEdicion();
        }
    }
}
