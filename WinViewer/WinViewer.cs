/* All content in this sample is 擜S IS?with with no warranties, and confer no rights. 
 * Any code on this blog is subject to the terms specified at http://www.microsoft.com/info/cpyright.mspx. 
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxRDPCOMAPILib;

using System.Timers;
using System.Net.Sockets;
using System.Net;

namespace WinViewer
{
    public partial class WinViewer : Form
    {
        public WinViewer()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            string ConnectionString = null;
            UdpClient udpcRecv;

            IPEndPoint localIpep = new IPEndPoint(IPAddress.Parse("192.168.0.106"), 7788); // 本机IP，指定的端口号
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Any, 0); // 发送到的IP地址和端口号
            udpcRecv = new UdpClient(localIpep);

            System.Timers.Timer tick = new System.Timers.Timer();
            tick.Interval = 3000;
            tick.Elapsed += delegate
            {
                
                byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
                ConnectionString = System.Text.Encoding.Default.GetString(bytRecv);
                //ConnectionString = ReadFromFile();
                if (ConnectionString != null)
                {
                    try
                    {
                        pRdpViewer.Connect(ConnectionString, "Viewer1", "");
                    }
                    catch (Exception ex)
                    {
                        LogTextBox.Text += ("Error in Connecting. Error Info: " + ex.ToString() + Environment.NewLine);
                    }
                }
            };
            
            tick.Start();
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            pRdpViewer.Disconnect();
        }

        private string ReadFromFile()
        {
            string ReadText = null;
            string FileName = null;
            string[] args = Environment.GetCommandLineArgs();
            
            if (args.Length == 2)
            {
                if (!args[1].EndsWith("inv.xml"))
                {
                    FileName = args[1] + @"\" + "inv.xml";
                }
                else
                {
                    FileName = args[1];
                }
            }
            else
            {
                FileName = "inv.xml";
            }
            
            LogTextBox.Text += ("Reading the connection string from the file name " +
                FileName + Environment.NewLine);
            try
            {
                using (StreamReader sr = File.OpenText(FileName))
                {
                    ReadText = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                LogTextBox.Text += ("Error in Reading input file. Error Info: " + ex.ToString() + Environment.NewLine);
            }
            return ReadText;
        }

        private void OnConnectionEstablished(object sender, EventArgs e)
        {
            LogTextBox.Text += "Connection Established" + Environment.NewLine;
        }

        private void OnError(object sender, _IRDPSessionEvents_OnErrorEvent e)
        {
            int ErrorCode = (int)e.errorInfo;
            LogTextBox.Text += ("Error 0x" + ErrorCode.ToString("X") + Environment.NewLine);
        }

        private void OnConnectionTerminated(object sender, _IRDPSessionEvents_OnConnectionTerminatedEvent e)
        {
            LogTextBox.Text += "Connection Terminated. Reason: " + e.discReason + Environment.NewLine;
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            pRdpViewer.RequestControl(RDPCOMAPILib.CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE);
        }

        private void OnConnectionFailed(object sender, EventArgs e)
        {
            LogTextBox.Text += "Connection Failed." + Environment.NewLine;
        }
    }
}