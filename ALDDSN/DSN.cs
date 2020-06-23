using Aliyun.Acs.Alidns.Model.V20150109;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ALDDSN
{
    public partial class DSN : Form
    {
        public DSN()
        {
            InitializeComponent();
            txtIP.Text = GetIp();
            lbRecord.Items.Add("初始化读取IP：" + txtIP.Text);
        }

        //定义全局
        private static string regionId = GetConfig("regionId");    // "cn-shenzhen";
        private static string accessKeyId = GetConfig("accessKeyId");
        private static string accessKeySecret = GetConfig("accessKeySecret");
        private static string recordId = GetConfig("recordId"); // "";//m.ymjob01.com的唯一记录ID
        //或者设置为空，然后下边有从阿里云获取的方法，只是不建议每次都去阿里云获取。
        private static string domain = GetConfig("domain"); //"ymjob01.com";
        private static string rr = GetConfig("rr"); //"m";//子域名
        private static string filename = AppDomain.CurrentDomain.BaseDirectory + "ip.dat";//缓存IP保存的位置
        private static readonly IClientProfile ClientProfile = DefaultProfile.GetProfile(regionId, accessKeyId, accessKeySecret);
        private static readonly IAcsClient Client = new DefaultAcsClient(ClientProfile);


        private void btnStart_Click(object sender, EventArgs e)
        {
            wt.Start();
            wt.Interval = Convert.ToInt32(GetConfig("renew")) * 60000;
            StartSever();
            btnStart.Enabled = false;
        }

        void button2_Click(object sender, EventArgs e)
        {
            wt.Stop();
            btnStart.Enabled = true;
        }

        #region 读取阿里云RecordId
        /// <summary>
        /// 读取阿里云RecordId
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="rr"></param>
        /// <returns></returns>
        static string GetRecordId(string domain, string rr)
        {
            DescribeDomainRecordsRequest reqq = new DescribeDomainRecordsRequest { DomainName = domain, RRKeyWord = rr };
            var rss = Client.GetAcsResponse(reqq);
            foreach (var record in rss.DomainRecords)
            {
                if (record.DomainName.Equals(domain) && record.RR.Equals(rr))
                {
                    return record.RecordId;
                }
            }
            return "";
        }
        #endregion

        #region 读取旧的IP地址
        /// <summary>
        /// 读取旧的IP地址
        /// </summary>
        /// <returns></returns>
        public static string Read()
        {
            FileInfo file = new FileInfo(filename);
            if (!file.Exists)
            {
                Write("尚未获取");
            }
            StreamReader sr = new StreamReader(filename, Encoding.Default);
            string res = "";
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                res += line;
            }
            sr.Close();
            return res;
        }
        #endregion

        #region 写入新的IP地址
        /// <summary>
        /// 写入新的IP地址 不错try 后续慢慢在修改呗
        /// </summary>
        /// <param name="ip"></param>
        public static void Write(string ip)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            //获得字节数组
            byte[] data = Encoding.Default.GetBytes(ip);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }
        #endregion

        #region 获取客户端IP
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        static string GetIp()
        {
            try
            {
                return new WebClient().DownloadString("http://www.ymjob01.cn/GetIP.aspx"); ;
            }
            catch (WebException webEx)
            {
                return "";
            }
        }
        #endregion

        #region 获取配置项
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static string GetConfig(string name)
        {
            var cfa = ReadAppConfig();
            return cfa.AppSettings.Settings[name].Value;
        }
        #endregion

        #region 获取配置文件
        static Configuration _config;
        static Configuration ReadAppConfig()
        {
            if (_config == null)
            {
                string configPath = @"ALDDSN.exe.config";
                _config = ConfigurationManager.OpenMappedExeConfiguration(
                    new ExeConfigurationFileMap()
                    {
                        ExeConfigFilename = configPath
                    }, ConfigurationUserLevel.None);
            }
            return _config;
        }
        #endregion

        #region 定时器控件自动更新
        /// <summary>
        /// 定时器控件自动更新
        /// 1000 等于 1秒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void wt_Tick(object sender, EventArgs e)
        {
            StartSever();
        }
        #endregion

        #region 启动服务
        /// <summary>
        /// 启动服务
        /// </summary>
        void StartSever()
        {
            //获取成功，并且有变化时
            string lastIp = Read();
            txtIP.Text = GetIp();//读取访问一下看看IP更新没有
            string curIp = txtIP.Text.Trim();
            if (!curIp.Equals("") && !lastIp.Equals(curIp))
            {
                switch (GetConfig("ddtype").ToString())
                {
                    case "0": ALiYun(curIp); break;
                    default: break;//阿里云方法
                }
                Write(curIp);//更新成功后保存IP地址到本地
                txtIP.Text = curIp;
                lbmsg.Text = "更新成功：" + curIp + " 时间：" + DateTime.Now;
                lbRecord.Items.Add(lbmsg.Text);
            }
            else
            {
                lbmsg.Text = "IP地址无变化无需更新：" + curIp + " 时间：" + DateTime.Now;
                lbRecord.Items.Add(lbmsg.Text);
            }

        }
        #endregion

        #region 阿里云的DNS -  void ALiYun(string curIp)
        /// <summary>
        /// 阿里云的DNS
        /// </summary>
        /// <param name="curIp"></param>
        void ALiYun(string curIp)
        {
            recordId = GetRecordId(domain, rr);
            if (recordId == "") return;
            //  TTL:我的是免费版，最小为10分钟，600秒
            var request = new UpdateDomainRecordRequest { RecordId = recordId, RR = rr, Type = "A", Value = curIp, TTL = 600, Priority = 10 };
            var dd = Client.DoAction(request);//返回xml  不管是否报错了  反正一些没有用的数据

        }
        #endregion

        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DSN_Load(object sender, EventArgs e)
        {
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            notifyIcon.Icon = new Icon("logo.ico");
            notifyIcon.Visible = false;
            notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            this.SizeChanged += new System.EventHandler(this.DSN_SizeChanged);
        }
        #endregion

        #region 窗体大小变化事件
        /// <summary>
        /// 窗体大小变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DSN_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.notifyIcon.Visible = true;
            }
        }
        #endregion

        #region 小图标点击事件
        /// <summary>
        /// 小图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.notifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }
        #endregion




    }
}
