namespace StuMgmServer
{
    partial class Server
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
            this.tlpAll = new System.Windows.Forms.TableLayoutPanel();
            this.rtxHistory = new System.Windows.Forms.RichTextBox();
            this.tlpSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lblSwiSta = new System.Windows.Forms.Label();
            this.btnSerSwitch = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.cbxIPAddr = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新IPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.tlpAll.SuspendLayout();
            this.tlpSettings.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAll
            // 
            this.tlpAll.ColumnCount = 2;
            this.tlpAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.25581F));
            this.tlpAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.74419F));
            this.tlpAll.Controls.Add(this.rtxHistory, 0, 0);
            this.tlpAll.Controls.Add(this.tlpSettings, 0, 0);
            this.tlpAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAll.Location = new System.Drawing.Point(0, 28);
            this.tlpAll.Name = "tlpAll";
            this.tlpAll.RowCount = 1;
            this.tlpAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 409F));
            this.tlpAll.Size = new System.Drawing.Size(1075, 409);
            this.tlpAll.TabIndex = 0;
            // 
            // rtxHistory
            // 
            this.rtxHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxHistory.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.rtxHistory.Location = new System.Drawing.Point(295, 3);
            this.rtxHistory.Name = "rtxHistory";
            this.rtxHistory.Size = new System.Drawing.Size(777, 403);
            this.rtxHistory.TabIndex = 2;
            this.rtxHistory.Text = "";
            // 
            // tlpSettings
            // 
            this.tlpSettings.ColumnCount = 2;
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.8042F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.1958F));
            this.tlpSettings.Controls.Add(this.lblSwiSta, 0, 2);
            this.tlpSettings.Controls.Add(this.btnSerSwitch, 1, 2);
            this.tlpSettings.Controls.Add(this.lblPort, 0, 1);
            this.tlpSettings.Controls.Add(this.txtPort, 1, 1);
            this.tlpSettings.Controls.Add(this.lblIP, 0, 0);
            this.tlpSettings.Controls.Add(this.cbxIPAddr, 1, 0);
            this.tlpSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpSettings.Location = new System.Drawing.Point(3, 3);
            this.tlpSettings.Name = "tlpSettings";
            this.tlpSettings.RowCount = 3;
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSettings.Size = new System.Drawing.Size(286, 178);
            this.tlpSettings.TabIndex = 0;
            // 
            // lblSwiSta
            // 
            this.lblSwiSta.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSwiSta.AutoSize = true;
            this.lblSwiSta.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.lblSwiSta.Location = new System.Drawing.Point(3, 136);
            this.lblSwiSta.Name = "lblSwiSta";
            this.lblSwiSta.Size = new System.Drawing.Size(110, 23);
            this.lblSwiSta.TabIndex = 1;
            this.lblSwiSta.Text = "服务器状态";
            // 
            // btnSerSwitch
            // 
            this.btnSerSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSerSwitch.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.btnSerSwitch.Location = new System.Drawing.Point(134, 121);
            this.btnSerSwitch.Name = "btnSerSwitch";
            this.btnSerSwitch.Size = new System.Drawing.Size(149, 54);
            this.btnSerSwitch.TabIndex = 2;
            this.btnSerSwitch.Text = "开启服务器";
            this.btnSerSwitch.UseVisualStyleBackColor = true;
            this.btnSerSwitch.Click += new System.EventHandler(this.btnSerSwitch_Click);
            // 
            // lblPort
            // 
            this.lblPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.lblPort.Location = new System.Drawing.Point(3, 77);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(90, 23);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "本地端口";
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.txtPort.Location = new System.Drawing.Point(134, 74);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(149, 29);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "502";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIP
            // 
            this.lblIP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.lblIP.Location = new System.Drawing.Point(3, 18);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(66, 23);
            this.lblIP.TabIndex = 4;
            this.lblIP.Text = "本机IP";
            // 
            // cbxIPAddr
            // 
            this.cbxIPAddr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxIPAddr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIPAddr.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.cbxIPAddr.FormattingEnabled = true;
            this.cbxIPAddr.Location = new System.Drawing.Point(134, 14);
            this.cbxIPAddr.Name = "cbxIPAddr";
            this.cbxIPAddr.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbxIPAddr.Size = new System.Drawing.Size(149, 30);
            this.cbxIPAddr.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1075, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新IPToolStripMenuItem});
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // 刷新IPToolStripMenuItem
            // 
            this.刷新IPToolStripMenuItem.Name = "刷新IPToolStripMenuItem";
            this.刷新IPToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.刷新IPToolStripMenuItem.Text = "刷新IP";
            this.刷新IPToolStripMenuItem.Click += new System.EventHandler(this.刷新IPToolStripMenuItem_Click);
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 437);
            this.Controls.Add(this.tlpAll);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Server";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.Load += new System.EventHandler(this.Server_Load);
            this.tlpAll.ResumeLayout(false);
            this.tlpSettings.ResumeLayout(false);
            this.tlpSettings.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAll;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label lblSwiSta;
        private System.Windows.Forms.TableLayoutPanel tlpSettings;
        private System.Windows.Forms.Button btnSerSwitch;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Timer tmr;
        private System.Windows.Forms.RichTextBox rtxHistory;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.ComboBox cbxIPAddr;
        private System.Windows.Forms.ToolStripMenuItem 菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新IPToolStripMenuItem;
    }
}

