using HalconDotNet;
using PublicOperation;

namespace Demo
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]  //ע���﷨ ����Main����
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();
            Application.Run(new RuningForm());  // ������Ϣѭ��
            Application.Exit();
        }
    }
}