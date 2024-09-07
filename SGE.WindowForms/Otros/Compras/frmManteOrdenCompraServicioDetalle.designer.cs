﻿namespace SGE.WindowForms.Otros.Compras
{
    partial class frmManteOrdenCompraServicioDetalle
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
            this.lkpUnidadMedida = new DevExpress.XtraEditors.LookUpEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnAgregar = new DevExpress.XtraBars.BarButtonItem();
            this.btnModificar = new DevExpress.XtraBars.BarButtonItem();
            this.btnSalir = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnVerDoc = new System.Windows.Forms.Button();
            this.txtDescripcionOCDetalle = new DevExpress.XtraEditors.TextEdit();
            this.txtPVenta = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescuento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.dtmFechaEntrega = new DevExpress.XtraEditors.DateEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodFabricante = new DevExpress.XtraEditors.TextEdit();
            this.txtPUnitario = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtCantidad = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtUM = new DevExpress.XtraEditors.TextEdit();
            this.txtItem = new DevExpress.XtraEditors.TextEdit();
            this.labelControl32 = new DevExpress.XtraEditors.LabelControl();
            this.txtCaracteristica = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUnidadMedida.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionOCDetalle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPVenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescuento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFechaEntrega.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFechaEntrega.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodFabricante.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPUnitario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaracteristica.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkpUnidadMedida);
            this.groupControl1.Controls.Add(this.labelControl15);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnVerDoc);
            this.groupControl1.Controls.Add(this.txtDescripcionOCDetalle);
            this.groupControl1.Controls.Add(this.txtPVenta);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtDescuento);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.dtmFechaEntrega);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtCodFabricante);
            this.groupControl1.Controls.Add(this.txtPUnitario);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtCantidad);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtUM);
            this.groupControl1.Controls.Add(this.txtItem);
            this.groupControl1.Controls.Add(this.labelControl32);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(611, 132);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Datos";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // lkpUnidadMedida
            // 
            this.lkpUnidadMedida.Location = new System.Drawing.Point(411, 76);
            this.lkpUnidadMedida.MenuManager = this.barManager1;
            this.lkpUnidadMedida.Name = "lkpUnidadMedida";
            this.lkpUnidadMedida.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpUnidadMedida.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpUnidadMedida.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpUnidadMedida.Properties.NullText = "";
            this.lkpUnidadMedida.Size = new System.Drawing.Size(152, 20);
            this.lkpUnidadMedida.TabIndex = 79;
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
            this.btnSalir});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnModificar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSalir)});
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
            // btnModificar
            // 
            this.btnModificar.Caption = "Modificar";
            this.btnModificar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.btnModificar.Id = 1;
            this.btnModificar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnModificar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModificar_ItemClick);
            // 
            // btnSalir
            // 
            this.btnSalir.Caption = "Salir";
            this.btnSalir.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnSalir.Id = 2;
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
            this.barDockControlTop.Size = new System.Drawing.Size(611, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 212);
            this.barDockControlBottom.Size = new System.Drawing.Size(611, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 212);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(611, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 212);
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(350, 79);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(55, 13);
            this.labelControl15.TabIndex = 80;
            this.labelControl15.Text = "U. Medida :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 53);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 13);
            this.labelControl1.TabIndex = 78;
            this.labelControl1.Text = "Descripcion General:";
            // 
            // btnVerDoc
            // 
            this.btnVerDoc.Location = new System.Drawing.Point(477, 102);
            this.btnVerDoc.Name = "btnVerDoc";
            this.btnVerDoc.Size = new System.Drawing.Size(129, 23);
            this.btnVerDoc.TabIndex = 77;
            this.btnVerDoc.Text = "Ver Documentos";
            this.btnVerDoc.UseVisualStyleBackColor = true;
            this.btnVerDoc.Click += new System.EventHandler(this.btnVerDoc_Click);
            // 
            // txtDescripcionOCDetalle
            // 
            this.txtDescripcionOCDetalle.Location = new System.Drawing.Point(115, 50);
            this.txtDescripcionOCDetalle.Name = "txtDescripcionOCDetalle";
            this.txtDescripcionOCDetalle.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcionOCDetalle.Properties.Appearance.Options.UseFont = true;
            this.txtDescripcionOCDetalle.Size = new System.Drawing.Size(393, 20);
            this.txtDescripcionOCDetalle.TabIndex = 76;
            // 
            // txtPVenta
            // 
            this.txtPVenta.EditValue = "0.00";
            this.txtPVenta.Location = new System.Drawing.Point(385, 99);
            this.txtPVenta.Name = "txtPVenta";
            this.txtPVenta.Properties.DisplayFormat.FormatString = "n2";
            this.txtPVenta.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPVenta.Properties.Mask.EditMask = "n2";
            this.txtPVenta.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPVenta.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPVenta.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPVenta.Size = new System.Drawing.Size(65, 20);
            this.txtPVenta.TabIndex = 59;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(341, 102);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(38, 13);
            this.labelControl8.TabIndex = 58;
            this.labelControl8.Text = "V.Total:";
            // 
            // txtDescuento
            // 
            this.txtDescuento.EditValue = "0.00";
            this.txtDescuento.Location = new System.Drawing.Point(251, 99);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Properties.DisplayFormat.FormatString = "n2";
            this.txtDescuento.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtDescuento.Properties.Mask.EditMask = "n2";
            this.txtDescuento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtDescuento.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDescuento.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDescuento.Size = new System.Drawing.Size(65, 20);
            this.txtDescuento.TabIndex = 57;
            this.txtDescuento.EditValueChanged += new System.EventHandler(this.txtDescuento_EditValueChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(176, 102);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(69, 13);
            this.labelControl7.TabIndex = 56;
            this.labelControl7.Text = "% Descuento:";
            // 
            // dtmFechaEntrega
            // 
            this.dtmFechaEntrega.EditValue = null;
            this.dtmFechaEntrega.Location = new System.Drawing.Point(84, 76);
            this.dtmFechaEntrega.Name = "dtmFechaEntrega";
            this.dtmFechaEntrega.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtmFechaEntrega.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtmFechaEntrega.Size = new System.Drawing.Size(111, 20);
            this.dtmFechaEntrega.TabIndex = 55;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(20, 79);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 13);
            this.labelControl4.TabIndex = 23;
            this.labelControl4.Text = "F.Entrega:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(307, 25);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(89, 13);
            this.labelControl3.TabIndex = 22;
            this.labelControl3.Text = "Cod. Fabricantes: ";
            // 
            // txtCodFabricante
            // 
            this.txtCodFabricante.Location = new System.Drawing.Point(402, 22);
            this.txtCodFabricante.Name = "txtCodFabricante";
            this.txtCodFabricante.Properties.MaxLength = 50;
            this.txtCodFabricante.Properties.ReadOnly = true;
            this.txtCodFabricante.Size = new System.Drawing.Size(197, 20);
            this.txtCodFabricante.TabIndex = 21;
            // 
            // txtPUnitario
            // 
            this.txtPUnitario.EditValue = "0.00";
            this.txtPUnitario.Location = new System.Drawing.Point(84, 99);
            this.txtPUnitario.Name = "txtPUnitario";
            this.txtPUnitario.Properties.DisplayFormat.FormatString = "n2";
            this.txtPUnitario.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtPUnitario.Properties.Mask.EditMask = "n2";
            this.txtPUnitario.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtPUnitario.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPUnitario.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPUnitario.Size = new System.Drawing.Size(65, 20);
            this.txtPUnitario.TabIndex = 19;
            this.txtPUnitario.EditValueChanged += new System.EventHandler(this.txtPUnitario_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 102);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(54, 13);
            this.labelControl5.TabIndex = 18;
            this.labelControl5.Text = "V. Unitario:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.EditValue = "0.00";
            this.txtCantidad.Location = new System.Drawing.Point(251, 76);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Properties.DisplayFormat.FormatString = "n2";
            this.txtCantidad.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtCantidad.Properties.Mask.EditMask = "n2";
            this.txtCantidad.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCantidad.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCantidad.Size = new System.Drawing.Size(65, 20);
            this.txtCantidad.TabIndex = 12;
            this.txtCantidad.EditValueChanged += new System.EventHandler(this.txtCantidad_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(201, 81);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(47, 13);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Cantidad:";
            // 
            // txtUM
            // 
            this.txtUM.Location = new System.Drawing.Point(176, 25);
            this.txtUM.Name = "txtUM";
            this.txtUM.Properties.MaxLength = 50;
            this.txtUM.Properties.ReadOnly = true;
            this.txtUM.Size = new System.Drawing.Size(98, 20);
            this.txtUM.TabIndex = 10;
            this.txtUM.Visible = false;
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(69, 24);
            this.txtItem.Name = "txtItem";
            this.txtItem.Properties.MaxLength = 50;
            this.txtItem.Properties.ReadOnly = true;
            this.txtItem.Size = new System.Drawing.Size(50, 20);
            this.txtItem.TabIndex = 1;
            // 
            // labelControl32
            // 
            this.labelControl32.Location = new System.Drawing.Point(19, 26);
            this.labelControl32.Name = "labelControl32";
            this.labelControl32.Size = new System.Drawing.Size(26, 13);
            this.labelControl32.TabIndex = 0;
            this.labelControl32.Text = "Item:";
            // 
            // txtCaracteristica
            // 
            this.txtCaracteristica.Location = new System.Drawing.Point(97, 138);
            this.txtCaracteristica.MenuManager = this.barManager1;
            this.txtCaracteristica.Name = "txtCaracteristica";
            this.txtCaracteristica.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCaracteristica.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCaracteristica.Properties.MaxLength = 200;
            this.txtCaracteristica.Size = new System.Drawing.Size(502, 70);
            this.txtCaracteristica.TabIndex = 16;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(14, 141);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(58, 13);
            this.labelControl9.TabIndex = 19;
            this.labelControl9.Text = "Descripcion:";
            // 
            // frmManteOrdenCompraServicioDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 239);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.txtCaracteristica);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManteOrdenCompraServicioDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento Orden de Compra Servicio - Productos";
            this.Load += new System.EventHandler(this.frmManteOrdenCompraDetalleDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUnidadMedida.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionOCDetalle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPVenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescuento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFechaEntrega.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmFechaEntrega.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodFabricante.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPUnitario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCaracteristica.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.TextEdit txtCantidad;
        public DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtUM;
        public DevExpress.XtraEditors.TextEdit txtItem;
        private DevExpress.XtraEditors.LabelControl labelControl32;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnSalir;
        public DevExpress.XtraBars.BarButtonItem btnAgregar;
        public DevExpress.XtraBars.BarButtonItem btnModificar;
        public DevExpress.XtraEditors.TextEdit txtPUnitario;
        public DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtCodFabricante;
        public DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.DateEdit dtmFechaEntrega;
        public DevExpress.XtraEditors.TextEdit txtPVenta;
        public DevExpress.XtraEditors.LabelControl labelControl8;
        public DevExpress.XtraEditors.TextEdit txtDescuento;
        public DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.MemoEdit txtCaracteristica;
        public DevExpress.XtraEditors.LabelControl labelControl9;
        public DevExpress.XtraEditors.TextEdit txtDescripcionOCDetalle;
        private System.Windows.Forms.Button btnVerDoc;
        public DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lkpUnidadMedida;
        private DevExpress.XtraEditors.LabelControl labelControl15;
    }
}