using System;
using System.Security.Principal;

namespace SGE.Entity
{

    public class EAuditoria
    {
        public int intUsuario { get; set; }
        public string strPc { get; set; }
        public int anio { get; set; }
        public DateTime fechaAudit { get; set; }
        public EAuditoria()
        {
            strPc = WindowsIdentity.GetCurrent().Name;
            fechaAudit = DateTime.Now;
        }
    }
}
