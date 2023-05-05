using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo;
namespace Demo
{
    internal class ImageOperation
    {
        /// <summary>
        /// 鼠标按下时的横纵坐标
        /// </summary>
        HTuple RowDown, ColDown;

        /// <summary>
        /// 图像放大与缩小
        /// </summary>
        /// <param name="sender">窗口句柄</param>
        /// <param name="e">操作类型</param>
        /// <param name="image">图像</param>
        //private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    int x = e.Location.X;
        //    int y = e.Location.Y;

        //    int ow = Dispplay_pictureBox1.Width;
        //    int oh = pictureBox1.Height;
        //    int VX, VY;
        //    if (e.Delta > 0)
        //    {
        //        pictureBox1.Width += zoomStep;
        //        pictureBox1.Height += zoomStep;

        //        PropertyInfo pInfo = pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance |
        //            BindingFlags.NonPublic);
        //        Rectangle rect = (Rectangle)pInfo.GetValue(pictureBox1, null);

        //        pictureBox1.Width = rect.Width;
        //        pictureBox1.Height = rect.Height;
        //    }
        //    if (e.Delta < 0)
        //    {

        //        if (pictureBox1.Width < myBmp.Width / 10)
        //            return;

        //        pictureBox1.Width -= zoomStep;
        //        pictureBox1.Height -= zoomStep;
        //        PropertyInfo pInfo = pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance |
        //            BindingFlags.NonPublic);
        //        Rectangle rect = (Rectangle)pInfo.GetValue(pictureBox1, null);
        //        pictureBox1.Width = rect.Width;
        //        pictureBox1.Height = rect.Height;
        //    }

        //    VX = (int)((double)x * (ow - pictureBox1.Width) / ow);
        //    VY = (int)((double)y * (oh - pictureBox1.Height) / oh);
        //    pictureBox1.Location = new Point(pictureBox1.Location.X + VX, pictureBox1.Location.Y + VY);
        //}

        /// <summary>
        /// 鼠标摁下
        /// </summary>
        /// <param name="sender">窗口句柄</param>
        /// <param name="e">操作类型</param>
        /// <param name="image">原始图像</param>
        /// <param name="hv_imageWidth">图像的宽</param>
        /// <param name="hv_imageHeight">图像的高</param>
        /// <param name="isMouseDown">是否摁下了鼠标左键</param>
        public void MouseDown(object sender, HMouseEventArgs e, HObject image, HTuple hv_imageWidth, HTuple hv_imageHeight, ref bool isMouseDown)
        {
            if (image.CountObj() > 0 || image != null)//必须有图的情况下使用
            {
                try
                {
                    HWindowControl hWindowControl = sender as HWindowControl;
                    HTuple Row, Column, Button;
                    //返回鼠标当前按下点的图像坐标
                    HOperatorSet.GetMposition(hWindowControl.HalconWindow, out Row, out Column, out Button);
                    RowDown = Row;    //鼠标按下时的行坐标
                    ColDown = Column; //鼠标按下时的列坐标
                    if (Button.I == 1)//左键嗯下值为1
                        isMouseDown = true;
                    if (Button.I == 4)//鼠标右键恢复原图
                    {
                        HOperatorSet.SetPart(hWindowControl.HalconWindow, 0, 0, hv_imageHeight, hv_imageWidth);
                        HOperatorSet.DispObj(image, hWindowControl.HalconWindow);
                    }
                }
                catch (Exception)
                {
                }
            }
        }


        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender">窗口句柄</param>
        /// <param name="e">操作类型</param>
        /// <param name="image">原始图像</param>
        /// <param name="isMouseDown">是否摁下的鼠标左键</param>
        /// <param name="GrayMsg">返回鼠标当前位置的灰度值</param>
        public void MouseMove(object sender, HMouseEventArgs e, HObject image, bool isMouseDown, out string GrayMsg)
        {
            GrayMsg = "";
            if (image.CountObj() > 0 || image != null)//必须有图的情况下使用
            {
                try
                {
                    HWindowControl hWindowControl = sender as HWindowControl;
                    HObject CurrImage = null;
                    HOperatorSet.GenEmptyObj(out CurrImage);
                    CurrImage = image.Clone();
                    HTuple Row = new HTuple(), Column = new HTuple(), Button = new HTuple(), pointGray = new HTuple();
                    HTuple hv_Width = new HTuple();
                    HTuple hv_Height = new HTuple();
                    if (CurrImage != null)
                    {
                        HOperatorSet.GetImageSize(CurrImage, out hv_Width, out hv_Height);
                    }
                    //获取当前鼠标的坐标值
                    HOperatorSet.GetMposition(hWindowControl.HalconWindow, out Row, out Column, out Button);
                    //获取当前点的灰度值
                    if (hv_Height != null && (Row > 0 && Row < hv_Height) && (Column > 0 && Column < hv_Width))//设置3个条件项，防止程序崩溃。
                        HOperatorSet.GetGrayval(CurrImage, Row, Column, out pointGray);
                    else
                        pointGray = "_";
                    GrayMsg = string.Format("Row:{0}  Col:{1}  Gray:{2}", Row, Column, pointGray);

                    if (isMouseDown)
                    {
                        HTuple row1, col1, row2, col2;
                        double RowMove = Row - RowDown;   //鼠标弹起时的行坐标减去按下时的行坐标，得到行坐标的移动值
                        double ColMove = Column - ColDown;//鼠标弹起时的列坐标减去按下时的列坐标，得到列坐标的移动值
                                                          //得到当前的窗口坐标
                        HOperatorSet.GetPart(hWindowControl.HalconWindow, out row1, out col1, out row2, out col2);
                        //移动后的左上角和右下角坐标，这里可能有些不好理解。以左上角原点为参考点
                        HOperatorSet.SetPart(hWindowControl.HalconWindow, row1 - RowMove, col1 - ColMove, row2 - RowMove, col2 - ColMove);
                        HOperatorSet.ClearWindow(hWindowControl.HalconWindow);
                        if (CurrImage != null)
                            HOperatorSet.DispObj(CurrImage, hWindowControl.HalconWindow);
                    }
                }
                catch (Exception)
                {
                }
            }
        }


        /// <summary>
        /// 鼠标松开
        /// </summary>
        /// <param name="sender">窗口句柄</param>
        /// <param name="e">操作类型</param>
        /// <param name="image">原始图像</param>
        /// <param name="isMouseDown">返回是否松开了鼠标左键</param>
        public void MouseUp(object sender, HMouseEventArgs e, HObject image, ref bool isMouseDown)
        {
            if (image.CountObj() > 0 || image != null)//必须有图的情况下使用
            {
                try
                {
                    if (isMouseDown)
                    {
                        isMouseDown = false;
                        HWindowControl hWindowControl = sender as HWindowControl;
                        HObject CurrImage = null;
                        HOperatorSet.GenEmptyObj(out CurrImage);
                        CurrImage = image.Clone();
                        HTuple row1, col1, row2, col2, Row, Column, Button;
                        //获取当前鼠标的坐标值
                        HOperatorSet.GetMposition(hWindowControl.HalconWindow, out Row, out Column, out Button);
                        double RowMove = Row - RowDown;   //鼠标弹起时的行坐标减去按下时的行坐标，得到行坐标的移动值
                        double ColMove = Column - ColDown;//鼠标弹起时的列坐标减去按下时的列坐标，得到列坐标的移动值
                                                          //得到当前的窗口坐标
                        HOperatorSet.GetPart(hWindowControl.HalconWindow, out row1, out col1, out row2, out col2);
                        //移动后的左上角和右下角坐标，这里可能有些不好理解。以左上角原点为参考点
                        HOperatorSet.SetPart(hWindowControl.HalconWindow, row1 - RowMove, col1 - ColMove, row2 - RowMove, col2 - ColMove);
                        HOperatorSet.ClearWindow(hWindowControl.HalconWindow);
                        if (CurrImage != null)
                            HOperatorSet.DispObj(CurrImage, hWindowControl.HalconWindow);
                    }
                }
                catch (Exception)
                {
                }
            }
        }





    }
}
