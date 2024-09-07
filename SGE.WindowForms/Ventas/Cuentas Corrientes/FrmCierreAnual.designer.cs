namespace SGE.WindowForms.Ventas.Cuentas_Corrientes
{
    partial class FrmCierreAnual
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
            this.view = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cierreDelEjercicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgr)).BeginInit();
            this.mnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.view)).BeginInit();
            this.SuspendLayout();
            // 
            // dgr
            // 
            this.dgr.ContextMenuStrip = this.mnu;
            this.dgr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgr.Location = new System.Drawing.Point(0, 0);
            this.dgr.MainView = this.view;
            this.dgr.Name = "dgr";
            this.dgr.Size = new System.Drawing.Size(1052, 452);
            this.dgr.TabIndex = 10;
            this.dgr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.view});
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.todos,
            this.pendientes,
            this.cierreDelEjercicioToolStripMenuItem});
            this.mnu.Name = "contextMenuStrip1";
            this.mnu.Size = new System.Drawing.Size(223, 92);
            // 
            // todos
            // 
            this.todos.Image = global::SGE.WindowForms.Properties.Resources.page_white_find;
            this.todos.Name = "todos";
            this.todos.Size = new System.Drawing.Size(222, 22);
            this.todos.Text = "Ver todos los documentos";
            this.todos.Click += new System.EventHandler(this.todos_Click);
            // 
            // pendientes
            // 
            this.pendientes.Image = global::SGE.WindowForms.Properties.Resources.page_white_find;
            this.pendientes.Name = "pendientes";
            this.pendientes.Size = new System.Drawing.Size(222, 22);
            this.pendientes.Text = "Ver documentos pendientes";
            this.pendientes.Click += new System.EventHandler(this.pendientes_Click);
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
            // cierreDelEjercicioToolStripMenuItem
            // 
            this.cierreDelEjercicioToolStripMenuItem.Name = "cierreDelEjercicioToolStripMenuItem";
            this.cierreDelEjercicioToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.cierreDelEjercicioToolStripMenuItem.Text = "Cierre del Ejercicio";
            this.cierreDelEjercicioToolStripMenuItem.Click += new System.EventHandler(this.cierreDelEjercicioToolStripMenuItem_Click);
            // 
            // FrmCierreAnual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 452);
            this.Controls.Add(this.dgr);
            this.Name = "FrmCierreAnual";
            this.Text = "Pase de Cuentas por Cobrar";
            this.Load += new System.EventHandler(this.FrmEstadoCuentaClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgr)).EndInit();
            this.mnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.view)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private System.Windows.Forms.ToolStripMenuItem cierreDelEjercicioToolStripMenuItem;
    }
}