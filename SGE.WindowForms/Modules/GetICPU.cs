using System.Collections.Generic;
using System.Management;

namespace SGE.WindowForms.Modules
{
    public class GetICPU
    {
        public static string get()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            List<string> listProcessor = new List<string>();
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                listProcessor.Add(wmi_HD["ProcessorID"].ToString());
            }

            return listProcessor[0];
        }
    }
}
