using System;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.IO;

namespace Tom.Common
{
    public class GYF
    {
        private static string defaultKey = "Yestock";
        /// <summary>
        /// 向文件中写日志
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="filename">文件名</param>
        public static void WriteLog(string str, string filename)
        {
            str = DateTime.Now.ToString() + ":  " + str + "\r\n\r\n";
            string url, dir;
            if (HttpContext.Current != null)
            {
                url = HttpContext.Current.Request.ServerVariables["URL"];
                int idx = url.IndexOf("/", 2);
                if (idx < 2)
                    url = "";
                else
                    url = "/" + url.Substring(1, idx - 1);
                dir = HttpContext.Current.Server.MapPath(url + "/LOG/");
            }
            else
            {
                //throw new Exception("No Context!");
                dir = AppDomain.CurrentDomain.BaseDirectory + "\\log\\";
            }
            if (filename == null || filename.Length == 0)
                filename = DateTime.Now.ToString("yyyyMMdd");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string filePath = dir + filename + ".txt";
            System.IO.FileInfo file = new FileInfo(filePath);
            try
            {
                if (!file.Exists)
                    using (StreamWriter sw = file.CreateText())
                    {
                        sw.WriteLine(str);
                        sw.Close();
                    }
                else
                {
                    StreamWriter sw = file.AppendText();

                    sw.WriteLine(str);
                    sw.Close();
                }
            }
            catch (Exception)
            {
                //


            }

        }
        public static void WriteLog(Exception ex, string filename)
        {
            WriteLog(ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace, filename);
        }
        public static void WriteLogEx(Exception ex, string extStr, string filename)
        {
            WriteLog(ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace + "\r\n ext:" + extStr ?? "", filename);
        }
        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="source">源数据</param>
        /// <returns></returns>
        public static string Translate(string source)
        {
            return Translate(source, defaultKey, null);
        }
        /// <summary>
        /// 动态暗语加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dymstr"></param>
        /// <returns></returns>
        public static string TranslateDym(string source,string dymstr)
        {
            dymstr = dymstr ?? "";
            return Translate(source,dymstr+defaultKey);
        }
        public static string Translate(byte[] bitSource)
        {
            return Translate(bitSource, defaultKey, null);
        }

        public static string TranslateRD(string source)
        {
            return TranslateRD(source, 2);
        }
        /// <summary>
        /// 添加随机变量
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string TranslateRD(string source, int rndLen)
        {
            return TranslateRD(source, defaultKey, null, rndLen);
        }
        public static string TranslateRD(string source, string crypto, string numCode, int rndLen)
        {
            return TranslateRD(source, crypto, numCode, Encoding.Default, rndLen);
        }
        public static string TranslateRD(string source, string crypto, string numCode, Encoding ed, int rndLen)
        {
            if (string.IsNullOrEmpty(source)) return "";
            if (rndLen < 1 || rndLen > 4) rndLen = 2;
            string tmp = DateTime.Now.ToString("ssfff"); ;
            source = source + tmp.Substring(tmp.Length - rndLen);
            byte[] bts = ed.GetBytes(source);
            return Translate(bts, crypto, numCode);
        }
        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="source">源数据</param>
        /// <param name="crypto">暗语</param>
        /// <returns>加密后数据</returns>
        public static string Translate(string source, string crypto)
        {
            return Translate(source, crypto, null);
        }
        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="source">源数据</param>
        /// <param name="crypto">暗语</param>
        /// <param name="numCode">编码基码</param>
        /// <returns></returns>
        public static string Translate(string source, string crypto, string numCode)
        {
            return Translate(source, crypto, numCode, Encoding.Default);
        }

        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="source">源数据</param>
        /// <param name="crypto">暗语</param>
        /// <param name="numCode">编码基码</param>
        /// <param name="ed">编码方式</param>
        /// <returns></returns>
        public static string Translate(string source, string crypto, string numCode, Encoding ed)
        {
            if (string.IsNullOrEmpty(source)) return "";
            byte[] bts = ed.GetBytes(source);
            return Translate(bts, crypto, numCode);
        }
        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="bitSource">源数据</param>
        /// <param name="crypto">暗语</param>
        /// <param name="numCode">编码基码</param>
        /// <returns></returns>
        public static string Translate(byte[] bitSource, string crypto, string numCode)
        {
            string NumCode = "bPsMYnAW8vmxeaDFyS@k3V2qJ!N9URrgG7otliBT6XpOCKwuL4QHhfzj5cd1ZI0E";//编码Key
            if (bitSource == null || bitSource.Length < 2 || string.IsNullOrEmpty(crypto))
                return "";
            if (numCode == null || numCode.Length != 64) 
                numCode = NumCode;
            //加密
            byte[] bts = bitSource;
            byte[] btgoal = new byte[bts.Length + 1];
            byte last;
            int clen = crypto.Length;
            int slen = bts.Length;
            int checknum = 0;
            for (int i = 0; i < slen; i++)
            {
                checknum += bts[i];
            }
            last = (byte)(checknum % 256);
            for (int i = 0; i < slen; i++)
            {
                btgoal[i] = (byte)((bts[i] + (i + ((byte)crypto[i % clen])) * last) % 256);
            }
            btgoal[slen] = (byte)((slen + last + ((byte)crypto[slen % clen])) % 256);

            //编码
            byte temp, temp1, temp2, overnum = 0;
            int len = slen + 1;
            StringBuilder sb = new StringBuilder();
            int pos = 2, prevnum = 0;
            for (int i = 0; i < len; i++)
            {
                //if (i == len-1)
                //    pos += 0;
                temp1 = (byte)(btgoal[i] >> pos);
                temp2 = (byte)(overnum << prevnum);
                temp = (byte)(temp1 + temp2);
                overnum = (byte)(btgoal[i] - (temp1 << pos));
                sb.Append(numCode[temp]);
                prevnum = 6 - pos;
                pos = (8 * (i + 2)) % 6;
                if (prevnum == 0)
                {
                    sb.Append(numCode[overnum]);
                    overnum = 0;
                }
                if (pos == 0) pos = 6;

            }
            if (prevnum != 0)
            {
                sb.Append(numCode[overnum << prevnum]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="source">解密字符</param>
        /// <returns>原密码,如果为空则异常</returns>
        public static string Rtranslate(string source)
        {
            return Rtranslate(source, defaultKey);
        }
        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="source">解密字符</param>
        /// <param name="crypto">暗语</param>
        /// <returns>原密码,如果为空则异常</returns>
        public static string Rtranslate(string source, string crypto)
        {
            return Rtranslate(source, crypto, null);
        }
        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="source">解密字符</param>
        /// <param name="crypto">暗语</param>
        /// <param name="numCode">编码基码</param>
        /// <returns>原密码,如果为空则异常</returns>
        public static string Rtranslate(string source, string crypto, string numCode)
        {
            return Rtranslate(source, crypto, numCode, Encoding.Default);
        }
        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="source">解密字符</param>
        /// <param name="crypto">暗语</param>
        /// <param name="numCode">编码基码</param>
        /// <param name="ed">编码方式</param>
        /// <returns>原密码,如果为空则异常</returns>
        public static string Rtranslate(string source, string crypto, string numCode, Encoding ed)
        {
            byte[] btresult = RtranslateBt(source, crypto, numCode);
            return ed.GetString(btresult);
        }

        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="source">解密字符</param>
        /// <param name="crypto">暗语</param>
        /// <param name="numCode">编码基码</param>
        /// <returns>原密码,如果为空则异常</returns>
        public static byte[] RtranslateBt(string source, string crypto, string numCode)
        {
            byte[] defBits = new byte[2] { 0, 0 };
            string NumCode = "bPsMYnAW8vmxeaDFyS@k3V2qJ!N9URrgG7otliBT6XpOCKwuL4QHhfzj5cd1ZI0E";//编码Key
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(crypto)) return defBits;
            if (numCode == null || numCode.Length != 64) numCode = NumCode;

            //解码
            byte prevnum, nextnum, temp;
            int glen = (source.Length * 6) / 8;
            int pos = 2;
            int k = 0;
            int prevpos = 6 - pos;

            byte[] bts = new byte[glen];
            prevnum = (byte)(numCode.IndexOf(source[0]));
            for (int i = 1; i < source.Length; i++)
            {
                //if (prevpos == 0) continue;
                nextnum = (byte)(numCode.IndexOf(source[i]));
                temp = (byte)(nextnum >> prevpos);
                prevnum = (byte)((prevnum << pos) + temp);
                bts[k] = prevnum;
                prevnum = (byte)(nextnum - (temp << prevpos));
                if (prevpos == 0 && i < source.Length - 1)
                {
                    i++;
                    prevnum = (byte)(numCode.IndexOf(source[i]));
                    //continue;
                }
                pos = (2 + pos) % 6;
                if (pos == 0) pos = 6;
                prevpos = 6 - pos;

                k++;

            }
            if (glen != k) return defBits;//组数不匹配
            //解密
            int slen, clen, tmp;
            byte checknum;
            int checktemp = 0;
            slen = glen - 1;
            clen = crypto.Length;
            tmp = slen % 256 + crypto[slen % clen];
            int last = bts[slen] - tmp;
            byte[] btresult = new byte[slen];
            if (last < 0) last += 256;

            for (int i = 0; i < slen; i++)
            {
                tmp = bts[i] - ((i + ((byte)crypto[i % clen])) * last) % 256;
                if (tmp < 0) tmp += 256;
                checktemp += tmp;
                btresult[i] = (byte)tmp;
            }
            checktemp = checktemp % 256;
            checknum = (byte)((slen + checktemp + ((byte)crypto[slen % clen])) % 256);
            if (checknum != bts[slen]) return defBits; //;校验码不正确
            return btresult;
            //return Encoding.Unicode.GetString(btresult);
        }
        public static byte[] RtranslateBt(string source)
        {
            return RtranslateBt(source, defaultKey, null);
        }
        /// <summary>
        /// 动态暗语解密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dymstr"></param>
        /// <returns></returns>
        public static string RtranslateDym(string source, string dymstr)
        {
            dymstr = dymstr ?? "";
            return Rtranslate(source, dymstr + defaultKey);
        }
    }
}
