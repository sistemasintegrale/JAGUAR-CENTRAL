namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    partial class FrmMantePaisDepProvDis
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.LkpProvincia = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.LkpDepartamento = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.LkpPais = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.LkpTipoUbicacion = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.LkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.txtNombre = new DevExpress.XtraEditors.TextEdit();
            this.txtIdUbicacion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.BtnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.BtnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpProvincia.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpDepartamento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpPais.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoUbicacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdUbicacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.LkpProvincia);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.LkpDepartamento);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.LkpPais);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.LkpTipoUbicacion);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.LkpSituacion);
            this.groupControl1.Controls.Add(this.txtNombre);
            this.groupControl1.Controls.Add(this.txtIdUbicacion);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(435, 136);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "País - Departamento - Provincia -Distrito";
            // 
            // LkpProvincia
            // 
            this.LkpProvincia.Enabled = false;
            this.LkpProvincia.Location = new System.Drawing.Point(303, 110);
            this.LkpProvincia.Name = "LkpProvincia";
            this.LkpProvincia.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpProvincia.Properties.NullText = "";
            this.LkpProvincia.Size = new System.Drawing.Size(120, 20);
            this.LkpProvincia.TabIndex = 14;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(224, 113);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(47, 13);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "Provincia:";
            // 
            // LkpDepartamento
            // 
            this.LkpDepartamento.Enabled = false;
            this.LkpDepartamento.Location = new System.Drawing.Point(303, 89);
            this.LkpDepartamento.Name = "LkpDepartamento";
            this.LkpDepartamento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpDepartamento.Properties.NullText = "";
            this.LkpDepartamento.Size = new System.Drawing.Size(120, 20);
            this.LkpDepartamento.TabIndex = 12;
            this.LkpDepartamento.EditValueChanged += new System.EventHandler(this.LkpDepartamento_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(224, 92);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(73, 13);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Departamento:";
            // 
            // LkpPais
            // 
            this.LkpPais.Enabled = false;
            this.LkpPais.Location = new System.Drawing.Point(90, 89);
            this.LkpPais.Name = "LkpPais";
            this.LkpPais.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpPais.Properties.NullText = "";
            this.LkpPais.Size = new System.Drawing.Size(120, 20);
            this.LkpPais.TabIndex = 10;
            this.LkpPais.EditValueChanged += new System.EventHandler(this.LkpPais_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 92);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(23, 13);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "País:";
            // 
            // LkpTipoUbicacion
            // 
            this.LkpTipoUbicacion.Enabled = false;
            this.LkpTipoUbicacion.Location = new System.Drawing.Point(90, 69);
            this.LkpTipoUbicacion.Name = "LkpTipoUbicacion";
            this.LkpTipoUbicacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpTipoUbicacion.Properties.NullText = "";
            this.LkpTipoUbicacion.Size = new System.Drawing.Size(120, 20);
            this.LkpTipoUbicacion.TabIndex = 8;
            this.LkpTipoUbicacion.EditValueChanged += new System.EventHandler(this.LkpTipoUbicacion_EditValueChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(12, 72);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 13);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "Tipo:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(250, 30);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Situación:";
            // 
            // LkpSituacion
            // 
            this.LkpSituacion.Enabled = false;
            this.LkpSituacion.Location = new System.Drawing.Point(303, 27);
            this.LkpSituacion.Name = "LkpSituacion";
            this.LkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpSituacion.Properties.NullText = "";
            this.LkpSituacion.Size = new System.Drawing.Size(120, 20);
            this.LkpSituacion.TabIndex = 2;
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Location = new System.Drawing.Point(90, 48);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Properties.MaxLength = 50;
            this.txtNombre.Size = new System.Drawing.Size(333, 20);
            this.txtNombre.TabIndex = 3;
            this.txtNombre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtdescripcion_KeyUp);
            // 
            // txtIdUbicacion
            // 
            this.txtIdUbicacion.Location = new System.Drawing.Point(90, 27);
            this.txtIdUbicacion.Name = "txtIdUbicacion";
            this.txtIdUbicacion.Properties.Mask.ShowPlaceHolders = false;
            this.txtIdUbicacion.Properties.MaxLength = 3;
            this.txtIdUbicacion.Size = new System.Drawing.Size(120, 20);
            this.txtIdUbicacion.TabIndex = 1;
            this.txtIdUbicacion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cerrar_form);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(41, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Nombre:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Código:";
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
            this.barDockControlTop.Size = new System.Drawing.Size(435, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 136);
            this.barDockControlBottom.Size = new System.Drawing.Size(435, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 136);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(435, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 136);
            // 
            // FrmMantePaisDepProvDis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 163);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmMantePaisDepProvDis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Registro de Ubicación Geográfica";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManteDetalleAnalitica_FormClosing);
            this.Load += new System.EventHandler(this.FrmManteVendedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpProvincia.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpDepartamento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpPais.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoUbicacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdUbicacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit txtNombre;
        public DevExpress.XtraEditors.TextEdit txtIdUbicacion;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.LookUpEdit LkpTipoUbicacion;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        private DevExpress.XtraBars.BarButtonItem BtnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraEditors.LookUpEdit LkpSituacion;
        public DevExpress.XtraEditors.LookUpEdit LkpPais;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.LookUpEdit LkpDepartamento;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.LookUpEdit LkpProvincia;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}