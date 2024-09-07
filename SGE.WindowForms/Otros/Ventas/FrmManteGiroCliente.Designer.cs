namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    partial class FrmManteGiroCliente
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
            this.components = new System.ComponentModel.Container();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.BtnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.BtnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtIdGiro = new DevExpress.XtraEditors.TextEdit();
            this.txtGiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.LkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkIndicador = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdGiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIndicador.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BtnGuardar,
            this.BtnCancelar});
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.BtnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnGuardar.Appearance.Options.UseFont = true;
            this.BtnGuardar.Caption = "Guardar";
            this.BtnGuardar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.BtnGuardar.Hint = "Guardar";
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGuardar_ItemClick);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnCancelar.Appearance.Options.UseFont = true;
            this.BtnCancelar.Caption = "Cancelar";
            this.BtnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.BtnCancelar.Hint = "Cancelar";
            this.BtnCancelar.Id = 1;
            this.BtnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(406, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 97);
            this.barDockControlBottom.Size = new System.Drawing.Size(406, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 97);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(406, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 97);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Código:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(23, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Giro:";
            // 
            // txtIdGiro
            // 
            this.txtIdGiro.Location = new System.Drawing.Point(99, 33);
            this.txtIdGiro.Name = "txtIdGiro";
            this.txtIdGiro.Properties.Mask.ShowPlaceHolders = false;
            this.txtIdGiro.Properties.MaxLength = 1;
            this.txtIdGiro.Size = new System.Drawing.Size(96, 20);
            this.txtIdGiro.TabIndex = 1;
            this.txtIdGiro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cerrar_form);
            // 
            // txtGiro
            // 
            this.txtGiro.Enabled = false;
            this.txtGiro.Location = new System.Drawing.Point(99, 54);
            this.txtGiro.Name = "txtGiro";
            this.txtGiro.Properties.MaxLength = 50;
            this.txtGiro.Size = new System.Drawing.Size(293, 20);
            this.txtGiro.TabIndex = 3;
            this.txtGiro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtdescripcion_KeyUp);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(222, 34);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(47, 13);
            this.labelControl12.TabIndex = 0;
            this.labelControl12.Text = "Situación:";
            // 
            // LkpSituacion
            // 
            this.LkpSituacion.Enabled = false;
            this.LkpSituacion.Location = new System.Drawing.Point(275, 33);
            this.LkpSituacion.Name = "LkpSituacion";
            this.LkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpSituacion.Properties.NullText = "";
            this.LkpSituacion.Size = new System.Drawing.Size(117, 20);
            this.LkpSituacion.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.chkIndicador);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.LkpSituacion);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.txtGiro);
            this.groupControl1.Controls.Add(this.txtIdGiro);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(406, 97);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Giro de Clientes";
            // 
            // chkIndicador
            // 
            this.chkIndicador.Location = new System.Drawing.Point(97, 75);
            this.chkIndicador.MenuManager = this.barManager1;
            this.chkIndicador.Name = "chkIndicador";
            this.chkIndicador.Properties.Caption = "";
            this.chkIndicador.Size = new System.Drawing.Size(75, 19);
            this.chkIndicador.TabIndex = 4;
            this.chkIndicador.Visible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(65, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Toma pedido:";
            this.labelControl3.Visible = false;
            // 
            // FrmManteGiroCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 124);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteGiroCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Giro de Clientes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManteDetalleAnalitica_FormClosing);
            this.Load += new System.EventHandler(this.FrmManteGiroCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdGiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIndicador.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        private DevExpress.XtraBars.BarButtonItem BtnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.LookUpEdit LkpSituacion;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        public DevExpress.XtraEditors.TextEdit txtGiro;
        public DevExpress.XtraEditors.TextEdit txtIdGiro;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.CheckEdit chkIndicador;
    }
}