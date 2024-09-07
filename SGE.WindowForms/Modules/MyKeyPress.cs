using System;
using System.Windows.Forms;

namespace SGE.WindowForms.Modules
{
    public class MyKeyPress
    {
        public void MyKeyCounter(Object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= (char)48 && e.KeyChar <= (char)57 || e.KeyChar <= (char)8))
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
