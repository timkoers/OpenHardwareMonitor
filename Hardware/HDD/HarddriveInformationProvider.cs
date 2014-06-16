using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace OpenHardwareMonitor.Hardware.HDD
{
    public static class HarddriveInformationProvider
    {
        public static string description;
        public static string data;
        public static string capabilities;
        public static string CapabilityDescriptions;
        public static string PowerManagementCapabilities;
        public static String model;
        static ManagementObjectSearcher mosDisks;

        public static void GetInformation(String model)
        {

            mosDisks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE Model ='" + model + "'");

            // Loop through each object (disk) retrieved by WMI
            foreach (ManagementObject Disk in mosDisks.Get())
            {
                if (Disk["Capabilities"] == null)
                {
                    capabilities = "None";
                }
                else
                {
                    UInt16[] arrCapabilities = (UInt16[])(Disk["Capabilities"]);
                    foreach (UInt16 arrValue in arrCapabilities)
                    {
                        capabilities = arrValue.ToString();
                    }
                }

                if (Disk["CapabilityDescriptions"] == null)
                    CapabilityDescriptions = "None";
                else
                {
                    String[] arrCapabilityDescriptions = (String[])(Disk["CapabilityDescriptions"]);
                    foreach (String arrValue in arrCapabilityDescriptions)
                    {
                        CapabilityDescriptions = arrValue.ToString();
                    }
                }

                if (Disk["PowerManagementCapabilities"] == null)
                    PowerManagementCapabilities = "None";
                else
                {
                    UInt16[] arrPowerManagementCapabilities = (UInt16[])(Disk["PowerManagementCapabilities"]);
                    foreach (UInt16 arrValue in arrPowerManagementCapabilities)
                    {
                        PowerManagementCapabilities = arrValue.ToString();
                    }
                }
                description = "Model: " + Disk["Model"] + Environment.NewLine + "Serial: " + Disk["SerialNumber"] + Environment.NewLine + "Interface: " + Disk["InterfaceType"].ToString();

                data = "Availability: " + Disk["Availability"] + Environment.NewLine + "Bytes Per Sector: " + Disk["BytesPerSector"] + Environment.NewLine +
                    "Capabilities: " + capabilities + Environment.NewLine + "Capability Descriptions: " + CapabilityDescriptions
                    + Environment.NewLine + "Caption: " + Disk["Caption"] + Environment.NewLine + "Compression Method: " + Disk["CompressionMethod"]
                    + Environment.NewLine + "Config Manager ErrorCode: " + Disk["ConfigManagerErrorCode"]
                    + Environment.NewLine + "Config Manager UserConfig: " + Disk["ConfigManagerUserConfig"] + Environment.NewLine + "Creation Class Name: " + Disk["CreationClassName"]
                    + Environment.NewLine + "Default Block Size: " + Disk["DefaultBlockSize"] + Environment.NewLine + "Description: " + Disk["Description"]
                    + Environment.NewLine + "Device ID: " + Disk["DeviceID"] + Environment.NewLine + "Error Cleared: " + Disk["ErrorCleared"]
                    + Environment.NewLine + "Error Description: " + Disk["ErrorDescription"] + Environment.NewLine + "Error Methodology: " + Disk["ErrorMethodology"]
                    + Environment.NewLine + "Firmware Revision: " + Disk["FirmwareRevision"] + Environment.NewLine + "Index: " + "Index: " + Disk["Index"]
                    + Environment.NewLine + "Install Date: " + Disk["InstallDate"] + Environment.NewLine + "Interface: " + Disk["InterfaceType"]
                    + Environment.NewLine + "Last Error Code: " + Disk["LastErrorCode"] + Environment.NewLine + "Manufacturer: " + Disk["Manufacturer"]
                    + Environment.NewLine + "Max Block Size: " + Disk["MaxBlockSize"] + "Max Media Size: " + Disk["MaxMediaSize"]
                    + Environment.NewLine + "Media Loaded: " + Disk["MediaLoaded"] + Environment.NewLine + "Media Type: " + Disk["MediaType"]
                    + Environment.NewLine + "Min Block Size: " + Disk["MinBlockSize"] + Environment.NewLine + "Model: " + Disk["Model"]
                    + Environment.NewLine + "Name: " + Disk["Name"] + Environment.NewLine + "Needs Cleaning: " + Disk["NeedsCleaning"]
                    + Environment.NewLine + "Number Of Media Supported: " + Disk["NumberOfMediaSupported"] + Environment.NewLine + "Partitions: " + Disk["Partitions"]
                    + Environment.NewLine + "PNP Device ID: " + Disk["PNPDeviceID"] + Environment.NewLine + "Power Management Capabilities: " + PowerManagementCapabilities
                    + Environment.NewLine + "Power Management Supported: " + Disk["PowerManagementSupported"] + Environment.NewLine + "SCSIBus: " + Disk["SCSIBus"]
                    + Environment.NewLine + "SCSI Logical Unit: " + Disk["SCSILogicalUnit"] + "SCSIPort: " + Disk["SCSIPort"]
                    + Environment.NewLine + "SCSI Target Id: " + Disk["SCSITargetId"] + Environment.NewLine + "Sectors Per Track: " + Disk["SectorsPerTrack"]
                    + Environment.NewLine + "Serial Number: " + Disk["SerialNumber"] + Environment.NewLine + "Signature: " + Disk["Signature"]
                    + Environment.NewLine + "Size: " + Disk["Size"] + " bytes (" + Math.Round(((((double)Convert.ToDouble(Disk["Size"]) / 1024) / 1024) / 1024), 2) + " GB)"
                    + Environment.NewLine + "Status: " + Disk["Status"]
                    + Environment.NewLine + "Status Info: " + Disk["StatusInfo"] + Environment.NewLine + "System Creation ClassName: " + Disk["SystemCreationClassName"]
                    + Environment.NewLine + "System Name: " + Disk["SystemName"] + Environment.NewLine + "Total Cylinders: " + Disk["TotalCylinders"]
                    + Environment.NewLine + "Total Heads: " + Disk["TotalHeads"] + Environment.NewLine + "Total Sectors: " + Disk["TotalSectors"]
                    + Environment.NewLine + "Total Tracks: " + Disk["TotalTracks"] + "Tracks Per Cylinder: " + Disk["TracksPerCylinder"];
            }
        }
    }
}
