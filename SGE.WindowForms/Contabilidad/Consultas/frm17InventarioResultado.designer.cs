namespace SGE.WindowForms.Contabilidad.Consultas
{
    partial class frm17InventarioResultado
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
            this.gcFiltro = new DevExpress.XtraEditors.GroupControl();
            this.cbActivarFiltro = new System.Windows.Forms.CheckBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.grdExcel = new DevExpress.XtraGrid.GridControl();
            this.gvExcelSD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnLimpiar = new DevExpress.XtraEditors.SimpleButton();
            this.lkpMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpDigitos = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblMes = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.lkpMeses = new DevExpress.XtraEditors.LookUpEdit();
            this.lblCuentaF = new DevExpress.XtraEditors.LabelControl();
            this.lblCuentaI = new DevExpress.XtraEditors.LabelControl();
            this.grdAuxiliar = new DevExpress.XtraGrid.GridControl();
            this.mnuComprobantes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.movimientosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarAExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewAuxiliar = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.sfdRuta = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gcFiltro)).BeginInit();
            this.gcFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExcelSD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpDigitos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMeses.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAuxiliar)).BeginInit();
            this.mnuComprobantes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewAuxiliar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcFiltro
            // 
            this.gcFiltro.Controls.Add(this.cbActivarFiltro);
            this.gcFiltro.Controls.Add(this.panelControl1);
            this.gcFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcFiltro.Location = new System.Drawing.Point(0, 0);
            this.gcFiltro.Name = "gcFiltro";
            this.gcFiltro.Size = new System.Drawing.Size(911, 128);
            this.gcFiltro.TabIndex = 2;
            this.gcFiltro.Text = "Criterios de Búsqueda";
            // 
            // cbActivarFiltro
            // 
            this.cbActivarFiltro.AutoSize = true;
            this.cbActivarFiltro.BackColor = System.Drawing.Color.Transparent;
            this.cbActivarFiltro.Location = new System.Drawing.Point(5, 107);
            this.cbActivarFiltro.Name = "cbActivarFiltro";
            this.cbActivarFiltro.Size = new System.Drawing.Size(92, 17);
            this.cbActivarFiltro.TabIndex = 105;
            this.cbActivarFiltro.Text = "Activar Filtros";
            this.cbActivarFiltro.UseVisualStyleBackColor = false;
            this.cbActivarFiltro.CheckedChanged += new System.EventHandler(this.cbActivarFiltro_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.grdExcel);
            this.panelControl1.Controls.Add(this.btnLimpiar);
            this.panelControl1.Controls.Add(this.lkpMoneda);
            this.panelControl1.Controls.Add(this.lkpDigitos);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.lblMes);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnBuscar);
            this.panelControl1.Controls.Add(this.lkpMeses);
            this.panelControl1.Controls.Add(this.lblCuentaF);
            this.panelControl1.Controls.Add(this.lblCuentaI);
            this.panelControl1.Location = new System.Drawing.Point(5, 25);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(838, 80);
            this.panelControl1.TabIndex = 89;
            // 
            // grdExcel
            // 
            this.grdExcel.Location = new System.Drawing.Point(762, 10);
            this.grdExcel.MainView = this.gvExcelSD;
            this.grdExcel.MenuManager = this.barManager1;
            this.grdExcel.Name = "grdExcel";
            this.grdExcel.Size = new System.Drawing.Size(31, 28);
            this.grdExcel.TabIndex = 109;
            this.grdExcel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExcelSD});
            this.grdExcel.Visible = false;
            // 
            // gvExcelSD
            // 
            this.gvExcelSD.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gvExcelSD.GridControl = this.grdExcel;
            this.gvExcelSD.Name = "gvExcelSD";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "CUENTA";
            this.gridColumn3.FieldName = "viid_cuenta_contable";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "DESCRIPCIÓN";
            this.gridColumn4.FieldName = "vdescripcion_cuenta_contable";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "SALDO ANTERIOR";
            this.gridColumn9.DisplayFormat.FormatString = "n2";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "ctacc_iid_cuenta_contable_acumulado_sol";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "DÉBITOS";
            this.gridColumn10.FieldName = "nmto_tot_debe_sol";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "CRÉDITOS";
            this.gridColumn11.FieldName = "nmto_tot_haber_sol";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "SALDO ACTUAL";
            this.gridColumn12.DisplayFormat.FormatString = "n2";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn12.FieldName = "ctacc_iid_cuenta_contable_saldo_sol";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 5;
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
            this.btnRefresh});
            this.barManager1.MaxItemId = 1;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRefresh)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnRefresh.Caption = "Refresh";
            this.btnRefresh.Glyph = global::SGE.WindowForms.Properties.Resources.btnRefresh;
            this.btnRefresh.Id = 0;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(911, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 398);
            this.barDockControlBottom.Size = new System.Drawing.Size(911, 26);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 398);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(911, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 398);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::SGE.WindowForms.Properties.Resources.broom;
            this.btnLimpiar.Location = new System.Drawing.Point(273, 30);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 90;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lkpMoneda
            // 
            this.lkpMoneda.Location = new System.Drawing.Point(117, 30);
            this.lkpMoneda.Name = "lkpMoneda";
            this.lkpMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMoneda.Properties.NullText = "";
            this.lkpMoneda.Size = new System.Drawing.Size(144, 20);
            this.lkpMoneda.TabIndex = 97;
            // 
            // lkpDigitos
            // 
            this.lkpDigitos.Location = new System.Drawing.Point(117, 53);
            this.lkpDigitos.Name = "lkpDigitos";
            this.lkpDigitos.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpDigitos.Properties.NullText = "";
            this.lkpDigitos.Size = new System.Drawing.Size(144, 20);
            this.lkpDigitos.TabIndex = 96;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(111, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(4, 13);
            this.labelControl2.TabIndex = 95;
            this.labelControl2.Text = ":";
            // 
            // lblMes
            // 
            this.lblMes.Location = new System.Drawing.Point(6, 11);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(79, 13);
            this.lblMes.TabIndex = 94;
            this.lblMes.Text = "Saldos al Mes de";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(111, 57);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(4, 13);
            this.labelControl3.TabIndex = 92;
            this.labelControl3.Text = ":";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(111, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(4, 13);
            this.labelControl1.TabIndex = 90;
            this.labelControl1.Text = ":";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::SGE.WindowForms.Properties.Resources.page_white_magnify;
            this.btnBuscar.Location = new System.Drawing.Point(273, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 89;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lkpMeses
            // 
            this.lkpMeses.Location = new System.Drawing.Point(117, 7);
            this.lkpMeses.Name = "lkpMeses";
            this.lkpMeses.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMeses.Properties.NullText = "";
            this.lkpMeses.Size = new System.Drawing.Size(144, 20);
            this.lkpMeses.TabIndex = 1;
            // 
            // lblCuentaF
            // 
            this.lblCuentaF.Location = new System.Drawing.Point(6, 57);
            this.lblCuentaF.Name = "lblCuentaF";
            this.lblCuentaF.Size = new System.Drawing.Size(98, 13);
            this.lblCuentaF.TabIndex = 3;
            this.lblCuentaF.Text = "Resumen a # dígitos";
            // 
            // lblCuentaI
            // 
            this.lblCuentaI.Location = new System.Drawing.Point(6, 34);
            this.lblCuentaI.Name = "lblCuentaI";
            this.lblCuentaI.Size = new System.Drawing.Size(76, 13);
            this.lblCuentaI.TabIndex = 2;
            this.lblCuentaI.Text = "Tipo de Moneda";
            // 
            // grdAuxiliar
            // 
            this.grdAuxiliar.ContextMenuStrip = this.mnuComprobantes;
            this.grdAuxiliar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAuxiliar.Location = new System.Drawing.Point(0, 128);
            this.grdAuxiliar.MainView = this.viewAuxiliar;
            this.grdAuxiliar.Name = "grdAuxiliar";
            this.grdAuxiliar.Size = new System.Drawing.Size(911, 270);
            this.grdAuxiliar.TabIndex = 4;
            this.grdAuxiliar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewAuxiliar});
            // 
            // mnuComprobantes
            // 
            this.mnuComprobantes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.movimientosToolStripMenuItem,
            this.imprimirToolStripMenuItem,
            this.exportarAExcelToolStripMenuItem});
            this.mnuComprobantes.Name = "mnuComprobantes";
            this.mnuComprobantes.Size = new System.Drawing.Size(156, 92);
            // 
            // movimientosToolStripMenuItem
            // 
            this.movimientosToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_find;
            this.movimientosToolStripMenuItem.Name = "movimientosToolStripMenuItem";
            this.movimientosToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.movimientosToolStripMenuItem.Text = "Movimientos";
            this.movimientosToolStripMenuItem.Visible = false;
            this.movimientosToolStripMenuItem.Click += new System.EventHandler(this.movimientosToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // exportarAExcelToolStripMenuItem
            // 
            this.exportarAExcelToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_excel;
            this.exportarAExcelToolStripMenuItem.Name = "exportarAExcelToolStripMenuItem";
            this.exportarAExcelToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exportarAExcelToolStripMenuItem.Text = "Exportar a Excel";
            this.exportarAExcelToolStripMenuItem.Click += new System.EventHandler(this.exportarAExcelToolStripMenuItem_Click);
            // 
            // viewAuxiliar
            // 
            this.viewAuxiliar.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn2,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21,
            this.gridColumn22});
            this.viewAuxiliar.GridControl = this.grdAuxiliar;
            this.viewAuxiliar.GroupPanelText = " ";
            this.viewAuxiliar.Name = "viewAuxiliar";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Cuenta";
            this.gridColumn1.FieldName = "strNroCuenta";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 64;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Saldo Ant.";
            this.gridColumn5.DisplayFormat.FormatString = "n2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "ctacc_iid_cuenta_contable_acumulado_sol";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.Width = 81;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Debe";
            this.gridColumn6.DisplayFormat.FormatString = "n2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "vcocd_nmto_tot_debe_sol";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.AllowMove = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 81;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Haber";
            this.gridColumn7.DisplayFormat.FormatString = "n2";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "vcocd_nmto_tot_haber_sol";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMove = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 81;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Saldo Act.";
            this.gridColumn8.DisplayFormat.FormatString = "n2";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "ctacc_iid_cuenta_contable_saldo_sol";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.AllowMove = false;
            this.gridColumn8.Width = 81;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Descripción Cuenta";
            this.gridColumn2.FieldName = "strDesCuenta";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 236;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Deudor";
            this.gridColumn13.DisplayFormat.FormatString = "n2";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "Deudor";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.AllowFocus = false;
            this.gridColumn13.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn13.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Acreedor";
            this.gridColumn14.DisplayFormat.FormatString = "n2";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn14.FieldName = "Acreedor";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsColumn.AllowFocus = false;
            this.gridColumn14.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn14.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 5;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Activo";
            this.gridColumn17.DisplayFormat.FormatString = "n2";
            this.gridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn17.FieldName = "Activo";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            this.gridColumn17.OptionsColumn.AllowFocus = false;
            this.gridColumn17.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn17.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 6;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Pasivo";
            this.gridColumn18.DisplayFormat.FormatString = "n2";
            this.gridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn18.FieldName = "Pasivo";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            this.gridColumn18.OptionsColumn.AllowFocus = false;
            this.gridColumn18.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn18.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 7;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "N.Perdida";
            this.gridColumn19.DisplayFormat.FormatString = "n2";
            this.gridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn19.FieldName = "NPerdida";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            this.gridColumn19.OptionsColumn.AllowFocus = false;
            this.gridColumn19.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn19.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 8;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "N.Ganancia";
            this.gridColumn20.DisplayFormat.FormatString = "n2";
            this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn20.FieldName = "NGanancia";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.OptionsColumn.AllowEdit = false;
            this.gridColumn20.OptionsColumn.AllowFocus = false;
            this.gridColumn20.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn20.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 9;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "F.Perdida";
            this.gridColumn21.DisplayFormat.FormatString = "n2";
            this.gridColumn21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn21.FieldName = "FPerdida";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.OptionsColumn.AllowFocus = false;
            this.gridColumn21.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn21.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 10;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "F.Ganancia";
            this.gridColumn22.DisplayFormat.FormatString = "n2";
            this.gridColumn22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn22.FieldName = "FGanancia";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.OptionsColumn.AllowEdit = false;
            this.gridColumn22.OptionsColumn.AllowFocus = false;
            this.gridColumn22.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn22.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.marqueeProgressBarControl1);
            this.panel1.Controls.Add(this.lblMensaje);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 356);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 42);
            this.panel1.TabIndex = 9;
            this.panel1.Visible = false;
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.EditValue = 0;
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(303, 22);
            this.marqueeProgressBarControl1.MenuManager = this.barManager1;
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(540, 18);
            this.marqueeProgressBarControl1.TabIndex = 12;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblMensaje.ForeColor = System.Drawing.Color.Red;
            this.lblMensaje.Location = new System.Drawing.Point(15, 22);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(282, 19);
            this.lblMensaje.TabIndex = 11;
            this.lblMensaje.Text = "Espere mientras se cargan los datos ...";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // frm17InventarioResultado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 424);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grdAuxiliar);
            this.Controls.Add(this.gcFiltro);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frm17InventarioResultado";
            this.Text = "Inventario y Resultado";
            this.Load += new System.EventHandler(this.frm12BalanceComprobacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcFiltro)).EndInit();
            this.gcFiltro.ResumeLayout(false);
            this.gcFiltro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExcelSD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpDigitos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMeses.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAuxiliar)).EndInit();
            this.mnuComprobantes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewAuxiliar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl gcFiltro;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit lkpMoneda;
        private DevExpress.XtraEditors.LookUpEdit lkpDigitos;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblMes;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.LookUpEdit lkpMeses;
        private DevExpress.XtraEditors.LabelControl lblCuentaF;
        private DevExpress.XtraEditors.LabelControl lblCuentaI;
        private DevExpress.XtraGrid.GridControl grdAuxiliar;
        private DevExpress.XtraGrid.Views.Grid.GridView viewAuxiliar;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.ContextMenuStrip mnuComprobantes;
        private System.Windows.Forms.ToolStripMenuItem movimientosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private System.Windows.Forms.Label lblMensaje;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.SimpleButton btnLimpiar;
        private System.Windows.Forms.ToolStripMenuItem exportarAExcelToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdRuta;
        private DevExpress.XtraGrid.GridControl grdExcel;
        private DevExpress.XtraGrid.Views.Grid.GridView gvExcelSD;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private System.Windows.Forms.CheckBox cbActivarFiltro;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
    }
}