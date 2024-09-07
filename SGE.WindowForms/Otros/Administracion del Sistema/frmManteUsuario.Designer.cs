namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    partial class frmManteUsuario
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chkVerContaseña = new DevExpress.XtraEditors.CheckEdit();
            this.chkSupervisor = new DevExpress.XtraEditors.CheckEdit();
            this.lkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.pnModificarContraseña = new DevExpress.XtraEditors.PanelControl();
            this.txtCntrAntigua = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtCntrNueva = new DevExpress.XtraEditors.TextEdit();
            this.txtCntrNuevaConfirma = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cbModificarContrasena = new DevExpress.XtraEditors.CheckEdit();
            this.txtCntrConfirma = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCntr = new DevExpress.XtraEditors.TextEdit();
            this.txtNombreApe = new DevExpress.XtraEditors.TextEdit();
            this.txtUsuario = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblIGV = new DevExpress.XtraEditors.LabelControl();
            this.lkpPuntoVenta = new DevExpress.XtraEditors.LookUpEdit();
            this.ckPuntoVenta = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVerContaseña.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSupervisor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnModificarContraseña)).BeginInit();
            this.pnModificarContraseña.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntrAntigua.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntrNueva.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntrNuevaConfirma.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbModificarContrasena.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntrConfirma.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreApe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckPuntoVenta.Properties)).BeginInit();
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
            this.btnGuardar.Id = 0;
            this.btnGuardar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Caption = "Cancelar";
            this.btnCancelar.Id = 1;
            this.btnCancelar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_exit;
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
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(526, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 138);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(526, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 138);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(526, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 138);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkpPuntoVenta);
            this.groupControl1.Controls.Add(this.ckPuntoVenta);
            this.groupControl1.Controls.Add(this.chkVerContaseña);
            this.groupControl1.Controls.Add(this.chkSupervisor);
            this.groupControl1.Controls.Add(this.lkpSituacion);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.pnModificarContraseña);
            this.groupControl1.Controls.Add(this.cbModificarContrasena);
            this.groupControl1.Controls.Add(this.txtCntrConfirma);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtCntr);
            this.groupControl1.Controls.Add(this.txtNombreApe);
            this.groupControl1.Controls.Add(this.txtUsuario);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.lblIGV);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(526, 138);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Datos de Usuario";
            // 
            // chkVerContaseña
            // 
            this.chkVerContaseña.Location = new System.Drawing.Point(276, 74);
            this.chkVerContaseña.MenuManager = this.barManager1;
            this.chkVerContaseña.Name = "chkVerContaseña";
            this.chkVerContaseña.Properties.Caption = "Ver Contraseña";
            this.chkVerContaseña.Size = new System.Drawing.Size(102, 19);
            this.chkVerContaseña.TabIndex = 23;
            this.chkVerContaseña.CheckedChanged += new System.EventHandler(this.chkVerContaseña_CheckedChanged);
            // 
            // chkSupervisor
            // 
            this.chkSupervisor.Location = new System.Drawing.Point(384, 74);
            this.chkSupervisor.MenuManager = this.barManager1;
            this.chkSupervisor.Name = "chkSupervisor";
            this.chkSupervisor.Properties.Caption = "Supervisor";
            this.chkSupervisor.Size = new System.Drawing.Size(82, 19);
            this.chkSupervisor.TabIndex = 22;
            this.chkSupervisor.CheckedChanged += new System.EventHandler(this.chkSupervisor_CheckedChanged);
            // 
            // lkpSituacion
            // 
            this.lkpSituacion.Enabled = false;
            this.lkpSituacion.Location = new System.Drawing.Point(317, 30);
            this.lkpSituacion.Name = "lkpSituacion";
            this.lkpSituacion.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpSituacion.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSituacion.Properties.NullText = "";
            this.lkpSituacion.Size = new System.Drawing.Size(182, 20);
            this.lkpSituacion.TabIndex = 8;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(271, 33);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(40, 13);
            this.labelControl7.TabIndex = 21;
            this.labelControl7.Text = "Estado :";
            // 
            // pnModificarContraseña
            // 
            this.pnModificarContraseña.Controls.Add(this.txtCntrAntigua);
            this.pnModificarContraseña.Controls.Add(this.labelControl5);
            this.pnModificarContraseña.Controls.Add(this.labelControl6);
            this.pnModificarContraseña.Controls.Add(this.txtCntrNueva);
            this.pnModificarContraseña.Controls.Add(this.txtCntrNuevaConfirma);
            this.pnModificarContraseña.Controls.Add(this.labelControl4);
            this.pnModificarContraseña.Location = new System.Drawing.Point(4, 143);
            this.pnModificarContraseña.Name = "pnModificarContraseña";
            this.pnModificarContraseña.Size = new System.Drawing.Size(484, 72);
            this.pnModificarContraseña.TabIndex = 20;
            this.pnModificarContraseña.Visible = false;
            // 
            // txtCntrAntigua
            // 
            this.txtCntrAntigua.EditValue = "";
            this.txtCntrAntigua.Location = new System.Drawing.Point(158, 5);
            this.txtCntrAntigua.Name = "txtCntrAntigua";
            this.txtCntrAntigua.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCntrAntigua.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCntrAntigua.Properties.MaxLength = 15;
            this.txtCntrAntigua.Properties.UseSystemPasswordChar = true;
            this.txtCntrAntigua.Size = new System.Drawing.Size(138, 20);
            this.txtCntrAntigua.TabIndex = 5;
            this.txtCntrAntigua.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCntrAntigua_KeyUp);
            // 
            // labelControl5
            // 
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl5.Location = new System.Drawing.Point(7, 30);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(97, 13);
            this.labelControl5.TabIndex = 14;
            this.labelControl5.Text = "Contraseña Nueva :";
            // 
            // labelControl6
            // 
            this.labelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl6.Location = new System.Drawing.Point(7, 9);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(103, 13);
            this.labelControl6.TabIndex = 18;
            this.labelControl6.Text = "Contraseña Antigua :";
            // 
            // txtCntrNueva
            // 
            this.txtCntrNueva.EditValue = "";
            this.txtCntrNueva.Enabled = false;
            this.txtCntrNueva.Location = new System.Drawing.Point(158, 26);
            this.txtCntrNueva.Name = "txtCntrNueva";
            this.txtCntrNueva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCntrNueva.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCntrNueva.Properties.MaxLength = 15;
            this.txtCntrNueva.Properties.UseSystemPasswordChar = true;
            this.txtCntrNueva.Size = new System.Drawing.Size(138, 20);
            this.txtCntrNueva.TabIndex = 6;
            this.txtCntrNueva.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCntrNueva_KeyUp);
            // 
            // txtCntrNuevaConfirma
            // 
            this.txtCntrNuevaConfirma.Enabled = false;
            this.txtCntrNuevaConfirma.Location = new System.Drawing.Point(158, 47);
            this.txtCntrNuevaConfirma.Name = "txtCntrNuevaConfirma";
            this.txtCntrNuevaConfirma.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCntrNuevaConfirma.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCntrNuevaConfirma.Properties.MaxLength = 15;
            this.txtCntrNuevaConfirma.Properties.UseSystemPasswordChar = true;
            this.txtCntrNuevaConfirma.Size = new System.Drawing.Size(138, 20);
            this.txtCntrNuevaConfirma.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl4.Location = new System.Drawing.Point(7, 51);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(147, 13);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "Confirmar Contraseña Nueva :";
            // 
            // cbModificarContrasena
            // 
            this.cbModificarContrasena.Enabled = false;
            this.cbModificarContrasena.Location = new System.Drawing.Point(7, 120);
            this.cbModificarContrasena.MenuManager = this.barManager1;
            this.cbModificarContrasena.Name = "cbModificarContrasena";
            this.cbModificarContrasena.Properties.Caption = "Modificar Contraseña";
            this.cbModificarContrasena.Size = new System.Drawing.Size(150, 19);
            this.cbModificarContrasena.TabIndex = 13;
            this.cbModificarContrasena.CheckedChanged += new System.EventHandler(this.cbModificarContrasena_CheckedChanged);
            // 
            // txtCntrConfirma
            // 
            this.txtCntrConfirma.Enabled = false;
            this.txtCntrConfirma.Location = new System.Drawing.Point(127, 94);
            this.txtCntrConfirma.Name = "txtCntrConfirma";
            this.txtCntrConfirma.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCntrConfirma.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCntrConfirma.Properties.MaxLength = 15;
            this.txtCntrConfirma.Properties.UseSystemPasswordChar = true;
            this.txtCntrConfirma.Size = new System.Drawing.Size(138, 20);
            this.txtCntrConfirma.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl2.Location = new System.Drawing.Point(11, 97);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(113, 13);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "Confirmar Contraseña :";
            // 
            // txtCntr
            // 
            this.txtCntr.EditValue = "";
            this.txtCntr.Location = new System.Drawing.Point(127, 73);
            this.txtCntr.Name = "txtCntr";
            this.txtCntr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCntr.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCntr.Properties.MaxLength = 15;
            this.txtCntr.Properties.UseSystemPasswordChar = true;
            this.txtCntr.Size = new System.Drawing.Size(138, 20);
            this.txtCntr.TabIndex = 3;
            this.txtCntr.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCntr_KeyUp);
            // 
            // txtNombreApe
            // 
            this.txtNombreApe.Location = new System.Drawing.Point(127, 52);
            this.txtNombreApe.Name = "txtNombreApe";
            this.txtNombreApe.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtNombreApe.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtNombreApe.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreApe.Properties.MaxLength = 100;
            this.txtNombreApe.Size = new System.Drawing.Size(372, 20);
            this.txtNombreApe.TabIndex = 2;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(127, 31);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtUsuario.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtUsuario.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuario.Properties.MaxLength = 30;
            this.txtUsuario.Size = new System.Drawing.Size(138, 20);
            this.txtUsuario.TabIndex = 1;
            this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsuario_KeyDown);
            // 
            // labelControl3
            // 
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl3.Location = new System.Drawing.Point(11, 76);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(63, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Contraseña :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 55);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(98, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Nombre y Apellidos :";
            // 
            // lblIGV
            // 
            this.lblIGV.Location = new System.Drawing.Point(11, 33);
            this.lblIGV.Name = "lblIGV";
            this.lblIGV.Size = new System.Drawing.Size(46, 13);
            this.lblIGV.TabIndex = 0;
            this.lblIGV.Text = "Usuario  :";
            // 
            // lkpPuntoVenta
            // 
            this.lkpPuntoVenta.Location = new System.Drawing.Point(384, 94);
            this.lkpPuntoVenta.MenuManager = this.barManager1;
            this.lkpPuntoVenta.Name = "lkpPuntoVenta";
            this.lkpPuntoVenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpPuntoVenta.Properties.NullText = "NINGUNO";
            this.lkpPuntoVenta.Size = new System.Drawing.Size(115, 20);
            this.lkpPuntoVenta.TabIndex = 29;
            // 
            // ckPuntoVenta
            // 
            this.ckPuntoVenta.Location = new System.Drawing.Point(276, 95);
            this.ckPuntoVenta.MenuManager = this.barManager1;
            this.ckPuntoVenta.Name = "ckPuntoVenta";
            this.ckPuntoVenta.Properties.Caption = "Punto de Venta";
            this.ckPuntoVenta.Size = new System.Drawing.Size(102, 19);
            this.ckPuntoVenta.TabIndex = 28;
            this.ckPuntoVenta.CheckedChanged += new System.EventHandler(this.ckPuntoVenta_CheckedChanged);
            // 
            // frmManteUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 166);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManteUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Registro de Usuario";
            this.Load += new System.EventHandler(this.frmManteUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVerContaseña.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSupervisor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnModificarContraseña)).EndInit();
            this.pnModificarContraseña.ResumeLayout(false);
            this.pnModificarContraseña.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntrAntigua.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntrNueva.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntrNuevaConfirma.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbModificarContrasena.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntrConfirma.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCntr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreApe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpPuntoVenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckPuntoVenta.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl pnModificarContraseña;
        public DevExpress.XtraEditors.TextEdit txtCntrAntigua;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtCntrNueva;
        public DevExpress.XtraEditors.TextEdit txtCntrNuevaConfirma;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckEdit cbModificarContrasena;
        public DevExpress.XtraEditors.TextEdit txtCntrConfirma;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtCntr;
        public DevExpress.XtraEditors.TextEdit txtNombreApe;
        public DevExpress.XtraEditors.TextEdit txtUsuario;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblIGV;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.LookUpEdit lkpSituacion;
        private DevExpress.XtraEditors.CheckEdit chkSupervisor;
        private DevExpress.XtraEditors.CheckEdit chkVerContaseña;
        private DevExpress.XtraEditors.LookUpEdit lkpPuntoVenta;
        private DevExpress.XtraEditors.CheckEdit ckPuntoVenta;
    }
}