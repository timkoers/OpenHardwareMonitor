using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace OpenHardwareMonitor.Hardware.RAM
{
    public static class RAMInformationProvider
    {
        public static string description;
        public static string data;
        static ManagementObjectSearcher mosRAM;
        public static string bankLabel;


        public static void GetInformation(string BankLabel)
        {
            bankLabel = BankLabel.Substring(BankLabel.IndexOf("(") +1, BankLabel.Length - BankLabel.IndexOf("(") -2);
            mosRAM = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory WHERE BankLabel ='" + bankLabel + "'");

            // Loop through each object (disk) retrieved by WMI
            foreach (ManagementObject DIMM in mosRAM.Get())
            {
                foreach (PropertyData property in DIMM.Properties)
                {
                    description = "Manufacturer: " + DIMM["Manufacturer"] + Environment.NewLine + "PartNumber: " + DIMM["PartNumber"];

                    if (property.Value != null)
                    {

                        data =  data + property.Name + ": " +property.Value.ToString() + Environment.NewLine;
                    }
                }
            }
        }

        public static void Cleanup()
        {
            bankLabel = "";
            description = "";
            data = "";
        }


    }
}
