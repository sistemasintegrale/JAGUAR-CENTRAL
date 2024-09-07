namespace SGE.WindowForms.Producción.Consulta
{
    partial class FrmListarCatalogoProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListarCatalogoProducto));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.LkpColor = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.LkpModelo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.LkpMarca = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dgrMotonave = new DevExpress.XtraGrid.GridControl();
            this.mnuAlmacen = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMotonave = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idf_almacen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.descripcion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lkpgrdUM = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpModelo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpMarca.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrMotonave)).BeginInit();
            this.mnuAlmacen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewMotonave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpgrdUM)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.LkpColor);
            this.groupControl1.Controls.Add(this.labelControl11);
            this.groupControl1.Controls.Add(this.LkpModelo);
            this.groupControl1.Controls.Add(this.labelControl15);
            this.groupControl1.Controls.Add(this.LkpMarca);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1125, 64);
            this.groupControl1.TabIndex = 75;
            this.groupControl1.Text = "Relacion de Catalogos de Productos";
            // 
            // LkpColor
            // 
            this.LkpColor.Location = new System.Drawing.Point(447, 31);
            this.LkpColor.Name = "LkpColor";
            this.LkpColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpColor.Properties.NullText = "";
            this.LkpColor.Size = new System.Drawing.Size(143, 20);
            this.LkpColor.TabIndex = 79;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(405, 34);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(32, 13);
            this.labelControl11.TabIndex = 80;
            this.labelControl11.Text = "Color :";
            // 
            // LkpModelo
            // 
            this.LkpModelo.Location = new System.Drawing.Point(250, 31);
            this.LkpModelo.Name = "LkpModelo";
            this.LkpModelo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpModelo.Properties.NullText = "";
            this.LkpModelo.Size = new System.Drawing.Size(143, 20);
            this.LkpModelo.TabIndex = 76;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(203, 35);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(41, 13);
            this.labelControl15.TabIndex = 78;
            this.labelControl15.Text = "Modelo :";
            // 
            // LkpMarca
            // 
            this.LkpMarca.Location = new System.Drawing.Point(54, 32);
            this.LkpMarca.Name = "LkpMarca";
            this.LkpMarca.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpMarca.Properties.NullText = "";
            this.LkpMarca.Size = new System.Drawing.Size(143, 20);
            this.LkpMarca.TabIndex = 75;
            this.LkpMarca.EditValueChanged += new System.EventHandler(this.LkpMarca_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 35);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 13);
            this.labelControl4.TabIndex = 77;
            this.labelControl4.Text = "Marca :";
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(605, 29);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(86, 22);
            this.simpleButton1.TabIndex = 71;
            this.simpleButton1.Text = "Buscar";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // dgrMotonave
            // 
            this.dgrMotonave.ContextMenuStrip = this.mnuAlmacen;
            this.dgrMotonave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrMotonave.Location = new System.Drawing.Point(0, 64);
            this.dgrMotonave.MainView = this.viewMotonave;
            this.dgrMotonave.Name = "dgrMotonave";
            this.dgrMotonave.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lkpgrdUM});
            this.dgrMotonave.Size = new System.Drawing.Size(1125, 304);
            this.dgrMotonave.TabIndex = 76;
            this.dgrMotonave.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewMotonave});
            // 
            // mnuAlmacen
            // 
            this.mnuAlmacen.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.mnuAlmacen.Name = "contextMenuStrip1";
            this.mnuAlmacen.Size = new System.Drawing.Size(121, 48);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.nuevoToolStripMenuItem.Text = "Foto";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // viewMotonave
            // 
            this.viewMotonave.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idf_almacen,
            this.descripcion,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4});
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
            this.idf_almacen.FieldName = "pr_vcodigo_externo";
            this.idf_almacen.Name = "idf_almacen";
            this.idf_almacen.OptionsColumn.AllowEdit = false;
            this.idf_almacen.OptionsColumn.AllowFocus = false;
            this.idf_almacen.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.idf_almacen.OptionsColumn.AllowMove = false;
            this.idf_almacen.OptionsColumn.ReadOnly = true;
            this.idf_almacen.Visible = true;
            this.idf_almacen.VisibleIndex = 3;
            this.idf_almacen.Width = 130;
            // 
            // descripcion
            // 
            this.descripcion.Caption = "Descripcion";
            this.descripcion.FieldName = "pr_vdescripcion_producto";
            this.descripcion.Name = "descripcion";
            this.descripcion.OptionsColumn.AllowEdit = false;
            this.descripcion.OptionsColumn.AllowFocus = false;
            this.descripcion.Visible = true;
            this.descripcion.VisibleIndex = 0;
            this.descripcion.Width = 454;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Situacion";
            this.gridColumn1.FieldName = "strSituacion";
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
            this.gridColumn1.Width = 84;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Abreviado";
            this.gridColumn2.FieldName = "pr_vabreviatura_producto";
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
            this.gridColumn2.Width = 113;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "U.M.";
            this.gridColumn4.ColumnEdit = this.lkpgrdUM;
            this.gridColumn4.FieldName = "unidc_icod_unidad_medida";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // lkpgrdUM
            // 
            this.lkpgrdUM.AutoHeight = false;
            this.lkpgrdUM.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpgrdUM.Name = "lkpgrdUM";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "U.M";
            this.gridColumn3.FieldName = "abreviaUnidadMedi";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // FrmListarCatalogoProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 368);
            this.Controls.Add(this.dgrMotonave);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmListarCatalogoProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relación de Productos";
            this.Load += new System.EventHandler(this.FrmListarCatalogoProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LkpColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpModelo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpMarca.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrMotonave)).EndInit();
            this.mnuAlmacen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewMotonave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpgrdUM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl dgrMotonave;
        private DevExpress.XtraGrid.Views.Grid.GridView viewMotonave;
        private DevExpress.XtraGrid.Columns.GridColumn idf_almacen;
        private DevExpress.XtraGrid.Columns.GridColumn descripcion;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        public DevExpress.XtraEditors.LookUpEdit LkpModelo;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        public DevExpress.XtraEditors.LookUpEdit LkpMarca;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.LookUpEdit LkpColor;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private System.Windows.Forms.ContextMenuStrip mnuAlmacen;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lkpgrdUM;
    }
}