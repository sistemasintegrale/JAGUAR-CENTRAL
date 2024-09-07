namespace SGE.WindowForms.Otros.Tesoreria.Ventas
{
    partial class FrmManteEventoVenta
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtNEvento = new DevExpress.XtraEditors.TextEdit();
            this.txtlugar = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.LkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtnombreEvento = new DevExpress.XtraEditors.TextEdit();
            this.lkpalmacen = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.dteFechaFinal = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.dteFechaInicio = new DevExpress.XtraEditors.DateEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtRepresentante = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txttelefono = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtcontacto = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtcorreo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDireccion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNEvento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtlugar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnombreEvento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpalmacen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaInicio.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaInicio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepresentante.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttelefono.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcontacto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcorreo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion.Properties)).BeginInit();
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
            this.BtnGuardar.Hint = "Guardar";
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnGuardar.ItemAppearance.Normal.Options.UseFont = true;
            this.BtnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGuardar_ItemClick);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.BtnCancelar.Caption = "Salir";
            this.BtnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.BtnCancelar.Hint = "Cancelar";
            this.BtnCancelar.Id = 1;
            this.BtnCancelar.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnCancelar.ItemAppearance.Normal.Options.UseFont = true;
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
            this.barDockControlTop.Size = new System.Drawing.Size(662, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 163);
            this.barDockControlBottom.Size = new System.Drawing.Size(662, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 163);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(662, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 163);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nº de Evento:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Lugar:";
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // txtNEvento
            // 
            this.txtNEvento.EditValue = "00000";
            this.txtNEvento.Location = new System.Drawing.Point(99, 25);
            this.txtNEvento.Name = "txtNEvento";
            this.txtNEvento.Properties.Mask.EditMask = "d5";
            this.txtNEvento.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNEvento.Properties.Mask.ShowPlaceHolders = false;
            this.txtNEvento.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNEvento.Properties.MaxLength = 1;
            this.txtNEvento.Properties.ReadOnly = true;
            this.txtNEvento.Size = new System.Drawing.Size(79, 20);
            this.txtNEvento.TabIndex = 1;
            this.txtNEvento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cerrar_form);
            // 
            // txtlugar
            // 
            this.txtlugar.Enabled = false;
            this.txtlugar.Location = new System.Drawing.Point(99, 48);
            this.txtlugar.Name = "txtlugar";
            this.txtlugar.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtlugar.Properties.MaxLength = 60;
            this.txtlugar.Size = new System.Drawing.Size(328, 20);
            this.txtlugar.TabIndex = 3;
            this.txtlugar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtdescripcion_KeyUp);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(486, 28);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(47, 13);
            this.labelControl12.TabIndex = 0;
            this.labelControl12.Text = "Situación:";
            // 
            // LkpSituacion
            // 
            this.LkpSituacion.Enabled = false;
            this.LkpSituacion.Location = new System.Drawing.Point(539, 25);
            this.LkpSituacion.Name = "LkpSituacion";
            this.LkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpSituacion.Properties.NullText = "";
            this.LkpSituacion.Size = new System.Drawing.Size(117, 20);
            this.LkpSituacion.TabIndex = 2;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtnombreEvento);
            this.groupControl1.Controls.Add(this.lkpalmacen);
            this.groupControl1.Controls.Add(this.labelControl18);
            this.groupControl1.Controls.Add(this.dteFechaFinal);
            this.groupControl1.Controls.Add(this.labelControl9);
            this.groupControl1.Controls.Add(this.dteFechaInicio);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Controls.Add(this.txtRepresentante);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txttelefono);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.txtcontacto);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.txtcorreo);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.txtDireccion);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.LkpSituacion);
            this.groupControl1.Controls.Add(this.labelControl12);
            this.groupControl1.Controls.Add(this.txtlugar);
            this.groupControl1.Controls.Add(this.txtNEvento);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(662, 163);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Evento por Ventas";
            // 
            // txtnombreEvento
            // 
            this.txtnombreEvento.Location = new System.Drawing.Point(184, 25);
            this.txtnombreEvento.Name = "txtnombreEvento";
            this.txtnombreEvento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtnombreEvento.Properties.MaxLength = 60;
            this.txtnombreEvento.Size = new System.Drawing.Size(296, 20);
            this.txtnombreEvento.TabIndex = 26;
            // 
            // lkpalmacen
            // 
            this.lkpalmacen.Location = new System.Drawing.Point(486, 116);
            this.lkpalmacen.Name = "lkpalmacen";
            this.lkpalmacen.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpalmacen.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpalmacen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpalmacen.Properties.NullText = "";
            this.lkpalmacen.Size = new System.Drawing.Size(170, 20);
            this.lkpalmacen.TabIndex = 25;
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(414, 119);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(44, 13);
            this.labelControl18.TabIndex = 24;
            this.labelControl18.Text = "Almacén:";
            // 
            // dteFechaFinal
            // 
            this.dteFechaFinal.EditValue = null;
            this.dteFechaFinal.Location = new System.Drawing.Point(282, 116);
            this.dteFechaFinal.Name = "dteFechaFinal";
            this.dteFechaFinal.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dteFechaFinal.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dteFechaFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFechaFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFechaFinal.Size = new System.Drawing.Size(98, 20);
            this.dteFechaFinal.TabIndex = 18;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(203, 121);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(58, 13);
            this.labelControl9.TabIndex = 17;
            this.labelControl9.Text = "Fecha Final:";
            // 
            // dteFechaInicio
            // 
            this.dteFechaInicio.EditValue = null;
            this.dteFechaInicio.Location = new System.Drawing.Point(99, 116);
            this.dteFechaInicio.Name = "dteFechaInicio";
            this.dteFechaInicio.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dteFechaInicio.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dteFechaInicio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFechaInicio.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFechaInicio.Size = new System.Drawing.Size(98, 20);
            this.dteFechaInicio.TabIndex = 16;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(17, 119);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(64, 13);
            this.labelControl8.TabIndex = 15;
            this.labelControl8.Text = "Fecha Inicio :";
            // 
            // txtRepresentante
            // 
            this.txtRepresentante.Location = new System.Drawing.Point(99, 140);
            this.txtRepresentante.Name = "txtRepresentante";
            this.txtRepresentante.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRepresentante.Properties.MaxLength = 50;
            this.txtRepresentante.Size = new System.Drawing.Size(347, 20);
            this.txtRepresentante.TabIndex = 14;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 143);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 13);
            this.labelControl3.TabIndex = 13;
            this.labelControl3.Text = "Representante:";
            // 
            // txttelefono
            // 
            this.txttelefono.Location = new System.Drawing.Point(486, 93);
            this.txttelefono.Name = "txttelefono";
            this.txttelefono.Properties.MaxLength = 50;
            this.txttelefono.Size = new System.Drawing.Size(170, 20);
            this.txttelefono.TabIndex = 12;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(423, 96);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(46, 13);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "Teléfono:";
            // 
            // txtcontacto
            // 
            this.txtcontacto.Location = new System.Drawing.Point(99, 93);
            this.txtcontacto.Name = "txtcontacto";
            this.txtcontacto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcontacto.Properties.MaxLength = 50;
            this.txtcontacto.Size = new System.Drawing.Size(248, 20);
            this.txtcontacto.TabIndex = 10;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(16, 96);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 13);
            this.labelControl7.TabIndex = 9;
            this.labelControl7.Text = "Contacto:";
            // 
            // txtcorreo
            // 
            this.txtcorreo.Location = new System.Drawing.Point(486, 70);
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Properties.MaxLength = 40;
            this.txtcorreo.Size = new System.Drawing.Size(170, 20);
            this.txtcorreo.TabIndex = 8;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(423, 73);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(37, 13);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "Correo:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(99, 70);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccion.Properties.MaxLength = 90;
            this.txtDireccion.Size = new System.Drawing.Size(318, 20);
            this.txtDireccion.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(17, 73);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(47, 13);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "Dirección:";
            // 
            // FrmManteEventoVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 190);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximumSize = new System.Drawing.Size(678, 229);
            this.MinimumSize = new System.Drawing.Size(678, 228);
            this.Name = "FrmManteEventoVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Evento por Ventas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManteDetalleAnalitica_FormClosing);
            this.Load += new System.EventHandler(this.FrmManteGiroCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNEvento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtlugar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpSituacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtnombreEvento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpalmacen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaInicio.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaInicio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepresentante.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttelefono.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcontacto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcorreo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        public DevExpress.XtraBars.BarButtonItem BtnGuardar;
        private DevExpress.XtraBars.BarButtonItem BtnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.LookUpEdit LkpSituacion;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        public DevExpress.XtraEditors.TextEdit txtlugar;
        public DevExpress.XtraEditors.TextEdit txtNEvento;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtRepresentante;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.TextEdit txttelefono;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtcontacto;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.TextEdit txtcorreo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtDireccion;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        public DevExpress.XtraEditors.DateEdit dteFechaFinal;
        public DevExpress.XtraEditors.DateEdit dteFechaInicio;
        public DevExpress.XtraEditors.LookUpEdit lkpalmacen;
        public DevExpress.XtraEditors.TextEdit txtnombreEvento;
    }
}