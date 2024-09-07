namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    partial class Frm01PaisDepProvDis
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
            this.dgrLugar = new DevExpress.XtraGrid.GridControl();
            this.mnuVendedor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCodigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNombre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTipoUbicacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTipo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPadre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIdSituacion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lkpTipo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNombre = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrLugar)).BeginInit();
            this.mnuVendedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrLugar
            // 
            this.dgrLugar.ContextMenuStrip = this.mnuVendedor;
            this.dgrLugar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrLugar.Location = new System.Drawing.Point(0, 80);
            this.dgrLugar.MainView = this.gridView1;
            this.dgrLugar.Name = "dgrLugar";
            this.dgrLugar.Size = new System.Drawing.Size(897, 441);
            this.dgrLugar.TabIndex = 4;
            this.dgrLugar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // mnuVendedor
            // 
            this.mnuVendedor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.mnuVendedor.Name = "contextMenuStrip1";
            this.mnuVendedor.Size = new System.Drawing.Size(153, 114);
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
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click_1);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCodigo,
            this.gcId,
            this.gcNombre,
            this.gcTipoUbicacion,
            this.gcTipo,
            this.gcPadre,
            this.gcIdSituacion});
            this.gridView1.GridControl = this.dgrLugar;
            this.gridView1.GroupPanelText = "Resultado de la consulta";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyUp);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gcCodigo
            // 
            this.gcCodigo.FieldName = "ubicc_icod_ubicacion";
            this.gcCodigo.Name = "gcCodigo";
            this.gcCodigo.Width = 20;
            // 
            // gcId
            // 
            this.gcId.Caption = "Código";
            this.gcId.FieldName = "ubicc_ccod_ubicacion";
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
            this.gcId.Width = 96;
            // 
            // gcNombre
            // 
            this.gcNombre.Caption = "Nombre";
            this.gcNombre.FieldName = "ubicc_vnombre_ubicacion";
            this.gcNombre.Name = "gcNombre";
            this.gcNombre.OptionsColumn.AllowEdit = false;
            this.gcNombre.OptionsColumn.AllowFocus = false;
            this.gcNombre.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcNombre.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcNombre.OptionsColumn.AllowMove = false;
            this.gcNombre.OptionsColumn.ReadOnly = true;
            this.gcNombre.Visible = true;
            this.gcNombre.VisibleIndex = 1;
            this.gcNombre.Width = 602;
            // 
            // gcTipoUbicacion
            // 
            this.gcTipoUbicacion.FieldName = "tablc_iid_tipo_ubicacion";
            this.gcTipoUbicacion.Name = "gcTipoUbicacion";
            this.gcTipoUbicacion.OptionsColumn.AllowEdit = false;
            this.gcTipoUbicacion.OptionsColumn.AllowFocus = false;
            this.gcTipoUbicacion.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcTipoUbicacion.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcTipoUbicacion.OptionsColumn.AllowMove = false;
            this.gcTipoUbicacion.OptionsColumn.ReadOnly = true;
            this.gcTipoUbicacion.Width = 20;
            // 
            // gcTipo
            // 
            this.gcTipo.Caption = "Tipo";
            this.gcTipo.FieldName = "Ubicacion";
            this.gcTipo.Name = "gcTipo";
            this.gcTipo.OptionsColumn.AllowEdit = false;
            this.gcTipo.OptionsColumn.AllowFocus = false;
            this.gcTipo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gcTipo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gcTipo.OptionsColumn.AllowMove = false;
            this.gcTipo.Visible = true;
            this.gcTipo.VisibleIndex = 2;
            this.gcTipo.Width = 178;
            // 
            // gcPadre
            // 
            this.gcPadre.FieldName = "ubicc_icod_ubicacion_padre";
            this.gcPadre.Name = "gcPadre";
            // 
            // gcIdSituacion
            // 
            this.gcIdSituacion.FieldName = "ubicc_iid_situacion_ubicacion";
            this.gcIdSituacion.Name = "gcIdSituacion";
            this.gcIdSituacion.Width = 20;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkpTipo);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtNombre);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(897, 80);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Criterios de Busqueda";
            // 
            // lkpTipo
            // 
            this.lkpTipo.Location = new System.Drawing.Point(286, 25);
            this.lkpTipo.Name = "lkpTipo";
            this.lkpTipo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipo.Properties.NullText = "";
            this.lkpTipo.Size = new System.Drawing.Size(144, 20);
            this.lkpTipo.TabIndex = 5;
            this.lkpTipo.EditValueChanged += new System.EventHandler(this.lkpTipo_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(240, 30);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(27, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Tipo :";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(75, 49);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(355, 20);
            this.txtNombre.TabIndex = 3;
            this.txtNombre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyUp);
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
            this.labelControl1.Size = new System.Drawing.Size(44, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Nombre :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(75, 26);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyUp);
            // 
            // Frm01PaisDepProvDis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 521);
            this.Controls.Add(this.dgrLugar);
            this.Controls.Add(this.groupControl1);
            this.Name = "Frm01PaisDepProvDis";
            this.Text = "Registro de Ubicación Geográfica";
            this.Load += new System.EventHandler(this.FrmPaisDepProvDis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrLugar)).EndInit();
            this.mnuVendedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dgrLugar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gcId;
        private DevExpress.XtraGrid.Columns.GridColumn gcNombre;
        private DevExpress.XtraGrid.Columns.GridColumn gcTipoUbicacion;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtNombre;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private System.Windows.Forms.ContextMenuStrip mnuVendedor;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gcCodigo;
        private DevExpress.XtraGrid.Columns.GridColumn gcIdSituacion;
        private DevExpress.XtraGrid.Columns.GridColumn gcTipo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lkpTipo;
        private DevExpress.XtraGrid.Columns.GridColumn gcPadre;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
    }
}