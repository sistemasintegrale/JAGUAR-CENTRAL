using SGE.Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SGE.DataAccess
{
    public class DocumentoEMysqlData
    {

        //public bool InsertarDocumento(EDocumentoElectronicoMysql doc)
        //{
        //    MySqlConnection conexion = new MySqlConnection(Helper.conexionMysql());
        //    string query = "INSERT INTO DocumentoElectronico(ruc, numero_documento, tipo_documento, fecha_emision, monto_precio, xml, pdf)" +
        //                      $"VALUES ({doc.ruc},{doc.numero_documento},{doc.tipo_documento},{doc.fecha_emision},{doc.monto_precio},{doc.xml},{doc.pdf})";
        //    MySqlCommand command = new MySqlCommand(query, conexion);
        //    command.CommandType = System.Data.CommandType.Text;
        //    conexion.Open();
        //    try
        //    {
        //        int result = (int)command.ExecuteScalar();
        //        if (result > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //        conexion.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        conexion.Close();
        //        return false;
        //    }

        //}

        public bool InsertarDocumento(EDocumentoElectronicoMysql doc)
        {
            SqlConnection sqlConnection = new SqlConnection("workstation id=DocSunat.mssql.somee.com;packet size=4096;user id=side2017;pwd=s1d32017;data source=DocSunat.mssql.somee.com;persist security info=False;initial catalog=DocSunat");

            string query = "INSERT INTO DocumentoElectronico(ruc, numero_documento, tipo_documento, fecha_emision, monto_precio, xml_bytes, pdf_bytes)" +
                              $"VALUES ('{doc.ruc}','{doc.numero_documento}','{doc.tipo_documento}','{doc.fecha_emision}',{doc.monto_precio},'{doc.xml_bytes}','{doc.pdf_bytes}')";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            sqlConnection.Open();
            try
            {
                int res = (int)sqlCommand.ExecuteScalar();
                if (res > 0)
                    return true;
                else
                    return false;

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                return false;
            }
        }

    }
}
