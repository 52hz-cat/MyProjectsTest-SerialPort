namespace MyProjectsTest
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
            components = new System.ComponentModel.Container();
            btnOpen = new Button();
            comboBoxPorts = new ComboBox();
            textBox1 = new TextBox();
            btnSend = new Button();
            textBox2 = new TextBox();
            labelStatus = new Label();
            comboBoxEncoding = new ComboBox();
            chkHexDisplay = new CheckBox();
            chkHexSend = new CheckBox();
            btnRefresh = new Button();
            btnClear = new Button();
            chkEnableConfig = new CheckBox();
            comboBoxRecvCheck = new ComboBox();
            comboBoxSendCheck = new ComboBox();
            groupBoxScale = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            chkRawIsAdc = new CheckBox();
            txtEngMax = new TextBox();
            txtEngMin = new TextBox();
            txtRawMax = new TextBox();
            txtRawMin = new TextBox();
            comboBoxSignalType = new ComboBox();
            chkScale = new CheckBox();
            toolTip1 = new ToolTip(components);
            groupBoxScale.SuspendLayout();
            SuspendLayout();
            // 
            // btnOpen
            // 
            btnOpen.Anchor = AnchorStyles.None;
            btnOpen.Location = new Point(1085, 64);
            btnOpen.Margin = new Padding(1);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(94, 29);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "打开串口";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // comboBoxPorts
            // 
            comboBoxPorts.Anchor = AnchorStyles.None;
            comboBoxPorts.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPorts.FormattingEnabled = true;
            comboBoxPorts.Items.AddRange(new object[] { "COM1", "COM2", "COM3" });
            comboBoxPorts.Location = new Point(1068, 31);
            comboBoxPorts.Margin = new Padding(1);
            comboBoxPorts.Name = "comboBoxPorts";
            comboBoxPorts.Size = new Size(111, 27);
            comboBoxPorts.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(10, 3);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(762, 443);
            textBox1.TabIndex = 2;
            textBox1.WordWrap = false;
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.None;
            btnSend.Location = new Point(776, 450);
            btnSend.Margin = new Padding(1);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(94, 29);
            btnSend.TabIndex = 3;
            btnSend.Text = "发送";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += BtnSend_Click;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.ForeColor = SystemColors.WindowText;
            textBox2.Location = new Point(10, 452);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new Size(762, 289);
            textBox2.TabIndex = 4;
            // 
            // labelStatus
            // 
            labelStatus.Anchor = AnchorStyles.None;
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(1101, 9);
            labelStatus.Margin = new Padding(1);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(69, 19);
            labelStatus.TabIndex = 5;
            labelStatus.Text = "串口状态";
            // 
            // comboBoxEncoding
            // 
            comboBoxEncoding.Anchor = AnchorStyles.None;
            comboBoxEncoding.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEncoding.FormattingEnabled = true;
            comboBoxEncoding.Items.AddRange(new object[] { "GBK", "UTF-8", "ASCII" });
            comboBoxEncoding.Location = new Point(776, 66);
            comboBoxEncoding.Margin = new Padding(1);
            comboBoxEncoding.Name = "comboBoxEncoding";
            comboBoxEncoding.Size = new Size(123, 27);
            comboBoxEncoding.TabIndex = 6;
            // 
            // chkHexDisplay
            // 
            chkHexDisplay.Anchor = AnchorStyles.None;
            chkHexDisplay.AutoSize = true;
            chkHexDisplay.Location = new Point(776, 41);
            chkHexDisplay.Margin = new Padding(1);
            chkHexDisplay.Name = "chkHexDisplay";
            chkHexDisplay.Size = new Size(109, 23);
            chkHexDisplay.TabIndex = 7;
            chkHexDisplay.Text = "16进制显示";
            chkHexDisplay.UseVisualStyleBackColor = true;
            // 
            // chkHexSend
            // 
            chkHexSend.Anchor = AnchorStyles.None;
            chkHexSend.AutoSize = true;
            chkHexSend.Location = new Point(776, 483);
            chkHexSend.Margin = new Padding(1);
            chkHexSend.Name = "chkHexSend";
            chkHexSend.Size = new Size(109, 23);
            chkHexSend.TabIndex = 8;
            chkHexSend.Text = "16进制发送";
            chkHexSend.UseVisualStyleBackColor = true;
            chkHexSend.CheckedChanged += chkHexSend_CheckedChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.None;
            btnRefresh.Location = new Point(968, 31);
            btnRefresh.Margin = new Padding(1);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 29);
            btnRefresh.TabIndex = 9;
            btnRefresh.Text = "刷新";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.None;
            btnClear.Location = new Point(776, 10);
            btnClear.Margin = new Padding(1);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 29);
            btnClear.TabIndex = 10;
            btnClear.Text = "清空接收";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // chkEnableConfig
            // 
            chkEnableConfig.AutoSize = true;
            chkEnableConfig.Location = new Point(1068, 732);
            chkEnableConfig.Margin = new Padding(2);
            chkEnableConfig.Name = "chkEnableConfig";
            chkEnableConfig.Size = new Size(121, 23);
            chkEnableConfig.TabIndex = 11;
            chkEnableConfig.Text = "启用配置文件";
            chkEnableConfig.UseVisualStyleBackColor = true;
            // 
            // comboBoxRecvCheck
            // 
            comboBoxRecvCheck.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRecvCheck.FormattingEnabled = true;
            comboBoxRecvCheck.Items.AddRange(new object[] { "None", "CRC16", "Sum", "Xor", "CRC8" });
            comboBoxRecvCheck.Location = new Point(776, 508);
            comboBoxRecvCheck.Margin = new Padding(1);
            comboBoxRecvCheck.Name = "comboBoxRecvCheck";
            comboBoxRecvCheck.Size = new Size(123, 27);
            comboBoxRecvCheck.TabIndex = 12;
            // 
            // comboBoxSendCheck
            // 
            comboBoxSendCheck.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSendCheck.FormattingEnabled = true;
            comboBoxSendCheck.Items.AddRange(new object[] { "None", "CRC16", "Sum", "Xor", "CRC8" });
            comboBoxSendCheck.Location = new Point(776, 117);
            comboBoxSendCheck.Margin = new Padding(1);
            comboBoxSendCheck.Name = "comboBoxSendCheck";
            comboBoxSendCheck.Size = new Size(123, 27);
            comboBoxSendCheck.TabIndex = 13;
            // 
            // groupBoxScale
            // 
            groupBoxScale.Controls.Add(label2);
            groupBoxScale.Controls.Add(label1);
            groupBoxScale.Controls.Add(chkRawIsAdc);
            groupBoxScale.Controls.Add(txtEngMax);
            groupBoxScale.Controls.Add(txtEngMin);
            groupBoxScale.Controls.Add(txtRawMax);
            groupBoxScale.Controls.Add(txtRawMin);
            groupBoxScale.Controls.Add(comboBoxSignalType);
            groupBoxScale.Controls.Add(chkScale);
            groupBoxScale.Location = new Point(778, 274);
            groupBoxScale.Name = "groupBoxScale";
            groupBoxScale.Size = new Size(354, 172);
            groupBoxScale.TabIndex = 14;
            groupBoxScale.TabStop = false;
            groupBoxScale.Text = "工程量转换";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(222, 117);
            label2.Name = "label2";
            label2.Size = new Size(54, 19);
            label2.TabIndex = 8;
            label2.Text = "物理值";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 117);
            label1.Name = "label1";
            label1.Size = new Size(54, 19);
            label1.TabIndex = 7;
            label1.Text = "原始值";
            // 
            // chkRawIsAdc
            // 
            chkRawIsAdc.AutoSize = true;
            chkRawIsAdc.Location = new Point(0, 55);
            chkRawIsAdc.Name = "chkRawIsAdc";
            chkRawIsAdc.Size = new Size(124, 23);
            chkRawIsAdc.TabIndex = 6;
            chkRawIsAdc.Text = "原始值为ADC";
            toolTip1.SetToolTip(chkRawIsAdc, "勾选后，原始值范围请填写程序实际收到的ADC值（如0~65535）；不勾选则填写信号值（如4~20）");
            chkRawIsAdc.UseVisualStyleBackColor = true;
            // 
            // txtEngMax
            // 
            txtEngMax.Location = new Point(288, 139);
            txtEngMax.Name = "txtEngMax";
            txtEngMax.Size = new Size(60, 27);
            txtEngMax.TabIndex = 5;
            txtEngMax.Text = "100";
            // 
            // txtEngMin
            // 
            txtEngMin.Location = new Point(222, 139);
            txtEngMin.Name = "txtEngMin";
            txtEngMin.Size = new Size(60, 27);
            txtEngMin.TabIndex = 4;
            txtEngMin.Text = "0";
            // 
            // txtRawMax
            // 
            txtRawMax.Location = new Point(72, 139);
            txtRawMax.Name = "txtRawMax";
            txtRawMax.Size = new Size(60, 27);
            txtRawMax.TabIndex = 3;
            txtRawMax.Text = "20";
            // 
            // txtRawMin
            // 
            txtRawMin.Location = new Point(6, 139);
            txtRawMin.Name = "txtRawMin";
            txtRawMin.Size = new Size(60, 27);
            txtRawMin.TabIndex = 2;
            txtRawMin.Text = "4";
            // 
            // comboBoxSignalType
            // 
            comboBoxSignalType.FormattingEnabled = true;
            comboBoxSignalType.Location = new Point(198, 22);
            comboBoxSignalType.Name = "comboBoxSignalType";
            comboBoxSignalType.Size = new Size(150, 27);
            comboBoxSignalType.TabIndex = 1;
            // 
            // chkScale
            // 
            chkScale.AutoSize = true;
            chkScale.Location = new Point(2, 26);
            chkScale.Name = "chkScale";
            chkScale.Size = new Size(61, 23);
            chkScale.TabIndex = 0;
            chkScale.Text = "启用";
            chkScale.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 753);
            Controls.Add(groupBoxScale);
            Controls.Add(comboBoxSendCheck);
            Controls.Add(comboBoxRecvCheck);
            Controls.Add(chkEnableConfig);
            Controls.Add(chkHexSend);
            Controls.Add(btnSend);
            Controls.Add(btnClear);
            Controls.Add(btnRefresh);
            Controls.Add(chkHexDisplay);
            Controls.Add(comboBoxEncoding);
            Controls.Add(labelStatus);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(comboBoxPorts);
            Controls.Add(btnOpen);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "串口工具 by52赫兹";
            FormClosed += Form1_FormClosed;
            groupBoxScale.ResumeLayout(false);
            groupBoxScale.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOpen;
        private ComboBox comboBoxPorts;
        private TextBox textBox1;
        private Button btnSend;
        private TextBox textBox2;
        private Label labelStatus;
        private ComboBox comboBoxEncoding;
        private CheckBox chkHexDisplay;
        private CheckBox chkHexSend;
        private Button btnRefresh;
        private Button btnClear;
        private CheckBox chkEnableConfig;
        private ComboBox comboBoxRecvCheck;
        private ComboBox comboBoxSendCheck;
        private GroupBox groupBoxScale;
        private ComboBox comboBoxSignalType;
        private CheckBox chkScale;
        private TextBox txtEngMax;
        private TextBox txtEngMin;
        private TextBox txtRawMax;
        private TextBox txtRawMin;
        private CheckBox chkRawIsAdc;
        private ToolTip toolTip1;
        private Label label2;
        private Label label1;
    }
}