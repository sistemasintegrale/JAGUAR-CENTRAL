using SGE.WindowForms.UI;
using System;
using System.Windows.Forms;

namespace SGE.WindowForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSystemLogin());
        }
    }
}
