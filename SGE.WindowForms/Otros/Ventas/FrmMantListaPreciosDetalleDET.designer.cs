namespace SGE.WindowForms.Otros.Ventas
{
    partial class FrmMantListaPreciosDetalleDET
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
            this.txtRem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lkpserie1 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lkpserie2 = new DevExpress.XtraEditors.LookUpEdit();
            this.txtMinorista = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtMayorista = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtdescripcion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnSalir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpserie1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpserie2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinorista.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMayorista.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtRem);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lkpserie1);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.lkpserie2);
            this.groupControl1.Controls.Add(this.txtMinorista);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtMayorista);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtdescripcion);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(616, 73);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Datos";
            // 
            // txtRem
            // 
            this.txtRem.EditValue = "0.00";
            this.txtRem.Location = new System.Drawing.Point(366, 48);
            this.txtRem.Name = "txtRem";
            this.txtRem.Properties.DisplayFormat.FormatString = "n2";
            this.txtRem.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtRem.Properties.EditFormat.FormatString = "n2";
            this.txtRem.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtRem.Properties.Mask.EditMask = "n2";
            this.txtRem.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRem.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtRem.Size = new System.Drawing.Size(50, 20);
            this.txtRem.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(301, 51);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(56, 13);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "Rem/Otros:";
            // 
            // lkpserie1
            // 
            this.lkpserie1.Location = new System.Drawing.Point(455, 28);
            this.lkpserie1.Name = "lkpserie1";
            this.lkpserie1.Properties.Appearance.BackColor = System.Drawing.Color.Silver;
            this.lkpserie1.Properties.Appearance.Options.UseBackColor = true;
            this.lkpserie1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.lkpserie1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpserie1.Properties.NullText = "";
            this.lkpserie1.Size = new System.Drawing.Size(50, 22);
            this.lkpserie1.TabIndex = 1;
            this.lkpserie1.EditValueChanged += new System.EventHandler(this.lkpserie1_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(516, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(7, 14);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "\\";
            // 
            // lkpserie2
            // 
            this.lkpserie2.Location = new System.Drawing.Point(535, 28);
            this.lkpserie2.Name = "lkpserie2";
            this.lkpserie2.Properties.Appearance.BackColor = System.Drawing.Color.Silver;
            this.lkpserie2.Properties.Appearance.Options.UseBackColor = true;
            this.lkpserie2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.lkpserie2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpserie2.Properties.NullText = "";
            this.lkpserie2.Size = new System.Drawing.Size(50, 22);
            this.lkpserie2.TabIndex = 2;
            this.lkpserie2.EditValueChanged += new System.EventHandler(this.lkpserie2_EditValueChanged);
            // 
            // txtMinorista
            // 
            this.txtMinorista.EditValue = "0.00";
            this.txtMinorista.Location = new System.Drawing.Point(100, 48);
            this.txtMinorista.Name = "txtMinorista";
            this.txtMinorista.Properties.DisplayFormat.FormatString = "n2";
            this.txtMinorista.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMinorista.Properties.EditFormat.FormatString = "n2";
            this.txtMinorista.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMinorista.Properties.Mask.EditMask = "n2";
            this.txtMinorista.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMinorista.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMinorista.Size = new System.Drawing.Size(50, 20);
            this.txtMinorista.TabIndex = 3;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 51);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 13);
            this.labelControl6.TabIndex = 4;
            this.labelControl6.Text = " P.Minorista:";
            // 
            // txtMayorista
            // 
            this.txtMayorista.EditValue = "0.00";
            this.txtMayorista.Location = new System.Drawing.Point(236, 48);
            this.txtMayorista.Name = "txtMayorista";
            this.txtMayorista.Properties.DisplayFormat.FormatString = "n2";
            this.txtMayorista.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMayorista.Properties.EditFormat.FormatString = "n2";
            this.txtMayorista.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtMayorista.Properties.Mask.EditMask = "n2";
            this.txtMayorista.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtMayorista.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtMayorista.Size = new System.Drawing.Size(50, 20);
            this.txtMayorista.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(161, 51);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(64, 13);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "P. Mayorista:";
            // 
            // txtdescripcion
            // 
            this.txtdescripcion.Location = new System.Drawing.Point(100, 27);
            this.txtdescripcion.Name = "txtdescripcion";
            this.txtdescripcion.Size = new System.Drawing.Size(315, 20);
            this.txtdescripcion.TabIndex = 0;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(421, 30);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Serie:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Descripción:";
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
            this.btnSalir,
            this.btnModificar});
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
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGuardar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnModificar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSalir)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnGuardar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnGuardar.Caption = "Guardar";
            //this.btnGuardar.Glyph = global::PVT.WindowsForm.Properties.Resources.guardar_16x16;
            this.btnGuardar.Id = 0;
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // btnModificar
            // 
            this.btnModificar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnModificar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnModificar.Caption = "Modificar";
            //this.btnModificar.Glyph = global::PVT.WindowsForm.Properties.Resources.doc_edit;
            this.btnModificar.Id = 2;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnModificar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModificar_ItemClick);
            // 
            // btnSalir
            // 
            this.btnSalir.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnSalir.Caption = "Salir";
            //this.btnSalir.Glyph = global::PVT.WindowsForm.Properties.Resources.Shutdown;
            this.btnSalir.Id = 1;
            this.btnSalir.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSalir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSalir_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(616, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 73);
            this.barDockControlBottom.Size = new System.Drawing.Size(616, 31);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 73);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(616, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 73);
            // 
            // FrmMantListaPreciosDetalleDET
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 104);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmMantListaPreciosDetalleDET";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista Precios Detalle";
            this.Load += new System.EventHandler(this.FrmMantListaPreciosDetalleDET_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpserie1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpserie2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinorista.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMayorista.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.LookUpEdit lkpserie1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.LookUpEdit lkpserie2;
        public DevExpress.XtraEditors.TextEdit txtMinorista;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtMayorista;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtdescripcion;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnSalir;
        public DevExpress.XtraBars.BarButtonItem btnGuardar;
        public DevExpress.XtraEditors.TextEdit txtRem;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraBars.BarButtonItem btnModificar;
    }
}