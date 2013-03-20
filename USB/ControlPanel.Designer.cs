namespace com.AComm
{
    partial class ControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanel));
            this.buttonFindDevices = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.textBoxModuleName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonReadSetting = new System.Windows.Forms.Button();
            this.comboBoxSettings = new System.Windows.Forms.ComboBox();
            this.comboBoxSettingsSet = new System.Windows.Forms.ComboBox();
            this.buttonSetSetting = new System.Windows.Forms.Button();
            this.textBoxSettingsSetValue = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonFindDevices
            // 
            this.buttonFindDevices.Location = new System.Drawing.Point(49, 12);
            this.buttonFindDevices.Name = "buttonFindDevices";
            this.buttonFindDevices.Size = new System.Drawing.Size(75, 23);
            this.buttonFindDevices.TabIndex = 0;
            this.buttonFindDevices.Text = "Find Devices";
            this.buttonFindDevices.Click += new System.EventHandler(this.buttonFindDevices_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(0, 55);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(597, 212);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(186, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(46, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(140, 12);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(40, 23);
            this.buttonOpen.TabIndex = 3;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // textBoxModuleName
            // 
            this.textBoxModuleName.Location = new System.Drawing.Point(238, 14);
            this.textBoxModuleName.Name = "textBoxModuleName";
            this.textBoxModuleName.Size = new System.Drawing.Size(100, 20);
            this.textBoxModuleName.TabIndex = 4;
            this.textBoxModuleName.Text = "QUSB-0\\0\\0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(360, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "Device Description";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonReadSetting
            // 
            this.buttonReadSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReadSetting.Location = new System.Drawing.Point(40, 284);
            this.buttonReadSetting.Name = "buttonReadSetting";
            this.buttonReadSetting.Size = new System.Drawing.Size(84, 23);
            this.buttonReadSetting.TabIndex = 6;
            this.buttonReadSetting.Text = "Read Setting";
            this.buttonReadSetting.UseVisualStyleBackColor = true;
            this.buttonReadSetting.Click += new System.EventHandler(this.buttonReadSetting_Click);
            // 
            // comboBoxSettings
            // 
            this.comboBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxSettings.FormattingEnabled = true;
            this.comboBoxSettings.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBoxSettings.Location = new System.Drawing.Point(130, 286);
            this.comboBoxSettings.Name = "comboBoxSettings";
            this.comboBoxSettings.Size = new System.Drawing.Size(50, 21);
            this.comboBoxSettings.TabIndex = 7;
            // 
            // comboBoxSettingsSet
            // 
            this.comboBoxSettingsSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxSettingsSet.FormattingEnabled = true;
            this.comboBoxSettingsSet.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBoxSettingsSet.Location = new System.Drawing.Point(130, 315);
            this.comboBoxSettingsSet.Name = "comboBoxSettingsSet";
            this.comboBoxSettingsSet.Size = new System.Drawing.Size(50, 21);
            this.comboBoxSettingsSet.TabIndex = 9;
            // 
            // buttonSetSetting
            // 
            this.buttonSetSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSetSetting.Location = new System.Drawing.Point(40, 313);
            this.buttonSetSetting.Name = "buttonSetSetting";
            this.buttonSetSetting.Size = new System.Drawing.Size(84, 23);
            this.buttonSetSetting.TabIndex = 8;
            this.buttonSetSetting.Text = "Set Setting";
            this.buttonSetSetting.UseVisualStyleBackColor = true;
            this.buttonSetSetting.Click += new System.EventHandler(this.buttonSetSetting_Click);
            // 
            // textBoxSettingsSetValue
            // 
            this.textBoxSettingsSetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSettingsSetValue.Location = new System.Drawing.Point(186, 316);
            this.textBoxSettingsSetValue.Name = "textBoxSettingsSetValue";
            this.textBoxSettingsSetValue.Size = new System.Drawing.Size(53, 20);
            this.textBoxSettingsSetValue.TabIndex = 10;
            this.textBoxSettingsSetValue.Text = "250";
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Image = ((System.Drawing.Image)(resources.GetObject("buttonClear.Image")));
            this.buttonClear.Location = new System.Drawing.Point(568, 268);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(29, 27);
            this.buttonClear.TabIndex = 11;
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 366);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.textBoxSettingsSetValue);
            this.Controls.Add(this.comboBoxSettingsSet);
            this.Controls.Add(this.buttonSetSetting);
            this.Controls.Add(this.comboBoxSettings);
            this.Controls.Add(this.buttonReadSetting);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxModuleName);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonFindDevices);
            this.Name = "ControlPanel";
            this.Text = "ControlPanel";
            this.Load += new System.EventHandler(this.ControlPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFindDevices;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.TextBox textBoxModuleName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonReadSetting;
        private System.Windows.Forms.ComboBox comboBoxSettings;
        private System.Windows.Forms.ComboBox comboBoxSettingsSet;
        private System.Windows.Forms.Button buttonSetSetting;
        private System.Windows.Forms.TextBox textBoxSettingsSetValue;
        private System.Windows.Forms.Button buttonClear;
    }
}