namespace SGE.WindowForms.Otros.Operaciones
{
    partial class frmManteHojaCostosRubros
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
            this.txtOrden = new DevExpress.XtraEditors.TextEdit();
            this.txtDescripcion = new DevExpress.XtraEditors.MemoEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.txtCTotal = new DevExpress.XtraEditors.TextEdit();
            this.lkpTipo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnProducto = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.lkpUnidadMedida = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.lkpMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.txtCantidad = new DevExpress.XtraEditors.TextEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.txtCUnitario = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrden.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProducto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUnidadMedida.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCUnitario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtOrden);
            this.groupControl1.Controls.Add(this.txtDescripcion);
            this.groupControl1.Controls.Add(this.txtCTotal);
            this.groupControl1.Controls.Add(this.lkpTipo);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.btnProducto);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl21);
            this.groupControl1.Controls.Add(this.lkpUnidadMedida);
            this.groupControl1.Controls.Add(this.labelControl15);
            this.groupControl1.Controls.Add(this.lkpMoneda);
            this.groupControl1.Controls.Add(this.labelControl16);
            this.groupControl1.Controls.Add(this.txtCantidad);
            this.groupControl1.Controls.Add(this.labelControl20);
            this.groupControl1.Controls.Add(this.txtCUnitario);
            this.groupControl1.Controls.Add(this.labelControl19);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtCodigo);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(529, 158);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Datos";
            // 
            // txtOrden
            // 
            this.txtOrden.Enabled = false;
            this.txtOrden.Location = new System.Drawing.Point(84, 27);
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtOrden.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtOrden.Properties.Mask.ShowPlaceHolders = false;
            this.txtOrden.Size = new System.Drawing.Size(74, 20);
            this.txtOrden.TabIndex = 50;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(84, 49);
            this.txtDescripcion.MenuManager = this.barManager1;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcion.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescripcion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Size = new System.Drawing.Size(431, 48);
            this.txtDescripcion.TabIndex = 49;
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
            this.barDockControlTop.Size = new System.Drawing.Size(529, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 158);
            this.barDockControlBottom.Size = new System.Drawing.Size(529, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 158);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(529, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 158);
            // 
            // txtCTotal
            // 
            this.txtCTotal.EditValue = "0";
            this.txtCTotal.Location = new System.Drawing.Point(423, 103);
            this.txtCTotal.Name = "txtCTotal";
            this.txtCTotal.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCTotal.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCTotal.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCTotal.Properties.Mask.EditMask = "n4";
            this.txtCTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCTotal.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCTotal.Size = new System.Drawing.Size(92, 20);
            this.txtCTotal.TabIndex = 48;
            // 
            // lkpTipo
            // 
            this.lkpTipo.Location = new System.Drawing.Point(50, 128);
            this.lkpTipo.MenuManager = this.barManager1;
            this.lkpTipo.Name = "lkpTipo";
            this.lkpTipo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpTipo.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpTipo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipo.Properties.NullText = "";
            this.lkpTipo.Size = new System.Drawing.Size(126, 20);
            this.lkpTipo.TabIndex = 45;
            this.lkpTipo.EditValueChanged += new System.EventHandler(this.lkpTipo_EditValueChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(20, 131);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 13);
            this.labelControl6.TabIndex = 46;
            this.labelControl6.Text = "Tipo:";
            // 
            // btnProducto
            // 
            this.btnProducto.Location = new System.Drawing.Point(305, 128);
            this.btnProducto.Name = "btnProducto";
            this.btnProducto.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.btnProducto.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.btnProducto.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnProducto.Properties.Mask.ShowPlaceHolders = false;
            this.btnProducto.Properties.ReadOnly = true;
            this.btnProducto.Size = new System.Drawing.Size(210, 20);
            this.btnProducto.TabIndex = 41;
            this.btnProducto.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnProducto_ButtonClick);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(252, 131);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 42;
            this.labelControl4.Text = "Producto:";
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(375, 106);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(42, 13);
            this.labelControl21.TabIndex = 40;
            this.labelControl21.Text = "C.Total :";
            // 
            // lkpUnidadMedida
            // 
            this.lkpUnidadMedida.Location = new System.Drawing.Point(406, 25);
            this.lkpUnidadMedida.MenuManager = this.barManager1;
            this.lkpUnidadMedida.Name = "lkpUnidadMedida";
            this.lkpUnidadMedida.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpUnidadMedida.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpUnidadMedida.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpUnidadMedida.Properties.NullText = "";
            this.lkpUnidadMedida.Size = new System.Drawing.Size(109, 20);
            this.lkpUnidadMedida.TabIndex = 37;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(359, 28);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(41, 13);
            this.labelControl15.TabIndex = 38;
            this.labelControl15.Text = "Medida :";
            // 
            // lkpMoneda
            // 
            this.lkpMoneda.Location = new System.Drawing.Point(228, 103);
            this.lkpMoneda.Name = "lkpMoneda";
            this.lkpMoneda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpMoneda.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpMoneda.Properties.NullText = "";
            this.lkpMoneda.Size = new System.Drawing.Size(118, 20);
            this.lkpMoneda.TabIndex = 25;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(180, 106);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(42, 13);
            this.labelControl16.TabIndex = 24;
            this.labelControl16.Text = "Moneda:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.EditValue = "0";
            this.txtCantidad.Location = new System.Drawing.Point(260, 27);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCantidad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCantidad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCantidad.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCantidad.Properties.Mask.EditMask = "n2";
            this.txtCantidad.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCantidad.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCantidad.Size = new System.Drawing.Size(60, 20);
            this.txtCantidad.TabIndex = 22;
            this.txtCantidad.EditValueChanged += new System.EventHandler(this.txtCantidad_EditValueChanged);
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(204, 28);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(50, 13);
            this.labelControl20.TabIndex = 23;
            this.labelControl20.Text = "Cantidad :";
            // 
            // txtCUnitario
            // 
            this.txtCUnitario.EditValue = "0";
            this.txtCUnitario.Location = new System.Drawing.Point(84, 103);
            this.txtCUnitario.Name = "txtCUnitario";
            this.txtCUnitario.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCUnitario.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCUnitario.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCUnitario.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCUnitario.Properties.Mask.EditMask = "n4";
            this.txtCUnitario.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCUnitario.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCUnitario.Size = new System.Drawing.Size(90, 20);
            this.txtCUnitario.TabIndex = 20;
            this.txtCUnitario.EditValueChanged += new System.EventHandler(this.txtCUnitario_EditValueChanged);
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(17, 106);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(58, 13);
            this.labelControl19.TabIndex = 21;
            this.labelControl19.Text = "C.Unitario : ";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(74, 30);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(4, 13);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = ":";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(74, 53);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(4, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = ":";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(50, 172);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCodigo.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCodigo.Properties.Mask.ShowPlaceHolders = false;
            this.txtCodigo.Size = new System.Drawing.Size(74, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Descripción";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Código ";
            // 
            // frmManteHojaCostosRubros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 185);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManteHojaCostosRubros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registro de Rubros";
            this.Load += new System.EventHandler(this.frmManteBanco_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrden.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProducto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUnidadMedida.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCUnitario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txtCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.TextEdit txtCantidad;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.TextEdit txtCUnitario;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LookUpEdit lkpUnidadMedida;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        public DevExpress.XtraEditors.LookUpEdit lkpMoneda;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        public DevExpress.XtraEditors.ButtonEdit btnProducto;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.LookUpEdit lkpTipo;
        private DevExpress.XtraEditors.TextEdit txtCTotal;
        private DevExpress.XtraEditors.MemoEdit txtDescripcion;
        public DevExpress.XtraEditors.TextEdit txtOrden;
    }
}