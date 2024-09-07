namespace SGE.WindowForms.Ventas.Consultas_de_Cuentas_Corrientes
{
    partial class Frm02EstadoCuentaClientes
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
            this.dgr = new DevExpress.XtraGrid.GridControl();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.todos = new System.Windows.Forms.ToolStripMenuItem();
            this.pendientes = new System.Windows.Forms.ToolStripMenuItem();
            this.EstadoCuenta = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirLista = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirConDocumentos = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirSoloPendientes = new System.Windows.Forms.ToolStripMenuItem();
            this.CobranzaDudosa = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirListaDudosa = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirConDocumentosDudosa = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirSóloPendientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.view = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtcodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtNombre = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dgr)).BeginInit();
            this.mnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dgr
            // 
            this.dgr.ContextMenuStrip = this.mnu;
            this.dgr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgr.Location = new System.Drawing.Point(0, 40);
            this.dgr.MainView = this.view;
            this.dgr.Name = "dgr";
            this.dgr.Size = new System.Drawing.Size(1052, 412);
            this.dgr.TabIndex = 10;
            this.dgr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.view});
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.todos,
            this.pendientes,
            this.EstadoCuenta,
            this.CobranzaDudosa});
            this.mnu.Name = "contextMenuStrip1";
            this.mnu.Size = new System.Drawing.Size(267, 92);
            // 
            // todos
            // 
            this.todos.Image = global::SGE.WindowForms.Properties.Resources.page_white_find;
            this.todos.Name = "todos";
            this.todos.Size = new System.Drawing.Size(266, 22);
            this.todos.Text = "Ver todos los documentos";
            this.todos.Click += new System.EventHandler(this.todos_Click);
            // 
            // pendientes
            // 
            this.pendientes.Image = global::SGE.WindowForms.Properties.Resources.page_white_find;
            this.pendientes.Name = "pendientes";
            this.pendientes.Size = new System.Drawing.Size(266, 22);
            this.pendientes.Text = "Ver documentos pendientes";
            this.pendientes.Click += new System.EventHandler(this.pendientes_Click);
            // 
            // EstadoCuenta
            // 
            this.EstadoCuenta.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirLista,
            this.imprimirConDocumentos,
            this.imprimirSoloPendientes});
            this.EstadoCuenta.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.EstadoCuenta.Name = "EstadoCuenta";
            this.EstadoCuenta.Size = new System.Drawing.Size(266, 22);
            this.EstadoCuenta.Text = "Imprimir Estado de Cuenta";
            this.EstadoCuenta.Click += new System.EventHandler(this.EstadoCuenta_Click);
            // 
            // imprimirLista
            // 
            this.imprimirLista.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirLista.Name = "imprimirLista";
            this.imprimirLista.Size = new System.Drawing.Size(214, 22);
            this.imprimirLista.Text = "Imprimir Lista";
            this.imprimirLista.Click += new System.EventHandler(this.imprimirLista_Click);
            // 
            // imprimirConDocumentos
            // 
            this.imprimirConDocumentos.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirConDocumentos.Name = "imprimirConDocumentos";
            this.imprimirConDocumentos.Size = new System.Drawing.Size(214, 22);
            this.imprimirConDocumentos.Text = "Imprimir con Documentos";
            this.imprimirConDocumentos.Click += new System.EventHandler(this.imprimirConDocumentos_Click);
            // 
            // imprimirSoloPendientes
            // 
            this.imprimirSoloPendientes.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirSoloPendientes.Name = "imprimirSoloPendientes";
            this.imprimirSoloPendientes.Size = new System.Drawing.Size(214, 22);
            this.imprimirSoloPendientes.Text = "Imprimir sólo Pendientes";
            this.imprimirSoloPendientes.Click += new System.EventHandler(this.imprimirSoloPendientes_Click);
            // 
            // CobranzaDudosa
            // 
            this.CobranzaDudosa.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirListaDudosa,
            this.imprimirConDocumentosDudosa,
            this.imprimirSóloPendientesToolStripMenuItem});
            this.CobranzaDudosa.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.CobranzaDudosa.Name = "CobranzaDudosa";
            this.CobranzaDudosa.Size = new System.Drawing.Size(266, 22);
            this.CobranzaDudosa.Text = "Imprimir con Doc. Cobranza Dudosa";
            this.CobranzaDudosa.Click += new System.EventHandler(this.CobranzaDudosa_Click);
            // 
            // imprimirListaDudosa
            // 
            this.imprimirListaDudosa.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirListaDudosa.Name = "imprimirListaDudosa";
            this.imprimirListaDudosa.Size = new System.Drawing.Size(214, 22);
            this.imprimirListaDudosa.Text = "Imprimir Lista";
            this.imprimirListaDudosa.Click += new System.EventHandler(this.imprimirListaDudosa_Click);
            // 
            // imprimirConDocumentosDudosa
            // 
            this.imprimirConDocumentosDudosa.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirConDocumentosDudosa.Name = "imprimirConDocumentosDudosa";
            this.imprimirConDocumentosDudosa.Size = new System.Drawing.Size(214, 22);
            this.imprimirConDocumentosDudosa.Text = "Imprimir con Documentos";
            this.imprimirConDocumentosDudosa.Click += new System.EventHandler(this.imprimirConDocumentosDudosa_Click);
            // 
            // imprimirSóloPendientesToolStripMenuItem
            // 
            this.imprimirSóloPendientesToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirSóloPendientesToolStripMenuItem.Name = "imprimirSóloPendientesToolStripMenuItem";
            this.imprimirSóloPendientesToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.imprimirSóloPendientesToolStripMenuItem.Text = "Imprimir sólo Pendientes";
            this.imprimirSóloPendientesToolStripMenuItem.Click += new System.EventHandler(this.imprimirSóloPendientesToolStripMenuItem_Click);
            // 
            // view
            // 
            this.view.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn6});
            this.view.GridControl = this.dgr;
            this.view.GroupPanelText = "Relación de Proveedores con sus saldos";
            this.view.Name = "view";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Código";
            this.gridColumn3.FieldName = "cliec_vcod_cliente";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 200;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Situación";
            this.gridColumn4.FieldName = "Situacion";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nombre/Razón Social";
            this.gridColumn1.FieldName = "cliec_vnombre_cliente";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 278;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Saldo S/.";
            this.gridColumn2.DisplayFormat.FormatString = "n2";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "doxcc_nmonto_saldo_soles";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 203;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Saldo $";
            this.gridColumn5.DisplayFormat.FormatString = "n2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "doxcc_nmonto_saldo_dolares";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 210;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Giro";
            this.gridColumn6.FieldName = "giroc_vnombre_giro";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 143;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtcodigo);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtNombre);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(1052, 40);
            this.groupControl1.TabIndex = 11;
            this.groupControl1.Text = "Estado de Cuenta de Clientes";
            // 
            // txtcodigo
            // 
            this.txtcodigo.Location = new System.Drawing.Point(57, 10);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcodigo.Size = new System.Drawing.Size(108, 20);
            this.txtcodigo.TabIndex = 24;
            this.txtcodigo.EditValueChanged += new System.EventHandler(this.txtcodigo_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "Código:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(314, 10);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Size = new System.Drawing.Size(171, 20);
            this.txtNombre.TabIndex = 1;
            this.txtNombre.EditValueChanged += new System.EventHandler(this.txtNombre_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(190, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(105, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nombre/Razón Social:";
            // 
            // Frm02EstadoCuentaClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 452);
            this.Controls.Add(this.dgr);
            this.Controls.Add(this.groupControl1);
            this.Name = "Frm02EstadoCuentaClientes";
            this.Text = "Estado de Cuentas por Clientes";
            this.Load += new System.EventHandler(this.FrmEstadoCuentaClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgr)).EndInit();
            this.mnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dgr;
        private DevExpress.XtraGrid.Views.Grid.GridView view;
        private System.Windows.Forms.ContextMenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem todos;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.ToolStripMenuItem pendientes;
        private System.Windows.Forms.ToolStripMenuItem EstadoCuenta;
        private System.Windows.Forms.ToolStripMenuItem CobranzaDudosa;
        private System.Windows.Forms.ToolStripMenuItem imprimirLista;
        private System.Windows.Forms.ToolStripMenuItem imprimirConDocumentos;
        private System.Windows.Forms.ToolStripMenuItem imprimirSoloPendientes;
        private System.Windows.Forms.ToolStripMenuItem imprimirListaDudosa;
        private System.Windows.Forms.ToolStripMenuItem imprimirConDocumentosDudosa;
        private System.Windows.Forms.ToolStripMenuItem imprimirSóloPendientesToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtNombre;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.TextEdit txtcodigo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}