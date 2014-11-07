using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ed.Common
{
    /// <summary>
    /// 自动生成唯一键值
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// 生成唯一客户编号
        /// </summary>
        /// <returns></returns>
        public static string GetCustomerNumber(string depCode, int maxId)
        {

            string longId = maxId.ToString();
            var numlength = maxId.ToString().Trim().Length;
            if (numlength < 4)
            {
                var zero_count = 4 - numlength;
                for (int i = 0; i < zero_count; i++)
                {
                    longId = String.Concat(0, longId);
                }
            }
            string day_num = DateTime.Now.ToString("yyMMdd"); //yyyyMMddHHmmssms
            var clientCode = new object[] { "KH", depCode,day_num, 0, longId };
            return String.Concat(clientCode);
        }

        /// <summary>
        /// 生成唯一订单编号
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNumber()
        {

            string num = DateTime.Now.ToString("yyMMddHHmmss"); //yyyyMMddHHmmssms
            return "DD" + num + Number(2).ToString();
        }
     

        /// <summary>
        /// 生成唯一月嫂编号
        /// </summary>
        /// <returns></returns>
        public static string GetYuesaoNumber(string depCode,int maxId)
        {

            string longId = maxId.ToString();
            var numlength = maxId.ToString().Trim().Length;
            if (numlength<4)
            {
                var zero_count = 4 - numlength;
                for (int i = 0; i < zero_count; i++)
                {
                    longId = String.Concat(0, longId);
                }
            }
            var yuesaoCode = new object[] { "YS", depCode,0,longId };
             return String.Concat(yuesaoCode);
        }


        /// <summary>
        /// 生成唯一编号
        /// </summary>
        /// <returns></returns>
        public static string GetGuidNumber()
        {
            return Guid.NewGuid().ToString("D");
        }



        #region 生成指定长度的字符串

        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }

            return ReturnStr;
        }

        #endregion

     

        #region 生成日期随机码

        /// <summary>
        /// 生成日期随机码
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            #region

            return DateTime.Now.ToString("yyyyMMddHHmmssffff");

            #endregion
        }

        #endregion

 

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns></returns>
        public static string Number(int Length)
        {
            return Number(Length, false);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        /// <summary>
        /// 生成随机字母字符串(数字字母混和)
        /// </summary>
        /// <param name="codeCount">待生成的位数</param>
        public static string GetCheckCode(int codeCount)
        {
            string str = string.Empty;
            int rep = 0;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
        private static int Next(int numSeeds, int length)
        {
            byte[] buffer = new byte[length];
            System.Security.Cryptography.RNGCryptoServiceProvider Gen =
                new System.Security.Cryptography.RNGCryptoServiceProvider();
            Gen.GetBytes(buffer);
            uint randomResult = 0x0; //这里用uint作为生成的随机数  
            for (int i = 0; i < length; i++)
            {
                randomResult |= ((uint)buffer[i] << ((length - 1 - i) * 8));
            }
            return (int)(randomResult % numSeeds);
        }

 

    }
}