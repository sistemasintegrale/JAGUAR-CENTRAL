namespace SGE.WindowForms.Ventas.Registro_de_Datos_de_Ventas
{
    partial class Frm03Cliente
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
            this.dgrCliente = new DevExpress.XtraGrid.GridControl();
            this.mnuCliente = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.direccionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCliente = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn28 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtRazonSocial = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.LkpVendedor = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.LkpGiro = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.LkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrCliente)).BeginInit();
            this.mnuCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazonSocial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpVendedor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpGiro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrCliente
            // 
            this.dgrCliente.ContextMenuStrip = this.mnuCliente;
            this.dgrCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrCliente.Location = new System.Drawing.Point(0, 81);
            this.dgrCliente.MainView = this.viewCliente;
            this.dgrCliente.Name = "dgrCliente";
            this.dgrCliente.Size = new System.Drawing.Size(1019, 371);
            this.dgrCliente.TabIndex = 10;
            this.dgrCliente.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewCliente});
            // 
            // mnuCliente
            // 
            this.mnuCliente.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.imprimirToolStripMenuItem,
            this.direccionToolStripMenuItem});
            this.mnuCliente.Name = "contextMenuStrip1";
            this.mnuCliente.Size = new System.Drawing.Size(153, 136);
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
            // direccionToolStripMenuItem
            // 
            this.direccionToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_sub;
            this.direccionToolStripMenuItem.Name = "direccionToolStripMenuItem";
            this.direccionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.direccionToolStripMenuItem.Text = "Direcciones";
            this.direccionToolStripMenuItem.Click += new System.EventHandler(this.direccionToolStripMenuItem_Click);
            // 
            // viewCliente
            // 
            this.viewCliente.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn9,
            this.gridColumn8,
            this.gridColumn7,
            this.gridColumn5,
            this.gridColumn28,
            this.gridColumn10,
            this.gridColumn11});
            this.viewCliente.GridControl = this.dgrCliente;
            this.viewCliente.GroupPanelText = "Clientes";
            this.viewCliente.Name = "viewCliente";
            this.viewCliente.OptionsView.ColumnAutoWidth = false;
            this.viewCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.viewCliente_KeyUp);
            this.viewCliente.DoubleClick += new System.EventHandler(this.viewCliente_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Código";
            this.gridColumn1.FieldName = "cliec_vcod_cliente";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 100;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Giro";
            this.gridColumn3.FieldName = "giroc_vnombre_giro";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 113;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Situación";
            this.gridColumn2.FieldName = "Situacion";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 78;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Nombre/Razón Social";
            this.gridColumn4.FieldName = "cliec_vnombre_cliente";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 224;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Dirección";
            this.gridColumn6.FieldName = "cliec_vdireccion_cliente";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn6.OptionsColumn.AllowMove = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 146;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Teléfono";
            this.gridColumn9.FieldName = "cliec_vnro_telefono";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.AllowFocus = false;
            this.gridColumn9.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn9.OptionsColumn.AllowMove = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 5;
            this.gridColumn9.Width = 90;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Correo";
            this.gridColumn8.FieldName = "cliec_vcorreo_electronico";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.AllowFocus = false;
            this.gridColumn8.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn8.OptionsColumn.AllowMove = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 103;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "FAX";
            this.gridColumn7.FieldName = "cliec_vnro_fax";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.OptionsColumn.AllowMove = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 94;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "DNI";
            this.gridColumn5.FieldName = "cliec_vnumero_doc_cli";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 8;
            this.gridColumn5.Width = 100;
            // 
            // gridColumn28
            // 
            this.gridColumn28.Caption = "RUC";
            this.gridColumn28.FieldName = "cliec_cruc";
            this.gridColumn28.Name = "gridColumn28";
            this.gridColumn28.OptionsColumn.AllowEdit = false;
            this.gridColumn28.OptionsColumn.AllowFocus = false;
            this.gridColumn28.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn28.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn28.OptionsColumn.AllowMove = false;
            this.gridColumn28.Visible = true;
            this.gridColumn28.VisibleIndex = 9;
            this.gridColumn28.Width = 105;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Vendedor";
            this.gridColumn10.FieldName = "vendc_vnombre_vendedor";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowFocus = false;
            this.gridColumn10.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsColumn.AllowMove = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 10;
            this.gridColumn10.Width = 137;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Representante";
            this.gridColumn11.FieldName = "cliec_vnombre_contacto";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.AllowFocus = false;
            this.gridColumn11.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn11.OptionsColumn.AllowMove = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 11;
            this.gridColumn11.Width = 112;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtRazonSocial);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Controls.Add(this.radioGroup1);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Controls.Add(this.LkpVendedor);
            this.groupControl1.Controls.Add(this.labelControl20);
            this.groupControl1.Controls.Add(this.LkpGiro);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.LkpSituacion);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1019, 81);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Criterios de búsqueda";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(293, 55);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(105, 13);
            this.labelControl1.TabIndex = 38;
            this.labelControl1.Text = "Nombre/Razón Social:";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Location = new System.Drawing.Point(404, 52);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(120, 20);
            this.txtRazonSocial.TabIndex = 37;
            this.txtRazonSocial.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp_1);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(114, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 36;
            this.labelControl2.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(167, 52);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(120, 20);
            this.txtCodigo.TabIndex = 35;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp_1);
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(12, 20);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "DNI"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "RUC"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Todos")});
            this.radioGroup1.Size = new System.Drawing.Size(96, 59);
            this.radioGroup1.TabIndex = 34;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(546, 34);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(50, 13);
            this.labelControl14.TabIndex = 0;
            this.labelControl14.Text = "Vendedor:";
            // 
            // LkpVendedor
            // 
            this.LkpVendedor.Location = new System.Drawing.Point(602, 31);
            this.LkpVendedor.Name = "LkpVendedor";
            this.LkpVendedor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpVendedor.Properties.NullText = "";
            this.LkpVendedor.Size = new System.Drawing.Size(120, 20);
            this.LkpVendedor.TabIndex = 5;
            this.LkpVendedor.EditValueChanged += new System.EventHandler(this.LkpVendedor_EditValueChanged);
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(329, 34);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(23, 13);
            this.labelControl20.TabIndex = 0;
            this.labelControl20.Text = "Giro:";
            // 
            // LkpGiro
            // 
            this.LkpGiro.Location = new System.Drawing.Point(404, 31);
            this.LkpGiro.Name = "LkpGiro";
            this.LkpGiro.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpGiro.Properties.NullText = "";
            this.LkpGiro.Size = new System.Drawing.Size(120, 20);
            this.LkpGiro.TabIndex = 4;
            this.LkpGiro.EditValueChanged += new System.EventHandler(this.LkpVendedor_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(114, 34);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Situación:";
            // 
            // LkpSituacion
            // 
            this.LkpSituacion.Location = new System.Drawing.Point(167, 31);
            this.LkpSituacion.Name = "LkpSituacion";
            this.LkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpSituacion.Properties.NullText = "";
            this.LkpSituacion.Size = new System.Drawing.Size(120, 20);
            this.LkpSituacion.TabIndex = 3;
            this.LkpSituacion.EditValueChanged += new System.EventHandler(this.LkpVendedor_EditValueChanged);
            // 
            // Frm03Cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 452);
            this.Controls.Add(this.dgrCliente);
            this.Controls.Add(this.groupControl1);
            this.Name = "Frm03Cliente";
            this.Text = "Registro de Clientes";
            this.Load += new System.EventHandler(this.FrmCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrCliente)).EndInit();
            this.mnuCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRazonSocial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpVendedor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpGiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dgrCliente;
        private DevExpress.XtraGrid.Views.Grid.GridView viewCliente;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ContextMenuStrip mnuCliente;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn28;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.LookUpEdit LkpSituacion;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        public DevExpress.XtraEditors.LookUpEdit LkpGiro;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        public DevExpress.XtraEditors.LookUpEdit LkpVendedor;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtRazonSocial;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private System.Windows.Forms.ToolStripMenuItem direccionToolStripMenuItem;
    }
}