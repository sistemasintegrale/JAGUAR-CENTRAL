﻿namespace SGE.WindowForms.Compras.Consultas_de_Cuentas_Corrientes
{
    partial class Frm02EstadoCuentaProveedores
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
            this.cbActivarFiltro = new System.Windows.Forms.CheckBox();
            this.txtcodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtNombre = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dgrProveedores = new DevExpress.XtraGrid.GridControl();
            this.mnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.todos = new System.Windows.Forms.ToolStripMenuItem();
            this.pendientes = new System.Windows.Forms.ToolStripMenuItem();
            this.EstadoCuenta = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirLista = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirConDocumentos = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirSoloPendientes = new System.Windows.Forms.ToolStripMenuItem();
            this.viewProveedores = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrProveedores)).BeginInit();
            this.mnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewProveedores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cbActivarFiltro);
            this.groupControl1.Controls.Add(this.txtcodigo);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtNombre);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(740, 88);
            this.groupControl1.TabIndex = 12;
            this.groupControl1.Text = "Estado de Cuentas";
            // 
            // cbActivarFiltro
            // 
            this.cbActivarFiltro.AutoSize = true;
            this.cbActivarFiltro.Location = new System.Drawing.Point(8, 61);
            this.cbActivarFiltro.Name = "cbActivarFiltro";
            this.cbActivarFiltro.Size = new System.Drawing.Size(92, 17);
            this.cbActivarFiltro.TabIndex = 50;
            this.cbActivarFiltro.Text = "Activar Filtros";
            this.cbActivarFiltro.UseVisualStyleBackColor = true;
            this.cbActivarFiltro.CheckedChanged += new System.EventHandler(this.cbActivarFiltro_CheckedChanged);
            // 
            // txtcodigo
            // 
            this.txtcodigo.Location = new System.Drawing.Point(51, 35);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcodigo.Size = new System.Drawing.Size(102, 20);
            this.txtcodigo.TabIndex = 3;
            this.txtcodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNombre_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Código:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(282, 35);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Size = new System.Drawing.Size(303, 20);
            this.txtNombre.TabIndex = 1;
            this.txtNombre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNombre_KeyUp);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(171, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(105, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nombre/Razón Social:";
            // 
            // dgrProveedores
            // 
            this.dgrProveedores.ContextMenuStrip = this.mnu;
            this.dgrProveedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrProveedores.Location = new System.Drawing.Point(0, 88);
            this.dgrProveedores.MainView = this.viewProveedores;
            this.dgrProveedores.Name = "dgrProveedores";
            this.dgrProveedores.Size = new System.Drawing.Size(740, 315);
            this.dgrProveedores.TabIndex = 13;
            this.dgrProveedores.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewProveedores});
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.todos,
            this.pendientes,
            this.EstadoCuenta});
            this.mnu.Name = "contextMenuStrip1";
            this.mnu.Size = new System.Drawing.Size(223, 70);
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
            // EstadoCuenta
            // 
            this.EstadoCuenta.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirLista,
            this.imprimirConDocumentos,
            this.imprimirSoloPendientes});
            this.EstadoCuenta.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.EstadoCuenta.Name = "EstadoCuenta";
            this.EstadoCuenta.Size = new System.Drawing.Size(222, 22);
            this.EstadoCuenta.Text = "Imprimir Estado de Cuenta";
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
            // viewProveedores
            // 
            this.viewProveedores.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn5,
            this.gridColumn6});
            this.viewProveedores.GridControl = this.dgrProveedores;
            this.viewProveedores.GroupPanelText = " ";
            this.viewProveedores.Name = "viewProveedores";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Código";
            this.gridColumn3.FieldName = "vcod_proveedor";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 142;
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
            this.gridColumn1.FieldName = "vnombrecompleto";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 357;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Saldo S/.";
            this.gridColumn2.DisplayFormat.FormatString = "n2";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "doxpc_nmonto_total_saldo_soles";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 136;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Saldo $";
            this.gridColumn5.DisplayFormat.FormatString = "n2";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "doxpc_nmonto_total_saldo_dolares";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 128;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Situación";
            this.gridColumn6.FieldName = "Situacion";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            this.gridColumn6.Width = 65;
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
            this.barManager1.MaxItemId = 1;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(740, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 403);
            this.barDockControlBottom.Size = new System.Drawing.Size(740, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 403);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(740, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 403);
            // 
            // Frm02EstadoCuentaProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 426);
            this.Controls.Add(this.dgrProveedores);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "Frm02EstadoCuentaProveedores";
            this.Text = "Estado de Cuenta por Proveedores";
            this.Load += new System.EventHandler(this.FrmEstadoCuentaProveedores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrProveedores)).EndInit();
            this.mnu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewProveedores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtNombre;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl dgrProveedores;
        private DevExpress.XtraGrid.Views.Grid.GridView viewProveedores;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.TextEdit txtcodigo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ContextMenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem todos;
        private System.Windows.Forms.ToolStripMenuItem EstadoCuenta;
        private System.Windows.Forms.ToolStripMenuItem imprimirLista;
        private System.Windows.Forms.ToolStripMenuItem imprimirConDocumentos;
        private System.Windows.Forms.ToolStripMenuItem imprimirSoloPendientes;
        private System.Windows.Forms.ToolStripMenuItem pendientes;
        private System.Windows.Forms.CheckBox cbActivarFiltro;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}