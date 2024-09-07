namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    partial class Frm02GiroCliente
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
            this.dgrGiro = new DevExpress.XtraGrid.GridControl();
            this.mnuGiro = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCodigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcGiro = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIdSituacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtGiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrGiro)).BeginInit();
            this.mnuGiro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrGiro
            // 
            this.dgrGiro.ContextMenuStrip = this.mnuGiro;
            this.dgrGiro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrGiro.Location = new System.Drawing.Point(0, 80);
            this.dgrGiro.MainView = this.gridView1;
            this.dgrGiro.Name = "dgrGiro";
            this.dgrGiro.Size = new System.Drawing.Size(897, 441);
            this.dgrGiro.TabIndex = 4;
            this.dgrGiro.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.dgrGiro.Click += new System.EventHandler(this.dgrGiro_Click);
            // 
            // mnuGiro
            // 
            this.mnuGiro.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.mnuGiro.Name = "contextMenuStrip1";
            this.mnuGiro.Size = new System.Drawing.Size(153, 114);
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
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCodigo,
            this.gcId,
            this.gcGiro,
            this.gcIdSituacion,
            this.gridColumn1});
            this.gridView1.GridControl = this.dgrGiro;
            this.gridView1.GroupPanelText = "Giros";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyUp);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gcCodigo
            // 
            this.gcCodigo.FieldName = "giroc_icod_giro";
            this.gcCodigo.Name = "gcCodigo";
            this.gcCodigo.Width = 20;
            // 
            // gcId
            // 
            this.gcId.Caption = "Código";
            this.gcId.FieldName = "giroc_iid_giro";
            this.gcId.Name = "gcId";
            this.gcId.OptionsColumn.AllowEdit = false;
            this.gcId.OptionsColumn.AllowFocus = false;
            this.gcId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcId.OptionsColumn.AllowMove = false;
            this.gcId.OptionsColumn.ReadOnly = true;
            this.gcId.SummaryItem.DisplayFormat = "Total : {0}";
            this.gcId.SummaryItem.FieldName = "descripcion";
            this.gcId.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            this.gcId.Visible = true;
            this.gcId.VisibleIndex = 0;
            this.gcId.Width = 98;
            // 
            // gcGiro
            // 
            this.gcGiro.Caption = "Descripción";
            this.gcGiro.FieldName = "giroc_vnombre_giro";
            this.gcGiro.Name = "gcGiro";
            this.gcGiro.OptionsColumn.AllowEdit = false;
            this.gcGiro.OptionsColumn.AllowFocus = false;
            this.gcGiro.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcGiro.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcGiro.OptionsColumn.AllowMove = false;
            this.gcGiro.OptionsColumn.ReadOnly = true;
            this.gcGiro.Visible = true;
            this.gcGiro.VisibleIndex = 1;
            this.gcGiro.Width = 701;
            // 
            // gcIdSituacion
            // 
            this.gcIdSituacion.FieldName = "tablc_iid_situacion_giro";
            this.gcIdSituacion.Name = "gcIdSituacion";
            this.gcIdSituacion.Width = 20;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Situación";
            this.gridColumn1.FieldName = "DescripSituacion";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtGiro);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(897, 80);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Criterios de búsqueda";
            // 
            // txtGiro
            // 
            this.txtGiro.Location = new System.Drawing.Point(75, 49);
            this.txtGiro.Name = "txtGiro";
            this.txtGiro.Size = new System.Drawing.Size(133, 20);
            this.txtGiro.TabIndex = 3;
            this.txtGiro.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 30);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Código :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Giro:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(75, 26);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyUp);
            // 
            // Frm02GiroCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 521);
            this.Controls.Add(this.dgrGiro);
            this.Controls.Add(this.groupControl1);
            this.Name = "Frm02GiroCliente";
            this.Text = "Registro de Giros de Clientes";
            this.Load += new System.EventHandler(this.FrmGiroCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrGiro)).EndInit();
            this.mnuGiro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dgrGiro;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gcId;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtGiro;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private System.Windows.Forms.ContextMenuStrip mnuGiro;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gcCodigo;
        private DevExpress.XtraGrid.Columns.GridColumn gcIdSituacion;
        private DevExpress.XtraGrid.Columns.GridColumn gcGiro;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}