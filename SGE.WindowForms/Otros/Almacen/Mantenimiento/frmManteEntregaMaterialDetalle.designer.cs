namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    partial class frmManteEntregaMaterialDetalle
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
            this.btnAceptar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraMateriales = new DevExpress.XtraTab.XtraTabPage();
            this.grdMateriales = new DevExpress.XtraGrid.GridControl();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMateriales = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtStock = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalCantidadEntregada = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lkpUbicacion = new DevExpress.XtraEditors.LookUpEdit();
            this.txtUM = new DevExpress.XtraEditors.TextEdit();
            this.txtDescProd = new DevExpress.XtraEditors.TextEdit();
            this.bteProducto = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescripcion = new DevExpress.XtraEditors.MemoEdit();
            this.txtCantidadRequerida = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraMateriales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMateriales)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewMateriales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCantidadEntregada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUbicacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescProd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadRequerida.Properties)).BeginInit();
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
            this.btnAceptar,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAceptar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnAceptar.Caption = "Aceptar";
            this.btnAceptar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_check;
            this.btnAceptar.Id = 0;
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAceptar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAceptar_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(521, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 399);
            this.barDockControlBottom.Size = new System.Drawing.Size(521, 26);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 399);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(521, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 399);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.xtraTabControl1);
            this.groupControl1.Controls.Add(this.txtStock);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtTotalCantidadEntregada);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lkpUbicacion);
            this.groupControl1.Controls.Add(this.txtUM);
            this.groupControl1.Controls.Add(this.txtDescProd);
            this.groupControl1.Controls.Add(this.bteProducto);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtDescripcion);
            this.groupControl1.Controls.Add(this.txtCantidadRequerida);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl20);
            this.groupControl1.Controls.Add(this.labelControl21);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(521, 399);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Datos Mercaderia";
            this.groupControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.groupControl1_MouseMove);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(13, 192);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraMateriales;
            this.xtraTabControl1.Size = new System.Drawing.Size(495, 199);
            this.xtraTabControl1.TabIndex = 211;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraMateriales});
            // 
            // xtraMateriales
            // 
            this.xtraMateriales.Controls.Add(this.grdMateriales);
            this.xtraMateriales.Name = "xtraMateriales";
            this.xtraMateriales.Size = new System.Drawing.Size(490, 174);
            this.xtraMateriales.Text = "Materiales";
            // 
            // grdMateriales
            // 
            this.grdMateriales.ContextMenuStrip = this.menu;
            this.grdMateriales.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdMateriales.Location = new System.Drawing.Point(0, 0);
            this.grdMateriales.MainView = this.viewMateriales;
            this.grdMateriales.Name = "grdMateriales";
            this.grdMateriales.Size = new System.Drawing.Size(490, 175);
            this.grdMateriales.TabIndex = 2;
            this.grdMateriales.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewMateriales,
            this.gridView3,
            this.gridView5,
            this.gridView2});
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.menu.Name = "contextMenuStrip1";
            this.menu.Size = new System.Drawing.Size(118, 48);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // viewMateriales
            // 
            this.viewMateriales.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1});
            this.viewMateriales.GridControl = this.grdMateriales;
            this.viewMateriales.GroupPanelText = " ";
            this.viewMateriales.Name = "viewMateriales";
            this.viewMateriales.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Fecha Entrega";
            this.gridColumn2.FieldName = "rqmed_sfecha_entrega";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 63;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Cantidad ";
            this.gridColumn1.FieldName = "rqmed_ncantidad_entregada";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.grdMateriales;
            this.gridView3.Name = "gridView3";
            // 
            // gridView5
            // 
            this.gridView5.GridControl = this.grdMateriales;
            this.gridView5.Name = "gridView5";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.grdMateriales;
            this.gridView2.Name = "gridView2";
            // 
            // txtStock
            // 
            this.txtStock.EditValue = "0";
            this.txtStock.Enabled = false;
            this.txtStock.Location = new System.Drawing.Point(411, 145);
            this.txtStock.Name = "txtStock";
            this.txtStock.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtStock.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtStock.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtStock.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtStock.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtStock.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtStock.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtStock.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtStock.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtStock.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtStock.Size = new System.Drawing.Size(97, 20);
            this.txtStock.TabIndex = 210;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(372, 148);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(33, 13);
            this.labelControl4.TabIndex = 209;
            this.labelControl4.Text = "Stock :";
            // 
            // txtTotalCantidadEntregada
            // 
            this.txtTotalCantidadEntregada.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            327680});
            this.txtTotalCantidadEntregada.Enabled = false;
            this.txtTotalCantidadEntregada.Location = new System.Drawing.Point(130, 166);
            this.txtTotalCantidadEntregada.Name = "txtTotalCantidadEntregada";
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtTotalCantidadEntregada.Properties.DisplayFormat.FormatString = "n5";
            this.txtTotalCantidadEntregada.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTotalCantidadEntregada.Properties.EditFormat.FormatString = "n5";
            this.txtTotalCantidadEntregada.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTotalCantidadEntregada.Properties.Mask.EditMask = "n5";
            this.txtTotalCantidadEntregada.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTotalCantidadEntregada.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTotalCantidadEntregada.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalCantidadEntregada.Size = new System.Drawing.Size(80, 20);
            this.txtTotalCantidadEntregada.TabIndex = 208;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 169);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(103, 13);
            this.labelControl2.TabIndex = 207;
            this.labelControl2.Text = "Cantidad Entregada :";
            // 
            // lkpUbicacion
            // 
            this.lkpUbicacion.Enabled = false;
            this.lkpUbicacion.Location = new System.Drawing.Point(79, 25);
            this.lkpUbicacion.Name = "lkpUbicacion";
            this.lkpUbicacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpUbicacion.Properties.NullText = "";
            this.lkpUbicacion.Size = new System.Drawing.Size(157, 20);
            this.lkpUbicacion.TabIndex = 206;
            // 
            // txtUM
            // 
            this.txtUM.Enabled = false;
            this.txtUM.Location = new System.Drawing.Point(79, 143);
            this.txtUM.Name = "txtUM";
            this.txtUM.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtUM.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtUM.Properties.MaxLength = 50;
            this.txtUM.Properties.ReadOnly = true;
            this.txtUM.Size = new System.Drawing.Size(64, 20);
            this.txtUM.TabIndex = 205;
            // 
            // txtDescProd
            // 
            this.txtDescProd.Enabled = false;
            this.txtDescProd.Location = new System.Drawing.Point(209, 120);
            this.txtDescProd.Name = "txtDescProd";
            this.txtDescProd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescProd.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescProd.Properties.MaxLength = 50;
            this.txtDescProd.Properties.ReadOnly = true;
            this.txtDescProd.Size = new System.Drawing.Size(299, 20);
            this.txtDescProd.TabIndex = 203;
            // 
            // bteProducto
            // 
            this.bteProducto.Enabled = false;
            this.bteProducto.Location = new System.Drawing.Point(80, 120);
            this.bteProducto.Name = "bteProducto";
            this.bteProducto.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteProducto.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteProducto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteProducto.Properties.Mask.ShowPlaceHolders = false;
            this.bteProducto.Properties.ReadOnly = true;
            this.bteProducto.Size = new System.Drawing.Size(111, 20);
            this.bteProducto.TabIndex = 202;
            this.bteProducto.Click += new System.EventHandler(this.bteProducto_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 123);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(47, 13);
            this.labelControl3.TabIndex = 204;
            this.labelControl3.Text = "Producto:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Location = new System.Drawing.Point(79, 48);
            this.txtDescripcion.MenuManager = this.barManager1;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescripcion.Size = new System.Drawing.Size(424, 68);
            this.txtDescripcion.TabIndex = 201;
            // 
            // txtCantidadRequerida
            // 
            this.txtCantidadRequerida.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            327680});
            this.txtCantidadRequerida.Enabled = false;
            this.txtCantidadRequerida.Location = new System.Drawing.Point(273, 143);
            this.txtCantidadRequerida.Name = "txtCantidadRequerida";
            this.txtCantidadRequerida.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCantidadRequerida.Properties.DisplayFormat.FormatString = "n5";
            this.txtCantidadRequerida.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCantidadRequerida.Properties.EditFormat.FormatString = "n5";
            this.txtCantidadRequerida.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCantidadRequerida.Properties.Mask.EditMask = "n5";
            this.txtCantidadRequerida.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCantidadRequerida.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCantidadRequerida.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCantidadRequerida.Size = new System.Drawing.Size(75, 20);
            this.txtCantidadRequerida.TabIndex = 200;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(178, 146);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 13);
            this.labelControl1.TabIndex = 199;
            this.labelControl1.Text = "Cantidad Pedida :";
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(13, 50);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(61, 13);
            this.labelControl20.TabIndex = 196;
            this.labelControl20.Text = "Descripción :";
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(21, 28);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(52, 13);
            this.labelControl21.TabIndex = 195;
            this.labelControl21.Text = "Ubicación :";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(13, 142);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(41, 13);
            this.labelControl14.TabIndex = 194;
            this.labelControl14.Text = "Medida :";
            // 
            // frmManteEntregaMaterialDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 425);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new System.Drawing.Size(468, 215);
            this.Name = "frmManteEntregaMaterialDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento Entrega de Material Det.";
            this.Load += new System.EventHandler(this.frmManteFacturaDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraMateriales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMateriales)).EndInit();
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewMateriales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCantidadEntregada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUbicacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescProd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadRequerida.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnAceptar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        public DevExpress.XtraEditors.LookUpEdit lkpUbicacion;
        public DevExpress.XtraEditors.TextEdit txtUM;
        public DevExpress.XtraEditors.TextEdit txtDescProd;
        public DevExpress.XtraEditors.ButtonEdit bteProducto;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.MemoEdit txtDescripcion;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        public DevExpress.XtraEditors.TextEdit txtCantidadRequerida;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtTotalCantidadEntregada;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtStock;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraMateriales;
        private DevExpress.XtraGrid.GridControl grdMateriales;
        private DevExpress.XtraGrid.Views.Grid.GridView viewMateriales;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
    }
}