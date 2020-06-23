namespace ALDDSN
{
    partial class DSN
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DSN));
            this.btnStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.wt = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.lbmsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbRecord = new System.Windows.Forms.ListBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(43, 67);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(235, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(29, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "本地外网IP地址：";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(127, 18);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(137, 21);
            this.txtIP.TabIndex = 6;
            // 
            // wt
            // 
            this.wt.Interval = 6000;
            this.wt.Tick += new System.EventHandler(this.wt_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(43, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(235, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "停止";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbmsg
            // 
            this.lbmsg.AutoSize = true;
            this.lbmsg.Location = new System.Drawing.Point(29, 49);
            this.lbmsg.Name = "lbmsg";
            this.lbmsg.Size = new System.Drawing.Size(0, 12);
            this.lbmsg.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "更新记录";
            // 
            // lbRecord
            // 
            this.lbRecord.FormattingEnabled = true;
            this.lbRecord.ItemHeight = 12;
            this.lbRecord.Location = new System.Drawing.Point(15, 141);
            this.lbRecord.Name = "lbRecord";
            this.lbRecord.Size = new System.Drawing.Size(280, 232);
            this.lbRecord.TabIndex = 11;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // DSN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(307, 377);
            this.Controls.Add(this.lbRecord);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbmsg);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DSN";
            this.Text = "阿里云域名动态工具";
            this.Load += new System.EventHandler(this.DSN_Load);
            this.SizeChanged += new System.EventHandler(this.DSN_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Timer wt;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lbmsg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbRecord;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

