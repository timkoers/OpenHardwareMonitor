using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Timers;
using System.Threading;
using System.Management;
using System.Windows.Forms;

namespace OpenHardwareMonitor.GUI
{
    public partial class RAMForm : Form
    {

        public static Label Loadinglabel = new Label();
        public static ProgressBar LoadingProgress = new ProgressBar();
        public static ComboBox ramCombo = new ComboBox();

        public static string RequestedHardwareName;
        public static RichTextBox InformationBox;

        public static Label Description = new Label();

        public static System.Timers.Timer wait;

        string RAMdata;
        public RAMForm()
        {
            InitializeComponent();
            loadingLabel = Loadinglabel;
            LoadingProgress = loadingBar;

            ramCombo = DIMMComboBox;
            Description = descriptionLabel;
            InformationBox = InfoBox;
        }

        public void addData(string data)
        {
            RAMdata = data;
        }

        private void RAMForm_Load(object sender, EventArgs e)
        {

            Loadinglabel.Text = "Loading";
            InfoBox.Text = "Please select a DIMM.";
            RAMdata = "";
            // Get all the disk drives
            ManagementObjectSearcher mosRAM = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            // Loop through each object (disk) retrieved by WMI
            foreach (ManagementObject moRAM in mosRAM.Get())
            {
                // Add the HDD to the list (use the Model field as the item's caption)
                ramCombo.Items.Add(moRAM["Manufacturer"].ToString() + " " + moRAM["PartNumber"].ToString() + " " + "(" + moRAM["BankLabel"].ToString() + ")");
                if (moRAM["Manufacturer"].ToString().Contains("Manufacturer"))
                {
                    WarningLabel.Text = "Open Hardware Monitor has detected that your motherboard cannot display the right RAM information." + Environment.NewLine + "Since Open Hardware Monitor depends on the motherboard information," + Environment.NewLine +"wrong information may be displayed.";

                }
            }
            wait = new System.Timers.Timer(1000);
            wait.Elapsed += TimerElapsed;
            wait.Enabled = true;
            
        }

        private static void TimerElapsed(Object source, ElapsedEventArgs e)
        {
            ramCombo.SelectedItem = ramCombo.Items[0].ToString();
            wait.Enabled = false;
        }


        private void DIMMComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InfoBox.Text = "";
            InfoBox.Visible = false;
            OpenHardwareMonitor.Hardware.RAM.RAMInformationProvider.Cleanup();

            OpenHardwareMonitor.Hardware.RAM.RAMInformationProvider.GetInformation(ramCombo.SelectedItem.ToString());
            addData(OpenHardwareMonitor.Hardware.RAM.RAMInformationProvider.data);
            descriptionLabel.Text = OpenHardwareMonitor.Hardware.RAM.RAMInformationProvider.description;
            InfoBox.Visible = true;
            InfoBox.Text = RAMdata;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
