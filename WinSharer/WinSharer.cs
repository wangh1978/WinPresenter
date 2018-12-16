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
using System.Runtime.InteropServices;
using RDPCOMAPILib;

using System.Net;
using System.Net.Sockets;

using System.Timers;

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
            LogTextBox.Text += ("Attendee Disconnected: " + pDiscInfo.Attendee.RemoteName + Environment.NewLine);
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

                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 7788);//默认向全世界所有主机发送即可，路由器自动给你过滤，只发给局域网主机

                System.Timers.Timer tick = new System.Timers.Timer();

                tick.Interval = 3000;

                tick.Elapsed += delegate
                {
                    UdpSender.Send(ipByte, ipByte.Length, endpoint);
                };

                tick.Start();

                WriteToFile(invitationString);
                LogTextBox.Text += "Presentation Started. Your Desktop is being shared." + Environment.NewLine;
            }
            catch (Exception ex)
            {
                LogTextBox.Text += "Error occured while starting presentation. Error: " + ex.ToString() + Environment.NewLine;
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
                m_pRdpSession.Close();
                LogTextBox.Text += "Presentation Stopped." + Environment.NewLine;
                Marshal.ReleaseComObject(m_pRdpSession);
                m_pRdpSession = null;
            }
            catch (Exception ex)
            {
                LogTextBox.Text += "Error occured while stopping presentation. Error: " + ex.ToString();
            }
        }

        private void OnAttendeeConnected(object pObjAttendee)
        {
            IRDPSRAPIAttendee pAttendee = pObjAttendee as IRDPSRAPIAttendee;
            pAttendee.ControlLevel = CTRL_LEVEL.CTRL_LEVEL_VIEW;
            LogTextBox.Text += ("Attendee Connected: " + pAttendee.RemoteName + Environment.NewLine);
        }

        public void WriteToFile(string InviteString)
        {
            using (StreamWriter sw = File.CreateText("inv.xml"))
            {
                sw.WriteLine (InviteString);
            }

        }
    }
}