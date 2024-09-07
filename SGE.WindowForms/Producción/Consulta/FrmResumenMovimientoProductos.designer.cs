﻿namespace SGE.WindowForms.Producción.Consulta
{
    partial class FrmResumenMovimientoProductos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResumenMovimientoProductos));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.dtpFechaIni = new DevExpress.XtraEditors.DateEdit();
            this.dtpFechaFin = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnBuscar = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dgrNotaIngreso = new DevExpress.XtraGrid.GridControl();
            this.mnuconsulta = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kardexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewProductoEspecifico = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cantidad_ingreso = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cantidad_salida = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cantidad_saldo_prod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaIni.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaIni.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrNotaIngreso)).BeginInit();
            this.mnuconsulta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewProductoEspecifico)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.dtpFechaIni);
            this.groupControl1.Controls.Add(this.dtpFechaFin);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.btnBuscar);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(944, 64);
            this.groupControl1.TabIndex = 84;
            this.groupControl1.Text = "Kardex de un producto";
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.EditValue = null;
            this.dtpFechaIni.EnterMoveNextControl = true;
            this.dtpFechaIni.Location = new System.Drawing.Point(96, 32);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpFechaIni.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dtpFechaIni.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.dtpFechaIni.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dtpFechaIni.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFechaIni.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpFechaIni.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpFechaIni.Size = new System.Drawing.Size(99, 20);
            this.dtpFechaIni.TabIndex = 84;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.EditValue = null;
            this.dtpFechaFin.EnterMoveNextControl = true;
            this.dtpFechaFin.Location = new System.Drawing.Point(266, 32);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpFechaFin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dtpFechaFin.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.dtpFechaFin.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dtpFechaFin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFechaFin.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpFechaFin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpFechaFin.Size = new System.Drawing.Size(92, 20);
            this.dtpFechaFin.TabIndex = 68;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(222, 35);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(27, 13);
            this.labelControl4.TabIndex = 77;
            this.labelControl4.Text = "hasta";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscar.Image")));
            this.btnBuscar.Location = new System.Drawing.Point(398, 27);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(98, 28);
            this.btnBuscar.TabIndex = 71;
            this.btnBuscar.Text = "Buscar...";
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 13);
            this.labelControl1.TabIndex = 67;
            this.labelControl1.Text = "Fecha :    Desde";
            // 
            // dgrNotaIngreso
            // 
            this.dgrNotaIngreso.ContextMenuStrip = this.mnuconsulta;
            this.dgrNotaIngreso.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrNotaIngreso.Location = new System.Drawing.Point(0, 64);
            this.dgrNotaIngreso.MainView = this.viewProductoEspecifico;
            this.dgrNotaIngreso.Name = "dgrNotaIngreso";
            this.dgrNotaIngreso.Size = new System.Drawing.Size(944, 301);
            this.dgrNotaIngreso.TabIndex = 85;
            this.dgrNotaIngreso.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewProductoEspecifico});
            // 
            // mnuconsulta
            // 
            this.mnuconsulta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kardexToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.mnuconsulta.Name = "contextMenuStrip1";
            this.mnuconsulta.Size = new System.Drawing.Size(121, 48);
            // 
            // kardexToolStripMenuItem
            // 
            this.kardexToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_check;
            this.kardexToolStripMenuItem.Name = "kardexToolStripMenuItem";
            this.kardexToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.kardexToolStripMenuItem.Text = "Kadex";
            this.kardexToolStripMenuItem.Click += new System.EventHandler(this.kardexToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // viewProductoEspecifico
            // 
            this.viewProductoEspecifico.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.cantidad_ingreso,
            this.cantidad_salida,
            this.cantidad_saldo_prod,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn3,
            this.gridColumn5});
            this.viewProductoEspecifico.GridControl = this.dgrNotaIngreso;
            this.viewProductoEspecifico.GroupPanelText = "Resultado de la Busqueda";
            this.viewProductoEspecifico.Name = "viewProductoEspecifico";
            this.viewProductoEspecifico.OptionsView.ColumnAutoWidth = false;
            this.viewProductoEspecifico.OptionsView.ShowFooter = true;
            // 
            // cantidad_ingreso
            // 
            this.cantidad_ingreso.Caption = "Ingresos";
            this.cantidad_ingreso.DisplayFormat.FormatString = "n2";
            this.cantidad_ingreso.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.cantidad_ingreso.FieldName = "tipo_movimiento_ingreso";
            this.cantidad_ingreso.Name = "cantidad_ingreso";
            this.cantidad_ingreso.OptionsColumn.AllowEdit = false;
            this.cantidad_ingreso.OptionsColumn.AllowFocus = false;
            this.cantidad_ingreso.Visible = true;
            this.cantidad_ingreso.VisibleIndex = 2;
            this.cantidad_ingreso.Width = 76;
            // 
            // cantidad_salida
            // 
            this.cantidad_salida.Caption = "Salidas";
            this.cantidad_salida.DisplayFormat.FormatString = "n2";
            this.cantidad_salida.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.cantidad_salida.FieldName = "tipo_movimiento_salida";
            this.cantidad_salida.Name = "cantidad_salida";
            this.cantidad_salida.OptionsColumn.AllowEdit = false;
            this.cantidad_salida.OptionsColumn.AllowFocus = false;
            this.cantidad_salida.Visible = true;
            this.cantidad_salida.VisibleIndex = 3;
            this.cantidad_salida.Width = 89;
            // 
            // cantidad_saldo_prod
            // 
            this.cantidad_saldo_prod.Caption = "Stk. Act.";
            this.cantidad_saldo_prod.DisplayFormat.FormatString = "n2";
            this.cantidad_saldo_prod.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.cantidad_saldo_prod.FieldName = "stockActual";
            this.cantidad_saldo_prod.Name = "cantidad_saldo_prod";
            this.cantidad_saldo_prod.OptionsColumn.AllowEdit = false;
            this.cantidad_saldo_prod.OptionsColumn.AllowFocus = false;
            this.cantidad_saldo_prod.Visible = true;
            this.cantidad_saldo_prod.VisibleIndex = 4;
            this.cantidad_saldo_prod.Width = 98;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Stk. Ant.";
            this.gridColumn1.DisplayFormat.FormatString = "n2";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "stockaAnterior";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 108;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Descripción";
            this.gridColumn2.FieldName = "pr_vdescripcion_producto";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 260;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Código";
            this.gridColumn4.FieldName = "pr_vcodigo_externo";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowFocus = false;
            this.gridColumn4.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsColumn.AllowMove = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 140;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "U.M";
            this.gridColumn3.FieldName = "DesUnidadMedida";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsColumn.AllowMove = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 6;
            this.gridColumn3.Width = 70;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Almacén";
            this.gridColumn5.FieldName = "descripcionAlmacen";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowFocus = false;
            this.gridColumn5.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn5.OptionsColumn.AllowMove = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 7;
            this.gridColumn5.Width = 298;
            // 
            // FrmResumenMovimientoProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 365);
            this.Controls.Add(this.dgrNotaIngreso);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmResumenMovimientoProductos";
            this.Text = "Resumen de Movimientos de Productos";
            this.Load += new System.EventHandler(this.FrmResumenMovimientoProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaIni.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaIni.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFechaFin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrNotaIngreso)).EndInit();
            this.mnuconsulta.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewProductoEspecifico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.DateEdit dtpFechaIni;
        private DevExpress.XtraEditors.DateEdit dtpFechaFin;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnBuscar;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl dgrNotaIngreso;
        private DevExpress.XtraGrid.Views.Grid.GridView viewProductoEspecifico;
        private DevExpress.XtraGrid.Columns.GridColumn cantidad_ingreso;
        private DevExpress.XtraGrid.Columns.GridColumn cantidad_salida;
        private DevExpress.XtraGrid.Columns.GridColumn cantidad_saldo_prod;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private System.Windows.Forms.ContextMenuStrip mnuconsulta;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.ToolStripMenuItem kardexToolStripMenuItem;
    }
}