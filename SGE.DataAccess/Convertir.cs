using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SGE.DataAccess
{
    public class Convertir
    {
        public DataTable ConvertirADataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable("Table");

            // nombre de las columnas
            PropertyInfo[] properties = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {

                // Use reflection para obtener nombres de propiedades, para crear la tabla, Sólo primera vez

                if (properties == null)
                {
                    properties = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in properties)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in properties)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                /// agrego el datarow al datatable
                dtReturn.Rows.Add(dr);
            }

            /// retorna un dataTable
            return dtReturn;
        }
    }
}
