using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    internal class ControlAutoSize
    {
        /*该类包含两个方法：setTag和setControl
         * setTag记录当前窗体控件大小
         * setControl当窗体大小发生改变时，控件随之等比例改变
         */
        public void setTag(Control cons)
        {
            foreach (Control con in cons.Controls) 
            {
                con.Tag = con.Width +";"+ con.Height +";"+ con.Left +";"+ con.Top +";"+ con.Font.Size;
                if (con.Controls.Count >0)
                {
                    setTag(con);
                }
            }
        }

        public void setControls(float new_x,float new_y,Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                if (con.Tag != null)
                {
                    //根据窗体缩放比例确定控件的值
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    con.Width = Convert.ToInt32(Convert.ToSingle(mytag[0]) * new_x);//宽度
                    con.Height = Convert.ToInt32(Convert.ToSingle(mytag[1]) * new_y);//高度
                    con.Left = Convert.ToInt32(Convert.ToSingle(mytag[2]) * new_x);//左边距
                    con.Top = Convert.ToInt32(Convert.ToSingle(mytag[3]) * new_y);//顶边距
                    Single CurrentSize = Convert.ToSingle(mytag[4]) * new_y; //字体大小
                    con.Font = new System.Drawing.Font(con.Font.Name,CurrentSize,con.Font.Style,con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(new_x, new_y, con);
                    }
                }
            }
        }
    }
}
