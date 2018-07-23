using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finisar.SQLite;

namespace EditorOlimpiadas
{
    public partial class frmEditor : Form
    {
        public string ruta;
        public frmEditor()
        {
            InitializeComponent();
            
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog buscarBD = new OpenFileDialog();
            buscarBD.Filter = "DB Files (.sqlite)|*.sqlite";
            if (buscarBD.ShowDialog() == DialogResult.OK)
            {
                //EditorOlimpiadas.CBD.sqConnectionString = "DataSource=" + buscarBD.FileName;
                //CBD.contadorRegistro();
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
            CrearBD();
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
                    SQLiteConnection conexion_sqlite;
                    SQLiteCommand cmd_sqlite;
                    // SQLiteDataReader datareader_sqlite;
                    //Crear una nueva conexión de la base de datos
                    conexion_sqlite = new SQLiteConnection("Data Source=" + folderPath + "\\database.db;Version=3;New=True;Compress=True;");
                    conexion_sqlite.Open();
                    cmd_sqlite = conexion_sqlite.CreateCommand();
                    cmd_sqlite.CommandText = "CREATE TABLE tblOlimpiada( intIdOlimpiada INTEGER NOT NULL PRIMARY KEY, txtNombre TEXT NOT NULL UNIQUE);";
                    cmd_sqlite.ExecuteNonQuery();

                    cmd_sqlite.CommandText = "CREATE TABLE tblCuestionario( intID INTEGER NULL, txtPregunta TEXT NULL, txtVideo NVARCHAR NULL, txtEcuaciones NVARCHAR NULL, txtOtros NVARCHAR NULL, intIdCategoria INTEGER NOT NULL, txtCorrecta TEXT NOT NULL, intIdOlimpiada INTEGER NOT NULL, CHECK ((txtPregunta is not null) or (txtVideo is not null) or (txtEcuaciones is not null) or (txtOtros is not null) ) PRIMARY KEY (intID,intIdOlimpiada) FOREIGN KEY(intIdCategoria,intIdOlimpiada) REFERENCES TblCategoria(intIdCategoria,intIdOlimpiada) );";
                    cmd_sqlite.ExecuteNonQuery();

                    cmd_sqlite.CommandText = "CREATE TABLE tblCategoria( intIdCategoria INTEGER NOT NULL, txtNombreCategoria TEXT NOT NULL , intIdOlimpiada INTEGER NOT NULL , PRIMARY KEY(intIdCategoria,intIdOlimpiada), FOREIGN KEY(intIdOlimpiada) REFERENCES TblOlimpiada(intIdOlimpiada) );";
                    cmd_sqlite.ExecuteNonQuery();

                    cmd_sqlite.CommandText = "CREATE TABLE TblRepuestaErronea( intID INTEGER NOT NULL PRIMARY KEY, intIdOlimpiada INTEGER NOT NULL, txtRespuesta1 TEXT NOT NULL, txtRespuesta2 TEXT NOT NULL, txtRespuesta3 TEXT NOT NULL, FOREIGN KEY(intIdOlimpiada,intID) REFERENCES TblCuestionario(intIdOlimpiada,intID) );";
                    cmd_sqlite.ExecuteNonQuery();

                    //Insertando datos en la tabla
                    //cmd_sqlite.CommandText = "INSERT INTO demo(id, texto) VALUES (1, 'Este es el primer texto');";
                    //cmd_sqlite.ExecuteNonQuery();
                    //cmd_sqlite.CommandText = "INSERT INTO demo(id, texto) VALUES (2, 'Este es el segundo texto');";
                    //cmd_sqlite.ExecuteNonQuery();
                    this.Text = folderPath;
                    MessageBox.Show("Base de datos creada exitosamente!");
                    conexion_sqlite.Close();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error al intentar crear la base de datos");
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
                MessageBox.Show(extension2[2]);
                if (extension2[2] == "BMP" || extension2[2] == "JPG" || extension2[2] == "PNG")
                {
                    picPregunta.ImageLocation = openFileDialog1.FileName;
                }
                if (extension2[2] == "AVI" || extension2[2] == "MP4" || extension2[2] == "?")
                {

                }
                if (extension2[2] == "WAV" || extension2[2] == "MP3" )
                {

                }

            }
        }
    }   
}
