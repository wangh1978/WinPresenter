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
using System.Drawing;
using System.Runtime.InteropServices;

namespace WinViewer
{
    public partial class WinViewer : Form
    {
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static Bitmap GetWindowCapture(IntPtr hWnd)
        {
            IntPtr hscrdc = GetWindowDC(hWnd);
            RECT windowRect = new RECT();
            GetWindowRect(hWnd, ref windowRect);
            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;

            IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, width, height);
            IntPtr hmemdc = CreateCompatibleDC(hscrdc);
            SelectObject(hmemdc, hbitmap);
            PrintWindow(hWnd, hmemdc, 0);
            Bitmap bmp = Bitmap.FromHbitmap(hbitmap);
            DeleteDC(hscrdc);//删除用过的对象
            DeleteDC(hmemdc);//删除用过的对象
            return bmp;
        }

        //[DllImport("user32")]
        //[return: MarshalAs(UnmanagedType.Bool)]

        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);

        public delegate bool CallBack(IntPtr hwnd, int lParam);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(
            IntPtr hwndParent,
            uint hwndChildAfter,
            string lpszClass,
            string lpszWindow
            );

        private IntPtr FindWindowEx(IntPtr hwnd, string lpszWindow, bool bChild)
        {
            IntPtr iResult = IntPtr.Zero;
            // 首先在父窗体上查找控件
            iResult = FindWindowEx(hwnd, 0, null, lpszWindow);
            // 如果找到直接返回控件句柄
            if (iResult != IntPtr.Zero) return iResult;

            // 如果设定了不在子窗体中查找
            if (!bChild) return iResult;

            // 枚举子窗体，查找控件句柄
            int i = EnumChildWindows(
            hwnd,
            (h, l) =>
            {
                IntPtr f1 = FindWindowEx(h, 0, null, lpszWindow);
                if (f1 == IntPtr.Zero)
                    return true;
                else
                {
                    iResult = f1;
                    return false;
                }
            },
            0);
            // 返回查找结果
            return iResult;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd
         );

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(
         IntPtr hwnd,                // Window to copy,Handle to the window that will be copied.
         IntPtr hdcBlt,              // HDC to print into,Handle to the device context.
         UInt32 nFlags               // Optional flags,Specifies the drawing options. It can be one of the following values.
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(
         string lpszDriver,         // driver name驱动名
         string lpszDevice,         // device name设备名
         string lpszOutput,         // not used; should be NULL
         IntPtr lpInitData   // optional printer data
         );
        [DllImport("gdi32.dll")]
        public static extern int BitBlt(
         IntPtr hdcDest, // handle to destination DC目标设备的句柄
         int nXDest,   // x-coord of destination upper-left corner目标对象的左上角的X坐标
         int nYDest,   // y-coord of destination upper-left corner目标对象的左上角的Y坐标
         int nWidth,   // width of destination rectangle目标对象的矩形宽度
         int nHeight, // height of destination rectangle目标对象的矩形长度
         IntPtr hdcSrc,   // handle to source DC源设备的句柄
         int nXSrc,    // x-coordinate of source upper-left corner源对象的左上角的X坐标
         int nYSrc,    // y-coordinate of source upper-left corner源对象的左上角的Y坐标
         UInt32 dwRop   // raster operation code光栅的操作值
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(
         IntPtr hdc // handle to DC
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
         IntPtr hdc,         // handle to DC
         int nWidth,      // width of bitmap, in pixels
         int nHeight      // height of bitmap, in pixels
         );

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
         IntPtr hdc,           // handle to DC
         IntPtr hgdiobj    // handle to object
         );

        [DllImport("gdi32.dll")]
        public static extern int DeleteDC(
         IntPtr hdc           // handle to DC
         );


        public WinViewer()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            //RdpViewerWindow rdpViewerWindow = new RdpViewerWindow
            //{
            //    MdiParent = this
            //};
            //rdpViewerWindow.Show();
            

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
            

            Timer tick = new Timer();//这里必须使用System.Windows.Froms.Timer ，否则不在同一系工作线程
            tick.Interval = 3000;
            tick.Tick += delegate
            {
                udpcRecv = new UdpClient(localIpep);
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
                udpcRecv.Close();
   
            };
            
            tick.Start();
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            pRdpViewer.Disconnect();
        }

        private void CaptureWndButton_Click(object sender, EventArgs e)
        {
            IntPtr HandleResult = FindWindowEx(pRdpViewer.Handle,@"Output Painter Window",true);

            Bitmap sourceBitmap = GetWindowCapture(HandleResult);

            sourceBitmap.Save(@"form2.bmp");
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