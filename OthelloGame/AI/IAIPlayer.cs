using System.Collections.Generic;
using OthelloGame.Models;

namespace OthelloGame.AI
{
    public interface IAIPlayer
    {
        /// <summary>
        /// Trả về nước đi tốt nhất cho AI
        /// </summary>
        (int row, int col) GetMove(Board board, PieceColor aiColor);

        /// <summary>
        /// Tên AI (hiển thị UI)
        /// </summary>
        string Name { get; }
    }
}
