namespace SGE.WindowForms.Otros.Contabilidad
{
    partial class frmRangoCuentas
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
            this.txtCuentaF = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnImprimir = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.txtCuentaI = new DevExpress.XtraEditors.TextEdit();
            this.bteCuentaF = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.bteCuentaI = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaF.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuentaF.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuentaI.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtCuentaF);
            this.groupControl1.Controls.Add(this.txtCuentaI);
            this.groupControl1.Controls.Add(this.bteCuentaF);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.bteCuentaI);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(418, 92);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Rango de Cuentas";
            // 
            // txtCuentaF
            // 
            this.txtCuentaF.Enabled = false;
            this.txtCuentaF.Location = new System.Drawing.Point(159, 55);
            this.txtCuentaF.MenuManager = this.barManager1;
            this.txtCuentaF.Name = "txtCuentaF";
            this.txtCuentaF.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.txtCuentaF.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCuentaF.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtCuentaF.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCuentaF.Size = new System.Drawing.Size(254, 20);
            this.txtCuentaF.TabIndex = 7;
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
            this.btnImprimir,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImprimir),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnImprimir.Caption = "Imprimir";
            this.btnImprimir.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.btnImprimir.Id = 0;
            this.btnImprimir.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnImprimir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImprimir_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(418, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 92);
            this.barDockControlBottom.Size = new System.Drawing.Size(418, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 92);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(418, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 92);
            // 
            // txtCuentaI
            // 
            this.txtCuentaI.Enabled = false;
            this.txtCuentaI.Location = new System.Drawing.Point(159, 32);
            this.txtCuentaI.MenuManager = this.barManager1;
            this.txtCuentaI.Name = "txtCuentaI";
            this.txtCuentaI.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.txtCuentaI.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCuentaI.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtCuentaI.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCuentaI.Size = new System.Drawing.Size(254, 20);
            this.txtCuentaI.TabIndex = 6;
            // 
            // bteCuentaF
            // 
            this.bteCuentaF.Location = new System.Drawing.Point(58, 55);
            this.bteCuentaF.Name = "bteCuentaF";
            this.bteCuentaF.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.bteCuentaF.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteCuentaF.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.bteCuentaF.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteCuentaF.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCuentaF.Properties.ReadOnly = true;
            this.bteCuentaF.Size = new System.Drawing.Size(98, 20);
            this.bteCuentaF.TabIndex = 5;
            this.bteCuentaF.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCuentaF_ButtonClick);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(48, 58);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(4, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = ":";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(48, 35);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(4, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = ":";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Hasta";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Desde";
            // 
            // bteCuentaI
            // 
            this.bteCuentaI.Location = new System.Drawing.Point(58, 32);
            this.bteCuentaI.Name = "bteCuentaI";
            this.bteCuentaI.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.bteCuentaI.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteCuentaI.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.bteCuentaI.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteCuentaI.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCuentaI.Properties.ReadOnly = true;
            this.bteCuentaI.Size = new System.Drawing.Size(98, 20);
            this.bteCuentaI.TabIndex = 0;
            this.bteCuentaI.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCuentaI_ButtonClick);
            // 
            // FrmImpresionCuentaContable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 120);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmImpresionCuentaContable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccionar Rango de Cuentas";
            this.Load += new System.EventHandler(this.FrmImpresionCuentaContable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaF.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuentaF.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuentaI.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ButtonEdit bteCuentaI;
        private DevExpress.XtraEditors.TextEdit txtCuentaF;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnImprimir;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.TextEdit txtCuentaI;
        private DevExpress.XtraEditors.ButtonEdit bteCuentaF;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}