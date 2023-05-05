using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Controls;
using HalconDotNet;
using PublicOperation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Demo
{
    public partial class RuningForm : Form
    {
        /// <summary>
        /// TCP通讯
        /// </summary>
        TcpCommunication TCP = new TcpCommunication();
        BasicFrom BF = new BasicFrom();
        VideoSourcePlayer VideoSourcePlayer1 = new VideoSourcePlayer();
        RunParameter RP = new RunParameter();
        count count = new count();
        ctrWindow ctrWindow = new ctrWindow();
        const int Camnum = 1;
        HikVisionCamera[] Cam = new HikVisionCamera[Camnum];
        //是否运行
        private volatile bool isRun = false;
        //开启运行线程
        Thread RuningProgram;
        //是否连接客户端
        private bool isConnectClient = false;
        //接收客户端数据
        private string[] strReceiveData = new string[4];
        private int iTimes = 0;
        /// <summary>
        /// 判断相机是否打开
        /// </summary>
        private bool isOpenCamera = true;

        private HObject Image;
        Bitmap bitmap;
        public RuningForm()
        {
            InitializeComponent();

        }

        private void Setting_Click(object sender, EventArgs e)
        {
            BF.ShowDialog();
            //this.Close();
            //Application.ExitThread();//退出当前窗体，这一步很重要，否则最后可能无法将所有进程关闭。最好是在跳转页面后，将之前的页面退出。
        }

        private void Load_RuningForm(object sender, EventArgs e)
        {
            LoadParameter();
            init();
            //for (int i = 0; i < Camnum; i++)
            //{
            //    Cam[i] = new HikVisionCamera();
            //}
        }

        private void Start_Run_Click(object sender, EventArgs e)
        {
            if (!isRun)
            {
                try
                {
                    TCP.OpenServer(RP.IP, RP.Port);
                    CPublic.InsertNote(DateTime.Now + " 通讯地址:" + RP.IP + ":" + RP.Port, Listlog, true);
                    #region  打开多相机(工业相机)
                    //try
                    //{
                    //    for (int i = 0; i < Camnum; i++)
                    //    {
                    //        bool b_temp = Cam[i].OpenCamera(RP.listCamera[i].CamName);
                    //        isOpenCamera = isOpenCamera & b_temp;
                    //        if (!isOpenCamera)
                    //        {
                    //            CPublic.InsertNote("第" + (i + 1) + "相机打开失败!", Listlog, true);
                    //            return;
                    //        }
                    //        else
                    //        {
                    //            CPublic.InsertNote("第" + (i + 1) + "相机打开成功!", Listlog, true);
                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    CPublic.InsertNote("相机打开出错，错误信息:" + ex.Message,Listlog,true);
                    //}
                    #endregion
                    try
                    {
                        BF.ConnectCamera();
                        CPublic.InsertNote("打开相机成功!", Listlog, true);

                    }
                    catch (Exception ex)
                    {
                        CPublic.InsertNote("打开相机失败!错误信息:" + ex.Message);
                        return;
                    }
                    isRun = true;
                    RuningProgram = new Thread(new ThreadStart(RunProgram));
                    RuningProgram.Start(); //开启线程
                    btn_start.BackColor = Color.Green;
                    btn_start.Text = "运行中...";

                }
                catch (Exception ex)
                {
                    CPublic.InsertNote("TCP通讯出错，错误信息:" + ex.Message, Listlog, true);
                }

            }
            else
            {
                TCP.CloseServer();
                if (RuningProgram != null && RuningProgram.IsAlive)
                {
                    RuningProgram.Join(100);
                }
                isRun = false;
                btn_start.Text = "运行";
                btn_start.BackColor = Color.LightGray;
                BF.ShutCamera();
                BF.SaveCount();
                CPublic.InsertNote("已停止运行!程序退出", Listlog, true);
            }
        }

        public void RunProgram()
        {
            while (isRun)
            {
                //TCP通讯
                isConnectClient = TCP.isServerConnectClient;
                if (!isConnectClient && iTimes == 0)
                {
                    iTimes = 1;
                    TCP.CloseServer();
                    TCP.OpenServer(RP.IP, RP.Port);
                    CPublic.InsertNote("等待客户端连接...  " + "IP:" + RP.IP + "  PORT:" + RP.Port, Listlog, true);
                    continue;
                }
                else if (isConnectClient && iTimes == 1)
                {
                    iTimes = 0;
                    CPublic.InsertNote("客户端已连接成功!", Listlog, true);
                }
                else if (!isConnectClient)
                {
                    continue;
                }
                //CAPTURE,试样号 \r\n
                //CAPTURE_RESULT,试样号,OK/NG \r\n
                //方法
                string str_ReceiveData = TCP.strServerReceiveData;
                if (str_ReceiveData != null)
                {
                    strReceiveData = str_ReceiveData.Replace("\r\n", "").Split(new char[] { ',' });
                    str_ReceiveData = "";
                    if (strReceiveData[0] == "CAPTURE")
                    {
                        HTuple width, height;
                        if (VideoSourcePlayer1.VideoSource == null)
                        {
                            MessageBox.Show("请先打开相机!", "提示");
                            return;
                        }
                        //videoSourcePlayer继承Control父类，GetCurrentVideoFrame可以输出bitmap
                        bitmap = VideoSourcePlayer1.GetCurrentVideoFrame();
                        //picturebox显示的彩色bitmap图像格式转成可用halcon的彩色Hobject格式
                        BF.Bitmap2HObjectBpp24(bitmap, out Image);
                        HOperatorSet.GetImageSize(Image, out width, out height);
                        HOperatorSet.SetPart(PanelWindow.Handle, 0, 0, height, width);
                        HOperatorSet.DispObj(Image, PanelWindow.Handle);
                    }

                }

            }
        }

        private void LoadParameter()
        {
            try
            {
                RP = (RunParameter)BasicArithmetic.LoadFromXml(Application.StartupPath + "\\RunParameter.xml", RP.GetType());
                count = (count)BasicArithmetic.LoadFromXml(Application.StartupPath + "\\Count.xml", count.GetType());
            }
            catch (Exception ex)
            {
                CPublic.InsertNote("加载函数异常" + ex, Listlog, true);
            }

        }

        private void init()
        {
            //this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            //this.WindowState = FormWindowState.Maximized;
            //ctrWindow.Dock = DockStyle.Fill;
            PanelWindow.Controls.Add(ctrWindow);
        }



        private void RunningForm_ClosingFrom(object sender, FormClosingEventArgs e)
        {
            if (RuningProgram != null && RuningProgram.IsAlive)
            {
                RuningProgram.Join(100);
            }
            isRun = false;
            this.Dispose();
            this.Close();
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            if (RuningProgram != null && RuningProgram.IsAlive)
            {
                RuningProgram.Join(100);
            }
            isRun = false;
            this.Dispose();
            this.Close();
        }
    }
}
