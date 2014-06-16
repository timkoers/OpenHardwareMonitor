using System;
using System.Collections.Generic;
using System.Text;

namespace OpenHardwareMonitor.Hardware.HDD
{
    public static class HarddriveInformation
    {
        public static List<string> harddriveinformationList = new List<string>();
        public static string harddriveDescription;
        public static List<string> harddriveName= new List<string>();
        public static void clearList(){

            harddriveinformationList.Clear();
            harddriveDescription = "";
            harddriveName.Clear(); ;
        }

    }

    
  }

