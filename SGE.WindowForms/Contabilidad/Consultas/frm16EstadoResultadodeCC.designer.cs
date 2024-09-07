namespace SGE.WindowForms.Contabilidad.Consultas
{
    partial class frm16EstadoResultadodeCC
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
            this.mnuCompras = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.actualizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarPlantillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarDetalleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grDetalleEx = new DevExpress.XtraGrid.GridControl();
            this.grDetalla = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.grdExcelCD = new DevExpress.XtraGrid.GridControl();
            this.gvExcelCD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lkpMes = new DevExpress.XtraEditors.LookUpEdit();
            this.bteCCostoI = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblMes = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.grd = new DevExpress.XtraGrid.GridControl();
            this.gv = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.sfdRuta = new System.Windows.Forms.SaveFileDialog();
            this.mnuCompras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grDetalleEx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grDetalla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcelCD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExcelCD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCostoI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuCompras
            // 
            this.mnuCompras.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizarToolStripMenuItem,
            this.exportarPlantillaToolStripMenuItem,
            this.exportarDetalleToolStripMenuItem});
            this.mnuCompras.Name = "contextMenuStrip1";
            this.mnuCompras.Size = new System.Drawing.Size(163, 70);
            // 
            // actualizarToolStripMenuItem
            // 
            this.actualizarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_sub;
            this.actualizarToolStripMenuItem.Name = "actualizarToolStripMenuItem";
            this.actualizarToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.actualizarToolStripMenuItem.Text = "Detalle Cuentas";
            this.actualizarToolStripMenuItem.Click += new System.EventHandler(this.actualizarToolStripMenuItem_Click);
            // 
            // exportarPlantillaToolStripMenuItem
            // 
            this.exportarPlantillaToolStripMenuItem.Name = "exportarPlantillaToolStripMenuItem";
            this.exportarPlantillaToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exportarPlantillaToolStripMenuItem.Text = "Exportar Plantilla";
            this.exportarPlantillaToolStripMenuItem.Click += new System.EventHandler(this.exportarPlantillaToolStripMenuItem_Click);
            // 
            // exportarDetalleToolStripMenuItem
            // 
            this.exportarDetalleToolStripMenuItem.Name = "exportarDetalleToolStripMenuItem";
            this.exportarDetalleToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.exportarDetalleToolStripMenuItem.Text = "Exportar Detalle";
            this.exportarDetalleToolStripMenuItem.Click += new System.EventHandler(this.exportarDetalleToolStripMenuItem_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grDetalleEx);
            this.groupControl1.Controls.Add(this.grdExcelCD);
            this.groupControl1.Controls.Add(this.lkpMes);
            this.groupControl1.Controls.Add(this.bteCCostoI);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lblMes);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnBuscar);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1061, 68);
            this.groupControl1.TabIndex = 17;
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // grDetalleEx
            // 
            this.grDetalleEx.Location = new System.Drawing.Point(930, 25);
            this.grDetalleEx.MainView = this.grDetalla;
            this.grDetalleEx.Name = "grDetalleEx";
            this.grDetalleEx.Size = new System.Drawing.Size(77, 34);
            this.grDetalleEx.TabIndex = 104;
            this.grDetalleEx.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grDetalla});
            this.grDetalleEx.Visible = false;
            // 
            // grDetalla
            // 
            this.grDetalla.GridControl = this.grDetalleEx;
            this.grDetalla.Name = "grDetalla";
            // 
            // grdExcelCD
            // 
            this.grdExcelCD.Location = new System.Drawing.Point(847, 25);
            this.grdExcelCD.MainView = this.gvExcelCD;
            this.grdExcelCD.Name = "grdExcelCD";
            this.grdExcelCD.Size = new System.Drawing.Size(77, 34);
            this.grdExcelCD.TabIndex = 103;
            this.grdExcelCD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvExcelCD});
            this.grdExcelCD.Visible = false;
            // 
            // gvExcelCD
            // 
            this.gvExcelCD.GridControl = this.grdExcelCD;
            this.gvExcelCD.Name = "gvExcelCD";
            // 
            // lkpMes
            // 
            this.lkpMes.Location = new System.Drawing.Point(140, 34);
            this.lkpMes.Name = "lkpMes";
            this.lkpMes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMes.Properties.NullText = "";
            this.lkpMes.Size = new System.Drawing.Size(172, 20);
            this.lkpMes.TabIndex = 100;
            // 
            // bteCCostoI
            // 
            this.bteCCostoI.Location = new System.Drawing.Point(569, 34);
            this.bteCCostoI.Name = "bteCCostoI";
            this.bteCCostoI.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCCostoI.Properties.ReadOnly = true;
            this.bteCCostoI.Size = new System.Drawing.Size(102, 20);
            this.bteCCostoI.TabIndex = 99;
            this.bteCCostoI.Visible = false;
            this.bteCCostoI.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCCostoI_ButtonClick);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(481, 37);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(72, 13);
            this.labelControl7.TabIndex = 98;
            this.labelControl7.Text = "C. Costo Inicial";
            this.labelControl7.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(130, 37);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(4, 13);
            this.labelControl2.TabIndex = 96;
            this.labelControl2.Text = ":";
            // 
            // lblMes
            // 
            this.lblMes.Location = new System.Drawing.Point(21, 37);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(99, 13);
            this.lblMes.TabIndex = 94;
            this.lblMes.Text = "Mayor de C.C al Mes";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(559, 37);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(4, 13);
            this.labelControl1.TabIndex = 93;
            this.labelControl1.Text = ":";
            this.labelControl1.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::SGE.WindowForms.Properties.Resources.Refresh;
            this.btnBuscar.Location = new System.Drawing.Point(321, 31);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(118, 23);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Generar";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // grd
            // 
            this.grd.ContextMenuStrip = this.mnuCompras;
            this.grd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grd.Location = new System.Drawing.Point(0, 68);
            this.grd.MainView = this.gv;
            this.grd.Name = "grd";
            this.grd.Size = new System.Drawing.Size(1061, 375);
            this.grd.TabIndex = 20;
            this.grd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv});
            this.grd.Click += new System.EventHandler(this.grd_Click);
            // 
            // gv
            // 
            this.gv.GridControl = this.grd;
            this.gv.Name = "gv";
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEliminar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "[F7]";
            this.btnNuevo.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.btnNuevo.Id = 0;
            this.btnNuevo.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btnModificar
            // 
            this.btnModificar.Caption = "[F5]";
            this.btnModificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.Id = 1;
            this.btnModificar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "[F9]";
            this.btnEliminar.Glyph = global::SGE.WindowForms.Properties.Resources.page_white_code_red;
            this.btnEliminar.Id = 2;
            this.btnEliminar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1061, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 443);
            this.barDockControlBottom.Size = new System.Drawing.Size(1061, 26);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 469);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1061, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 469);
            // 
            // frm16EstadoResultadodeCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 469);
            this.Controls.Add(this.grd);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Name = "frm16EstadoResultadodeCC";
            this.Text = "Estado Resultado de C.Costo";
            this.Load += new System.EventHandler(this.frm13EstadoResultadoCC_Load);
            this.mnuCompras.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grDetalleEx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grDetalla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcelCD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvExcelCD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCCostoI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private System.Windows.Forms.ContextMenuStrip mnuCompras;
        private System.Windows.Forms.ToolStripMenuItem actualizarToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private DevExpress.XtraGrid.GridControl grd;
        private DevExpress.XtraGrid.Views.Grid.GridView gv;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblMes;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ButtonEdit bteCCostoI;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnModificar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.LookUpEdit lkpMes;
        private System.Windows.Forms.ToolStripMenuItem exportarPlantillaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarDetalleToolStripMenuItem;
        private DevExpress.XtraGrid.GridControl grDetalleEx;
        private DevExpress.XtraGrid.Views.Grid.GridView grDetalla;
        private DevExpress.XtraGrid.GridControl grdExcelCD;
        private DevExpress.XtraGrid.Views.Grid.GridView gvExcelCD;
        private System.Windows.Forms.SaveFileDialog sfdRuta;
    }
}