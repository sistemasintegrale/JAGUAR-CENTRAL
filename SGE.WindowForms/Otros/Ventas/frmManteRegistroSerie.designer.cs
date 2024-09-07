namespace SGE.WindowForms.Otros.Ventas
{
    partial class frmManteRegistroSerie
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
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.txtCorrelativo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtSerie = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lkpPuntoVent = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.lkpTipoDocumento = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoDocumento.Properties)).BeginInit();
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
            this.btnGuardar,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnGuardar.Caption = "Guardar";
            this.btnGuardar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.btnGuardar.Id = 0;
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(305, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 93);
            this.barDockControlBottom.Size = new System.Drawing.Size(305, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 93);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(305, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 93);
            // 
            // txtCorrelativo
            // 
            this.txtCorrelativo.EditValue = "";
            this.txtCorrelativo.Location = new System.Drawing.Point(207, 12);
            this.txtCorrelativo.Name = "txtCorrelativo";
            this.txtCorrelativo.Properties.LookAndFeel.SkinName = "Blue";
            this.txtCorrelativo.Properties.Mask.EditMask = "00000000";
            this.txtCorrelativo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCorrelativo.Properties.Mask.ShowPlaceHolders = false;
            this.txtCorrelativo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCorrelativo.Properties.MaxLength = 8;
            this.txtCorrelativo.Size = new System.Drawing.Size(77, 20);
            this.txtCorrelativo.TabIndex = 31;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(8, 15);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(28, 13);
            this.labelControl9.TabIndex = 28;
            this.labelControl9.Text = "Serie:";
            // 
            // txtSerie
            // 
            this.txtSerie.EditValue = "";
            this.txtSerie.Location = new System.Drawing.Point(42, 12);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Properties.LookAndFeel.SkinName = "Blue";
            this.txtSerie.Properties.Mask.ShowPlaceHolders = false;
            this.txtSerie.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtSerie.Properties.MaxLength = 4;
            this.txtSerie.Size = new System.Drawing.Size(40, 20);
            this.txtSerie.TabIndex = 30;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(143, 15);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(57, 13);
            this.labelControl8.TabIndex = 29;
            this.labelControl8.Text = "Correlativo:";
            // 
            // lkpPuntoVent
            // 
            this.lkpPuntoVent.Location = new System.Drawing.Point(98, 37);
            this.lkpPuntoVent.Name = "lkpPuntoVent";
            this.lkpPuntoVent.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpPuntoVent.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpPuntoVent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpPuntoVent.Properties.NullText = "";
            this.lkpPuntoVent.Size = new System.Drawing.Size(175, 20);
            this.lkpPuntoVent.TabIndex = 145;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(10, 39);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(81, 13);
            this.labelControl15.TabIndex = 144;
            this.labelControl15.Text = "Punto de  Venta:";
            // 
            // lkpTipoDocumento
            // 
            this.lkpTipoDocumento.Location = new System.Drawing.Point(98, 63);
            this.lkpTipoDocumento.Name = "lkpTipoDocumento";
            this.lkpTipoDocumento.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpTipoDocumento.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpTipoDocumento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoDocumento.Properties.NullText = "";
            this.lkpTipoDocumento.Size = new System.Drawing.Size(175, 20);
            this.lkpTipoDocumento.TabIndex = 151;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 65);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 13);
            this.labelControl1.TabIndex = 150;
            this.labelControl1.Text = "Tipo Documento:";
            // 
            // frmManteRegistroSerie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 120);
            this.Controls.Add(this.lkpTipoDocumento);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lkpPuntoVent);
            this.Controls.Add(this.labelControl15);
            this.Controls.Add(this.txtCorrelativo);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximumSize = new System.Drawing.Size(615, 300);
            this.Name = "frmManteRegistroSerie";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Registro de Serie";
            this.Load += new System.EventHandler(this.frmMantePersonal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCorrelativo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSerie.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoDocumento.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraEditors.TextEdit txtCorrelativo;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        public DevExpress.XtraEditors.TextEdit txtSerie;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit lkpPuntoVent;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LookUpEdit lkpTipoDocumento;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}