namespace SGE.WindowForms.Planillas.Procesos_de_Planilla
{
    partial class frm12InicialesProvPlanilla
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
            this.dgrComprobante = new DevExpress.XtraGrid.GridControl();
            this.mnuComprobantes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NuevotoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ModificartoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.EliminartoolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ImprimirtoolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.detalleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewComprobante = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescripcion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCalculadora = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.detalleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgrComprobante)).BeginInit();
            this.mnuComprobantes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewComprobante)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrComprobante
            // 
            this.dgrComprobante.ContextMenuStrip = this.mnuComprobantes;
            this.dgrComprobante.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrComprobante.EmbeddedNavigator.ContextMenuStrip = this.mnuComprobantes;
            this.dgrComprobante.Location = new System.Drawing.Point(0, 88);
            this.dgrComprobante.MainView = this.viewComprobante;
            this.dgrComprobante.Name = "dgrComprobante";
            this.dgrComprobante.Size = new System.Drawing.Size(751, 317);
            this.dgrComprobante.TabIndex = 11;
            this.dgrComprobante.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewComprobante});
            // 
            // mnuComprobantes
            // 
            this.mnuComprobantes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NuevotoolStripMenuItem1,
            this.ModificartoolStripMenuItem2,
            this.EliminartoolStripMenuItem3,
            this.ImprimirtoolStripMenuItem4,
            this.detalleToolStripMenuItem1});
            this.mnuComprobantes.Name = "contextMenuStrip1";
            this.mnuComprobantes.Size = new System.Drawing.Size(153, 136);
            // 
            // NuevotoolStripMenuItem1
            // 
            this.NuevotoolStripMenuItem1.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.NuevotoolStripMenuItem1.Name = "NuevotoolStripMenuItem1";
            this.NuevotoolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.NuevotoolStripMenuItem1.Text = "Nuevo";
            this.NuevotoolStripMenuItem1.Click += new System.EventHandler(this.NuevotoolStripMenuItem1_Click);
            // 
            // ModificartoolStripMenuItem2
            // 
            this.ModificartoolStripMenuItem2.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.ModificartoolStripMenuItem2.Name = "ModificartoolStripMenuItem2";
            this.ModificartoolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.ModificartoolStripMenuItem2.Text = "Modificar";
            this.ModificartoolStripMenuItem2.Click += new System.EventHandler(this.ModificartoolStripMenuItem2_Click);
            // 
            // EliminartoolStripMenuItem3
            // 
            this.EliminartoolStripMenuItem3.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.EliminartoolStripMenuItem3.Name = "EliminartoolStripMenuItem3";
            this.EliminartoolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.EliminartoolStripMenuItem3.Text = "Eliminar";
            this.EliminartoolStripMenuItem3.Click += new System.EventHandler(this.EliminartoolStripMenuItem3_Click);
            // 
            // ImprimirtoolStripMenuItem4
            // 
            this.ImprimirtoolStripMenuItem4.Enabled = false;
            this.ImprimirtoolStripMenuItem4.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.ImprimirtoolStripMenuItem4.Name = "ImprimirtoolStripMenuItem4";
            this.ImprimirtoolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.ImprimirtoolStripMenuItem4.Text = "Imprimir";
            this.ImprimirtoolStripMenuItem4.Visible = false;
            this.ImprimirtoolStripMenuItem4.Click += new System.EventHandler(this.ImprimirtoolStripMenuItem4_Click);
            // 
            // detalleToolStripMenuItem1
            // 
            this.detalleToolStripMenuItem1.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_sub;
            this.detalleToolStripMenuItem1.Name = "detalleToolStripMenuItem1";
            this.detalleToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.detalleToolStripMenuItem1.Text = "Detalle";
            this.detalleToolStripMenuItem1.Click += new System.EventHandler(this.detalleToolStripMenuItem1_Click);
            // 
            // viewComprobante
            // 
            this.viewComprobante.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.viewComprobante.GridControl = this.dgrComprobante;
            this.viewComprobante.GroupPanelText = " ";
            this.viewComprobante.Name = "viewComprobante";
            this.viewComprobante.DoubleClick += new System.EventHandler(this.viewAnalitica_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "Nº ";
            this.gridColumn1.FieldName = "ippc_iid_inicial_provision_planilla";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 94;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Descripción";
            this.gridColumn2.FieldName = "ippc_vdescripcion";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 486;
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(751, 88);
            this.groupControl1.TabIndex = 10;
            this.groupControl1.Text = "Criterios de Búsqueda ";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.txtDescripcion);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtCodigo);
            this.panelControl1.Location = new System.Drawing.Point(5, 25);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(664, 57);
            this.panelControl1.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(64, 8);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(4, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = ":";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(74, 28);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(355, 20);
            this.txtDescripcion.TabIndex = 7;
            this.txtDescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 13);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "Código ";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "Descripción :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(74, 5);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(144, 20);
            this.txtCodigo.TabIndex = 4;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
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
            this.btnNuevo,
            this.btnModificar,
            this.btnEliminar,
            this.btnCalculadora});
            this.barManager1.MaxItemId = 6;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNuevo),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnModificar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEliminar),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.KeyTip, this.btnCalculadora, "", false, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard, "", "")});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "[F7]";
            this.btnNuevo.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.btnNuevo.Id = 1;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnModificar
            // 
            this.btnModificar.Caption = "[F5]";
            this.btnModificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.Id = 2;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnModificar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModificar_ItemClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "[F9]";
            this.btnEliminar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.btnEliminar.Id = 3;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnEliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminar_ItemClick);
            // 
            // btnCalculadora
            // 
            this.btnCalculadora.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnCalculadora.Caption = "Calculadora";
            this.btnCalculadora.Glyph = global::SGE.WindowForms.Properties.Resources.calculadora;
            this.btnCalculadora.Id = 5;
            this.btnCalculadora.Name = "btnCalculadora";
            toolTipTitleItem1.Text = "Abrir Calculadora";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.btnCalculadora.SuperTip = superToolTip1;
            this.btnCalculadora.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCalculadora_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(751, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 405);
            this.barDockControlBottom.Size = new System.Drawing.Size(751, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 405);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(751, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 405);
            // 
            // detalleToolStripMenuItem
            // 
            this.detalleToolStripMenuItem.Name = "detalleToolStripMenuItem";
            this.detalleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.detalleToolStripMenuItem.Text = "Detalle";
            // 
            // frm12InicialesProvPlanilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 432);
            this.Controls.Add(this.dgrComprobante);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frm12InicialesProvPlanilla";
            this.Text = "Registro Iniciales Provision Planilla";
            this.Load += new System.EventHandler(this.FrmComprobantes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrComprobante)).EndInit();
            this.mnuComprobantes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewComprobante)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dgrComprobante;
        private DevExpress.XtraGrid.Views.Grid.GridView viewComprobante;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtDescripcion;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnModificar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraBars.BarButtonItem btnCalculadora;
        private System.Windows.Forms.ToolStripMenuItem detalleToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mnuComprobantes;
        private System.Windows.Forms.ToolStripMenuItem NuevotoolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ModificartoolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem EliminartoolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem ImprimirtoolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem detalleToolStripMenuItem1;
    }
}