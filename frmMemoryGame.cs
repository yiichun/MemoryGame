using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace MemoryGame
{
    public partial class frmMemoryGame : Form
    {
        List<PictureBox> pics = new List<PictureBox>();
        int[] cardValues;
        PictureBox firstCard = null, secondCard = null;
        bool isChecking = true;

        DateTime startTime;
        int moveCount = 0;
        int bestMove = int.MaxValue;
        int bestTimeInSeconds = int.MaxValue;

        private bool isPaused = false;
        private DateTime pauseStartTime;
        private TimeSpan totalPausedDuration = TimeSpan.Zero;

        private System.Windows.Media.MediaPlayer bgmPlayer = new System.Windows.Media.MediaPlayer();
        private System.Windows.Media.MediaPlayer soundSuccess = new System.Windows.Media.MediaPlayer();
        private System.Windows.Media.MediaPlayer soundFail = new System.Windows.Media.MediaPlayer();

        private bool isBgmLoaded = false;
        private double currentVolume = 0.4; 

        private Panel pnlInfoContainer = null;
        private DateTime infoStartTime;
        private bool isShowingInfo = false;

        private Dictionary<string, Image> imageCache = new Dictionary<string, Image>();

        public frmMemoryGame()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            EnableControlDoubleBuffer(grpTable);

            if (cmbSeconds.Items.Count > 0) cmbSeconds.SelectedIndex = 0;
            if (cmbCardCount.Items.Count > 0) cmbCardCount.SelectedIndex = 0;

            tkbVolume.Minimum = 0;
            tkbVolume.Maximum = 10;
            tkbVolume.Value = 4;

            this.KeyDown += new KeyEventHandler(frmMemoryGame_KeyDown);
            this.KeyPreview = true;
            btnReset.Enabled = false;
            btnPause.Enabled = false; 
            pnlResult.Visible = false;

            InitializeAudio();

            LoadBestRecord(16);
        }

        private void EnableControlDoubleBuffer(Control c)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            pi?.SetValue(c, true, null);
        }

        private void InitializeAudio()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string bgmPath = Path.Combine(baseDir, "bgm.mp3");
                string successPath = Path.Combine(baseDir, "success.wav");
                string failPath = Path.Combine(baseDir, "fail.wav");

                if (!File.Exists(bgmPath) && Properties.Resources.bgm != null)
                    File.WriteAllBytes(bgmPath, Properties.Resources.bgm);

                if (!File.Exists(successPath) && Properties.Resources.success != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Properties.Resources.success.CopyTo(ms);
                        File.WriteAllBytes(successPath, ms.ToArray());
                    }
                }

                if (!File.Exists(failPath) && Properties.Resources.fail != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Properties.Resources.fail.CopyTo(ms);
                        File.WriteAllBytes(failPath, ms.ToArray());
                    }
                }

                if (File.Exists(bgmPath))
                {
                    bgmPlayer.Open(new Uri(Path.GetFullPath(bgmPath)));

                    bgmPlayer.MediaEnded += (s, e) => {
                        this.BeginInvoke(new Action(() => {
                            bgmPlayer.Position = TimeSpan.Zero;
                            bgmPlayer.Play();
                        }));
                    };
                    isBgmLoaded = true;
                }

                if (File.Exists(successPath)) soundSuccess.Open(new Uri(Path.GetFullPath(successPath)));
                if (File.Exists(failPath)) soundFail.Open(new Uri(Path.GetFullPath(failPath)));

                UpdateAllVolumes(currentVolume);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("音訊系統初始化失敗: " + ex.Message);
            }
        }

        private void UpdateAllVolumes(double volume)
        {
            currentVolume = volume;
            bgmPlayer.Volume = volume;
            soundSuccess.Volume = volume;
            soundFail.Volume = volume;
        }

        private void tkbVolume_Scroll(object sender, EventArgs e)
        {
            double newVolume = tkbVolume.Value / 10.0;
            UpdateAllVolumes(newVolume);
        }

        private void LoadBestRecord(int totalCards)
        {
            string fileName = $"highscore_{totalCards}.txt";
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                if (lines.Length >= 2)
                {
                    int.TryParse(lines[0], out bestMove);
                    int.TryParse(lines[1], out bestTimeInSeconds);
                    string timeStr = $"{bestTimeInSeconds / 60:00}:{bestTimeInSeconds % 60:00}";
                    lblBestRecord.Text = $"最佳紀錄({totalCards}張): {bestMove}步 / {timeStr}";
                    return;
                }
            }
            bestMove = int.MaxValue;
            bestTimeInSeconds = int.MaxValue;
            lblBestRecord.Text = $"最佳紀錄({totalCards}張): 尚未有紀錄";
        }

        private async void btnStart_Click(object sender, EventArgs e) => await StartNewGame();

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (pics.Any(p => (int)p.Tag != -1))
            {
                if (MessageBox.Show("確定要結束當前遊戲，重新選擇牌面嗎？", "重新選擇",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            gameTimer.Stop();
            if (isBgmLoaded) bgmPlayer.Stop();

            foreach (var p in pics)
            {
                grpTable.Controls.Remove(p);
                p.Image = null;
                p.Dispose();
            }
            pics.Clear();

            cmbCardCount.Enabled = true;
            cmbSeconds.Enabled = true;

            isChecking = true; // 鎖定點擊
            isPaused = false;
            isShowingInfo = false;
            if (pnlInfoContainer != null) HideGameInfo(); // 如果有開說明同步關閉
            pnlResult.Visible = false; // 關閉結算畫面

            moveCount = 0;
            lblMoveCount.Text = "步數: 0";
            lblTime.Text = "時間: 00:00";
            lblMsg.Text = "請重新選擇牌面與秒數，然後點擊「開始遊戲」！";

            btnStart.Enabled = true;
            btnReset.Enabled = false;
            btnPause.Enabled = false;
            btnPause.Text = "暫停遊戲";

            int currentTotalCards = int.Parse(cmbCardCount.SelectedItem?.ToString() ?? "16");
            LoadBestRecord(currentTotalCards);
        }

        private async Task StartNewGame()
        {
            if (isShowingInfo) HideGameInfo();

            pnlResult.Visible = false;
            btnStart.Enabled = false;
            btnReset.Enabled = false;
            btnPause.Enabled = false; // 預覽記憶阶段還不能按暫停
            btnPause.Text = "暫停遊戲";

            isPaused = false;
            totalPausedDuration = TimeSpan.Zero;

            cmbSeconds.Enabled = false;
            cmbCardCount.Enabled = false;
            isChecking = true;

            moveCount = 0;
            lblMoveCount.Text = "步數: 0";
            gameTimer.Stop();
            lblTime.Text = "時間: 00:00";

            foreach (var p in pics)
            {
                grpTable.Controls.Remove(p);
                p.Image = null;
                p.Dispose();
            }
            pics.Clear();

            int totalCards = int.Parse(cmbCardCount.SelectedItem?.ToString() ?? "16");
            LoadBestRecord(totalCards);

            imageCache.Clear();
            imageCache["back"] = Properties.Resources.back;
            Shuffle(totalCards);

            for (int i = 0; i < totalCards; i++)
            {
                string picKey = "pic" + (cardValues[i] + 1);
                if (!imageCache.ContainsKey(picKey))
                {
                    Image originalImg = (Image)Properties.Resources.ResourceManager.GetObject(picKey);
                    imageCache[picKey] = originalImg;
                }
            }

            CreateAdaptiveLayout(totalCards);

            if (isBgmLoaded)
            {
                bgmPlayer.Position = TimeSpan.Zero;
                bgmPlayer.Play();
            }

            for (int i = 0; i < totalCards; i++)
                pics[i].Image = imageCache["pic" + (cardValues[i] + 1)];

            int previewSeconds = int.Parse(cmbSeconds.SelectedItem?.ToString() ?? "3");
            for (int i = previewSeconds; i > 0; i--)
            {
                lblMsg.Text = $"請記住位置！剩餘 {i} 秒...";
                await Task.Delay(1000);
            }

            foreach (var p in pics) p.Image = imageCache["back"];
            lblMsg.Text = "遊戲開始！";

            startTime = DateTime.Now;
            gameTimer.Start();

            Application.DoEvents();
            isChecking = false;

            btnReset.Enabled = true;
            btnPause.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (isShowingInfo) return;

            if (!isPaused)
            {
                isPaused = true;
                gameTimer.Stop();
                pauseStartTime = DateTime.Now;
                isChecking = true;

                if (isBgmLoaded) bgmPlayer.Pause();

                btnPause.Text = "繼續遊戲";
                lblMsg.Text = "遊戲暫停中...";

                cmbCardCount.Enabled = false;
                cmbSeconds.Enabled = false;

                foreach (var p in pics)
                {
                    p.Visible = false;
                }
            }
            else
            {
                isPaused = false;
                totalPausedDuration += (DateTime.Now - pauseStartTime);

                gameTimer.Start();
                isChecking = false;

                if (isBgmLoaded) bgmPlayer.Play();

                btnPause.Text = "暫停遊戲";
                lblMsg.Text = "遊戲繼續！";

                cmbCardCount.Enabled = false;
                cmbSeconds.Enabled = false;

                foreach (var p in pics)
                {
                    if ((int)p.Tag != -1)
                    {
                        p.Visible = true;
                    }
                }

                Application.DoEvents();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if (isShowingInfo) HideGameInfo();
            else ShowGameInfo();
        }

        private void ShowGameInfo()
        {
            isShowingInfo = true;
            infoStartTime = DateTime.Now;

            bool wasTimerRunning = gameTimer.Enabled;
            if (wasTimerRunning) gameTimer.Stop();

            foreach (var p in pics) p.Visible = false;

            pnlInfoContainer = new Panel();
            pnlInfoContainer.Size = new Size(grpTable.Width - 20, grpTable.Height - 30);
            pnlInfoContainer.Location = new Point(10, 20);
            pnlInfoContainer.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            pnlInfoContainer.BorderStyle = BorderStyle.FixedSingle;

            Label lblTitle = new Label();
            lblTitle.Text = "🃏 翻牌記憶對對碰 - 完整遊戲秘笈 🃏";
            lblTitle.Font = new Font("Microsoft JhengHei", 24, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 70;
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);

            RichTextBox rtbContent = new RichTextBox();
            rtbContent.Font = new Font("Microsoft JhengHei", 14, FontStyle.Regular);
            rtbContent.ReadOnly = true;
            rtbContent.BorderStyle = BorderStyle.None;
            rtbContent.BackColor = pnlInfoContainer.BackColor;
            rtbContent.Dock = DockStyle.Fill;
            rtbContent.ScrollBars = RichTextBoxScrollBars.Vertical;

            rtbContent.Text =
                "\n歡迎來到記憶力大考驗！在開始這場大腦風暴之前，請先閱讀以下完美的遊戲導覽：\n\n" +
                "⚙️【 玩法參數自由配 】\n" +
                "  • 遊玩張數：彈性提供 4、8、12、16、20、30、40 張牌，更支援一鍵挑戰 52張 撲克豪華全陣容！\n" +
                "  • 預覽時間：開局時卡片會翻開供您記憶，可自由拉選 3、5、10、15、20、25、30、35、40秒 記憶時間。\n\n" +
                "⏳【 記憶倒數與啟動 】\n" +
                "  • 點擊「開始遊戲」後進入記憶模式，下方提示欄會同步進行秒數倒數。\n" +
                "  • 倒數結束後，卡片會翻回背面，右側的「時間計時」與「步數統計」將會正式啟動！\n\n" +
                "🎯【 翻牌消除與反饋 】\n" +
                "  • 用滑鼠點擊任意兩張卡片：\n" +
                "    - 若圖案相同 (成功)：觸發輕快音效，外框亮起【綠色】，卡片隨後「流暢縮小蒸發」！\n" +
                "    - 若圖案不同 (失敗)：觸發警示音效，外框亮起【紅色】，並在 0.8秒 後自動蓋回。\n\n" +
                "🔄【 自由重選牌面機制 】\n" +
                "  • 遊戲中若想更換難度，可點擊「重新開始」，確認後系統會「清空目前場地」並「解除選單鎖定」。\n" +
                "  • 您可以好整以暇地「重新選牌」與調整秒數，調整完畢後再按下「開始遊戲」開啟新局！\n\n" +
                "⏸️【 貼心暫停防偷看 】\n" +
                "  • 遊玩中隨時可點選「暫停遊戲」：此時時間凍結，所有未配對卡片會自動隱藏，嚴防作弊！\n" +
                "  • 貼心提醒：在非文字輸入狀態下，按下鍵盤【空白鍵】也能瞬間觸發暫停與繼續喔！\n\n" +
                "🏆【 榮譽殿堂最高紀錄 】\n" +
                "  • 系統會自動記錄每種張數模式下的「最少步數」與「最快時間」，並永久保存至本地。\n" +
                "  • 如果想要重新挑戰自我，隨時可以點擊「清除紀錄」將該難度的成績歸零。";

            HighlightText(rtbContent, "52張", System.Drawing.Color.Crimson);
            HighlightText(rtbContent, "開始遊戲", System.Drawing.Color.RoyalBlue);
            HighlightText(rtbContent, "時間計時", System.Drawing.Color.DarkOrange);
            HighlightText(rtbContent, "步數統計", System.Drawing.Color.DarkOrange);
            HighlightText(rtbContent, "【綠色】", System.Drawing.Color.LimeGreen);
            HighlightText(rtbContent, "「流暢縮小蒸發」", System.Drawing.Color.ForestGreen);
            HighlightText(rtbContent, "【紅色】", System.Drawing.Color.Red);
            HighlightText(rtbContent, "重新開始", System.Drawing.Color.Purple);
            HighlightText(rtbContent, "「清空目前場地」", System.Drawing.Color.DarkRed);
            HighlightText(rtbContent, "「解除選單鎖定」", System.Drawing.Color.Chocolate);
            HighlightText(rtbContent, "「重新選牌」", System.Drawing.Color.DeepSkyBlue);
            HighlightText(rtbContent, "「暫停遊戲」", System.Drawing.Color.Purple);
            HighlightText(rtbContent, "【空白鍵】", System.Drawing.Color.DodgerBlue);
            HighlightText(rtbContent, "「最少步數」", System.Drawing.Color.Goldenrod);
            HighlightText(rtbContent, "「最快時間」", System.Drawing.Color.Goldenrod);
            HighlightText(rtbContent, "⚙️【 玩法參數自由配 】", System.Drawing.Color.FromArgb(41, 128, 185));
            HighlightText(rtbContent, "⏳【 記憶倒數與啟動 】", System.Drawing.Color.FromArgb(41, 128, 185));
            HighlightText(rtbContent, "🎯【 翻牌消除與反饋 】", System.Drawing.Color.FromArgb(41, 128, 185));
            HighlightText(rtbContent, "🔄【 自由重選牌面機制 】", System.Drawing.Color.FromArgb(41, 128, 185));
            HighlightText(rtbContent, "⏸️【 貼心暫停防偷看 】", System.Drawing.Color.FromArgb(41, 128, 185));
            HighlightText(rtbContent, "🏆【 榮譽殿堂最高紀錄 】", System.Drawing.Color.FromArgb(41, 128, 185));

            Button btnBack = new Button();
            btnBack.Text = "了解！返回遊戲 ↩";
            btnBack.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
            btnBack.Size = new Size(300, 60);
            btnBack.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            btnBack.ForeColor = System.Drawing.Color.White;
            btnBack.Cursor = Cursors.Hand;
            btnBack.Dock = DockStyle.Bottom;
            btnBack.Click += (s, ev) => HideGameInfo();

            pnlInfoContainer.Controls.Add(rtbContent);
            pnlInfoContainer.Controls.Add(lblTitle);
            pnlInfoContainer.Controls.Add(btnBack);

            grpTable.Controls.Add(pnlInfoContainer);
            pnlInfoContainer.BringToFront();
        }

        private void HighlightText(RichTextBox box, string word, System.Drawing.Color color)
        {
            int pos = 0;
            while ((pos = box.Text.IndexOf(word, pos)) != -1)
            {
                box.Select(pos, word.Length);
                box.SelectionColor = color;
                box.SelectionFont = new Font(box.Font, FontStyle.Bold);
                pos += word.Length;
            }
            box.Select(0, 0);
        }

        private void HideGameInfo()
        {
            if (!isShowingInfo) return;
            isShowingInfo = false;

            if (pnlInfoContainer != null)
            {
                grpTable.Controls.Remove(pnlInfoContainer);
                pnlInfoContainer.Dispose();
                pnlInfoContainer = null;
            }

            foreach (var p in pics)
            {
                if ((int)p.Tag != -1)
                {
                    p.Visible = !isPaused;
                }
            }

            if (!btnStart.Enabled && !isPaused && pics.Count > 0)
            {
                totalPausedDuration += (DateTime.Now - infoStartTime);
                gameTimer.Start();
            }

            Application.DoEvents();
        }

        private void CreateAdaptiveLayout(int total)
        {
            int columns;
            if (total <= 16) columns = 4;
            else if (total <= 20) columns = 5;
            else if (total <= 30) columns = 6;
            else if (total <= 40) columns = 8;
            else columns = 13;

            int cardW = 150;
            int cardH = 210;
            int spacing = 15;

            int rows = (int)Math.Ceiling((double)total / columns);
            int totalGridWidth = columns * (cardW + spacing) - spacing;
            int totalGridHeight = rows * (cardH + spacing) - spacing;

            int startX = (grpTable.Width - totalGridWidth) / 2;
            int startY = (grpTable.Height - totalGridHeight) / 2;

            if (startX < 15) startX = 15;
            if (startY < 45) startY = 45;

            for (int i = 0; i < total; i++)
            {
                PictureBox p = new PictureBox();
                p.Size = new Size(cardW, cardH);
                p.Location = new Point(
                    startX + (i % columns) * (cardW + spacing),
                    startY + (i / columns) * (cardH + spacing)
                );

                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Image = imageCache["back"];
                p.BorderStyle = BorderStyle.FixedSingle;
                p.Cursor = Cursors.Hand;
                p.BackColor = System.Drawing.Color.White;
                p.Tag = i;
                p.Click += Card_Click;

                p.MouseEnter += (s, ev) => {
                    PictureBox pb = s as PictureBox;
                    if (pb != null && pb.Visible && pb.BackColor == System.Drawing.Color.White && !isChecking)
                        pb.BackColor = System.Drawing.Color.LightSkyBlue;
                };
                p.MouseLeave += (s, ev) => {
                    PictureBox pb = s as PictureBox;
                    if (pb != null && pb.BackColor == System.Drawing.Color.LightSkyBlue)
                        pb.BackColor = System.Drawing.Color.White;
                };

                pics.Add(p);
                grpTable.Controls.Add(p);
            }
        }

        private void Shuffle(int total)
        {
            cardValues = new int[total];
            for (int i = 0; i < total; i++) cardValues[i] = i / 2;
            Random rand = new Random();
            for (int i = 0; i < total; i++)
            {
                int target = rand.Next(total);
                int temp = cardValues[i];
                cardValues[i] = cardValues[target];
                cardValues[target] = temp;
            }
        }

        private async void Card_Click(object sender, EventArgs e)
        {
            if (isChecking) return;

            PictureBox clicked = sender as PictureBox;
            if (clicked == null || !clicked.Visible || clicked == firstCard) return;

            int idx = (int)clicked.Tag;
            clicked.Image = imageCache["pic" + (cardValues[idx] + 1)];

            clicked.BackColor = System.Drawing.Color.Orange;
            clicked.Padding = new Padding(5);
            clicked.Refresh();

            if (firstCard == null)
            {
                firstCard = clicked;
            }
            else
            {
                secondCard = clicked;
                isChecking = true;
                moveCount++;
                lblMoveCount.Text = $"步數: {moveCount}";

                if (cardValues[(int)firstCard.Tag] == cardValues[(int)secondCard.Tag])
                {
                    lblMsg.Text = "★ 配對成功！ ★";

                    firstCard.BackColor = secondCard.BackColor = System.Drawing.Color.LimeGreen;
                    firstCard.Padding = secondCard.Padding = new Padding(6);

                    soundSuccess.Position = TimeSpan.Zero;
                    soundSuccess.Play();

                    firstCard.Refresh();
                    secondCard.Refresh();

                    int originalWidth = firstCard.Width;
                    int originalHeight = firstCard.Height;
                    Point p1Start = firstCard.Location;
                    Point p2Start = secondCard.Location;

                    int steps = 3;
                    for (int step = 1; step <= steps; step++)
                    {
                        double scale = 1.0 - ((double)step / steps);
                        int newW = (int)(originalWidth * scale);
                        int newH = (int)(originalHeight * scale);

                        if (firstCard == null || firstCard.IsDisposed || secondCard == null || secondCard.IsDisposed)
                            return;

                        if (newW > 0 && newH > 0)
                        {
                            firstCard.Size = new Size(newW, newH);
                            firstCard.Location = new Point(p1Start.X + (originalWidth - newW) / 2, p1Start.Y + (originalHeight - newH) / 2);

                            secondCard.Size = new Size(newW, newH);
                            secondCard.Location = new Point(p2Start.X + (originalWidth - newW) / 2, p2Start.Y + (originalHeight - newH) / 2);

                            grpTable.Invalidate();
                            grpTable.Update();
                        }
                        await Task.Delay(10); // 釋放 UI 執行緒
                    }

                    if (firstCard != null && !firstCard.IsDisposed && secondCard != null && !secondCard.IsDisposed)
                    {
                        firstCard.Visible = secondCard.Visible = false;
                        await Task.Delay(150);
                        firstCard.Tag = -1;
                        secondCard.Tag = -1;
                    }
                }
                else
                {
                    lblMsg.Text = "配對失敗";

                    firstCard.BackColor = secondCard.BackColor = System.Drawing.Color.Red;
                    firstCard.Padding = secondCard.Padding = new Padding(6);

                    soundFail.Position = TimeSpan.Zero;
                    soundFail.Play();

                    await Task.Delay(800); // 讓玩家看清楚 0.8 秒

                    if (firstCard != null && !firstCard.IsDisposed && secondCard != null && !secondCard.IsDisposed)
                    {
                        firstCard.Image = secondCard.Image = imageCache["back"];
                        firstCard.BackColor = secondCard.BackColor = System.Drawing.Color.White;
                        firstCard.Padding = secondCard.Padding = new Padding(0);
                    }
                }

                firstCard = null;
                secondCard = null;

                Application.DoEvents();

                if (pics.Count > 0 && pics.All(p => p.IsDisposed || (int)p.Tag == -1))
                {
                    isChecking = true; 
                    gameTimer.Stop();
                    btnPause.Enabled = false;
                    if (isBgmLoaded) bgmPlayer.Stop();

                    TimeSpan finalTime = (DateTime.Now - startTime) - totalPausedDuration;
                    int currentSec = (int)finalTime.TotalSeconds;
                    int totalCards = int.Parse(cmbCardCount.SelectedItem?.ToString() ?? "16");

                    bool bMove = false, bTime = false;
                    if (moveCount < bestMove) { bestMove = moveCount; bMove = true; }
                    if (currentSec < bestTimeInSeconds) { bestTimeInSeconds = currentSec; bTime = true; }

                    if (bMove || bTime)
                    {
                        File.WriteAllLines($"highscore_{totalCards}.txt", new string[] { bestMove.ToString(), bestTimeInSeconds.ToString() });
                        LoadBestRecord(totalCards);
                    }

                    lblResultTitle.Text = "🏆 遊戲通關 🏆";
                    string summary = (bMove && bTime) ? "★ 完美突破！雙紀錄達成 ★" : (bMove || bTime) ? "\n★ 恭喜打破紀錄！ ★" : "通關成功！";
                    lblFinalStats.Text = $"{summary}\n\n步數：{moveCount}\n時間：{finalTime.Minutes:00}:{finalTime.Seconds:00}";

                    pnlResult.Location = new Point((this.ClientSize.Width - pnlResult.Width) / 2, (this.ClientSize.Height - pnlResult.Height) / 2);
                    pnlResult.Visible = true;
                    pnlResult.BringToFront();
                    btnCloseResult.Enabled = true;

                    btnStart.Enabled = true;
                    cmbCardCount.Enabled = true;
                    cmbSeconds.Enabled = true;

                    return; 
                }

                isChecking = false;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = (DateTime.Now - startTime) - totalPausedDuration;
            lblTime.Text = $"時間: {elapsed.Minutes:00}:{elapsed.Seconds:00}";
        }

        private void btnCloseResult_Click(object sender, EventArgs e) => pnlResult.Visible = false;

        private void btnClearRecord_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要刪除所有最高紀錄嗎？", "清除紀錄", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int totalCards = int.Parse(cmbCardCount.SelectedItem?.ToString() ?? "16");
                string fileName = $"highscore_{totalCards}.txt";

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                bestMove = int.MaxValue;
                bestTimeInSeconds = int.MaxValue;
                lblBestRecord.Text = $"最佳紀錄({totalCards}張): 尚未有紀錄";

                MessageBox.Show("紀錄已清除！");
            }
        }

        private void frmMemoryGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("確定要結束遊戲並退出嗎？", "結束確認",
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (isBgmLoaded) bgmPlayer.Close();
                soundSuccess.Close();
                soundFail.Close();
            }
        }

        private void cmbCardCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            int totalCards = int.Parse(cmbCardCount.SelectedItem?.ToString() ?? "16");
            LoadBestRecord(totalCards);

            if (totalCards <= 12) this.BackColor = System.Drawing.Color.LightGreen;
            else if (totalCards <= 24) this.BackColor = System.Drawing.Color.LightSkyBlue;
            else if (totalCards <= 40) this.BackColor = System.Drawing.Color.Thistle;
            else this.BackColor = System.Drawing.Color.LightCoral;
        }

        private void frmMemoryGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();

            if (e.KeyCode == Keys.Space)
            {
                if (btnPause.Enabled && !isShowingInfo && !pnlResult.Visible)
                {
                    btnPause_Click(sender, e);
                    e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }
    }
}