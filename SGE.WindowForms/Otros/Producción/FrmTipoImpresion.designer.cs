namespace SGE.WindowForms.Otros.Produccion
{
    partial class FrmTipoImpresion
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.cbosituacion = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cbosituacion.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(162, 8);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 23;
            this.simpleButton1.Text = "Imprimir";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // cbosituacion
            // 
            this.cbosituacion.EditValue = "Todos";
            this.cbosituacion.Location = new System.Drawing.Point(65, 9);
            this.cbosituacion.Name = "cbosituacion";
            this.cbosituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbosituacion.Properties.Items.AddRange(new object[] {
            "Todos",
            "Activo",
            "Inactivo"});
            this.cbosituacion.Size = new System.Drawing.Size(85, 20);
            this.cbosituacion.TabIndex = 21;
            this.cbosituacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbosituacion_KeyPress);
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(12, 12);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(50, 13);
            this.labelControl16.TabIndex = 22;
            this.labelControl16.Text = "Situacion :";
            // 
            // FrmTipoImpresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 40);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.cbosituacion);
            this.Controls.Add(this.labelControl16);
            this.Name = "FrmTipoImpresion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impresión";
            this.Load += new System.EventHandler(this.FrmTipoImpresion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbosituacion.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.ComboBoxEdit cbosituacion;
        private DevExpress.XtraEditors.LabelControl labelControl16;
    }
}