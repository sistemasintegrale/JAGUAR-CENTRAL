using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
namespace SGE.WindowForms.Modules
{
    public class BSConverter<T>
    {
        public static DataTable Convert(List<T> items)
        {
            DataTable ReturnValue = new DataTable();
            Type ItemType = typeof(T);
            foreach (PropertyInfo prop in ItemType.GetProperties())
            {
                DataColumn Column = new DataColumn(prop.Name);
                System.Type CurType = prop.PropertyType;
                // Preguntar por tipos nulos
                if (prop.PropertyType.IsGenericType)
                {
                    CurType = prop.PropertyType.GetGenericArguments()[0];
                }
                Column.DataType = CurType;
                ReturnValue.Columns.Add(Column);
            }
            int j = 0;
            foreach (T item in items)
            {
                j = 0;
                object[] newRow = new object[ReturnValue.Columns.Count];
                foreach (PropertyInfo prop in ItemType.GetProperties())
                {
                    object obj = prop.GetValue(item, null);
                    newRow[j] = prop.GetValue(item, null);
                    j += 1;
                }
                ReturnValue.Rows.Add(newRow);
            }
            return ReturnValue;
        }
    }

}
