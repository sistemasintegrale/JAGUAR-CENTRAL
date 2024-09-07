namespace SGE.WindowForms.Sistema
{
    partial class FrmEnviarInformacion
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
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(139, 0);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(100, 18);
            this.marqueeProgressBarControl1.TabIndex = 0;
            this.marqueeProgressBarControl1.EditValueChanged += new System.EventHandler(this.marqueeProgressBarControl1_EditValueChanged);
            // 
            // FrmEnviarInformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 1);
            this.Controls.Add(this.marqueeProgressBarControl1);
            this.Name = "FrmEnviarInformacion";
            this.Text = "PVC- Enviar Información";
            this.Load += new System.EventHandler(this.FrmEnviarInformacion_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrmEnviarInformacion_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmEnviarInformacion_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
    }
}