namespace SGE.WindowForms.Otros.Almacen.Mantenimiento
{
    partial class FrmManteTipo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteTipo));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtabreviatura = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtdescripcion = new DevExpress.XtraEditors.TextEdit();
            this.txtcodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.BtnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.BtnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtabreviatura.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtabreviatura);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtdescripcion);
            this.groupControl1.Controls.Add(this.txtcodigo);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(394, 71);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Datos";
            // 
            // txtabreviatura
            // 
            this.txtabreviatura.Enabled = false;
            this.txtabreviatura.Location = new System.Drawing.Point(281, 25);
            this.txtabreviatura.Name = "txtabreviatura";
            this.txtabreviatura.Properties.Mask.ShowPlaceHolders = false;
            this.txtabreviatura.Size = new System.Drawing.Size(108, 20);
            this.txtabreviatura.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(214, 28);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Abreviatura :";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 50);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(61, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Descripcion :";
            // 
            // txtdescripcion
            // 
            this.txtdescripcion.Enabled = false;
            this.txtdescripcion.Location = new System.Drawing.Point(103, 47);
            this.txtdescripcion.Name = "txtdescripcion";
            this.txtdescripcion.Properties.MaxLength = 50;
            this.txtdescripcion.Size = new System.Drawing.Size(286, 20);
            this.txtdescripcion.TabIndex = 2;
            this.txtdescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cerrar_form);
            // 
            // txtcodigo
            // 
            this.txtcodigo.EditValue = "000";
            this.txtcodigo.Enabled = false;
            this.txtcodigo.Location = new System.Drawing.Point(103, 25);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Properties.DisplayFormat.FormatString = "d3";
            this.txtcodigo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtcodigo.Properties.EditFormat.FormatString = "d3";
            this.txtcodigo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtcodigo.Properties.Mask.EditMask = "d3";
            this.txtcodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtcodigo.Properties.Mask.ShowPlaceHolders = false;
            this.txtcodigo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtcodigo.Size = new System.Drawing.Size(82, 20);
            this.txtcodigo.TabIndex = 0;
            this.txtcodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cerrar_form);
            this.txtcodigo.Leave += new System.EventHandler(this.txtcodigo_Leave);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Codigo :";
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
            this.BtnGuardar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnGuardar.Appearance.Options.UseFont = true;
            this.BtnGuardar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.BtnGuardar.Caption = "Guardar";
            this.BtnGuardar.Glyph = ((System.Drawing.Image)(resources.GetObject("BtnGuardar.Glyph")));
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Text = "Grabar(Enter)";
            superToolTip1.Items.Add(toolTipTitleItem1);
            this.BtnGuardar.SuperTip = superToolTip1;
            this.BtnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGuardar_ItemClick);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.BtnCancelar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnCancelar.Appearance.Options.UseFont = true;
            this.BtnCancelar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.BtnCancelar.Caption = "Salir";
            this.BtnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.BtnCancelar.Id = 1;
            this.BtnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Text = "Salir(Esc)";
            superToolTip2.Items.Add(toolTipTitleItem2);
            this.BtnCancelar.SuperTip = superToolTip2;
            this.BtnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnCancelar_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(394, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 71);
            this.barDockControlBottom.Size = new System.Drawing.Size(394, 30);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 71);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(394, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 71);
            // 
            // FrmManteTipo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 101);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteTipo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManteMotonave_FormClosing);
            this.Load += new System.EventHandler(this.FrmManteMotonave_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtabreviatura.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtdescripcion;
        public DevExpress.XtraEditors.TextEdit txtcodigo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        private DevExpress.XtraBars.BarButtonItem BtnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraEditors.TextEdit txtabreviatura;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}