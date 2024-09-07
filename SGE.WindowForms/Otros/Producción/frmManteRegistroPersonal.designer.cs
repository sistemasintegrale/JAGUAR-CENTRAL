namespace SGE.WindowForms.Otros.Producción
{
    partial class frmManteRegistroPersonal
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
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dteFechaNacimiento = new DevExpress.XtraEditors.DateEdit();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lkpSubAreaEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCargo = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpRelacionLaboral = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.lkpSexo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtTelefono = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtDomicilio = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtDNI = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtNombreApellidos = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.lkpAreaProceso = new DevExpress.XtraEditors.LookUpEdit();
            this.lkpAreaEmpresa = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaNacimiento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaNacimiento.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSubAreaEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCargo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRelacionLaboral.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSexo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefono.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDomicilio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDNI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreApellidos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpAreaProceso.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpAreaEmpresa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).BeginInit();
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
            this.barDockControlTop.Size = new System.Drawing.Size(765, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 155);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(765, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 155);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(765, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 155);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem,
            this.imprimirToolStripMenuItem});
            this.menu.Name = "contextMenuStrip1";
            this.menu.Size = new System.Drawing.Size(126, 92);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_anadir;
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_min_modificar;
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_eliminar;
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Image = global::SGE.WindowForms.Properties.Resources.doc_mini_imprimir;
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(260, 105);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(91, 13);
            this.labelControl2.TabIndex = 24;
            this.labelControl2.Text = "Fecha Nacimiento :";
            // 
            // dteFechaNacimiento
            // 
            this.dteFechaNacimiento.EditValue = null;
            this.dteFechaNacimiento.Location = new System.Drawing.Point(366, 101);
            this.dteFechaNacimiento.Name = "dteFechaNacimiento";
            this.dteFechaNacimiento.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dteFechaNacimiento.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dteFechaNacimiento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFechaNacimiento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFechaNacimiento.Size = new System.Drawing.Size(140, 20);
            this.dteFechaNacimiento.TabIndex = 25;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lkpSituacion);
            this.groupBox2.Controls.Add(this.labelControl4);
            this.groupBox2.Controls.Add(this.lkpSubAreaEmpresa);
            this.groupBox2.Controls.Add(this.labelControl3);
            this.groupBox2.Controls.Add(this.lkpCargo);
            this.groupBox2.Controls.Add(this.lkpRelacionLaboral);
            this.groupBox2.Controls.Add(this.labelControl9);
            this.groupBox2.Controls.Add(this.lkpSexo);
            this.groupBox2.Controls.Add(this.labelControl8);
            this.groupBox2.Controls.Add(this.txtTelefono);
            this.groupBox2.Controls.Add(this.labelControl7);
            this.groupBox2.Controls.Add(this.txtDomicilio);
            this.groupBox2.Controls.Add(this.labelControl6);
            this.groupBox2.Controls.Add(this.txtDNI);
            this.groupBox2.Controls.Add(this.dteFechaNacimiento);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.txtNombreApellidos);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Controls.Add(this.txtCodigo);
            this.groupBox2.Controls.Add(this.labelControl18);
            this.groupBox2.Controls.Add(this.lkpAreaProceso);
            this.groupBox2.Controls.Add(this.lkpAreaEmpresa);
            this.groupBox2.Controls.Add(this.labelControl20);
            this.groupBox2.Controls.Add(this.labelControl21);
            this.groupBox2.Controls.Add(this.labelControl23);
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(5, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(755, 129);
            this.groupBox2.TabIndex = 118;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Registro del Personal";
            // 
            // lkpSubAreaEmpresa
            // 
            this.lkpSubAreaEmpresa.Location = new System.Drawing.Point(628, 57);
            this.lkpSubAreaEmpresa.Name = "lkpSubAreaEmpresa";
            this.lkpSubAreaEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSubAreaEmpresa.Properties.NullText = "";
            this.lkpSubAreaEmpresa.Size = new System.Drawing.Size(122, 20);
            this.lkpSubAreaEmpresa.TabIndex = 139;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(532, 61);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(92, 13);
            this.labelControl3.TabIndex = 138;
            this.labelControl3.Text = "Sub Area Empresa:";
            // 
            // lkpCargo
            // 
            this.lkpCargo.Location = new System.Drawing.Point(366, 79);
            this.lkpCargo.Name = "lkpCargo";
            this.lkpCargo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCargo.Properties.NullText = "";
            this.lkpCargo.Size = new System.Drawing.Size(140, 20);
            this.lkpCargo.TabIndex = 137;
            // 
            // lkpRelacionLaboral
            // 
            this.lkpRelacionLaboral.Location = new System.Drawing.Point(102, 101);
            this.lkpRelacionLaboral.Name = "lkpRelacionLaboral";
            this.lkpRelacionLaboral.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpRelacionLaboral.Properties.NullText = "";
            this.lkpRelacionLaboral.Size = new System.Drawing.Size(140, 20);
            this.lkpRelacionLaboral.TabIndex = 136;
            this.lkpRelacionLaboral.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lkpRelacionLaboral_KeyDown);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(6, 105);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(85, 13);
            this.labelControl9.TabIndex = 135;
            this.labelControl9.Text = "Relacion Laboral :";
            // 
            // lkpSexo
            // 
            this.lkpSexo.Location = new System.Drawing.Point(628, 79);
            this.lkpSexo.Name = "lkpSexo";
            this.lkpSexo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSexo.Properties.NullText = "";
            this.lkpSexo.Size = new System.Drawing.Size(122, 20);
            this.lkpSexo.TabIndex = 134;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(532, 83);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(31, 13);
            this.labelControl8.TabIndex = 133;
            this.labelControl8.Text = "Sexo :";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(102, 57);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Properties.MaxLength = 50;
            this.txtTelefono.Size = new System.Drawing.Size(140, 20);
            this.txtTelefono.TabIndex = 132;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(6, 62);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(49, 13);
            this.labelControl7.TabIndex = 131;
            this.labelControl7.Text = "Telefono :";
            // 
            // txtDomicilio
            // 
            this.txtDomicilio.Location = new System.Drawing.Point(366, 35);
            this.txtDomicilio.Name = "txtDomicilio";
            this.txtDomicilio.Properties.MaxLength = 50;
            this.txtDomicilio.Size = new System.Drawing.Size(327, 20);
            this.txtDomicilio.TabIndex = 130;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(260, 37);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(47, 13);
            this.labelControl6.TabIndex = 129;
            this.labelControl6.Text = "Domicilio :";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(102, 35);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Properties.MaxLength = 50;
            this.txtDNI.Size = new System.Drawing.Size(140, 20);
            this.txtDNI.TabIndex = 128;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(6, 37);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(25, 13);
            this.labelControl5.TabIndex = 127;
            this.labelControl5.Text = "DNI :";
            // 
            // txtNombreApellidos
            // 
            this.txtNombreApellidos.Location = new System.Drawing.Point(366, 13);
            this.txtNombreApellidos.Name = "txtNombreApellidos";
            this.txtNombreApellidos.Properties.MaxLength = 50;
            this.txtNombreApellidos.Size = new System.Drawing.Size(327, 20);
            this.txtNombreApellidos.TabIndex = 126;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(260, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(100, 13);
            this.labelControl1.TabIndex = 125;
            this.labelControl1.Text = "Nombres y Apellidos:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(102, 13);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.MaxLength = 50;
            this.txtCodigo.Size = new System.Drawing.Size(140, 20);
            this.txtCodigo.TabIndex = 123;
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(7, 16);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(40, 13);
            this.labelControl18.TabIndex = 112;
            this.labelControl18.Text = "Codigo :";
            // 
            // lkpAreaProceso
            // 
            this.lkpAreaProceso.Location = new System.Drawing.Point(102, 79);
            this.lkpAreaProceso.Name = "lkpAreaProceso";
            this.lkpAreaProceso.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpAreaProceso.Properties.NullText = "";
            this.lkpAreaProceso.Size = new System.Drawing.Size(140, 20);
            this.lkpAreaProceso.TabIndex = 107;
            // 
            // lkpAreaEmpresa
            // 
            this.lkpAreaEmpresa.Location = new System.Drawing.Point(366, 57);
            this.lkpAreaEmpresa.Name = "lkpAreaEmpresa";
            this.lkpAreaEmpresa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpAreaEmpresa.Properties.NullText = "";
            this.lkpAreaEmpresa.Size = new System.Drawing.Size(140, 20);
            this.lkpAreaEmpresa.TabIndex = 105;
            this.lkpAreaEmpresa.EditValueChanged += new System.EventHandler(this.lkpAreaEmpresa_EditValueChanged);
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(6, 84);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(71, 13);
            this.labelControl20.TabIndex = 36;
            this.labelControl20.Text = "Area Proceso :";
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(260, 62);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(71, 13);
            this.labelControl21.TabIndex = 35;
            this.labelControl21.Text = "Area Empresa:";
            // 
            // labelControl23
            // 
            this.labelControl23.Location = new System.Drawing.Point(260, 85);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(36, 13);
            this.labelControl23.TabIndex = 34;
            this.labelControl23.Text = "Cargo :";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupBox2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(765, 155);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Datos";
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // lkpSituacion
            // 
            this.lkpSituacion.Location = new System.Drawing.Point(628, 101);
            this.lkpSituacion.Name = "lkpSituacion";
            this.lkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSituacion.Properties.NullText = "";
            this.lkpSituacion.Size = new System.Drawing.Size(122, 20);
            this.lkpSituacion.TabIndex = 141;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(532, 104);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(50, 13);
            this.labelControl4.TabIndex = 140;
            this.labelControl4.Text = "Situación :";
            // 
            // frmManteRegistroPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 183);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmManteRegistroPersonal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento - Registro del Personal";
            this.Load += new System.EventHandler(this.frmManteSeries_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaNacimiento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaNacimiento.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSubAreaEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCargo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpRelacionLaboral.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSexo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelefono.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDomicilio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDNI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreApellidos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpAreaProceso.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpAreaEmpresa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnGuardar;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        public DevExpress.XtraEditors.LookUpEdit lkpAreaProceso;
        public DevExpress.XtraEditors.LookUpEdit lkpAreaEmpresa;
        private DevExpress.XtraEditors.LabelControl labelControl20;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.DateEdit dteFechaNacimiento;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip menu;
        public DevExpress.XtraEditors.LookUpEdit lkpRelacionLaboral;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        public DevExpress.XtraEditors.LookUpEdit lkpSexo;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        public DevExpress.XtraEditors.TextEdit txtTelefono;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.TextEdit txtDomicilio;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.TextEdit txtDNI;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.TextEdit txtNombreApellidos;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtCodigo;
        public DevExpress.XtraEditors.LookUpEdit lkpCargo;
        public DevExpress.XtraEditors.LookUpEdit lkpSubAreaEmpresa;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.LookUpEdit lkpSituacion;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}