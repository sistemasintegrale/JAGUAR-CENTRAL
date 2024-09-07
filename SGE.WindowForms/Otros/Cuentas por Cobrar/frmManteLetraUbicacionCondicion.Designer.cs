namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    partial class frmManteLetraUbicacionCondicion
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
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcDatos = new DevExpress.XtraEditors.GroupControl();
            this.lkpBanco = new DevExpress.XtraEditors.LookUpEdit();
            this.txtLetra = new DevExpress.XtraEditors.TextEdit();
            this.LkpCondicion = new DevExpress.XtraEditors.LookUpEdit();
            this.lbl5 = new DevExpress.XtraEditors.LabelControl();
            this.LkpUbicacion = new DevExpress.XtraEditors.LookUpEdit();
            this.lbl1 = new DevExpress.XtraEditors.LabelControl();
            this.lbl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroUnico = new DevExpress.XtraEditors.TextEdit();
            this.lbl3 = new DevExpress.XtraEditors.LabelControl();
            this.lbl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatos)).BeginInit();
            this.gcDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpBanco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetra.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpCondicion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpUbicacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroUnico.Properties)).BeginInit();
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
            this.btnGuardar,
            this.btnCancelar});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnGuardar.Caption = "Guardar";
            this.btnGuardar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.btnGuardar.Id = 0;
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Caption = "Cancelar";
            this.btnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnCancelar.Id = 1;
            this.btnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(399, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 112);
            this.barDockControlBottom.Size = new System.Drawing.Size(399, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 112);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(399, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 112);
            // 
            // gcDatos
            // 
            this.gcDatos.Controls.Add(this.lkpBanco);
            this.gcDatos.Controls.Add(this.txtLetra);
            this.gcDatos.Controls.Add(this.LkpCondicion);
            this.gcDatos.Controls.Add(this.lbl5);
            this.gcDatos.Controls.Add(this.LkpUbicacion);
            this.gcDatos.Controls.Add(this.lbl1);
            this.gcDatos.Controls.Add(this.lbl4);
            this.gcDatos.Controls.Add(this.txtNumeroUnico);
            this.gcDatos.Controls.Add(this.lbl3);
            this.gcDatos.Controls.Add(this.lbl2);
            this.gcDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDatos.Location = new System.Drawing.Point(0, 0);
            this.gcDatos.Name = "gcDatos";
            this.gcDatos.Size = new System.Drawing.Size(399, 112);
            this.gcDatos.TabIndex = 4;
            this.gcDatos.Text = "Ubicación / Condición de la Letra";
            // 
            // lkpBanco
            // 
            this.lkpBanco.Location = new System.Drawing.Point(61, 66);
            this.lkpBanco.Name = "lkpBanco";
            this.lkpBanco.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpBanco.Properties.NullText = "";
            this.lkpBanco.Size = new System.Drawing.Size(164, 20);
            this.lkpBanco.TabIndex = 7;
            // 
            // txtLetra
            // 
            this.txtLetra.Enabled = false;
            this.txtLetra.Location = new System.Drawing.Point(61, 25);
            this.txtLetra.MenuManager = this.barManager1;
            this.txtLetra.Name = "txtLetra";
            this.txtLetra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtLetra.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtLetra.Size = new System.Drawing.Size(116, 20);
            this.txtLetra.TabIndex = 6;
            // 
            // LkpCondicion
            // 
            this.LkpCondicion.Location = new System.Drawing.Point(61, 88);
            this.LkpCondicion.Name = "LkpCondicion";
            this.LkpCondicion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpCondicion.Properties.NullText = "";
            this.LkpCondicion.Size = new System.Drawing.Size(190, 20);
            this.LkpCondicion.TabIndex = 5;
            // 
            // lbl5
            // 
            this.lbl5.Location = new System.Drawing.Point(5, 91);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(50, 13);
            this.lbl5.TabIndex = 4;
            this.lbl5.Text = "Condición:";
            // 
            // LkpUbicacion
            // 
            this.LkpUbicacion.Location = new System.Drawing.Point(61, 45);
            this.LkpUbicacion.Name = "LkpUbicacion";
            this.LkpUbicacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpUbicacion.Properties.NullText = "";
            this.LkpUbicacion.Size = new System.Drawing.Size(190, 20);
            this.LkpUbicacion.TabIndex = 1;
            this.LkpUbicacion.EditValueChanged += new System.EventHandler(this.LkpUbicacion_EditValueChanged);
            // 
            // lbl1
            // 
            this.lbl1.Location = new System.Drawing.Point(5, 28);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(44, 13);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "Letra N°:";
            // 
            // lbl4
            // 
            this.lbl4.Location = new System.Drawing.Point(231, 69);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(41, 13);
            this.lbl4.TabIndex = 0;
            this.lbl4.Text = "N° Unico";
            // 
            // txtNumeroUnico
            // 
            this.txtNumeroUnico.EditValue = "";
            this.txtNumeroUnico.Enabled = false;
            this.txtNumeroUnico.Location = new System.Drawing.Point(278, 66);
            this.txtNumeroUnico.MenuManager = this.barManager1;
            this.txtNumeroUnico.Name = "txtNumeroUnico";
            this.txtNumeroUnico.Size = new System.Drawing.Size(116, 20);
            this.txtNumeroUnico.TabIndex = 3;
            // 
            // lbl3
            // 
            this.lbl3.Location = new System.Drawing.Point(5, 69);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(33, 13);
            this.lbl3.TabIndex = 0;
            this.lbl3.Text = "Banco:";
            // 
            // lbl2
            // 
            this.lbl2.Location = new System.Drawing.Point(5, 48);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(49, 13);
            this.lbl2.TabIndex = 0;
            this.lbl2.Text = "Ubicación:";
            // 
            // frmManteLetraUbicacionCondicion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 139);
            this.Controls.Add(this.gcDatos);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManteLetraUbicacionCondicion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantemiento - Ubicación Condición";
            this.Load += new System.EventHandler(this.frmManteLetraUbicacionCondicion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatos)).EndInit();
            this.gcDatos.ResumeLayout(false);
            this.gcDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpBanco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLetra.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpCondicion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpUbicacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroUnico.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl gcDatos;
        private DevExpress.XtraEditors.TextEdit txtLetra;
        public DevExpress.XtraEditors.LookUpEdit LkpCondicion;
        private DevExpress.XtraEditors.LabelControl lbl5;
        public DevExpress.XtraEditors.LookUpEdit LkpUbicacion;
        private DevExpress.XtraEditors.LabelControl lbl1;
        private DevExpress.XtraEditors.LabelControl lbl4;
        public DevExpress.XtraEditors.TextEdit txtNumeroUnico;
        private DevExpress.XtraEditors.LabelControl lbl3;
        private DevExpress.XtraEditors.LabelControl lbl2;
        public DevExpress.XtraEditors.LookUpEdit lkpBanco;
    }
}