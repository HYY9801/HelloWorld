using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Threading;

namespace Demo
{
    public class HikVisionCamera
    {
        bool bContinueGrab = false;
        HObject image;
        bool bGrabComplete = false;

        public MyCamera.cbOutputExdelegate ImageCallback;//相机回调函数
        public MyCamera m_pMyCamera = new MyCamera();    //相机
        public bool m_bIsColor;                          //是否彩色相机，true为彩色，false为黑白


        //pData：图像数据地址；pFrameInfo：图像帧信息；pUser：用户自定义参数
        public void ImageCallbackFunc(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            try
            {
                if (image != null)
                    image.Dispose();
                //把图像数据生成halcon图像
                if (!m_bIsColor)
                {
                    //如果是黑白相机，
                    HOperatorSet.GenImage1(out image, "byte", pFrameInfo.nWidth, pFrameInfo.nHeight, (HTuple)pData);
                }
                else
                //如果是彩色相机
                {
                    uint g_nPayloadSize = 0;
                    MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                    int nRet = m_pMyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                    if (MyCamera.MV_OK != nRet)
                    {
                        //MessageBox.Show("Get PayloadSize Fail");
                        return;
                    }
                    g_nPayloadSize = stParam.nCurValue;

                    IntPtr pImageBuffer = Marshal.AllocHGlobal((int)g_nPayloadSize * 3);
                    //IntPtr pImageBuffer = IntPtr.Zero;
                    if (pFrameInfo.enPixelType != MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                    {
                        ConvertToRGB(m_pMyCamera, pData, pFrameInfo.nHeight, pFrameInfo.nWidth, pFrameInfo.enPixelType, pImageBuffer);
                    }
                    HOperatorSet.GenImageInterleaved(out image, (HTuple)pImageBuffer, "rgb", pFrameInfo.nWidth, pFrameInfo.nHeight, 0, "byte", 0, 0, 0, 0, 8, 0);
                    if (pImageBuffer != IntPtr.Zero) { Marshal.FreeHGlobal(pImageBuffer); }
                }
                //if (image != null && image.IsInitialized() && image.CountObj() > 0)
                //{
                //    //if (RgbToGray)
                //    //    HOperatorSet.Rgb1ToGray(image, out image);
                //    ////图像翻转--0不翻转、1水平翻转、2垂直翻转
                //    //if (MirrorImage.Equals(1))
                //    //    HOperatorSet.MirrorImage(image, out image, "column");
                //    //else if (MirrorImage.Equals(2))
                //    //    HOperatorSet.MirrorImage(image, out image, "row");
                //    ////图像旋转--0不旋转、1旋转90度、2旋转180度、3旋转270度
                //    //if (RotateImage > 0)
                //    //HOperatorSet.RotateImage(image, out image, 90 * RotateImage, "constant");
                //}
                //采图完成
                bGrabComplete = true;
                pData = IntPtr.Zero;
                //m_pMyCamera.MV_CC_ClearImageBuffer_NET();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private bool IsColorPixelFormat(MyCamera.MvGvspPixelType enType)
        {
            switch (enType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGBA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BGRA8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                    return true;
                default:
                    return false;
            }
        }
        public Int32 ConvertToRGB(object obj, IntPtr pSrc, ushort nHeight, ushort nWidth, MyCamera.MvGvspPixelType nPixelType, IntPtr pDst)
        {
            if (IntPtr.Zero == pSrc || IntPtr.Zero == pDst)
            {
                return MyCamera.MV_E_PARAMETER;
            }

            int nRet = MyCamera.MV_OK;
            MyCamera device = obj as MyCamera;
            MyCamera.MV_PIXEL_CONVERT_PARAM stPixelConvertParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stPixelConvertParam.pSrcData = pSrc;//源数据
            if (IntPtr.Zero == stPixelConvertParam.pSrcData)
            {
                return -1;
            }
            stPixelConvertParam.nWidth = nWidth;//图像宽度
            stPixelConvertParam.nHeight = nHeight;//图像高度
            stPixelConvertParam.enSrcPixelType = nPixelType;//源数据的格式
            stPixelConvertParam.nSrcDataLen = (uint)(nWidth * nHeight * ((((uint)nPixelType) >> 16) & 0x00ff) >> 3);

            stPixelConvertParam.nDstBufferSize = (uint)(nWidth * nHeight * ((((uint)MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed) >> 16) & 0x00ff) >> 3);
            stPixelConvertParam.pDstBuffer = pDst;//转换后的数据
            stPixelConvertParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            stPixelConvertParam.nDstBufferSize = (uint)nWidth * nHeight * 3;

            nRet = device.MV_CC_ConvertPixelType_NET(ref stPixelConvertParam);//格式转换
            if (MyCamera.MV_OK != nRet)
            {
                return -1;
            }
            return MyCamera.MV_OK;
        }

        public bool CloseCamera()
        {
            try
            {
                //设置相机工触发模式--0:连续模式 1:触发模式
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
                //停止采集
                m_pMyCamera.MV_CC_StopGrabbing_NET();
                //关闭相机
                m_pMyCamera.MV_CC_CloseDevice_NET();
                //销毁相机
                m_pMyCamera.MV_CC_DestroyDevice_NET();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        private List<string> enumerateCameras()
        {
            //垃圾回收
            GC.Collect();
            GC.WaitForPendingFinalizers();

            List<string> List_CameraSN = new List<string>();

            MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
            if (0 != nRet)
            {
                Console.WriteLine("Enumerate devices fail!", nRet);
                return List_CameraSN;
            }

            //获取设备枚举
            for (int i = 0; i < m_pDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                    List_CameraSN.Add(gigeInfo.chSerialNumber);
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));

                    List_CameraSN.Add(usbInfo.chSerialNumber);
                }
            }

            return List_CameraSN;
        }


        private Action ManualAcquireCompleteEvent;
        public bool OneGrab(double d_Exposure, ref HObject ho_Image)
        {
            try
            {
                bool bRtn = true;

                if (!setExposureTime(d_Exposure))
                {
                    return false;
                }

                if (false)
                {
                    //设置触发源--0:Line0 1:Line1 2:Line2  3:Line3  4:Counter  7:Software;
                    if (m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 0) != 0)
                        bRtn = false;
                }
                else
                {
                    //m_pMyCamera.MV_CC_ClearImageBuffer_NET();
                    //设置触发源--0:Line0 1:Line1 2:Line2  3:Line3  4:Counter  7:Software;
                    //if (m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7) != 0)
                    //    bRtn = false;
                    //软触发单帧采图
                    if (m_pMyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware") != 0)
                        bRtn = false;
                }
                if (!bRtn)
                {
                    return false;
                }
                else
                {
                    //
                    //将单帧采集设定为阻塞模式
                    //
                    int i_time = 3000;
                    while (true)
                    {
                        if (bGrabComplete)
                        {
                            bGrabComplete = false;
                            HOperatorSet.CopyImage(image, out ho_Image);
                            return true;
                        }
                        else
                        {
                            if (i_time == 0)
                            {
                                ho_Image = null;
                                return false;
                            }
                            i_time -= 20;
                            Thread.Sleep(20);
                        }

                    }
                }
            }
            catch
            {
                return false;
            }
        }


        public bool OneGrab(double d_Exposure, ref HObject ho_Image, out string s_Str)
        {
            bool bRtn = true;
            s_Str = "";
            if (setExposureTime(d_Exposure))
            {
                return false;
            }

            if (false)
            {
                //设置触发源--0:Line0 1:Line1 2:Line2  3:Line3  4:Counter  7:Software;
                if (m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 0) != 0)
                    bRtn = false;
            }
            else
            {
                //设置触发源--0:Line0 1:Line1 2:Line2  3:Line3  4:Counter  7:Software;
                if (m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7) != 0)
                    bRtn = false;
                //软触发单帧采图
                if (m_pMyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware") != 0)
                    bRtn = false;
            }

            if (!bRtn)
            {
                return false;
            }
            else
            {
                //
                //将单帧采集设定为阻塞模式
                //
                int i_time = 3000;
                while (true)
                {
                    if (bGrabComplete)
                    {
                        bGrabComplete = false;
                        HOperatorSet.CopyImage(image, out ho_Image);
                        return true;
                    }
                    else
                    {
                        if (i_time == 0)
                        {
                            ho_Image = null;
                            return false;
                        }
                        i_time -= 20;
                        Thread.Sleep(20);
                    }

                }


            }
        }

        public bool OpenCamera(string str_CamareName)
        {
            try
            {
                MyCamera.MV_CC_DEVICE_INFO_LIST pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
                int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref pDeviceList);
                if (0 != nRet)
                {
                    Console.WriteLine("Get PayloadSize failed");
                    return false;
                }

                bool bSN_OK = false;
                int nIndex;
                MyCamera.MV_CC_DEVICE_INFO device = new MyCamera.MV_CC_DEVICE_INFO();
                #region SN

                #endregion
                int iDeviceFlag = -1;
                for (int i = 0; i < pDeviceList.nDeviceNum; i++)
                {
                    MyCamera.MV_CC_DEVICE_INFO device_info = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(pDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                    if (device_info.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device_info.SpecialInfo.stGigEInfo, 0);
                        MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        if (gigeInfo.chUserDefinedName == str_CamareName)
                        {
                            iDeviceFlag = i;
                            break;
                        }
                    }
                    else if (device_info.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device_info.SpecialInfo.stUsb3VInfo, 0);
                        MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        if (usbInfo.chUserDefinedName == str_CamareName)
                        {
                            iDeviceFlag = i;
                            break;
                        }
                    }
                }
                if (iDeviceFlag == -1)
                    return false;
                else
                {
                    // ch:获取选择的设备信息 | en:Get selected device information
                    device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(pDeviceList.pDeviceInfo[iDeviceFlag], typeof(MyCamera.MV_CC_DEVICE_INFO));

                    // ch:打开设备 | en:Open device
                    if (null == m_pMyCamera)
                    {
                        m_pMyCamera = new MyCamera();
                        if (null == m_pMyCamera)
                            return false;
                    }
                    //创建相机
                    nRet = m_pMyCamera.MV_CC_CreateDevice_NET(ref device);
                }
                if (0 != nRet)
                {
                    return false;
                }
                //打开相机
                nRet = m_pMyCamera.MV_CC_OpenDevice_NET();
                if (0 != nRet)
                {
                    return false;
                }
                Console.WriteLine("打开相机成功");


                MyCamera.MV_IMAGE_BASIC_INFO pstInfo = new MyCamera.MV_IMAGE_BASIC_INFO();
                //查询相机是黑白相机还是彩色相机
                nRet = m_pMyCamera.MV_CC_GetImageInfo_NET(ref pstInfo);
                MyCamera.MvGvspPixelType m_nPixelType = (MyCamera.MvGvspPixelType)pstInfo.enPixelType; ;//图像像素类型
                //创建图像缓存
                if (pstInfo.enPixelType == (uint)(MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8))
                {
                    m_bIsColor = false;
                }
                else
                {
                    m_bIsColor = true;
                }



                //设置采集连续模式--0:SingleFrame 1:MultiFrame 2:Continuous
                m_pMyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);
                //设置相机工触发模式--0:连续模式 1:触发模式
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
                //设置触发源--0:Line0 1:Line1 2:Line2  3:Line3  4:Counter  7:Software;
                m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7);


                //注册相机回调函数，在回调函数中获取图像数据，进行图像处理
                try
                {
                    ImageCallback = new MyCamera.cbOutputExdelegate(ImageCallbackFunc);
                    int bRet = m_pMyCamera.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero);
                    if (0 != bRet)
                    {
                        Console.WriteLine("Cam0相机注册回调失败!");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cam0相机注册回调失败!" + ex.ToString());
                    return false;
                }

                //相机开始采集
                m_pMyCamera.MV_CC_StartGrabbing_NET();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected bool startLiveDisplay(HWindow windowHandle)
        {
            //设置相机工触发模式--0:连续模式 1:触发模式
            m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
            //hContinueGrabWinHandle = windowHandle;
            bContinueGrab = true;
            return true;
        }

        protected bool stopLiveDisplay()
        {
            //设置相机工触发模式--0:连续模式 1:触发模式
            m_pMyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
            bContinueGrab = false;
            return true;
        }

        protected double getCurrentExposureTime()
        {
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            if (m_pMyCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam) == 0)
            {
                return (double)stParam.fCurValue;
            }
            else
            {
                return -1;
            }
        }
        //protected override bool getMinMaxExposureTime()
        //{
        //    bool bRtn = true;
        //    MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
        //    if (m_pMyCamera.MV_CC_GetIntValue_NET("AutoExposureTimeLowerLimit", ref stParam) == 0)
        //    {
        //        MinExposureTime = (int)stParam.nCurValue;
        //    }
        //    else
        //    {
        //        bRtn = false;
        //    }
        //    if (m_pMyCamera.MV_CC_GetIntValue_NET("AutoExposureTimeUpperLimit", ref stParam) == 0)
        //    {
        //        MaxExposureTime = (int)stParam.nCurValue;
        //    }
        //    else
        //    {
        //        bRtn = false;
        //    }
        //    return bRtn;
        //}
        protected bool setExposureTime(double exposureTime)
        {
            if (m_pMyCamera.MV_CC_SetFloatValue_NET("ExposureTime", (float)exposureTime) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //protected override double[] getMinMaxCameraGain()
        //{
        //    bool bRtn = true;
        //    MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
        //    if (m_pMyCamera.MV_CC_GetFloatValue_NET("AutoGainLowerLimit", ref stParam) == 0)
        //    {
        //        MinCameraGain = (double)stParam.fCurValue;
        //    }
        //    else
        //    {
        //        bRtn = false;
        //    }
        //    if (m_pMyCamera.MV_CC_GetFloatValue_NET("AutoGainUpperLimit", ref stParam) == 0)
        //    {
        //        MaxCameraGain = (double)stParam.fCurValue;
        //    }
        //    else
        //    {
        //        bRtn = false;
        //    }
        //    return bRtn;
        //}

        protected double getCurrentCameraGain()
        {
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            if (m_pMyCamera.MV_CC_GetFloatValue_NET("Gain", ref stParam) == 0)
            {
                return (double)stParam.fCurValue;
            }
            else
            {
                return -1;
            }
        }
        protected bool setCameraGain(double cameraGain)
        {
            if (m_pMyCamera.MV_CC_SetFloatValue_NET("Gain", (float)cameraGain) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
