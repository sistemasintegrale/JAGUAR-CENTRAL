using Dapper;
using SGE.Entity;
using System.Data;

namespace SGE.DataAccesDapper.Modules
{
    public class AlmacenService
    {
        private readonly Conexion _conexion;
        public AlmacenService()
        {
            _conexion = new Conexion();
        }

        public int StockProductoPorAnio(int stocc_ianio, int stocc_iid_producto)
        {
            using (var conexion = _conexion.ObtenerConnexion())
            {
                var objParam = new DynamicParameters();
                objParam.Add("@stocc_ianio", stocc_ianio);
                objParam.Add("@stocc_iid_producto", stocc_iid_producto);

                return conexion.ExecuteScalar<int>("SGEV_STOCK_PRODUCTO_POR_ANIO", param: objParam, commandType: CommandType.StoredProcedure);

            }
        }

        public EProdProducto ProductoPvtGetByCode(string pr_vcodigo_externo)
        {
            using (var conexion = _conexion.ObtenerConnexion())
            {
                var objParam = new DynamicParameters();
                objParam.Add("@pr_vcodigo_externo", pr_vcodigo_externo);
                return conexion.QuerySingleOrDefault<EProdProducto>("SGE_PVT_PRODUCTO_GET_BY_CODIGO", param: objParam, commandType: CommandType.StoredProcedure);
            }

        }

    }
}
