using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Xml;
using System.Collections;

namespace DetourHome
{
    public partial class ListCheChi : Form
    {
        public ListCheChi()
        {
            InitializeComponent();
        }
        delegate void SysOperate(string msg);        
        private void ListCheChi_Load(object sender, EventArgs e)
        {
            //var dt =DataAccess.dataTable("select * from s_province");
            //dgvProvince.DataSource = dt;
            //dgvProvince.databind
        }
        Hashtable ht = new Hashtable();
        private void BtListALL_Click(object sender, EventArgs e)
        {
            string blockdata = tbContentBlock.Text.Trim(';');
            string[] bd = blockdata.Split(';');
            string[] blockx;
            string a ;
            int d;
            ht.Clear();
            for (int i = 0; i<bd.Length; i++)
            {
                blockx = bd[i].Split(',');
                a = blockx[0];
                if (blockx.Length == 2)
                {
                    d = int.Parse(blockx[1]);
                    for (int j = 1; j < d + 1; j++)
                    {
                        GetCheciBlockX(a + "_" + j.ToString());
                    }
                }
                else
                {
                    GetCheciBlockX(a + "_1");
                }
            }
        }

        private void GetCheciBlock(string cid)
        {
            Random rn = new Random();
            string url = "http://checi.114piaowu.com/"+cid+"?rnd=" + rn.Next(1, 1000000).ToString();
            
            //+ "?rnd=" + rn.Next(1, 1000000).ToString()
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://qq.ip138.com/train/search138.asp" + "?rnd=" + rn.Next(1, 1000000).ToString());
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.baidu.com");

            //req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded";

            //req.ReadWriteTimeout = 5000;
            //string param = "act=1&stationname=" + stationName;
            //ASCIIEncoding encoding = new ASCIIEncoding();

            //byte[] data = GetEncodingBytes(param, Encoding.GetEncoding("gb2312"));            
            //req.ContentLength = data.Length;
            //using (Stream reqStream = req.GetRequestStream())
            //{
            //    reqStream.Write(data, 0, data.Length);
            //    reqStream.Close();
            //}
            req.BeginGetResponse(new AsyncCallback(OnResponse), req);
        }
        private void GetCheciBlockX(string cid)
        {
            Random rn = new Random();
            string url = "http://checi.114piaowu.com/" + cid + "?rnd=" + rn.Next(1, 1000000).ToString();

            //+ "?rnd=" + rn.Next(1, 1000000).ToString()
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://qq.ip138.com/train/search138.asp" + "?rnd=" + rn.Next(1, 1000000).ToString());
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.baidu.com");

            //req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded";

            //req.ReadWriteTimeout = 5000;
            //string param = "act=1&stationname=" + stationName;
            //ASCIIEncoding encoding = new ASCIIEncoding();

            //byte[] data = GetEncodingBytes(param, Encoding.GetEncoding("gb2312"));            
            //req.ContentLength = data.Length;
            //using (Stream reqStream = req.GetRequestStream())
            //{
            //    reqStream.Write(data, 0, data.Length);
            //    reqStream.Close();
            //}
            req.BeginGetResponse(new AsyncCallback(OnResponseX), req);
        }
        protected void OnResponse(IAsyncResult ar)
        {

            WebRequest wrq = (WebRequest)ar.AsyncState;


            try
            {
                //WebResponse wrp = ss.BeginGetResponse(); 
                WebResponse wrs = wrq.EndGetResponse(ar);
                Stream stm = wrs.GetResponseStream();
                TextReader tr = new StreamReader(stm, Encoding.GetEncoding("utf-8"));
                string strRes = tr.ReadToEnd();
                wrs.Close();
                //SysOperate so = new SysOperate(DisplayDD);
                //this.Invoke(so, strRes);   
                SysOperate so = new SysOperate(DisplayRes);
                this.Invoke(so, strRes);

            }
            catch (Exception ex)
            {
                SysOperate so = new SysOperate(DisplayDD);
                this.Invoke(so, ex.Message);
            }
        }
        protected void OnResponseX(IAsyncResult ar)
        {

            WebRequest wrq = (WebRequest)ar.AsyncState;


            try
            {
                //WebResponse wrp = ss.BeginGetResponse(); 
                WebResponse wrs = wrq.EndGetResponse(ar);
                Stream stm = wrs.GetResponseStream();
                TextReader tr = new StreamReader(stm, Encoding.GetEncoding("utf-8"));
                string strRes = tr.ReadToEnd();
                wrs.Close();
                //SysOperate so = new SysOperate(DisplayDD);
                //this.Invoke(so, strRes);   
                SysOperate so = new SysOperate(DisplayResX);
                this.Invoke(so, strRes);

            }
            catch (Exception ex)
            {
                SysOperate so = new SysOperate(DisplayDD);
                this.Invoke(so, ex.Message);
            }
        }
        public void DisplayDD(string msg)
        {
            tbContent.Text = msg;
        }
        public void DisplayRes(string Data)
        {
            //Data = Data.Replace("<br>", "<br/>");
            string result = GetResultR(Data);            
            if (result != "")
                tbContentBlock.Text += ";" + result;
        }
        public void DisplayResX(string Data)
        {
            //Data = Data.Replace("<br>", "<br/>");
            string result = GetResultX(Data);
            if (result != "")
                tbContent.Text += ";" + result;
        }
        private string GetResultR(string xml)
        {
            string subxml = "";
            string strResult = "";
            string  a3 = "";
            try
            {
                //Regex reg = new Regex("(?<uuu><table.*T2_timelist_table\">.*(<tr.*>.*<strong>(?<ddd>[\\w/]+)</strong></a>.*</tr>)+.*</table>)", RegexOptions.Singleline);
                //Regex reg = new Regex("<tr((?!<tr)(?!</tr).)*<strong>(?<uuu>[\\w/]+)</strong></a>((?!<tr)(?!</tr).)*</tr>", RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                //Regex reg = new Regex("<tr((?!<tr)(?!</tr).)*<strong>(?<uuu>[\\w/]+)</strong></a>((?!<tr)(?!</tr).)*<a[^<>]*>(?<sss>[\u4e00-\u9fa5]*)</a>\\)</td>((?!<tr)(?!</tr).)*</tr>", RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                //Regex reg = new Regex("<tr((?!<tr)(?!</tr).)*<strong>(?<uuu>[\\w/]+)</strong></a>[^<>]*<a[^<>]*>(?<ddd>[\u4e00-\u9fa5]*)</a>[^<>]*<a[^<>]*>(?<sss>[\u4e00-\u9fa5]*)</a>\\)</td>((?!<tr)(?!</tr).)*</tr>", RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                Regex reg = new Regex("<meta name=\"mobile-agent\" content=\"format=html5;url=http://m.114piaowu.com/huochepiao/checi_(?<ddd>\\w{1})/\"/>");
                Regex reg2 = new Regex("<a href='\\w{1}_(?<dss>\\w+).html'>>></a>");
                Match m = reg.Match(xml);
                Match m2= reg2.Match(xml);
                string s="",e="";
                if (m.Success)
                {
                    s=m.Groups["ddd"].Value;
                    if (m2.Success)
                    {
                        e = m2.Groups["dss"].Value;
                    }

                }
                a3 = s + "," + e;
                //[\u4e00-\u9fa5] 
                //while (m.Success)
                //{
                //    strResult+=","+m.Groups["uuu"].Value;
                //    m = m.NextMatch();
                //}

                strResult = a3.Trim(',');
                //tbContent.Text = strResult;
            }
            catch (Exception ex)
            {
                tbContent.Text = ex.Message;
                return "";
            }
            return strResult;
        }
        private string GetResultX(string xml)
        {
             
            string strResult = "";
            string a3 = "",s="",subxml="";
            try
            {
                Regex reg = new Regex("(?<ddd><div class=\"checicx\">.*)<div class=\"checisk\">", RegexOptions.Singleline);
                Match m = reg.Match(xml);
                string   e = "";
                if (m.Success)
                {
                    subxml = m.Groups["ddd"].Value;

                }
                subxml = subxml.Replace("&nbsp;", "");
                subxml = subxml.Replace(">>><", ">$1<");
                subxml = subxml.Replace("><<<", ">$2<");
                subxml = subxml.Replace("><<", ">$3<");
                subxml = subxml.Replace(">><", ">$4<");
                XmlDocument xd = new XmlDocument();                
                xd.XmlResolver = null;//不解释外部资源
                xd.LoadXml(subxml);
                XmlNodeList xnl = xd.SelectNodes("//ul/li/a");
                foreach (XmlNode n in xnl)
                {
                    //s += "," + n.InnerText;
                    s = n.InnerText;
                    if (!ht.Contains(s))
                        ht.Add(s, "");
                }                
                strResult = s;
  
            }
            catch (Exception ex)
            {
                tbContent.Text = ex.Message;
                return "";
            }
            return strResult;
        }
        private void btCheChiBlock_Click(object sender, EventArgs e)
        {
            string data=ConfigurationSettings.AppSettings["checiStart"];
            if(string.IsNullOrEmpty(data))
            {
                MessageBox.Show("参数错误!");
                return;
            }
            string[] datas = data.Split(',');
            for (int i = 0; i < datas.Length; i++)
            {
                GetCheciBlock(datas[i] + "_1");
            }
            //GetCheciBlock("C_1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string temp = "";
            foreach (string o in ht.Keys)
            {
                temp += "." + o;
            }
            tbContent.Text = temp ;
        }
    }
}
