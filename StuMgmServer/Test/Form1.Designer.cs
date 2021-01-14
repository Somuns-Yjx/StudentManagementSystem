namespace Test
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
            this.tlpAll = new System.Windows.Forms.TableLayoutPanel();
            this.rtxHistory = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblSwiSta = new System.Windows.Forms.Label();
            this.btnSerSwitch = new System.Windows.Forms.Button();
            this.lblPort = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tmr = new System.Windows.Forms.Timer(this.components);
            this.tlpAll.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpAll
            // 
            this.tlpAll.ColumnCount = 2;
            this.tlpAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.25581F));
            this.tlpAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.74419F));
            this.tlpAll.Controls.Add(this.rtxHistory, 0, 0);
            this.tlpAll.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlpAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpAll.Location = new System.Drawing.Point(0, 24);
            this.tlpAll.Name = "tlpAll";
            this.tlpAll.RowCount = 1;
            this.tlpAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 425F));
            this.tlpAll.Size = new System.Drawing.Size(965, 425);
            this.tlpAll.TabIndex = 2;
            // 
            // rtxHistory
            // 
            this.rtxHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxHistory.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.rtxHistory.Location = new System.Drawing.Point(266, 3);
            this.rtxHistory.Name = "rtxHistory";
            this.rtxHistory.Size = new System.Drawing.Size(696, 419);
            this.rtxHistory.TabIndex = 2;
            this.rtxHistory.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.8042F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.1958F));
            this.tableLayoutPanel1.Controls.Add(this.txtPort, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSwiSta, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSerSwitch, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPort, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(257, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.txtPort.Location = new System.Drawing.Point(120, 10);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(134, 29);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "502";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSwiSta
            // 
            this.lblSwiSta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSwiSta.AutoSize = true;
            this.lblSwiSta.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.lblSwiSta.Location = new System.Drawing.Point(3, 63);
            this.lblSwiSta.Name = "lblSwiSta";
            this.lblSwiSta.Size = new System.Drawing.Size(110, 23);
            this.lblSwiSta.TabIndex = 1;
            this.lblSwiSta.Text = "服务器状态";
            // 
            // btnSerSwitch
            // 
            this.btnSerSwitch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSerSwitch.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.btnSerSwitch.Location = new System.Drawing.Point(120, 53);
            this.btnSerSwitch.Name = "btnSerSwitch";
            this.btnSerSwitch.Size = new System.Drawing.Size(134, 44);
            this.btnSerSwitch.TabIndex = 2;
            this.btnSerSwitch.Text = "开启服务器";
            this.btnSerSwitch.UseVisualStyleBackColor = true;
            this.btnSerSwitch.Click += new System.EventHandler(this.btnSerSwitch_Click);
            // 
            // lblPort
            // 
            this.lblPort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Calibri", 10.8F);
            this.lblPort.Location = new System.Drawing.Point(13, 13);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(90, 23);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "本地端口";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(965, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tmr
            // 
            this.tmr.Enabled = true;
            this.tmr.Interval = 500;
            this.tmr.Tick += new System.EventHandler(this.tmr_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 449);
            this.Controls.Add(this.tlpAll);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tlpAll.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpAll;
        private System.Windows.Forms.RichTextBox rtxHistory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblSwiSta;
        private System.Windows.Forms.Button btnSerSwitch;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Timer tmr;
    }
}

