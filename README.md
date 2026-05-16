# 🃏 翻牌記憶對對碰 (Memory Match Game)

本遊戲的核心玩法為經典的記憶消除。玩家在開局時可自由設定卡片數量與預覽記憶時間，在時限過後卡片會覆蓋，玩家必須憑藉記憶翻開兩張圖案相同的卡片進行消除，直到場上卡片全數清空為止。

## 專案功能

- **自訂難度與自動排版**：可以選 4 到 52 張牌，不管選幾張，畫面上的卡片都會自動計算格子大小並置中，不會跑版。
- **畫面優化（防閃爍）**：有用 DoubleBuffered 解決 WinForms 動態產生大量圖片時會一直閃爍的問題。
- **防作弊暫停機制**：按【空白鍵】或點按鈕可以暫停計時，暫停的時候卡片會全部蓋起來，防止玩家邊暫停邊偷記牌。
- **背景音樂與音效控制**：用 WPF MediaPlayer 來播背景音樂、成功和失敗的音效，拉動底下的拉桿可以即時調音量。
- **紀錄排行榜**：過關時如果打破該難度的「最少步數」或「最快時間」，系統會把新紀錄存到電腦裡的文字檔。

## 遊戲畫面截圖

1. 初始畫面

<img width="2879" height="1799" alt="image" src="https://github.com/user-attachments/assets/cebc02fa-a16b-4a7f-b6f1-2aadbe64ae27" />

2. 開始遊戲與記牌倒數

<img width="2879" height="1799" alt="image" src="https://github.com/user-attachments/assets/763e7559-cd7b-4138-8229-4b4cfd825798" />

3. 遊戲進行中（翻牌比對，配對失敗和配對成功）

<img width="2879" height="1799" alt="image" src="https://github.com/user-attachments/assets/c7a785d8-5c08-41e5-b9b2-365a31f82adc" />

<img width="2879" height="1799" alt="image" src="https://github.com/user-attachments/assets/5ebd72f6-ad02-43b7-b366-fe942e1ed287" />

4. 暫停畫面（卡片隱藏）

<img width="2879" height="1799" alt="image" src="https://github.com/user-attachments/assets/7e1c8bee-e212-436f-9e7c-dbb276d8d0d3" />

5. 通關結算

<img width="2879" height="1799" alt="image" src="https://github.com/user-attachments/assets/9391903d-ed3c-498f-8821-7c46a468d4ff" />
