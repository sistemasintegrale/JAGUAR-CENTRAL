using System;
using static SGE.Common.Codigos;

namespace SGE.Common
{
    public static class CommonFunctions
    {
        public static int GetMotivoByMotivoGRE(int motivoGRE)
        {
            switch (motivoGRE)
            {
                case (int)MotivoGuiaRemision.Venta:
                    return (int)MotivoSalidaAlmacen.Ventas;
                case (int)MotivoGuiaRemision.Compra:
                    return (int)MotivoSalidaAlmacen.Otros;
                case (int)MotivoGuiaRemision.ParaTransformacion:
                    return (int)MotivoSalidaAlmacen.Transformacion;
                case (int)MotivoGuiaRemision.Devolucion:
                    return (int)MotivoSalidaAlmacen.Devolucion;
                case (int)MotivoGuiaRemision.TransfEntreEstablecimientosMismaEmpr:
                    return (int)MotivoSalidaAlmacen.TransferenciaEntreAlmacenes;
                default:
                    return (int)MotivoSalidaAlmacen.Otros;

            }
        }

        public static string GetValueItem<T>(T item, string property) where T : class
        {
            var val = typeof(T).GetProperty(property).GetValue(item);
            return val == null ? "0" : val.ToString();
        }
        public static void SetValueItem<T>(T item, string property, object value) where T : class
        {
            var propInfo = typeof(T).GetProperty(property);
            if (propInfo != null && propInfo.CanWrite)
            {
                var targetType = propInfo.PropertyType;

                // Si la propiedad es un tipo nullable, obtenemos el tipo subyacente
                if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    targetType = Nullable.GetUnderlyingType(targetType);
                }

                // Convertimos el valor al tipo apropiado, si es necesario
                var convertedValue = (value == null || targetType == null) ? null : Convert.ChangeType(value, targetType);

                propInfo.SetValue(item, convertedValue, null);
            }
        }
    }
}
