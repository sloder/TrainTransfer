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
using System.Xml;

namespace DetourHome
{
    public partial class ListCheDetail : Form
    {
        delegate void SysOperate(string msg);          
        public ListCheDetail()
        {
            InitializeComponent();
        }

        private void btGet_Click(object sender, EventArgs e)
        {
            string cc = tbCheCi.Text.Trim();
            if (cc == "")
                return;
            GetCheciBlock(cc);
        }

        private void GetCheciBlock(string cid)
        {
            Random rn = new Random();
            int dd = rn.Next(1, 1000000);
            string url = string.Format("http://trains.ctrip.com/TrainSchedule/{0}/?rnd={1}", cid, dd);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.BeginGetResponse(new AsyncCallback(OnResponse), req);
        }
        protected void OnResponse(IAsyncResult ar)
        {

            WebRequest wrq = (WebRequest)ar.AsyncState;


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
                SysOperate sm = new SysOperate(DealCheCi);
                this.Invoke(sm, strRes);

            }
            catch (Exception ex)
            {
                Tom.Common.GYF.WriteLog(ex, "testErrord");
                SysOperate so = new SysOperate(DisplayDD);
                this.Invoke(so, ex.Message);
            }
        }
        public void DisplayDD(string msg)
        {
            tbContent.Text = msg;
        }
        public void DealCheCi(string xml)
        {
            //tbContent.Text = msg;
            //id="tn" value="Z185"
            string subxml = "";
            string strResult = "";
            string a3 = "未找到!";            
            try
            {                
                Regex reg = new Regex("id=\"tn\" value=\"(?<ddd>\\w+)\"");
                Regex reg2 = new Regex("(?<eee><div class=\"s_bd\">.+)<div class=\"page\">",RegexOptions.Singleline);
                Match m = reg.Match(xml);
                Match m2 = reg2.Match(xml);
                string qCheCi = "", e = "";
                string sql1="";
                if (m.Success)
                {
                    qCheCi = m.Groups["ddd"].Value;  
                }
                if (m2.Success)
                {
                    subxml = m2.Groups["eee"].Value;
                    XmlDocument xd = new XmlDocument();
                    xd.XmlResolver = null;//不解释外部资源
                    xd.LoadXml(subxml);
                    XmlNode xn = xd.SelectSingleNode("//table[@class='tb_result tb_inquiry']/tbody/tr");
                    XmlNodeList xny = xd.SelectNodes("//table[@class='tb_result tb_inquiry tb_gray']/tbody/tr");
                    if (xn != null && xny != null)
                    {
                        a3 = "找到!";
                        XmlNodeList xnl = xn.ChildNodes;
                        CheChiModel ccm = new CheChiModel();
                        ccm.sStation = xnl[1].InnerText.Trim();
                        ccm.eStation = xnl[2].InnerText.Trim();
                        ccm.StartTime = xnl[3].InnerText.Trim();
                        ccm.EndTime = xnl[4].InnerText.Trim();
                        ccm.TotalDuration = xnl[5].InnerText.Trim();
                        ccm.TotalMiles = xnl[6].InnerText.Trim();
                        //string sql1 = @"insert into checiMain (cid) select 9999 as c from dual where 2=2";
                        sql1 = string.Format("update checiMain set stations='{0}',statione='{1}',STime='{2}',ETime='{3}',totalHour='{4}',totalMile='{5}',isenable=1,isupdate=1 where uid='{6}'"
                                            , ccm.sStation, ccm.eStation, ccm.StartTime, ccm.EndTime, ccm.TotalDuration, ccm.TotalMiles, qCheCi);
                        DataAccess.excuteSql(sql1);
                        string sql2 = "";
                        foreach (XmlNode n in xny)
                        {
                            xnl = n.ChildNodes;
                            sql2 = string.Format("insert into CheCiDetail (checi,stationName,inTime,outTime,FarHour,FarMile,idx) select '{0}','{1}','{2}','{3}','{4}','{5}',{6} from dual where not exists(select 1 from CheCiDetail where checi='{0}' AND stationName='{1}')"
                                , qCheCi, xnl[2].InnerText.Trim(), xnl[3].InnerText.Trim(), xnl[4].InnerText.Trim(), xnl[5].InnerText.Trim(), xnl[6].InnerText.Trim(), xnl[1].InnerText.Trim()
                                );
                            DataAccess.excuteSql(sql2);
                        }

                    }
                    
                }
                else
                {
                    sql1 = string.Format("update checiMain set isupdate=1,isenable=0 where uid='{0}'", qCheCi);
                    DataAccess.excuteSql(sql1);
                }
                //[\u4e00-\u9fa5] 
                //while (m.Success)
                //{
                //    strResult+=","+m.Groups["uuu"].Value;
                //    m = m.NextMatch();
                //}

                tbContent.Text+=string.Format(",{0}({1})",qCheCi,a3);
                //tbContent.Text = strResult;
            }
            catch (Exception ex)
            {
                Tom.Common.GYF.WriteLog(ex, "testError");
                tbContent.Text = ex.Message;
                
            }
            
        }
        private DataTable dt = null;
        private int idx = 0;
        private void btTimeX_Click(object sender, EventArgs e)
        {
            AlterStatus();
        }

        private void AlterStatus()
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                btTimeX.Text = "启动";
            }
            else
            {
                idx = 0;
                dt = GetNoUpdate();
                timer1.Start();
                btTimeX.Text = "停止";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > idx)
                {
                    GetCheciBlock(dt.Rows[idx][0].ToString());
                    idx++;
                }
                else
                {

                    AlterStatus();
                }
            }
        }
        private DataTable GetNoUpdate()
        {
            string sql = "select    uid from checimain where isupdate=false and isenable=true";
            return DataAccess.dataTable(sql);
        }

    }
    public struct CheChiModel
    {
        public string sStation;
        public string eStation;
        public string StartTime;
        public string EndTime;
        public string TotalDuration;
        public string TotalMiles;
    }
}
