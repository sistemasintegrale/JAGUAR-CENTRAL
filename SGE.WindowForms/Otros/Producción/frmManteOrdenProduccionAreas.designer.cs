namespace SGE.WindowForms.Otros.Producción
{
    partial class frmManteOrdenProduccionAreas
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
            this.btnAceptar = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelar = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnLimpiarPersonal = new DevExpress.XtraEditors.SimpleButton();
            this.btePersonal = new DevExpress.XtraEditors.ButtonEdit();
            this.lkpProceso = new DevExpress.XtraEditors.LookUpEdit();
            this.dteFechaTerm = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cbVªBª = new System.Windows.Forms.CheckBox();
            this.cbTermino = new System.Windows.Forms.CheckBox();
            this.dteFechaAsg = new DevExpress.XtraEditors.DateEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btePersonal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpProceso.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaTerm.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaTerm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaAsg.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaAsg.Properties)).BeginInit();
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
            this.btnAceptar,
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAceptar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.btnAceptar.Caption = "Aceptar";
            this.btnAceptar.Id = 0;
            this.btnAceptar.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.doc_check;
            this.btnAceptar.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Enter);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAceptar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAceptar_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(452, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 132);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(452, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 132);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(452, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 132);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnLimpiarPersonal);
            this.groupControl1.Controls.Add(this.btePersonal);
            this.groupControl1.Controls.Add(this.lkpProceso);
            this.groupControl1.Controls.Add(this.dteFechaTerm);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.cbVªBª);
            this.groupControl1.Controls.Add(this.cbTermino);
            this.groupControl1.Controls.Add(this.dteFechaAsg);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(452, 132);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "Datos de Asignacion y Término de Tarea";
            this.groupControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.groupControl1_MouseMove);
            // 
            // btnLimpiarPersonal
            // 
            this.btnLimpiarPersonal.ImageOptions.Image = global::SGE.WindowForms.Properties.Resources.broom_icon;
            this.btnLimpiarPersonal.Location = new System.Drawing.Point(373, 33);
            this.btnLimpiarPersonal.Name = "btnLimpiarPersonal";
            this.btnLimpiarPersonal.Size = new System.Drawing.Size(24, 20);
            this.btnLimpiarPersonal.TabIndex = 144;
            this.btnLimpiarPersonal.Click += new System.EventHandler(this.btnLimpiarPersonal_Click);
            // 
            // btePersonal
            // 
            this.btePersonal.Location = new System.Drawing.Point(71, 33);
            this.btePersonal.Name = "btePersonal";
            this.btePersonal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btePersonal.Size = new System.Drawing.Size(301, 20);
            this.btePersonal.TabIndex = 143;
            this.btePersonal.Click += new System.EventHandler(this.btePersonal_Click);
            // 
            // lkpProceso
            // 
            this.lkpProceso.Location = new System.Drawing.Point(495, 109);
            this.lkpProceso.Name = "lkpProceso";
            this.lkpProceso.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpProceso.Properties.NullText = "";
            this.lkpProceso.Size = new System.Drawing.Size(102, 20);
            this.lkpProceso.TabIndex = 142;
            this.lkpProceso.Visible = false;
            // 
            // dteFechaTerm
            // 
            this.dteFechaTerm.EditValue = null;
            this.dteFechaTerm.Location = new System.Drawing.Point(284, 83);
            this.dteFechaTerm.Name = "dteFechaTerm";
            this.dteFechaTerm.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dteFechaTerm.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dteFechaTerm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFechaTerm.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFechaTerm.Size = new System.Drawing.Size(113, 20);
            this.dteFechaTerm.TabIndex = 141;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(196, 86);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(77, 13);
            this.labelControl3.TabIndex = 140;
            this.labelControl3.Text = "Fecha Termino :";
            // 
            // cbVªBª
            // 
            this.cbVªBª.AutoSize = true;
            this.cbVªBª.Location = new System.Drawing.Point(71, 108);
            this.cbVªBª.Name = "cbVªBª";
            this.cbVªBª.Size = new System.Drawing.Size(51, 17);
            this.cbVªBª.TabIndex = 139;
            this.cbVªBª.Text = "Vº Bº";
            this.cbVªBª.UseVisualStyleBackColor = true;
            // 
            // cbTermino
            // 
            this.cbTermino.AutoSize = true;
            this.cbTermino.Location = new System.Drawing.Point(71, 85);
            this.cbTermino.Name = "cbTermino";
            this.cbTermino.Size = new System.Drawing.Size(64, 17);
            this.cbTermino.TabIndex = 138;
            this.cbTermino.Text = "Termino";
            this.cbTermino.UseVisualStyleBackColor = true;
            this.cbTermino.CheckedChanged += new System.EventHandler(this.cbTermino_CheckedChanged);
            this.cbTermino.Click += new System.EventHandler(this.cbTermino_Click);
            // 
            // dteFechaAsg
            // 
            this.dteFechaAsg.EditValue = null;
            this.dteFechaAsg.Location = new System.Drawing.Point(111, 60);
            this.dteFechaAsg.Name = "dteFechaAsg";
            this.dteFechaAsg.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
            this.dteFechaAsg.Properties.AppearanceDisabled.Options.UseForeColor = true;
            this.dteFechaAsg.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFechaAsg.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFechaAsg.Size = new System.Drawing.Size(102, 20);
            this.dteFechaAsg.TabIndex = 117;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(436, 112);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(45, 13);
            this.labelControl6.TabIndex = 116;
            this.labelControl6.Text = "Proceso :";
            this.labelControl6.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 36);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 13);
            this.labelControl2.TabIndex = 108;
            this.labelControl2.Text = "Personal :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 63);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(90, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Fecha Asignación :";
            // 
            // frmManteOrdenProduccionAreas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 160);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(468, 150);
            this.Name = "frmManteOrdenProduccionAreas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento Orden Produccion Areas";
            this.Load += new System.EventHandler(this.frmManteFacturaDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btePersonal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpProceso.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaTerm.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaTerm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaAsg.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFechaAsg.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem btnAceptar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraBars.BarButtonItem btnCancelar;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.DateEdit dteFechaAsg;
        private System.Windows.Forms.CheckBox cbVªBª;
        private System.Windows.Forms.CheckBox cbTermino;
        private DevExpress.XtraEditors.DateEdit dteFechaTerm;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        public DevExpress.XtraEditors.LookUpEdit lkpProceso;
        public DevExpress.XtraEditors.ButtonEdit btePersonal;
        private DevExpress.XtraEditors.SimpleButton btnLimpiarPersonal;
    }
}