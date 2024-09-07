namespace SGE.WindowForms.Otros.Compras
{
    partial class frmManteGarantiaProveedores
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
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.dteFecha = new DevExpress.XtraEditors.DateEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.bteProveedores = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.bteCCosto = new DevExpress.XtraEditors.ButtonEdit();
            this.bteOCS = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumero = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.bteFactura = new DevExpress.XtraEditors.ButtonEdit();
            this.txtMonto = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lkpMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.bteProyecto = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProveedores.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteOCS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteFactura.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProyecto.Properties)).BeginInit();
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar, true),
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
            this.barDockControlTop.Size = new System.Drawing.Size(599, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 108);
            this.barDockControlBottom.Size = new System.Drawing.Size(599, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 108);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(599, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 108);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem1.Caption = "Guardar";
            this.barButtonItem1.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Garantia N° :";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(243, 28);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 13);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Fecha :";
            // 
            // dteFecha
            // 
            this.dteFecha.EditValue = null;
            this.dteFecha.Location = new System.Drawing.Point(285, 25);
            this.dteFecha.MenuManager = this.barManager1;
            this.dteFecha.Name = "dteFecha";
            this.dteFecha.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dteFecha.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dteFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFecha.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFecha.Size = new System.Drawing.Size(96, 20);
            this.dteFecha.TabIndex = 2;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(451, 30);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(50, 13);
            this.labelControl7.TabIndex = 23;
            this.labelControl7.Text = "Situacion :";
            // 
            // lkpSituacion
            // 
            this.lkpSituacion.Enabled = false;
            this.lkpSituacion.Location = new System.Drawing.Point(503, 27);
            this.lkpSituacion.Name = "lkpSituacion";
            this.lkpSituacion.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpSituacion.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSituacion.Properties.NullText = "";
            this.lkpSituacion.Size = new System.Drawing.Size(84, 20);
            this.lkpSituacion.TabIndex = 3;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Location = new System.Drawing.Point(8, 56);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(57, 13);
            this.labelControl9.TabIndex = 36;
            this.labelControl9.Text = "Proveedor :";
            // 
            // bteProveedores
            // 
            this.bteProveedores.Location = new System.Drawing.Point(67, 53);
            this.bteProveedores.Name = "bteProveedores";
            this.bteProveedores.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteProveedores.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteProveedores.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteProveedores.Properties.ReadOnly = true;
            this.bteProveedores.Size = new System.Drawing.Size(192, 20);
            this.bteProveedores.TabIndex = 37;
            this.bteProveedores.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCliente_ButtonClick);
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl10.Location = new System.Drawing.Point(453, 56);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(43, 13);
            this.labelControl10.TabIndex = 38;
            this.labelControl10.Text = "C.Costo:";
            // 
            // bteCCosto
            // 
            this.bteCCosto.Enabled = false;
            this.bteCCosto.Location = new System.Drawing.Point(502, 53);
            this.bteCCosto.Name = "bteCCosto";
            this.bteCCosto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteCCosto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteCCosto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCCosto.Properties.ReadOnly = true;
            this.bteCCosto.Size = new System.Drawing.Size(85, 20);
            this.bteCCosto.TabIndex = 39;
            this.bteCCosto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCosto_ButtonClick);
            // 
            // bteOCS
            // 
            this.bteOCS.Location = new System.Drawing.Point(56, 79);
            this.bteOCS.Name = "bteOCS";
            this.bteOCS.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteOCS.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteOCS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteOCS.Properties.ReadOnly = true;
            this.bteOCS.Size = new System.Drawing.Size(98, 20);
            this.bteOCS.TabIndex = 40;
            this.bteOCS.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteOCS_ButtonClick);
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Location = new System.Drawing.Point(6, 82);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(43, 13);
            this.labelControl12.TabIndex = 41;
            this.labelControl12.Text = "OCS N° :";
            // 
            // txtNumero
            // 
            this.txtNumero.EditValue = "000000";
            this.txtNumero.Location = new System.Drawing.Point(79, 26);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Properties.DisplayFormat.FormatString = "d6";
            this.txtNumero.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumero.Properties.EditFormat.FormatString = "d6";
            this.txtNumero.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNumero.Properties.Mask.EditMask = "d6";
            this.txtNumero.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNumero.Properties.Mask.ShowPlaceHolders = false;
            this.txtNumero.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNumero.Properties.MaxLength = 6;
            this.txtNumero.Size = new System.Drawing.Size(55, 20);
            this.txtNumero.TabIndex = 98;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.bteFactura);
            this.groupControl1.Controls.Add(this.txtMonto);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lkpMoneda);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.bteProyecto);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtNumero);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.bteOCS);
            this.groupControl1.Controls.Add(this.bteCCosto);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.bteProveedores);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.lkpSituacion);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.dteFecha);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(599, 108);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Datos";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Location = new System.Drawing.Point(426, 82);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(44, 13);
            this.labelControl5.TabIndex = 106;
            this.labelControl5.Text = "Factura :";
            // 
            // bteFactura
            // 
            this.bteFactura.Location = new System.Drawing.Point(476, 79);
            this.bteFactura.Name = "bteFactura";
            this.bteFactura.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteFactura.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteFactura.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteFactura.Properties.ReadOnly = true;
            this.bteFactura.Size = new System.Drawing.Size(111, 20);
            this.bteFactura.TabIndex = 105;
            this.bteFactura.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteFactura_ButtonClick);
            // 
            // txtMonto
            // 
            this.txtMonto.EditValue = "0";
            this.txtMonto.Location = new System.Drawing.Point(355, 79);
            this.txtMonto.MenuManager = this.barManager1;
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Properties.Mask.EditMask = "n2";
            this.txtMonto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMonto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMonto.Size = new System.Drawing.Size(62, 20);
            this.txtMonto.TabIndex = 104;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(316, 82);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(37, 13);
            this.labelControl4.TabIndex = 103;
            this.labelControl4.Text = "Monto :";
            // 
            // lkpMoneda
            // 
            this.lkpMoneda.Location = new System.Drawing.Point(210, 79);
            this.lkpMoneda.Name = "lkpMoneda";
            this.lkpMoneda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpMoneda.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMoneda.Properties.NullText = "";
            this.lkpMoneda.Size = new System.Drawing.Size(84, 20);
            this.lkpMoneda.TabIndex = 101;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(160, 82);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(45, 13);
            this.labelControl3.TabIndex = 102;
            this.labelControl3.Text = "Moneda :";
            // 
            // bteProyecto
            // 
            this.bteProyecto.Location = new System.Drawing.Point(331, 53);
            this.bteProyecto.Name = "bteProyecto";
            this.bteProyecto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteProyecto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteProyecto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteProyecto.Properties.ReadOnly = true;
            this.bteProyecto.Size = new System.Drawing.Size(116, 20);
            this.bteProyecto.TabIndex = 100;
            this.bteProyecto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteProyecto_ButtonClick);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Location = new System.Drawing.Point(280, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 99;
            this.labelControl2.Text = "Proyecto :";
            // 
            // frmManteGarantiaProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 135);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximumSize = new System.Drawing.Size(615, 500);
            this.MinimumSize = new System.Drawing.Size(615, 166);
            this.Name = "frmManteGarantiaProveedores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Registro de Garantia Proveedores";
            this.Load += new System.EventHandler(this.frmMantePersonal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProveedores.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteOCS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteFactura.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProyecto.Properties)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.LookUpEdit lkpMoneda;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.ButtonEdit bteProyecto;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtNumero;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        public DevExpress.XtraEditors.ButtonEdit bteOCS;
        public DevExpress.XtraEditors.ButtonEdit bteCCosto;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.ButtonEdit bteProveedores;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        public DevExpress.XtraEditors.LookUpEdit lkpSituacion;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.DateEdit dteFecha;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.ButtonEdit bteFactura;
        public DevExpress.XtraEditors.TextEdit txtMonto;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}