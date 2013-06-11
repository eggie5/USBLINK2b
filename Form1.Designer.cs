namespace com.AComm
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.usbStatusStrip = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItemConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.uSBControlPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusPanelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPanelUSBStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkBoxMatlabEnabled = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Graph = new ZedGraph.ZedGraphControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonOpenCPanel = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.zedGraphControlFFTs = new ZedGraph.ZedGraphControl();
            this.timerContFeed = new System.Windows.Forms.Timer(this.components);
            this.timerStatusMessage = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.channel1_info_label = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.channel3_info_label = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.channel2_info_label = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.channel4_info_label = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usbStatusStrip,
            this.statusPanelInfo,
            this.statusPanelUSBStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 751);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1028, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // usbStatusStrip
            // 
            this.usbStatusStrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemConnect,
            this.toolStripMenuItemDisconnect,
            this.uSBControlPanelToolStripMenuItem});
            this.usbStatusStrip.Image = global::com.AComm.Properties.Resources.try7;
            this.usbStatusStrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.usbStatusStrip.Name = "usbStatusStrip";
            this.usbStatusStrip.Size = new System.Drawing.Size(108, 20);
            this.usbStatusStrip.Text = "USB Status:";
            // 
            // toolStripMenuItemConnect
            // 
            this.toolStripMenuItemConnect.Image = global::com.AComm.Properties.Resources.connect3;
            this.toolStripMenuItemConnect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemConnect.Name = "toolStripMenuItemConnect";
            this.toolStripMenuItemConnect.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemConnect.Text = "Connect";
            this.toolStripMenuItemConnect.Click += new System.EventHandler(this.toolStripMenuItemConnect_Click);
            // 
            // toolStripMenuItemDisconnect
            // 
            this.toolStripMenuItemDisconnect.Image = global::com.AComm.Properties.Resources.try7;
            this.toolStripMenuItemDisconnect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItemDisconnect.Name = "toolStripMenuItemDisconnect";
            this.toolStripMenuItemDisconnect.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemDisconnect.Text = "Disconect";
            this.toolStripMenuItemDisconnect.Click += new System.EventHandler(this.toolStripMenuItemDisconnect_Click);
            // 
            // uSBControlPanelToolStripMenuItem
            // 
            this.uSBControlPanelToolStripMenuItem.Name = "uSBControlPanelToolStripMenuItem";
            this.uSBControlPanelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uSBControlPanelToolStripMenuItem.Text = "USB Control Panel";
            this.uSBControlPanelToolStripMenuItem.Click += new System.EventHandler(this.uSBControlPanelToolStripMenuItem_Click);
            // 
            // statusPanelInfo
            // 
            this.statusPanelInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusPanelInfo.ForeColor = System.Drawing.Color.Black;
            this.statusPanelInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusPanelInfo.Name = "statusPanelInfo";
            this.statusPanelInfo.Size = new System.Drawing.Size(77, 17);
            this.statusPanelInfo.Text = "[statusPanel1]";
            // 
            // statusPanelUSBStatus
            // 
            this.statusPanelUSBStatus.Name = "statusPanelUSBStatus";
            this.statusPanelUSBStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.checkBoxMatlabEnabled);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 100);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-1, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(421, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // checkBoxMatlabEnabled
            // 
            this.checkBoxMatlabEnabled.AutoSize = true;
            this.checkBoxMatlabEnabled.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxMatlabEnabled.Image")));
            this.checkBoxMatlabEnabled.Location = new System.Drawing.Point(966, 35);
            this.checkBoxMatlabEnabled.Name = "checkBoxMatlabEnabled";
            this.checkBoxMatlabEnabled.Size = new System.Drawing.Size(45, 22);
            this.checkBoxMatlabEnabled.TabIndex = 31;
            this.checkBoxMatlabEnabled.UseVisualStyleBackColor = true;
            this.checkBoxMatlabEnabled.CheckedChanged += new System.EventHandler(this.checkBoxMatlabEnabled_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(176, 106);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(618, 615);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Graph);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(610, 589);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "USB Output";
            // 
            // Graph
            // 
            this.Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Graph.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Graph.IsEnableHPan = true;
            this.Graph.IsEnableVPan = true;
            this.Graph.IsEnableZoom = true;
            this.Graph.IsScrollY2 = false;
            this.Graph.IsShowContextMenu = true;
            this.Graph.IsShowHScrollBar = false;
            this.Graph.IsShowPointValues = false;
            this.Graph.IsShowVScrollBar = false;
            this.Graph.IsZoomOnMouseCenter = false;
            this.Graph.Location = new System.Drawing.Point(3, 3);
            this.Graph.Name = "Graph";
            this.Graph.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.Graph.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.Graph.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.Graph.PointDateFormat = "g";
            this.Graph.PointValueFormat = "G";
            this.Graph.ScrollMaxX = 0D;
            this.Graph.ScrollMaxY = 0D;
            this.Graph.ScrollMaxY2 = 0D;
            this.Graph.ScrollMinX = 0D;
            this.Graph.ScrollMinY = 0D;
            this.Graph.ScrollMinY2 = 0D;
            this.Graph.Size = new System.Drawing.Size(604, 583);
            this.Graph.TabIndex = 0;
            this.Graph.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.Graph.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.Graph.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.Graph.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.Graph.ZoomStepFraction = 0.1D;
            this.Graph.Load += new System.EventHandler(this.Graph_Load);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonOpenCPanel);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(610, 589);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Controls";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonOpenCPanel
            // 
            this.buttonOpenCPanel.Location = new System.Drawing.Point(19, 6);
            this.buttonOpenCPanel.Name = "buttonOpenCPanel";
            this.buttonOpenCPanel.Size = new System.Drawing.Size(196, 79);
            this.buttonOpenCPanel.TabIndex = 0;
            this.buttonOpenCPanel.Text = "Open Control Panel";
            this.buttonOpenCPanel.UseVisualStyleBackColor = true;
            this.buttonOpenCPanel.Click += new System.EventHandler(this.buttonOpenCPanel_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.zedGraphControlFFTs);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(610, 589);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "FFT";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // zedGraphControlFFTs
            // 
            this.zedGraphControlFFTs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControlFFTs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zedGraphControlFFTs.IsEnableHPan = true;
            this.zedGraphControlFFTs.IsEnableVPan = true;
            this.zedGraphControlFFTs.IsEnableZoom = true;
            this.zedGraphControlFFTs.IsScrollY2 = false;
            this.zedGraphControlFFTs.IsShowContextMenu = true;
            this.zedGraphControlFFTs.IsShowHScrollBar = false;
            this.zedGraphControlFFTs.IsShowPointValues = false;
            this.zedGraphControlFFTs.IsShowVScrollBar = false;
            this.zedGraphControlFFTs.IsZoomOnMouseCenter = false;
            this.zedGraphControlFFTs.Location = new System.Drawing.Point(3, 3);
            this.zedGraphControlFFTs.Name = "zedGraphControlFFTs";
            this.zedGraphControlFFTs.PanButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraphControlFFTs.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
            this.zedGraphControlFFTs.PanModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zedGraphControlFFTs.PointDateFormat = "g";
            this.zedGraphControlFFTs.PointValueFormat = "G";
            this.zedGraphControlFFTs.ScrollMaxX = 0D;
            this.zedGraphControlFFTs.ScrollMaxY = 0D;
            this.zedGraphControlFFTs.ScrollMaxY2 = 0D;
            this.zedGraphControlFFTs.ScrollMinX = 0D;
            this.zedGraphControlFFTs.ScrollMinY = 0D;
            this.zedGraphControlFFTs.ScrollMinY2 = 0D;
            this.zedGraphControlFFTs.Size = new System.Drawing.Size(604, 583);
            this.zedGraphControlFFTs.TabIndex = 1;
            this.zedGraphControlFFTs.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraphControlFFTs.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
            this.zedGraphControlFFTs.ZoomModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraphControlFFTs.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
            this.zedGraphControlFFTs.ZoomStepFraction = 0.1D;
            // 
            // timerContFeed
            // 
            this.timerContFeed.Interval = 250;
            this.timerContFeed.Tick += new System.EventHandler(this.timerTick);
            // 
            // timerStatusMessage
            // 
            this.timerStatusMessage.Enabled = true;
            this.timerStatusMessage.Interval = 3000;
            this.timerStatusMessage.Tick += new System.EventHandler(this.timerStatusMessage_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.channel1_info_label);
            this.groupBox2.Location = new System.Drawing.Point(1, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 249);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Channel 1";
            // 
            // channel1_info_label
            // 
            this.channel1_info_label.AutoSize = true;
            this.channel1_info_label.Location = new System.Drawing.Point(11, 26);
            this.channel1_info_label.Name = "channel1_info_label";
            this.channel1_info_label.Size = new System.Drawing.Size(79, 13);
            this.channel1_info_label.TabIndex = 0;
            this.channel1_info_label.Text = "[SIGNAL INFO]";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.channel3_info_label);
            this.groupBox3.Location = new System.Drawing.Point(1, 462);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(169, 259);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Channel 3";
            // 
            // channel3_info_label
            // 
            this.channel3_info_label.AutoSize = true;
            this.channel3_info_label.Location = new System.Drawing.Point(11, 28);
            this.channel3_info_label.Name = "channel3_info_label";
            this.channel3_info_label.Size = new System.Drawing.Size(79, 13);
            this.channel3_info_label.TabIndex = 1;
            this.channel3_info_label.Text = "[SIGNAL INFO]";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.channel2_info_label);
            this.groupBox4.Location = new System.Drawing.Point(810, 154);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(169, 249);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Channel 2";
            // 
            // channel2_info_label
            // 
            this.channel2_info_label.AutoSize = true;
            this.channel2_info_label.Location = new System.Drawing.Point(11, 26);
            this.channel2_info_label.Name = "channel2_info_label";
            this.channel2_info_label.Size = new System.Drawing.Size(79, 13);
            this.channel2_info_label.TabIndex = 0;
            this.channel2_info_label.Text = "[SIGNAL INFO]";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.BackColor = System.Drawing.Color.White;
            this.groupBox5.Controls.Add(this.channel4_info_label);
            this.groupBox5.Location = new System.Drawing.Point(824, 462);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(169, 249);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Channel 4";
            // 
            // channel4_info_label
            // 
            this.channel4_info_label.AutoSize = true;
            this.channel4_info_label.Location = new System.Drawing.Point(11, 26);
            this.channel4_info_label.Name = "channel4_info_label";
            this.channel4_info_label.Size = new System.Drawing.Size(79, 13);
            this.channel4_info_label.TabIndex = 0;
            this.channel4_info_label.Text = "[SIGNAL INFO]";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1028, 773);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(850, 38);
            this.Name = "Form1";
            this.Text = "USBLiNK3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ZedGraph.ZedGraphControl Graph;
        internal System.Windows.Forms.ToolStripStatusLabel statusPanelInfo;
        internal System.Windows.Forms.Timer timerContFeed;
        private System.Windows.Forms.ToolStripSplitButton usbStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConnect;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisconnect;
        private System.Windows.Forms.ToolStripMenuItem uSBControlPanelToolStripMenuItem;
        private System.Windows.Forms.Timer timerStatusMessage;
        private System.Windows.Forms.CheckBox checkBoxMatlabEnabled;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label channel1_info_label;
        private System.Windows.Forms.Label channel3_info_label;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label channel2_info_label;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label channel4_info_label;
        private System.Windows.Forms.TabPage tabPage4;
        private ZedGraph.ZedGraphControl zedGraphControlFFTs;
        private System.Windows.Forms.Button buttonOpenCPanel;
        internal System.Windows.Forms.ToolStripStatusLabel statusPanelUSBStatus;
    }
}

