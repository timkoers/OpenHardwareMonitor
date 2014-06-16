/*
 
  This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 
	Copyright (C) 2012 Prince Samuel <prince.samuel@gmail.com>

*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Diagnostics;

namespace OpenHardwareMonitor.GUI {
  public partial class PortForm : Form {
    private MainForm parent;
    private string localIP;
   
   
    public PortForm(MainForm m) {
      InitializeComponent();
      parent = m;
      localIP = getLocalIP();
    }

    private void portTextBox_TextChanged(object sender, EventArgs e) {

    }


    private string getLocalIP()
    {
        string Localip = "?";
        foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            
            var defaultGateway =
   from nics in NetworkInterface.GetAllNetworkInterfaces()
   from props in nics.GetIPProperties().GatewayAddresses
   where nics.OperationalStatus == OperationalStatus.Up
   select props.Address.ToString();
            
            GatewayIPAddressInformationCollection prop = netInterface.GetIPProperties().GatewayAddresses;

            if(defaultGateway.First() != null){

            IPInterfaceProperties ipProps = netInterface.GetIPProperties();
 
            foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
            {
                
                if (addr.Address.ToString().Contains(defaultGateway.First().Remove(defaultGateway.First().LastIndexOf("."))))
                {
                   
                    if (Localip == "?") // check if the string has been changed before
                    {
                        Localip = addr.Address.ToString();
                    }
                }
                
            }

            }
            
        }
       return Localip;
    }

    private string getLocalIPold() {
      IPHostEntry host;
      string localIPold = "?";
      host = Dns.GetHostEntry(Dns.GetHostName());
      foreach (IPAddress ip in host.AddressList) {
        if (ip.AddressFamily == AddressFamily.InterNetwork) {
          localIP = ip.ToString(); // gives the wrong IP address
        }
      }
     // localIP = System.Net.Dns.GetHostByName(Environment.MachineName).AddressList[0].ToString(); also gives the wrong IP address

      return localIPold;
    }

    private void portNumericUpDn_ValueChanged(object sender, EventArgs e) {
      string url = "http://" + localIP + ":" + portNumericUpDn.Value + "/";
      webServerLinkLabel.Text = url;
      webServerLinkLabel.Links.Remove(webServerLinkLabel.Links[0]);
      webServerLinkLabel.Links.Add(0, webServerLinkLabel.Text.Length, url);
    }

    private void portOKButton_Click(object sender, EventArgs e) {

      parent.Server.ListenerPort = (int)portNumericUpDn.Value;
      MainForm.server.StartHTTPListener();
      MainForm.rWSMI.Checked = true;
      this.Close();
    }

    private void portCancelButton_Click(object sender, EventArgs e) {
       this.Close();
    }

    private void PortForm_Load(object sender, EventArgs e) {
      portNumericUpDn.Value = parent.Server.ListenerPort;
      portNumericUpDn_ValueChanged(null, null);
    }

    private void webServerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      try {
        Process.Start(new ProcessStartInfo(e.Link.LinkData.ToString()));
      } catch { }
    }

   
  }
}
