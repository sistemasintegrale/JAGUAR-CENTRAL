namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    partial class Frm04EstadoCuentaClientesFecha
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
            this.grdCobranzaporRango = new DevExpress.XtraGrid.GridControl();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.todos = new System.Windows.Forms.ToolStripMenuItem();
            this.EstadoCuenta = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirLista = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirConDocumentos = new System.Windows.Forms.ToolStripMenuItem();
            this.CobranzaDudosa = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirListaDudosa = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirConDocumentosDudosa = new System.Windows.Forms.ToolStripMenuItem();
            this.cobranzaPorRangoDeDíasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewConbrazaporRango = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deInicio = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtcodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.rdTipoMovimiento = new DevExpress.XtraEditors.RadioGroup();
            this.txtNombre = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dgr = new DevExpress.XtraGrid.GridControl();
            this.view = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sfdRuta = new System.Windows.Forms.SaveFileDialog();
            this.exportarExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCobranzaporRango)).BeginInit();
            this.mnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewConbrazaporRango)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInicio.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInicio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTipoMovimiento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grdCobranzaporRango);
            this.groupControl1.Controls.Add(this.deInicio);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtcodigo);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.rdTipoMovimiento);
            this.groupControl1.Controls.Add(this.txtNombre);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(920, 53);
            this.groupControl1.TabIndex = 12;
            this.groupControl1.Text = "Estado de Cuenta de Clientes";
            // 
            // grdCobranzaporRango
            // 
            this.grdCobranzaporRango.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grdCobranzaporRango.ContextMenuStrip = this.mnu;
            this.grdCobranzaporRango.Location = new System.Drawing.Point(865, 12);
            this.grdCobranzaporRango.MainView = this.ViewConbrazaporRango;
            this.grdCobranzaporRango.Name = "grdCobranzaporRango";
            this.grdCobranzaporRango.Size = new System.Drawing.Size(43, 36);
            this.grdCobranzaporRango.TabIndex = 14;
            this.grdCobranzaporRango.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewConbrazaporRango});
            this.grdCobranzaporRango.Visible = false;
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.todos,
            this.EstadoCuenta,
            this.CobranzaDudosa,
            this.cobranzaPorRangoDeDíasToolStripMenuItem,
            this.exportarExcelToolStripMenuItem});
            this.mnu.Name = "contextMenuStrip1";
            this.mnu.Size = new System.Drawing.Size(267, 136);
            // 
            // todos
            // 
            this.todos.Name = "todos";
            this.todos.Size = new System.Drawing.Size(266, 22);
            this.todos.Text = "Ver todos los documentos";
            this.todos.Click += new System.EventHandler(this.todos_Click);
            // 
            // EstadoCuenta
            // 
            this.EstadoCuenta.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirLista,
            this.imprimirConDocumentos});
            this.EstadoCuenta.Name = "EstadoCuenta";
            this.EstadoCuenta.Size = new System.Drawing.Size(266, 22);
            this.EstadoCuenta.Text = "Imprimir Estado de Cuenta";
            this.EstadoCuenta.Click += new System.EventHandler(this.EstadoCuenta_Click);
            // 
            // imprimirLista
            // 
            this.imprimirLista.Name = "imprimirLista";
            this.imprimirLista.Size = new System.Drawing.Size(214, 22);
            this.imprimirLista.Text = "Imprimir Lista";
            this.imprimirLista.Click += new System.EventHandler(this.imprimirLista_Click);
            // 
            // imprimirConDocumentos
            // 
            this.imprimirConDocumentos.Name = "imprimirConDocumentos";
            this.imprimirConDocumentos.Size = new System.Drawing.Size(214, 22);
            this.imprimirConDocumentos.Text = "Imprimir con Documentos";
            this.imprimirConDocumentos.Click += new System.EventHandler(this.imprimirConDocumentos_Click);
            // 
            // CobranzaDudosa
            // 
            this.CobranzaDudosa.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirListaDudosa,
            this.imprimirConDocumentosDudosa});
            this.CobranzaDudosa.Name = "CobranzaDudosa";
            this.CobranzaDudosa.Size = new System.Drawing.Size(266, 22);
            this.CobranzaDudosa.Text = "Imprimir con Doc. Cobranza Dudosa";
            // 
            // imprimirListaDudosa
            // 
            this.imprimirListaDudosa.Name = "imprimirListaDudosa";
            this.imprimirListaDudosa.Size = new System.Drawing.Size(214, 22);
            this.imprimirListaDudosa.Text = "Imprimir Lista";
            this.imprimirListaDudosa.Click += new System.EventHandler(this.imprimirListaDudosa_Click);
            // 
            // imprimirConDocumentosDudosa
            // 
            this.imprimirConDocumentosDudosa.Name = "imprimirConDocumentosDudosa";
            this.imprimirConDocumentosDudosa.Size = new System.Drawing.Size(214, 22);
            this.imprimirConDocumentosDudosa.Text = "Imprimir con Documentos";
            this.imprimirConDocumentosDudosa.Click += new System.EventHandler(this.imprimirConDocumentosDudosa_Click);
            // 
            // cobranzaPorRangoDeDíasToolStripMenuItem
            // 
            this.cobranzaPorRangoDeDíasToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_magnify;
            this.cobranzaPorRangoDeDíasToolStripMenuItem.Name = "cobranzaPorRangoDeDíasToolStripMenuItem";
            this.cobranzaPorRangoDeDíasToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.cobranzaPorRangoDeDíasToolStripMenuItem.Text = "Cobranzas por Rango de Días";
            this.cobranzaPorRangoDeDíasToolStripMenuItem.Click += new System.EventHandler(this.cobranzaPorRangoDeDíasToolStripMenuItem_Click);
            // 
            // ViewConbrazaporRango
            // 
            this.ViewConbrazaporRango.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn9,
            this.gridColumn8,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17});
            this.ViewConbrazaporRango.GridControl = this.grdCobranzaporRango;
            this.ViewConbrazaporRango.GroupPanelText = "Relación de Proveedores con sus saldos";
            this.ViewConbrazaporRango.Name = "ViewConbrazaporRango";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Código";
            this.gridColumn7.FieldName = "cliec_vcod_cliente";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMove = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 200;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Nombre/Razón Social";
            this.gridColumn9.FieldName = "cliec_vnombre_cliente";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowFocus = false;
            this.gridColumn9.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.OptionsColumn.AllowMove = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            this.gridColumn9.Width = 278;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Monto US$";
            this.gridColumn8.DisplayFormat.FormatString = "n2";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "MontoUS";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 2;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "0-30";
            this.gridColumn12.DisplayFormat.FormatString = "n2";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn12.FieldName = "dias_0_30";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "31-60";
            this.gridColumn13.DisplayFormat.FormatString = "n2";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "dias_31_60";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "61-90";
            this.gridColumn14.DisplayFormat.FormatString = "n2";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn14.FieldName = "dias_61_90";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 5;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "91-120";
            this.gridColumn15.DisplayFormat.FormatString = "n2";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "dias_91_120";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 6;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "121-180";
            this.gridColumn16.DisplayFormat.FormatString = "n2";
            this.gridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn16.FieldName = "dias_121_180";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 7;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "181 a Más";
            this.gridColumn17.DisplayFormat.FormatString = "n2";
            this.gridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn17.FieldName = "dias_181_mas";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 8;
            // 
            // deInicio
            // 
            this.deInicio.EditValue = null;
            this.deInicio.EnterMoveNextControl = true;
            this.deInicio.Location = new System.Drawing.Point(52, 17);
            this.deInicio.Name = "deInicio";
            this.deInicio.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.deInicio.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.deInicio.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.deInicio.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.deInicio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deInicio.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deInicio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.deInicio.Size = new System.Drawing.Size(92, 20);
            this.deInicio.TabIndex = 26;
            this.deInicio.EditValueChanged += new System.EventHandler(this.deInicio_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(33, 13);
            this.labelControl3.TabIndex = 25;
            this.labelControl3.Text = "Fecha:";
            // 
            // txtcodigo
            // 
            this.txtcodigo.Location = new System.Drawing.Point(414, 16);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcodigo.Size = new System.Drawing.Size(108, 20);
            this.txtcodigo.TabIndex = 24;
            this.txtcodigo.EditValueChanged += new System.EventHandler(this.txtcodigo_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(372, 19);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "Código:";
            // 
            // rdTipoMovimiento
            // 
            this.rdTipoMovimiento.Location = new System.Drawing.Point(193, 12);
            this.rdTipoMovimiento.Name = "rdTipoMovimiento";
            this.rdTipoMovimiento.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdTipoMovimiento.Properties.Appearance.Options.UseBackColor = true;
            this.rdTipoMovimiento.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.rdTipoMovimiento.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.rdTipoMovimiento.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rdTipoMovimiento.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Clientes"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Empleados")});
            this.rdTipoMovimiento.Size = new System.Drawing.Size(172, 25);
            this.rdTipoMovimiento.TabIndex = 22;
            this.rdTipoMovimiento.SelectedIndexChanged += new System.EventHandler(this.rdTipoMovimiento_SelectedIndexChanged);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(671, 16);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Size = new System.Drawing.Size(171, 20);
            this.txtNombre.TabIndex = 1;
            this.txtNombre.EditValueChanged += new System.EventHandler(this.txtNombre_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(546, 19);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(105, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nombre/Razón Social:";
            // 
            // dgr
            // 
            this.dgr.ContextMenuStrip = this.mnu;
            this.dgr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgr.Location = new System.Drawing.Point(0, 53);
            this.dgr.MainView = this.view;
            this.dgr.Name = "dgr";
            this.dgr.Size = new System.Drawing.Size(920, 339);
            this.dgr.TabIndex = 13;
            this.dgr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.view});
            // 
            // view
            // 
            this.view.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn6});
            this.view.GridControl = this.dgr;
            this.view.GroupPanelText = "Relación de Proveedores con sus saldos";
            this.view.Name = "view";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Código";
            this.gridColumn3.FieldName = "cliec_vcod_cliente";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 200;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Situación";
            this.gridColumn4.FieldName = "Situacion";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nombre/Razón Social";
            this.gridColumn1.FieldName = "cliec_vnombre_cliente";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 278;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Saldo S/.";
            this.gridColumn2.DisplayFormat.FormatString = "n2";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "doxcc_nmonto_saldo_soles";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 203;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Saldo $";
            this.gridColumn5.DisplayFormat.FormatString = "n2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "doxcc_nmonto_saldo_dolares";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 210;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Giro";
            this.gridColumn6.FieldName = "giroc_vnombre_giro";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 143;
            // 
            // exportarExcelToolStripMenuItem
            // 
            this.exportarExcelToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_excel;
            this.exportarExcelToolStripMenuItem.Name = "exportarExcelToolStripMenuItem";
            this.exportarExcelToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.exportarExcelToolStripMenuItem.Text = "Exportar Excel";
            this.exportarExcelToolStripMenuItem.Click += new System.EventHandler(this.exportarExcelToolStripMenuItem_Click);
            // 
            // Frm04EstadoCuentaClientesFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 392);
            this.Controls.Add(this.dgr);
            this.Controls.Add(this.groupControl1);
            this.Name = "Frm04EstadoCuentaClientesFecha";
            this.Text = "Estado Cuentas Por Fecha";
            this.Load += new System.EventHandler(this.FrmEstadoCuentaClientesFecha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCobranzaporRango)).EndInit();
            this.mnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewConbrazaporRango)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInicio.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInicio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdTipoMovimiento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtcodigo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.RadioGroup rdTipoMovimiento;
        private DevExpress.XtraEditors.TextEdit txtNombre;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl dgr;
        private DevExpress.XtraGrid.Views.Grid.GridView view;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.DateEdit deInicio;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.ContextMenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem todos;
        private System.Windows.Forms.ToolStripMenuItem EstadoCuenta;
        private System.Windows.Forms.ToolStripMenuItem imprimirLista;
        private System.Windows.Forms.ToolStripMenuItem imprimirConDocumentos;
        private System.Windows.Forms.ToolStripMenuItem CobranzaDudosa;
        private System.Windows.Forms.ToolStripMenuItem imprimirListaDudosa;
        private System.Windows.Forms.ToolStripMenuItem imprimirConDocumentosDudosa;
        private System.Windows.Forms.ToolStripMenuItem cobranzaPorRangoDeDíasToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl grdCobranzaporRango;
        private DevExpress.XtraGrid.Views.Grid.GridView ViewConbrazaporRango;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private System.Windows.Forms.SaveFileDialog sfdRuta;
        private System.Windows.Forms.ToolStripMenuItem exportarExcelToolStripMenuItem;
    }
}