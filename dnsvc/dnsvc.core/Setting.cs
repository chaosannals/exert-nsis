using System;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace DnSvc
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class Setting
    {
        public static string FilePath { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dnsvc.ini"); } }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string Get(string section, string key, string defaultValue = null, int size = 1024)
        {
            StringBuilder result = new StringBuilder(size);
            GetPrivateProfileString(section, key, defaultValue, result, size, FilePath);
            return result.ToString();
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetBool(string section, string key, bool defaultValue = false)
        {
            return bool.Parse(Get(section, key, defaultValue.ToString()));
        }

        /// <summary>
        /// 获取整型值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string section, string key, int defaultValue = 0)
        {
            return int.Parse(Get(section, key, defaultValue.ToString()));
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetNumber(string section, string key, double defaultValue = 0.0)
        {
            return double.Parse(Get(section, key, defaultValue.ToString()));
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long Set(string section, string key, string data)
        {
            return WritePrivateProfileString(section, key, data, FilePath);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string data, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder result, int size, string filePath);
    }
}
