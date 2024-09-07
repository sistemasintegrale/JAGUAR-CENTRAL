namespace SGE.WindowForms.Producción.Almacenes
{
    partial class FrmTransferencia
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
            this.txtalmSalida = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtalmIngreso = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.dgrTransferencia = new DevExpress.XtraGrid.GridControl();
            this.mnuTransferencia = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTransferencia = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.numerof_transf_producto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fecha = new DevExpress.XtraGrid.Columns.GridColumn();
            this.descripcion_almacen_salida = new DevExpress.XtraGrid.Columns.GridColumn();
            this.descripcion_almacen_ingreso = new DevExpress.XtraGrid.Columns.GridColumn();
            this.observaciones = new DevExpress.XtraGrid.Columns.GridColumn();
            this.id_motivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.descripcion_motivo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnagregar = new DevExpress.XtraBars.BarButtonItem();
            this.btnmodificar = new DevExpress.XtraBars.BarButtonItem();
            this.btneliminar = new DevExpress.XtraBars.BarButtonItem();
            this.btnimprimir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtalmSalida.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtalmIngreso.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrTransferencia)).BeginInit();
            this.mnuTransferencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewTransferencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtalmSalida);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtalmIngreso);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 31);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(831, 61);
            this.groupControl1.TabIndex = 55;
            this.groupControl1.Text = "Criterios de Busqueda";
            // 
            // txtalmSalida
            // 
            this.txtalmSalida.Location = new System.Drawing.Point(471, 30);
            this.txtalmSalida.Name = "txtalmSalida";
            this.txtalmSalida.Size = new System.Drawing.Size(134, 20);
            this.txtalmSalida.TabIndex = 5;
            this.txtalmSalida.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtalmSalida_KeyUp);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(397, 33);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Alm. Salida :";
            // 
            // txtalmIngreso
            // 
            this.txtalmIngreso.Location = new System.Drawing.Point(242, 30);
            this.txtalmIngreso.Name = "txtalmIngreso";
            this.txtalmIngreso.Size = new System.Drawing.Size(133, 20);
            this.txtalmIngreso.TabIndex = 3;
            this.txtalmIngreso.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtalmIngreso_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 33);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(88, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "N° Transferencia :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(168, 33);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Alm. Ingreso :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.EditValue = "";
            this.txtCodigo.Location = new System.Drawing.Point(105, 32);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.MaxLength = 6;
            this.txtCodigo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCodigo.Size = new System.Drawing.Size(52, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // dgrTransferencia
            // 
            this.dgrTransferencia.ContextMenuStrip = this.mnuTransferencia;
            this.dgrTransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrTransferencia.Location = new System.Drawing.Point(0, 92);
            this.dgrTransferencia.MainView = this.viewTransferencia;
            this.dgrTransferencia.Name = "dgrTransferencia";
            this.dgrTransferencia.Size = new System.Drawing.Size(831, 277);
            this.dgrTransferencia.TabIndex = 56;
            this.dgrTransferencia.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTransferencia});
            // 
            // mnuTransferencia
            // 
            this.mnuTransferencia.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.mnuTransferencia.Name = "contextMenuStrip1";
            this.mnuTransferencia.Size = new System.Drawing.Size(153, 114);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
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
            // viewTransferencia
            // 
            this.viewTransferencia.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.numerof_transf_producto,
            this.fecha,
            this.descripcion_almacen_salida,
            this.descripcion_almacen_ingreso,
            this.observaciones,
            this.id_motivo,
            this.descripcion_motivo});
            this.viewTransferencia.GridControl = this.dgrTransferencia;
            this.viewTransferencia.GroupPanelText = "Resultado de la Busqueda";
            this.viewTransferencia.Name = "viewTransferencia";
            // 
            // numerof_transf_producto
            // 
            this.numerof_transf_producto.Caption = "Numero Transf.";
            this.numerof_transf_producto.DisplayFormat.FormatString = "{0:000000}";
            this.numerof_transf_producto.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numerof_transf_producto.FieldName = "trfc_inum_transf";
            this.numerof_transf_producto.Name = "numerof_transf_producto";
            this.numerof_transf_producto.OptionsColumn.AllowEdit = false;
            this.numerof_transf_producto.OptionsFilter.AllowFilter = false;
            this.numerof_transf_producto.Visible = true;
            this.numerof_transf_producto.VisibleIndex = 0;
            this.numerof_transf_producto.Width = 20;
            // 
            // fecha
            // 
            this.fecha.Caption = "Fecha";
            this.fecha.FieldName = "trfc_sfecha_transf";
            this.fecha.Name = "fecha";
            this.fecha.OptionsColumn.AllowEdit = false;
            this.fecha.OptionsColumn.AllowFocus = false;
            this.fecha.Visible = true;
            this.fecha.VisibleIndex = 1;
            this.fecha.Width = 20;
            // 
            // descripcion_almacen_salida
            // 
            this.descripcion_almacen_salida.Caption = "Alm. Salida";
            this.descripcion_almacen_salida.FieldName = "almac_vdescripcion_salida";
            this.descripcion_almacen_salida.Name = "descripcion_almacen_salida";
            this.descripcion_almacen_salida.OptionsColumn.AllowEdit = false;
            this.descripcion_almacen_salida.OptionsColumn.AllowFocus = false;
            this.descripcion_almacen_salida.Visible = true;
            this.descripcion_almacen_salida.VisibleIndex = 2;
            this.descripcion_almacen_salida.Width = 20;
            // 
            // descripcion_almacen_ingreso
            // 
            this.descripcion_almacen_ingreso.Caption = "Alm. Ingreso";
            this.descripcion_almacen_ingreso.FieldName = "almac_vdescripcion_ingreso";
            this.descripcion_almacen_ingreso.Name = "descripcion_almacen_ingreso";
            this.descripcion_almacen_ingreso.OptionsColumn.AllowEdit = false;
            this.descripcion_almacen_ingreso.OptionsColumn.AllowFocus = false;
            this.descripcion_almacen_ingreso.Visible = true;
            this.descripcion_almacen_ingreso.VisibleIndex = 3;
            this.descripcion_almacen_ingreso.Width = 20;
            // 
            // observaciones
            // 
            this.observaciones.Caption = "Observaciones";
            this.observaciones.FieldName = "trfc_vobservaciones";
            this.observaciones.Name = "observaciones";
            this.observaciones.OptionsColumn.AllowEdit = false;
            this.observaciones.OptionsColumn.AllowFocus = false;
            this.observaciones.Visible = true;
            this.observaciones.VisibleIndex = 4;
            this.observaciones.Width = 20;
            // 
            // id_motivo
            // 
            this.id_motivo.Caption = "id_motivo";
            this.id_motivo.FieldName = "id_motivo";
            this.id_motivo.Name = "id_motivo";
            this.id_motivo.OptionsColumn.AllowEdit = false;
            this.id_motivo.OptionsColumn.AllowFocus = false;
            this.id_motivo.Width = 20;
            // 
            // descripcion_motivo
            // 
            this.descripcion_motivo.Caption = "Motivo";
            this.descripcion_motivo.FieldName = "descripcion_motivo";
            this.descripcion_motivo.Name = "descripcion_motivo";
            this.descripcion_motivo.OptionsColumn.AllowEdit = false;
            this.descripcion_motivo.OptionsColumn.AllowFocus = false;
            this.descripcion_motivo.Width = 20;
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
            this.btneliminar,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btneliminar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnimprimir)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnagregar
            // 
            this.btnagregar.Caption = "barButtonItem1";
            this.btnagregar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.btnagregar.Id = 0;
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnagregar_ItemClick_1);
            // 
            // btnmodificar
            // 
            this.btnmodificar.Caption = "barButtonItem2";
            this.btnmodificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnmodificar.Id = 1;
            this.btnmodificar.Name = "btnmodificar";
            this.btnmodificar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnmodificar_ItemClick);
            // 
            // btneliminar
            // 
            this.btneliminar.Caption = "barButtonItem3";
            this.btneliminar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.btneliminar.Id = 2;
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btneliminar_ItemClick);
            // 
            // btnimprimir
            // 
            this.btnimprimir.Caption = "barButtonItem4";
            this.btnimprimir.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.btnimprimir.Id = 3;
            this.btnimprimir.Name = "btnimprimir";
            this.btnimprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnimprimir_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(831, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 369);
            this.barDockControlBottom.Size = new System.Drawing.Size(831, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 338);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(831, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 338);
            // 
            // FrmTransferencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 369);
            this.Controls.Add(this.dgrTransferencia);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmTransferencia";
            this.Text = "Registro de Transferencia entre Álmacenes PT";
            this.Load += new System.EventHandler(this.FrmTransferencia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtalmSalida.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtalmIngreso.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgrTransferencia)).EndInit();
            this.mnuTransferencia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewTransferencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtalmSalida;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtalmIngreso;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private DevExpress.XtraGrid.GridControl dgrTransferencia;
        private System.Windows.Forms.ContextMenuStrip mnuTransferencia;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraGrid.Views.Grid.GridView viewTransferencia;
        private DevExpress.XtraGrid.Columns.GridColumn numerof_transf_producto;
        private DevExpress.XtraGrid.Columns.GridColumn fecha;
        private DevExpress.XtraGrid.Columns.GridColumn descripcion_almacen_salida;
        private DevExpress.XtraGrid.Columns.GridColumn descripcion_almacen_ingreso;
        private DevExpress.XtraGrid.Columns.GridColumn observaciones;
        private DevExpress.XtraGrid.Columns.GridColumn id_motivo;
        private DevExpress.XtraGrid.Columns.GridColumn descripcion_motivo;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnagregar;
        private DevExpress.XtraBars.BarButtonItem btnmodificar;
        private DevExpress.XtraBars.BarButtonItem btneliminar;
        private DevExpress.XtraBars.BarButtonItem btnimprimir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}