/* All content in this sample is 擜S IS?with with no warranties, and confer no rights. 
 * Any code on this blog is subject to the terms specified at http://www.microsoft.com/info/cpyright.mspx. 
 */

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AxRDPCOMAPILib;
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
            IPAddress ipAddr = null;
            IPHostEntry ipAddrEntry = Dns.GetHostEntry(Dns.GetHostName());//获得当前HOST集

            //找到ipv4有效的hostip
            foreach (IPAddress _IPAddress in ipAddrEntry.AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    ipAddr = _IPAddress;
                }
            }

            string ip = ipAddr.ToString();

            IPEndPoint localIpep = new IPEndPoint(ipAddr, 7788); // 本机IP，指定的端口号
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Any, 0); // 发送到的IP地址和端口号
            udpcRecv = new UdpClient(localIpep);

            Timer tick = new Timer();//这里必须使用System.Windows.Froms.Timer ，否则不在同一系工作线程
            tick.Interval = 3000;
            tick.Tick += delegate
            {
                
                byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
                ConnectionString = Encoding.Default.GetString(bytRecv);
                //ConnectionString = ReadFromFile();
                if (ConnectionString != null)
                {
                    try
                    {
                        string shareIp = remoteIpep.Address.ToString();
                        m_ConnectIp = shareIp;//该接口用来以后显示连接中的客户端IP
                        LogTextBox.Text += "链接到终端机: " + m_ConnectIp + Environment.NewLine;
                        pRdpViewer.SmartSizing = true;
                        pRdpViewer.Connect(ConnectionString, m_ConnectIp, "");
                        tick.Stop();
                    }
                    catch (Exception ex)
                    {
                        LogTextBox.Text += "链接错误. 错误信息: " + ex.ToString() + Environment.NewLine;
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
            
            LogTextBox.Text += ("从文件读取链接字窜 " +
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
                LogTextBox.Text += ("读取链接字串文件出错. 错误信息: " + ex.ToString() + Environment.NewLine);
            }
            return ReadText;
        }

        private void OnConnectionEstablished(object sender, EventArgs e)
        {
            LogTextBox.Text += "链接建立" + Environment.NewLine;
        }

        private void OnError(object sender, _IRDPSessionEvents_OnErrorEvent e)
        {
            int ErrorCode = (int)e.errorInfo;
            LogTextBox.Text += ("Error 0x" + ErrorCode.ToString("X") + Environment.NewLine);
        }

        private void OnConnectionTerminated(object sender, _IRDPSessionEvents_OnConnectionTerminatedEvent e)
        {
            LogTextBox.Text += "链接终止. 原因: " + e.discReason + Environment.NewLine;
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            pRdpViewer.RequestControl(RDPCOMAPILib.CTRL_LEVEL.CTRL_LEVEL_INTERACTIVE);
        }

        private void OnConnectionFailed(object sender, EventArgs e)
        {
            LogTextBox.Text += "链接失败." + Environment.NewLine;
        }

        private string m_ConnectIp = null;
        private bool m_bIsConnected = false;
    }
}