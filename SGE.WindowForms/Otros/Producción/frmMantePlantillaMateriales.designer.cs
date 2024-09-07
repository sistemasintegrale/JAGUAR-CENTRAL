namespace SGE.WindowForms.Otros.Producción
{
    partial class frmMantePlantillaMateriales
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
            this.btnAgregar = new DevExpress.XtraBars.BarButtonItem();
            this.btnsalir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lkpUbicacion = new DevExpress.XtraEditors.LookUpEdit();
            this.txtUM = new DevExpress.XtraEditors.TextEdit();
            this.txtDescProd = new DevExpress.XtraEditors.TextEdit();
            this.bteProducto = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescripcion = new DevExpress.XtraEditors.MemoEdit();
            this.txtCantidad = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lkpArea = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtitem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUbicacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescProd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtitem.Properties)).BeginInit();
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
            this.btnAgregar,
            this.btnModificar,
            this.btnsalir});
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
            this.btnAgregar.Caption = "Guardar";
            this.btnAgregar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.btnAgregar.Id = 0;
            this.btnAgregar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAgregar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAgregar_ItemClick);
            // 
            // btnsalir
            // 
            this.btnsalir.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
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
            this.barDockControlTop.Size = new System.Drawing.Size(485, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 89);
            this.barDockControlBottom.Size = new System.Drawing.Size(485, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 89);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(485, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 89);
            // 
            // btnModificar
            // 
            this.btnModificar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnModificar.Caption = "Modificar";
            this.btnModificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.Id = 1;
            this.btnModificar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lkpUbicacion);
            this.panelControl1.Controls.Add(this.lkpArea);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.labelControl21);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtitem);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(482, 84);
            this.panelControl1.TabIndex = 46;
            // 
            // lkpUbicacion
            // 
            this.lkpUbicacion.Location = new System.Drawing.Point(313, 41);
            this.lkpUbicacion.Name = "lkpUbicacion";
            this.lkpUbicacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpUbicacion.Properties.NullText = "";
            this.lkpUbicacion.Size = new System.Drawing.Size(157, 20);
            this.lkpUbicacion.TabIndex = 191;
            this.lkpUbicacion.EditValueChanged += new System.EventHandler(this.lkpUbicacion_EditValueChanged);
            // 
            // txtUM
            // 
            this.txtUM.Enabled = false;
            this.txtUM.Location = new System.Drawing.Point(78, 307);
            this.txtUM.Name = "txtUM";
            this.txtUM.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtUM.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtUM.Properties.MaxLength = 50;
            this.txtUM.Properties.ReadOnly = true;
            this.txtUM.Size = new System.Drawing.Size(100, 20);
            this.txtUM.TabIndex = 190;
            this.txtUM.Visible = false;
            // 
            // txtDescProd
            // 
            this.txtDescProd.Enabled = false;
            this.txtDescProd.Location = new System.Drawing.Point(208, 284);
            this.txtDescProd.Name = "txtDescProd";
            this.txtDescProd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescProd.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescProd.Properties.MaxLength = 50;
            this.txtDescProd.Properties.ReadOnly = true;
            this.txtDescProd.Size = new System.Drawing.Size(299, 20);
            this.txtDescProd.TabIndex = 188;
            this.txtDescProd.Visible = false;
            // 
            // bteProducto
            // 
            this.bteProducto.Location = new System.Drawing.Point(79, 284);
            this.bteProducto.Name = "bteProducto";
            this.bteProducto.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteProducto.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteProducto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteProducto.Properties.Mask.ShowPlaceHolders = false;
            this.bteProducto.Properties.ReadOnly = true;
            this.bteProducto.Size = new System.Drawing.Size(111, 20);
            this.bteProducto.TabIndex = 187;
            this.bteProducto.Visible = false;
            this.bteProducto.Click += new System.EventHandler(this.bteProducto_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 287);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(47, 13);
            this.labelControl3.TabIndex = 189;
            this.labelControl3.Text = "Producto:";
            this.labelControl3.Visible = false;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(78, 212);
            this.txtDescripcion.MenuManager = this.barManager1;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescripcion.Size = new System.Drawing.Size(424, 68);
            this.txtDescripcion.TabIndex = 186;
            this.txtDescripcion.Visible = false;
            // 
            // txtCantidad
            // 
            this.txtCantidad.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            327680});
            this.txtCantidad.Location = new System.Drawing.Point(334, 307);
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
            this.txtCantidad.Size = new System.Drawing.Size(128, 20);
            this.txtCantidad.TabIndex = 185;
            this.txtCantidad.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(278, 310);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 184;
            this.labelControl1.Text = "Cantidad :";
            this.labelControl1.Visible = false;
            // 
            // lkpArea
            // 
            this.lkpArea.Location = new System.Drawing.Point(78, 41);
            this.lkpArea.Name = "lkpArea";
            this.lkpArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpArea.Properties.NullText = "";
            this.lkpArea.Size = new System.Drawing.Size(157, 20);
            this.lkpArea.TabIndex = 183;
            this.lkpArea.EditValueChanged += new System.EventHandler(this.lkpArea_EditValueChanged_1);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(12, 44);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(30, 13);
            this.labelControl5.TabIndex = 180;
            this.labelControl5.Text = "Area :";
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(12, 214);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(61, 13);
            this.labelControl20.TabIndex = 177;
            this.labelControl20.Text = "Descripción :";
            this.labelControl20.Visible = false;
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(255, 44);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(52, 13);
            this.labelControl21.TabIndex = 176;
            this.labelControl21.Text = "Ubicación :";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 25);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(37, 13);
            this.labelControl2.TabIndex = 173;
            this.labelControl2.Text = "Orden :";
            // 
            // txtitem
            // 
            this.txtitem.Location = new System.Drawing.Point(79, 18);
            this.txtitem.Name = "txtitem";
            this.txtitem.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtitem.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtitem.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtitem.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtitem.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.txtitem.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtitem.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtitem.Size = new System.Drawing.Size(60, 20);
            this.txtitem.TabIndex = 172;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(12, 306);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(41, 13);
            this.labelControl14.TabIndex = 174;
            this.labelControl14.Text = "Medida :";
            this.labelControl14.Visible = false;
            // 
            // frmMantePlantillaMateriales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 116);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.txtUM);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.labelControl20);
            this.Controls.Add(this.txtDescProd);
            this.Controls.Add(this.labelControl14);
            this.Controls.Add(this.bteProducto);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmMantePlantillaMateriales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro Plantilla de Materiales";
            this.Load += new System.EventHandler(this.FrmNotaSalidaDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUbicacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescProd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtitem.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnsalir;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarButtonItem btnAgregar;
        public DevExpress.XtraBars.BarButtonItem btnModificar;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        public DevExpress.XtraEditors.TextEdit txtCantidad;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.LookUpEdit lkpArea;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtitem;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        public DevExpress.XtraEditors.TextEdit txtDescProd;
        public DevExpress.XtraEditors.ButtonEdit bteProducto;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtUM;
        public DevExpress.XtraEditors.MemoEdit txtDescripcion;
        public DevExpress.XtraEditors.LookUpEdit lkpUbicacion;
    }
}