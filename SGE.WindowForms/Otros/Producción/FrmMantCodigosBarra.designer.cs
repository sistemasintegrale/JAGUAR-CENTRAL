namespace SGE.WindowForms.Otros.Produccion
{
    partial class FrmMantCodigosBarra
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.mnuCodigoBarra = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kardexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registroDeCantidadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnSalir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.grpNotaIngreso = new DevExpress.XtraEditors.GroupControl();
            this.gcDetallecodigoBarra = new DevExpress.XtraEditors.GroupControl();
            this.LkpUnidad = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.btnmodelo = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.LkpMarca = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnAgregar = new DevExpress.XtraEditors.SimpleButton();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtgrupo = new DevExpress.XtraEditors.TextEdit();
            this.txtgrupo2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.pcDetalle = new DevExpress.XtraEditors.PanelControl();
            this.btnGenerar = new DevExpress.XtraEditors.SimpleButton();
            this.dgdCodigo = new DevExpress.XtraGrid.GridControl();
            this.viewCodigos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtpFecha = new DevExpress.XtraEditors.DateEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtObservaciones = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNroTransferencia = new DevExpress.XtraEditors.TextEdit();
            this.mnuCodigoBarra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpNotaIngreso)).BeginInit();
            this.grpNotaIngreso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetallecodigoBarra)).BeginInit();
            this.gcDetallecodigoBarra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpUnidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnmodelo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpMarca.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtgrupo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtgrupo2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdCodigo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCodigos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFecha.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservaciones.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroTransferencia.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuCodigoBarra
            // 
            this.mnuCodigoBarra.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kardexToolStripMenuItem,
            this.imprimirToolStripMenuItem,
            this.registroDeCantidadToolStripMenuItem});
            this.mnuCodigoBarra.Name = "contextMenuStrip1";
            this.mnuCodigoBarra.Size = new System.Drawing.Size(185, 70);
            // 
            // kardexToolStripMenuItem
            // 
            this.kardexToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_check;
            this.kardexToolStripMenuItem.Name = "kardexToolStripMenuItem";
            this.kardexToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.kardexToolStripMenuItem.Text = "Nuevo";
            this.kardexToolStripMenuItem.Click += new System.EventHandler(this.kardexToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.imprimirToolStripMenuItem.Text = "Eliminar";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // registroDeCantidadToolStripMenuItem
            // 
            this.registroDeCantidadToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.registroDeCantidadToolStripMenuItem.Name = "registroDeCantidadToolStripMenuItem";
            this.registroDeCantidadToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.registroDeCantidadToolStripMenuItem.Text = "Registro de Cantidad";
            this.registroDeCantidadToolStripMenuItem.Click += new System.EventHandler(this.registroDeCantidadToolStripMenuItem_Click);
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
            this.btnSalir});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSalir)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnGuardar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnGuardar.Caption = "Guardar";
            this.btnGuardar.Id = 0;
            this.btnGuardar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // btnSalir
            // 
            this.btnSalir.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnSalir.Caption = "Salir";
            this.btnSalir.Id = 1;
            this.btnSalir.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.Refresh;
            this.btnSalir.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSalir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSalir_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(713, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 299);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(713, 38);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 299);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(713, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 299);
            // 
            // grpNotaIngreso
            // 
            this.grpNotaIngreso.Controls.Add(this.gcDetallecodigoBarra);
            this.grpNotaIngreso.Controls.Add(this.pcDetalle);
            this.grpNotaIngreso.Controls.Add(this.btnGenerar);
            this.grpNotaIngreso.Controls.Add(this.dgdCodigo);
            this.grpNotaIngreso.Controls.Add(this.dtpFecha);
            this.grpNotaIngreso.Controls.Add(this.labelControl10);
            this.grpNotaIngreso.Controls.Add(this.txtObservaciones);
            this.grpNotaIngreso.Controls.Add(this.labelControl6);
            this.grpNotaIngreso.Controls.Add(this.labelControl3);
            this.grpNotaIngreso.Controls.Add(this.txtNroTransferencia);
            this.grpNotaIngreso.Location = new System.Drawing.Point(0, -4);
            this.grpNotaIngreso.Name = "grpNotaIngreso";
            this.grpNotaIngreso.Size = new System.Drawing.Size(711, 303);
            this.grpNotaIngreso.TabIndex = 38;
            this.grpNotaIngreso.Text = "Códigos de Barra";
            // 
            // gcDetallecodigoBarra
            // 
            this.gcDetallecodigoBarra.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcDetallecodigoBarra.Controls.Add(this.LkpUnidad);
            this.gcDetallecodigoBarra.Controls.Add(this.labelControl17);
            this.gcDetallecodigoBarra.Controls.Add(this.btnmodelo);
            this.gcDetallecodigoBarra.Controls.Add(this.labelControl15);
            this.gcDetallecodigoBarra.Controls.Add(this.LkpMarca);
            this.gcDetallecodigoBarra.Controls.Add(this.labelControl4);
            this.gcDetallecodigoBarra.Controls.Add(this.simpleButton1);
            this.gcDetallecodigoBarra.Controls.Add(this.btnAgregar);
            this.gcDetallecodigoBarra.Controls.Add(this.txtDescripcion);
            this.gcDetallecodigoBarra.Controls.Add(this.labelControl2);
            this.gcDetallecodigoBarra.Controls.Add(this.txtgrupo);
            this.gcDetallecodigoBarra.Controls.Add(this.txtgrupo2);
            this.gcDetallecodigoBarra.Controls.Add(this.labelControl14);
            this.gcDetallecodigoBarra.Location = new System.Drawing.Point(71, 131);
            this.gcDetallecodigoBarra.Name = "gcDetallecodigoBarra";
            this.gcDetallecodigoBarra.Size = new System.Drawing.Size(575, 118);
            this.gcDetallecodigoBarra.TabIndex = 51;
            this.gcDetallecodigoBarra.Text = "Datos Detalle";
            // 
            // LkpUnidad
            // 
            this.LkpUnidad.Enabled = false;
            this.LkpUnidad.Location = new System.Drawing.Point(104, 85);
            this.LkpUnidad.Name = "LkpUnidad";
            this.LkpUnidad.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpUnidad.Properties.NullText = "";
            this.LkpUnidad.Size = new System.Drawing.Size(129, 20);
            this.LkpUnidad.TabIndex = 105;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(13, 88);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(85, 13);
            this.labelControl17.TabIndex = 104;
            this.labelControl17.Text = "Unidad de Medida";
            // 
            // btnmodelo
            // 
            this.btnmodelo.Location = new System.Drawing.Point(271, 24);
            this.btnmodelo.Name = "btnmodelo";
            this.btnmodelo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnmodelo.Properties.ReadOnly = true;
            this.btnmodelo.Size = new System.Drawing.Size(142, 20);
            this.btnmodelo.TabIndex = 69;
            this.btnmodelo.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnmodelo_ButtonClick);
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(224, 27);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(41, 13);
            this.labelControl15.TabIndex = 68;
            this.labelControl15.Text = "Modelo :";
            // 
            // LkpMarca
            // 
            this.LkpMarca.Location = new System.Drawing.Point(61, 25);
            this.LkpMarca.Name = "LkpMarca";
            this.LkpMarca.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpMarca.Properties.NullText = "";
            this.LkpMarca.Size = new System.Drawing.Size(143, 20);
            this.LkpMarca.TabIndex = 66;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(17, 27);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 13);
            this.labelControl4.TabIndex = 67;
            this.labelControl4.Text = "Marca :";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(489, 92);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 20);
            this.simpleButton1.TabIndex = 103;
            this.simpleButton1.Text = "Salir";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(423, 92);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(65, 20);
            this.btnAgregar.TabIndex = 102;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Location = new System.Drawing.Point(81, 51);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(460, 20);
            this.txtDescripcion.TabIndex = 63;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 54);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 13);
            this.labelControl2.TabIndex = 62;
            this.labelControl2.Text = "Descripción:";
            // 
            // txtgrupo
            // 
            this.txtgrupo.EditValue = "00";
            this.txtgrupo.Enabled = false;
            this.txtgrupo.Location = new System.Drawing.Point(503, 24);
            this.txtgrupo.Name = "txtgrupo";
            this.txtgrupo.Properties.DisplayFormat.FormatString = "d2";
            this.txtgrupo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtgrupo.Properties.EditFormat.FormatString = "d2";
            this.txtgrupo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtgrupo.Properties.Mask.EditMask = "d2";
            this.txtgrupo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtgrupo.Properties.Mask.ShowPlaceHolders = false;
            this.txtgrupo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtgrupo.Size = new System.Drawing.Size(29, 20);
            this.txtgrupo.TabIndex = 57;
            // 
            // txtgrupo2
            // 
            this.txtgrupo2.EditValue = "00";
            this.txtgrupo2.Enabled = false;
            this.txtgrupo2.Location = new System.Drawing.Point(535, 24);
            this.txtgrupo2.Name = "txtgrupo2";
            this.txtgrupo2.Properties.DisplayFormat.FormatString = "d2";
            this.txtgrupo2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtgrupo2.Properties.EditFormat.FormatString = "d2";
            this.txtgrupo2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtgrupo2.Properties.Mask.EditMask = "d2";
            this.txtgrupo2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtgrupo2.Properties.Mask.ShowPlaceHolders = false;
            this.txtgrupo2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtgrupo2.Size = new System.Drawing.Size(29, 20);
            this.txtgrupo2.TabIndex = 58;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(448, 27);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(31, 13);
            this.labelControl14.TabIndex = 59;
            this.labelControl14.Text = "Serie :";
            // 
            // pcDetalle
            // 
            this.pcDetalle.Location = new System.Drawing.Point(71, 141);
            this.pcDetalle.Name = "pcDetalle";
            this.pcDetalle.Size = new System.Drawing.Size(579, 108);
            this.pcDetalle.TabIndex = 71;
            this.pcDetalle.Visible = false;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(609, 31);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(97, 63);
            this.btnGenerar.TabIndex = 70;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.Visible = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click_1);
            // 
            // dgdCodigo
            // 
            this.dgdCodigo.ContextMenuStrip = this.mnuCodigoBarra;
            gridLevelNode1.RelationName = "Level1";
            gridLevelNode2.RelationName = "Level2";
            this.dgdCodigo.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1,
            gridLevelNode2});
            this.dgdCodigo.Location = new System.Drawing.Point(5, 103);
            this.dgdCodigo.MainView = this.viewCodigos;
            this.dgdCodigo.Name = "dgdCodigo";
            this.dgdCodigo.Size = new System.Drawing.Size(701, 195);
            this.dgdCodigo.TabIndex = 5;
            this.dgdCodigo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewCodigos});
            // 
            // viewCodigos
            // 
            this.viewCodigos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.viewCodigos.GridControl = this.dgdCodigo;
            this.viewCodigos.Name = "viewCodigos";
            this.viewCodigos.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Código";
            this.gridColumn1.FieldName = "pr_vcodigo_externo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 122;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Serie";
            this.gridColumn2.FieldName = "picbd_rango_talla";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 64;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Descripción";
            this.gridColumn3.FieldName = "pr_vdescripcion_producto";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 497;
            // 
            // dtpFecha
            // 
            this.dtpFecha.EditValue = null;
            this.dtpFecha.EnterMoveNextControl = true;
            this.dtpFecha.Location = new System.Drawing.Point(494, 31);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpFecha.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dtpFecha.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.dtpFecha.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dtpFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFecha.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpFecha.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpFecha.Size = new System.Drawing.Size(109, 20);
            this.dtpFecha.TabIndex = 1;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(400, 35);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(29, 13);
            this.labelControl10.TabIndex = 65;
            this.labelControl10.Text = "Fecha";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.EditValue = "";
            this.txtObservaciones.Location = new System.Drawing.Point(127, 52);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtObservaciones.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtObservaciones.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtObservaciones.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtObservaciones.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtObservaciones.Properties.MaxLength = 50;
            this.txtObservaciones.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtObservaciones.Size = new System.Drawing.Size(476, 20);
            this.txtObservaciones.TabIndex = 4;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(15, 56);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(71, 13);
            this.labelControl6.TabIndex = 62;
            this.labelControl6.Text = "Observaciones";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(86, 13);
            this.labelControl3.TabIndex = 57;
            this.labelControl3.Text = "Nro de Impresión:";
            // 
            // txtNroTransferencia
            // 
            this.txtNroTransferencia.EditValue = "000000";
            this.txtNroTransferencia.Enabled = false;
            this.txtNroTransferencia.EnterMoveNextControl = true;
            this.txtNroTransferencia.Location = new System.Drawing.Point(127, 30);
            this.txtNroTransferencia.Name = "txtNroTransferencia";
            this.txtNroTransferencia.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNroTransferencia.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtNroTransferencia.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtNroTransferencia.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtNroTransferencia.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtNroTransferencia.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtNroTransferencia.Properties.DisplayFormat.FormatString = "d6";
            this.txtNroTransferencia.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNroTransferencia.Properties.EditFormat.FormatString = "d6";
            this.txtNroTransferencia.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNroTransferencia.Properties.Mask.EditMask = "d6";
            this.txtNroTransferencia.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNroTransferencia.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNroTransferencia.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNroTransferencia.Size = new System.Drawing.Size(52, 20);
            this.txtNroTransferencia.TabIndex = 0;
            // 
            // FrmMantCodigosBarra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 337);
            this.Controls.Add(this.grpNotaIngreso);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmMantCodigosBarra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generación de Código de Barras";
            this.Load += new System.EventHandler(this.FrmMantCodigosBarra_Load);
            this.mnuCodigoBarra.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpNotaIngreso)).EndInit();
            this.grpNotaIngreso.ResumeLayout(false);
            this.grpNotaIngreso.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetallecodigoBarra)).EndInit();
            this.gcDetallecodigoBarra.ResumeLayout(false);
            this.gcDetallecodigoBarra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpUnidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnmodelo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpMarca.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtgrupo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtgrupo2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdCodigo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewCodigos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFecha.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservaciones.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroTransferencia.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnSalir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem btnGuardar;
        private System.Windows.Forms.ContextMenuStrip mnuCodigoBarra;
        private System.Windows.Forms.ToolStripMenuItem kardexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registroDeCantidadToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl grpNotaIngreso;
        private DevExpress.XtraEditors.GroupControl gcDetallecodigoBarra;
        public DevExpress.XtraEditors.LookUpEdit LkpUnidad;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        public DevExpress.XtraEditors.ButtonEdit btnmodelo;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        public DevExpress.XtraEditors.LookUpEdit LkpMarca;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnAgregar;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtgrupo;
        public DevExpress.XtraEditors.TextEdit txtgrupo2;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.PanelControl pcDetalle;
        public DevExpress.XtraEditors.SimpleButton btnGenerar;
        private DevExpress.XtraGrid.GridControl dgdCodigo;
        private DevExpress.XtraGrid.Views.Grid.GridView viewCodigos;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        public DevExpress.XtraEditors.DateEdit dtpFecha;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        public DevExpress.XtraEditors.TextEdit txtObservaciones;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtNroTransferencia;
    }
}