﻿namespace DetourHome
{
    partial class Form1
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
            this.tbStation = new System.Windows.Forms.TextBox();
            this.btListCurrent = new System.Windows.Forms.Button();
            this.btListDBData = new System.Windows.Forms.Button();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btTest = new System.Windows.Forms.Button();
            this.btResult = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btTimerControl = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbStation
            // 
            this.tbStation.Location = new System.Drawing.Point(35, 12);
            this.tbStation.Name = "tbStation";
            this.tbStation.Size = new System.Drawing.Size(111, 21);
            this.tbStation.TabIndex = 0;
            // 
            // btListCurrent
            // 
            this.btListCurrent.Location = new System.Drawing.Point(149, 10);
            this.btListCurrent.Name = "btListCurrent";
            this.btListCurrent.Size = new System.Drawing.Size(54, 23);
            this.btListCurrent.TabIndex = 1;
            this.btListCurrent.Text = "查询";
            this.btListCurrent.UseVisualStyleBackColor = true;
            this.btListCurrent.Click += new System.EventHandler(this.btListCurrent_Click);
            // 
            // btListDBData
            // 
            this.btListDBData.Location = new System.Drawing.Point(209, 10);
            this.btListDBData.Name = "btListDBData";
            this.btListDBData.Size = new System.Drawing.Size(75, 23);
            this.btListDBData.TabIndex = 2;
            this.btListDBData.Text = "列出所有";
            this.btListDBData.UseVisualStyleBackColor = true;
            this.btListDBData.Click += new System.EventHandler(this.btListDBData_Click);
            // 
            // tbContent
            // 
            this.tbContent.Location = new System.Drawing.Point(6, 41);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbContent.Size = new System.Drawing.Size(469, 321);
            this.tbContent.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "车站";
            // 
            // btTest
            // 
            this.btTest.Location = new System.Drawing.Point(290, 12);
            this.btTest.Name = "btTest";
            this.btTest.Size = new System.Drawing.Size(75, 23);
            this.btTest.TabIndex = 5;
            this.btTest.Text = "测试";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // btResult
            // 
            this.btResult.Location = new System.Drawing.Point(371, 12);
            this.btResult.Name = "btResult";
            this.btResult.Size = new System.Drawing.Size(42, 23);
            this.btResult.TabIndex = 6;
            this.btResult.Text = "结果";
            this.btResult.UseVisualStyleBackColor = true;
            this.btResult.Click += new System.EventHandler(this.btResult_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btTimerControl
            // 
            this.btTimerControl.Location = new System.Drawing.Point(419, 12);
            this.btTimerControl.Name = "btTimerControl";
            this.btTimerControl.Size = new System.Drawing.Size(42, 23);
            this.btTimerControl.TabIndex = 7;
            this.btTimerControl.Text = "结束";
            this.btTimerControl.UseVisualStyleBackColor = true;
            this.btTimerControl.Click += new System.EventHandler(this.btTimerControl_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 374);
            this.Controls.Add(this.btTimerControl);
            this.Controls.Add(this.btResult);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbContent);
            this.Controls.Add(this.btListDBData);
            this.Controls.Add(this.btListCurrent);
            this.Controls.Add(this.tbStation);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbStation;
        private System.Windows.Forms.Button btListCurrent;
        private System.Windows.Forms.Button btListDBData;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.Button btResult;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btTimerControl;
    }
}

