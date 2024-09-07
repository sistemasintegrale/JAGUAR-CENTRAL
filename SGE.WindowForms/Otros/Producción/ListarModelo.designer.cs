namespace SGE.WindowForms.Otros.Produccion
{
    partial class ListarModelo
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
            this.txtdescripcion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.dgrMotonave = new DevExpress.XtraGrid.GridControl();
            this.viewMotonave = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idf_almacen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.descripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkpGrdCategoria = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkpGrdTipo = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkpGrdLinea = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkpgrdMarca = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrMotonave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMotonave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGrdCategoria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGrdTipo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGrdLinea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpgrdMarca)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtdescripcion);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(690, 74);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Criterios de Busqueda";
            // 
            // txtdescripcion
            // 
            this.txtdescripcion.Location = new System.Drawing.Point(112, 36);
            this.txtdescripcion.Name = "txtdescripcion";
            this.txtdescripcion.Size = new System.Drawing.Size(203, 20);
            this.txtdescripcion.TabIndex = 3;
            this.txtdescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(128, 103);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Codigo :";
            this.labelControl2.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(92, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Código de Modelo :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(205, 100);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(139, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.Visible = false;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // dgrMotonave
            // 
            this.dgrMotonave.ContextMenuStrip = this.contextMenuStrip1;
            this.dgrMotonave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrMotonave.Location = new System.Drawing.Point(0, 74);
            this.dgrMotonave.MainView = this.viewMotonave;
            this.dgrMotonave.Name = "dgrMotonave";
            this.dgrMotonave.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkpGrdCategoria,
            this.lkpGrdTipo,
            this.lkpGrdLinea,
            this.lkpgrdMarca});
            this.dgrMotonave.Size = new System.Drawing.Size(690, 337);
            this.dgrMotonave.TabIndex = 10;
            this.dgrMotonave.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewMotonave});
            this.dgrMotonave.Click += new System.EventHandler(this.dgrMotonave_Click);
            // 
            // viewMotonave
            // 
            this.viewMotonave.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idf_almacen,
            this.descripcion,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.viewMotonave.GridControl = this.dgrMotonave;
            this.viewMotonave.GroupPanelText = "Resultado de la Busqueda";
            this.viewMotonave.Name = "viewMotonave";
            this.viewMotonave.DoubleClick += new System.EventHandler(this.viewMotonave_DoubleClick);
            // 
            // idf_almacen
            // 
            this.idf_almacen.AppearanceCell.Options.UseTextOptions = true;
            this.idf_almacen.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.idf_almacen.Caption = "Codigo";
            this.idf_almacen.FieldName = "mo_viid_modelo";
            this.idf_almacen.Name = "idf_almacen";
            this.idf_almacen.OptionsColumn.AllowEdit = false;
            this.idf_almacen.Width = 63;
            // 
            // descripcion
            // 
            this.descripcion.Caption = "Codigo de Modelo";
            this.descripcion.FieldName = "mo_vdescripcion";
            this.descripcion.Name = "descripcion";
            this.descripcion.OptionsColumn.AllowEdit = false;
            this.descripcion.OptionsColumn.AllowFocus = false;
            this.descripcion.Visible = true;
            this.descripcion.VisibleIndex = 0;
            this.descripcion.Width = 149;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Categoria";
            this.gridColumn1.ColumnEdit = this.lkpGrdCategoria;
            this.gridColumn1.FieldName = "pr_iid_tipo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 95;
            // 
            // lkpGrdCategoria
            // 
            this.lkpGrdCategoria.AutoHeight = false;
            this.lkpGrdCategoria.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpGrdCategoria.Name = "lkpGrdCategoria";
            this.lkpGrdCategoria.NullText = "";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tipo";
            this.gridColumn2.ColumnEdit = this.lkpGrdTipo;
            this.gridColumn2.FieldName = "pr_iid_categoria";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 95;
            // 
            // lkpGrdTipo
            // 
            this.lkpGrdTipo.AutoHeight = false;
            this.lkpGrdTipo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpGrdTipo.Name = "lkpGrdTipo";
            this.lkpGrdTipo.NullText = "";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Linea";
            this.gridColumn3.ColumnEdit = this.lkpGrdLinea;
            this.gridColumn3.FieldName = "pr_iid_linea";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 95;
            // 
            // lkpGrdLinea
            // 
            this.lkpGrdLinea.AutoHeight = false;
            this.lkpGrdLinea.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpGrdLinea.Name = "lkpGrdLinea";
            this.lkpGrdLinea.NullText = "";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Capellada";
            this.gridColumn4.FieldName = "pr_iid_capellada_descripcion";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            this.gridColumn4.Width = 95;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Planta";
            this.gridColumn5.FieldName = "pr_iid_planta_descripcion";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.Width = 95;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Forro";
            this.gridColumn6.FieldName = "pr_iid_forro_descripcion";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.AllowMove = false;
            this.gridColumn6.Width = 95;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Taco";
            this.gridColumn7.FieldName = "pr_iid_taco_descripcion";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMove = false;
            this.gridColumn7.Width = 105;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Marca";
            this.gridColumn8.ColumnEdit = this.lkpgrdMarca;
            this.gridColumn8.FieldName = "tarec_icorrelativo";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            // 
            // lkpgrdMarca
            // 
            this.lkpgrdMarca.AutoHeight = false;
            this.lkpgrdMarca.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpgrdMarca.Name = "lkpgrdMarca";
            this.lkpgrdMarca.NullText = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(110, 26);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.menuToolStripMenuItem.Text = "Nuevo";
            this.menuToolStripMenuItem.Click += new System.EventHandler(this.menuToolStripMenuItem_Click);
            // 
            // ListarModelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 411);
            this.Controls.Add(this.dgrMotonave);
            this.Controls.Add(this.groupControl1);
            this.Name = "ListarModelo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listar Modelo";
            this.Load += new System.EventHandler(this.ListarModelo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrMotonave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewMotonave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGrdCategoria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGrdTipo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGrdLinea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpgrdMarca)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtdescripcion;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private DevExpress.XtraGrid.GridControl dgrMotonave;
        private DevExpress.XtraGrid.Views.Grid.GridView viewMotonave;
        private DevExpress.XtraGrid.Columns.GridColumn idf_almacen;
        private DevExpress.XtraGrid.Columns.GridColumn descripcion;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkpGrdCategoria;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkpGrdTipo;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkpGrdLinea;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkpgrdMarca;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
    }
}