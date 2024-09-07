using QRCoder;
using SGE.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace SGE.WindowForms.Modules
{
    public class Convertir
    {
        public static decimal RedondearNumero(decimal numero)
        {
            decimal NumeroRedondeado = 0;
            NumeroRedondeado = Math.Round(numero, 2, MidpointRounding.ToEven);
            return NumeroRedondeado;
        }
        public static string ConvertMesEnLetras(DateTime fecha)
        {
            string Mes = "";
            switch (fecha.Month)
            {
                case 1:
                    Mes = "Enero";
                    break;
                case 2:
                    Mes = "Febrero";
                    break;
                case 3:
                    Mes = "Marzo";
                    break;
                case 4:
                    Mes = "Abril";
                    break;
                case 5:
                    Mes = "Mayo";
                    break;
                case 6:
                    Mes = "Junio";
                    break;
                case 7:
                    Mes = "Julio";
                    break;
                case 8:
                    Mes = "Agosto";
                    break;
                case 9:
                    Mes = "Septiembre";
                    break;
                case 10:
                    Mes = "Octubre";
                    break;
                case 11:
                    Mes = "Noviembre";
                    break;
                case 12:
                    Mes = "Diciembre";
                    break;
            }
            return Mes;
        }

        public static string ConvertNumeroEnLetras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));

            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }
            else
                dec = " CON " + "00" + "/100";

            res = ConvertNumeroEnLetras(Convert.ToDouble(entero)) + dec;
            return res;
        }


        private static string ConvertNumeroEnLetras(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);

            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + ConvertNumeroEnLetras(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + ConvertNumeroEnLetras(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = ConvertNumeroEnLetras(Math.Truncate(value / 10) * 10) + " Y " + ConvertNumeroEnLetras(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + ConvertNumeroEnLetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = ConvertNumeroEnLetras(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = ConvertNumeroEnLetras(Math.Truncate(value / 100) * 100) + " " + ConvertNumeroEnLetras(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + ConvertNumeroEnLetras(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = ConvertNumeroEnLetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + ConvertNumeroEnLetras(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + ConvertNumeroEnLetras(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = ConvertNumeroEnLetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + ConvertNumeroEnLetras(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + ConvertNumeroEnLetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = ConvertNumeroEnLetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + ConvertNumeroEnLetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;
        }

        public DataTable FiltrarDataTable(DataTable dt, String filtro)
        {
            DataRow[] rows;
            DataTable dtNew;

            dtNew = dt.Clone();

            rows = dt.Select(filtro);

            foreach (DataRow dr in rows)
            {
                dtNew.ImportRow(dr);
            }

            return dtNew;
        }
        static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        public static string CrearTemplateEmail()
        {
            var pathTemplate = string.Format(@"{0}Utilities\PlantillaHTML\Factura.html", AppDomain.CurrentDomain.BaseDirectory);
            StreamReader reader = new StreamReader(pathTemplate);
            string template = reader.ReadToEnd();
            return template;
        }
        public static string GenerarCodigoQR(EFacturaVentaElectronica Obe)
        {
            string codigoQR = string.Empty;
            codigoQR = $"{Obe.nroDocumentoEmisior}|{Obe.tipoDocumento}|{Obe.idDocumento.Split('-')[0]}|{Obe.idDocumento.Split('-')[1]}|{Obe.fechaEmision.Replace("/", "-").Replace("/", "-")}";
            return codigoQR;

        }
        public static Image GenerarQR(string codigo = "")
        {
            codigo = codigo ?? "";
            Image imagen_;

            try
            {
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(codigo, QRCodeGenerator.ECCLevel.Q))
                    {
                        using (QRCode qrCode = new QRCode(qrCodeData))
                        {

                            imagen_ = qrCode.GetGraphic(20);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                imagen_ = null;
            }
            return imagen_;
        }
    }
}
