namespace SGE.WindowForms.Otros.Producción
{
    partial class frmManteParametroProduccion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManteParametroProduccion));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.BtnGuardar = new DevExpress.XtraBars.BarButtonItem();
            this.BtnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtNotaPedido = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtRuta = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtOrdenProduccion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNroOrdenPedido = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtNroFichaTecnica = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtRutaBase = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtRutaFichaTecnica = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtRutaOrdenPedido = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRutaOrdenProduccion = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtRutaModelo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotaPedido.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrdenProduccion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroOrdenPedido.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroFichaTecnica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaBase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaFichaTecnica.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaOrdenPedido.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaOrdenProduccion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaModelo.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.BtnGuardar.Hint = "Guardar";
            this.BtnGuardar.Id = 0;
            this.BtnGuardar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_save;
            this.BtnGuardar.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.BtnGuardar.ItemAppearance.Normal.Options.UseFont = true;
            this.BtnGuardar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.BtnGuardar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGuardar_ItemClick);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Caption = "Cancelar";
            this.BtnCancelar.Hint = "Cancelar";
            this.BtnCancelar.Id = 1;
            this.BtnCancelar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_exit;
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
            this.barDockControlTop.Size = new System.Drawing.Size(456, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 365);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(456, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 365);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(456, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 365);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupBox2);
            this.groupControl1.Controls.Add(this.groupBox1);
            this.groupControl1.Controls.Add(this.txtRuta);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(456, 365);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Variables";
            // 
            // txtNotaPedido
            // 
            this.txtNotaPedido.EditValue = "";
            this.txtNotaPedido.Location = new System.Drawing.Point(130, 43);
            this.txtNotaPedido.Name = "txtNotaPedido";
            this.txtNotaPedido.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNotaPedido.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNotaPedido.Size = new System.Drawing.Size(223, 20);
            this.txtNotaPedido.TabIndex = 40;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 46);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(95, 13);
            this.labelControl5.TabIndex = 41;
            this.labelControl5.Text = "Nº Nota de Pedido :";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(993, 181);
            this.txtRuta.MenuManager = this.barManager1;
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Properties.MaxLength = 400;
            this.txtRuta.Size = new System.Drawing.Size(201, 20);
            this.txtRuta.TabIndex = 39;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(908, 181);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(66, 13);
            this.labelControl4.TabIndex = 23;
            this.labelControl4.Text = "Ruta Imagen:";
            // 
            // txtOrdenProduccion
            // 
            this.txtOrdenProduccion.EditValue = "";
            this.txtOrdenProduccion.Location = new System.Drawing.Point(130, 95);
            this.txtOrdenProduccion.Name = "txtOrdenProduccion";
            this.txtOrdenProduccion.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtOrdenProduccion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOrdenProduccion.Size = new System.Drawing.Size(223, 20);
            this.txtOrdenProduccion.TabIndex = 20;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 98);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(104, 13);
            this.labelControl3.TabIndex = 21;
            this.labelControl3.Text = "Nº Orden Producción:";
            // 
            // txtNroOrdenPedido
            // 
            this.txtNroOrdenPedido.EditValue = "";
            this.txtNroOrdenPedido.Location = new System.Drawing.Point(130, 69);
            this.txtNroOrdenPedido.Name = "txtNroOrdenPedido";
            this.txtNroOrdenPedido.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNroOrdenPedido.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNroOrdenPedido.Size = new System.Drawing.Size(223, 20);
            this.txtNroOrdenPedido.TabIndex = 18;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 72);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(81, 13);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "NªOrden Pedido:";
            // 
            // txtNroFichaTecnica
            // 
            this.txtNroFichaTecnica.EditValue = "";
            this.txtNroFichaTecnica.Location = new System.Drawing.Point(130, 17);
            this.txtNroFichaTecnica.Name = "txtNroFichaTecnica";
            this.txtNroFichaTecnica.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtNroFichaTecnica.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNroFichaTecnica.Size = new System.Drawing.Size(223, 20);
            this.txtNroFichaTecnica.TabIndex = 16;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 13);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "Nº Ficha Tecnica:";
            // 
            // txtRutaBase
            // 
            this.txtRutaBase.Location = new System.Drawing.Point(129, 28);
            this.txtRutaBase.MenuManager = this.barManager1;
            this.txtRutaBase.Name = "txtRutaBase";
            this.txtRutaBase.Properties.MaxLength = 400;
            this.txtRutaBase.Size = new System.Drawing.Size(224, 20);
            this.txtRutaBase.TabIndex = 43;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(20, 31);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(56, 13);
            this.labelControl6.TabIndex = 42;
            this.labelControl6.Text = "Ruta Base :";
            // 
            // txtRutaFichaTecnica
            // 
            this.txtRutaFichaTecnica.Location = new System.Drawing.Point(129, 54);
            this.txtRutaFichaTecnica.MenuManager = this.barManager1;
            this.txtRutaFichaTecnica.Name = "txtRutaFichaTecnica";
            this.txtRutaFichaTecnica.Properties.MaxLength = 400;
            this.txtRutaFichaTecnica.Size = new System.Drawing.Size(224, 20);
            this.txtRutaFichaTecnica.TabIndex = 45;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(20, 57);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(71, 13);
            this.labelControl7.TabIndex = 44;
            this.labelControl7.Text = "Ficha Técnica :";
            // 
            // txtRutaOrdenPedido
            // 
            this.txtRutaOrdenPedido.Location = new System.Drawing.Point(129, 80);
            this.txtRutaOrdenPedido.MenuManager = this.barManager1;
            this.txtRutaOrdenPedido.Name = "txtRutaOrdenPedido";
            this.txtRutaOrdenPedido.Properties.MaxLength = 400;
            this.txtRutaOrdenPedido.Size = new System.Drawing.Size(224, 20);
            this.txtRutaOrdenPedido.TabIndex = 47;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(20, 83);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(72, 13);
            this.labelControl8.TabIndex = 46;
            this.labelControl8.Text = "Orden Pedido :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRutaModelo);
            this.groupBox1.Controls.Add(this.labelControl10);
            this.groupBox1.Controls.Add(this.txtRutaOrdenProduccion);
            this.groupBox1.Controls.Add(this.labelControl9);
            this.groupBox1.Controls.Add(this.txtRutaBase);
            this.groupBox1.Controls.Add(this.txtRutaOrdenPedido);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.labelControl8);
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.txtRutaFichaTecnica);
            this.groupBox1.Location = new System.Drawing.Point(27, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 160);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Almacenanmiento Nube :";
            // 
            // txtRutaOrdenProduccion
            // 
            this.txtRutaOrdenProduccion.Location = new System.Drawing.Point(129, 106);
            this.txtRutaOrdenProduccion.MenuManager = this.barManager1;
            this.txtRutaOrdenProduccion.Name = "txtRutaOrdenProduccion";
            this.txtRutaOrdenProduccion.Properties.MaxLength = 400;
            this.txtRutaOrdenProduccion.Size = new System.Drawing.Size(224, 20);
            this.txtRutaOrdenProduccion.TabIndex = 49;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(20, 109);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(92, 13);
            this.labelControl9.TabIndex = 48;
            this.labelControl9.Text = "Orden Producción :";
            // 
            // txtRutaModelo
            // 
            this.txtRutaModelo.Location = new System.Drawing.Point(129, 132);
            this.txtRutaModelo.MenuManager = this.barManager1;
            this.txtRutaModelo.Name = "txtRutaModelo";
            this.txtRutaModelo.Properties.MaxLength = 400;
            this.txtRutaModelo.Size = new System.Drawing.Size(224, 20);
            this.txtRutaModelo.TabIndex = 51;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(20, 135);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(41, 13);
            this.labelControl10.TabIndex = 50;
            this.labelControl10.Text = "Modelo :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Controls.Add(this.txtNroFichaTecnica);
            this.groupBox2.Controls.Add(this.txtNotaPedido);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.txtNroOrdenPedido);
            this.groupBox2.Controls.Add(this.labelControl3);
            this.groupBox2.Controls.Add(this.txtOrdenProduccion);
            this.groupBox2.Location = new System.Drawing.Point(27, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 132);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Correlativos :";
            // 
            // frmManteParametroProduccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 393);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmManteParametroProduccion.IconOptions.Icon")));
            this.Name = "frmManteParametroProduccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SIG - Variables";
            this.Load += new System.EventHandler(this.FrmVariables_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotaPedido.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRuta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrdenProduccion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroOrdenPedido.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNroFichaTecnica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaBase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaFichaTecnica.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaOrdenPedido.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaOrdenProduccion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRutaModelo.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        public DevExpress.XtraEditors.TextEdit txtNroFichaTecnica;
        public DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txtNroOrdenPedido;
        public DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txtOrdenProduccion;
        public DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit txtRuta;
        public DevExpress.XtraEditors.TextEdit txtNotaPedido;
        public DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.GroupBox groupBox1;
        public DevExpress.XtraEditors.TextEdit txtRutaOrdenProduccion;
        public DevExpress.XtraEditors.LabelControl labelControl9;
        public DevExpress.XtraEditors.TextEdit txtRutaBase;
        public DevExpress.XtraEditors.TextEdit txtRutaOrdenPedido;
        public DevExpress.XtraEditors.LabelControl labelControl6;
        public DevExpress.XtraEditors.LabelControl labelControl8;
        public DevExpress.XtraEditors.LabelControl labelControl7;
        public DevExpress.XtraEditors.TextEdit txtRutaFichaTecnica;
        public DevExpress.XtraEditors.TextEdit txtRutaModelo;
        public DevExpress.XtraEditors.LabelControl labelControl10;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}