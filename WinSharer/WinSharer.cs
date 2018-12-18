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


namespace WinSharer
{
    public partial class WinSharer : Form
    {
        public WinSharer()
        {
            InitializeComponent();
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

                m_pRdpSession.Open();
                IRDPSRAPIInvitation pInvitation = m_pRdpSession.Invitations.CreateInvitation("WinPresenter","PresentationGroup","",5);
                string invitationString = pInvitation.ConnectionString;

                UdpClient UdpSender = new UdpClient(new IPEndPoint(IPAddress.Any, 0));

                byte[] ipByte = System.Text.Encoding.ASCII.GetBytes(invitationString);
                //以下内容参数需要添加配置
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 7788);//默认向全局域网所有主机发送

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
    }
}