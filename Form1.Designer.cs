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
            chkEnableConfig.Location = new Point(1088, 732);
            chkEnableConfig.Margin = new Padding(2);
            chkEnableConfig.Name = "chkEnableConfig";
            chkEnableConfig.Size = new Size(91, 23);
            chkEnableConfig.TabIndex = 11;
            chkEnableConfig.Text = "启用配置";
            chkEnableConfig.UseVisualStyleBackColor = true;
            // 
            // comboBoxRecvCheck
            // 
            comboBoxRecvCheck.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRecvCheck.FormattingEnabled = true;
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
            comboBoxSendCheck.Location = new Point(776, 95);
            comboBoxSendCheck.Margin = new Padding(1);
            comboBoxSendCheck.Name = "comboBoxSendCheck";
            comboBoxSendCheck.Size = new Size(123, 27);
            comboBoxSendCheck.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 753);
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
            Text = "串口工具    by52赫兹";
            FormClosed += Form1_FormClosed;
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
    }
}