Cấu trúc project OthelloGame
Thư mục / file quan trọng
OthelloGame/Program.cs: entrypoint, init WinForms + load runtime settings, mở MainMenuForm.
OthelloGame/Forms/: UI WinForms
MainMenuForm: menu vào game (PvP, PvAI, AI vs AI, Continue, Settings, Hướng dẫn).
GameForm: màn chơi chính (vẽ bàn cờ bằng GDI+ trên pnlBoard, xử lý click, undo, hint, autosave/continue).
SettingForm: chọn độ khó AI (Easy/Normal/Hard) và lưu cấu hình runtime.
TutorialForm: màn hướng dẫn (hiển thị hình/asset).
OthelloGame/Controllers/Controller.cs: GameController bọc GameState + gọi AI (nếu có).
OthelloGame/Models/: core rule/game state
Board: luật đi, valid moves, lật quân, clone board.
GameState: current player, pass count, điều kiện game over, score/winner.
PieceColor: enum + Opposite().
OthelloGame/AI/: các bot
GreedyAI, HeuristicAI, MinimaxAI (alpha-beta).
IAIPlayer: interface.
OthelloGame/Properties/Resources.* + OthelloGame/Resources/: resource/ảnh phục vụ UI.
OthelloGame/Assets/Images/: ảnh (nếu dùng trực tiếp).
Luồng chạy (high-level)
Program.Main() → AppRuntime.Load() → Application.Run(new MainMenuForm()).
MainMenuForm mở GameForm theo mode.
GameForm dùng GameController để thao tác game; phần UI (hint/undo/save/load) có dùng reflection để set field/private-set property trong controller/models (để không phải sửa logic lõi).
Tính năng trong game (project OthelloGame)
PvP: 2 người chơi trên cùng máy.
Human vs AI: chơi với AI, có 3 mức độ khó (Easy/Normal/Hard).
AI vs AI: 2 AI tự đánh (có lựa chọn từ menu).
Hint:
Hiển thị các nước hợp lệ (vòng tròn nét đứt).
Có chế độ best-move (tính bằng MinimaxAI) khi bật nút Hint.
Undo:
PvP: undo 1 bước.
PvAI: undo 2 bước (để quay lại trước lượt AI).
Continue / Autosave:
Khi đóng GameForm giữa ván, game tự lưu để “Continue”.
Khi game over/đầu hàng/new game: save sẽ bị clear để không continue ván đã kết thúc.
Yêu cầu môi trường
Windows 10/11
.NET SDK 8.x (vì project target net8.0-windows)
IDE khuyến nghị: Visual Studio 2022 (hoặc dùng dotnet CLI)
Cách chạy (dev) – project OthelloGame
Cách 1: Visual Studio
Mở OthelloGame.sln
Set Startup Project là OthelloGame
Run (F5 / Ctrl+F5)
Cách 2: dotnet CLI (PowerShell)
Chạy từ root repo:

dotnet restore
dotnet build .\OthelloGame\OthelloGame.csproj -c Debug
dotnet run --project .\OthelloGame\OthelloGame.csproj
Hướng dẫn triển khai (deploy) game từ OthelloGame
A) Publish dạng folder (khuyến nghị để gửi người khác)
Từ root repo:

dotnet publish .\OthelloGame\OthelloGame.csproj -c Release -r win-x64 --self-contained false -o .\publish
File chạy sẽ nằm ở: publish/OthelloGame.exe
Máy người dùng cần cài .NET Desktop Runtime 8 (vì --self-contained false).
B) Publish self-contained (mang đi chạy không cần runtime)
dotnet publish .\OthelloGame\OthelloGame.csproj -c Release -r win-x64 --self-contained true -o .\publish-selfcontained
File chạy: publish-selfcontained/OthelloGame.exe
Thư mục publish sẽ lớn hơn nhưng tiện phân phối.
Nếu bạn muốn single-file (gọn hơn) có thể thử thêm:

-p:PublishSingleFile=true

Dữ liệu cấu hình & savegame (runtime)
App lưu file vào %AppData%\OthelloGame\:

settings.json: lưu độ khó AI (từ SettingForm qua AppRuntime).
savegame.json: lưu ván đang chơi để dùng Continue.
Gợi ý khi debug: nếu Continue/Setting “kỳ”, hãy thử xóa 2 file này.

