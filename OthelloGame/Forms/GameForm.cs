using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using OthelloGame;
using OthelloGame.AI;
using OthelloGame.Core.Controllers;
using OthelloGame.Models;

namespace OthelloGame.Forms
{
    public partial class GameForm : Form
    {
        // =========================
        // Cấu hình hiển thị bàn cờ
        // =========================
        private const int BoardSize = Board.Size;
        private const int DotSize = 8;

        // =========================
        // Trạng thái UI
        // =========================
        private Rectangle _boardRect;          // Vùng vẽ bàn cờ trong pnlBoard
        private int _cellSize;                 // Kích thước 1 ô
        private List<(int row, int col)> _cachedValidMoves = new();
        private bool _showBestMoveHint;
        private (int row, int col)? _bestMoveHint;

        // =========================
        // Game / Controller
        // =========================
        private readonly GameController _controller;
        private readonly PieceColor _humanColor;
        private readonly PieceColor _aiColor;
        private bool _enableAI;
        private bool _aiVsAi;
        private AIDifficulty _difficulty;
        private readonly SavedGameData? _pendingLoad;

        // Lưu lịch sử để Undo (không sửa Models/Controllers → dùng snapshot + reflection)
        private readonly Stack<GameSnapshot> _history = new();

        // Cờ chống click khi AI đang tính
        private bool _isBusy;

        // -------------------------
        // Constructor mặc định: chạy được ngay từ Program.cs
        // -------------------------
        public GameForm()
            : this(enableAI: false, difficulty: OthelloGame.AppRuntime.Difficulty, humanColor: PieceColor.Black)
        {
        }

        public GameForm(SavedGameData saved)
            : this(enableAI: saved.EnableAI, difficulty: saved.Difficulty, humanColor: saved.HumanColor)
        {
            _aiVsAi = saved.AiVsAi;
            _pendingLoad = saved;
        }

        /// <summary>
        /// Constructor dành cho MainMenuForm truyền tham số (mode/difficulty/màu).
        /// Không bắt buộc MainMenu phải dùng ngay, nhưng GameForm vẫn sẵn sàng để wiring sau.
        /// </summary>
        public GameForm(bool enableAI, AIDifficulty difficulty, PieceColor humanColor)
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            _enableAI = enableAI;
            _difficulty = difficulty;
            _humanColor = humanColor;
            _aiColor = humanColor.Opposite();

            // Controller khởi tạo 1 lần; AI sẽ được (re)gán khi New game theo độ khó hiện tại
            _controller = new GameController(null);

            // Gắn sự kiện
            Load += GameForm_Load;
            FormClosing += GameForm_FormClosing;
            pnlBoard.Paint += PnlBoard_Paint;
            pnlBoard.Resize += (_, __) => pnlBoard.Invalidate();
            pnlBoard.MouseClick += PnlBoard_MouseClick;

            btnNewGame.Click += (_, __) => StartNewGame();
            btnSetting.Click += (_, __) => OpenSetting();
            btnSurrender.Click += (_, __) => Surrender();
            btnHint.Click += (_, __) => ToggleHint();
            btnUndo.Click += (_, __) => Undo();
            btnQuit.Click += (_, __) => Close();

            // Chống nháy cho pnlBoard
            EnableDoubleBuffer(pnlBoard);
        }

        // =========================
        // Public enums (để MainMenu truyền vào)
        // =========================
        public enum AIDifficulty
        {
            Easy,
            Normal,
            Hard
        }

        // =========================
        // Form lifecycle
        // =========================
        private void GameForm_Load(object? sender, EventArgs e)
        {
            ApplyModernButtonStyle(btnNewGame);
            ApplyModernButtonStyle(btnSetting);
            ApplyModernButtonStyle(btnSurrender);
            ApplyModernButtonStyle(btnHint);
            ApplyModernButtonStyle(btnUndo);
            ApplyModernButtonStyle(btnQuit);

            // Bo góc ngay cả khi resize
            Resize += (_, __) =>
            {
                RoundButton(btnNewGame);
                RoundButton(btnSetting);
                RoundButton(btnSurrender);
                RoundButton(btnHint);
                RoundButton(btnUndo);
                RoundButton(btnQuit);
                Invalidate(); // để vẽ lại 2 vòng tròn "Time"
            };

            RoundButton(btnNewGame);
            RoundButton(btnSetting);
            RoundButton(btnSurrender);
            RoundButton(btnHint);
            RoundButton(btnUndo);
            RoundButton(btnQuit);

            if (_pendingLoad != null)
                LoadFromSavedGame(_pendingLoad);
            else
                StartNewGame();
        }

        // =========================
        // Vẽ: form (2 hình tròn Time)
        // =========================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 2 vòng tròn "Time" giống ảnh mẫu (chỉ là UI placeholder)
            DrawTimeBadge(g, new Rectangle(18, 18, 78, 78), "Time");
            DrawTimeBadge(g, new Rectangle(ClientSize.Width - 96, ClientSize.Height - 96, 78, 78), "Time");
        }

        // =========================
        // Vẽ: bàn cờ (GDI+) trong pnlBoard
        // =========================
        private void PnlBoard_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Tính toán boardRect luôn nằm giữa pnlBoard và co giãn theo size
            var client = pnlBoard.ClientRectangle;
            int side = Math.Min(client.Width, client.Height) - 20; // trừ padding
            if (side <= 0) return;

            side = side / BoardSize * BoardSize; // đảm bảo chia hết để nét đẹp
            _cellSize = side / BoardSize;

            int startX = client.X + (client.Width - side) / 2;
            int startY = client.Y + (client.Height - side) / 2;
            _boardRect = new Rectangle(startX, startY, side, side);

            // 1) Nền xanh (không có asset board → dùng màu #228B22)
            using (var br = new SolidBrush(Color.FromArgb(0x22, 0x8B, 0x22)))
                g.FillRectangle(br, _boardRect);

            // 2) Lưới 8x8
            using (var pen = new Pen(Color.FromArgb(20, 20, 20), 1))
            {
                for (int i = 0; i <= BoardSize; i++)
                {
                    int x = _boardRect.Left + i * _cellSize;
                    int y = _boardRect.Top + i * _cellSize;
                    g.DrawLine(pen, x, _boardRect.Top, x, _boardRect.Bottom);
                    g.DrawLine(pen, _boardRect.Left, y, _boardRect.Right, y);
                }
            }

            // 3) 4 chấm marker tiêu chuẩn
            DrawMarkerDot(g, 2, 2);
            DrawMarkerDot(g, 2, 6);
            DrawMarkerDot(g, 6, 2);
            DrawMarkerDot(g, 6, 6);

            // 4) Quân cờ theo trạng thái Board (lấy từ controller qua reflection)
            var board = TryGetBoard();
            if (board != null)
            {
                DrawPieces(g, board);
            }

            // 5) Hint: vẽ vòng tròn rỗng nét đứt tại các ô hợp lệ
            DrawHints(g, _cachedValidMoves);

            // Best-move hint (Minimax)
            if (_showBestMoveHint && _bestMoveHint.HasValue)
                DrawBestMoveHint(g, _bestMoveHint.Value);
        }

        private void DrawMarkerDot(Graphics g, int row, int col)
        {
            int cx = _boardRect.Left + col * _cellSize;
            int cy = _boardRect.Top + row * _cellSize;
            int x = cx - DotSize / 2;
            int y = cy - DotSize / 2;

            using var b = new SolidBrush(Color.FromArgb(25, 25, 25));
            g.FillEllipse(b, x, y, DotSize, DotSize);
        }

        private void DrawPieces(Graphics g, Board board)
        {
            for (int r = 0; r < BoardSize; r++)
            {
                for (int c = 0; c < BoardSize; c++)
                {
                    var p = board.GetPieceAt(r, c);
                    if (p == PieceColor.Empty) continue;
                    DrawPiece3D(g, r, c, p);
                }
            }
        }

        private void DrawPiece3D(Graphics g, int row, int col, PieceColor color)
        {
            // Margin để quân không chạm lưới
            int margin = Math.Max(5, _cellSize / 12);
            var rect = new Rectangle(
                _boardRect.Left + col * _cellSize + margin,
                _boardRect.Top + row * _cellSize + margin,
                _cellSize - margin * 2,
                _cellSize - margin * 2
            );

            // Bóng nhẹ
            var shadowRect = rect;
            shadowRect.Offset(Math.Max(2, _cellSize / 25), Math.Max(2, _cellSize / 25));
            using (var shadow = new SolidBrush(Color.FromArgb(70, 0, 0, 0)))
                g.FillEllipse(shadow, shadowRect);

            // Gradient 3D
            Color baseColor = color == PieceColor.Black ? Color.FromArgb(30, 30, 30) : Color.FromArgb(245, 245, 245);
            Color highlight = color == PieceColor.Black ? Color.FromArgb(90, 90, 90) : Color.White;
            Color edge = color == PieceColor.Black ? Color.FromArgb(10, 10, 10) : Color.FromArgb(200, 200, 200);

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using var brush = new PathGradientBrush(path)
                {
                    CenterColor = highlight,
                    SurroundColors = new[] { baseColor }
                };
                g.FillEllipse(brush, rect);
            }

            using (var pen = new Pen(edge, Math.Max(1, _cellSize / 28)))
                g.DrawEllipse(pen, rect);

            // Đốm phản sáng nhỏ (giống ảnh)
            var glare = rect;
            glare.Width = glare.Width / 3;
            glare.Height = glare.Height / 3;
            glare.Offset(rect.Width / 6, rect.Height / 6);
            using var glareBrush = new SolidBrush(Color.FromArgb(color == PieceColor.Black ? 60 : 90, Color.White));
            g.FillEllipse(glareBrush, glare);
        }

        private void DrawHints(Graphics g, List<(int row, int col)> hints)
        {
            if (hints == null || hints.Count == 0) return;

            int margin = Math.Max(12, _cellSize / 4);
            using var pen = new Pen(Color.FromArgb(160, 0, 0, 0), Math.Max(2, _cellSize / 20f));
            pen.DashStyle = DashStyle.Dash;

            foreach (var (row, col) in hints)
            {
                var rect = new Rectangle(
                    _boardRect.Left + col * _cellSize + margin,
                    _boardRect.Top + row * _cellSize + margin,
                    _cellSize - margin * 2,
                    _cellSize - margin * 2
                );
                g.DrawEllipse(pen, rect);
            }
        }

        // =========================
        // Input: click lên bàn cờ
        // =========================
        private async void PnlBoard_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_isBusy) return;
            if (_aiVsAi) return;
            if (!_boardRect.Contains(e.Location)) return;

            int col = (e.X - _boardRect.Left) / _cellSize;
            int row = (e.Y - _boardRect.Top) / _cellSize;
            if (row < 0 || row >= BoardSize || col < 0 || col >= BoardSize) return;

            // Nếu có AI: chỉ cho người đi khi đúng lượt người
            if (_enableAI && _controller.GetCurrentPlayer() != _humanColor)
                return;

            // Lưu snapshot trước khi đi để Undo
            PushSnapshot();

            bool moved = _controller.HandlePlayerMove(row, col);
            if (!moved)
            {
                // Nước đi không hợp lệ → bỏ snapshot vừa push
                if (_history.Count > 0) _history.Pop();
                return;
            }

            AfterAnyMoveRefreshUI();

            // Nếu đến lượt AI → chạy async để UI không đơ
            if (_enableAI)
            {
                await MaybeDoAIMoveAsync();
            }
        }

        private async Task MaybeDoAIMoveAsync()
        {
            if (_controller.IsGameOver()) return;
            if (!_enableAI) return;
            if (!_aiVsAi && _controller.GetCurrentPlayer() != _aiColor) return;

            try
            {
                _isBusy = true;
                SetButtonsEnabled(false);

                // Lưu snapshot trước khi AI đi (để Undo quay lại trước nước AI)
                PushSnapshot();

                await Task.Delay(500); // (delay 1 giây)


                await Task.Run(() => _controller.HandleAIMove());

                AfterAnyMoveRefreshUI();
            }
            finally
            {
                _isBusy = false;
                SetButtonsEnabled(true);
            }

            if (_aiVsAi && !_controller.IsGameOver())
            {
                await Task.Delay(250);
                await MaybeDoAIMoveAsync();
            }
        }

        // =========================
        // Buttons
        // =========================
        private void StartNewGame()
        {
            _showBestMoveHint = false;
            _bestMoveHint = null;
            _history.Clear();
            _cachedValidMoves = new List<(int row, int col)>();

            // New game → ván cũ không còn ý nghĩa
            OthelloGame.SavedGameStore.Clear();

            // New game sẽ lấy difficulty mới nhất từ runtime (SettingForm đã lưu)
            _difficulty = OthelloGame.AppRuntime.Difficulty;
            ApplyAIToController();

            _controller.StartGame();
            AfterAnyMoveRefreshUI();

            // Nếu có AI và đến lượt AI (hoặc AI vs AI) → chạy ngay
            if (_enableAI && (_aiVsAi || _controller.GetCurrentPlayer() == _aiColor))
            {
                _ = MaybeDoAIMoveAsync();
            }
        }

        private void ApplyAIToController()
        {
            // Không sửa Controllers → set _aiPlayer bằng reflection
            try
            {
                var f = typeof(GameController).GetField("_aiPlayer", BindingFlags.Instance | BindingFlags.NonPublic);
                if (f == null) return;

                IAIPlayer? ai = null;
                if (_enableAI)
                    ai = CreateAI(_difficulty);

                f.SetValue(_controller, ai);
            }
            catch
            {
                // ignore
            }
        }

        private void ToggleHint()
        {
            _showBestMoveHint = !_showBestMoveHint;
            if (_showBestMoveHint)
                _bestMoveHint = ComputeBestMoveHint();
            else
                _bestMoveHint = null;
            pnlBoard.Invalidate();
        }

        private void Undo()
        {
            if (_isBusy) return;
            if (_history.Count == 0) return;

            // PvAI → undo 2 bước
            int steps = _enableAI ? 2 : 1;

            for (int i = 0; i < steps; i++)
            {
                if (_history.Count == 0) break;
                var snap = _history.Pop();
                RestoreSnapshot(snap);
            }

            AfterAnyMoveRefreshUI();
        }

        private void OpenSetting()
        {
            // SettingForm hiện chưa có logic lưu cấu hình,
            // nên chỉ mở form để bạn tiếp tục wiring sau.
            using var f = new SettingForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void Surrender()
        {
            if (_isBusy) return;

            var current = _controller.GetCurrentPlayer();
            var winner = current.Opposite();
            MessageBox.Show(
                this,
                $"Bạn đã đầu hàng. Người thắng: {(winner == PieceColor.Black ? "Đen" : "Trắng")}.",
                "Kết thúc",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Đầu hàng không phải "quit giữa chừng" → không lưu continue
            OthelloGame.SavedGameStore.Clear();
            Close();
        }

        // =========================
        // UI helpers
        // =========================
        private void AfterAnyMoveRefreshUI()
        {
            // Auto-pass nếu bên hiện tại hết nước đi (cuối game hay gặp)
            // Có thể xảy ra nhiều lần liên tiếp, nhưng tối đa 2 lần là game over.
            while (_controller.EnsureCurrentPlayerCanMove())
            {
                // no-op: chỉ cần cập nhật lại sau khi pass
            }

            UpdateScoreLabels();
            UpdateTurnLabel();

            // Gợi ý nước hợp lệ luôn có sẵn mỗi lượt
            _cachedValidMoves = _controller.GetValidMoves();

            if (_showBestMoveHint)
                _bestMoveHint = ComputeBestMoveHint();

            pnlBoard.Invalidate();

            if (_controller.IsGameOver())
            {
                var winner = _controller.GetWinner();
                string msg = winner == PieceColor.Empty
                    ? "Hòa!"
                    : $"Người thắng: {(winner == PieceColor.Black ? "Đen" : "Trắng")}";

                MessageBox.Show(this, msg, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Game kết thúc → clear save để Continue không quay lại ván đã xong
                OthelloGame.SavedGameStore.Clear();
            }
            else
            {
                // Nếu sau auto-pass mà đến lượt AI thì cho AI đi tiếp
                if (_enableAI && (_aiVsAi || _controller.GetCurrentPlayer() == _aiColor))
                    _ = MaybeDoAIMoveAsync();
            }
        }

        private void UpdateScoreLabels()
        {
            var (black, white) = _controller.GetScores();

            // Player 1/2: giữ đúng text ảnh mẫu; điểm sẽ gắn theo màu thực tế (đen/trắng)
            lblScore1.Text = _humanColor == PieceColor.Black ? $"Score: {black}" : $"Score: {white}";
            lblScore2.Text = _humanColor == PieceColor.Black ? $"Score: {white}" : $"Score: {black}";
        }

        private void UpdateTurnLabel()
        {
            var p = _controller.GetCurrentPlayer();
            lblTurn.Text = p == PieceColor.Black ? "Lượt: Đen" : "Lượt: Trắng";
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnNewGame.Enabled = enabled;
            btnSetting.Enabled = enabled;
            btnSurrender.Enabled = enabled;
            btnHint.Enabled = enabled;
            btnUndo.Enabled = enabled;
            btnQuit.Enabled = enabled;
        }

        public static GameForm CreateAIVsAI(AIDifficulty difficulty)
        {
            var f = new GameForm(enableAI: true, difficulty: difficulty, humanColor: PieceColor.Empty)
            {
                _aiVsAi = true
            };
            return f;
        }

        private void GameForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                if (_controller.IsGameOver())
                {
                    OthelloGame.SavedGameStore.Clear();
                    return;
                }

                SaveCurrentGame();
            }
            catch
            {
                // ignore
            }
        }

        private void SaveCurrentGame()
        {
            var gs = GetGameStateUnsafe();
            if (gs == null) return;

            var data = OthelloGame.SavedGameStore.FromGameState(
                gs.Board,
                gs.CurrentPlayer,
                gs.PassCount,
                enableAI: _enableAI,
                aiVsAi: _aiVsAi,
                difficulty: _difficulty,
                humanColor: _humanColor
            );

            OthelloGame.SavedGameStore.Save(data);
        }

        private void LoadFromSavedGame(SavedGameData saved)
        {
            _showBestMoveHint = false;
            _bestMoveHint = null;
            _history.Clear();

            _enableAI = saved.EnableAI;
            _aiVsAi = saved.AiVsAi;
            _difficulty = saved.Difficulty;

            ApplyAIToController();

            var gs = new GameState();
            SetAutoProperty(gs, nameof(GameState.Board), OthelloGame.SavedGameStore.ToBoard(saved));
            SetAutoProperty(gs, nameof(GameState.CurrentPlayer), saved.CurrentPlayer);
            SetAutoProperty(gs, nameof(GameState.PassCount), saved.PassCount);

            // gán _gameState trong controller bằng reflection
            var field = typeof(GameController).GetField("_gameState", BindingFlags.Instance | BindingFlags.NonPublic);
            field?.SetValue(_controller, gs);

            AfterAnyMoveRefreshUI();

            if (_enableAI && (_aiVsAi || _controller.GetCurrentPlayer() == _aiColor))
                _ = MaybeDoAIMoveAsync();
        }

        private static void SetAutoProperty(object target, string propertyName, object value)
        {
            var prop = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
            if (prop == null) return;

            var setter = prop.GetSetMethod(true);
            if (setter == null) return;
            setter.Invoke(target, new[] { value });
        }

        private static void DrawTimeBadge(Graphics g, Rectangle rect, string text)
        {
            using var pen = new Pen(Color.FromArgb(40, 40, 40), 2f);
            using var brush = new SolidBrush(Color.White);
            g.FillEllipse(brush, rect);
            g.DrawEllipse(pen, rect);

            using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            using var font = new Font("Segoe UI", 10F, FontStyle.Bold);
            using var tb = new SolidBrush(Color.Black);
            g.DrawString(text, font, tb, rect, sf);
        }

        // =========================
        // Style: bo góc + flat
        // =========================
        private static void ApplyModernButtonStyle(Button btn)
        {
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 40);
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.Cursor = Cursors.Hand;
        }

        private static void RoundButton(Button btn)
        {
            int radius = Math.Min(22, Math.Max(16, btn.Height / 3));
            using var path = RoundedRect(new Rectangle(0, 0, btn.Width, btn.Height), radius);
            btn.Region = new Region(path);
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Top, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static void EnableDoubleBuffer(Control control)
        {
            // Panel không public DoubleBuffered → bật bằng reflection
            var prop = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            prop?.SetValue(control, true, null);
        }

        // =========================
        // AI factory
        // =========================
        private static IAIPlayer CreateAI(AIDifficulty difficulty)
        {
            return difficulty switch
            {
                AIDifficulty.Easy => new GreedyAI(),
                AIDifficulty.Normal => new HeuristicAI(),
                AIDifficulty.Hard => new MinimaxAI(depth: 5),
                _ => new HeuristicAI()
            };
        }

        private (int row, int col)? ComputeBestMoveHint()
        {
            var board = TryGetBoard();
            if (board == null) return null;

            var current = _controller.GetCurrentPlayer();

            // Nếu là PvAI thì hint chỉ dành cho người chơi
            if (_enableAI && !_aiVsAi && current != _humanColor) return null;
            if (_aiVsAi) return null;

            int depth = _difficulty switch
            {
                AIDifficulty.Easy => 2,
                AIDifficulty.Normal => 3,
                AIDifficulty.Hard => 5,
                _ => 3
            };

            var mm = new MinimaxAI(depth);
            var move = mm.GetMove(board, current);
            if (move.row < 0 || move.col < 0) return null;
            return move;
        }

        private void DrawBestMoveHint(Graphics g, (int row, int col) move)
        {
            int margin = Math.Max(10, _cellSize / 5);
            using var pen = new Pen(Color.FromArgb(230, 0, 255, 0), Math.Max(3, _cellSize / 14f));
            pen.DashStyle = DashStyle.Solid;

            var rect = new Rectangle(
                _boardRect.Left + move.col * _cellSize + margin,
                _boardRect.Top + move.row * _cellSize + margin,
                _cellSize - margin * 2,
                _cellSize - margin * 2
            );
            g.DrawEllipse(pen, rect);
        }

        // =========================
        // Reflection bridge (KHÔNG sửa Controllers/Models)
        // =========================
        private Board? TryGetBoard()
        {
            try
            {
                var gs = GetGameStateUnsafe();
                return gs?.Board;
            }
            catch
            {
                return null;
            }
        }

        private GameState? GetGameStateUnsafe()
        {
            var field = typeof(GameController).GetField("_gameState", BindingFlags.Instance | BindingFlags.NonPublic);
            return field?.GetValue(_controller) as GameState;
        }

        private void PushSnapshot()
        {
            var gs = GetGameStateUnsafe();
            if (gs == null) return;

            _history.Push(GameSnapshot.From(gs));
        }

        private void RestoreSnapshot(GameSnapshot snapshot)
        {
            var gs = GetGameStateUnsafe();
            if (gs == null) return;

            snapshot.ApplyTo(gs);
        }

        private sealed class GameSnapshot
        {
            public Board Board { get; init; } = new Board();
            public PieceColor CurrentPlayer { get; init; }
            public int PassCount { get; init; }

            public static GameSnapshot From(GameState state)
            {
                return new GameSnapshot
                {
                    Board = state.Board.Clone(),
                    CurrentPlayer = state.CurrentPlayer,
                    PassCount = state.PassCount
                };
            }

            public void ApplyTo(GameState state)
            {
                // Board có private set nên gán bằng reflection
                SetAutoProperty(state, nameof(GameState.Board), Board.Clone());
                SetAutoProperty(state, nameof(GameState.CurrentPlayer), CurrentPlayer);
                SetAutoProperty(state, nameof(GameState.PassCount), PassCount);
            }

            private static void SetAutoProperty(object target, string propertyName, object value)
            {
                var prop = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                if (prop == null) return;

                // private set → cần lấy setter non-public
                var setter = prop.GetSetMethod(true);
                if (setter == null) return;
                setter.Invoke(target, new[] { value });
            }
        }
    }
}