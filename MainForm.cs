using HalconDotNet;
using System.ComponentModel;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using static System.Windows.Forms.MonthCalendar;
using EasyBuild;
using System.Runtime.CompilerServices;
using System.Reflection;
using AForge.Video.DirectShow;
using AForge.Controls;
using AForge.Imaging;
using System.Drawing.Imaging;
using PublicOperation;

namespace Demo
{
    public partial class BasicFrom : Form
    {
        public HTuple WindowID; //窗口句柄
        private HObject Image;

        //存放图像的数组
        private HObject[] ImageArray = new HObject[700];
        HTuple hv_ImageFiles = new HTuple();
        //引入线程对象
        private Thread ThreadObject;     //正常测试线程

        private Thread ThreadPhoto;  //连续采集图像线程

        //线程停止标记
        private bool Thread_Stop = false;  //正常测试线程停止标记
        private bool ThreadPhoto_Stop = false;

        private FilterInfoCollection _VideoDevices;
        private VideoCaptureDevice _VideoCapture;

        private VideoSourcePlayer VideoSourcePlayer1 = new VideoSourcePlayer();


        private float x;//记录自适应屏幕X坐标
        private float y;//记录自适应屏幕Y坐标


        Point mouseDownPoint = new Point(); //记录拖拽过程鼠标位置
        bool isMove = false;    //判断鼠标在picturebox上移动时，是否处于拖拽过程(鼠标左键是否按下)
        int zoomStep = 40;      //缩放步长
        Bitmap bitmap;

        /// <summary>
        /// 总数    voiatile关键字，为保证数据完整性，一次只允许一个线程访问
        /// </summary>
        private volatile int iTotalNum = 0;

        /// <summary>
        /// 不良
        /// </summary>
        private volatile int iBadNum = 0;


        /// <summary>
        /// 运行参数
        /// </summary>
        RunParameter RP = new RunParameter();
        /// <summary>
        /// 计数
        /// </summary>
        count count = new count();

        private List<string> ImagePath = new List<string>();
        private int ImageCount = 0;


        public BasicFrom()
        {
            InitializeComponent();
            //对象实例化
            Image = new HObject();
            CreateHalconWindow();
            Load_Camera();
            ReadParameter();
            DisplayProductCapacity(iTotalNum, iBadNum);

            //批量加载图片到数组中
            //LoadBatchImage();
            //线程实例化
            //ThreadObject = new Thread(new ThreadStart(PlayThread));
            ThreadPhoto = new Thread(new ThreadStart(ThreadPhotos));

            //窗口缩放
            x = this.Width; y = this.Height;
            ControlAutoSize controlAutoSize = new ControlAutoSize();
            controlAutoSize.setTag(this);

            HOperatorSet.ClearWindow(WindowID);
            //关闭相机
            ShutCamera();
            GC.Collect();


        }

        //线程函数
        public void PlayThread()
        {
            int i = 0;
            HTuple Width, Height;
            Thread_Stop = false;
            while (!Thread_Stop)
            {
                HOperatorSet.GetImageSize(ImageArray[i], out Width, out Height);
                //设置对象显示的颜色
                HOperatorSet.SetColor(WindowID, "yellow");
                //通过改变图像缩放来适应图像在窗口的正常显示
                HOperatorSet.SetPart(WindowID, 0, 0, Height, Width);
                HOperatorSet.DispObj(ImageArray[i], WindowID);
                Log_Show("第" + (i + 1) + "张图片显示中......");
                SelectQueXian(ImageArray[i]);

                Thread.Sleep(1000);
                i++;
                if (i >= ImageCount)
                {
                    Thread_Stop = true;
                    i = 0;
                }
            }
            SaveCount();
        }

        /// <summary>
        /// 连续拍照线程函数
        /// </summary>
        public void ThreadPhotos()
        {
            HTuple width, height;
            ThreadPhoto_Stop = false;
            while (!ThreadPhoto_Stop)
            {
                //videoSourcePlayer继承Control父类，GetCurrentVideoFrame可以输出bitmap
                bitmap = VideoSourcePlayer1.GetCurrentVideoFrame();
                //picturebox显示的彩色bitmap图像格式转成可用halcon的彩色Hobject格式
                Bitmap2HObjectBpp24(bitmap, out Image);
                HOperatorSet.GetImageSize(Image, out width, out height);
                HOperatorSet.SetPart(WindowID, 0, 0, height - 1, width - 1);
                HOperatorSet.DispObj(Image, WindowID);
                Thread.Sleep(50);
            }
        }

        //批量读取图片
        public void LoadBatchImage()
        {



            foreach (string path in Directory.GetFiles("D:\\图片\\Test"))
            {
                string PathExt = path.Substring(path.Length - 3, 3);
                if (PathExt == "jpg" || PathExt == "bmp" || PathExt == "png")
                {
                    ImagePath.Add(path);
                }
            }

            if (ImagePath.Count != 0)
            {
                ImageCount = ImagePath.Count;
            }

            for (int i = 0; i < ImageCount; i++)
            {

                HOperatorSet.ReadImage(out ImageArray[i], "D:\\图片\\Test\\" + (i + 1).ToString() + ".jpg");
            }

            //for (int i = 0; i < 20; i++)
            //{
            //    HOperatorSet.ReadImage(out ImageArray[i], "D:\\图片\\韶钢二工序视觉\\halconNG\\" + "NG_" + (i + 1).ToString() + ".jpg");
            //}
        }
        public void CreateHalconWindow()
        {
            //将界面所创建的窗口句柄赋值给FatherWindowID
            HTuple FatherWindow = this.Display_pictureBox.Handle;
            //设置窗口的背景颜色
            HOperatorSet.SetWindowAttr("background_color", "black");
            //窗口开始行，列，宽，高，父窗口，是否可视化，窗口在那个机器上(填写机器名称，0为本机)，输出句柄
            HOperatorSet.OpenWindow(0, 0, this.Display_pictureBox.Width, this.Display_pictureBox.Height, FatherWindow, "visible", "", out WindowID);


        }
        #region 加载单张图片
        public void LoadImage()
        {
            HTuple Width, Height;
            //读图
            HOperatorSet.ReadImage(out Image, "D:/图片/NG/2023021121284823G501824C8504_2370.4889.jpg");
            //获取图像尺寸大小
            HOperatorSet.GetImageSize(Image, out Width, out Height);
            //设置对象显示的颜色
            HOperatorSet.SetColor(WindowID, "yellow");
            //通过改变图像缩放来适应图像在窗口的正常显示
            HOperatorSet.SetPart(WindowID, 0, 0, Height, Width);
            //在窗口显示图像
            HOperatorSet.DispObj(Image, WindowID);

        }
        #endregion

        private void Start_Click(object sender, EventArgs e)
        {
            //判断线程状态是否打开
            if (ThreadObject.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                ThreadObject.Start();
            }
            //判断线程状态是否停止或则被中断，停止或中断后重新实例化并且打开线程
            if (ThreadObject.ThreadState == System.Threading.ThreadState.Stopped || ThreadObject.ThreadState == System.Threading.ThreadState.Aborted)
            {
                ThreadObject = new Thread(new ThreadStart(PlayThread));
                ThreadObject.Start();
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Thread_Stop = true;
            Log_Show("已停止采集!");
        }


        private void Check_Click(object sender, EventArgs e)
        {
            SelectQueXian(Image);
        }
        #region  缺陷检测
        public void SelectQueXian(HObject Image)
        {
            #region 缺陷检测优化20230322
            // Local iconic variables 

            HObject ho_GrayImage = null, ho_Regions = null;
            HObject ho_RegionFillUp = null, ho_ConnectedRegions = null;
            HObject ho_SelectedRegions = null, ho_ROI_0 = null, ho_ImageReduced = null;
            HObject ho_ImageMedian = null, ho_Regions1 = null, ho_ConnectedRegions1 = null;
            HObject ho_SelectedRegions1 = null, ho_ConnectedRegions2 = null;
            HObject ho_Rectangle = null;

            // Local control variables 

            HTuple hv_ImageFiles = new HTuple(), hv_Index = new HTuple();
            HTuple hv_WindowHandle = new HTuple(), hv_Area = new HTuple();
            HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Area2 = new HTuple(), hv_Row2 = new HTuple();
            HTuple hv_Column2 = new HTuple(), hv_Area1 = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Phi = new HTuple(), hv_Num = new HTuple(), hv_i = new HTuple(), hv_Num2 = new HTuple(), hv_Num3 = new HTuple();
            ;
            // Initialize local and output iconic variables 

            HOperatorSet.GenEmptyObj(out ho_GrayImage);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_ROI_0);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ImageMedian);
            HOperatorSet.GenEmptyObj(out ho_Regions1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);

            try
            {

                HOperatorSet.Rgb1ToGray(Image, out ho_GrayImage);
                //二值化 区域分割 面积过滤
                ho_Regions.Dispose();
                HOperatorSet.Threshold(ho_GrayImage, out ho_Regions, 125, 235);

                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUp(ho_Regions, out ho_RegionFillUp);

                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionFillUp, out ho_ConnectedRegions);

                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                "and", 943715, 3e+06);

                hv_Area.Dispose(); hv_Row.Dispose(); hv_Column.Dispose();
                HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row, out hv_Column);

                ho_ROI_0.Dispose();
                HOperatorSet.GenCircle(out ho_ROI_0, hv_Row, hv_Column, RP.Roi_Radius);

                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_GrayImage, ho_ROI_0, out ho_ImageReduced);

                ho_ImageMedian.Dispose();
                HOperatorSet.MedianImage(ho_ImageReduced, out ho_ImageMedian, "square", RP.MedianMarkRaduis,
                    "continued");

                ho_Regions1.Dispose();
                HOperatorSet.Threshold(ho_ImageMedian, out ho_Regions1, RP.MinGyay, RP.MaxGyay);

                ho_ConnectedRegions1.Dispose();
                HOperatorSet.Connection(ho_Regions1, out ho_ConnectedRegions1);
                hv_Area2.Dispose(); hv_Row2.Dispose(); hv_Column2.Dispose();
                HOperatorSet.AreaCenter(ho_ConnectedRegions1, out hv_Area2, out hv_Row2,
                    out hv_Column2);

                ho_SelectedRegions1.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions1, "area",
                    "and", RP.SearchAreaMin, RP.SearchAreaMax);

                hv_Num.Dispose();
                HOperatorSet.CountObj(ho_SelectedRegions1, out hv_Num);


                HOperatorSet.DispObj(Image, WindowID);

                if ((int)(new HTuple(hv_Area2.TupleEqual(0))) != 0)
                {


                    HOperatorSet.SetColor(WindowID, "green");
                    //HOperatorSet.SetFont(WindowID, "default-Normal-40");
                    //设置字体大小
                    HOperatorSet.SetFont(WindowID, "default-Bold-60");
                    HOperatorSet.SetTposition(WindowID, 30, 100);
                    HOperatorSet.WriteString(WindowID, "OK");
                    iTotalNum++;
                    //表示一个委托,Invoke用于线程之间
                    Invoke((MethodInvoker)(() =>
                    {
                        DisplayProductCapacity(iTotalNum, iBadNum);
                    }));
                    return;
                }
                else if ((int)(new HTuple(hv_Area2.TupleGreater(0))) > 0 && hv_Num == 0)
                {
                    HOperatorSet.SetColor(WindowID, "yellow");
                    //HOperatorSet.SetFont(WindowID, "default-Normal-40");
                    //设置字体大小
                    HOperatorSet.SetFont(WindowID, "default-Bold-20");
                    HOperatorSet.SetTposition(WindowID, 30, 80);
                    HOperatorSet.WriteString(WindowID, "缺陷面积不满足要求");
                    iTotalNum++;
                    //表示一个委托,Invoke用于线程之间
                    Invoke((MethodInvoker)(() =>
                    {
                        DisplayProductCapacity(iTotalNum, iBadNum);
                    }));
                    return;
                }

                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_SelectedRegions1, out ho_ConnectedRegions2);

                hv_Area1.Dispose(); hv_Row1.Dispose(); hv_Column1.Dispose();
                HOperatorSet.AreaCenter(ho_ConnectedRegions2, out hv_Area1, out hv_Row1,
                    out hv_Column1);

                hv_Phi.Dispose();
                HOperatorSet.OrientationRegion(ho_ConnectedRegions2, out hv_Phi);

                hv_Num3.Dispose();
                HOperatorSet.CountObj(ho_ConnectedRegions2, out hv_Num2);

                HOperatorSet.DispObj(Image, WindowID);

                if ((int)(new HTuple(hv_Num2.TupleNotEqual(0))) != 0)
                {
                    HTuple end_val41 = hv_Num2;
                    HTuple step_val41 = 1;
                    for (hv_i = 1; hv_i.Continue(end_val41, step_val41); hv_i = hv_i.TupleAdd(step_val41))
                    {
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_Rectangle.Dispose();
                            HOperatorSet.GenRectangle2ContourXld(out ho_Rectangle, hv_Row1.TupleSelect(
                                hv_i - 1), hv_Column1.TupleSelect(hv_i - 1), hv_Phi.TupleSelect(hv_i - 1),
                                110, 110);
                        }
                        HOperatorSet.SetColor(WindowID, "red");
                        HOperatorSet.DispObj(ho_Rectangle, WindowID);
                    }
                    iBadNum++;
                }
                iTotalNum++;
                //表示一个委托,Invoke用于线程之间
                Invoke((MethodInvoker)(() =>
                {
                    DisplayProductCapacity(iTotalNum, iBadNum);
                }));

            }
            catch (HalconException HDevExpDefaultException)
            {

                ho_GrayImage.Dispose();
                ho_Regions.Dispose();
                ho_RegionFillUp.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_ROI_0.Dispose();
                ho_ImageReduced.Dispose();
                ho_ImageMedian.Dispose();
                ho_Regions1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_Rectangle.Dispose();

                hv_ImageFiles.Dispose();
                hv_Index.Dispose();
                hv_WindowHandle.Dispose();
                hv_Area.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Area2.Dispose();
                hv_Row2.Dispose();
                hv_Column2.Dispose();
                hv_Area1.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Phi.Dispose();
                hv_Num.Dispose();
                hv_Num2.Dispose();
                hv_i.Dispose();

                throw HDevExpDefaultException;
            }
            ho_GrayImage.Dispose();
            ho_Regions.Dispose();
            ho_RegionFillUp.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions.Dispose();
            ho_ROI_0.Dispose();
            ho_ImageReduced.Dispose();
            ho_ImageMedian.Dispose();
            ho_Regions1.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_ConnectedRegions2.Dispose();
            ho_Rectangle.Dispose();

            hv_ImageFiles.Dispose();
            hv_Index.Dispose();
            hv_WindowHandle.Dispose();
            hv_Area.Dispose();
            hv_Row.Dispose();
            hv_Column.Dispose();
            hv_Area2.Dispose();
            hv_Row2.Dispose();
            hv_Column2.Dispose();
            hv_Area1.Dispose();
            hv_Row1.Dispose();
            hv_Column1.Dispose();
            hv_Phi.Dispose();
            hv_Num.Dispose();
            hv_Num2.Dispose();
            hv_i.Dispose();

            #endregion
        }
        #endregion


        private void Quit_Click(object sender, EventArgs e)
        {
            SaveCount();
            ThreadPhoto_Stop = true;
            ShutCamera();
            this.Close();


        }

        private void Open_Image_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string FilePath = openFileDialog.FileName;
                HTuple width, height;
                //判断文件路径是否为null或空
                if (!string.IsNullOrEmpty(FilePath))
                {
                    HOperatorSet.ReadImage(out Image, FilePath);
                    HOperatorSet.GetImageSize(Image, out width, out height);
                    HOperatorSet.SetPart(WindowID, 0, 0, height - 1, width - 1);
                    HOperatorSet.DispObj(Image, WindowID);
                    Log_Show("打开图片成功!");
                }
            }
            else
            {
                Log_Show("打开图片失败!");
            }

            bitmap = new Bitmap(openFileDialog.FileName);
            //Display_pictureBox.Image = bitmap;
            //Display_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;//设置Display_pictureBox.SizeMode为缩放模式
            //Display_pictureBox.Width = bitmap.Width;
            //Display_pictureBox.Height = bitmap.Height;

        }



        //窗口缩放
        private void BasicDemo_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / x;//宽放大倍数
            float newy = (this.Height) / y;//高放大倍数
            ControlAutoSize controlsAutosize = new ControlAutoSize();
            controlsAutosize.setControls(newx, newy, this);
        }

        //显示日志的方法
        //Action和Func委托，异步委托调用BeginInvoke和Endlnvoke方法，Lambda表达式
        private void Log_Show(string msg)
        {
            try
            {
                this.Log.BeginInvoke(new Action(() => { this.Log.Items.Add(DateTime.Now.ToString() + ":" + msg); }));

                this.Log.BeginInvoke(new Action(() => { this.Log.SelectedIndex = this.Log.Items.Count - 1; }));
            }
            catch (Exception e)
            {
                Log_Show("错误信息:" + e.Message);
            }

        }

        private void Save_Parameter_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认是否保存参数？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    SaveRunParameter();
                    Log_Show("参数保存成功!");
                }
                else
                {
                    Log_Show("取消参数保存!");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SaveRunParameter()
        {
            try
            {
                RP.listCamera[0].CamName = List_Camera.Text;
                RP.SearchAreaMax = Convert.ToDouble(SearchArea_Max.Text);
                RP.SearchAreaMin = Convert.ToDouble(SearchArea_Min.Text);
                RP.Roi_Radius = Convert.ToInt16(ROI_Radius.Text);
                RP.MedianMarkRaduis = Convert.ToInt16(MedianMaskRadius.Text);
                RP.MaxGyay = Convert.ToInt16(MaxGray.Text);
                RP.MinGyay = Convert.ToInt16(MinGray.Text);
                RP.IP = IP.Text;
                RP.Port = Convert.ToInt16(Port.Text);
                BasicArithmetic.SaveToXml(Application.StartupPath + "\\RunParameter.xml", RP, RP.GetType());
            }
            catch (Exception)
            {

                Log_Show("保存参数异常!");
            }
        }
        //显示参数
        public void ShowRunParameter()
        {
            try
            {

                SearchArea_Max.Text = Convert.ToString(RP.SearchAreaMax);
                SearchArea_Min.Text = Convert.ToString(RP.SearchAreaMin);
                ROI_Radius.Text = Convert.ToString(RP.Roi_Radius);
                MedianMaskRadius.Text = Convert.ToString(RP.MedianMarkRaduis);
                MaxGray.Text = Convert.ToString(RP.MaxGyay);
                MinGray.Text = Convert.ToString(RP.MinGyay);
                IP.Text = Convert.ToString(RP.IP);
                Port.Text = Convert.ToString(RP.Port);
            }
            catch (Exception)
            {

                Log_Show("加载参数异常");
            }
        }

        public void ReadParameter()
        {
            try
            {
                //显示转换，将object类型强转为RunParameter类型
                RP = (RunParameter)BasicArithmetic.LoadFromXml(Application.StartupPath + "\\RunParameter.xml", RP.GetType());
                //显示转换，将object类型强转为count类型
                count = (count)BasicArithmetic.LoadFromXml(Application.StartupPath + "\\Count.xml", count.GetType());

                iTotalNum = count.TotalNum;
                iBadNum = count.BadNum;
            }
            catch (Exception ex)
            {
                Log_Show("加载参数异常:" + ex.Message);
            }
            ShowRunParameter();
        }

        /// <summary>
        /// 显示产品总数、不良以及良率
        /// </summary>
        /// <param name="TotalNum">总数</param>
        /// <param name="BadNum">不良</param>
        public void DisplayProductCapacity(int TotalNum, int BadNum)
        {
            LabTotal.Text = "总数:" + TotalNum.ToString();
            LabBad.Text = "不良:" + BadNum.ToString();
            if (TotalNum == 0)
            {
                labYield.Text = "良率: 100%";
            }
            else
            {
                labYield.Text = "良率:" + Convert.ToString(((TotalNum - BadNum) / (double)TotalNum * 100).ToString("0.00")) + "%";
            }

        }

        public void SaveCount()
        {
            count.TotalNum = iTotalNum;
            count.BadNum = iBadNum;
            BasicArithmetic.SaveToXml(Application.StartupPath + "\\Count.xml", count, count.GetType());
        }
        /// <summary>
        /// 清零按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearNum_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("确认是否清零?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                iTotalNum = 0;
                iBadNum = 0;
                DisplayProductCapacity(0, 0);
                SaveCount();
                Log_Show("数量已清零！");
            }
            else
            {
                Log_Show("已取消清零操作！");
            }

        }


        /// <summary>
        /// 鼠标按下功能
        /// </summary>
        /// <param name="sender">句柄</param>
        /// <param name="e">事件</param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;
                isMove = true;
                Display_pictureBox.Focus();
            }
        }


        /// <summary>
        /// 鼠标松开功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMove = false;
            }
        }


        /// <summary>
        /// 鼠标移动功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Display_pictureBox.Focus();
            if (isMove)
            {
                int x, y;
                int moveX, moveY;
                moveX = Cursor.Position.X - mouseDownPoint.X;
                moveY = Cursor.Position.Y - mouseDownPoint.Y;
                x = Display_pictureBox.Location.X + moveX;
                y = Display_pictureBox.Location.Y + moveY;
                Display_pictureBox.Location = new Point(x, y);
                mouseDownPoint.X = Cursor.Position.X;
                mouseDownPoint.Y = Cursor.Position.Y;



            }
        }


        /// <summary>
        /// 鼠标滚轮滚动功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {


                int x = e.Location.X;
                int y = e.Location.Y;
                int ow = Display_pictureBox.Width;
                int oh = Display_pictureBox.Height;
                int VX, VY;
                if (e.Delta > 0)
                {
                    Display_pictureBox.Width += zoomStep;
                    Display_pictureBox.Height += zoomStep;

                    PropertyInfo pInfo = Display_pictureBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance |
                        BindingFlags.NonPublic);
                    Rectangle rect = (Rectangle)pInfo.GetValue(Display_pictureBox, null);

                    Display_pictureBox.Width = rect.Width;
                    Display_pictureBox.Height = rect.Height;
                }
                if (e.Delta < 0)
                {
                    if (bitmap != null)
                    {
                        if (Display_pictureBox.Width < bitmap.Width / 10)
                            return;

                        Display_pictureBox.Width -= zoomStep;
                        Display_pictureBox.Height -= zoomStep;
                        PropertyInfo pInfo = Display_pictureBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance |
                            BindingFlags.NonPublic);
                        Rectangle rect = (Rectangle)pInfo.GetValue(Display_pictureBox, null);
                        Display_pictureBox.Width = rect.Width;
                        Display_pictureBox.Height = rect.Height;
                    }
                }

                VX = (int)((double)x * (ow - Display_pictureBox.Width) / ow);
                VY = (int)((double)y * (oh - Display_pictureBox.Height) / oh);
                Display_pictureBox.Location = new Point(Display_pictureBox.Location.X + VX, Display_pictureBox.Location.Y + VY);

            }
            catch (Exception ex)
            {

                Log_Show("错误信息为:" + ex.Message);
            }
        }


        public void Display_MouseDouleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Display_pictureBox.Width = bitmap.Width;
                Display_pictureBox.Height = bitmap.Height;
                Display_pictureBox.Image = bitmap;

            }
        }


        //加载所有相机
        public void Load_Camera()
        {
            try
            {
                //检测摄像头
                _VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (_VideoDevices.Count == 0)
                {
                    throw new ApplicationException();
                }
                foreach (FilterInfo device in _VideoDevices)
                {
                    List_Camera.Items.Add(device.Name);
                }
                List_Camera.SelectedIndex = 0;
            }
            catch (Exception)
            {

                List_Camera.Items.Add("没有找到摄像头!");
                _VideoDevices = null;
            }

        }

        /// <summary>
        /// 关闭相机并释放相机资源
        /// </summary>
        public void ShutCamera()
        {
            if (VideoSourcePlayer1.VideoSource != null)
            {
                VideoSourcePlayer1.SignalToStop();
                VideoSourcePlayer1.WaitForStop();
                VideoSourcePlayer1.VideoSource = null;
            }
        }

        /// <summary>
        /// 连接相机
        /// </summary>
        public void ConnectCamera()
        {
            if (VideoSourcePlayer1.VideoSource != null)
            {
                MessageBox.Show("相机已打开，请勿重复打开!", "警告❗");
            }
            else
            {
                _VideoCapture = new VideoCaptureDevice(_VideoDevices[List_Camera.SelectedIndex].MonikerString);
                VideoSourcePlayer1.VideoSource = _VideoCapture;
                VideoSourcePlayer1.Start();
            }
        }

        private void open_camera_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectCamera();
                Log_Show("连接相机成功！");
            }
            catch (Exception ex)
            {

                Log_Show("连接相机失败！" + ex.Message);
            }

        }

        private void Single_frame_Click(object sender, EventArgs e)
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
            Bitmap2HObjectBpp24(bitmap, out Image);
            HOperatorSet.GetImageSize(Image, out width, out height);
            HOperatorSet.SetPart(WindowID, 0, 0, height - 1, width - 1);
            HOperatorSet.DispObj(Image, WindowID);

            //Display_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            //Display_pictureBox.Width = bitmap.Width;
            //Display_pictureBox.Height = bitmap.Height;
            //this.VideoSourcePlayer1.Visible = true;
            //this.Display_pictureBox.Visible = true;
            Log_Show("采集图像成功!");

        }
        /// <summary>
        /// 彩色bitmap转彩色Hobject
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="image"></param>
        public void Bitmap2HObjectBpp24(Bitmap bmp, out HObject image)
        {
            try
            {
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData srcBmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                HOperatorSet.GenImageInterleaved(out image, srcBmpData.Scan0, "bgr", bmp.Width, bmp.Height, 0, "byte", 0, 0, 0, 0, -1, 0);
                bmp.UnlockBits(srcBmpData);

            }
            catch (Exception ex)
            {
                CPublic.InsertNote("图片转换出错! 错误信息:" + ex.Message);
                HOperatorSet.GenEmptyObj(out image);
            }
        }

        private void Continuous_acquisition_Click(object sender, EventArgs e)
        {
            if (VideoSourcePlayer1.VideoSource == null)
            {
                MessageBox.Show("请先打开相机!", "提示");
                return;
            }
            //判断线程状态是否打开
            if (ThreadPhoto.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                ThreadPhoto.Start();
            }
            //判断线程状态是否停止或则被中断，停止或中断后重新实例化并且打开线程
            if (ThreadPhoto.ThreadState == System.Threading.ThreadState.Stopped || ThreadPhoto.ThreadState == System.Threading.ThreadState.Aborted)
            {
                ThreadPhoto = new Thread(new ThreadStart(ThreadPhotos));
                ThreadPhoto.Start();
            }
        }

        private void Stop_Camera_Click(object sender, EventArgs e)
        {
            ThreadPhoto_Stop = true;
            ShutCamera();
        }

        private void MainForm_ClosingForm(object sender, FormClosingEventArgs e)
        {
            if (ThreadPhoto != null && ThreadPhoto.IsAlive)
            {
                ThreadObject.Join(100);
                ThreadPhoto.Join(100);
            }
            this.Dispose();
            this.Close();
        }

    }
}