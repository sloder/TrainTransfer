namespace DetourHome
{
    partial class ListCheChi
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
            this.BtListALL = new System.Windows.Forms.Button();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.btCheChiBlock = new System.Windows.Forms.Button();
            this.tbContentBlock = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbMSG = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtListALL
            // 
            this.BtListALL.Location = new System.Drawing.Point(12, 88);
            this.BtListALL.Name = "BtListALL";
            this.BtListALL.Size = new System.Drawing.Size(90, 23);
            this.BtListALL.TabIndex = 0;
            this.BtListALL.Text = "列出所有车次信息";
            this.BtListALL.UseVisualStyleBackColor = true;
            this.BtListALL.Click += new System.EventHandler(this.BtListALL_Click);
            // 
            // tbContent
            // 
            this.tbContent.Location = new System.Drawing.Point(7, 108);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbContent.Size = new System.Drawing.Size(528, 142);
            this.tbContent.TabIndex = 1;
            // 
            // btCheChiBlock
            // 
            this.btCheChiBlock.Location = new System.Drawing.Point(7, 3);
            this.btCheChiBlock.Name = "btCheChiBlock";
            this.btCheChiBlock.Size = new System.Drawing.Size(90, 23);
            this.btCheChiBlock.TabIndex = 2;
            this.btCheChiBlock.Text = "列出车次模块";
            this.btCheChiBlock.UseVisualStyleBackColor = true;
            this.btCheChiBlock.Click += new System.EventHandler(this.btCheChiBlock_Click);
            // 
            // tbContentBlock
            // 
            this.tbContentBlock.Location = new System.Drawing.Point(7, 23);
            this.tbContentBlock.Multiline = true;
            this.tbContentBlock.Name = "tbContentBlock";
            this.tbContentBlock.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbContentBlock.Size = new System.Drawing.Size(528, 63);
            this.tbContentBlock.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(108, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "显示";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbMSG
            // 
            this.lbMSG.AutoSize = true;
            this.lbMSG.Location = new System.Drawing.Point(329, 88);
            this.lbMSG.Name = "lbMSG";
            this.lbMSG.Size = new System.Drawing.Size(23, 12);
            this.lbMSG.TabIndex = 5;
            this.lbMSG.Text = "MSG";
            // 
            // ListCheChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 358);
            this.Controls.Add(this.lbMSG);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbContentBlock);
            this.Controls.Add(this.btCheChiBlock);
            this.Controls.Add(this.tbContent);
            this.Controls.Add(this.BtListALL);
            this.Name = "ListCheChi";
            this.Text = "ListCheChi";
            this.Load += new System.EventHandler(this.ListCheChi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtListALL;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Button btCheChiBlock;
        private System.Windows.Forms.TextBox tbContentBlock;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbMSG;

    }
}