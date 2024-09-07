namespace SGE.WindowForms.Producción.Consulta
{
    partial class FrmListarStockAlmacenFecha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListarStockAlmacenFecha));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lkpPuntoVenta = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.LkpAlmacen = new DevExpress.XtraEditors.LookUpEdit();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.dtpFechaFin = new DevExpress.XtraEditors.DateEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.dgrStockAlmacen = new DevExpress.XtraGrid.GridControl();
            this.mnuStockProducto = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.historialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStockProducto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idf_almacen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.descripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.exportarExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpAlmacen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrStockAlmacen)).BeginInit();
            this.mnuStockProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewStockProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkpPuntoVenta);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.LkpAlmacen);
            this.groupControl1.Controls.Add(this.btnBuscar);
            this.groupControl1.Controls.Add(this.labelControl10);
            this.groupControl1.Controls.Add(this.dtpFechaFin);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(732, 81);
            this.groupControl1.TabIndex = 75;
            this.groupControl1.Text = "Relacion de Productos x Almacen";
            // 
            // lkpPuntoVenta
            // 
            this.lkpPuntoVenta.Enabled = false;
            this.lkpPuntoVenta.Location = new System.Drawing.Point(96, 31);
            this.lkpPuntoVenta.Name = "lkpPuntoVenta";
            this.lkpPuntoVenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpPuntoVenta.Properties.NullText = "";
            this.lkpPuntoVenta.Size = new System.Drawing.Size(200, 20);
            this.lkpPuntoVenta.TabIndex = 86;
            this.lkpPuntoVenta.EditValueChanged += new System.EventHandler(this.lkpPuntoVenta_EditValueChanged);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 34);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(42, 13);
            this.labelControl8.TabIndex = 85;
            this.labelControl8.Text = "P.Venta:";
            // 
            // LkpAlmacen
            // 
            this.LkpAlmacen.Location = new System.Drawing.Point(96, 52);
            this.LkpAlmacen.Name = "LkpAlmacen";
            this.LkpAlmacen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpAlmacen.Properties.NullText = "";
            this.LkpAlmacen.Size = new System.Drawing.Size(200, 20);
            this.LkpAlmacen.TabIndex = 72;
            // 
            // btnBuscar
            // 
            this.btnBuscar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.ImageOptions.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(424, 52);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(98, 22);
            this.btnBuscar.TabIndex = 71;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(340, 34);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(73, 13);
            this.labelControl10.TabIndex = 67;
            this.labelControl10.Text = "Fecha :   Hasta";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.EditValue = null;
            this.dtpFechaFin.EnterMoveNextControl = true;
            this.dtpFechaFin.Location = new System.Drawing.Point(424, 31);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpFechaFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dtpFechaFin.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.dtpFechaFin.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dtpFechaFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFechaFin.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpFechaFin.Size = new System.Drawing.Size(98, 20);
            this.dtpFechaFin.TabIndex = 66;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(12, 53);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(47, 13);
            this.labelControl7.TabIndex = 52;
            this.labelControl7.Text = "Almacen :";
            // 
            // dgrStockAlmacen
            // 
            this.dgrStockAlmacen.ContextMenuStrip = this.mnuStockProducto;
            this.dgrStockAlmacen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrStockAlmacen.Location = new System.Drawing.Point(0, 81);
            this.dgrStockAlmacen.MainView = this.viewStockProducto;
            this.dgrStockAlmacen.Name = "dgrStockAlmacen";
            this.dgrStockAlmacen.Size = new System.Drawing.Size(732, 294);
            this.dgrStockAlmacen.TabIndex = 76;
            this.dgrStockAlmacen.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewStockProducto,
            this.gridView1});
            // 
            // mnuStockProducto
            // 
            this.mnuStockProducto.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.historialToolStripMenuItem,
            this.exportarExcelToolStripMenuItem});
            this.mnuStockProducto.Name = "contextMenuStrip1";
            this.mnuStockProducto.Size = new System.Drawing.Size(181, 70);
            // 
            // historialToolStripMenuItem
            // 
            this.historialToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_find;
            this.historialToolStripMenuItem.Name = "historialToolStripMenuItem";
            this.historialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.historialToolStripMenuItem.Text = "Kardex";
            this.historialToolStripMenuItem.Click += new System.EventHandler(this.historialToolStripMenuItem_Click);
            // 
            // viewStockProducto
            // 
            this.viewStockProducto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idf_almacen,
            this.descripcion,
            this.gridColumn1,
            this.gridColumn2});
            this.viewStockProducto.GridControl = this.dgrStockAlmacen;
            this.viewStockProducto.GroupPanelText = "Resultado de la Busqueda";
            this.viewStockProducto.Name = "viewStockProducto";
            // 
            // idf_almacen
            // 
            this.idf_almacen.AppearanceCell.Options.UseTextOptions = true;
            this.idf_almacen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.idf_almacen.Caption = "Código";
            this.idf_almacen.FieldName = "vcodigo_externo";
            this.idf_almacen.Name = "idf_almacen";
            this.idf_almacen.OptionsColumn.AllowEdit = false;
            this.idf_almacen.OptionsColumn.AllowFocus = false;
            this.idf_almacen.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.AllowMove = false;
            this.idf_almacen.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.ReadOnly = true;
            this.idf_almacen.Visible = true;
            this.idf_almacen.VisibleIndex = 3;
            this.idf_almacen.Width = 214;
            // 
            // descripcion
            // 
            this.descripcion.Caption = "Descripción";
            this.descripcion.FieldName = "descripcion_producto";
            this.descripcion.Name = "descripcion";
            this.descripcion.OptionsColumn.AllowEdit = false;
            this.descripcion.OptionsColumn.AllowFocus = false;
            this.descripcion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.descripcion.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.descripcion.OptionsColumn.AllowMove = false;
            this.descripcion.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.descripcion.OptionsColumn.ReadOnly = true;
            this.descripcion.Visible = true;
            this.descripcion.VisibleIndex = 0;
            this.descripcion.Width = 466;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Stock";
            this.gridColumn1.FieldName = "stock_prod";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 79;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "U.Med";
            this.gridColumn2.FieldName = "descripcion_unidad_medida";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 66;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gridView1.GridControl = this.dgrStockAlmacen;
            this.gridView1.Name = "gridView1";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Fecha";
            this.gridColumn3.FieldName = "kardx_sfecha";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Motivo";
            this.gridColumn4.FieldName = "vmotivo";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "NroDocumento";
            this.gridColumn5.FieldName = "NroDocumento";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Ingreso";
            this.gridColumn6.FieldName = "cantidad_ingreso";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Salida";
            this.gridColumn7.FieldName = "cantidad_salida";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Saldo";
            this.gridColumn8.FieldName = "cantidad_saldo_prod";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            // 
            // exportarExcelToolStripMenuItem
            // 
            this.exportarExcelToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_white_excel;
            this.exportarExcelToolStripMenuItem.Name = "exportarExcelToolStripMenuItem";
            this.exportarExcelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportarExcelToolStripMenuItem.Text = "Exportar Excel";
            this.exportarExcelToolStripMenuItem.Click += new System.EventHandler(this.exportarExcelToolStripMenuItem_Click);
            // 
            // FrmListarStockAlmacenFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 375);
            this.Controls.Add(this.dgrStockAlmacen);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmListarStockAlmacenFecha";
            this.Text = "Consulta Stock Producto por Fecha";
            this.Load += new System.EventHandler(this.FrmListarStockAlmacenCentralFecha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpAlmacen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrStockAlmacen)).EndInit();
            this.mnuStockProducto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewStockProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.LookUpEdit lkpPuntoVenta;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit LkpAlmacen;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit dtpFechaFin;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraGrid.GridControl dgrStockAlmacen;
        private System.Windows.Forms.ContextMenuStrip mnuStockProducto;
        private System.Windows.Forms.ToolStripMenuItem historialToolStripMenuItem;
        private DevExpress.XtraGrid.Views.Grid.GridView viewStockProducto;
        private DevExpress.XtraGrid.Columns.GridColumn idf_almacen;
        private DevExpress.XtraGrid.Columns.GridColumn descripcion;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.ToolStripMenuItem exportarExcelToolStripMenuItem;
    }
}