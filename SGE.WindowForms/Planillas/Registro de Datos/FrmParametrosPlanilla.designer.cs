namespace SGE.WindowForms.Planillas.Registro_de_Datos
{
    partial class FrmParametrosPlanilla
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
            this.SuspendLayout();
            // 
            // FrmParametrosPlanilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 0);
            this.Name = "FrmParametrosPlanilla";
            this.Text = "Registro de Parámetros Planillas";
            this.Load += new System.EventHandler(this.rptFrm01ParametroContable_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rptFrm01ParametroContable_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rptFrm01ParametroContable_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}