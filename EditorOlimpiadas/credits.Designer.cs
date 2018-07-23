namespace EditorOlimpiadas
{
    partial class frmcredits
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmcredits));
            this.lblSicei = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.picSICEI = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSICEI)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSicei
            // 
            this.lblSicei.AutoSize = true;
            this.lblSicei.Location = new System.Drawing.Point(113, 252);
            this.lblSicei.Name = "lblSicei";
            this.lblSicei.Size = new System.Drawing.Size(501, 17);
            this.lblSicei.TabIndex = 0;
            this.lblSicei.Text = "Docentes: Vesna Srdanovic - Oscar Ignacio Botero - Julián Galeano Echeverri";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(310, 292);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 32);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Salir";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // picSICEI
            // 
            this.picSICEI.Image = ((System.Drawing.Image)(resources.GetObject("picSICEI.Image")));
            this.picSICEI.Location = new System.Drawing.Point(43, 34);
            this.picSICEI.Name = "picSICEI";
            this.picSICEI.Size = new System.Drawing.Size(622, 198);
            this.picSICEI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSICEI.TabIndex = 2;
            this.picSICEI.TabStop = false;
            // 
            // frmcredits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 350);
            this.ControlBox = false;
            this.Controls.Add(this.picSICEI);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblSicei);
            this.Name = "frmcredits";
            this.Text = "Créditos";
            ((System.ComponentModel.ISupportInitialize)(this.picSICEI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSicei;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox picSICEI;
    }
}