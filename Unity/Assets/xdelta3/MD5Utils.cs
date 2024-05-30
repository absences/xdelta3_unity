using System.Security.Cryptography;
using System;
using System.Text;
using System.IO;


	public class MD5Utils
	{
		public static string Md5(string source)
		{
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(source));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; ++i)
            {
                sb.Append(result[i].ToString("x2"));
            }
            return sb.ToString();
		}

        public static string Md5(System.IO.Stream sm)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(sm);
            //string str = System.BitConverter.ToString(hash_byte);
            //str = str.Replace("-", "");
            //return str;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; ++i)
            {
                sb.Append(result[i].ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取文件的MD5
        /// </summary>
        public static string FileMD5(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return Md5(fs);
                }
            }
            catch (Exception e)
            {
               // YooLogger.Exception(e);
                return string.Empty;
            }
        }
    }


