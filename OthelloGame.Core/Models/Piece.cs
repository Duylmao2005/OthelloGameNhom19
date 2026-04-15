namespace OthelloGame.Models
{
    // Màu của quân cờ
    public enum PieceColor
    {
        Empty,  // ô trống
        Black,  // quân đen (đi trước)
        White   // quân trắng (đi sau)
    }

    // Dùng để lấy màu đối thủ nhanh hơn
    public static class PieceColorExtensions
    {
        public static PieceColor Opposite(this PieceColor color)
        {
            if (color == PieceColor.Black) return PieceColor.White;
            if (color == PieceColor.White) return PieceColor.Black;
            return PieceColor.Empty;
        }
    }
}