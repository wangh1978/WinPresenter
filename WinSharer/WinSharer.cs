/* All content in this sample is 擜S IS?with with no warranties, and confer no rights. 
 * Any code on this blog is subject to the terms specified at http://www.microsoft.com/info/cpyright.mspx. 
 */

using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using RDPCOMAPILib;

using System.Net;
using System.Net.Sockets;
using System.Configuration;


namespace WinSharer
{
    public partial class WinSharer : Form
    {
        public WinSharer()
        {
            InitializeComponent();
            textIp.Text = GetConnectionStringsConfig("HostIp");
        }

        //依据连接串名字connectionName返回数据连接字符串  
        public static string GetConnectionStringsConfig(string connectionName)
        {
            //指定config文件读取
            string file = System.Windows.Forms.Application.ExecutablePath;
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            string connectionString =
                config.ConnectionStrings.ConnectionStrings[connectionName].ConnectionString.ToString();
            return connectionString;
        }

        ///<summary> 
        ///更新连接字符串  
        ///</summary> 
        ///<param name="newName">连接字符串名称</param> 
        ///<param name="newConString">连接字符串内容</param> 
        ///<param name="newProviderName">数据提供程序名称</param> 
        public static void UpdateConnectionStringsConfig(string newName, string newConString, string newProviderName)
        {
            //指定config文件读取
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);

            bool exist = false; //记录该连接串是否已经存在  
            //如果要更改的连接串已经存在  
            if (config.ConnectionStrings.ConnectionStrings[newName] != null)
            {
                exist = true;
            }
            // 如果连接串已存在，首先删除它  
            if (exist)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(newName);
            }
            //新建一个连接字符串实例  
            ConnectionStringSettings mySettings =
                new ConnectionStringSettings(newName, newConString, newProviderName);
            // 将新的连接串添加到配置文件中.  
            config.ConnectionStrings.ConnectionStrings.Add(mySettings);
            // 保存对配置文件所作的更改  
            config.Save(ConfigurationSaveMode.Modified);
            // 强制重新载入配置文件的ConnectionStrings配置节  
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        void OnAttendeeDisconnected(object pDisconnectInfo)
        {
            IRDPSRAPIAttendeeDisconnectInfo pDiscInfo = pDisconnectInfo as IRDPSRAPIAttendeeDisconnectInfo;
            LogTextBox.Text += ("链接中断: " + pDiscInfo.Attendee.RemoteName + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                m_pRdpSession = new RDPSession();

                m_pRdpSession.OnAttendeeConnected += new _IRDPSessionEvents_OnAttendeeConnectedEventHandler(OnAttendeeConnected);
                m_pRdpSession.OnAttendeeDisconnected += new _IRDPSessionEvents_OnAttendeeDisconnectedEventHandler(OnAttendeeDisconnected);
                m_pRdpSession.OnControlLevelChangeRequest += new _IRDPSessionEvents_OnControlLevelChangeRequestEventHandler(OnControlLevelChangeRequest);
                //m_pRdpSession.colordepth = 16;
                m_pRdpSession.Open();
                IRDPSRAPIInvitation pInvitation = m_pRdpSession.Invitations.CreateInvitation("WinPresenter","PresentationGroup","",5);
                string invitationString = pInvitation.ConnectionString;

                UdpClient UdpSender = new UdpClient(new IPEndPoint(IPAddress.Any, 0));

                byte[] ipByte = System.Text.Encoding.ASCII.GetBytes(invitationString);
                //以下内容参数需要添加配置
                //IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 7788);//默认向全局域网所有主机发送
                //IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("192.168.0.120"), 7788);//向指定主机发送
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(textIp.Text), 7788);//向指定主机发送
                
                m_Timer.Interval = 3000;

                m_Timer.Tick += delegate
                {
                    UdpSender.Send(ipByte, ipByte.Length, endpoint);
                };

                m_Timer.Start();

                //WriteToFile(invitationString);
                LogTextBox.Text += "开启局域网共享广播." + Environment.NewLine;
            }
            catch (Exception ex)
            {
                LogTextBox.Text += "当前共享广播出错. 错误: " + ex.ToString() + Environment.NewLine;
            }
        }

        void OnControlLevelChangeRequest(object pObjAttendee, CTRL_LEVEL RequestedLevel)
        {
            IRDPSRAPIAttendee pAttendee = pObjAttendee as IRDPSRAPIAttendee;
            pAttendee.ControlLevel = RequestedLevel;
        }

        protected RDPSession m_pRdpSession = null;

        private void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                m_Timer.Start();
                m_pRdpSession.Close();
                LogTextBox.Text += "停止共享." + Environment.NewLine;
                Marshal.ReleaseComObject(m_pRdpSession);
                m_pRdpSession = null;
            }
            catch (Exception ex)
            {
                LogTextBox.Text += "停止共享出错. 错误: " + ex.ToString();
            }
        }

        private void OnAttendeeConnected(object pObjAttendee)
        {
            IRDPSRAPIAttendee pAttendee = pObjAttendee as IRDPSRAPIAttendee;
            pAttendee.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_VIEW;
            LogTextBox.Text += ("链接到控制端: " + pAttendee.RemoteName + Environment.NewLine);
        }

        public void WriteToFile(string InviteString)
        {
            using (StreamWriter sw = File.CreateText("inv.xml"))
            {
                sw.WriteLine (InviteString);
            }

        }
        private Timer m_Timer = new Timer();

        private void SetButton_Click(object sender, EventArgs e)
        {
            UpdateConnectionStringsConfig("HostIp",textIp.Text,"");
        }
    }
}