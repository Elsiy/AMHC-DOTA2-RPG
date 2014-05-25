namespace AMHC_SERVER_CLIENT
{
    partial class ClientForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.debug_dialog = new System.Windows.Forms.ListBox();
            this.amhc_btn_exit = new System.Windows.Forms.Button();
            this.addonList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.amhc_btn_set_addon = new System.Windows.Forms.Button();
            this.installList = new System.Windows.Forms.ListBox();
            this.btn_download = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(170, 280);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(144, 15);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // debug_dialog
            // 
            this.debug_dialog.FormattingEnabled = true;
            this.debug_dialog.ItemHeight = 12;
            this.debug_dialog.Items.AddRange(new object[] {
            "88:88:88 ：此处显示DEBUG信息。"});
            this.debug_dialog.Location = new System.Drawing.Point(325, 43);
            this.debug_dialog.Name = "debug_dialog";
            this.debug_dialog.Size = new System.Drawing.Size(247, 280);
            this.debug_dialog.TabIndex = 3;
            // 
            // amhc_btn_exit
            // 
            this.amhc_btn_exit.BackColor = System.Drawing.SystemColors.HotTrack;
            this.amhc_btn_exit.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.amhc_btn_exit.Location = new System.Drawing.Point(325, 385);
            this.amhc_btn_exit.Name = "amhc_btn_exit";
            this.amhc_btn_exit.Size = new System.Drawing.Size(247, 45);
            this.amhc_btn_exit.TabIndex = 4;
            this.amhc_btn_exit.Text = "退出";
            this.amhc_btn_exit.UseVisualStyleBackColor = false;
            this.amhc_btn_exit.Click += new System.EventHandler(this.amhc_btn_exit_Click);
            // 
            // addonList
            // 
            this.addonList.Font = new System.Drawing.Font("宋体", 11F);
            this.addonList.FormattingEnabled = true;
            this.addonList.ItemHeight = 15;
            this.addonList.Location = new System.Drawing.Point(12, 102);
            this.addonList.Name = "addonList";
            this.addonList.Size = new System.Drawing.Size(144, 184);
            this.addonList.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(10, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "已安装的RPG：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(13, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "当前活动RPG：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(301, 21);
            this.textBox1.TabIndex = 9;
            // 
            // amhc_btn_set_addon
            // 
            this.amhc_btn_set_addon.Location = new System.Drawing.Point(13, 292);
            this.amhc_btn_set_addon.Name = "amhc_btn_set_addon";
            this.amhc_btn_set_addon.Size = new System.Drawing.Size(88, 33);
            this.amhc_btn_set_addon.TabIndex = 10;
            this.amhc_btn_set_addon.Text = "设为活动RPG";
            this.amhc_btn_set_addon.UseVisualStyleBackColor = true;
            this.amhc_btn_set_addon.Click += new System.EventHandler(this.amhc_btn_set_addon_Click);
            // 
            // installList
            // 
            this.installList.Font = new System.Drawing.Font("宋体", 11F);
            this.installList.FormattingEnabled = true;
            this.installList.ItemHeight = 15;
            this.installList.Items.AddRange(new object[] {
            "Frota",
            "Reflex",
            "TrollsAndElves"});
            this.installList.Location = new System.Drawing.Point(170, 102);
            this.installList.Name = "installList";
            this.installList.Size = new System.Drawing.Size(144, 184);
            this.installList.TabIndex = 12;
            // 
            // btn_download
            // 
            this.btn_download.Location = new System.Drawing.Point(170, 292);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(144, 33);
            this.btn_download.TabIndex = 13;
            this.btn_download.Text = "下载RPG";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(167, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "可下载RPG：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(443, 331);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 37);
            this.button2.TabIndex = 15;
            this.button2.Text = "清空DEBUG信息";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 440);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(509, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "www.dota2rpg.com  play.dota2rpg.com  联系我:XavierCHN@vip.qq.com Xavier_CHN@live.com" +
    "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(107, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 33);
            this.button1.TabIndex = 17;
            this.button1.Text = "删除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(325, 331);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 37);
            this.button3.TabIndex = 18;
            this.button3.Text = "设置AUTOEXEC";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = global::AMHC_SERVER_CLIENT.Properties.Resources.Bitmap1;
            this.button4.Location = new System.Drawing.Point(12, 331);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(300, 100);
            this.button4.TabIndex = 19;
            this.button4.Text = " 运行DOTA2";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.ControlBox = false;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_download);
            this.Controls.Add(this.installList);
            this.Controls.Add(this.amhc_btn_set_addon);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addonList);
            this.Controls.Add(this.amhc_btn_exit);
            this.Controls.Add(this.debug_dialog);
            this.MaximumSize = new System.Drawing.Size(600, 500);
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "ClientForm";
            this.Text = "AMHC-DOTA2RPG安装工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox debug_dialog;
        private System.Windows.Forms.Button amhc_btn_exit;
        private System.Windows.Forms.ListBox addonList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button amhc_btn_set_addon;
        private System.Windows.Forms.ListBox installList;
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

