﻿namespace SGE.WindowForms.Otros.Administracion_del_Sistema
{
    partial class frmFondo
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
            // frmFondo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImageStore = global::SGE.WindowForms.Properties.Resources.CALZADOS_JAGUAR_FONDO;
            this.ClientSize = new System.Drawing.Size(1100, 645);
            this.ControlBox = false;
            this.Name = "frmFondo";
            this.Text = "Bienvenido";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFondo_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
    }
}