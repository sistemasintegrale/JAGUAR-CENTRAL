namespace SGE.WindowForms.Ventas.Factura_Electronica
{
    partial class Frm10GuiaRemisionElectronica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm10GuiaRemisionElectronica));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.lkpEstadoSunat = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grdGuiaRemision = new DevExpress.XtraGrid.GridControl();
            this.mnuMarca = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enviarASunatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirElectrónicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descargarCDRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descargarXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewGuiaRemision = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnNuevo = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnEliminar = new DevExpress.XtraBars.BarButtonItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.darDeBajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEstadoSunat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGuiaRemision)).BeginInit();
            this.mnuMarca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewGuiaRemision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.chkTodos);
            this.groupControl1.Controls.Add(this.lkpEstadoSunat);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.pictureBox2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.pictureBox1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1455, 74);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Criterios de búsqueda";
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Location = new System.Drawing.Point(263, 37);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(55, 17);
            this.chkTodos.TabIndex = 73;
            this.chkTodos.Text = "Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            this.chkTodos.CheckedChanged += new System.EventHandler(this.chkTodos_CheckedChanged);
            // 
            // lkpEstadoSunat
            // 
            this.lkpEstadoSunat.Location = new System.Drawing.Point(98, 36);
            this.lkpEstadoSunat.Name = "lkpEstadoSunat";
            this.lkpEstadoSunat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpEstadoSunat.Properties.NullText = "";
            this.lkpEstadoSunat.Size = new System.Drawing.Size(150, 20);
            this.lkpEstadoSunat.TabIndex = 71;
            this.lkpEstadoSunat.EditValueChanged += new System.EventHandler(this.lkpEstadoSunat_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(23, 39);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(71, 13);
            this.labelControl4.TabIndex = 72;
            this.labelControl4.Text = "Estado Sunat :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(442, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 70;
            this.label2.Text = "Aprobado";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.LightGreen;
            this.pictureBox2.Location = new System.Drawing.Point(422, 37);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(14, 15);
            this.pictureBox2.TabIndex = 69;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(532, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Con Exceso de Dias";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Salmon;
            this.pictureBox1.Location = new System.Drawing.Point(512, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 15);
            this.pictureBox1.TabIndex = 67;
            this.pictureBox1.TabStop = false;
            // 
            // grdGuiaRemision
            // 
            this.grdGuiaRemision.ContextMenuStrip = this.mnuMarca;
            this.grdGuiaRemision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGuiaRemision.Location = new System.Drawing.Point(0, 74);
            this.grdGuiaRemision.MainView = this.viewGuiaRemision;
            this.grdGuiaRemision.Name = "grdGuiaRemision";
            this.grdGuiaRemision.Size = new System.Drawing.Size(1455, 301);
            this.grdGuiaRemision.TabIndex = 7;
            this.grdGuiaRemision.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewGuiaRemision});
            this.grdGuiaRemision.Click += new System.EventHandler(this.grdGuiaRemision_Click);
            // 
            // mnuMarca
            // 
            this.mnuMarca.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enviarASunatToolStripMenuItem,
            this.imprimirElectrónicoToolStripMenuItem,
            this.descargarCDRToolStripMenuItem,
            this.descargarXMLToolStripMenuItem,
            this.darDeBajaToolStripMenuItem});
            this.mnuMarca.Name = "contextMenuStrip1";
            this.mnuMarca.Size = new System.Drawing.Size(183, 136);
            this.mnuMarca.Opening += new System.ComponentModel.CancelEventHandler(this.mnu_Opening);
            // 
            // enviarASunatToolStripMenuItem
            // 
            this.enviarASunatToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.EnviarSUNAT;
            this.enviarASunatToolStripMenuItem.Name = "enviarASunatToolStripMenuItem";
            this.enviarASunatToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.enviarASunatToolStripMenuItem.Text = "Enviar a Sunat";
            this.enviarASunatToolStripMenuItem.Click += new System.EventHandler(this.enviarASunatToolStripMenuItem_Click);
            // 
            // imprimirElectrónicoToolStripMenuItem
            // 
            this.imprimirElectrónicoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirElectrónicoToolStripMenuItem.Name = "imprimirElectrónicoToolStripMenuItem";
            this.imprimirElectrónicoToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.imprimirElectrónicoToolStripMenuItem.Text = "Imprimir Electrónico";
            this.imprimirElectrónicoToolStripMenuItem.Click += new System.EventHandler(this.imprimirElectrónicoToolStripMenuItem_Click);
            // 
            // descargarCDRToolStripMenuItem
            // 
            this.descargarCDRToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.descargar;
            this.descargarCDRToolStripMenuItem.Name = "descargarCDRToolStripMenuItem";
            this.descargarCDRToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.descargarCDRToolStripMenuItem.Text = "Descargar CDR";
            this.descargarCDRToolStripMenuItem.Click += new System.EventHandler(this.descargarCDRToolStripMenuItem_Click);
            // 
            // descargarXMLToolStripMenuItem
            // 
            this.descargarXMLToolStripMenuItem.Name = "descargarXMLToolStripMenuItem";
            this.descargarXMLToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.descargarXMLToolStripMenuItem.Text = "Descargar XML";
            this.descargarXMLToolStripMenuItem.Click += new System.EventHandler(this.descargarXMLToolStripMenuItem_Click);
            // 
            // viewGuiaRemision
            // 
            this.viewGuiaRemision.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn6,
            this.gridColumn7});
            this.viewGuiaRemision.GridControl = this.grdGuiaRemision;
            this.viewGuiaRemision.GroupPanelText = " ";
            this.viewGuiaRemision.Name = "viewGuiaRemision";
            this.viewGuiaRemision.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.viewGuiaRemision_RowStyle);
            this.viewGuiaRemision.DoubleClick += new System.EventHandler(this.viewGuiaRemision_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Número";
            this.gridColumn1.FieldName = "remic_vnumero_remision";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 102;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Fecha";
            this.gridColumn2.FieldName = "remic_sfecha_remision";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 87;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Destinatario";
            this.gridColumn3.FieldName = "remic_vnombre_destinatario";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 271;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Fecha Envio";
            this.gridColumn10.FieldName = "FechaEnvio";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.AllowFocus = false;
            this.gridColumn10.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Estado Sunat";
            this.gridColumn11.FieldName = "EstadoSunat";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.AllowFocus = false;
            this.gridColumn11.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 4;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Mensaje Error";
            this.gridColumn6.FieldName = "MensajeError";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.AllowFocus = false;
            this.gridColumn6.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Días";
            this.gridColumn7.FieldName = "Dias";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.AllowFocus = false;
            this.gridColumn7.OptionsColumn.AllowIncrementalSearch = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 48;
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
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnNuevo,
            this.btnModificar,
            this.btnEliminar});
            this.barManager1.MaxItemId = 3;
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
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1455, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 375);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1455, 22);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 375);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1455, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 375);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Caption = "[F7]";
            this.btnNuevo.Id = 0;
            this.btnNuevo.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.btnNuevo.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnNuevo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNuevo_ItemClick);
            // 
            // btnModificar
            // 
            this.btnModificar.Caption = "[F5]";
            this.btnModificar.Id = 1;
            this.btnModificar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnModificar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModificar_ItemClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Caption = "[F9]";
            this.btnEliminar.Id = 2;
            this.btnEliminar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.btnEliminar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnEliminar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEliminar_ItemClick);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(660, 174);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(93, 74);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLoading.TabIndex = 37;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // darDeBajaToolStripMenuItem
            // 
            this.darDeBajaToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.page_red;
            this.darDeBajaToolStripMenuItem.Name = "darDeBajaToolStripMenuItem";
            this.darDeBajaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.darDeBajaToolStripMenuItem.Text = "Dar de Baja";
            this.darDeBajaToolStripMenuItem.Click += new System.EventHandler(this.darDeBajaToolStripMenuItem_Click);
            // 
            // Frm10GuiaRemisionElectronica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 397);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.grdGuiaRemision);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "Frm10GuiaRemisionElectronica";
            this.Text = "Envío Guía Remisión Electrónica";
            this.Load += new System.EventHandler(this.Frm04Factura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEstadoSunat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGuiaRemision)).EndInit();
            this.mnuMarca.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewGuiaRemision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdGuiaRemision;
        private DevExpress.XtraGrid.Views.Grid.GridView viewGuiaRemision;
        private System.Windows.Forms.ContextMenuStrip mnuMarca;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnNuevo;
        private DevExpress.XtraBars.BarButtonItem btnModificar;
        private DevExpress.XtraBars.BarButtonItem btnEliminar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private System.Windows.Forms.ToolStripMenuItem enviarASunatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirElectrónicoToolStripMenuItem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.CheckBox chkTodos;
        private DevExpress.XtraEditors.LookUpEdit lkpEstadoSunat;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.ToolStripMenuItem descargarCDRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descargarXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darDeBajaToolStripMenuItem;
    }
}