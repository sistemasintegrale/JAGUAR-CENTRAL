namespace SGE.WindowForms.Otros.Produccion
{
    partial class FrmListarProductosDeStock
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.LkpColor = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.LkpModelo = new DevExpress.XtraEditors.LookUpEdit();
            this.LkpMarca = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dgrProducto = new DevExpress.XtraGrid.GridControl();
            this.viewProducto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idf_almacen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.descripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnSalir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpModelo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpMarca.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.LkpColor);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.LkpModelo);
            this.groupControl1.Controls.Add(this.LkpMarca);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(724, 55);
            this.groupControl1.TabIndex = 12;
            this.groupControl1.Text = "Criterios de Busqueda";
            // 
            // LkpColor
            // 
            this.LkpColor.Location = new System.Drawing.Point(524, 27);
            this.LkpColor.Name = "LkpColor";
            this.LkpColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpColor.Properties.NullText = "";
            this.LkpColor.Size = new System.Drawing.Size(143, 20);
            this.LkpColor.TabIndex = 3;
            this.LkpColor.EditValueChanged += new System.EventHandler(this.LkpColor_EditValueChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(489, 30);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(29, 13);
            this.labelControl11.TabIndex = 0;
            this.labelControl11.Text = "Color:";
            // 
            // LkpModelo
            // 
            this.LkpModelo.Location = new System.Drawing.Point(290, 27);
            this.LkpModelo.Name = "LkpModelo";
            this.LkpModelo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpModelo.Properties.NullText = "";
            this.LkpModelo.Size = new System.Drawing.Size(143, 20);
            this.LkpModelo.TabIndex = 2;
            this.LkpModelo.EditValueChanged += new System.EventHandler(this.LkpModelo_EditValueChanged);
            // 
            // LkpMarca
            // 
            this.LkpMarca.Location = new System.Drawing.Point(47, 27);
            this.LkpMarca.Name = "LkpMarca";
            this.LkpMarca.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpMarca.Properties.NullText = "";
            this.LkpMarca.Size = new System.Drawing.Size(143, 20);
            this.LkpMarca.TabIndex = 1;
            this.LkpMarca.EditValueChanged += new System.EventHandler(this.LkpMarca_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Marca:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(246, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(38, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Modelo:";
            // 
            // dgrProducto
            // 
            this.dgrProducto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrProducto.Location = new System.Drawing.Point(0, 55);
            this.dgrProducto.MainView = this.viewProducto;
            this.dgrProducto.Name = "dgrProducto";
            this.dgrProducto.Size = new System.Drawing.Size(724, 303);
            this.dgrProducto.TabIndex = 13;
            this.dgrProducto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewProducto});
            // 
            // viewProducto
            // 
            this.viewProducto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idf_almacen,
            this.descripcion,
            this.gridColumn1});
            this.viewProducto.GridControl = this.dgrProducto;
            this.viewProducto.GroupPanelText = "Resultado de la Busqueda";
            this.viewProducto.Name = "viewProducto";
            this.viewProducto.DoubleClick += new System.EventHandler(this.viewProducto_DoubleClick);
            // 
            // idf_almacen
            // 
            this.idf_almacen.AppearanceCell.Options.UseTextOptions = true;
            this.idf_almacen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.idf_almacen.Caption = "Código";
            this.idf_almacen.FieldName = "pr_vcodigo_externo";
            this.idf_almacen.Name = "idf_almacen";
            this.idf_almacen.OptionsColumn.AllowEdit = false;
            this.idf_almacen.OptionsColumn.AllowFocus = false;
            this.idf_almacen.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.AllowMove = false;
            this.idf_almacen.OptionsColumn.ReadOnly = true;
            this.idf_almacen.Visible = true;
            this.idf_almacen.VisibleIndex = 0;
            this.idf_almacen.Width = 275;
            // 
            // descripcion
            // 
            this.descripcion.Caption = "Descripción";
            this.descripcion.FieldName = "pr_vdescripcion_producto";
            this.descripcion.Name = "descripcion";
            this.descripcion.OptionsColumn.AllowEdit = false;
            this.descripcion.OptionsColumn.AllowFocus = false;
            this.descripcion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.descripcion.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.descripcion.OptionsColumn.AllowMove = false;
            this.descripcion.OptionsColumn.ReadOnly = true;
            this.descripcion.Visible = true;
            this.descripcion.VisibleIndex = 1;
            this.descripcion.Width = 511;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Stock";
            this.gridColumn1.FieldName = "stocc_nstock_prod";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 96;
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
            this.btnSalir});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSalir)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnSalir
            // 
            this.btnSalir.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnSalir.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnSalir.Caption = "Salir";
            this.btnSalir.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnSalir.Id = 0;
            this.btnSalir.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            toolTipTitleItem1.Appearance.Image = global::SGE.WindowForms.Properties.Resources.doc_exit;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = global::SGE.WindowForms.Properties.Resources.doc_exit;
            toolTipTitleItem1.Text = "Salir(ESC)";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.btnSalir.SuperTip = superToolTip1;
            this.btnSalir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSalir_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(724, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 358);
            this.barDockControlBottom.Size = new System.Drawing.Size(724, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 358);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(724, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 358);
            // 
            // FrmListarProductosDeStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 386);
            this.Controls.Add(this.dgrProducto);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmListarProductosDeStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Stock de Productos";
            this.Load += new System.EventHandler(this.FrmListarProductosDeStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpModelo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpMarca.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.LookUpEdit LkpColor;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        public DevExpress.XtraEditors.LookUpEdit LkpModelo;
        public DevExpress.XtraEditors.LookUpEdit LkpMarca;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl dgrProducto;
        private DevExpress.XtraGrid.Views.Grid.GridView viewProducto;
        private DevExpress.XtraGrid.Columns.GridColumn idf_almacen;
        private DevExpress.XtraGrid.Columns.GridColumn descripcion;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnSalir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}