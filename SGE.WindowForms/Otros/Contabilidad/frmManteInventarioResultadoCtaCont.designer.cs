namespace SGE.WindowForms.Otros.Contabilidad
{
    partial class frmManteInventarioResultadoCtaCont
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
            this.btnAniadir = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bteCuenta = new DevExpress.XtraEditors.ButtonEdit();
            this.txtCuentaDes = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.btnAniadir,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAniadir),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnAniadir
            // 
            this.btnAniadir.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnAniadir.Caption = "Añadir";
            this.btnAniadir.Id = 0;
            this.btnAniadir.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAniadir.Name = "btnAniadir";
            this.btnAniadir.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAniadir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAniadir_ItemClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Caption = "Cancelar";
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
            this.barDockControlTop.Size = new System.Drawing.Size(497, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 75);
            this.barDockControlBottom.Size = new System.Drawing.Size(497, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 75);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(497, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 75);
            // 
            // bteCuenta
            // 
            this.bteCuenta.Location = new System.Drawing.Point(96, 35);
            this.bteCuenta.MenuManager = this.barManager1;
            this.bteCuenta.Name = "bteCuenta";
            this.bteCuenta.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bteCuenta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteCuenta.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.bteCuenta.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteCuenta.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.bteCuenta.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteCuenta.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.bteCuenta.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteCuenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCuenta.Size = new System.Drawing.Size(103, 20);
            this.bteCuenta.TabIndex = 0;
            this.bteCuenta.ToolTip = "Presione F10 para desplegar lista";
            this.bteCuenta.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCuenta_ButtonClick);
            this.bteCuenta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bteCuenta_KeyDown);
            this.bteCuenta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bteCuenta_KeyUp);
            // 
            // txtCuentaDes
            // 
            this.txtCuentaDes.Enabled = false;
            this.txtCuentaDes.Location = new System.Drawing.Point(217, 35);
            this.txtCuentaDes.Name = "txtCuentaDes";
            this.txtCuentaDes.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCuentaDes.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCuentaDes.Size = new System.Drawing.Size(212, 20);
            this.txtCuentaDes.TabIndex = 28;
            this.txtCuentaDes.TabStop = false;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(21, 38);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(62, 13);
            this.labelControl17.TabIndex = 29;
            this.labelControl17.Text = "Nro. Cuenta ";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl17);
            this.groupControl1.Controls.Add(this.bteCuenta);
            this.groupControl1.Controls.Add(this.txtCuentaDes);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(497, 75);
            this.groupControl1.TabIndex = 30;
            this.groupControl1.Text = "Datos";
            // 
            // frmMantePosFinanCtaCont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 100);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmMantePosFinanCtaCont";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SGI - Cuentas Contables";
            this.Load += new System.EventHandler(this.frmMantePosFinanCtaCont_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaDes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnAniadir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraEditors.ButtonEdit bteCuenta;
        public DevExpress.XtraEditors.TextEdit txtCuentaDes;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}