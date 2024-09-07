namespace SGE.WindowForms.Producción.Consulta
{
    partial class FrmListarProductoKardexFecha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListarProductoKardexFecha));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btncodigoProducto = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtpFechaFin = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LkpAlmacen = new DevExpress.XtraEditors.LookUpEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.dtpFechaIni = new DevExpress.XtraEditors.DateEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.dgrStockAlmacen = new DevExpress.XtraGrid.GridControl();
            this.mnuKardex = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarAnioStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStockAlmacen = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idf_almacen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.descripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exportarExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btncodigoProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpAlmacen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaIni.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaIni.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrStockAlmacen)).BeginInit();
            this.mnuKardex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewStockAlmacen)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btncodigoProducto);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.dtpFechaFin);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.LkpAlmacen);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.dtpFechaIni);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(752, 85);
            this.groupControl1.TabIndex = 74;
            this.groupControl1.Text = "Kardex de un Producto por Fechas";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // btncodigoProducto
            // 
            this.btncodigoProducto.Location = new System.Drawing.Point(263, 27);
            this.btncodigoProducto.Name = "btncodigoProducto";
            this.btncodigoProducto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btncodigoProducto.Properties.ReadOnly = true;
            this.btncodigoProducto.Size = new System.Drawing.Size(180, 20);
            this.btncodigoProducto.TabIndex = 76;
            this.btncodigoProducto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btncodigoProducto_ButtonClick);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(212, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 75;
            this.labelControl2.Text = "Producto :";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.EditValue = null;
            this.dtpFechaFin.EnterMoveNextControl = true;
            this.dtpFechaFin.Location = new System.Drawing.Point(263, 49);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpFechaFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dtpFechaFin.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.dtpFechaFin.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dtpFechaFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFechaFin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpFechaFin.Size = new System.Drawing.Size(111, 20);
            this.dtpFechaFin.TabIndex = 74;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(212, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 73;
            this.labelControl1.Text = "Hasta";
            // 
            // LkpAlmacen
            // 
            this.LkpAlmacen.Location = new System.Drawing.Point(95, 27);
            this.LkpAlmacen.Name = "LkpAlmacen";
            this.LkpAlmacen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpAlmacen.Properties.NullText = "";
            this.LkpAlmacen.Size = new System.Drawing.Size(111, 20);
            this.LkpAlmacen.TabIndex = 72;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(449, 26);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(86, 22);
            this.simpleButton1.TabIndex = 71;
            this.simpleButton1.Text = "Buscar";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(12, 52);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(75, 13);
            this.labelControl10.TabIndex = 67;
            this.labelControl10.Text = "Fecha :   Desde";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.EditValue = null;
            this.dtpFechaIni.EnterMoveNextControl = true;
            this.dtpFechaIni.Location = new System.Drawing.Point(95, 49);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpFechaIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dtpFechaIni.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.dtpFechaIni.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dtpFechaIni.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFechaIni.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpFechaIni.Size = new System.Drawing.Size(111, 20);
            this.dtpFechaIni.TabIndex = 66;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(12, 30);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(47, 13);
            this.labelControl7.TabIndex = 52;
            this.labelControl7.Text = "Almacen :";
            // 
            // dgrStockAlmacen
            // 
            this.dgrStockAlmacen.ContextMenuStrip = this.mnuKardex;
            this.dgrStockAlmacen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrStockAlmacen.Location = new System.Drawing.Point(0, 85);
            this.dgrStockAlmacen.MainView = this.viewStockAlmacen;
            this.dgrStockAlmacen.Name = "dgrStockAlmacen";
            this.dgrStockAlmacen.Size = new System.Drawing.Size(752, 284);
            this.dgrStockAlmacen.TabIndex = 75;
            this.dgrStockAlmacen.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewStockAlmacen});
            // 
            // mnuKardex
            // 
            this.mnuKardex.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirToolStripMenuItem,
            this.actualizarAnioStockToolStripMenuItem,
            this.exportarExcelToolStripMenuItem});
            this.mnuKardex.Name = "contextMenuStrip1";
            this.mnuKardex.Size = new System.Drawing.Size(187, 92);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // actualizarAnioStockToolStripMenuItem
            // 
            this.actualizarAnioStockToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.arrow_circle_double;
            this.actualizarAnioStockToolStripMenuItem.Name = "actualizarAnioStockToolStripMenuItem";
            this.actualizarAnioStockToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.actualizarAnioStockToolStripMenuItem.Text = "Actualizar Anio Stock";
            this.actualizarAnioStockToolStripMenuItem.Visible = false;
            this.actualizarAnioStockToolStripMenuItem.Click += new System.EventHandler(this.actualizarAnioStockToolStripMenuItem_Click);
            // 
            // viewStockAlmacen
            // 
            this.viewStockAlmacen.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idf_almacen,
            this.descripcion,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.viewStockAlmacen.GridControl = this.dgrStockAlmacen;
            this.viewStockAlmacen.GroupPanelText = "Resultado de la Busqueda";
            this.viewStockAlmacen.Name = "viewStockAlmacen";
            // 
            // idf_almacen
            // 
            this.idf_almacen.AppearanceCell.Options.UseTextOptions = true;
            this.idf_almacen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.idf_almacen.Caption = "Fecha";
            this.idf_almacen.FieldName = "kardx_sfecha";
            this.idf_almacen.Name = "idf_almacen";
            this.idf_almacen.OptionsColumn.AllowEdit = false;
            this.idf_almacen.OptionsColumn.AllowFocus = false;
            this.idf_almacen.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.AllowMove = false;
            this.idf_almacen.OptionsColumn.AllowShowHide = false;
            this.idf_almacen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.ReadOnly = true;
            this.idf_almacen.Visible = true;
            this.idf_almacen.VisibleIndex = 0;
            this.idf_almacen.Width = 95;
            // 
            // descripcion
            // 
            this.descripcion.Caption = "Motivo";
            this.descripcion.FieldName = "vmotivo";
            this.descripcion.Name = "descripcion";
            this.descripcion.OptionsColumn.AllowEdit = false;
            this.descripcion.OptionsColumn.AllowFocus = false;
            this.descripcion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.descripcion.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.descripcion.OptionsColumn.AllowMove = false;
            this.descripcion.OptionsColumn.AllowShowHide = false;
            this.descripcion.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.descripcion.OptionsColumn.ReadOnly = true;
            this.descripcion.Visible = true;
            this.descripcion.VisibleIndex = 1;
            this.descripcion.Width = 118;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nro Documento";
            this.gridColumn1.FieldName = "NroDocumento";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.OptionsColumn.AllowShowHide = false;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 189;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Ingreso";
            this.gridColumn2.FieldName = "cantidad_ingreso";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.OptionsColumn.AllowShowHide = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 72;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Salida";
            this.gridColumn3.FieldName = "cantidad_salida";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.OptionsColumn.AllowShowHide = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 65;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Saldo";
            this.gridColumn4.FieldName = "cantidad_saldo_prod";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            this.gridColumn4.OptionsColumn.AllowShowHide = false;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 69;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Referencia";
            this.gridColumn5.FieldName = "Beneficiario";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.OptionsColumn.AllowShowHide = false;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 127;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Observaciones";
            this.gridColumn6.FieldName = "Observaciones";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.AllowMove = false;
            this.gridColumn6.OptionsColumn.AllowShowHide = false;
            this.gridColumn6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            this.gridColumn6.Width = 161;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Nota";
            this.gridColumn7.FieldName = "vNroNota";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMove = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 8;
            // 
            // exportarExcelToolStripMenuItem
            // 
            this.exportarExcelToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_excel;
            this.exportarExcelToolStripMenuItem.Name = "exportarExcelToolStripMenuItem";
            this.exportarExcelToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exportarExcelToolStripMenuItem.Text = "Exportar Excel";
            this.exportarExcelToolStripMenuItem.Click += new System.EventHandler(this.exportarExcelToolStripMenuItem_Click);
            // 
            // FrmListarProductoKardexFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 369);
            this.Controls.Add(this.dgrStockAlmacen);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmListarProductoKardexFecha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kardex de un Producto";
            this.Load += new System.EventHandler(this.FrmListarProductoKardexFecha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btncodigoProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpAlmacen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaIni.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaIni.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrStockAlmacen)).EndInit();
            this.mnuKardex.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewStockAlmacen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LookUpEdit LkpAlmacen;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit dtpFechaIni;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dtpFechaFin;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl dgrStockAlmacen;
        private DevExpress.XtraGrid.Views.Grid.GridView viewStockAlmacen;
        private DevExpress.XtraGrid.Columns.GridColumn idf_almacen;
        private DevExpress.XtraGrid.Columns.GridColumn descripcion;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        public DevExpress.XtraEditors.ButtonEdit btncodigoProducto;
        private System.Windows.Forms.ContextMenuStrip mnuKardex;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actualizarAnioStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarExcelToolStripMenuItem;
    }
}