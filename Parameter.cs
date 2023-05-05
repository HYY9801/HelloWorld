using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    [Serializable]
    public class RunParameter
    {
        /// <summary>
        /// 搜索面积最小值
        /// </summary>
        double i_SearchAreaMin = 0.0;
        public double SearchAreaMin
        {
            set { i_SearchAreaMin = value; }
            get { return i_SearchAreaMin; }
            
            
        }

        /// <summary>
        /// 搜索面积最大值
        /// </summary>
        double i_SearchAreaMax = 0.0;
        public double SearchAreaMax
        {
            set { i_SearchAreaMax = value; }
            get { return i_SearchAreaMax; }
        }


        /// <summary>
        /// 获取ROI区域半径
        /// </summary>
        int i_Roi_Radius = 700;
        public int Roi_Radius
        {
            set { i_Roi_Radius = value;}
            get { return i_Roi_Radius; }
        }
        /// <summary>
        /// 中值滤波掩膜半径
        /// </summary>
        int i_MedianMarkRaduis = 18;
        public  int MedianMarkRaduis
        {
            set { i_MedianMarkRaduis = value;}
            get { return i_MedianMarkRaduis; }
        }
        /// <summary>
        /// 二值化最小灰度值
        /// </summary>
        int i_MinGyay = 0;
        public int MinGyay
        {
            set { i_MinGyay = value;}
            get { return i_MinGyay; }
        }
        /// <summary>
        /// 二值化最大灰度值
        /// </summary>
        int i_MaxGyay = 138;
        public int MaxGyay
        {
            set { i_MaxGyay = value;}
            get { return i_MaxGyay;} 
        }

        private string i_IP = "192.168.159.161";

        public string IP
        {
            set { i_IP = value; }
            get { return i_IP; }
        }

        private int i_Port = 2000;

        public int Port
        {
            set { i_Port = value; }
            get { return i_Port; }
        }

        public List<Camera> list_Camera = new List<Camera>();

        public List<Camera> listCamera
        {
            set { list_Camera = value; }
            get { return list_Camera; }
        }

    }

    public class count
    {
        /// <summary>
        /// 总数
        /// </summary>
        private int i_TotalNum = 0;

        public int TotalNum
        {
            set { i_TotalNum = value;}
            get { return i_TotalNum; }
        }

        /// <summary>
        /// 不良
        /// </summary>
        private int i_BadNum = 0;
        public int BadNum
        {
            set { i_BadNum = value;}
            get { return i_BadNum; }
        }

        

    }

    public class Camera
    {
        private string str_CamName = "DeviceX";
        /// <summary>
        /// 相机名称
        /// </summary>
        public string CamName
        {
            get { return str_CamName; }
            set { str_CamName = value; }
        }
    }


}
