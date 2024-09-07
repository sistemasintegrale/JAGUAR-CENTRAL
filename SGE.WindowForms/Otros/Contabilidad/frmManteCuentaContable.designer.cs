namespace SGE.WindowForms.Otros.Contabilidad
{
    partial class frmManteCuentaContable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteCuentaContable));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.TreeView1 = new System.Windows.Forms.TreeView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtbuscar = new DevExpress.XtraEditors.ButtonEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.txtCuentaContable = new DevExpress.XtraEditors.TextEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.bteCuentaHaber = new DevExpress.XtraEditors.ButtonEdit();
            this.bteCuentaDebe = new DevExpress.XtraEditors.ButtonEdit();
            this.ChkCentroCosto = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.lkpTipoMoneda = new DevExpress.XtraEditors.LookUpEdit();
            this.LkpEstado = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescripcionHaber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescripcionDebe = new DevExpress.XtraEditors.TextEdit();
            this.LkpTipoAnalitica = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lkpTipoCuenta = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescripcionLarga = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.BtnModificar = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbuscar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaContable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuentaHaber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuentaDebe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCentroCosto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoMoneda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpEstado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionHaber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionDebe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoAnalitica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoCuenta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionLarga.Properties)).BeginInit();
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
            this.barManager1.MaxItemId = 7;
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
            this.btnGuardar.Id = 5;
            this.btnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGuardar_ItemClick_1);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Caption = "Cancelar";
            this.btnCancelar.Glyph = global::SGE.WindowForms.Properties.Resources.doc_exit;
            this.btnCancelar.Id = 6;
            this.btnCancelar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCancelar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancelar_ItemClick_1);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(523, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 187);
            this.barDockControlBottom.Size = new System.Drawing.Size(523, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 187);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(523, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 187);
            // 
            // TreeView1
            // 
            this.TreeView1.Location = new System.Drawing.Point(598, 65);
            this.TreeView1.Name = "TreeView1";
            this.TreeView1.Size = new System.Drawing.Size(38, 35);
            this.TreeView1.TabIndex = 7;
            this.TreeView1.Visible = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtbuscar);
            this.groupControl1.Location = new System.Drawing.Point(598, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(46, 32);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Filtro";
            this.groupControl1.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 13);
            this.labelControl1.TabIndex = 22;
            this.labelControl1.Text = "Buscar Cuenta :";
            // 
            // txtbuscar
            // 
            this.txtbuscar.Enabled = false;
            this.txtbuscar.Location = new System.Drawing.Point(98, 31);
            this.txtbuscar.Name = "txtbuscar";
            this.txtbuscar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK)});
            this.txtbuscar.Size = new System.Drawing.Size(178, 20);
            this.txtbuscar.TabIndex = 2;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.txtCuentaContable);
            this.groupControl2.Controls.Add(this.labelControl20);
            this.groupControl2.Controls.Add(this.labelControl19);
            this.groupControl2.Controls.Add(this.labelControl18);
            this.groupControl2.Controls.Add(this.labelControl16);
            this.groupControl2.Controls.Add(this.labelControl15);
            this.groupControl2.Controls.Add(this.labelControl14);
            this.groupControl2.Controls.Add(this.labelControl13);
            this.groupControl2.Controls.Add(this.labelControl12);
            this.groupControl2.Controls.Add(this.bteCuentaHaber);
            this.groupControl2.Controls.Add(this.bteCuentaDebe);
            this.groupControl2.Controls.Add(this.ChkCentroCosto);
            this.groupControl2.Controls.Add(this.labelControl11);
            this.groupControl2.Controls.Add(this.lkpTipoMoneda);
            this.groupControl2.Controls.Add(this.LkpEstado);
            this.groupControl2.Controls.Add(this.labelControl10);
            this.groupControl2.Controls.Add(this.labelControl9);
            this.groupControl2.Controls.Add(this.txtDescripcionHaber);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.txtDescripcionDebe);
            this.groupControl2.Controls.Add(this.LkpTipoAnalitica);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.lkpTipoCuenta);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.txtDescripcionLarga);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(523, 187);
            this.groupControl2.TabIndex = 12;
            this.groupControl2.Text = "Detalle de Cuenta Contable";
            // 
            // txtCuentaContable
            // 
            this.txtCuentaContable.Location = new System.Drawing.Point(128, 24);
            this.txtCuentaContable.Name = "txtCuentaContable";
            this.txtCuentaContable.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtCuentaContable.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtCuentaContable.Size = new System.Drawing.Size(165, 20);
            this.txtCuentaContable.TabIndex = 1;
            this.txtCuentaContable.InvalidValue += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.txtCuentaContable_InvalidValue);
            this.txtCuentaContable.Validating += new System.ComponentModel.CancelEventHandler(this.txtCuentaContable_Validating);
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(356, 74);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(4, 13);
            this.labelControl20.TabIndex = 29;
            this.labelControl20.Text = ":";
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(121, 27);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(4, 13);
            this.labelControl19.TabIndex = 28;
            this.labelControl19.Text = ":";
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(121, 50);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(4, 13);
            this.labelControl18.TabIndex = 27;
            this.labelControl18.Text = ":";
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(121, 73);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(4, 13);
            this.labelControl16.TabIndex = 26;
            this.labelControl16.Text = ":";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(121, 97);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(4, 13);
            this.labelControl15.TabIndex = 25;
            this.labelControl15.Text = ":";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(121, 119);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(4, 13);
            this.labelControl14.TabIndex = 24;
            this.labelControl14.Text = ":";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(121, 165);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(4, 13);
            this.labelControl13.TabIndex = 23;
            this.labelControl13.Text = ":";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(121, 142);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(4, 13);
            this.labelControl12.TabIndex = 22;
            this.labelControl12.Text = ":";
            // 
            // bteCuentaHaber
            // 
            this.bteCuentaHaber.Location = new System.Drawing.Point(128, 139);
            this.bteCuentaHaber.MenuManager = this.barManager1;
            this.bteCuentaHaber.Name = "bteCuentaHaber";
            this.bteCuentaHaber.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteCuentaHaber.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteCuentaHaber.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteCuentaHaber.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteCuentaHaber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCuentaHaber.Properties.ReadOnly = true;
            this.bteCuentaHaber.Size = new System.Drawing.Size(100, 20);
            this.bteCuentaHaber.TabIndex = 21;
            this.bteCuentaHaber.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCuentaHaber_ButtonClick_1);
            // 
            // bteCuentaDebe
            // 
            this.bteCuentaDebe.Location = new System.Drawing.Point(128, 116);
            this.bteCuentaDebe.MenuManager = this.barManager1;
            this.bteCuentaDebe.Name = "bteCuentaDebe";
            this.bteCuentaDebe.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.bteCuentaDebe.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.bteCuentaDebe.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            this.bteCuentaDebe.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.bteCuentaDebe.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.bteCuentaDebe.Properties.ReadOnly = true;
            this.bteCuentaDebe.Size = new System.Drawing.Size(100, 20);
            this.bteCuentaDebe.TabIndex = 17;
            this.bteCuentaDebe.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bteCuentaDebe_ButtonClick_1);
            // 
            // ChkCentroCosto
            // 
            this.ChkCentroCosto.Location = new System.Drawing.Point(424, 163);
            this.ChkCentroCosto.MenuManager = this.barManager1;
            this.ChkCentroCosto.Name = "ChkCentroCosto";
            this.ChkCentroCosto.Properties.Caption = "Centro Costo";
            this.ChkCentroCosto.Size = new System.Drawing.Size(85, 19);
            this.ChkCentroCosto.TabIndex = 14;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(293, 74);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(61, 13);
            this.labelControl11.TabIndex = 20;
            this.labelControl11.Text = "Tipo Moneda";
            // 
            // lkpTipoMoneda
            // 
            this.lkpTipoMoneda.Location = new System.Drawing.Point(366, 71);
            this.lkpTipoMoneda.Name = "lkpTipoMoneda";
            this.lkpTipoMoneda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpTipoMoneda.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpTipoMoneda.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoMoneda.Properties.NullText = "";
            this.lkpTipoMoneda.Size = new System.Drawing.Size(143, 20);
            this.lkpTipoMoneda.TabIndex = 5;
            // 
            // LkpEstado
            // 
            this.LkpEstado.Location = new System.Drawing.Point(128, 162);
            this.LkpEstado.Name = "LkpEstado";
            this.LkpEstado.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.LkpEstado.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.LkpEstado.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpEstado.Properties.NullText = "";
            this.LkpEstado.Size = new System.Drawing.Size(143, 20);
            this.LkpEstado.TabIndex = 12;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(6, 165);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(33, 13);
            this.labelControl10.TabIndex = 17;
            this.labelControl10.Text = "Estado";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(5, 142);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(92, 13);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "2° Cta. Automática";
            // 
            // txtDescripcionHaber
            // 
            this.txtDescripcionHaber.Enabled = false;
            this.txtDescripcionHaber.Location = new System.Drawing.Point(234, 139);
            this.txtDescripcionHaber.Name = "txtDescripcionHaber";
            this.txtDescripcionHaber.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcionHaber.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescripcionHaber.Size = new System.Drawing.Size(275, 20);
            this.txtDescripcionHaber.TabIndex = 10;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(5, 120);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(92, 13);
            this.labelControl8.TabIndex = 13;
            this.labelControl8.Text = "1° Cta. Automática";
            // 
            // txtDescripcionDebe
            // 
            this.txtDescripcionDebe.Enabled = false;
            this.txtDescripcionDebe.Location = new System.Drawing.Point(234, 117);
            this.txtDescripcionDebe.Name = "txtDescripcionDebe";
            this.txtDescripcionDebe.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcionDebe.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescripcionDebe.Size = new System.Drawing.Size(275, 20);
            this.txtDescripcionDebe.TabIndex = 8;
            // 
            // LkpTipoAnalitica
            // 
            this.LkpTipoAnalitica.Location = new System.Drawing.Point(128, 94);
            this.LkpTipoAnalitica.Name = "LkpTipoAnalitica";
            this.LkpTipoAnalitica.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.LkpTipoAnalitica.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.LkpTipoAnalitica.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpTipoAnalitica.Properties.NullText = "";
            this.LkpTipoAnalitica.Size = new System.Drawing.Size(143, 20);
            this.LkpTipoAnalitica.TabIndex = 6;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(6, 98);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(63, 13);
            this.labelControl6.TabIndex = 8;
            this.labelControl6.Text = "Tipo Analítica";
            // 
            // lkpTipoCuenta
            // 
            this.lkpTipoCuenta.Location = new System.Drawing.Point(128, 70);
            this.lkpTipoCuenta.Name = "lkpTipoCuenta";
            this.lkpTipoCuenta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.lkpTipoCuenta.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.lkpTipoCuenta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipoCuenta.Properties.NullText = "";
            this.lkpTipoCuenta.Size = new System.Drawing.Size(143, 20);
            this.lkpTipoCuenta.TabIndex = 4;
            this.lkpTipoCuenta.EditValueChanged += new System.EventHandler(this.lkpTipoCuenta_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(6, 73);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(58, 13);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "Tipo Cuenta";
            // 
            // txtDescripcionLarga
            // 
            this.txtDescripcionLarga.Location = new System.Drawing.Point(128, 47);
            this.txtDescripcionLarga.Name = "txtDescripcionLarga";
            this.txtDescripcionLarga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.txtDescripcionLarga.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.txtDescripcionLarga.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcionLarga.Size = new System.Drawing.Size(381, 20);
            this.txtDescripcionLarga.TabIndex = 2;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(6, 50);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(54, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Descripción";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(81, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Cuenta Contable";
            // 
            // BtnModificar
            // 
            this.BtnModificar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.BtnModificar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.BtnModificar.Caption = "Modificar";
            this.BtnModificar.Glyph = ((System.Drawing.Image)(resources.GetObject("BtnModificar.Glyph")));
            this.BtnModificar.Id = 3;
            this.BtnModificar.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnModificar.ItemAppearance.Normal.Options.UseFont = true;
            this.BtnModificar.Name = "BtnModificar";
            this.BtnModificar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnModificar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // frmManteCuentaContable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 214);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.TreeView1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManteCuentaContable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Registro Plan de Cuentas";
            this.Load += new System.EventHandler(this.FrmCuentaContable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtbuscar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCuentaContable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuentaHaber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bteCuentaDebe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkCentroCosto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoMoneda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpEstado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionHaber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionDebe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpTipoAnalitica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipoCuenta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcionLarga.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        internal System.Windows.Forms.TreeView TreeView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit txtbuscar;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckEdit ChkCentroCosto;
        private DevExpress.XtraEditors.TextEdit txtCuentaContable;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LookUpEdit lkpTipoMoneda;
        private DevExpress.XtraEditors.LookUpEdit LkpEstado;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txtDescripcionHaber;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtDescripcionDebe;
        private DevExpress.XtraEditors.LookUpEdit LkpTipoAnalitica;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit lkpTipoCuenta;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtDescripcionLarga;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraBars.BarButtonItem BtnModificar;
        private DevExpress.XtraEditors.ButtonEdit bteCuentaDebe;
        private DevExpress.XtraEditors.ButtonEdit bteCuentaHaber;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
    }
}