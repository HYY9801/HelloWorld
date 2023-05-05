using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Demo
{
    public static class BasicArithmetic
    {

        /// <summary>
        /// 复制文件夹以及文件
        /// </summary>
        /// <param name="sourceFolder">原文件路径</param> 
        /// <param name="destFolder">目标文件路径</param> 
        public static void CopyFolder(string sourceFolder, string destFolder) 
        {
            try
            {
                if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }
                //得到原文件根目录下所有文件
                string[] files = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string file in files) 
                {
                    string name = System.IO.Path.GetFileName(file);//得到文件名称
                    string dest = System.IO.Path.Combine(destFolder,name);//将文件名和目标路径合并
                    System.IO.File.Copy(name,dest);//复制文件
                }
                //得到原文件根目录下所有文件夹
                string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = System.IO.Path.GetFileName(folder);
                    string dest = System.IO.Path.Combine(destFolder,name);
                    CopyFolder(folder,dest); //构建目标路径，递归复制文件
                }
            }
            catch (Exception e)
            {

                MessageBox.Show("错误信息:"+e.Message,"警告");
                return;
            }
        }

        //保存为XML文件
        public static void SaveToXml(string s_FilePath, object s_SourceObj, Type type)
        {
            if (!string.IsNullOrWhiteSpace(s_FilePath) && s_SourceObj != null)
            {
                type = ((type != (Type)null) ? type : s_SourceObj.GetType());
                using (StreamWriter textWriter = new StreamWriter(s_FilePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                    XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add("", "");
                    xmlSerializer.Serialize(textWriter, s_SourceObj, xmlSerializerNamespaces);
                }
            }
        }

        //加载XML文件
        public static object LoadFromXml(string s_FilePath, Type type)
        {
            object result = null;
            if (File.Exists(s_FilePath))
            {
                using (StreamReader textReader = new StreamReader(s_FilePath, System.Text.Encoding.UTF8))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                    result = xmlSerializer.Deserialize(textReader);
                }
            }
            return result;
        }
    }
}
