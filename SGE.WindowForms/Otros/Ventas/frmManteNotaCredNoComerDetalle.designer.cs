namespace SGE.WindowForms.Otros.Ventas
{
    partial class frmManteNotaCredNoComerDetalle
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
            this.txtCantidad = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnAceptar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.txtPUnitario = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtObservaciones = new DevExpress.XtraEditors.MemoEdit();
            this.txtMtoProductividad = new DevExpress.XtraEditors.TextEdit();
            this.txtProductividad = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtAreaPersonal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.btePersonal = new DevExpress.XtraEditors.ButtonEdit();
            this.txtMonto = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtItem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPUnitario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservaciones.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMtoProductividad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductividad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaPersonal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btePersonal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtCantidad);
            this.groupControl1.Controls.Add(this.labelControl15);
            this.groupControl1.Controls.Add(this.txtPUnitario);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtObservaciones);
            this.groupControl1.Controls.Add(this.txtMtoProductividad);
            this.groupControl1.Controls.Add(this.txtProductividad);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtAreaPersonal);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.btePersonal);
            this.groupControl1.Controls.Add(this.txtMonto);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtItem);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(480, 270);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos";
            this.groupControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.groupControl1_MouseMove);
            // 
            // txtCantidad
            // 
            this.txtCantidad.EditValue = "0";
            this.txtCantidad.Location = new System.Drawing.Point(150, 23);
            this.txtCantidad.MenuManager = this.barManager1;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCantidad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCantidad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCantidad.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCantidad.Properties.Mask.EditMask = "n4";
            this.txtCantidad.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCantidad.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCantidad.Size = new System.Drawing.Size(55, 20);
            this.txtCantidad.TabIndex = 111;
            this.txtCantidad.EditValueChanged += new System.EventHandler(this.txtCantidad_EditValueChanged_1);
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
            this.btnAceptar,
            this.btnCancelar});
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAceptar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnAceptar.Caption = "Aceptar";
            this.btnAceptar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.btnAceptar.Id = 0;
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAceptar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAceptar_ItemClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Caption = "Cancelar";
            this.btnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnCancelar.Id = 1;
            this.btnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(480, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 270);
            this.barDockControlBottom.Size = new System.Drawing.Size(480, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 270);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(480, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 270);
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(97, 25);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(50, 13);
            this.labelControl15.TabIndex = 112;
            this.labelControl15.Text = "Cantidad :";
            // 
            // txtPUnitario
            // 
            this.txtPUnitario.EditValue = "0.00";
            this.txtPUnitario.Location = new System.Drawing.Point(274, 23);
            this.txtPUnitario.Name = "txtPUnitario";
            this.txtPUnitario.Properties.DisplayFormat.FormatString = "n4";
            this.txtPUnitario.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPUnitario.Properties.Mask.EditMask = "n4";
            this.txtPUnitario.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPUnitario.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPUnitario.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPUnitario.Size = new System.Drawing.Size(65, 20);
            this.txtPUnitario.TabIndex = 47;
            this.txtPUnitario.EditValueChanged += new System.EventHandler(this.txtPUnitario_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(214, 26);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(54, 13);
            this.labelControl5.TabIndex = 46;
            this.labelControl5.Text = "P. Unitario:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(2, 47);
            this.txtObservaciones.MenuManager = this.barManager1;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtObservaciones.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtObservaciones.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservaciones.Size = new System.Drawing.Size(473, 218);
            this.txtObservaciones.TabIndex = 11;
            this.txtObservaciones.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtObservaciones_MouseMove);
            // 
            // txtMtoProductividad
            // 
            this.txtMtoProductividad.EditValue = "0";
            this.txtMtoProductividad.Enabled = false;
            this.txtMtoProductividad.Location = new System.Drawing.Point(397, 123);
            this.txtMtoProductividad.MenuManager = this.barManager1;
            this.txtMtoProductividad.Name = "txtMtoProductividad";
            this.txtMtoProductividad.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMtoProductividad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtMtoProductividad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtMtoProductividad.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtMtoProductividad.Properties.Mask.EditMask = "n2";
            this.txtMtoProductividad.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMtoProductividad.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMtoProductividad.Size = new System.Drawing.Size(55, 20);
            this.txtMtoProductividad.TabIndex = 45;
            this.txtMtoProductividad.Visible = false;
            // 
            // txtProductividad
            // 
            this.txtProductividad.EditValue = "0.00";
            this.txtProductividad.Enabled = false;
            this.txtProductividad.Location = new System.Drawing.Point(237, 123);
            this.txtProductividad.MenuManager = this.barManager1;
            this.txtProductividad.Name = "txtProductividad";
            this.txtProductividad.Properties.Appearance.Options.UseTextOptions = true;
            this.txtProductividad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtProductividad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtProductividad.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtProductividad.Properties.Mask.EditMask = "#0.00 %%;";
            this.txtProductividad.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtProductividad.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtProductividad.Properties.MaxLength = 5;
            this.txtProductividad.Size = new System.Drawing.Size(54, 20);
            this.txtProductividad.TabIndex = 44;
            this.txtProductividad.Visible = false;
            this.txtProductividad.EditValueChanged += new System.EventHandler(this.txtProductividad_EditValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(136, 126);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(97, 13);
            this.labelControl9.TabIndex = 43;
            this.labelControl9.Text = "Porc. Productividad:";
            this.labelControl9.Visible = false;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(300, 126);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(94, 13);
            this.labelControl8.TabIndex = 31;
            this.labelControl8.Text = "Mto. Productividad:";
            this.labelControl8.Visible = false;
            // 
            // txtAreaPersonal
            // 
            this.txtAreaPersonal.Enabled = false;
            this.txtAreaPersonal.Location = new System.Drawing.Point(297, 97);
            this.txtAreaPersonal.MenuManager = this.barManager1;
            this.txtAreaPersonal.Name = "txtAreaPersonal";
            this.txtAreaPersonal.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtAreaPersonal.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtAreaPersonal.Size = new System.Drawing.Size(155, 20);
            this.txtAreaPersonal.TabIndex = 6;
            this.txtAreaPersonal.Visible = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 100);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(97, 13);
            this.labelControl7.TabIndex = 28;
            this.labelControl7.Text = "Persona Encargada:";
            this.labelControl7.Visible = false;
            // 
            // btePersonal
            // 
            this.btePersonal.Location = new System.Drawing.Point(108, 97);
            this.btePersonal.MenuManager = this.barManager1;
            this.btePersonal.Name = "btePersonal";
            this.btePersonal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btePersonal.Properties.ReadOnly = true;
            this.btePersonal.Size = new System.Drawing.Size(183, 20);
            this.btePersonal.TabIndex = 5;
            this.btePersonal.Visible = false;
            this.btePersonal.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btePersonal_ButtonClick);
            this.btePersonal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btePersonal_KeyDown);
            // 
            // txtMonto
            // 
            this.txtMonto.EditValue = "0";
            this.txtMonto.Location = new System.Drawing.Point(413, 23);
            this.txtMonto.MenuManager = this.barManager1;
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMonto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtMonto.Properties.Mask.EditMask = "n4";
            this.txtMonto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMonto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMonto.Size = new System.Drawing.Size(55, 20);
            this.txtMonto.TabIndex = 9;
            this.txtMonto.EditValueChanged += new System.EventHandler(this.txtPrecio_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(352, 25);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(47, 13);
            this.labelControl6.TabIndex = 20;
            this.labelControl6.Text = "P. Monto:";
            // 
            // txtItem
            // 
            this.txtItem.Enabled = false;
            this.txtItem.Location = new System.Drawing.Point(37, 25);
            this.txtItem.MenuManager = this.barManager1;
            this.txtItem.Name = "txtItem";
            this.txtItem.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtItem.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtItem.Size = new System.Drawing.Size(54, 20);
            this.txtItem.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(26, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Item:";
            // 
            // frmManteNotaCredNoComerDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 297);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManteNotaCredNoComerDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle Nota de Credito No Comercial";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPUnitario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservaciones.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMtoProductividad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductividad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAreaPersonal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btePersonal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarButtonItem btnAceptar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraEditors.TextEdit txtMonto;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtItem;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ButtonEdit btePersonal;
        private DevExpress.XtraEditors.TextEdit txtAreaPersonal;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtMtoProductividad;
        private DevExpress.XtraEditors.TextEdit txtProductividad;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.MemoEdit txtObservaciones;
        public DevExpress.XtraEditors.TextEdit txtPUnitario;
        public DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtCantidad;
        private DevExpress.XtraEditors.LabelControl labelControl15;
    }
}