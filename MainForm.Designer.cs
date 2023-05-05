namespace Demo
{
    partial class BasicFrom
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Display_pictureBox = new PictureBox();
            Start = new Button();
            Stop = new Button();
            Quit = new Button();
            Check = new Button();
            Open_Image_button = new Button();
            SearchArea_Min = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SearchArea_Max = new TextBox();
            label3 = new Label();
            ROI_Radius = new TextBox();
            label4 = new Label();
            MedianMaskRadius = new TextBox();
            label5 = new Label();
            MinGray = new TextBox();
            label6 = new Label();
            MaxGray = new TextBox();
            Log = new ListBox();
            Save_Parameter = new Button();
            groupBox1 = new GroupBox();
            Stop_Camera = new Button();
            Continuous_acquisition = new Button();
            Single_frame = new Button();
            open_camera = new Button();
            ClearNum = new Button();
            groupBox2 = new GroupBox();
            panel1 = new Panel();
            LabTotal = new Label();
            panel2 = new Panel();
            LabBad = new Label();
            panel3 = new Panel();
            labYield = new Label();
            panel4 = new Panel();
            List_Camera = new ComboBox();
            groupBox3 = new GroupBox();
            Port = new TextBox();
            label8 = new Label();
            IP = new TextBox();
            label7 = new Label();
            panel5 = new Panel();
            ((System.ComponentModel.ISupportInitialize)Display_pictureBox).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            groupBox3.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // Display_pictureBox
            // 
            Display_pictureBox.BackColor = Color.Black;
            Display_pictureBox.Location = new Point(-3, 3);
            Display_pictureBox.Name = "Display_pictureBox";
            Display_pictureBox.Size = new Size(538, 438);
            Display_pictureBox.TabIndex = 0;
            Display_pictureBox.TabStop = false;
            Display_pictureBox.MouseClick += Display_MouseDouleClick;
            Display_pictureBox.MouseDown += pictureBox_MouseDown;
            Display_pictureBox.MouseMove += pictureBox_MouseMove;
            Display_pictureBox.MouseUp += pictureBox_MouseUp;
            Display_pictureBox.MouseWheel += pictureBox_MouseWheel;
            // 
            // Start
            // 
            Start.BackColor = SystemColors.GradientActiveCaption;
            Start.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Start.Location = new Point(233, 78);
            Start.Name = "Start";
            Start.Size = new Size(94, 29);
            Start.TabIndex = 3;
            Start.Text = "开始测试";
            Start.UseVisualStyleBackColor = false;
            Start.Click += Start_Click;
            // 
            // Stop
            // 
            Stop.BackColor = SystemColors.GradientActiveCaption;
            Stop.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Stop.Location = new Point(233, 26);
            Stop.Name = "Stop";
            Stop.Size = new Size(94, 29);
            Stop.TabIndex = 4;
            Stop.Text = "结束测试";
            Stop.UseVisualStyleBackColor = false;
            Stop.Click += Stop_Click;
            // 
            // Quit
            // 
            Quit.BackColor = SystemColors.GradientActiveCaption;
            Quit.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Quit.Location = new Point(560, 26);
            Quit.Name = "Quit";
            Quit.Size = new Size(94, 29);
            Quit.TabIndex = 5;
            Quit.Text = "退出";
            Quit.UseVisualStyleBackColor = false;
            Quit.Click += Quit_Click;
            // 
            // Check
            // 
            Check.BackColor = SystemColors.GradientActiveCaption;
            Check.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Check.Location = new Point(346, 26);
            Check.Name = "Check";
            Check.Size = new Size(94, 29);
            Check.TabIndex = 6;
            Check.Text = "缺陷检测";
            Check.UseVisualStyleBackColor = false;
            Check.Click += Check_Click;
            // 
            // Open_Image_button
            // 
            Open_Image_button.BackColor = SystemColors.GradientActiveCaption;
            Open_Image_button.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Open_Image_button.Location = new Point(9, 78);
            Open_Image_button.Name = "Open_Image_button";
            Open_Image_button.Size = new Size(94, 29);
            Open_Image_button.TabIndex = 7;
            Open_Image_button.Text = "打开图片";
            Open_Image_button.UseVisualStyleBackColor = false;
            Open_Image_button.Click += Open_Image_button_Click;
            // 
            // SearchArea_Min
            // 
            SearchArea_Min.Location = new Point(198, 28);
            SearchArea_Min.Name = "SearchArea_Min";
            SearchArea_Min.Size = new Size(106, 27);
            SearchArea_Min.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(6, 32);
            label1.Name = "label1";
            label1.Size = new Size(160, 18);
            label1.TabIndex = 10;
            label1.Text = "搜索面积最小值：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(329, 32);
            label2.Name = "label2";
            label2.Size = new Size(160, 18);
            label2.TabIndex = 11;
            label2.Text = "搜索面积最大值：";
            // 
            // SearchArea_Max
            // 
            SearchArea_Max.Location = new Point(517, 26);
            SearchArea_Max.Name = "SearchArea_Max";
            SearchArea_Max.Size = new Size(106, 27);
            SearchArea_Max.TabIndex = 12;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(6, 64);
            label3.Name = "label3";
            label3.Size = new Size(171, 18);
            label3.TabIndex = 13;
            label3.Text = "获取ROI区域半径：";
            // 
            // ROI_Radius
            // 
            ROI_Radius.Location = new Point(198, 61);
            ROI_Radius.Name = "ROI_Radius";
            ROI_Radius.Size = new Size(106, 27);
            ROI_Radius.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(329, 64);
            label4.Name = "label4";
            label4.Size = new Size(179, 18);
            label4.TabIndex = 15;
            label4.Text = "中值滤波掩膜半径：";
            // 
            // MedianMaskRadius
            // 
            MedianMaskRadius.Location = new Point(517, 64);
            MedianMaskRadius.Name = "MedianMaskRadius";
            MedianMaskRadius.Size = new Size(106, 27);
            MedianMaskRadius.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(9, 102);
            label5.Name = "label5";
            label5.Size = new Size(160, 18);
            label5.TabIndex = 17;
            label5.Text = "二值化最小灰度：";
            // 
            // MinGray
            // 
            MinGray.Location = new Point(198, 96);
            MinGray.Name = "MinGray";
            MinGray.Size = new Size(106, 27);
            MinGray.TabIndex = 18;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(329, 99);
            label6.Name = "label6";
            label6.Size = new Size(160, 18);
            label6.TabIndex = 19;
            label6.Text = "二值化最大灰度：";
            // 
            // MaxGray
            // 
            MaxGray.Location = new Point(517, 99);
            MaxGray.Name = "MaxGray";
            MaxGray.Size = new Size(106, 27);
            MaxGray.TabIndex = 20;
            // 
            // Log
            // 
            Log.BackColor = SystemColors.InactiveCaption;
            Log.FormattingEnabled = true;
            Log.HorizontalScrollbar = true;
            Log.ItemHeight = 20;
            Log.Location = new Point(3, 496);
            Log.Name = "Log";
            Log.Size = new Size(538, 144);
            Log.TabIndex = 21;
            // 
            // Save_Parameter
            // 
            Save_Parameter.BackColor = SystemColors.GradientActiveCaption;
            Save_Parameter.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Save_Parameter.Location = new Point(346, 78);
            Save_Parameter.Name = "Save_Parameter";
            Save_Parameter.Size = new Size(94, 29);
            Save_Parameter.TabIndex = 22;
            Save_Parameter.Text = "保存参数";
            Save_Parameter.UseVisualStyleBackColor = false;
            Save_Parameter.Click += Save_Parameter_Click;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.BackColor = SystemColors.InactiveCaption;
            groupBox1.Controls.Add(Stop_Camera);
            groupBox1.Controls.Add(Continuous_acquisition);
            groupBox1.Controls.Add(Single_frame);
            groupBox1.Controls.Add(open_camera);
            groupBox1.Controls.Add(ClearNum);
            groupBox1.Controls.Add(Open_Image_button);
            groupBox1.Controls.Add(Save_Parameter);
            groupBox1.Controls.Add(Start);
            groupBox1.Controls.Add(Stop);
            groupBox1.Controls.Add(Check);
            groupBox1.Controls.Add(Quit);
            groupBox1.Font = new Font("微软雅黑", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            groupBox1.Location = new Point(559, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(686, 133);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "数据区";
            // 
            // Stop_Camera
            // 
            Stop_Camera.BackColor = SystemColors.GradientActiveCaption;
            Stop_Camera.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Stop_Camera.Location = new Point(459, 26);
            Stop_Camera.Name = "Stop_Camera";
            Stop_Camera.Size = new Size(94, 29);
            Stop_Camera.TabIndex = 27;
            Stop_Camera.Text = "关闭相机";
            Stop_Camera.UseVisualStyleBackColor = false;
            Stop_Camera.Click += Stop_Camera_Click;
            // 
            // Continuous_acquisition
            // 
            Continuous_acquisition.BackColor = SystemColors.GradientActiveCaption;
            Continuous_acquisition.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Continuous_acquisition.Location = new Point(118, 78);
            Continuous_acquisition.Name = "Continuous_acquisition";
            Continuous_acquisition.Size = new Size(94, 29);
            Continuous_acquisition.TabIndex = 26;
            Continuous_acquisition.Text = "连续采集";
            Continuous_acquisition.UseVisualStyleBackColor = false;
            Continuous_acquisition.Click += Continuous_acquisition_Click;
            // 
            // Single_frame
            // 
            Single_frame.BackColor = SystemColors.GradientActiveCaption;
            Single_frame.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Single_frame.Location = new Point(118, 26);
            Single_frame.Name = "Single_frame";
            Single_frame.Size = new Size(94, 29);
            Single_frame.TabIndex = 25;
            Single_frame.Text = "单帧采集";
            Single_frame.UseVisualStyleBackColor = false;
            Single_frame.Click += Single_frame_Click;
            // 
            // open_camera
            // 
            open_camera.BackColor = SystemColors.GradientActiveCaption;
            open_camera.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            open_camera.Location = new Point(7, 26);
            open_camera.Name = "open_camera";
            open_camera.Size = new Size(94, 29);
            open_camera.TabIndex = 24;
            open_camera.Text = "打开相机";
            open_camera.UseVisualStyleBackColor = false;
            open_camera.Click += open_camera_Click;
            // 
            // ClearNum
            // 
            ClearNum.BackColor = SystemColors.GradientActiveCaption;
            ClearNum.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ClearNum.Location = new Point(459, 78);
            ClearNum.Name = "ClearNum";
            ClearNum.Size = new Size(94, 29);
            ClearNum.TabIndex = 23;
            ClearNum.Text = "清除数量";
            ClearNum.UseVisualStyleBackColor = false;
            ClearNum.Click += ClearNum_Click;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = SystemColors.InactiveCaption;
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(SearchArea_Min);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(MaxGray);
            groupBox2.Controls.Add(SearchArea_Max);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(MinGray);
            groupBox2.Controls.Add(ROI_Radius);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(MedianMaskRadius);
            groupBox2.Font = new Font("微软雅黑", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            groupBox2.Location = new Point(559, 158);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(654, 146);
            groupBox2.TabIndex = 24;
            groupBox2.TabStop = false;
            groupBox2.Text = "参数区";
            // 
            // panel1
            // 
            panel1.Controls.Add(LabTotal);
            panel1.Location = new Point(559, 476);
            panel1.Name = "panel1";
            panel1.Size = new Size(450, 52);
            panel1.TabIndex = 25;
            // 
            // LabTotal
            // 
            LabTotal.AutoSize = true;
            LabTotal.Font = new Font("华文楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            LabTotal.Location = new Point(9, 14);
            LabTotal.Name = "LabTotal";
            LabTotal.Size = new Size(66, 20);
            LabTotal.TabIndex = 0;
            LabTotal.Text = "总数：";
            // 
            // panel2
            // 
            panel2.Controls.Add(LabBad);
            panel2.Location = new Point(559, 534);
            panel2.Name = "panel2";
            panel2.Size = new Size(450, 50);
            panel2.TabIndex = 26;
            // 
            // LabBad
            // 
            LabBad.AutoSize = true;
            LabBad.Font = new Font("华文楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            LabBad.Location = new Point(6, 14);
            LabBad.Name = "LabBad";
            LabBad.Size = new Size(66, 20);
            LabBad.TabIndex = 0;
            LabBad.Text = "不良：";
            // 
            // panel3
            // 
            panel3.Controls.Add(labYield);
            panel3.Location = new Point(559, 590);
            panel3.Name = "panel3";
            panel3.Size = new Size(451, 50);
            panel3.TabIndex = 27;
            // 
            // labYield
            // 
            labYield.AutoSize = true;
            labYield.Font = new Font("华文楷体", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            labYield.Location = new Point(9, 14);
            labYield.Name = "labYield";
            labYield.Size = new Size(66, 20);
            labYield.TabIndex = 0;
            labYield.Text = "良率：";
            // 
            // panel4
            // 
            panel4.Controls.Add(Display_pictureBox);
            panel4.Location = new Point(3, 46);
            panel4.Name = "panel4";
            panel4.Size = new Size(538, 444);
            panel4.TabIndex = 24;
            // 
            // List_Camera
            // 
            List_Camera.FormattingEnabled = true;
            List_Camera.Location = new Point(3, 12);
            List_Camera.Name = "List_Camera";
            List_Camera.Size = new Size(538, 28);
            List_Camera.TabIndex = 28;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(Port);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(IP);
            groupBox3.Controls.Add(label7);
            groupBox3.Font = new Font("微软雅黑", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            groupBox3.Location = new Point(559, 321);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(648, 125);
            groupBox3.TabIndex = 29;
            groupBox3.TabStop = false;
            groupBox3.Text = "通讯";
            // 
            // Port
            // 
            Port.Location = new Point(373, 33);
            Port.Name = "Port";
            Port.Size = new Size(125, 27);
            Port.TabIndex = 3;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(310, 38);
            label8.Name = "label8";
            label8.Size = new Size(47, 15);
            label8.TabIndex = 2;
            label8.Text = "Port:";
            // 
            // IP
            // 
            IP.Location = new Point(52, 33);
            IP.Name = "IP";
            IP.Size = new Size(125, 27);
            IP.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("隶书", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(12, 38);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 0;
            label7.Text = "IP:";
            // 
            // panel5
            // 
            panel5.Controls.Add(List_Camera);
            panel5.Controls.Add(panel3);
            panel5.Controls.Add(groupBox3);
            panel5.Controls.Add(panel2);
            panel5.Controls.Add(panel4);
            panel5.Controls.Add(panel1);
            panel5.Controls.Add(Log);
            panel5.Controls.Add(groupBox1);
            panel5.Controls.Add(groupBox2);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(1298, 665);
            panel5.TabIndex = 30;
            // 
            // BasicFrom
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(1298, 665);
            Controls.Add(panel5);
            Name = "BasicFrom";
            Text = "上海美诺福科技有限公司";
            FormClosing += MainForm_ClosingForm;
            Resize += BasicDemo_Resize;
            ((System.ComponentModel.ISupportInitialize)Display_pictureBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ResumeLayout(false);
        }

        private void BasicFrom_Resize(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private PictureBox Display_pictureBox;
        private Button Start;
        private Button Stop;
        private Button Quit;
        private Button Check;
        private Button Open_Image_button;
        private TextBox SearchArea_Min;
        private Label label1;
        private Label label2;
        private TextBox SearchArea_Max;
        private Label label3;
        private TextBox ROI_Radius;
        private Label label4;
        private TextBox MedianMaskRadius;
        private Label label5;
        private TextBox MinGray;
        private Label label6;
        private TextBox MaxGray;
        private ListBox Log;
        private Button Save_Parameter;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Panel panel1;
        private Label LabTotal;
        private Panel panel2;
        private Label LabBad;
        private Panel panel3;
        private Label labYield;
        private Button ClearNum;
        private Panel panel4;
        private Button open_camera;
        private ComboBox List_Camera;
        private Button Continuous_acquisition;
        private Button Single_frame;
        private GroupBox groupBox3;
        private TextBox Port;
        private Label label8;
        private TextBox IP;
        private Label label7;
        private Button Stop_Camera;
        private Panel panel5;
    }
}