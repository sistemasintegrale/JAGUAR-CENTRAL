namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    partial class FrmManteEntegaMaterialPorFechaDetalle
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnAgregar = new DevExpress.XtraBars.BarButtonItem();
            this.btnsalir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.OpenFd = new System.Windows.Forms.OpenFileDialog();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtCantidad = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.dteFecha = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtStock = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtCantidadRequerida = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtTotalCantidadEntregada = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadRequerida.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCantidadEntregada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAgregar,
            this.btnModificar,
            this.btnsalir});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAgregar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnsalir)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnAgregar.Caption = "Agregar";
            this.btnAgregar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.btnAgregar.Id = 0;
            this.btnAgregar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAgregar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAgregar_ItemClick);
            // 
            // btnsalir
            // 
            this.btnsalir.Caption = "Salir";
            this.btnsalir.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnsalir.Id = 2;
            this.btnsalir.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnsalir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnsalir_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(552, 52);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 79);
            this.barDockControlBottom.Size = new System.Drawing.Size(552, 26);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 52);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 27);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(552, 52);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 27);
            // 
            // btnModificar
            // 
            this.btnModificar.Caption = "Modificar";
            this.btnModificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.Id = 1;
            this.btnModificar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // OpenFd
            // 
            this.OpenFd.FileName = "openFileDialog1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtCantidad);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.dteFecha);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.txtStock);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.txtCantidadRequerida);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.txtTotalCantidadEntregada);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(2, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(545, 75);
            this.groupControl2.TabIndex = 51;
            this.groupControl2.Text = "Datos Detalle";
            // 
            // txtCantidad
            // 
            this.txtCantidad.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            327680});
            this.txtCantidad.Location = new System.Drawing.Point(351, 47);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCantidad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCantidad.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtCantidad.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCantidad.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtCantidad.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCantidad.Properties.DisplayFormat.FormatString = "n5";
            this.txtCantidad.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCantidad.Properties.EditFormat.FormatString = "n5";
            this.txtCantidad.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCantidad.Properties.Mask.EditMask = "n5";
            this.txtCantidad.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCantidad.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCantidad.Size = new System.Drawing.Size(80, 20);
            this.txtCantidad.TabIndex = 218;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(285, 52);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(50, 13);
            this.labelControl5.TabIndex = 217;
            this.labelControl5.Text = "Cantidad :";
            // 
            // dteFecha
            // 
            this.dteFecha.EditValue = null;
            this.dteFecha.Location = new System.Drawing.Point(100, 48);
            this.dteFecha.Name = "dteFecha";
            this.dteFecha.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dteFecha.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dteFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFecha.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFecha.Size = new System.Drawing.Size(102, 20);
            this.dteFecha.TabIndex = 216;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 52);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(73, 13);
            this.labelControl3.TabIndex = 215;
            this.labelControl3.Text = "Fecha Entega :";
            // 
            // txtStock
            // 
            this.txtStock.EditValue = "0";
            this.txtStock.Enabled = false;
            this.txtStock.Location = new System.Drawing.Point(437, 25);
            this.txtStock.Name = "txtStock";
            this.txtStock.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtStock.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtStock.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtStock.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtStock.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtStock.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtStock.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtStock.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtStock.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtStock.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtStock.Size = new System.Drawing.Size(97, 20);
            this.txtStock.TabIndex = 214;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(398, 28);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(33, 13);
            this.labelControl4.TabIndex = 213;
            this.labelControl4.Text = "Stock :";
            // 
            // txtCantidadRequerida
            // 
            this.txtCantidadRequerida.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            327680});
            this.txtCantidadRequerida.Enabled = false;
            this.txtCantidadRequerida.Location = new System.Drawing.Point(104, 25);
            this.txtCantidadRequerida.Name = "txtCantidadRequerida";
            this.txtCantidadRequerida.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtCantidadRequerida.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCantidadRequerida.Properties.DisplayFormat.FormatString = "n5";
            this.txtCantidadRequerida.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCantidadRequerida.Properties.EditFormat.FormatString = "n5";
            this.txtCantidadRequerida.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCantidadRequerida.Properties.Mask.EditMask = "n5";
            this.txtCantidadRequerida.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCantidadRequerida.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCantidadRequerida.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCantidadRequerida.Size = new System.Drawing.Size(75, 20);
            this.txtCantidadRequerida.TabIndex = 212;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(85, 13);
            this.labelControl1.TabIndex = 211;
            this.labelControl1.Text = "Cantidad Pedida :";
            // 
            // txtTotalCantidadEntregada
            // 
            this.txtTotalCantidadEntregada.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            327680});
            this.txtTotalCantidadEntregada.Enabled = false;
            this.txtTotalCantidadEntregada.Location = new System.Drawing.Point(304, 25);
            this.txtTotalCantidadEntregada.Name = "txtTotalCantidadEntregada";
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtTotalCantidadEntregada.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtTotalCantidadEntregada.Properties.DisplayFormat.FormatString = "n5";
            this.txtTotalCantidadEntregada.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTotalCantidadEntregada.Properties.EditFormat.FormatString = "n5";
            this.txtTotalCantidadEntregada.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtTotalCantidadEntregada.Properties.Mask.EditMask = "n5";
            this.txtTotalCantidadEntregada.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTotalCantidadEntregada.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTotalCantidadEntregada.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTotalCantidadEntregada.Size = new System.Drawing.Size(80, 20);
            this.txtTotalCantidadEntregada.TabIndex = 210;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(186, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(103, 13);
            this.labelControl2.TabIndex = 209;
            this.labelControl2.Text = "Cantidad Entregada :";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Location = new System.Drawing.Point(3, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(549, 80);
            this.panelControl1.TabIndex = 41;
            // 
            // FrmManteEntegaMaterialPorFechaDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 105);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteEntegaMaterialPorFechaDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro Entega de Material Por Fecha Detalle";
            this.Load += new System.EventHandler(this.FrmNotaSalidaDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStock.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidadRequerida.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalCantidadEntregada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnsalir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem btnAgregar;
        public DevExpress.XtraBars.BarButtonItem btnModificar;
        private System.Windows.Forms.OpenFileDialog OpenFd;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        public DevExpress.XtraEditors.TextEdit txtTotalCantidadEntregada;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtCantidadRequerida;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtStock;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dteFecha;
        public DevExpress.XtraEditors.TextEdit txtCantidad;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}