using Dapper;
using SGE.Entity;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGE.DataAccesDapper.Modules
{
    public class CentralService
    {
        private readonly Conexion _conexion;
        public CentralService()
        {
            _conexion = new Conexion();
        }

        public List<EProdProducto> ProductoPorAlmacenListar(int stocc_iid_almacen, int intanio)
        {
            using (var conexion = _conexion.ObtenerConnexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("stocc_iid_almacen", stocc_iid_almacen);
                parametros.Add("intEjercicio", intanio);
                return conexion.Query<EProdProducto>("SGE_STOCK_PRODUCTO_LISTAR_PRODUCTOS_X_ALMACEN", parametros, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<EProdProducto> ProductoListar()
        {
            using (var conexion = _conexion.ObtenerConnexion())
            {
                return conexion.Query<EProdProducto>("SGECE_PVT_PRODUCTOS_LISTAR", commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<EProdProducto> ProductoListarPorMarca(int pr_tarec_icorrelativo)
        {
            using (var conexion = _conexion.ObtenerConnexion())
            {
                var parametros = new DynamicParameters();
                parametros.Add("@pr_tarec_icorrelativo", pr_tarec_icorrelativo);
                return conexion.Query<EProdProducto>("SGECE_PVT_PRODUCTOS_LISTAR_POR_MARCA", parametros, commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        public List<EProdModelo> ModeloListar(int Marca)
        {
            string where = string.Empty;
            if (Marca != 0)
            {
                //where = $"and tarec_icorrelativo = {Marca}";
            }
            using (var conexion = _conexion.ObtenerConnexion())
            {
                return conexion.Query<EProdModelo>($"select * from SGE_PVT_MODELO where mo_estado = 1 {where}").ToList();
            }
        }

        public int UltimoCorrelativoTabla(string NombreTabla)
        {
            using (var conexion = _conexion.ObtenerConnexion())
            {
                var objParam = new DynamicParameters();
                objParam.Add("@tabla", NombreTabla);
                int codigo = conexion.ExecuteScalar<int>("SGEA_OBTENER_ULTIMO_CORRELATIVO_TABLA", param: objParam, commandType: CommandType.StoredProcedure);
                return ++codigo;
            }
        }

    }
}
