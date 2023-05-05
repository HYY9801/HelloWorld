namespace Demo
{
    partial class RuningForm
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
            PanelWindow = new Panel();
            btn_start = new Button();
            btn_Setting = new Button();
            Listlog = new ListBox();
            panel_btn = new Panel();
            btn_quit = new Button();
            panel_btn.SuspendLayout();
            SuspendLayout();
            // 
            // PanelWindow
            // 
            PanelWindow.Location = new Point(12, 12);
            PanelWindow.Name = "PanelWindow";
            PanelWindow.Size = new Size(628, 436);
            PanelWindow.TabIndex = 0;
            // 
            // btn_start
            // 
            btn_start.BackColor = Color.LightGray;
            btn_start.Location = new Point(30, 29);
            btn_start.Name = "btn_start";
            btn_start.Size = new Size(186, 62);
            btn_start.TabIndex = 1;
            btn_start.Text = "运行";
            btn_start.UseVisualStyleBackColor = false;
            btn_start.Click += Start_Run_Click;
            // 
            // btn_Setting
            // 
            btn_Setting.BackColor = Color.LightGray;
            btn_Setting.Location = new Point(30, 146);
            btn_Setting.Name = "btn_Setting";
            btn_Setting.Size = new Size(186, 63);
            btn_Setting.TabIndex = 2;
            btn_Setting.Text = "设置";
            btn_Setting.UseVisualStyleBackColor = false;
            btn_Setting.Click += Setting_Click;
            // 
            // Listlog
            // 
            Listlog.FormattingEnabled = true;
            Listlog.ItemHeight = 20;
            Listlog.Location = new Point(12, 454);
            Listlog.Name = "Listlog";
            Listlog.Size = new Size(628, 124);
            Listlog.TabIndex = 3;
            // 
            // panel_btn
            // 
            panel_btn.Controls.Add(btn_quit);
            panel_btn.Controls.Add(btn_Setting);
            panel_btn.Controls.Add(btn_start);
            panel_btn.Location = new Point(646, 12);
            panel_btn.Name = "panel_btn";
            panel_btn.Size = new Size(241, 363);
            panel_btn.TabIndex = 4;
            // 
            // btn_quit
            // 
            btn_quit.BackColor = Color.LightGray;
            btn_quit.Location = new Point(30, 273);
            btn_quit.Name = "btn_quit";
            btn_quit.Size = new Size(186, 63);
            btn_quit.TabIndex = 3;
            btn_quit.Text = "退出";
            btn_quit.UseVisualStyleBackColor = false;
            btn_quit.Click += btn_quit_Click;
            // 
            // RuningForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(899, 584);
            Controls.Add(Listlog);
            Controls.Add(panel_btn);
            Controls.Add(PanelWindow);
            Name = "RuningForm";
            Text = "上海美诺福科技有限公司";
            FormClosing += RunningForm_ClosingFrom;
            Load += Load_RuningForm;
            panel_btn.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelWindow;
        private Button btn_start;
        private Button btn_Setting;
        private ListBox Listlog;
        private Panel panel_btn;
        private Button btn_quit;
    }
}