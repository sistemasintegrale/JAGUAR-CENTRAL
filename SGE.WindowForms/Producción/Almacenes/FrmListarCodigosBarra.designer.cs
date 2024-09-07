namespace SGE.WindowForms.Producción.Almacenes
{
    partial class FrmListarCodigosBarra
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.rbSucursal = new System.Windows.Forms.RadioButton();
            this.rbCentral = new System.Windows.Forms.RadioButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtGiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.mnucodigoBarra = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnagregar = new DevExpress.XtraBars.BarButtonItem();
            this.btnmodificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnsalir = new DevExpress.XtraBars.BarButtonItem();
            this.btnimprimir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gvCodigosBarra = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGiro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dgrcodigoBarra = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            this.mnucodigoBarra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCodigosBarra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrcodigoBarra)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 28);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(740, 94);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "Criterios de búsqueda";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.panelControl7);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.txtGiro);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.txtCodigo);
            this.groupControl2.Location = new System.Drawing.Point(12, 25);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(716, 62);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "Buscar";
            // 
            // panelControl7
            // 
            this.panelControl7.Controls.Add(this.rbSucursal);
            this.panelControl7.Controls.Add(this.rbCentral);
            this.panelControl7.Location = new System.Drawing.Point(470, 29);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(221, 25);
            this.panelControl7.TabIndex = 27;
            // 
            // rbSucursal
            // 
            this.rbSucursal.AutoSize = true;
            this.rbSucursal.ForeColor = System.Drawing.Color.Black;
            this.rbSucursal.Location = new System.Drawing.Point(109, 5);
            this.rbSucursal.Name = "rbSucursal";
            this.rbSucursal.Size = new System.Drawing.Size(65, 17);
            this.rbSucursal.TabIndex = 25;
            this.rbSucursal.Text = "Sucursal";
            this.rbSucursal.UseVisualStyleBackColor = true;
            // 
            // rbCentral
            // 
            this.rbCentral.AutoSize = true;
            this.rbCentral.Checked = true;
            this.rbCentral.ForeColor = System.Drawing.Color.Black;
            this.rbCentral.Location = new System.Drawing.Point(8, 6);
            this.rbCentral.Name = "rbCentral";
            this.rbCentral.Size = new System.Drawing.Size(60, 17);
            this.rbCentral.TabIndex = 24;
            this.rbCentral.TabStop = true;
            this.rbCentral.Text = "Central";
            this.rbCentral.UseVisualStyleBackColor = true;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Código:";
            // 
            // txtGiro
            // 
            this.txtGiro.Location = new System.Drawing.Point(302, 31);
            this.txtGiro.Name = "txtGiro";
            this.txtGiro.Size = new System.Drawing.Size(133, 20);
            this.txtGiro.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(235, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Descripción:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(84, 31);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 20);
            this.txtCodigo.TabIndex = 0;
            // 
            // mnucodigoBarra
            // 
            this.mnucodigoBarra.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.mnucodigoBarra.Name = "contextMenuStrip1";
            this.mnucodigoBarra.Size = new System.Drawing.Size(126, 92);
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
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnagregar,
            this.btnmodificar,
            this.btnsalir,
            this.btnimprimir});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnagregar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnmodificar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnsalir),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnimprimir)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnagregar
            // 
            this.btnagregar.Caption = "barButtonItem1";
            this.btnagregar.Id = 0;
            this.btnagregar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnagregar_ItemClick);
            // 
            // btnmodificar
            // 
            this.btnmodificar.Caption = "barButtonItem2";
            this.btnmodificar.Id = 1;
            this.btnmodificar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnmodificar.Name = "btnmodificar";
            this.btnmodificar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnmodificar_ItemClick);
            // 
            // btnsalir
            // 
            this.btnsalir.Caption = "barButtonItem3";
            this.btnsalir.Id = 2;
            this.btnsalir.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnsalir_ItemClick);
            // 
            // btnimprimir
            // 
            this.btnimprimir.Caption = "barButtonItem4";
            this.btnimprimir.Id = 3;
            this.btnimprimir.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.btnimprimir.Name = "btnimprimir";
            this.btnimprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnimprimir_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(740, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 380);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(740, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 352);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(740, 28);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 352);
            // 
            // gvCodigosBarra
            // 
            this.gvCodigosBarra.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcId,
            this.gcGiro,
            this.gridColumn2});
            this.gvCodigosBarra.GridControl = this.dgrcodigoBarra;
            this.gvCodigosBarra.GroupPanelText = "Resultado de la consulta";
            this.gvCodigosBarra.Name = "gvCodigosBarra";
            this.gvCodigosBarra.OptionsView.ShowFooter = true;
            // 
            // gcId
            // 
            this.gcId.Caption = "N°";
            this.gcId.DisplayFormat.FormatString = "{0:00}";
            this.gcId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcId.FieldName = "picbc_inumero_plant";
            this.gcId.Name = "gcId";
            this.gcId.OptionsColumn.AllowEdit = false;
            this.gcId.OptionsColumn.AllowFocus = false;
            this.gcId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcId.OptionsColumn.AllowMove = false;
            this.gcId.OptionsColumn.ReadOnly = true;
            this.gcId.Visible = true;
            this.gcId.VisibleIndex = 0;
            this.gcId.Width = 63;
            // 
            // gcGiro
            // 
            this.gcGiro.Caption = "Fecha";
            this.gcGiro.FieldName = "picbc_sfecha_plant";
            this.gcGiro.Name = "gcGiro";
            this.gcGiro.OptionsColumn.AllowEdit = false;
            this.gcGiro.OptionsColumn.AllowFocus = false;
            this.gcGiro.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcGiro.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcGiro.OptionsColumn.AllowMove = false;
            this.gcGiro.OptionsColumn.ReadOnly = true;
            this.gcGiro.Visible = true;
            this.gcGiro.VisibleIndex = 1;
            this.gcGiro.Width = 148;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Descrión de Impresión";
            this.gridColumn2.FieldName = "picbc_vdescrip";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 105;
            // 
            // dgrcodigoBarra
            // 
            this.dgrcodigoBarra.ContextMenuStrip = this.mnucodigoBarra;
            this.dgrcodigoBarra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrcodigoBarra.Location = new System.Drawing.Point(0, 122);
            this.dgrcodigoBarra.MainView = this.gvCodigosBarra;
            this.dgrcodigoBarra.Name = "dgrcodigoBarra";
            this.dgrcodigoBarra.Size = new System.Drawing.Size(740, 258);
            this.dgrcodigoBarra.TabIndex = 8;
            this.dgrcodigoBarra.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCodigosBarra});
            // 
            // FrmListarCodigosBarra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 380);
            this.Controls.Add(this.dgrcodigoBarra);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmListarCodigosBarra";
            this.Text = "Códigos de Barra";
            this.Load += new System.EventHandler(this.FrmListarCodigosBarra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            this.panelControl7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            this.mnucodigoBarra.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCodigosBarra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrcodigoBarra)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtGiro;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private System.Windows.Forms.ContextMenuStrip mnucodigoBarra;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnagregar;
        private DevExpress.XtraBars.BarButtonItem btnmodificar;
        private DevExpress.XtraBars.BarButtonItem btnsalir;
        private DevExpress.XtraBars.BarButtonItem btnimprimir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl dgrcodigoBarra;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCodigosBarra;
        private DevExpress.XtraGrid.Columns.GridColumn gcId;
        private DevExpress.XtraGrid.Columns.GridColumn gcGiro;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private System.Windows.Forms.RadioButton rbSucursal;
        private System.Windows.Forms.RadioButton rbCentral;
    }
}