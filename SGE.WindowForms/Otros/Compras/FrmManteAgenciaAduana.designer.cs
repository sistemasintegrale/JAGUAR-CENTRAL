namespace SGE.WindowForms.Otros.Compras
{
    partial class FrmManteAgenciaAduana
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnGrabar = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtruc = new DevExpress.XtraEditors.TextEdit();
            this.txtemail = new DevExpress.XtraEditors.TextEdit();
            this.txttelefono = new DevExpress.XtraEditors.TextEdit();
            this.txtdireccion = new DevExpress.XtraEditors.TextEdit();
            this.txtrazon = new DevExpress.XtraEditors.TextEdit();
            this.txtcodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtruc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttelefono.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdireccion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrazon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnCancelar);
            this.groupControl1.Controls.Add(this.btnGrabar);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtruc);
            this.groupControl1.Controls.Add(this.txtemail);
            this.groupControl1.Controls.Add(this.txttelefono);
            this.groupControl1.Controls.Add(this.txtdireccion);
            this.groupControl1.Controls.Add(this.txtrazon);
            this.groupControl1.Controls.Add(this.txtcodigo);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(412, 178);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnCancelar.Location = new System.Drawing.Point(334, 151);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.btnGrabar.Location = new System.Drawing.Point(261, 151);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(67, 23);
            this.btnGrabar.TabIndex = 12;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 151);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(25, 13);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "Ruc :";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 128);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(31, 13);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "Email :";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 82);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(50, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Direccion :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 59);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Razon/Nombre :";
            // 
            // txtruc
            // 
            this.txtruc.Enabled = false;
            this.txtruc.Location = new System.Drawing.Point(103, 148);
            this.txtruc.Name = "txtruc";
            this.txtruc.Properties.MaxLength = 11;
            this.txtruc.Size = new System.Drawing.Size(125, 20);
            this.txtruc.TabIndex = 11;
            this.txtruc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtruc_KeyPress);
            // 
            // txtemail
            // 
            this.txtemail.Enabled = false;
            this.txtemail.Location = new System.Drawing.Point(103, 125);
            this.txtemail.Name = "txtemail";
            this.txtemail.Properties.MaxLength = 50;
            this.txtemail.Size = new System.Drawing.Size(304, 20);
            this.txtemail.TabIndex = 9;
            this.txtemail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtemail_KeyPress);
            // 
            // txttelefono
            // 
            this.txttelefono.Enabled = false;
            this.txttelefono.Location = new System.Drawing.Point(103, 102);
            this.txttelefono.Name = "txttelefono";
            this.txttelefono.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txttelefono.Properties.MaxLength = 9;
            this.txttelefono.Size = new System.Drawing.Size(125, 20);
            this.txttelefono.TabIndex = 7;
            this.txttelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttelefono_KeyPress);
            // 
            // txtdireccion
            // 
            this.txtdireccion.Enabled = false;
            this.txtdireccion.Location = new System.Drawing.Point(103, 79);
            this.txtdireccion.Name = "txtdireccion";
            this.txtdireccion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtdireccion.Properties.MaxLength = 50;
            this.txtdireccion.Size = new System.Drawing.Size(304, 20);
            this.txtdireccion.TabIndex = 5;
            this.txtdireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdireccion_KeyPress);
            // 
            // txtrazon
            // 
            this.txtrazon.Enabled = false;
            this.txtrazon.Location = new System.Drawing.Point(103, 56);
            this.txtrazon.Name = "txtrazon";
            this.txtrazon.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtrazon.Properties.MaxLength = 50;
            this.txtrazon.Size = new System.Drawing.Size(304, 20);
            this.txtrazon.TabIndex = 3;
            this.txtrazon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtrazon_KeyPress);
            // 
            // txtcodigo
            // 
            this.txtcodigo.Enabled = false;
            this.txtcodigo.Location = new System.Drawing.Point(103, 33);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcodigo.Properties.Mask.ShowPlaceHolders = false;
            this.txtcodigo.Size = new System.Drawing.Size(108, 20);
            this.txtcodigo.TabIndex = 1;
            this.txtcodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcodigo_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 105);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(49, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Telefono :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Codigo :";
            // 
            // FrmManteAgenciaAduana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 178);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmManteAgenciaAduana";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Agencia Aduana";
            this.Load += new System.EventHandler(this.FrmManteAgenciaAduana_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtruc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttelefono.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdireccion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrazon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.TextEdit txtrazon;
        public DevExpress.XtraEditors.TextEdit txtcodigo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtruc;
        public DevExpress.XtraEditors.TextEdit txtemail;
        public DevExpress.XtraEditors.TextEdit txttelefono;
        public DevExpress.XtraEditors.TextEdit txtdireccion;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        public DevExpress.XtraEditors.SimpleButton btnGrabar;
    }
}