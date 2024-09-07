using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using SGE.Common;
using SGE.Common.ApiSunat;
using SGE.Entity;
using SGE.WindowForms.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGE.WindowForms.Modules
{
    public static class Services
    {
        public static int GetNumberFromString(string input)
        {
            return Convert.ToInt32(new string(input.Where(char.IsDigit).ToArray()));
        }

        

        public static void SetErrorInput(BaseEdit oBase, string message)
        {
            if (oBase != null)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(frmMain));
                oBase.Focus();
                oBase.ErrorIcon = (Image)resources.GetObject("Warning");
                oBase.ErrorText = message;
                oBase.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
            }

            MessageError(message);
        }
        public static void MessageInfo(string message)
        {
            XtraMessageBox.Show(message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void MessageError(string message)
        {
            XtraMessageBox.Show(message, "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult MessageQuestion(string message)
        {
            return XtraMessageBox.Show(message, "Información del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
        public static void FilterView(GridView view, Dictionary<string, string> filterValues)
        {
            foreach (var item in filterValues)
            {
                view.Columns[item.Key].FilterInfo = new ColumnFilterInfo($"[{item.Key}] LIKE '%" + item.Value + "%'");
            }

        }
        public static void SetCurrentDateTime(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = new DateTime(Parametros.intEjercicio, 1, 1);
        }

        public static void SetMaximunDateTime(DateEdit fecha)
        {
            if (DateTime.Now.Year == Parametros.intEjercicio)
                fecha.EditValue = DateTime.Now;
            else
                fecha.EditValue = new DateTime(Parametros.intEjercicio, 12, 31);
        }
        public static void DeshabilitarControles(Control container, BarButtonItem saveButton = null, ContextMenuStrip menu = null)
        {
            foreach (Control control in container.Controls)
            {
                if (control is TextEdit || control is DateEdit || control is LookUpEdit || control is ButtonEdit)
                {
                    BaseEdit baseEdit = (BaseEdit)control;
                    baseEdit.Properties.ReadOnly = true;
                }
                if (control is ButtonEdit)
                {
                    BaseEdit baseEdit = (BaseEdit)control;
                    baseEdit.Enabled = false;
                }



                if (control.HasChildren)
                {
                    DeshabilitarControles(control, null);
                }
            }
            if (menu != null)
                DeshabilitarOpcionesDelMenuContextual(menu);

            if (saveButton != null)
            {
                saveButton.Enabled = false;
            }
        }

        private static void DeshabilitarOpcionesDelMenuContextual(ToolStripDropDown toolStripDropDown)
        {
            foreach (ToolStripItem item in toolStripDropDown.Items)
            {
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)item;
                    toolStripMenuItem.Enabled = false;
                    if (toolStripMenuItem.HasDropDownItems)
                    {
                        DeshabilitarOpcionesDelMenuContextual(toolStripMenuItem.DropDown);
                    }
                }
            }
        }

        public static void ExportarExcel(GridControl grd)
        {
            SaveFileDialog sfdRuta = new SaveFileDialog();
            if (sfdRuta.ShowDialog() == DialogResult.OK)
            {

                string fileName = sfdRuta.FileName;
                if (!fileName.Contains(".xlsx"))
                {
                    grd.ExportToXlsx(fileName + ".xlsx");
                    System.Diagnostics.Process.Start(fileName + ".xlsx");
                }
                else
                {
                    grd.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }


            }
        }



        public static TimeSpan? GetTimeSpanValue(TimeEdit tmEdit)
        {
            var value = tmEdit.Time.TimeOfDay;

            return value == TimeSpan.Zero ? (TimeSpan?)null : value;
        }

        public static DateTime? GetTimeEditValue(DateEdit dtEdit)
        {
            var value = (DateTime?)dtEdit.EditValue;
            return value;
        }



        public class ObjConsultaDNI
        {
            public string token { get; set; }
            public string dni { get; set; }
        }

        public class objresultDNI
        {
            public string success { get; set; }
            public string dni { get; set; }
            public string nombre_completo { get; set; }

        }

        public class objresultDNI2
        {
            public bool success { get; set; }
            public string nombre { get; set; }
            public string message { get; set; }

        }
        public static async Task<Tuple<objresultDNI2, string>> ConsultaDni2(string dni)
        {
            var postData = new
            {
                token = "Z6IzNAgPTYkJcO1izWD76XtIUr3yv9rBOniC6WDPp5a5qqasCcxR7rWYHNMW",
                dni = dni
            };

            var json = JsonConvert.SerializeObject(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await new HttpClient().PostAsync("https://api.migo.pe/api/v1/dni", content);

                var resbody = await response.Content.ReadAsStringAsync();
                var res = new Tuple<objresultDNI2, string>(JsonConvert.DeserializeObject<objresultDNI2>(resbody), string.Empty);
                if (!res.Item1.success)
                {
                    MessageError($"No se encontró persona con el DNI {dni}");
                    return new Tuple<objresultDNI2, string>(null, ErrorMessage.NoEncontrado);
                }
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Tuple<objresultDNI2, string>(null, ex.Message);
            }

        }

        public static Tuple<objresultDNI, string> ConsultaDni(string dni)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ruc.com.pe/api/v1/consultas");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            var consult = new ObjConsultaDNI()
            {
                token = "1e4686de-cd50-4d30-a137-5037488fe3e1-552268a6-dd7a-44c9-8ba9-f62381c261a3",
                dni = dni
            };
            string json = JsonConvert.SerializeObject(consult);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return new Tuple<objresultDNI, string>(JsonConvert.DeserializeObject<objresultDNI>(result), "");

                };
            }
            catch (Exception ex)
            {

                return new Tuple<objresultDNI, string>(null, ex.Message);
            }

        }


        public static ResultConsultaRuc ConsultaRuc(string ruc)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://ruc.com.pe/api/v1/consultas");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            var consult = new
            {
                token = "ee270056-7c9d-4d98-b471-af25004b3172-de11769b-ebfe-4a1d-8d85-05c108a9d8d0",
                ruc = ruc
            };
            string json = JsonConvert.SerializeObject(consult);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<ResultConsultaRuc>(result);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Por favor, ingrese número de RUC válido", "Información del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }



        public static string GetDayNumber(DateTime? fecha)
        {
            if (fecha is null) return "";
            string value = fecha.Value.Day.ToString("D2"); ;
            return value;
        }

        public static string GetMonthNumber(DateTime? fecha)
        {
            if (fecha is null) return "";
            string value = fecha.Value.Month.ToString("D2"); ;
            return value;
        }

        public static string GetMonthName(DateTime? fecha)
        {
            if (fecha is null) return "";
            string value = fecha.Value.ToString("MMMM"); ;
            return value;
        }

        public static string GetYearNumber(DateTime? fecha)
        {
            if (fecha is null) return "";
            string value = fecha.Value.Year.ToString(); ;
            return value;
        }

        public static string GetFullDateNumber(DateTime? fecha)
        {
            if (fecha is null) return "";
            string value = fecha.Value.ToString("dd/MM/yyyy"); ;
            return value;
        }

        public static List<T> ObtenerElementosFiltrados<T>(ColumnView gridView1) where T : class
        {
            List<T> lista = new List<T>();

            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                // Obtén la fila y conviértela al tipo genérico T
                T item = gridView1.GetRow(i) as T;

                if (item != null)
                {
                    lista.Add(item);
                }
            }

            return lista;
        }
    }
}
