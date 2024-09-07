namespace SGE.WindowForms.Otros.Compras
{
    partial class frmManteDocRecepcionCompraSuministros
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
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.grdGuiaRemision = new DevExpress.XtraGrid.GridControl();
            this.viewGuiaRemision = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sfdTXT = new System.Windows.Forms.SaveFileDialog();
            this.sfdRuta = new System.Windows.Forms.SaveFileDialog();
            this.gcRemision = new DevExpress.XtraEditors.GroupControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumero2 = new DevExpress.XtraEditors.TextEdit();
            this.lkpMotivo = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpTipoDoc = new DevExpress.XtraEditors.LookUpEdit();
            this.lblMotivo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.btnAlmacen = new DevExpress.XtraEditors.ButtonEdit();
            this.txtObservaciones = new DevExpress.XtraEditors.TextEdit();
            this.lblDestino = new DevExpress.XtraEditors.LabelControl();
            this.lblAlmacen = new DevExpress.XtraEditors.LabelControl();
            this.txtSerie = new DevExpress.XtraEditors.TextEdit();
            this.lblNumero = new DevExpress.XtraEditors.LabelControl();
            this.lkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.lblFecha = new DevExpress.XtraEditors.LabelControl();
            this.txtNumero = new DevExpress.XtraEditors.TextEdit();
            this.dteFecha = new DevExpress.XtraEditors.DateEdit();
            this.lblDestinatario = new DevExpress.XtraEditors.LabelControl();
            this.btnProveedor = new DevExpress.XtraEditors.ButtonEdit();
            this.lblSituacion = new DevExpress.XtraEditors.LabelControl();
            this.mnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGuiaRemision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewGuiaRemision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRemision)).BeginInit();
            this.gcRemision.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMotivo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoDoc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAlmacen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservaciones.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProveedor.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.mnu.Name = "contextMenuStrip1";
            this.mnu.Size = new System.Drawing.Size(126, 70);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
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
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(766, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 535);
            this.barDockControlBottom.Size = new System.Drawing.Size(766, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 535);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(766, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 535);
            // 
            // grdGuiaRemision
            // 
            this.grdGuiaRemision.ContextMenuStrip = this.mnu;
            this.grdGuiaRemision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGuiaRemision.Location = new System.Drawing.Point(0, 111);
            this.grdGuiaRemision.MainView = this.viewGuiaRemision;
            this.grdGuiaRemision.Name = "grdGuiaRemision";
            this.grdGuiaRemision.Size = new System.Drawing.Size(766, 424);
            this.grdGuiaRemision.TabIndex = 10;
            this.grdGuiaRemision.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewGuiaRemision});
            // 
            // viewGuiaRemision
            // 
            this.viewGuiaRemision.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.viewGuiaRemision.GridControl = this.grdGuiaRemision;
            this.viewGuiaRemision.GroupPanelText = " ";
            this.viewGuiaRemision.Name = "viewGuiaRemision";
            this.viewGuiaRemision.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Item";
            this.gridColumn1.DisplayFormat.FormatString = "d3";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "drcd_iitem";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 60;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Código";
            this.gridColumn2.FieldName = "strCodProducto";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 121;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Cant.";
            this.gridColumn5.FieldName = "drcd_ncantidad";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 98;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Medida";
            this.gridColumn6.FieldName = "strDesUM";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 110;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Descripción";
            this.gridColumn7.FieldName = "drcd_vdescripcion_item";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 359;
            // 
            // gcRemision
            // 
            this.gcRemision.Controls.Add(this.groupBox1);
            this.gcRemision.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcRemision.Location = new System.Drawing.Point(0, 0);
            this.gcRemision.Name = "gcRemision";
            this.gcRemision.Size = new System.Drawing.Size(766, 111);
            this.gcRemision.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.txtNumero2);
            this.groupBox1.Controls.Add(this.lkpMotivo);
            this.groupBox1.Controls.Add(this.lkpTipoDoc);
            this.groupBox1.Controls.Add(this.lblMotivo);
            this.groupBox1.Controls.Add(this.labelControl26);
            this.groupBox1.Controls.Add(this.btnAlmacen);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.lblDestino);
            this.groupBox1.Controls.Add(this.lblAlmacen);
            this.groupBox1.Controls.Add(this.txtSerie);
            this.groupBox1.Controls.Add(this.lblNumero);
            this.groupBox1.Controls.Add(this.lkpSituacion);
            this.groupBox1.Controls.Add(this.lblFecha);
            this.groupBox1.Controls.Add(this.txtNumero);
            this.groupBox1.Controls.Add(this.dteFecha);
            this.groupBox1.Controls.Add(this.lblDestinatario);
            this.groupBox1.Controls.Add(this.btnProveedor);
            this.groupBox1.Controls.Add(this.lblSituacion);
            this.groupBox1.Location = new System.Drawing.Point(5, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 98);
            this.groupBox1.TabIndex = 82;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Principales";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(293, 20);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(4, 13);
            this.labelControl3.TabIndex = 110;
            this.labelControl3.Text = "/";
            // 
            // txtNumero2
            // 
            this.txtNumero2.EditValue = "";
            this.txtNumero2.Location = new System.Drawing.Point(303, 18);
            this.txtNumero2.Name = "txtNumero2";
            this.txtNumero2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumero2.Properties.Mask.ShowPlaceHolders = false;
            this.txtNumero2.Properties.MaxLength = 80;
            this.txtNumero2.Size = new System.Drawing.Size(109, 20);
            this.txtNumero2.TabIndex = 109;
            // 
            // lkpMotivo
            // 
            this.lkpMotivo.Enabled = false;
            this.lkpMotivo.Location = new System.Drawing.Point(411, 44);
            this.lkpMotivo.Name = "lkpMotivo";
            this.lkpMotivo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMotivo.Properties.NullText = "";
            this.lkpMotivo.Size = new System.Drawing.Size(114, 20);
            this.lkpMotivo.TabIndex = 101;
            this.lkpMotivo.EditValueChanged += new System.EventHandler(this.lkpMotivo_EditValueChanged_2);
            // 
            // lkpTipoDoc
            // 
            this.lkpTipoDoc.Location = new System.Drawing.Point(68, 18);
            this.lkpTipoDoc.MenuManager = this.barManager1;
            this.lkpTipoDoc.Name = "lkpTipoDoc";
            this.lkpTipoDoc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoDoc.Properties.NullText = "";
            this.lkpTipoDoc.Size = new System.Drawing.Size(57, 20);
            this.lkpTipoDoc.TabIndex = 107;
            this.lkpTipoDoc.EditValueChanged += new System.EventHandler(this.lkpTipoDoc_EditValueChanged);
            // 
            // lblMotivo
            // 
            this.lblMotivo.Location = new System.Drawing.Point(369, 47);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(36, 13);
            this.lblMotivo.TabIndex = 100;
            this.lblMotivo.Text = "Motivo:";
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(14, 21);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(48, 13);
            this.labelControl26.TabIndex = 108;
            this.labelControl26.Text = "Tipo Doc :";
            // 
            // btnAlmacen
            // 
            this.btnAlmacen.Location = new System.Drawing.Point(605, 44);
            this.btnAlmacen.Name = "btnAlmacen";
            this.btnAlmacen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.btnAlmacen.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.btnAlmacen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnAlmacen.Properties.ReadOnly = true;
            this.btnAlmacen.Size = new System.Drawing.Size(138, 20);
            this.btnAlmacen.TabIndex = 98;
            this.btnAlmacen.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnAlmacen_ButtonClick);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.EditValue = "";
            this.txtObservaciones.Enabled = false;
            this.txtObservaciones.Location = new System.Drawing.Point(100, 70);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservaciones.Properties.Mask.ShowPlaceHolders = false;
            this.txtObservaciones.Properties.MaxLength = 80;
            this.txtObservaciones.Size = new System.Drawing.Size(263, 20);
            this.txtObservaciones.TabIndex = 100;
            // 
            // lblDestino
            // 
            this.lblDestino.Location = new System.Drawing.Point(17, 73);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(78, 13);
            this.lblDestino.TabIndex = 87;
            this.lblDestino.Text = "Observaciones :";
            // 
            // lblAlmacen
            // 
            this.lblAlmacen.Location = new System.Drawing.Point(542, 47);
            this.lblAlmacen.Name = "lblAlmacen";
            this.lblAlmacen.Size = new System.Drawing.Size(47, 13);
            this.lblAlmacen.TabIndex = 88;
            this.lblAlmacen.Text = "Almacén :";
            // 
            // txtSerie
            // 
            this.txtSerie.EditValue = "";
            this.txtSerie.Enabled = false;
            this.txtSerie.Location = new System.Drawing.Point(177, 18);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSerie.Properties.Mask.ShowPlaceHolders = false;
            this.txtSerie.Properties.MaxLength = 3;
            this.txtSerie.Size = new System.Drawing.Size(45, 20);
            this.txtSerie.TabIndex = 93;
            this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSerie_KeyDown);
            // 
            // lblNumero
            // 
            this.lblNumero.Location = new System.Drawing.Point(131, 21);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(40, 13);
            this.lblNumero.TabIndex = 86;
            this.lblNumero.Text = "N° Guia:";
            // 
            // lkpSituacion
            // 
            this.lkpSituacion.Enabled = false;
            this.lkpSituacion.Location = new System.Drawing.Point(629, 18);
            this.lkpSituacion.Name = "lkpSituacion";
            this.lkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSituacion.Properties.NullText = "";
            this.lkpSituacion.Size = new System.Drawing.Size(114, 20);
            this.lkpSituacion.TabIndex = 97;
            // 
            // lblFecha
            // 
            this.lblFecha.Location = new System.Drawing.Point(428, 21);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(33, 13);
            this.lblFecha.TabIndex = 92;
            this.lblFecha.Text = "Fecha:";
            // 
            // txtNumero
            // 
            this.txtNumero.EditValue = "0000000";
            this.txtNumero.Enabled = false;
            this.txtNumero.Location = new System.Drawing.Point(228, 18);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumero.Properties.Mask.EditMask = "d7";
            this.txtNumero.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNumero.Properties.Mask.ShowPlaceHolders = false;
            this.txtNumero.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNumero.Properties.MaxLength = 7;
            this.txtNumero.Size = new System.Drawing.Size(59, 20);
            this.txtNumero.TabIndex = 94;
            this.txtNumero.Leave += new System.EventHandler(this.txtNumero_Leave);
            // 
            // dteFecha
            // 
            this.dteFecha.EditValue = null;
            this.dteFecha.Location = new System.Drawing.Point(467, 18);
            this.dteFecha.Name = "dteFecha";
            this.dteFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFecha.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFecha.Size = new System.Drawing.Size(102, 20);
            this.dteFecha.TabIndex = 95;
            // 
            // lblDestinatario
            // 
            this.lblDestinatario.Location = new System.Drawing.Point(17, 48);
            this.lblDestinatario.Name = "lblDestinatario";
            this.lblDestinatario.Size = new System.Drawing.Size(57, 13);
            this.lblDestinatario.TabIndex = 90;
            this.lblDestinatario.Text = "Proveedor :";
            // 
            // btnProveedor
            // 
            this.btnProveedor.EditValue = "";
            this.btnProveedor.Location = new System.Drawing.Point(80, 44);
            this.btnProveedor.Name = "btnProveedor";
            this.btnProveedor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.btnProveedor.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.btnProveedor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnProveedor.Properties.MaxLength = 80;
            this.btnProveedor.Properties.ReadOnly = true;
            this.btnProveedor.Size = new System.Drawing.Size(283, 20);
            this.btnProveedor.TabIndex = 96;
            this.btnProveedor.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnProveedor_ButtonClick);
            this.btnProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCliente_KeyDown);
            // 
            // lblSituacion
            // 
            this.lblSituacion.Location = new System.Drawing.Point(575, 21);
            this.lblSituacion.Name = "lblSituacion";
            this.lblSituacion.Size = new System.Drawing.Size(47, 13);
            this.lblSituacion.TabIndex = 91;
            this.lblSituacion.Text = "Situación:";
            // 
            // frmManteDocRecepcionCompraSuministros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 562);
            this.Controls.Add(this.grdGuiaRemision);
            this.Controls.Add(this.gcRemision);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.KeyPreview = true;
            this.Name = "frmManteDocRecepcionCompraSuministros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Guía de Remisión";
            this.Load += new System.EventHandler(this.frmManteGuiaRemision_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmManteGuiaRemision_KeyDown);
            this.mnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGuiaRemision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewGuiaRemision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRemision)).EndInit();
            this.gcRemision.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMotivo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoDoc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAlmacen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservaciones.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProveedor.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ContextMenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl grdGuiaRemision;
        private DevExpress.XtraGrid.Views.Grid.GridView viewGuiaRemision;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        public DevExpress.XtraBars.BarButtonItem btnGuardar;
        private System.Windows.Forms.SaveFileDialog sfdTXT;
        private System.Windows.Forms.SaveFileDialog sfdRuta;
        private DevExpress.XtraEditors.GroupControl gcRemision;
        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.ButtonEdit btnAlmacen;
        public DevExpress.XtraEditors.TextEdit txtObservaciones;
        private DevExpress.XtraEditors.LabelControl lblDestino;
        private DevExpress.XtraEditors.LabelControl lblAlmacen;
        public DevExpress.XtraEditors.TextEdit txtSerie;
        private DevExpress.XtraEditors.LabelControl lblNumero;
        public DevExpress.XtraEditors.LookUpEdit lkpSituacion;
        private DevExpress.XtraEditors.LabelControl lblFecha;
        public DevExpress.XtraEditors.TextEdit txtNumero;
        public DevExpress.XtraEditors.DateEdit dteFecha;
        private DevExpress.XtraEditors.LabelControl lblDestinatario;
        public DevExpress.XtraEditors.ButtonEdit btnProveedor;
        private DevExpress.XtraEditors.LabelControl lblSituacion;
        public DevExpress.XtraEditors.LookUpEdit lkpMotivo;
        private DevExpress.XtraEditors.LabelControl lblMotivo;
        public DevExpress.XtraEditors.LookUpEdit lkpTipoDoc;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtNumero2;
    }
}