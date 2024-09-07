namespace SGE.WindowForms.Ventas.Reporte
{
    partial class rptFichaTecnicaGRUPO
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.lblNumero = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCodigoMat = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPar = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMaterial = new DevExpress.XtraReports.UI.XRLabel();
            this.lblArea = new DevExpress.XtraReports.UI.XRLabel();
            this.lblUM = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.xpPageSelector1 = new DevExpress.Xpo.XPPageSelector(this.components);
            this.xpPageSelector2 = new DevExpress.Xpo.XPPageSelector(this.components);
            this.xpPageSelector3 = new DevExpress.Xpo.XPPageSelector(this.components);
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.lblNroDocumento = new DevExpress.XtraReports.UI.XRLabel();
            this.TipoDocumento = new DevExpress.XtraReports.UI.XRLabel();
            this.lblEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSerie = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblModelo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblColor = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTipoTrabajo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblLinea = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel40 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTipo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel41 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel45 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.ptrimagen = new DevExpress.XtraReports.UI.XRPictureBox();
            this.lblobservaciones = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.BackColor = System.Drawing.Color.Transparent;
            this.Detail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail.BorderWidth = 3F;
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNumero,
            this.lblCodigoMat,
            this.lblPar,
            this.lblMaterial,
            this.lblArea,
            this.lblUM});
            this.Detail.HeightF = 15.28486F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseBackColor = false;
            this.Detail.StylePriority.UseBorderWidth = false;
            this.Detail.StylePriority.UseTextAlignment = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // lblNumero
            // 
            this.lblNumero.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.lblNumero.BorderWidth = 1F;
            this.lblNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblNumero.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNumero.SizeF = new System.Drawing.SizeF(42.83334F, 15.28485F);
            this.lblNumero.StylePriority.UseBorders = false;
            this.lblNumero.StylePriority.UseBorderWidth = false;
            this.lblNumero.StylePriority.UseFont = false;
            this.lblNumero.StylePriority.UseTextAlignment = false;
            this.lblNumero.Text = "78";
            this.lblNumero.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.lblNumero.WordWrap = false;
            // 
            // lblCodigoMat
            // 
            this.lblCodigoMat.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.lblCodigoMat.BorderWidth = 1F;
            this.lblCodigoMat.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblCodigoMat.LocationFloat = new DevExpress.Utils.PointFloat(152.7562F, 0F);
            this.lblCodigoMat.Name = "lblCodigoMat";
            this.lblCodigoMat.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblCodigoMat.SizeF = new System.Drawing.SizeF(99.85658F, 15.28485F);
            this.lblCodigoMat.StylePriority.UseBorders = false;
            this.lblCodigoMat.StylePriority.UseBorderWidth = false;
            this.lblCodigoMat.StylePriority.UseFont = false;
            this.lblCodigoMat.StylePriority.UseTextAlignment = false;
            this.lblCodigoMat.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblCodigoMat.WordWrap = false;
            // 
            // lblPar
            // 
            this.lblPar.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.lblPar.BorderWidth = 1F;
            this.lblPar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblPar.LocationFloat = new DevExpress.Utils.PointFloat(588.8948F, 0F);
            this.lblPar.Name = "lblPar";
            this.lblPar.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblPar.SizeF = new System.Drawing.SizeF(64.61438F, 15.28485F);
            this.lblPar.StylePriority.UseBorders = false;
            this.lblPar.StylePriority.UseBorderWidth = false;
            this.lblPar.StylePriority.UseFont = false;
            this.lblPar.StylePriority.UseTextAlignment = false;
            this.lblPar.Text = "12.00";
            this.lblPar.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblPar.WordWrap = false;
            // 
            // lblMaterial
            // 
            this.lblMaterial.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.lblMaterial.BorderWidth = 1F;
            this.lblMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblMaterial.LocationFloat = new DevExpress.Utils.PointFloat(252.6128F, 0F);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblMaterial.SizeF = new System.Drawing.SizeF(336.2819F, 15.28485F);
            this.lblMaterial.StylePriority.UseBorders = false;
            this.lblMaterial.StylePriority.UseBorderWidth = false;
            this.lblMaterial.StylePriority.UseFont = false;
            this.lblMaterial.StylePriority.UseTextAlignment = false;
            this.lblMaterial.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblMaterial.WordWrap = false;
            // 
            // lblArea
            // 
            this.lblArea.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.lblArea.BorderWidth = 1F;
            this.lblArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblArea.LocationFloat = new DevExpress.Utils.PointFloat(42.83334F, 0F);
            this.lblArea.Name = "lblArea";
            this.lblArea.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblArea.SizeF = new System.Drawing.SizeF(109.9229F, 15.28485F);
            this.lblArea.StylePriority.UseBorders = false;
            this.lblArea.StylePriority.UseBorderWidth = false;
            this.lblArea.StylePriority.UseFont = false;
            this.lblArea.StylePriority.UseTextAlignment = false;
            this.lblArea.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblArea.WordWrap = false;
            // 
            // lblUM
            // 
            this.lblUM.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.lblUM.BorderWidth = 1F;
            this.lblUM.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.lblUM.LocationFloat = new DevExpress.Utils.PointFloat(678.0335F, 0F);
            this.lblUM.Name = "lblUM";
            this.lblUM.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblUM.SizeF = new System.Drawing.SizeF(100.9665F, 15.28485F);
            this.lblUM.StylePriority.UseBorders = false;
            this.lblUM.StylePriority.UseBorderWidth = false;
            this.lblUM.StylePriority.UseFont = false;
            this.lblUM.StylePriority.UseTextAlignment = false;
            this.lblUM.Text = "PIE";
            this.lblUM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblUM.WordWrap = false;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 22F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseForeColor = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 27.24552F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1,
            this.xrLabel34,
            this.lblSerie,
            this.xrLabel11,
            this.xrLabel7,
            this.lblModelo,
            this.xrLabel3,
            this.xrLabel14,
            this.lblColor,
            this.xrLabel8,
            this.xrLabel2,
            this.lblTipoTrabajo,
            this.xrLabel16,
            this.lblLinea,
            this.xrLabel19,
            this.xrLabel40,
            this.xrLabel24,
            this.lblTipo,
            this.xrLabel36,
            this.xrLabel41,
            this.xrLabel45,
            this.xrLabel20,
            this.xrLabel17,
            this.xrLabel1,
            this.xrLabel6});
            this.ReportHeader.HeightF = 135.2917F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.BorderWidth = 2F;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblNroDocumento,
            this.TipoDocumento,
            this.lblEmpresa});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(529.624F, 10.00001F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(239.376F, 85.79324F);
            this.xrPanel1.StylePriority.UseBorders = false;
            this.xrPanel1.StylePriority.UseBorderWidth = false;
            // 
            // lblNroDocumento
            // 
            this.lblNroDocumento.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblNroDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNroDocumento.LocationFloat = new DevExpress.Utils.PointFloat(10F, 52.48687F);
            this.lblNroDocumento.Name = "lblNroDocumento";
            this.lblNroDocumento.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblNroDocumento.SizeF = new System.Drawing.SizeF(215.712F, 22.17177F);
            this.lblNroDocumento.StylePriority.UseBorders = false;
            this.lblNroDocumento.StylePriority.UseFont = false;
            this.lblNroDocumento.StylePriority.UseTextAlignment = false;
            this.lblNroDocumento.Text = "FT-0000000";
            this.lblNroDocumento.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblNroDocumento.WordWrap = false;
            // 
            // TipoDocumento
            // 
            this.TipoDocumento.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.TipoDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.TipoDocumento.LocationFloat = new DevExpress.Utils.PointFloat(10.00003F, 29.94153F);
            this.TipoDocumento.Name = "TipoDocumento";
            this.TipoDocumento.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.TipoDocumento.SizeF = new System.Drawing.SizeF(215.7119F, 20.637F);
            this.TipoDocumento.StylePriority.UseBorders = false;
            this.TipoDocumento.StylePriority.UseFont = false;
            this.TipoDocumento.StylePriority.UseTextAlignment = false;
            this.TipoDocumento.Text = "FICHA TECNICA";
            this.TipoDocumento.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.TipoDocumento.WordWrap = false;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(10F, 10.00001F);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblEmpresa.SizeF = new System.Drawing.SizeF(215.712F, 19.94152F);
            this.lblEmpresa.StylePriority.UseBorders = false;
            this.lblEmpresa.StylePriority.UseFont = false;
            this.lblEmpresa.WordWrap = false;
            // 
            // xrLabel34
            // 
            this.xrLabel34.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel34.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(112.3302F, 39.99993F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(12.47003F, 13.47687F);
            this.xrLabel34.StylePriority.UseBorders = false;
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.StylePriority.UseTextAlignment = false;
            this.xrLabel34.Text = ":";
            this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel34.WordWrap = false;
            // 
            // lblSerie
            // 
            this.lblSerie.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblSerie.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerie.LocationFloat = new DevExpress.Utils.PointFloat(124.7998F, 40.00023F);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblSerie.SizeF = new System.Drawing.SizeF(207.0416F, 13.47656F);
            this.lblSerie.StylePriority.UseBorders = false;
            this.lblSerie.StylePriority.UseFont = false;
            this.lblSerie.WordWrap = false;
            // 
            // xrLabel11
            // 
            this.xrLabel11.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel11.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(2.000364F, 39.9999F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(69.85553F, 13.47688F);
            this.xrLabel11.StylePriority.UseBorders = false;
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.Text = "Serie";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel7.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(2.000364F, 10.00001F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(69.85554F, 15F);
            this.xrLabel7.StylePriority.UseBorders = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.Text = "Modelo";
            // 
            // lblModelo
            // 
            this.lblModelo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblModelo.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Bold);
            this.lblModelo.LocationFloat = new DevExpress.Utils.PointFloat(124.7998F, 10.00002F);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblModelo.SizeF = new System.Drawing.SizeF(207.0415F, 14.99997F);
            this.lblModelo.StylePriority.UseBorders = false;
            this.lblModelo.StylePriority.UseFont = false;
            this.lblModelo.WordWrap = false;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel3.Font = new System.Drawing.Font("Courier New", 10F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(112.3302F, 10.00004F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(12.46964F, 14.99998F);
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = ":";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel3.WordWrap = false;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel14.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(2.000364F, 24.9999F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(69.85553F, 15F);
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.Text = "Color";
            // 
            // lblColor
            // 
            this.lblColor.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblColor.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColor.LocationFloat = new DevExpress.Utils.PointFloat(124.7998F, 24.9999F);
            this.lblColor.Name = "lblColor";
            this.lblColor.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblColor.SizeF = new System.Drawing.SizeF(207.0416F, 15F);
            this.lblColor.StylePriority.UseBorders = false;
            this.lblColor.StylePriority.UseFont = false;
            this.lblColor.WordWrap = false;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel8.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(112.3302F, 24.99992F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(12.46964F, 14.99998F);
            this.xrLabel8.StylePriority.UseBorders = false;
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = ":";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel8.WordWrap = false;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(112.3302F, 66.41673F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(12.46964F, 14.99991F);
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = ":";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel2.WordWrap = false;
            // 
            // lblTipoTrabajo
            // 
            this.lblTipoTrabajo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblTipoTrabajo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoTrabajo.LocationFloat = new DevExpress.Utils.PointFloat(124.7998F, 66.41651F);
            this.lblTipoTrabajo.Name = "lblTipoTrabajo";
            this.lblTipoTrabajo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTipoTrabajo.SizeF = new System.Drawing.SizeF(207.0415F, 15.00014F);
            this.lblTipoTrabajo.StylePriority.UseBorders = false;
            this.lblTipoTrabajo.StylePriority.UseFont = false;
            this.lblTipoTrabajo.WordWrap = false;
            // 
            // xrLabel16
            // 
            this.xrLabel16.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel16.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(2.000364F, 66.41674F);
            this.xrLabel16.Multiline = true;
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(110.3298F, 15F);
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.Text = "Tipo Trabajo";
            // 
            // lblLinea
            // 
            this.lblLinea.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblLinea.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinea.LocationFloat = new DevExpress.Utils.PointFloat(124.7998F, 53.47678F);
            this.lblLinea.Name = "lblLinea";
            this.lblLinea.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblLinea.SizeF = new System.Drawing.SizeF(207.0415F, 12.94003F);
            this.lblLinea.StylePriority.UseBorders = false;
            this.lblLinea.StylePriority.UseFont = false;
            this.lblLinea.WordWrap = false;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(1.999792F, 53.47678F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(110.3304F, 12.93996F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.Text = "Linea";
            // 
            // xrLabel40
            // 
            this.xrLabel40.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel40.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel40.LocationFloat = new DevExpress.Utils.PointFloat(112.3302F, 53.47678F);
            this.xrLabel40.Name = "xrLabel40";
            this.xrLabel40.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel40.SizeF = new System.Drawing.SizeF(12.46964F, 12.93994F);
            this.xrLabel40.StylePriority.UseBorders = false;
            this.xrLabel40.StylePriority.UseFont = false;
            this.xrLabel40.StylePriority.UseTextAlignment = false;
            this.xrLabel40.Text = ":";
            this.xrLabel40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel40.WordWrap = false;
            // 
            // xrLabel24
            // 
            this.xrLabel24.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel24.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(112.3302F, 81.41664F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel24.SizeF = new System.Drawing.SizeF(12.46964F, 13.4758F);
            this.xrLabel24.StylePriority.UseBorders = false;
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            this.xrLabel24.Text = ":";
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrLabel24.WordWrap = false;
            // 
            // lblTipo
            // 
            this.lblTipo.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblTipo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipo.LocationFloat = new DevExpress.Utils.PointFloat(124.7998F, 82.31745F);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblTipo.SizeF = new System.Drawing.SizeF(207.0415F, 13.4758F);
            this.lblTipo.StylePriority.UseBorders = false;
            this.lblTipo.StylePriority.UseFont = false;
            this.lblTipo.WordWrap = false;
            // 
            // xrLabel36
            // 
            this.xrLabel36.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrLabel36.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(2.000364F, 81.41673F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(110.3298F, 13.47569F);
            this.xrLabel36.StylePriority.UseBorders = false;
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.Text = "Tipo";
            // 
            // xrLabel41
            // 
            this.xrLabel41.BackColor = System.Drawing.Color.Yellow;
            this.xrLabel41.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel41.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel41.LocationFloat = new DevExpress.Utils.PointFloat(42.83333F, 112.5418F);
            this.xrLabel41.Name = "xrLabel41";
            this.xrLabel41.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel41.SizeF = new System.Drawing.SizeF(109.9229F, 22.74982F);
            this.xrLabel41.StylePriority.UseBackColor = false;
            this.xrLabel41.StylePriority.UseBorders = false;
            this.xrLabel41.StylePriority.UseFont = false;
            this.xrLabel41.StylePriority.UseTextAlignment = false;
            this.xrLabel41.Text = "AREA";
            this.xrLabel41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel45
            // 
            this.xrLabel45.BackColor = System.Drawing.Color.Yellow;
            this.xrLabel45.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel45.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel45.LocationFloat = new DevExpress.Utils.PointFloat(152.7562F, 112.5418F);
            this.xrLabel45.Name = "xrLabel45";
            this.xrLabel45.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel45.SizeF = new System.Drawing.SizeF(99.85659F, 22.74982F);
            this.xrLabel45.StylePriority.UseBackColor = false;
            this.xrLabel45.StylePriority.UseBorders = false;
            this.xrLabel45.StylePriority.UseFont = false;
            this.xrLabel45.StylePriority.UseTextAlignment = false;
            this.xrLabel45.Text = "DESTINO";
            this.xrLabel45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel20
            // 
            this.xrLabel20.BackColor = System.Drawing.Color.Yellow;
            this.xrLabel20.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel20.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(678.0335F, 112.5417F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(100.9665F, 22.74985F);
            this.xrLabel20.StylePriority.UseBackColor = false;
            this.xrLabel20.StylePriority.UseBorders = false;
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            this.xrLabel20.Text = "U.MEDIDA";
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel17
            // 
            this.xrLabel17.BackColor = System.Drawing.Color.Yellow;
            this.xrLabel17.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel17.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(588.8948F, 112.5417F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(89.13861F, 22.74982F);
            this.xrLabel17.StylePriority.UseBackColor = false;
            this.xrLabel17.StylePriority.UseBorders = false;
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "C.X.PAR";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.Yellow;
            this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(252.6128F, 112.5417F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(336.282F, 22.74982F);
            this.xrLabel1.StylePriority.UseBackColor = false;
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "MATERIAL";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel6
            // 
            this.xrLabel6.BackColor = System.Drawing.Color.Yellow;
            this.xrLabel6.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel6.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 112.5418F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(42.83334F, 22.74982F);
            this.xrLabel6.StylePriority.UseBackColor = false;
            this.xrLabel6.StylePriority.UseBorders = false;
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "N°";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.HeightF = 0.7916768F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine1});
            this.GroupFooter1.HeightF = 2.083333F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrLine1
            // 
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(779F, 2.083333F);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.ptrimagen,
            this.lblobservaciones,
            this.xrLabel5});
            this.ReportFooter.HeightF = 409.2985F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // ptrimagen
            // 
            this.ptrimagen.LocationFloat = new DevExpress.Utils.PointFloat(124.7998F, 10.00001F);
            this.ptrimagen.Name = "ptrimagen";
            this.ptrimagen.SizeF = new System.Drawing.SizeF(504F, 320F);
            this.ptrimagen.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // lblobservaciones
            // 
            this.lblobservaciones.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.lblobservaciones.Font = new System.Drawing.Font("Courier New", 10F);
            this.lblobservaciones.LocationFloat = new DevExpress.Utils.PointFloat(152.7563F, 379.3402F);
            this.lblobservaciones.Multiline = true;
            this.lblobservaciones.Name = "lblobservaciones";
            this.lblobservaciones.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lblobservaciones.SizeF = new System.Drawing.SizeF(449.6803F, 14.99997F);
            this.lblobservaciones.StylePriority.UseBorders = false;
            this.lblobservaciones.StylePriority.UseFont = false;
            this.lblobservaciones.StylePriority.UseTextAlignment = false;
            this.lblobservaciones.Text = "QUEMAR SELLO R18 AL FINAL DE LA PRIMERA TIRA";
            this.lblobservaciones.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
            this.lblobservaciones.WordWrap = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel5.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(152.7562F, 353.9236F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(449.6804F, 25.41663F);
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "INDICACIONES PARA EL TRABAJO";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrLabel5.WordWrap = false;
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = null;
            // 
            // rptFichaTecnicaGRUPO
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.GroupFooter1,
            this.ReportFooter});
            this.BookmarkDuplicateSuppress = false;
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.bindingSource1,
            this.bindingSource2});
            this.DataSource = this.xpPageSelector1;
            this.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.Margins = new System.Drawing.Printing.Margins(14, 34, 22, 27);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "19.2";
            this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.XRLabel lblCodigoMat;
        private DevExpress.XtraReports.UI.XRLabel lblPar;
        private DevExpress.XtraReports.UI.XRLabel lblMaterial;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel lblUM;
        private DevExpress.XtraReports.UI.XRLabel lblNumero;
        private DevExpress.Xpo.XPPageSelector xpPageSelector1;
        private DevExpress.Xpo.XPPageSelector xpPageSelector2;
        private DevExpress.Xpo.XPPageSelector xpPageSelector3;
        private DevExpress.XtraReports.UI.XRLabel lblArea;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel41;
        private DevExpress.XtraReports.UI.XRLabel xrLabel45;
        private DevExpress.XtraReports.UI.XRLabel xrLabel20;
        private DevExpress.XtraReports.UI.XRLabel xrLabel17;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRLabel lblNroDocumento;
        private DevExpress.XtraReports.UI.XRLabel TipoDocumento;
        private DevExpress.XtraReports.UI.XRLabel lblEmpresa;
        private DevExpress.XtraReports.UI.XRLabel xrLabel34;
        private DevExpress.XtraReports.UI.XRLabel lblSerie;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel lblModelo;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel14;
        private DevExpress.XtraReports.UI.XRLabel lblColor;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel lblTipoTrabajo;
        private DevExpress.XtraReports.UI.XRLabel xrLabel16;
        private DevExpress.XtraReports.UI.XRLabel lblLinea;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRLabel xrLabel40;
        private DevExpress.XtraReports.UI.XRLabel xrLabel24;
        private DevExpress.XtraReports.UI.XRLabel lblTipo;
        private DevExpress.XtraReports.UI.XRLabel xrLabel36;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel lblobservaciones;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRPictureBox ptrimagen;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.BindingSource bindingSource2;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
    }
}
