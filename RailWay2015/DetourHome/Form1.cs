using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;

using System.Text.RegularExpressions;
using System.Collections;

namespace DetourHome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Hashtable ht = new Hashtable();
        Hashtable hts = new Hashtable(); 
        delegate void SysOperate(string msg);
        delegate void SysOperateX(string msg,string sid);
        string[] stationNames = null;
        private void btListCurrent_Click(object sender, EventArgs e)
        {
            string stationName=tbStation.Text.Trim();
            if(stationName=="") return  ;
            ht = new Hashtable();
            hts = new Hashtable();           
            GetListX(stationName);
        }

        protected void OnResponse(IAsyncResult ar)
        {

            WebRequest wrq = (WebRequest)ar.AsyncState;

            
            try
            {                
                //WebResponse wrp = ss.BeginGetResponse(); 
                WebResponse wrs = wrq.EndGetResponse(ar);
                Stream stm= wrs.GetResponseStream();
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

            LinkData ld = (LinkData)ar.AsyncState;
            var wrq = ld.hwr;

            try
            {
                //WebResponse wrp = ss.BeginGetResponse(); 
                WebResponse wrs = wrq.EndGetResponse(ar);
                Stream stm = wrs.GetResponseStream();
                TextReader tr = new StreamReader(stm, Encoding.GetEncoding("gb2312"));
                string strRes = tr.ReadToEnd();
                wrs.Close();
                //SysOperate so = new SysOperate(DisplayDD);
                //this.Invoke(so, strRes);   
                SysOperateX so = new SysOperateX(DisplayResX);
                this.Invoke(so, strRes,ld.stationid);

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
            Data=Data.Replace("<br>","<br/>");
            string result=GetResultR(Data);
            if (result != "")
                tbContent.Text += "," + result;     
        }
        public void DisplayResX(string Data,string sid)
        {
            Data = Data.Replace("<br>", "<br/>");
            string result = GetResultRX(Data,sid);
            if (result != "")
                tbContent.Text += "," + result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">UTF-8字符，系统字符</param>
        /// <param name="ed"></param>
        /// <returns></returns>
        private byte[] GetEncodingBytes(string source, Encoding ed)
        {
              
            byte[] gb;
            gb = Encoding.UTF8.GetBytes(source);
            gb = System.Text.Encoding.Convert(Encoding.UTF8, ed, gb);
            return gb;
        }
        private void btTest_Click(object sender, EventArgs e)
        {
            string xml = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\"\"http://www.w3.org/TR/html4/loose.dtd\"><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"><title>吉安列车时刻表 吉安火车时刻表 www.ip138.com</title><meta name=\"Keywords\" content=\"吉安,列车时刻,火车时刻\"><meta name=\"Description\" content=\"经过吉安站的105趟列车时刻表\"><link href=\"/css/tq.css\" rel=\"stylesheet\" type=\"text/css\"><script language=\"JavaScript\" src=\"/scripts/sort.js\"></script><script language=\"JavaScript\" src=\"/scripts/train_chk.js\"></script><script type=\"text/javascript\"></script></head><body><div align=\"center\"><center><table cellSpacing=\"0\" cellPadding=\"0\" width=\"780\" align=\"center\" border=\"0\">	<tr valign=\"bottom\">		<td align=\"left\"><b>www.ip138.com 查询网</b></td>		<td align=\"center\"><a href=\"http://www.ip138.com/ips1388.asp\" target=\"_blank\" class=\"green\">ip地址查询</a><a href=\"http://www.ip138.com/sj/\" target=\"_blank\" class=\"green\">手机号码查询</a><a href=\"http://www.ip138.com/post/\" target=\"_blank\" class=\"green\">邮编电话查询</a><a href=\"/daishoudian/\" class=\"red\" target=\"_blank\">火车票代售点大全</a></td>		<td align=\"right\"><a class=\"white\" href=\"http://www.ip138.com\" target=\"_blank\" class=\"green\"><b>查询主页</b></a></td>	</tr>	<tr valign=\"top\" align=\"left\">		<td colSpan=\"3\"><hr width=\"100%\" SIZE=\"1\">		</td>	</tr></table></center></div><br/><table width=\"720\" border=\"0\" align=\"center\">	<tr>		<td><a href=\"http://www.ip138.com/\">首页</a>&gt;<a href=\"/train/\">列车时刻首页</a>&gt;<a href=\"/train/jiangxi/\">江西列车时刻表</a>&gt;吉安火车时刻表</td>		<td align=\"right\"><div id=\"bdshare\" class=\"bdshare_t bds_tools get-codes-bdshare\"><a class=\"bds_qzone\"></a><a class=\"bds_tsina\"></a><a class=\"bds_tqq\"></a><a class=\"bds_renren\"></a><span class=\"bds_more\">更多</span></div><script type=\"text/javascript\" id=\"bdshare_js\" data=\"type=tools&amp;uid=582846\" ></script><script type=\"text/javascript\" id=\"bdshell_js\"></script><script type=\"text/javascript\">	document.getElementById(\"bdshell_js\").src = \"http://bdimg.share.baidu.com/static/js/shell_v2.js?t=\" + new Date().getHours();</script></td>	<td width=\"80\"><a href=\"javascript:;\" onClick=\"addBookMark()\">加入收藏</a></td>	</tr></table><h1 align=\"center\" style=\"margin-bottom:0px\">吉安列车时刻表</h1><div align=\"center\"><a href=\"/daishoudian/JiAn.htm\" target=\"_blank\" class=\"red\"><b>吉安代售点</b></a></div><table width=\"620\" border=\"0\" align=\"center\">	<tr>		<td style=\"text-indent:2em;line-height:120%;\"><p>吉安火车时刻表目前有105条火车运营线路经过，主要指标包括列车车次、始发站终点站名称、到达时间、发车时间、累计用时、累计距离等。</p></td>	</tr></table><center style=\"padding:3px\"><iframe src=\"/jss/bd_460x60.htm\" frameborder=\"no\" width=\"460\" height=\"60\" border=\"0\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\"></iframe></center><br/><div align=\"center\" id=\"checilist\"><table cellpadding=\"2\" cellspacing=\"0\" align=\"center\"  borderColorDark=\"#ffffff\" borderColorLight=\"#008000\" border=\"1\" width=\"760\">		<thead onclick=\"sortColumn(event)\"><tr style=\"cursor:hand;'\">	<th bgcolor=\"#CCE6CD\" onmouseover=\"this.style.backgroundColor='#E6F2E7'\" onmouseout=\"this.style.backgroundColor=''\">车次</th><th bgcolor=\"#CCE6CD\" onmouseover=\"this.style.backgroundColor='#E6F2E7'\" onmouseout=\"this.style.backgroundColor=''\">列车类型</th>	<th bgcolor=\"#CCE6CD\" onmouseover=\"this.style.backgroundColor='#E6F2E7'\" onmouseout=\"this.style.backgroundColor=''\">始发站</th><th bgcolor=\"#CCE6CD\" onmouseover=\"this.style.backgroundColor='#E6F2E7'\" onmouseout=\"this.style.backgroundColor=''\">始发时间</th>	<th bgcolor=\"#CCE6CD\" onmouseover=\"this.style.backgroundColor='#E6F2E7'\" onmouseout=\"this.style.backgroundColor=''\">经过站</th><th bgcolor=\"#CCE6CD\" onmouseover=\"this.style.backgroundColor='#E6F2E7'\" onmouseout=\"this.style.backgroundColor=''\">经过站<br/>到达时间</th>	<th bgcolor=\"#CCE6CD\" onmouseover=\"this.style.backgroundColor='#E6F2E7'\" onmouseout=\"this.style.backgroundColor=''\">经过站<br/>发车时间</th>	<th bgcolor=\"#CCE6CD\" onmouseover=\"this.style.backgroundColor='#E6F2E7'\" onmouseout=\"this.style.backgroundColor=''\">终点站</th>	<th bgcolor=\"#CCE6CD\" onmouseover=\"this.style.backgroundColor='#E6F2E7'\" onmouseout=\"this.style.backgroundColor=''\">到达时间</th></tr></thead><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/1202-1203.htm\"><b>1202/1203</b></a></td>	<td>普快</td><td>信阳</td>	<td>11:32</td>	<td>吉安</td><td>当天23:01</td><td>23:05</td><td>深圳西</td>	<td>09:07</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/1204.htm\"><b>1204</b></a></td>	<td>普快</td><td>深圳西</td>	<td>13:06</td>	<td>吉安</td><td>当天23:48</td><td>23:53</td><td>信阳</td>	<td>11:15</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/7213.htm\"><b>7213</b></a></td>	<td>普快</td><td>南昌</td>	<td>16:20</td>	<td>吉安</td><td>当天20:11</td><td>20:11</td><td>吉安</td>	<td>20:11</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/7214.htm\"><b>7214</b></a></td>	<td>普快</td><td>吉安</td>	<td>07:50</td>	<td>吉安</td><td>当天07:50</td><td>07:50</td><td>南昌</td>	<td>11:49</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/7215.htm\"><b>7215</b></a></td>	<td>普快</td><td>吉安</td>	<td>11:31</td>	<td>吉安</td><td>当天11:31</td><td>11:31</td><td>定南</td>	<td>17:53</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/7216.htm\"><b>7216</b></a></td>	<td>普快</td><td>定南</td>	<td>07:42</td>	<td>吉安</td><td>当天13:56</td><td>13:56</td><td>吉安</td>	<td>13:56</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1029.htm\"><b>K1029</b></a></td>	<td>空调快速</td><td>合肥</td>	<td>11:38</td>	<td>吉安</td><td>当天22:00</td><td>22:04</td><td>东莞东</td>	<td>06:30</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1030.htm\"><b>K1030</b></a></td>	<td>空调快速</td><td>东莞东</td>	<td>14:55</td>	<td>吉安</td><td>当天23:39</td><td>23:43</td><td>合肥</td>	<td>09:42</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K105.htm\"><b>K105</b></a></td>	<td>空调快速</td><td>北京西</td>	<td>23:20</td>	<td>吉安</td><td>第2日18:26</td><td>18:30</td><td>深圳</td>	<td>04:40</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K106.htm\"><b>K106</b></a></td>	<td>空调快速</td><td>深圳</td>	<td>10:36</td>	<td>吉安</td><td>当天20:13</td><td>20:17</td><td>北京西</td>	<td>16:20</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1092-K1093.htm\"><b>K1092/K1093</b></a></td>	<td>空调快速</td><td>深圳东</td>	<td>19:40</td>	<td>吉安</td><td>第2日04:23</td><td>04:28</td><td>成都东</td>	<td>10:59</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1094-K1091.htm\"><b>K1094/K1091</b></a></td>	<td>空调快速</td><td>成都东</td>	<td>12:55</td>	<td>吉安</td><td>第2日19:21</td><td>19:27</td><td>深圳东</td>	<td>05:25</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1136-K1137.htm\"><b>K1136/K1137</b></a></td>	<td>空调快速</td><td>青岛</td>	<td>17:46</td>	<td>吉安</td><td>第2日17:09</td><td>17:13</td><td>南宁</td>	<td>10:48</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1138-K1135.htm\"><b>K1138/K1135</b></a></td>	<td>空调快速</td><td>南宁</td>	<td>12:55</td>	<td>吉安</td><td>第2日07:43</td><td>07:58</td><td>青岛</td>	<td>06:44</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K115.htm\"><b>K115</b></a></td>	<td>空调快速</td><td>九江</td>	<td>15:16</td>	<td>吉安</td><td>当天19:50</td><td>19:55</td><td>深圳</td>	<td>05:50</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K116.htm\"><b>K116</b></a></td>	<td>空调快速</td><td>深圳</td>	<td>16:36</td>	<td>吉安</td><td>第2日01:41</td><td>01:45</td><td>九江</td>	<td>06:56</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1282-K1283.htm\"><b>K1282/K1283</b></a></td>	<td>空调快速</td><td>深圳东</td>	<td>16:26</td>	<td>吉安</td><td>第2日01:00</td><td>01:04</td><td>济南</td>	<td>18:50</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1284-K1281.htm\"><b>K1284/K1281</b></a></td>	<td>空调快速</td><td>济南</td>	<td>10:40</td>	<td>吉安</td><td>第2日04:33</td><td>04:41</td><td>深圳东</td>	<td>13:21</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K132-K133.htm\"><b>K132/K133</b></a></td>	<td>空调快速</td><td>兰州</td>	<td>13:39</td>	<td>吉安</td><td>第2日23:08</td><td>23:13</td><td>深圳西</td>	<td>09:38</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K134-K131.htm\"><b>K134/K131</b></a></td>	<td>空调快速</td><td>深圳西</td>	<td>11:38</td>	<td>吉安</td><td>当天20:45</td><td>20:48</td><td>兰州</td>	<td>07:05</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1453.htm\"><b>K1453</b></a></td>	<td>空调快速</td><td>北京西</td>	<td>12:17</td>	<td>吉安</td><td>第2日08:43</td><td>08:46</td><td>赣州</td>	<td>11:10</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1454.htm\"><b>K1454</b></a></td>	<td>空调快速</td><td>赣州</td>	<td>11:10</td>	<td>吉安</td><td>当天13:23</td><td>13:29</td><td>北京西</td>	<td>11:15</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1561.htm\"><b>K1561</b></a></td>	<td>空调快速</td><td>合肥</td>	<td>18:45</td>	<td>吉安</td><td>第2日05:22</td><td>05:29</td><td>南宁</td>	<td>20:18</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1562.htm\"><b>K1562</b></a></td>	<td>空调快速</td><td>南宁</td>	<td>11:25</td>	<td>吉安</td><td>第2日02:03</td><td>02:21</td><td>合肥</td>	<td>13:40</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K161.htm\"><b>K161</b></a></td>	<td>空调快速</td><td>徐州</td>	<td>18:00</td>	<td>吉安</td><td>第2日11:07</td><td>11:16</td><td>南宁</td>	<td>04:55</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K162.htm\"><b>K162</b></a></td>	<td>空调快速</td><td>南宁</td>	<td>10:30</td>	<td>吉安</td><td>第2日02:28</td><td>02:34</td><td>徐州</td>	<td>19:46</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1620-K1621.htm\"><b>K1620/K1621</b></a></td>	<td>空调快速</td><td>深圳东</td>	<td>08:25</td>	<td>吉安</td><td>当天17:56</td><td>18:00</td><td>天津</td>	<td>15:24</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K1622-K1619.htm\"><b>K1622/K1619</b></a></td>	<td>空调快速</td><td>天津</td>	<td>21:55</td>	<td>吉安</td><td>第2日20:27</td><td>20:33</td><td>深圳东</td>	<td>06:25</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K212-K209.htm\"><b>K212/K209</b></a></td>	<td>空调快速</td><td>宁波</td>	<td>15:35</td>	<td>吉安</td><td>第2日06:18</td><td>06:23</td><td>广州</td>	<td>15:11</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K2386-K2387.htm\"><b>K2386/K2387</b></a></td>	<td>空调快速</td><td>南宁</td>	<td>23:05</td>	<td>吉安</td><td>第2日18:20</td><td>18:33</td><td>长春</td>	<td>05:49</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K2388-K2385.htm\"><b>K2388/K2385</b></a></td>	<td>空调快速</td><td>长春</td>	<td>11:28</td>	<td>吉安</td><td>第2日22:07</td><td>22:26</td><td>南宁</td>	<td>17:48</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K25.htm\"><b>K25</b></a></td>	<td>空调快速</td><td>南京</td>	<td>15:40</td>	<td>吉安</td><td>第2日06:55</td><td>06:59</td><td>深圳</td>	<td>16:30</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K26.htm\"><b>K26</b></a></td>	<td>空调快速</td><td>深圳</td>	<td>12:20</td>	<td>吉安</td><td>当天21:28</td><td>21:32</td><td>南京</td>	<td>14:03</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K271.htm\"><b>K271</b></a></td>	<td>空调快速</td><td>上海南</td>	<td>17:49</td>	<td>吉安</td><td>第2日07:09</td><td>07:12</td><td>井冈山</td>	<td>08:25</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K272.htm\"><b>K272</b></a></td>	<td>空调快速</td><td>井冈山</td>	<td>17:55</td>	<td>吉安</td><td>当天19:00</td><td>19:04</td><td>上海南</td>	<td>08:16</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K309-K312.htm\"><b>K309/K312</b></a></td>	<td>空调快速</td><td>广州东</td>	<td>10:50</td>	<td>吉安</td><td>当天20:26</td><td>20:29</td><td>合肥</td>	<td>06:30</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K311-K310.htm\"><b>K311/K310</b></a></td>	<td>空调快速</td><td>合肥</td>	<td>13:45</td>	<td>吉安</td><td>当天23:19</td><td>23:23</td><td>广州东</td>	<td>09:36</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K335-K334.htm\"><b>K335/K334</b></a></td>	<td>空调快速</td><td>重庆北</td>	<td>19:22</td>	<td>吉安</td><td>第2日18:41</td><td>18:47</td><td>厦门高崎</td>	<td>06:00</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K336-K333.htm\"><b>K336/K333</b></a></td>	<td>空调快速</td><td>厦门高崎</td>	<td>20:38</td>	<td>吉安</td><td>第2日08:21</td><td>08:32</td><td>重庆北</td>	<td>06:32</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K441.htm\"><b>K441</b></a></td>	<td>空调快速</td><td>南昌</td>	<td>14:59</td>	<td>吉安</td><td>当天17:35</td><td>17:39</td><td>广州东</td>	<td>05:14</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K442.htm\"><b>K442</b></a></td>	<td>空调快速</td><td>广州东</td>	<td>17:48</td>	<td>吉安</td><td>第2日04:15</td><td>04:22</td><td>南昌</td>	<td>07:00</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K446-K447.htm\"><b>K446/K447</b></a></td>	<td>空调快速</td><td>深圳</td>	<td>08:58</td>	<td>吉安</td><td>当天18:03</td><td>18:09</td><td>西安</td>	<td>15:17</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K448-K445.htm\"><b>K448/K445</b></a></td>	<td>空调快速</td><td>西安</td>	<td>23:00</td>	<td>吉安</td><td>第2日19:13</td><td>19:18</td><td>深圳</td>	<td>04:55</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K469.htm\"><b>K469</b></a></td>	<td>空调快速</td><td>苏州</td>	<td>15:26</td>	<td>吉安</td><td>第2日05:31</td><td>05:37</td><td>赣州</td>	<td>08:09</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K470.htm\"><b>K470</b></a></td>	<td>空调快速</td><td>赣州</td>	<td>17:50</td>	<td>吉安</td><td>当天20:19</td><td>20:23</td><td>苏州</td>	<td>11:18</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K511.htm\"><b>K511</b></a></td>	<td>空调快速</td><td>上海南</td>	<td>09:54</td>	<td>吉安</td><td>当天22:29</td><td>22:35</td><td>海口</td>	<td>18:48</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K512.htm\"><b>K512</b></a></td>	<td>空调快速</td><td>海口</td>	<td>23:35</td>	<td>吉安</td><td>第2日19:19</td><td>19:38</td><td>上海南</td>	<td>08:54</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K555.htm\"><b>K555</b></a></td>	<td>空调快速</td><td>武昌</td>	<td>15:11</td>	<td>吉安</td><td>当天23:27</td><td>23:33</td><td>深圳东</td>	<td>09:10</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K556.htm\"><b>K556</b></a></td>	<td>空调快速</td><td>深圳东</td>	<td>14:46</td>	<td>吉安</td><td>第2日00:13</td><td>00:18</td><td>武昌</td>	<td>08:54</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K561.htm\"><b>K561</b></a></td>	<td>空调快速</td><td>阜阳</td>	<td>12:00</td>	<td>吉安</td><td>当天23:42</td><td>23:50</td><td>深圳</td>	<td>09:22</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K562.htm\"><b>K562</b></a></td>	<td>空调快速</td><td>深圳</td>	<td>11:40</td>	<td>吉安</td><td>当天21:36</td><td>21:42</td><td>阜阳</td>	<td>10:30</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K571.htm\"><b>K571</b></a></td>	<td>空调快速</td><td>北京西</td>	<td>16:50</td>	<td>吉安</td><td>第2日13:41</td><td>13:45</td><td>龙岩</td>	<td>20:39</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K572.htm\"><b>K572</b></a></td>	<td>空调快速</td><td>龙岩</td>	<td>11:12</td>	<td>吉安</td><td>当天17:35</td><td>17:39</td><td>北京西</td>	<td>12:50</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K636-K637.htm\"><b>K636/K637</b></a></td>	<td>空调快速</td><td>昆明</td>	<td>19:06</td>	<td>吉安</td><td>第3日03:47</td><td>04:15</td><td>福州</td>	<td>23:09</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K638-K635.htm\"><b>K638/K635</b></a></td>	<td>空调快速</td><td>福州</td>	<td>07:10</td>	<td>吉安</td><td>第2日00:36</td><td>00:55</td><td>昆明</td>	<td>09:08</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K726-K727.htm\"><b>K726/K727</b></a></td>	<td>空调快速</td><td>桂林北</td>	<td>10:15</td>	<td>吉安</td><td>当天23:26</td><td>23:32</td><td>哈尔滨</td>	<td>14:48</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K728-K725.htm\"><b>K728/K725</b></a></td>	<td>快速</td><td>哈尔滨</td>	<td>22:17</td>	<td>吉安</td><td>第3日12:02</td><td>12:05</td><td>桂林北</td>	<td>22:00</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K729-K732.htm\"><b>K729/K732</b></a></td>	<td>空调快速</td><td>广州东</td>	<td>12:53</td>	<td>吉安</td><td>当天22:55</td><td>23:01</td><td>大同</td>	<td>05:22</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K731-K730.htm\"><b>K731/K730</b></a></td>	<td>空调快速</td><td>大同</td>	<td>14:19</td>	<td>吉安</td><td>第2日18:56</td><td>19:01</td><td>广州东</td>	<td>05:15</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K742-K743.htm\"><b>K742/K743</b></a></td>	<td>空调快速</td><td>郑州</td>	<td>12:50</td>	<td>吉安</td><td>第2日04:25</td><td>04:32</td><td>厦门高崎</td>	<td>16:03</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K744-K741.htm\"><b>K744/K741</b></a></td>	<td>空调快速</td><td>厦门高崎</td>	<td>19:55</td>	<td>吉安</td><td>第2日08:04</td><td>08:12</td><td>郑州</td>	<td>23:54</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K793-K796.htm\"><b>K793/K796</b></a></td>	<td>空调快速</td><td>上饶</td>	<td>16:31</td>	<td>吉安</td><td>当天22:47</td><td>22:51</td><td>广州东</td>	<td>08:25</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K795-K794.htm\"><b>K795/K794</b></a></td>	<td>空调快速</td><td>广州东</td>	<td>16:30</td>	<td>吉安</td><td>第2日01:52</td><td>01:56</td><td>鹰潭</td>	<td>06:54</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K799-K798.htm\"><b>K799/K798</b></a></td>	<td>空调快速</td><td>武昌</td>	<td>12:55</td>	<td>吉安</td><td>当天20:45</td><td>20:50</td><td>汕头</td>	<td>08:38</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K800-K797.htm\"><b>K800/K797</b></a></td>	<td>空调快速</td><td>汕头</td>	<td>16:52</td>	<td>吉安</td><td>第2日03:32</td><td>03:36</td><td>武昌</td>	<td>11:35</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K85-K88.htm\"><b>K85/K88</b></a></td>	<td>空调快速</td><td>广州</td>	<td>18:25</td>	<td>吉安</td><td>第2日03:54</td><td>03:57</td><td>九江</td>	<td>08:32</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K87-K86.htm\"><b>K87/K86</b></a></td>	<td>空调快速</td><td>九江</td>	<td>18:15</td>	<td>吉安</td><td>当天22:37</td><td>22:41</td><td>广州</td>	<td>08:50</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K8703.htm\"><b>K8703</b></a></td>	<td>空调快速</td><td>南昌</td>	<td>12:26</td>	<td>吉安</td><td>当天15:37</td><td>15:42</td><td>井冈山</td>	<td>16:41</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K8704.htm\"><b>K8704</b></a></td>	<td>空调快速</td><td>井冈山</td>	<td>09:03</td>	<td>吉安</td><td>当天10:09</td><td>10:13</td><td>南昌</td>	<td>12:57</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K8723.htm\"><b>K8723</b></a></td>	<td>空调快速</td><td>南昌</td>	<td>06:55</td>	<td>吉安</td><td>当天09:41</td><td>09:47</td><td>瑞金</td>	<td>14:57</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K8724.htm\"><b>K8724</b></a></td>	<td>空调快速</td><td>瑞金</td>	<td>11:04</td>	<td>吉安</td><td>当天15:43</td><td>15:48</td><td>南昌</td>	<td>18:51</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K8725.htm\"><b>K8725</b></a></td>	<td>空调快速</td><td>南昌</td>	<td>05:35</td>	<td>吉安</td><td>当天08:17</td><td>08:21</td><td>赣州</td>	<td>10:30</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K8726.htm\"><b>K8726</b></a></td>	<td>空调快速</td><td>赣州</td>	<td>12:01</td>	<td>吉安</td><td>当天14:25</td><td>14:29</td><td>南昌</td>	<td>18:00</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K8739-K8742.htm\"><b>K8739/K8742</b></a></td>	<td>快速</td><td>吉安</td>	<td>14:30</td>	<td>吉安</td><td>当天14:30</td><td>14:30</td><td>福州</td>	<td>09:50</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K8740-K8741.htm\"><b>K8740/K8741</b></a></td>	<td>快速</td><td>福州</td>	<td>18:59</td>	<td>吉安</td><td>第2日12:57</td><td>12:57</td><td>吉安</td>	<td>12:57</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K903-K902.htm\"><b>K903/K902</b></a></td>	<td>空调快速</td><td>太原</td>	<td>11:48</td>	<td>吉安</td><td>第2日16:37</td><td>16:42</td><td>厦门高崎</td>	<td>05:39</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K904-K901.htm\"><b>K904/K901</b></a></td>	<td>空调快速</td><td>厦门高崎</td>	<td>13:36</td>	<td>吉安</td><td>当天23:54</td><td>00:12</td><td>太原</td>	<td>05:36</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K92-K93.htm\"><b>K92/K93</b></a></td>	<td>空调快速</td><td>深圳东</td>	<td>18:05</td>	<td>吉安</td><td>第2日03:08</td><td>03:12</td><td>泰州</td>	<td>22:03</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K921-K924.htm\"><b>K921/K924</b></a></td>	<td>空调快速</td><td>广州东</td>	<td>11:25</td>	<td>吉安</td><td>当天21:17</td><td>21:21</td><td>汉口</td>	<td>07:14</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/K94-K91.htm\"><b>K94/K91</b></a></td>	<td>空调快速</td><td>泰州</td>	<td>12:38</td>	<td>吉安</td><td>第2日05:37</td><td>05:43</td><td>深圳东</td>	<td>14:18</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T101.htm\"><b>T101</b></a></td>	<td>空调特快</td><td>上海南</td>	<td>13:40</td>	<td>吉安</td><td>当天23:59</td><td>00:03</td><td>深圳</td>	<td>08:25</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T126-T127.htm\"><b>T126/T127</b></a></td>	<td>空调特快</td><td>成都</td>	<td>22:35</td>	<td>吉安</td><td>第2日21:50</td><td>21:56</td><td>东莞东</td>	<td>05:15</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T128-T125.htm\"><b>T128/T125</b></a></td>	<td>空调特快</td><td>东莞东</td>	<td>12:16</td>	<td>吉安</td><td>当天19:28</td><td>19:32</td><td>成都</td>	<td>17:46</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T169.htm\"><b>T169</b></a></td>	<td>空调特快</td><td>上海南</td>	<td>11:13</td>	<td>吉安</td><td>当天21:31</td><td>21:37</td><td>广州</td>	<td>05:26</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T170.htm\"><b>T170</b></a></td>	<td>空调特快</td><td>广州</td>	<td>14:56</td>	<td>吉安</td><td>当天21:53</td><td>21:56</td><td>上海南</td>	<td>09:35</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T211.htm\"><b>T211</b></a></td>	<td>空调特快</td><td>上海南</td>	<td>11:45</td>	<td>吉安</td><td>当天22:16</td><td>22:20</td><td>深圳</td>	<td>06:08</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T212.htm\"><b>T212</b></a></td>	<td>空调特快</td><td>深圳</td>	<td>13:09</td>	<td>吉安</td><td>当天20:51</td><td>21:00</td><td>上海南</td>	<td>08:04</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T25.htm\"><b>T25</b></a></td>	<td>空调特快</td><td>上海南</td>	<td>17:01</td>	<td>吉安</td><td>第2日04:40</td><td>04:48</td><td>南宁</td>	<td>18:35</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T381.htm\"><b>T381</b></a></td>	<td>空调特快</td><td>上海南</td>	<td>16:29</td>	<td>吉安</td><td>第2日04:02</td><td>04:08</td><td>昆明</td>	<td>10:21</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T396-T397.htm\"><b>T396/T397</b></a></td>	<td>空调特快</td><td>青岛</td>	<td>16:53</td>	<td>吉安</td><td>第2日13:05</td><td>13:09</td><td>深圳</td>	<td>21:10</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T8001.htm\"><b>T8001</b></a></td>	<td>空调特快</td><td>南昌</td>	<td>17:53</td>	<td>吉安</td><td>当天20:03</td><td>20:07</td><td>赣州</td>	<td>22:05</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/T8002.htm\"><b>T8002</b></a></td>	<td>空调特快</td><td>赣州</td>	<td>07:55</td>	<td>吉安</td><td>当天09:54</td><td>09:58</td><td>南昌</td>	<td>12:09</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z107.htm\"><b>Z107</b></a></td>	<td>直达特快</td><td>北京西</td>	<td>19:55</td>	<td>吉安</td><td>第2日10:21</td><td>10:24</td><td>深圳</td>	<td>17:51</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z108.htm\"><b>Z108</b></a></td>	<td>直达特快</td><td>深圳</td>	<td>15:00</td>	<td>吉安</td><td>当天22:41</td><td>22:44</td><td>北京西</td>	<td>13:15</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z112-Z113.htm\"><b>Z112/Z113</b></a></td>	<td>直达特快</td><td>海口</td>	<td>17:00</td>	<td>吉安</td><td>第2日14:48</td><td>14:56</td><td>哈尔滨</td>	<td>19:10</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z114-Z111.htm\"><b>Z114/Z111</b></a></td>	<td>直达特快</td><td>哈尔滨</td>	<td>10:48</td>	<td>吉安</td><td>第2日14:43</td><td>14:49</td><td>海口</td>	<td>09:42</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z115.htm\"><b>Z115</b></a></td>	<td>直达特快</td><td>上海南</td>	<td>19:43</td>	<td>吉安</td><td>第2日05:11</td><td>05:14</td><td>深圳东</td>	<td>12:57</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z116.htm\"><b>Z116</b></a></td>	<td>直达特快</td><td>深圳东</td>	<td>14:35</td>	<td>吉安</td><td>当天22:14</td><td>22:19</td><td>上海南</td>	<td>08:32</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z133.htm\"><b>Z133</b></a></td>	<td>直达特快</td><td>北京西</td>	<td>19:24</td>	<td>吉安</td><td>第2日09:52</td><td>09:57</td><td>井冈山</td>	<td>10:56</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z134.htm\"><b>Z134</b></a></td>	<td>直达特快</td><td>井冈山</td>	<td>16:50</td>	<td>吉安</td><td>当天17:46</td><td>17:51</td><td>北京西</td>	<td>08:03</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z146-Z147.htm\"><b>Z146/Z147</b></a></td>	<td>直达特快</td><td>郑州</td>	<td>20:11</td>	<td>吉安</td><td>第2日07:15</td><td>07:19</td><td>深圳东</td>	<td>14:40</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z186-Z187.htm\"><b>Z186/Z187</b></a></td>	<td>直达特快</td><td>深圳</td>	<td>20:05</td>	<td>吉安</td><td>第2日04:04</td><td>04:16</td><td>沈阳北</td>	<td>06:47</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z188-Z185.htm\"><b>Z188/Z185</b></a></td>	<td>直达特快</td><td>沈阳北</td>	<td>17:25</td>	<td>吉安</td><td>第2日21:00</td><td>21:09</td><td>深圳</td>	<td>05:06</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z383-Z386.htm\"><b>Z383/Z386</b></a></td>	<td>直达特快</td><td>广州东</td>	<td>11:10</td>	<td>吉安</td><td>当天19:09</td><td>19:17</td><td>长春</td>	<td>22:55</td></tr><tr onmouseover=\"this.bgColor='#E6F2E7';\" onmouseout=\"this.bgColor=''\">	<td bgcolor=\"#CCE6CD\"><a href=\"/train/Z384-Z385.htm\"><b>Z384/Z385</b></a></td>	<td>直达特快</td><td>长春</td>	<td>11:04</td>	<td>吉安</td><td>第2日13:29</td><td>13:36</td><td>广州东</td>	<td>21:35</td></tr></table></div><br/><table width=\"680\" border=\"1\" align=\"center\" cellpadding=\"4\" bordercolor=\"#3366cc\" style=\"BORDER-COLLAPSE: collapse\"><tr><td class=\"tdc1\" bgcolor=\"#6699cc\" height=\"24\" align=\"middle\"><div align=\"left\">[江西 吉安 安福县]<strong>安福代售点火车售票处</strong></div></td></tr><tr align=\"middle\" bgcolor=\"#eff1f3\"><td class=\"tdc\"><div align=\"left\">联系电话：<strong><font color=\"blue\">13184693393</font></strong><br/>营业时间：8:20－12:30     13:00－17:30<br/>地址：安福县武功山大道304号</div></td></tr></table><br/><table width=\"680\" border=\"1\" align=\"center\" cellpadding=\"4\" bordercolor=\"#3366cc\" style=\"BORDER-COLLAPSE: collapse\"><tr><td class=\"tdc1\" bgcolor=\"#6699cc\" height=\"24\" align=\"middle\"><div align=\"left\">[江西 吉安 吉安县]<strong>吉安县代售点火车售票处</strong></div></td></tr><tr align=\"middle\" bgcolor=\"#eff1f3\"><td class=\"tdc\"><div align=\"left\">联系电话：<strong><font color=\"blue\">0796－8440688</font></strong><br/>营业时间：8:00－12:00     12:30－17:00<br/>地址：吉安县庐陵大道19号</div></td></tr></table><br/><table width=\"680\" border=\"1\" align=\"center\" cellpadding=\"4\" bordercolor=\"#3366cc\" style=\"BORDER-COLLAPSE: collapse\"><tr><td class=\"tdc1\" bgcolor=\"#6699cc\" height=\"24\" align=\"middle\"><div align=\"left\">[江西 吉安 吉水县]<strong>吉水代售点火车售票处</strong></div></td></tr><tr align=\"middle\" bgcolor=\"#eff1f3\"><td class=\"tdc\"><div align=\"left\">联系电话：<strong><font color=\"blue\">0796－3523322</font></strong><br/>营业时间：8:00－12:00     12:30－17:00<br/>地址：吉水县龙华大道323号</div></td></tr></table><br/><table width=\"680\" border=\"1\" align=\"center\" cellpadding=\"4\" bordercolor=\"#3366cc\" style=\"BORDER-COLLAPSE: collapse\"><tr><td class=\"tdc1\" bgcolor=\"#6699cc\" height=\"24\" align=\"middle\"><div align=\"left\">[江西 吉安 吉州区]<strong>井冈山大道代售点火车售票处</strong></div></td></tr><tr align=\"middle\" bgcolor=\"#eff1f3\"><td class=\"tdc\"><div align=\"left\">联系电话：<strong><font color=\"blue\">0796－7029393</font></strong><br/>营业时间：8:10－12:30      13:30－17:30<br/>地址：吉安市吉州区井冈山大道108号</div></td></tr></table><br/><table width=\"680\" border=\"1\" align=\"center\" cellpadding=\"4\" bordercolor=\"#3366cc\" style=\"BORDER-COLLAPSE: collapse\"><tr><td class=\"tdc1\" bgcolor=\"#6699cc\" height=\"24\" align=\"middle\"><div align=\"left\">[江西 吉安 泰和县]<strong>泰和代售点火车售票处</strong></div></td></tr><tr align=\"middle\" bgcolor=\"#eff1f3\"><td class=\"tdc\"><div align=\"left\">联系电话：<strong><font color=\"blue\">0796－5321598</font></strong><br/>营业时间：8:30－12:00     13:00－16:30<br/>地址：泰和县工农兵大道 62－2号</div></td></tr></table><br/><table width=\"680\" border=\"1\" align=\"center\" cellpadding=\"4\" bordercolor=\"#3366cc\" style=\"BORDER-COLLAPSE: collapse\"><tr><td class=\"tdc1\" bgcolor=\"#6699cc\" height=\"24\" align=\"middle\"><div align=\"left\">[江西 吉安 永丰县]<strong>永丰代售点火车售票处</strong></div></td></tr><tr align=\"middle\" bgcolor=\"#eff1f3\"><td class=\"tdc\"><div align=\"left\">联系电话：<strong><font color=\"blue\">0796－2267058</font></strong><br/>营业时间：8:00－12:00     12:30－17:30<br/>地址：永丰县恩江北路128号</div></td></tr></table><br/><table width=\"680\" border=\"1\" align=\"center\" cellpadding=\"4\" bordercolor=\"#3366cc\" style=\"BORDER-COLLAPSE: collapse\"><tr><td class=\"tdc1\" bgcolor=\"#eff1f3\" height=\"24\" align=\"middle\"><div align=\"left\"><a href=\"/daishoudian/JiAn.htm\" target=\"_blank\">更多吉安代售点...</a></div></td></tr></table><br/><div align=\"center\"><script type=\"text/javascript\">var cpro_id = \"u1649547\";</script><script src=\"http://cpro.baidustatic.com/cpro/ui/c.js\" type=\"text/javascript\"></script></div><br/><p align=\"center\">请选择上面相应的省、市进入查询或： 输入<FONT color=blue>车站名称</FONT>、<FONT color=blue>列车车次</FONT>或<FONT color=blue>出发及目的地</FONT>查询：</p><p align=\"center\"><table width=\"600\" border=\"1\" align=\"center\" cellpadding=\"3\" cellspacing=\"0\" borderColorLight=\"#008000\" borderColorDark=\"#ffffff\">	<form action=\"/train/search138.asp\" method=\"post\" enctype=\"application/x-www-form-urlencoded\" name=\"bystation\" onsubmit=\"return ChkForm(this)\">	<tr>		<td>按车站名称查询</td>		<td align=\"left\"><input name=\"stationname\" type=\"text\" id=\"stationname\"><input type=\"submit\" name=\"Submit\" value=\"提交\">			<input name=\"act\" type=\"hidden\" id=\"act\" value=\"1\"></td>	</tr>	</form>	<form action=\"/train/search138.asp\" method=\"post\" enctype=\"application/x-www-form-urlencoded\" name=\"bytrain\" onsubmit=\"return ChkForm(this)\"><tr>		<td>按列车车次查询</td>		<td align=\"left\"><input name=\"trainname\" type=\"text\" id=\"trainname\"><input type=\"submit\" name=\"Submit\" value=\"提交\">			<input name=\"act\" type=\"hidden\" id=\"act\" value=\"2\"></td>	</tr>	</form>	<form action=\"/train/search138.asp\" method=\"post\" enctype=\"application/x-www-form-urlencoded\" name=\"byroute\" onsubmit=\"return ChkForm(this)\">	<tr>		<td>按出发地点-目的地查询</td>		<td align=\"left\"><input name=\"from\" type=\"text\" id=\"from\">			-			<input name=\"to\" type=\"text\" id=\"to\"> 			<input type=\"submit\" name=\"Submit\" value=\"提交\">			<input name=\"act\" type=\"hidden\" id=\"act\" value=\"3\"></td>	</tr>	</form></table><center style=\"padding:3px\"><iframe src=\"/jss/bd_460x60.htm\" frameborder=\"no\" width=\"460\" height=\"60\" border=\"0\" marginwidth=\"0\" marginheight=\"0\" scrolling=\"no\"></iframe></center></p><p align=\"center\"><font color=\"#008000\">手机WAP上网查询:</font><fontcolor=\"#FF0000\">wap.ip138.com</font><font color=\"#008000\">用手机随时可以查</font></p><p align=\"center\">　</p><p align=\"center\"></a>联系我们.请<a href=\"http://www.ip138.com/mail.htm\" target=\"_blank\">发email</a>.或给<a href=\"http://qq.3533.com:8080/book.asp?siteid=7\" rel=\"nofollow\" target=\"_blank\">我们留言</a>谢谢!</p><p align=\"center\"><a href=\"http://www.miibeian.gov.cn/\" rel=\"nofollow\" target=\"_blank\">粤ICP备05004654号</a></p><script type=\"text/javascript\" src=\"http://tajs.qq.com/stats?sId=36241669\" charset=\"UTF-8\"></script></body></html>";
            string result = GetResult(xml);
            if(result!="")
                tbContent.Text +=","+result;
        }
        private string GetResult(string xml)
        { 
             string subxml="";
             string strResult = "";
             string a1,a2="";
             try
             {
                 Regex reg = new Regex("(?<uuu><div align=\"center\" id=\"checilist\"><table.+</table></div>)", RegexOptions.Singleline);                 
                 Match m = reg.Match(xml);
                 if (m.Success)
                 {
                     subxml = m.Groups["uuu"].Value;
                     XmlDocument xd = new XmlDocument();
                     //xd.XmlResolver
                     
                     xd.XmlResolver = null;//不解释外部资源
                     xd.LoadXml(subxml);
                     XmlNodeList xnl = xd.SelectNodes("/div[@id='checilist']/table/tr");

                     foreach (XmlNode xn in xnl)
                     {
                         //strResult += "," + xn.FirstChild.InnerText;
                         a1=xn.FirstChild.InnerText;
                         a2=xn.ChildNodes[4].InnerText;
                         if (!ht.Contains(a1))
                         {
                             ht.Add(a1, a2);
                         }
                     }
                     strResult = a2;
                 }
             }
             catch (Exception ex)
             {
                 tbContent.Text = ex.Message;
                 return "";
             }
             return strResult;
        }
        private string GetResultR(string xml)
        {
            string subxml = "";
            string strResult = "";
            string a1, a2 = "",a3="";
            try
            {
                //Regex reg = new Regex("(?<uuu><table.*T2_timelist_table\">.*(<tr.*>.*<strong>(?<ddd>[\\w/]+)</strong></a>.*</tr>)+.*</table>)", RegexOptions.Singleline);
                //Regex reg = new Regex("<tr((?!<tr)(?!</tr).)*<strong>(?<uuu>[\\w/]+)</strong></a>((?!<tr)(?!</tr).)*</tr>", RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                //Regex reg = new Regex("<tr((?!<tr)(?!</tr).)*<strong>(?<uuu>[\\w/]+)</strong></a>((?!<tr)(?!</tr).)*<a[^<>]*>(?<sss>[\u4e00-\u9fa5]*)</a>\\)</td>((?!<tr)(?!</tr).)*</tr>", RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                Regex reg = new Regex("<tr((?!<tr)(?!</tr).)*<strong>(?<uuu>[\\w/]+)</strong></a>[^<>]*<a[^<>]*>(?<ddd>[\u4e00-\u9fa5]*)</a>[^<>]*<a[^<>]*>(?<sss>[\u4e00-\u9fa5]*)</a>\\)</td>((?!<tr)(?!</tr).)*</tr>", RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                MatchCollection m = reg.Matches(xml);
                //[\u4e00-\u9fa5] 
                //while (m.Success)
                //{
                //    strResult+=","+m.Groups["uuu"].Value;
                //    m = m.NextMatch();
                //}
                for (int i = 0; i < m.Count; i++)
                {
                    a1=m[i].Groups["uuu"].Value;
                    a2 = m[i].Groups["sss"].Value;
                    a3 = m[i].Groups["ddd"].Value;
                    strResult += "," + a2+"-"+a3;
                    if (!ht.Contains(a1))
                    {
                        ht.Add(a1, a2);
                    }
                }
                strResult =a3;
                //tbContent.Text = strResult;
            }
            catch (Exception ex)
            {
                tbContent.Text = ex.Message;
                return "";
            }
            return strResult;
        }
        private string GetResultRX(string xml,string sid)
        {             
            string strResult = "";
            string a1="", a2 = "", a3 = "";
            int iSuccess=0;
            try
            {
                //Regex reg = new Regex("(?<uuu><table.*T2_timelist_table\">.*(<tr.*>.*<strong>(?<ddd>[\\w/]+)</strong></a>.*</tr>)+.*</table>)", RegexOptions.Singleline);
                //Regex reg = new Regex("<tr((?!<tr)(?!</tr).)*<strong>(?<uuu>[\\w/]+)</strong></a>((?!<tr)(?!</tr).)*</tr>", RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                //Regex reg = new Regex("<tr((?!<tr)(?!</tr).)*<strong>(?<uuu>[\\w/]+)</strong></a>((?!<tr)(?!</tr).)*<a[^<>]*>(?<sss>[\u4e00-\u9fa5]*)</a>\\)</td>((?!<tr)(?!</tr).)*</tr>", RegexOptions.Singleline | RegexOptions.ExplicitCapture);
                Regex reg = new Regex("\"TrainName\":\"(?<station>[\\w/]+)\"", RegexOptions.Singleline);
                MatchCollection m = reg.Matches(xml);
                //[\u4e00-\u9fa5] 
                //while (m.Success)
                //{
                //    strResult+=","+m.Groups["uuu"].Value;
                //    m = m.NextMatch();
                //}
                
                for (int i = 0; i < m.Count; i++)
                {
                    a1 = m[i].Groups["station"].Value;
                    if (!ht.Contains(a1))
                    {
                        ht.Add(a1, sid);
                    }
                }
                iSuccess=a1!=""?1:0;
                if (!hts.Contains(sid))
                    hts.Add(sid, iSuccess);
                strResult = a1;
                //tbContent.Text = strResult;
            }
            catch (Exception ex)
            {
                tbContent.Text = ex.Message;
                return "";
            }
            return strResult;
        }

        private void GetList(string stationName)
        {
            Random rn = new Random();
            string url = "http://www.huoche.com.cn/chaxun/resultz.php" + "?rnd=" + rn.Next(1, 1000000).ToString();
            url += "&txtchezhan=" + stationName;
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
        private void GetListX(string stationName)
        {
            Random rn = new Random();
            string url = "http://trains.ctrip.com/TrainBooking/Schedule/Station.aspx?rnd=" + rn.Next(1, 1000000).ToString();
            url += "&station=" + stationName;            
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            LinkData ld = new LinkData();
            ld.hwr = req;
            ld.stationid = stationName;
            req.BeginGetResponse(new AsyncCallback(OnResponseX), ld);
        }
        int initNum = 0, totalNum=0;
        string[] arrCity=null, arrDistricts=null;
        private void btListDBData_Click(object sender, EventArgs e)
        {
            ht = new Hashtable();
            hts = new Hashtable();
            
            //string citys = "北京,天津,石家庄,唐山,秦皇岛,邯郸,邢台,保定,张家口,承德,沧州,廊坊,衡水,太原,大同,阳泉,长治,晋城,朔州,晋中,运城,忻州,临汾,吕梁,呼和浩特,包头,乌海,赤峰,通辽,鄂尔多斯,呼伦贝尔,巴彦淖尔,乌兰察布,沈阳,大连,鞍山,抚顺,本溪,丹东,锦州,营口,阜新,辽阳,盘锦,铁岭,朝阳,葫芦岛,长春,吉林,四平,辽源,通化,白山,松原,白城,哈尔滨,齐齐哈尔,鸡西,鹤岗,双鸭山,大庆,伊春,佳木斯,七台河,牡丹江,黑河,绥化,大兴安岭,上海,南京,无锡,徐州,常州,苏州,南通,连云港,淮安,盐城,扬州,镇江,泰州,宿迁,杭州,宁波,温州,嘉兴,湖州,绍兴,金华,衢州,舟山,台州,丽水,合肥,芜湖,蚌埠,淮南,马鞍山,淮北,铜陵,安庆,黄山,滁州,阜阳,宿州,巢湖,六安,亳州,池州,宣城,福州,厦门,莆田,三明,泉州,漳州,南平,龙岩,宁德,南昌,景德镇,萍乡,九江,新余,鹰潭,赣州,吉安,宜春,抚州,上饶,济南,青岛,淄博,枣庄,东营,烟台,潍坊,济宁,泰安,威海,日照,莱芜,临沂,德州,聊城,滨州,荷泽,郑州,开封,洛阳,平顶山,安阳,鹤壁,新乡,焦作,濮阳,许昌,漯河,三门峡,南阳,商丘,信阳,周口,驻马店,武汉,黄石,十堰,宜昌,襄樊,鄂州,荆门,孝感,荆州,黄冈,咸宁,随州,长沙,株洲,湘潭,衡阳,邵阳,岳阳,常德,张家界,益阳,郴州,永州,怀化,娄底,广州,韶关,深圳,珠海,汕头,佛山,江门,湛江,茂名,肇庆,惠州,梅州,汕尾,河源,阳江,清远,东莞,中山,潮州,揭阳,云浮,南宁,柳州,桂林,梧州,北海,防城港,钦州,贵港,玉林,百色,贺州,河池,来宾,崇左,海口,三亚,重庆,成都,自贡,攀枝花,泸州,德阳,绵阳,广元,遂宁,内江,乐山,南充,眉山,宜宾,广安,达州,雅安,巴中,资阳,贵阳,六盘水,遵义,安顺,铜仁,毕节,昆明,曲靖,玉溪,保山,昭通,丽江,思茅,临沧,拉萨,昌都,山南,日喀则,那曲,阿里,林芝,西安,铜川,宝鸡,咸阳,渭南,延安,汉中,榆林,安康,商洛,兰州,嘉峪关,金昌,白银,天水,武威,张掖,平凉,酒泉,庆阳,定西,陇南,西宁,海东,银川,石嘴山,吴忠,固原,中卫,乌鲁木齐,克拉玛依,吐鲁番,哈密,阿克苏,喀什,和田,塔城,阿勒泰,石河子,阿拉尔,图木舒克,五家渠";
            //string districts = "密云,延庆,宁河,静海,蓟县,井陉,正定,栾城,行唐,灵寿,高邑,深泽,赞皇,无极,平山,元氏,赵县,辛集,藁城,晋州,新乐,鹿泉,滦县,滦南,乐亭,迁西,玉田,唐海,遵化,迁安,青龙满族自治,昌黎,抚宁,卢龙,邯郸,临漳,成安,大名,涉县,磁县,肥乡,永年,邱县,鸡泽,广平,馆陶,魏县,曲周,武安,邢台,临城,内丘,柏乡,隆尧,任县,南和,宁晋,巨鹿,新河,广宗,平乡,威县,清河,临西,南宫,沙河,满城,清苑,涞水,阜平,徐水,定兴,唐县,高阳,容城,涞源,望都,安新,易县,曲阳,蠡县,顺平,博野,雄县,涿州,定州,安国,高碑店,宣化,张北,康保,沽源,尚义,蔚县,阳原,怀安,万全,怀来,涿鹿,赤城,崇礼,承德,兴隆,平泉,滦平,隆化,丰宁满族自治,宽城满族自治,围场满族蒙古族自治,沧县,青县,东光,海兴,盐山,肃宁,南皮,吴桥,献县,孟村回族自治,泊头,任丘,黄骅,河间,固安,永清,香河,大城,文安,大厂回族自治,霸州,三河,枣强,武邑,武强,饶阳,安平,故城,景县,阜城,冀州,深州,清徐,阳曲,娄烦,古交,城区,矿区,阳高,天镇,广灵,灵丘,浑源,左云,大同,城区,矿区,郊区,平定,盂县,城区,郊区,长治,襄垣,屯留,平顺,黎城,壶关,长子,武乡,沁县,沁源,潞城,城区,沁水,阳城,陵川,泽州,高平,山阴,应县,右玉,怀仁,榆社,左权,和顺,昔阳,寿阳,太谷,祁县,平遥,灵石,介休,临猗,万荣,闻喜,稷山,新绛,绛县,垣曲,夏县,平陆,芮城,永济,河津,定襄,五台,代县,繁峙,宁武,静乐,神池,五寨,岢岚,河曲,保德,偏关,原平,曲沃,翼城,襄汾,洪洞,古县,安泽,浮山,吉县,乡宁,大宁,隰县,永和,蒲县,汾西,侯马,霍州,文水,交城,兴县,临县,柳林,石楼,岚县,方山,中阳,交口,孝义,汾阳,托克托,和林格尔,清水河,武川,固阳,林西,宁城,开鲁,霍林郭勒,满洲里,牙克石,扎兰屯,额尔古纳,根河,五原,磴口,卓资,化德,商都,兴和,凉城,丰镇,乌兰浩特,阿尔山,突泉,二连浩特,锡林浩特,多伦,辽中,康平,法库,新民,长海,瓦房店,普兰店,庄河,台安,岫岩满族自治,海城,抚顺,新宾满族自治,清原满族自治,本溪满族自治,桓仁满族自治,宽甸满族自治,东港,凤城,黑山,义县,凌海,北宁,盖州,大石桥,阜新蒙古族自治,彰武,辽阳,灯塔,大洼,盘山,铁岭,西丰,昌图,调兵山,开原,朝阳,建平,喀喇沁左翼蒙古族自治,北票,凌源,绥中,建昌,兴城,农安,九台,榆树,德惠,永吉,蛟河,桦甸,舒兰,磐石,梨树,伊通满族自治,公主岭,双辽,东丰,东辽,通化,辉南,柳河,梅河口,集安,抚松,靖宇,长白朝鲜族自治,江源,临江,前郭尔罗斯蒙古族自治,长岭,乾安,扶余,镇赉,通榆,洮南,大安,延吉,图们,敦化,珲春,龙井,和龙,汪清,安图,依兰,方正,宾县,巴彦,木兰,通河,延寿,阿城,双城,尚志,五常,龙江,依安,泰来,甘南,富裕,克山,克东,拜泉,讷河,鸡东,虎林,密山,萝北,绥滨,集贤,友谊,宝清,饶河,肇州,肇源,林甸,杜尔伯特蒙古族自治,嘉荫,铁力,郊区,桦南,桦川,汤原,抚远,同江,富锦,勃利,东宁,林口,绥芬河,海林,宁安,穆棱,嫩江,逊克,孙吴,北安,五大连池,望奎,兰西,青冈,庆安,明水,绥棱,安达,肇东,海伦,呼玛,塔河,漠河,崇明,溧水,高淳,江阴,宜兴,丰县,沛县,铜山,睢宁,新沂,邳州,溧阳,金坛,常熟,张家港,昆山,吴江,太仓,海安,如东,启东,如皋,通州,海门,赣榆,东海,灌云,灌南,涟水,洪泽,盱眙,金湖,响水,滨海,阜宁,射阳,建湖,东台,大丰,宝应,仪征,高邮,江都,丹阳,扬中,句容,兴化,靖江,泰兴,姜堰,沭阳,泗阳,泗洪,桐庐,淳安,建德,富阳,临安,象山,宁海,余姚,慈溪,奉化,洞头,永嘉,平阳,苍南,文成,泰顺,瑞安,乐清,嘉善,海盐,海宁,平湖,桐乡,德清,长兴,安吉,绍兴,新昌,诸暨,上虞,嵊州,武义,浦江,磐安,兰溪,义乌,东阳,永康,常山,开化,龙游,江山,岱山,嵊泗,玉环,三门,天台,仙居,温岭,临海,青田,缙云,遂昌,松阳,云和,庆元,景宁畲族自治,龙泉,长丰,肥东,肥西,芜湖,繁昌,南陵,怀远,五河,固镇,凤台,当涂,濉溪,郊区,铜陵,郊区,怀宁,枞阳,潜山,太湖,宿松,望江,岳西,桐城,歙县,休宁,黟县,祁门,来安,全椒,定远,凤阳,天长,明光,临泉,太和,阜南,颍上,界首,砀山,萧县,灵璧,泗县,庐江,无为,含山,和县,寿县,霍邱,舒城,金寨,霍山,涡阳,蒙城,利辛,东至,石台,青阳,郎溪,广德,泾县,绩溪,旌德,宁国,闽侯,连江,罗源,闽清,永泰,平潭,福清,长乐,仙游,明溪,清流,宁化,大田,尤溪,沙县,将乐,泰宁,建宁,永安,惠安,安溪,永春,德化,金门,石狮,晋江,南安,云霄,漳浦,诏安,长泰,东山,南靖,平和,华安,龙海,顺昌,浦城,光泽,松溪,政和,邵武,武夷山,建瓯,建阳,长汀,永定,上杭,武平,连城,漳平,霞浦,古田,屏南,寿宁,周宁,柘荣,福安,福鼎,南昌,新建,安义,进贤,浮梁,乐平,莲花,上栗,芦溪,九江,武宁,修水,永修,德安,星子,都昌,湖口,彭泽,瑞昌,分宜,余江,贵溪,赣县,信丰,大余,上犹,崇义,安远,龙南,定南,全南,宁都,于都,兴国,会昌,寻乌,石城,瑞金,南康,吉安,吉水,峡江,新干,永丰,泰和,遂川,万安,安福,永新,井冈山,奉新,万载,上高,宜丰,靖安,铜鼓,丰城,樟树,高安,南城,黎川,南丰,崇仁,乐安,宜黄,金溪,资溪,东乡,广昌,上饶,广丰,玉山,铅山,横峰,弋阳,余干,鄱阳,万年,婺源,德兴,平阴,济阳,商河,章丘,胶州,即墨,平度,胶南,莱西,桓台,高青,沂源,滕州,垦利,利津,广饶,长岛,龙口,莱阳,莱州,蓬莱,招远,栖霞,海阳,临朐,昌乐,青州,诸城,寿光,安丘,高密,昌邑,微山,鱼台,金乡,嘉祥,汶上,泗水,梁山,曲阜,兖州,邹城,宁阳,东平,新泰,肥城,文登,荣成,乳山,五莲,莒县,沂南,郯城,沂水,苍山,费县,平邑,莒南,蒙阴,临沭,陵县,宁津,庆云,临邑,齐河,平原,夏津,武城,乐陵,禹城,阳谷,莘县,茌平,东阿,冠县,高唐,临清,惠民,阳信,无棣,沾化,博兴,邹平,曹县,单县,成武,巨野,郓城,鄄城,定陶,东明,中牟,巩义,荥阳,新密,新郑,登封,郊区,杞县,通许,尉氏,开封,兰考,孟津,新安,栾川,嵩县,汝阳,宜阳,洛宁,伊川,偃师,宝丰,叶县,鲁山,郏县,舞钢,汝州,安阳,汤阴,滑县,内黄,林州,浚县,淇县,新乡,获嘉,原阳,延津,封丘,长垣,卫辉,辉县,修武,博爱,武陟,温县,济源,沁阳,孟州,清丰,南乐,范县,台前,濮阳,许昌,鄢陵,襄城,禹州,长葛,舞阳,临颍,渑池,陕县,卢氏,义马,灵宝,南召,方城,西峡,镇平,内乡,淅川,社旗,唐河,新野,桐柏,邓州,民权,睢县,宁陵,柘城,虞城,夏邑,永城,罗山,光山,新县,商城,固始,潢川,淮滨,息县,扶沟,西华,商水,沈丘,郸城,淮阳,太康,鹿邑,项城,西平,上蔡,平舆,正阳,确山,泌阳,汝南,遂平,新蔡,阳新,大冶,郧县,郧西,竹山,竹溪,房县,丹江口,远安,兴山,秭归,长阳土家族自治,五峰土家族自治,宜都,当阳,枝江,南漳,谷城,保康,老河口,枣阳,宜城,京山,沙洋,钟祥,孝昌,大悟,云梦,应城,安陆,汉川,公安,监利,江陵,石首,洪湖,松滋,团风,红安,罗田,英山,浠水,蕲春,黄梅,麻城,武穴,嘉鱼,通城,崇阳,通山,赤壁,广水,恩施,利川,建始,巴东,宣恩,咸丰,来凤,鹤峰,仙桃,潜江,天门,长沙,望城,宁乡,浏阳,株洲,攸县,茶陵,炎陵,醴陵,湘潭,湘乡,韶山,衡阳,衡南,衡山,衡东,祁东,耒阳,常宁,邵东,新邵,邵阳,隆回,洞口,绥宁,新宁,城步苗族自治,武冈,岳阳,华容,湘阴,平江,汨罗,临湘,安乡,汉寿,澧县,临澧,桃源,石门,津市,慈利,桑植,南县,桃江,安化,沅江,桂阳,宜章,永兴,嘉禾,临武,汝城,桂东,安仁,资兴,祁阳,东安,双牌,道县,江永,宁远,蓝山,新田,江华瑶族自治,中方,沅陵,辰溪,溆浦,会同,麻阳苗族自治,新晃侗族自治,芷江侗族自治,靖州苗族侗族自治,通道侗族自治,洪江,双峰,新化,冷水江,涟源,吉首,泸溪,凤凰,花垣,保靖,古丈,永顺,龙山,增城,从化,始兴,仁化,翁源,乳源瑶族自治,新丰,乐昌,南雄,南澳,台山,开平,鹤山,恩平,遂溪,徐闻,廉江,雷州,吴川,电白,高州,化州,信宜,广宁,怀集,封开,德庆,高要,四会,博罗,惠东,龙门,梅县,大埔,丰顺,五华,平远,蕉岭,兴宁,城区,海丰,陆河,陆丰,紫金,龙川,连平,和平,东源,阳西,阳东,阳春,佛冈,阳山,连山壮族瑶族自治,连南瑶族自治,清新,英德,连州,潮安,饶平,揭东,揭西,惠来,普宁,新兴,郁南,云安,罗定,武鸣,隆安,马山,上林,宾阳,横县,柳江,柳城,鹿寨,融安,融水苗族自治,三江侗族自治,阳朔,临桂,灵川,全州,兴安,永福,灌阳,龙胜各族自治,资源,平乐,荔蒲,恭城瑶族自治,苍梧,藤县,蒙山,岑溪,合浦,上思,东兴,灵山,浦北,平南,桂平,容县,陆川,博白,兴业,北流,田阳,田东,平果,德保,靖西,那坡,凌云,乐业,田林,西林,隆林各族自治,昭平,钟山,富川瑶族自治,南丹,天峨,凤山,东兰,罗城仫佬族自治,环江毛南族自治,巴马瑶族自治,都安瑶族自治,大化瑶族自治,宜州,忻城,象州,武宣,金秀瑶族自治,合山,扶绥,宁明,龙州,大新,天等,凭祥,五指山,琼海,儋州,文昌,万宁,东方,定安,屯昌,澄迈,临高,白沙黎族自治,昌江黎族自治,乐东黎族自治,陵水黎族自治,保亭黎族苗族自治,琼中黎族苗族自治,綦江,潼南,铜梁,大足,荣昌,璧山,梁平,城口,丰都,垫江,武隆,忠县,开县,云阳,奉节,巫山,巫溪,石柱土家族自治,秀山土家族苗族自治,酉阳土家族苗族自治,彭水苗族土家族自治,江津,合川,永川,南川,金堂,双流,郫县,大邑,蒲江,新津,都江堰,彭州,邛崃,崇州,荣县,富顺,东区,西区,米易,盐边,泸县,合江,叙永,古蔺,中江,罗江,广汉,什邡,绵竹,三台,盐亭,安县,梓潼,北川羌族自治,平武,江油,旺苍,青川,剑阁,苍溪,蓬溪,射洪,大英,威远,资中,隆昌,犍为,井研,夹江,沐川,峨边彝族自治,马边彝族自治,峨眉山,南部,营山,蓬安,仪陇,西充,阆中,仁寿,彭山,洪雅,丹棱,青神,宜宾,南溪,江安,长宁,高县,珙县,筠连,兴文,屏山,岳池,武胜,邻水,华蓥,达县,宣汉,开江,大竹,渠县,万源,名山,荥经,汉源,石棉,天全,芦山,宝兴,通江,南江,平昌,安岳,乐至,简阳,汶川,理县,茂县,松潘,九寨沟,金川,小金,黑水,马尔康,壤塘,阿坝,若尔盖,红原,康定,泸定,丹巴,九龙,雅江,道孚,炉霍,甘孜,新龙,德格,白玉,石渠,色达,理塘,巴塘,乡城,稻城,得荣,西昌,木里藏族自治,盐源,德昌,会理,会东,宁南,普格,布拖,金阳,昭觉,喜德,冕宁,越西,甘洛,美姑,雷波,开阳,息烽,修文,清镇,水城,盘县,遵义,桐梓,绥阳,正安,道真仡佬族苗族自治,务川仡佬族苗族自治,凤冈,湄潭,余庆,习水,赤水,仁怀,平坝,普定,镇宁布依族苗族自治,关岭布依族苗族自治,紫云苗族布依族自治,铜仁,江口,玉屏侗族自治,石阡,思南,印江土家族苗族自治,德江,沿河土家族自治,松桃苗族自治,兴义,兴仁,普安,晴隆,贞丰,望谟,册亨,安龙,毕节,大方,黔西,金沙,织金,纳雍,威宁彝族回族苗族自治,赫章,凯里,黄平,施秉,三穗,镇远,岑巩,天柱,锦屏,剑河,台江,黎平,榕江,从江,雷山,麻江,丹寨,都匀,福泉,荔波,贵定,瓮安,独山,平塘,罗甸,长顺,龙里,惠水,三都水族自治,呈贡,晋宁,富民,宜良,石林彝族自治,嵩明,禄劝彝族苗族自治,寻甸回族彝族自治,安宁,马龙,陆良,师宗,罗平,富源,会泽,沾益,宣威,江川,澄江,通海,华宁,易门,峨山彝族自治,新平彝族傣族自治,元江哈尼族彝族傣族自治,施甸,腾冲,龙陵,昌宁,鲁甸,巧家,盐津,大关,永善,绥江,镇雄,彝良,威信,水富,玉龙纳西族自治,永胜,华坪,宁蒗彝族自治,普洱哈尼族彝族自治,墨江哈尼族自治,景东彝族自治,景谷傣族彝族自治,镇沅彝族哈尼族拉祜族自治,江城哈尼族彝族自治,孟连傣族拉祜族佤族自治,澜沧拉祜族自治,西盟佤族自治,凤庆,云县,永德,镇康,双江拉祜族佤族布朗族傣族自治,耿马傣族佤族自治,沧源佤族自治,楚雄,双柏,牟定,南华,姚安,大姚,永仁,元谋,武定,禄丰,个旧,开远,蒙自,屏边苗族自治,建水,石屏,弥勒,泸西,元阳,红河,金平苗族瑶族傣族自治,绿春,河口瑶族自治,文山,砚山,西畴,麻栗坡,马关,丘北,广南,富宁,景洪,勐海,勐腊,大理,漾濞彝族自治,祥云,宾川,弥渡,南涧彝族自治,巍山彝族回族自治,永平,云龙,洱源,剑川,鹤庆,瑞丽,潞西,梁河,盈江,陇川,泸水,福贡,贡山独龙族怒族自治,兰坪白族普米族自治,香格里拉,德钦,维西傈僳族自治,林周,当雄,尼木,曲水,堆龙德庆,达孜,墨竹工卡,昌都,江达,贡觉,类乌齐,丁青,察雅,八宿,左贡,芒康,洛隆,边坝,乃东,扎囊,贡嘎,桑日,琼结,曲松,措美,洛扎,加查,隆子,错那,浪卡子,日喀则,南木林,江孜,定日,萨迦,拉孜,昂仁,谢通门,白朗,仁布,康马,定结,仲巴,亚东,吉隆,聂拉木,萨嘎,岗巴,那曲,嘉黎,比如,聂荣,安多,申扎,索县,班戈,巴青,尼玛,普兰,札达,噶尔,日土,革吉,改则,措勤,林芝,工布江达,米林,墨脱,波密,察隅,朗县,蓝田,周至,户县,高陵,宜君,凤翔,岐山,扶风,眉县,陇县,千阳,麟游,凤县,太白,三原,泾阳,乾县,礼泉,永寿,彬县,长武,旬邑,淳化,武功,兴平,华县,潼关,大荔,合阳,澄城,蒲城,白水,富平,韩城,华阴,延长,延川,子长,安塞,志丹,吴旗,甘泉,富县,洛川,宜川,黄龙,黄陵,南郑,城固,洋县,西乡,勉县,宁强,略阳,镇巴,留坝,佛坪,神木,府谷,横山,靖边,定边,绥德,米脂,佳县,吴堡,清涧,子洲,汉阴,石泉,宁陕,紫阳,岚皋,平利,镇坪,旬阳,白河,洛南,丹凤,商南,山阳,镇安,柞水,永登,皋兰,榆中,永昌,靖远,会宁,景泰,清水,秦安,甘谷,武山,张家川回族自治,民勤,古浪,天祝藏族自治,肃南裕固族自治,民乐,临泽,高台,山丹,泾川,灵台,崇信,华亭,庄浪,静宁,金塔,安西,肃北蒙古族自治,阿克塞哈萨克族自治,玉门,敦煌,庆城,环县,华池,合水,正宁,宁县,镇原,通渭,陇西,渭源,临洮,漳县,岷县,成县,文县,宕昌,康县,西和,礼县,徽县,两当,临夏,临夏,康乐,永靖,广河,和政,东乡族自治,积石山保安族东乡族撒拉族自治,合作,临潭,卓尼,舟曲,迭部,玛曲,碌曲,夏河,大通回族土族自治,湟中,湟源,平安,民和回族土族自治,乐都,互助土族自治,化隆回族自治,循化撒拉族自治,门源回族自治,祁连,海晏,刚察,同仁,尖扎,泽库,河南蒙古族自治,共和,同德,贵德,兴海,贵南,玛沁,班玛,甘德,达日,久治,玛多,玉树,杂多,称多,治多,囊谦,曲麻莱,格尔木,德令哈,乌兰,都兰,天峻,永宁,贺兰,灵武,平罗,盐池,同心,青铜峡,西吉,隆德,泾源,彭阳,中宁,海原,乌鲁木齐,吐鲁番,鄯善,托克逊,哈密,巴里坤哈萨克自治,伊吾,昌吉,阜康,米泉,呼图壁,玛纳斯,奇台,吉木萨尔,木垒哈萨克自治,博乐,精河,温泉,库尔勒,轮台,尉犁,若羌,且末,焉耆回族自治,和静,和硕,博湖,阿克苏,温宿,库车,沙雅,新和,拜城,乌什,阿瓦提,柯坪,阿图什,阿克陶,阿合奇,乌恰,喀什,疏附,疏勒,英吉沙,泽普,莎车,叶城,麦盖提,岳普湖,伽师,巴楚,塔什库尔干塔吉克自治,和田,和田,墨玉,皮山,洛浦,策勒,于田,民丰,伊宁,奎屯,伊宁,察布查尔锡伯自治,霍城,巩留,新源,昭苏,特克斯,尼勒克,塔城,乌苏,额敏,沙湾,托里,裕民,和布克赛尔蒙古自治,阿勒泰,布尔津,富蕴,福海,哈巴河,青河,吉木乃";
            //arrCity = districts.Split(',');
            //arrDistricts = districts.Split(',');
            //totalNum = arrCity.Length;
            initNum = 0;
            timer1.Start();
            btTimerControl.Text = "停止";
            //for (int i = 0; i < arrCity.Length; i++)
            //{
            //    GetList(arrCity[i]);
            //}
            
            //for (int i = 0; i < arrDistricts.Length; i++)
            //{
            //    GetList(arrDistricts[i]);
            //}
        }

        private void btResult_Click(object sender, EventArgs e)
        {
            tbContent.Text = WriteCheCi("checi");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (totalNum > 0 && stationNames != null)
            {
                if (initNum < totalNum)
                {
                    //GetList(arrCity[initNum]);
                    GetListX(stationNames[initNum]);
                    initNum++;
                }
                if (initNum >= totalNum)
                {
                    timer1.Stop();
                    btTimerControl.Text = "启动(已完成)";
                    WriteResult("resultx");
                    WriteCheCi("resultx");
                }
            }
        }

        private void btTimerControl_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                 
                timer1.Stop();
                btTimerControl.Text = "启动";
                WriteResult("result");
            }
            else
            {
                timer1.Start();
                btTimerControl.Text = "停止";
            }
        }
        private string WriteCheCi(string name)
        {
            string railwayId = "";
            foreach (object o in ht.Keys)
            {
                railwayId += "," + o.ToString();
            }
            Tom.Common.GYF.WriteLog("车次:" + railwayId.TrimStart(','), name);
            return railwayId;
        }
        private void WriteResult(string name)
        {
            string val, success = "", fail = "";
            foreach (string key in hts.Keys)
            {
                val = hts[key].ToString();
                if (val == "1")
                    success += "," + key;
                else
                    fail += "," + key;
            }
            Tom.Common.GYF.WriteLog("成功:" + success, name);
            Tom.Common.GYF.WriteLog("失败:" + fail, name);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var dt=DataAccess.dataTable("SELECT pyName FROM  StationData WHERE isEnable=true");
            if (dt != null)
            {
                totalNum = dt.Rows.Count;
                stationNames = new string[totalNum];
                int init=0;
                foreach (DataRow dr in dt.Rows)
                {
                    stationNames[init] = dr[0].ToString();
                    init++;
                }
            }
        }

    }
    public class LinkData
    {
        public HttpWebRequest hwr;
        public string stationid;
    }
}
