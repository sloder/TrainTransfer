namespace DetourHome
{
    partial class ListCheDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btGet = new System.Windows.Forms.Button();
            this.tbCheCi = new System.Windows.Forms.TextBox();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.btTimeX = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btGet
            // 
            this.btGet.Location = new System.Drawing.Point(118, 1);
            this.btGet.Name = "btGet";
            this.btGet.Size = new System.Drawing.Size(75, 23);
            this.btGet.TabIndex = 0;
            this.btGet.Text = "显示详情";
            this.btGet.UseVisualStyleBackColor = true;
            this.btGet.Click += new System.EventHandler(this.btGet_Click);
            // 
            // tbCheCi
            // 
            this.tbCheCi.Location = new System.Drawing.Point(12, 3);
            this.tbCheCi.Name = "tbCheCi";
            this.tbCheCi.Size = new System.Drawing.Size(100, 21);
            this.tbCheCi.TabIndex = 1;
            // 
            // tbContent
            // 
            this.tbContent.Location = new System.Drawing.Point(199, 1);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbContent.Size = new System.Drawing.Size(257, 249);
            this.tbContent.TabIndex = 2;
            // 
            // btTimeX
            // 
            this.btTimeX.Location = new System.Drawing.Point(118, 30);
            this.btTimeX.Name = "btTimeX";
            this.btTimeX.Size = new System.Drawing.Size(75, 23);
            this.btTimeX.TabIndex = 3;
            this.btTimeX.Text = "启动";
            this.btTimeX.UseVisualStyleBackColor = true;
            this.btTimeX.Click += new System.EventHandler(this.btTimeX_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ListCheDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 262);
            this.Controls.Add(this.btTimeX);
            this.Controls.Add(this.tbContent);
            this.Controls.Add(this.tbCheCi);
            this.Controls.Add(this.btGet);
            this.Name = "ListCheDetail";
            this.Text = "ListCheDetail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btGet;
        private System.Windows.Forms.TextBox tbCheCi;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Button btTimeX;
        private System.Windows.Forms.Timer timer1;
    }
}