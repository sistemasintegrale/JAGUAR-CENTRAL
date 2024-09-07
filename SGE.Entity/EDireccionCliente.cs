namespace SGE.Entity
{
    public class EDireccionCliente : EAuditoria
    {
        public int dc_icod_direccion { get; set; }
        public int cliec_icod_cliente { get; set; }
        public string dc_vdireccion { get; set; }
    }
}
