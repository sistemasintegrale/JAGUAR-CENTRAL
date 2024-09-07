namespace SGE.WindowForms.Otros.Produccion
{
    partial class FrmManteModelo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManteModelo));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.BtnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.BtnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lkpSituacion = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lkpTipo = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.LkpLinea = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.lkpCategoria = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtdescripcion = new DevExpress.XtraEditors.TextEdit();
            this.txtcodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnIMportar = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.OpenFd = new System.Windows.Forms.OpenFileDialog();
            this.lkpSerie = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpLinea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCategoria.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdescripcion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSerie.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lkpSerie);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.lkpSituacion);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lkpTipo);
            this.groupControl1.Controls.Add(this.labelControl14);
            this.groupControl1.Controls.Add(this.LkpLinea);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.lkpCategoria);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtdescripcion);
            this.groupControl1.Controls.Add(this.txtcodigo);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(459, 111);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Datos";
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
            this.BtnGuardar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.BtnGuardar.Caption = "Guardar";
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnGuardar.ImageOptions.Image")));
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
            this.BtnCancelar.Border = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.BtnCancelar.Caption = "Cancelar";
            this.BtnCancelar.Id = 1;
            this.BtnCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancelar.ImageOptions.Image")));
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
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(459, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 354);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(459, 29);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 354);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(459, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 354);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(265, 88);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(31, 13);
            this.labelControl4.TabIndex = 49;
            this.labelControl4.Text = "Serie :";
            // 
            // lkpSituacion
            // 
            this.lkpSituacion.Location = new System.Drawing.Point(325, 29);
            this.lkpSituacion.Name = "lkpSituacion";
            this.lkpSituacion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSituacion.Properties.NullText = "";
            this.lkpSituacion.Size = new System.Drawing.Size(115, 20);
            this.lkpSituacion.TabIndex = 47;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(269, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 48;
            this.labelControl2.Text = "Situación :";
            // 
            // lkpTipo
            // 
            this.lkpTipo.Location = new System.Drawing.Point(110, 55);
            this.lkpTipo.Name = "lkpTipo";
            this.lkpTipo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTipo.Properties.NullText = "";
            this.lkpTipo.Size = new System.Drawing.Size(115, 20);
            this.lkpTipo.TabIndex = 38;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(77, 58);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(27, 13);
            this.labelControl14.TabIndex = 46;
            this.labelControl14.Text = "Tipo :";
            // 
            // LkpLinea
            // 
            this.LkpLinea.Location = new System.Drawing.Point(110, 85);
            this.LkpLinea.Name = "LkpLinea";
            this.LkpLinea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LkpLinea.Properties.NullText = "";
            this.LkpLinea.Size = new System.Drawing.Size(115, 20);
            this.LkpLinea.TabIndex = 34;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(62, 88);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(32, 13);
            this.labelControl7.TabIndex = 42;
            this.labelControl7.Text = "Linea :";
            // 
            // lkpCategoria
            // 
            this.lkpCategoria.Location = new System.Drawing.Point(325, 55);
            this.lkpCategoria.Name = "lkpCategoria";
            this.lkpCategoria.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpCategoria.Properties.NullText = "";
            this.lkpCategoria.Size = new System.Drawing.Size(115, 20);
            this.lkpCategoria.TabIndex = 33;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(265, 58);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(54, 13);
            this.labelControl5.TabIndex = 41;
            this.labelControl5.Text = "Categoria :";
            this.labelControl5.Click += new System.EventHandler(this.labelControl5_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 32);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(92, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Código de Modelo :";
            // 
            // txtdescripcion
            // 
            this.txtdescripcion.Enabled = false;
            this.txtdescripcion.Location = new System.Drawing.Point(110, 29);
            this.txtdescripcion.Name = "txtdescripcion";
            this.txtdescripcion.Properties.MaxLength = 50;
            this.txtdescripcion.Size = new System.Drawing.Size(115, 20);
            this.txtdescripcion.TabIndex = 1;
            this.txtdescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cerrar_form);
            // 
            // txtcodigo
            // 
            this.txtcodigo.EditValue = "000";
            this.txtcodigo.Enabled = false;
            this.txtcodigo.Location = new System.Drawing.Point(519, 84);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Properties.DisplayFormat.FormatString = "d4";
            this.txtcodigo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtcodigo.Properties.EditFormat.FormatString = "d4";
            this.txtcodigo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtcodigo.Properties.Mask.EditMask = "d4";
            this.txtcodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtcodigo.Properties.Mask.ShowPlaceHolders = false;
            this.txtcodigo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtcodigo.Size = new System.Drawing.Size(61, 20);
            this.txtcodigo.TabIndex = 0;
            this.txtcodigo.Visible = false;
            this.txtcodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cerrar_form);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(448, 88);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Código :";
            this.labelControl1.Visible = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.btnIMportar);
            this.groupControl3.Controls.Add(this.pictureEdit1);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 111);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(459, 243);
            this.groupControl3.TabIndex = 17;
            this.groupControl3.Text = "Subir Foto";
            // 
            // btnIMportar
            // 
            this.btnIMportar.Location = new System.Drawing.Point(117, 208);
            this.btnIMportar.Name = "btnIMportar";
            this.btnIMportar.Size = new System.Drawing.Size(275, 23);
            this.btnIMportar.TabIndex = 1;
            this.btnIMportar.Text = "Importar Imagen...";
            this.btnIMportar.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(117, 25);
            this.pictureEdit1.MenuManager = this.barManager1;
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(275, 181);
            this.pictureEdit1.TabIndex = 0;
            // 
            // OpenFd
            // 
            this.OpenFd.FileName = "openFileDialog1";
            // 
            // lkpSerie
            // 
            this.lkpSerie.Location = new System.Drawing.Point(325, 85);
            this.lkpSerie.Name = "lkpSerie";
            this.lkpSerie.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpSerie.Properties.NullText = "";
            this.lkpSerie.Size = new System.Drawing.Size(115, 20);
            this.lkpSerie.TabIndex = 50;
            // 
            // FrmManteModelo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 383);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmManteModelo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modelo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManteMotonave_FormClosing);
            this.Load += new System.EventHandler(this.FrmManteModelo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSituacion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTipo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LkpLinea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCategoria.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdescripcion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpSerie.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private DevExpress.XtraEditors.GroupControl groupControl3;
        public DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private System.Windows.Forms.OpenFileDialog OpenFd;
        public DevExpress.XtraEditors.LookUpEdit lkpTipo;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        public DevExpress.XtraEditors.LookUpEdit LkpLinea;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.LookUpEdit lkpCategoria;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.SimpleButton btnIMportar;
        public DevExpress.XtraEditors.LookUpEdit lkpSituacion;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.LookUpEdit lkpSerie;
    }
}