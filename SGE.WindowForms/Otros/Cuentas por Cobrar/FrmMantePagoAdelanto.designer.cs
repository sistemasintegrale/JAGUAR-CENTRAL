namespace SGE.WindowForms.Otros.Cuentas_por_Cobrar
{
    partial class FrmMantePagoAdelanto
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
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.BtnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.BtnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gcDatos = new DevExpress.XtraEditors.GroupControl();
            this.lblMoneda = new DevExpress.XtraEditors.LabelControl();
            this.bteDXCAdelanto = new DevExpress.XtraEditors.ButtonEdit();
            this.lblTipoDocumento = new DevExpress.XtraEditors.LabelControl();
            this.lblSaldo = new DevExpress.XtraEditors.LabelControl();
            this.lblDocumentoXCobrar = new DevExpress.XtraEditors.LabelControl();
            this.lblObservacion = new DevExpress.XtraEditors.LabelControl();
            this.txtMonto = new DevExpress.XtraEditors.TextEdit();
            this.lblMonto = new DevExpress.XtraEditors.LabelControl();
            this.txtObservacion = new DevExpress.XtraEditors.TextEdit();
            this.txtTipoCambio = new DevExpress.XtraEditors.TextEdit();
            this.lblTipoCambio = new DevExpress.XtraEditors.LabelControl();
            this.deFechaDocumento = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblDocumento = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatos)).BeginInit();
            this.gcDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteDXCAdelanto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoCambio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.BtnGuardar,
            this.BtnCancelar});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.BtnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.BtnGuardar.Caption = "Guardar";
            this.BtnGuardar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGuardar_ItemClick);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Caption = "Cancelar";
            this.BtnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.BtnCancelar.Id = 1;
            this.BtnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(704, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 112);
            this.barDockControlBottom.Size = new System.Drawing.Size(704, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 112);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(704, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 112);
            // 
            // gcDatos
            // 
            this.gcDatos.Controls.Add(this.lblMoneda);
            this.gcDatos.Controls.Add(this.bteDXCAdelanto);
            this.gcDatos.Controls.Add(this.lblTipoDocumento);
            this.gcDatos.Controls.Add(this.lblSaldo);
            this.gcDatos.Controls.Add(this.lblDocumentoXCobrar);
            this.gcDatos.Controls.Add(this.lblObservacion);
            this.gcDatos.Controls.Add(this.txtMonto);
            this.gcDatos.Controls.Add(this.lblMonto);
            this.gcDatos.Controls.Add(this.txtObservacion);
            this.gcDatos.Controls.Add(this.txtTipoCambio);
            this.gcDatos.Controls.Add(this.lblTipoCambio);
            this.gcDatos.Controls.Add(this.deFechaDocumento);
            this.gcDatos.Controls.Add(this.labelControl9);
            this.gcDatos.Controls.Add(this.labelControl2);
            this.gcDatos.Controls.Add(this.lblDocumento);
            this.gcDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDatos.Location = new System.Drawing.Point(0, 0);
            this.gcDatos.Name = "gcDatos";
            this.gcDatos.Size = new System.Drawing.Size(704, 112);
            this.gcDatos.TabIndex = 0;
            this.gcDatos.Text = "Pagos por Adelantos";
            // 
            // lblMoneda
            // 
            this.lblMoneda.Location = new System.Drawing.Point(124, 73);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(0, 13);
            this.lblMoneda.TabIndex = 0;
            this.lblMoneda.Tag = "";
            // 
            // bteDXCAdelanto
            // 
            this.bteDXCAdelanto.Location = new System.Drawing.Point(124, 44);
            this.bteDXCAdelanto.MenuManager = this.barManager1;
            this.bteDXCAdelanto.Name = "bteDXCAdelanto";
            this.bteDXCAdelanto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteDXCAdelanto.Properties.ReadOnly = true;
            this.bteDXCAdelanto.Size = new System.Drawing.Size(161, 20);
            this.bteDXCAdelanto.TabIndex = 1;
            this.bteDXCAdelanto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteDXCAdelanto_ButtonClick);
            // 
            // lblTipoDocumento
            // 
            this.lblTipoDocumento.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTipoDocumento.Location = new System.Drawing.Point(78, 47);
            this.lblTipoDocumento.Name = "lblTipoDocumento";
            this.lblTipoDocumento.Size = new System.Drawing.Size(25, 14);
            this.lblTipoDocumento.TabIndex = 0;
            this.lblTipoDocumento.Tag = "";
            this.lblTipoDocumento.Text = "ADE";
            // 
            // lblSaldo
            // 
            this.lblSaldo.Location = new System.Drawing.Point(341, 25);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(30, 13);
            this.lblSaldo.TabIndex = 0;
            this.lblSaldo.Text = "Saldo:";
            // 
            // lblDocumentoXCobrar
            // 
            this.lblDocumentoXCobrar.Location = new System.Drawing.Point(5, 28);
            this.lblDocumentoXCobrar.Name = "lblDocumentoXCobrar";
            this.lblDocumentoXCobrar.Size = new System.Drawing.Size(113, 13);
            this.lblDocumentoXCobrar.TabIndex = 0;
            this.lblDocumentoXCobrar.Text = "Documento por Cobrar:";
            // 
            // lblObservacion
            // 
            this.lblObservacion.Location = new System.Drawing.Point(5, 91);
            this.lblObservacion.Name = "lblObservacion";
            this.lblObservacion.Size = new System.Drawing.Size(75, 13);
            this.lblObservacion.TabIndex = 0;
            this.lblObservacion.Text = "Observaciones:";
            // 
            // txtMonto
            // 
            this.txtMonto.EditValue = "0.00";
            this.txtMonto.Location = new System.Drawing.Point(381, 66);
            this.txtMonto.MenuManager = this.barManager1;
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Properties.Mask.EditMask = "n2";
            this.txtMonto.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMonto.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMonto.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMonto.Size = new System.Drawing.Size(116, 20);
            this.txtMonto.TabIndex = 3;
            // 
            // lblMonto
            // 
            this.lblMonto.Location = new System.Drawing.Point(341, 69);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(34, 13);
            this.lblMonto.TabIndex = 0;
            this.lblMonto.Text = "Monto:";
            // 
            // txtObservacion
            // 
            this.txtObservacion.EditValue = "";
            this.txtObservacion.Location = new System.Drawing.Point(124, 88);
            this.txtObservacion.MenuManager = this.barManager1;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacion.Properties.MaxLength = 50;
            this.txtObservacion.Size = new System.Drawing.Size(372, 20);
            this.txtObservacion.TabIndex = 5;
            // 
            // txtTipoCambio
            // 
            this.txtTipoCambio.EditValue = "0.0000";
            this.txtTipoCambio.Location = new System.Drawing.Point(577, 66);
            this.txtTipoCambio.MenuManager = this.barManager1;
            this.txtTipoCambio.Name = "txtTipoCambio";
            this.txtTipoCambio.Properties.Mask.EditMask = "n4";
            this.txtTipoCambio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtTipoCambio.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtTipoCambio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTipoCambio.Size = new System.Drawing.Size(116, 20);
            this.txtTipoCambio.TabIndex = 4;
            // 
            // lblTipoCambio
            // 
            this.lblTipoCambio.Location = new System.Drawing.Point(537, 69);
            this.lblTipoCambio.Name = "lblTipoCambio";
            this.lblTipoCambio.Size = new System.Drawing.Size(21, 13);
            this.lblTipoCambio.TabIndex = 0;
            this.lblTipoCambio.Text = "T/C:";
            // 
            // deFechaDocumento
            // 
            this.deFechaDocumento.EditValue = null;
            this.deFechaDocumento.Location = new System.Drawing.Point(380, 44);
            this.deFechaDocumento.Name = "deFechaDocumento";
            this.deFechaDocumento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFechaDocumento.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deFechaDocumento.Size = new System.Drawing.Size(116, 20);
            this.deFechaDocumento.TabIndex = 2;
            this.deFechaDocumento.EditValueChanged += new System.EventHandler(this.deFechaDocumento_EditValueChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(5, 69);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(42, 13);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "Moneda:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(341, 45);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Fecha:";
            // 
            // lblDocumento
            // 
            this.lblDocumento.Location = new System.Drawing.Point(5, 47);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(58, 13);
            this.lblDocumento.TabIndex = 0;
            this.lblDocumento.Text = "Documento:";
            // 
            // FrmMantePagoAdelanto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 139);
            this.Controls.Add(this.gcDatos);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(720, 177);
            this.MinimumSize = new System.Drawing.Size(720, 177);
            this.Name = "FrmMantePagoAdelanto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Pago por Adelantos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMantePagoAdelanto_FormClosing);
            this.Load += new System.EventHandler(this.FrmMantePagoAdelanto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDatos)).EndInit();
            this.gcDatos.ResumeLayout(false);
            this.gcDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bteDXCAdelanto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTipoCambio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFechaDocumento.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem BtnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        private DevExpress.XtraEditors.GroupControl gcDatos;
        private DevExpress.XtraEditors.LabelControl lblDocumento;
        public DevExpress.XtraEditors.DateEdit deFechaDocumento;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblTipoCambio;
        public DevExpress.XtraEditors.TextEdit txtTipoCambio;
        private DevExpress.XtraEditors.LabelControl lblObservacion;
        public DevExpress.XtraEditors.TextEdit txtObservacion;
        public DevExpress.XtraEditors.TextEdit txtMonto;
        private DevExpress.XtraEditors.LabelControl lblMonto;
        private DevExpress.XtraEditors.LabelControl lblDocumentoXCobrar;
        private DevExpress.XtraEditors.LabelControl lblSaldo;
        private DevExpress.XtraEditors.LabelControl lblTipoDocumento;
        public DevExpress.XtraEditors.ButtonEdit bteDXCAdelanto;
        public DevExpress.XtraEditors.LabelControl lblMoneda;
    }
}