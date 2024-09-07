
namespace SGE.WindowForms.Otros.Ventas
{
    partial class FrmManteCuota
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
            this.txtMontoPagado = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.spinNro = new DevExpress.XtraEditors.SpinEdit();
            this.txtMonto = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lkpEstado = new DevExpress.XtraEditors.LookUpEdit();
            this.dtefecha = new DevExpress.XtraEditors.DateEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoPagado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNro.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtefecha.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtefecha.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtMontoPagado);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.spinNro);
            this.groupControl1.Controls.Add(this.txtMonto);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.lkpEstado);
            this.groupControl1.Controls.Add(this.dtefecha);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(267, 122);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Datos de Ingreso";
            // 
            // txtMontoPagado
            // 
            this.txtMontoPagado.EditValue = "";
            this.txtMontoPagado.Location = new System.Drawing.Point(357, 85);
            this.txtMontoPagado.Name = "txtMontoPagado";
            this.txtMontoPagado.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMontoPagado.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtMontoPagado.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtMontoPagado.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtMontoPagado.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMontoPagado.Properties.Mask.EditMask = "n2";
            this.txtMontoPagado.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMontoPagado.Properties.Mask.ShowPlaceHolders = false;
            this.txtMontoPagado.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMontoPagado.Properties.MaxLength = 30;
            this.txtMontoPagado.Size = new System.Drawing.Size(127, 20);
            this.txtMontoPagado.TabIndex = 171;
            this.txtMontoPagado.Visible = false;
            this.txtMontoPagado.EditValueChanged += new System.EventHandler(this.txtMontoPagado_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(256, 88);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(91, 13);
            this.labelControl5.TabIndex = 170;
            this.labelControl5.Text = "Monto de Pagado :";
            this.labelControl5.Visible = false;
            // 
            // spinNro
            // 
            this.spinNro.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinNro.Enabled = false;
            this.spinNro.Location = new System.Drawing.Point(48, 32);
            this.spinNro.Name = "spinNro";
            this.spinNro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.spinNro.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.spinNro.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinNro.Properties.IsFloatValue = false;
            this.spinNro.Properties.Mask.EditMask = "N00";
            this.spinNro.Size = new System.Drawing.Size(39, 20);
            this.spinNro.TabIndex = 169;
            // 
            // txtMonto
            // 
            this.txtMonto.EditValue = "";
            this.txtMonto.Location = new System.Drawing.Point(119, 58);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Properties.Appearance.Options.UseTextOptions = true;
            this.txtMonto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtMonto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtMonto.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtMonto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMonto.Properties.Mask.EditMask = "n2";
            this.txtMonto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMonto.Properties.Mask.ShowPlaceHolders = false;
            this.txtMonto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMonto.Properties.MaxLength = 30;
            this.txtMonto.Size = new System.Drawing.Size(127, 20);
            this.txtMonto.TabIndex = 60;
            this.txtMonto.EditValueChanged += new System.EventHandler(this.txtMonto_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(18, 35);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "Nro :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(104, 35);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Fecha :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 88);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(87, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Estado de Cuota :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(18, 61);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Monto de Cuota :";
            // 
            // lkpEstado
            // 
            this.lkpEstado.Enabled = false;
            this.lkpEstado.Location = new System.Drawing.Point(119, 85);
            this.lkpEstado.Name = "lkpEstado";
            this.lkpEstado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpEstado.Size = new System.Drawing.Size(127, 20);
            this.lkpEstado.TabIndex = 2;
            // 
            // dtefecha
            // 
            this.dtefecha.EditValue = null;
            this.dtefecha.Location = new System.Drawing.Point(146, 32);
            this.dtefecha.Name = "dtefecha";
            this.dtefecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtefecha.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtefecha.Size = new System.Drawing.Size(100, 20);
            this.dtefecha.TabIndex = 1;
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
            this.barCheckItem1,
            this.barButtonItem1});
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Barra de estado";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barCheckItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Barra de estado";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "Salir";
            this.barCheckItem1.Id = 0;
            this.barCheckItem1.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.barCheckItem1.Name = "barCheckItem1";
            this.barCheckItem1.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem1_CheckedChanged);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem1.Caption = "Guardar";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(267, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 122);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(267, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 122);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(267, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 122);
            // 
            // FrmManteCuota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 150);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteCuota";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento Cuotas";
            this.Load += new System.EventHandler(this.FrmManteCuota_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMontoPagado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtefecha.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtefecha.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lkpEstado;
        private DevExpress.XtraEditors.DateEdit dtefecha;
        public DevExpress.XtraEditors.TextEdit txtMonto;
        public DevExpress.XtraEditors.SpinEdit spinNro;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraEditors.TextEdit txtMontoPagado;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}