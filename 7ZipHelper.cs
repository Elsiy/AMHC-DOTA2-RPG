using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace AMHC_SERVER_CLIENT
{
    class _7ZipHelper
    {
        // Fields
        private static string _7zInstallPath;

        // Methods
        public static void ZipHelper(string str7zInstallPath)
        {
            _7zInstallPath = str7zInstallPath;
            //return _7zInstallPath;
        }

        /// <summary>
        /// 压缩文件夹目录
        /// </summary>
        /// <param name="strInDirectoryPath">指定需要压缩的目录，如C:\test\，将压缩test目录下的所有文件</param>
        /// <param name="strOutFilePath">压缩后压缩文件的存放目录</param>
        public void CompressDirectory(string strInDirectoryPath, string strOutFilePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = _7zInstallPath;
            process.StartInfo.Arguments = " a -t7z " + strOutFilePath + " " + strInDirectoryPath + " -r";
            //隐藏DOS窗口
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="strInFilePath">指定需要压缩的文件，如C:\test\demo.xlsx，将压缩demo.xlsx文件</param>
        /// <param name="strOutFilePath">压缩后压缩文件的存放目录</param>
        public void CompressFile(string strInFilePath, string strOutFilePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = _7zInstallPath;
            process.StartInfo.Arguments = " a -t7z " + strOutFilePath + " " + strInFilePath + "";
            //隐藏DOS窗口
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="strInFilePath">压缩文件的路径</param>
        /// <param name="strOutDirectoryPath">解压缩后文件的路径</param>
        public static Boolean DecompressFileToDestDirectory(string strInFilePath, string strOutDirectoryPath)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = _7zInstallPath;
                process.StartInfo.Arguments = " x " + strInFilePath + " -o" + strOutDirectoryPath + " -r ";
                //隐藏DOS窗口
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
                process.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }


    }
}
