namespace MemoryGame
{
    partial class frmMemoryGame
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpTable = new System.Windows.Forms.GroupBox();
            this.btnInfo = new System.Windows.Forms.Button();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.btnCloseResult = new System.Windows.Forms.Button();
            this.lblFinalStats = new System.Windows.Forms.Label();
            this.lblResultTitle = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.cmbSeconds = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCardCount = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.lblMoveCount = new System.Windows.Forms.Label();
            this.lblBestRecord = new System.Windows.Forms.Label();
            this.btnClearRecord = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.tkbVolume = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.grpTable.SuspendLayout();
            this.pnlResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // grpTable
            // 
            this.grpTable.Controls.Add(this.btnInfo);
            this.grpTable.Controls.Add(this.pnlResult);
            this.grpTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTable.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpTable.Location = new System.Drawing.Point(0, 0);
            this.grpTable.Margin = new System.Windows.Forms.Padding(2);
            this.grpTable.Name = "grpTable";
            this.grpTable.Padding = new System.Windows.Forms.Padding(2);
            this.grpTable.Size = new System.Drawing.Size(2884, 1451);
            this.grpTable.TabIndex = 0;
            this.grpTable.TabStop = false;
            this.grpTable.Text = "牌桌";
            // 
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.SystemColors.Info;
            this.btnInfo.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnInfo.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnInfo.Location = new System.Drawing.Point(2545, 73);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(288, 83);
            this.btnInfo.TabIndex = 1;
            this.btnInfo.Text = "ℹ️ 規則說明";
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // pnlResult
            // 
            this.pnlResult.BackColor = System.Drawing.Color.SeaShell;
            this.pnlResult.Controls.Add(this.btnCloseResult);
            this.pnlResult.Controls.Add(this.lblFinalStats);
            this.pnlResult.Controls.Add(this.lblResultTitle);
            this.pnlResult.Location = new System.Drawing.Point(1129, 168);
            this.pnlResult.Margin = new System.Windows.Forms.Padding(7);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(868, 899);
            this.pnlResult.TabIndex = 0;
            this.pnlResult.Visible = false;
            // 
            // btnCloseResult
            // 
            this.btnCloseResult.BackColor = System.Drawing.Color.OldLace;
            this.btnCloseResult.Enabled = false;
            this.btnCloseResult.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCloseResult.ForeColor = System.Drawing.Color.Goldenrod;
            this.btnCloseResult.Location = new System.Drawing.Point(334, 704);
            this.btnCloseResult.Margin = new System.Windows.Forms.Padding(7);
            this.btnCloseResult.Name = "btnCloseResult";
            this.btnCloseResult.Size = new System.Drawing.Size(254, 133);
            this.btnCloseResult.TabIndex = 8;
            this.btnCloseResult.Text = "確定";
            this.btnCloseResult.UseVisualStyleBackColor = false;
            this.btnCloseResult.Click += new System.EventHandler(this.btnCloseResult_Click);
            // 
            // lblFinalStats
            // 
            this.lblFinalStats.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFinalStats.AutoSize = true;
            this.lblFinalStats.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblFinalStats.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblFinalStats.Location = new System.Drawing.Point(178, 212);
            this.lblFinalStats.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblFinalStats.Name = "lblFinalStats";
            this.lblFinalStats.Size = new System.Drawing.Size(107, 53);
            this.lblFinalStats.TabIndex = 9;
            this.lblFinalStats.Text = "步數";
            // 
            // lblResultTitle
            // 
            this.lblResultTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblResultTitle.AutoSize = true;
            this.lblResultTitle.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblResultTitle.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblResultTitle.Location = new System.Drawing.Point(217, 100);
            this.lblResultTitle.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblResultTitle.Name = "lblResultTitle";
            this.lblResultTitle.Size = new System.Drawing.Size(416, 77);
            this.lblResultTitle.TabIndex = 8;
            this.lblResultTitle.Text = "恭喜通關！🥳";
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("微軟正黑體", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMsg.Location = new System.Drawing.Point(33, 1521);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(207, 42);
            this.lblMsg.TabIndex = 1;
            this.lblMsg.Text = "請點擊開始...";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStart.Location = new System.Drawing.Point(861, 1598);
            this.btnStart.Margin = new System.Windows.Forms.Padding(7);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(254, 133);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "開始遊戲";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cmbSeconds
            // 
            this.cmbSeconds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeconds.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbSeconds.FormattingEnabled = true;
            this.cmbSeconds.Items.AddRange(new object[] {
            "3",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40"});
            this.cmbSeconds.Location = new System.Drawing.Point(296, 1598);
            this.cmbSeconds.Margin = new System.Windows.Forms.Padding(7);
            this.cmbSeconds.Name = "cmbSeconds";
            this.cmbSeconds.Size = new System.Drawing.Size(370, 45);
            this.cmbSeconds.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(33, 1600);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "請選擇秒數";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(33, 1678);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 46);
            this.label2.TabIndex = 4;
            this.label2.Text = "請選擇張數";
            // 
            // cmbCardCount
            // 
            this.cmbCardCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardCount.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbCardCount.FormattingEnabled = true;
            this.cmbCardCount.Items.AddRange(new object[] {
            "4",
            "8",
            "12",
            "16",
            "20",
            "30",
            "40",
            "52"});
            this.cmbCardCount.Location = new System.Drawing.Point(296, 1674);
            this.cmbCardCount.Margin = new System.Windows.Forms.Padding(7);
            this.cmbCardCount.Name = "cmbCardCount";
            this.cmbCardCount.Size = new System.Drawing.Size(370, 45);
            this.cmbCardCount.TabIndex = 5;
            this.cmbCardCount.SelectedIndexChanged += new System.EventHandler(this.cmbCardCount_SelectedIndexChanged);
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnReset.Location = new System.Drawing.Point(1129, 1598);
            this.btnReset.Margin = new System.Windows.Forms.Padding(7);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(254, 133);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "重新開始";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 1000;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // lblTime
            // 
            this.lblTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTime.Location = new System.Drawing.Point(1404, 1480);
            this.lblTime.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(203, 46);
            this.lblTime.TabIndex = 7;
            this.lblTime.Text = "時間: 00:00";
            // 
            // lblMoveCount
            // 
            this.lblMoveCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMoveCount.AutoSize = true;
            this.lblMoveCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMoveCount.Location = new System.Drawing.Point(1404, 1570);
            this.lblMoveCount.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblMoveCount.Name = "lblMoveCount";
            this.lblMoveCount.Size = new System.Drawing.Size(131, 46);
            this.lblMoveCount.TabIndex = 8;
            this.lblMoveCount.Text = "步數: 0";
            // 
            // lblBestRecord
            // 
            this.lblBestRecord.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBestRecord.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblBestRecord.Location = new System.Drawing.Point(1404, 1660);
            this.lblBestRecord.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblBestRecord.Name = "lblBestRecord";
            this.lblBestRecord.Size = new System.Drawing.Size(1261, 64);
            this.lblBestRecord.TabIndex = 9;
            this.lblBestRecord.Text = "最佳紀錄: --";
            // 
            // btnClearRecord
            // 
            this.btnClearRecord.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClearRecord.Location = new System.Drawing.Point(861, 1509);
            this.btnClearRecord.Margin = new System.Windows.Forms.Padding(7);
            this.btnClearRecord.Name = "btnClearRecord";
            this.btnClearRecord.Size = new System.Drawing.Size(254, 71);
            this.btnClearRecord.TabIndex = 10;
            this.btnClearRecord.Text = "重設最高分";
            this.btnClearRecord.UseVisualStyleBackColor = true;
            this.btnClearRecord.Click += new System.EventHandler(this.btnClearRecord_Click);
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPause.Location = new System.Drawing.Point(1129, 1509);
            this.btnPause.Margin = new System.Windows.Forms.Padding(7);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(254, 71);
            this.btnPause.TabIndex = 11;
            this.btnPause.Text = "暫停遊戲";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // tkbVolume
            // 
            this.tkbVolume.Location = new System.Drawing.Point(2010, 1456);
            this.tkbVolume.Name = "tkbVolume";
            this.tkbVolume.Size = new System.Drawing.Size(560, 101);
            this.tkbVolume.TabIndex = 12;
            this.tkbVolume.Value = 4;
            this.tkbVolume.Scroll += new System.EventHandler(this.tkbVolume_Scroll);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(1781, 1480);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 46);
            this.label3.TabIndex = 13;
            this.label3.Text = "音量大小";
            // 
            // frmMemoryGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2884, 1753);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tkbVolume);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnClearRecord);
            this.Controls.Add(this.lblBestRecord);
            this.Controls.Add(this.lblMoveCount);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cmbCardCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSeconds);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.grpTable);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMemoryGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "記憶翻牌遊戲";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMemoryGame_FormClosing);
            this.grpTable.ResumeLayout(false);
            this.pnlResult.ResumeLayout(false);
            this.pnlResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTable;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ComboBox cmbSeconds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCardCount;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Button btnCloseResult;
        private System.Windows.Forms.Label lblFinalStats;
        private System.Windows.Forms.Label lblResultTitle;
        private System.Windows.Forms.Label lblMoveCount;
        private System.Windows.Forms.Label lblBestRecord;
        private System.Windows.Forms.Button btnClearRecord;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.TrackBar tkbVolume;
        private System.Windows.Forms.Label label3;
    }
}

