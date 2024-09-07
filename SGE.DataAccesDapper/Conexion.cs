using SGE.DataAccess;
using System.Data.SqlClient;

namespace SGE.DataAccesDapper
{
    public class Conexion
    {
        private readonly string _connnectionString;

        public Conexion()
        {
            _connnectionString = Helper.conexion();
        }
        public SqlConnection ObtenerConnexion()
        {
            var conexion = new SqlConnection(_connnectionString);
            conexion.Open();
            return conexion;
        }
    }
}
