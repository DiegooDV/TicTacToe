using TicTacToe.Tests.Helpers;
using TicTacToe.Common;

namespace TicTacToe.Tests.Models
{
    public class BoardTests
    {

        [Fact]
        public void TryApplyMove_WhenMoveIsValid_ReturnsSuccess()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            var result = board.TryApplyMove(1, 1, 'X');
            Assert.True(result.Success);
            Assert.Equal('X', board.GetCellValue(0, 0));
        }

        [Fact]
        public void TryApplyMove_WhenCellIsOccupied_ReturnsFailure()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            board.TryApplyMove(1, 1, 'X');
            var result = board.TryApplyMove(1, 1, 'O');
            Assert.False(result.Success);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public void TryApplyMove_WhenMoveIsOutOfRange_ReturnsFailure()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            var result = board.TryApplyMove(0, 4, 'X');
            Assert.False(result.Success);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public void IsRowWinner_WhenRowHasSameSymbol_ReturnsTrue()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            for (int col = 1; col <= Constants.DefaultBoardSize; col++)
                board.TryApplyMove(2, col, Constants.PlayerX);
            Assert.True(board.IsRowWinner(1, Constants.PlayerX));
        }

        [Fact]
        public void IsColumnWinner_WhenColumnHasSameSymbol_ReturnsTrue()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            for (int row = 1; row <= Constants.DefaultBoardSize; row++)
                board.TryApplyMove(row, 2, Constants.PlayerX);
            Assert.True(board.IsColumnWinner(1, Constants.PlayerX));
        }

        [Fact]
        public void IsDiagonalWinner_WhenDiagonalHasSameSymbol_ReturnsTrue()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            for (int i = 1; i <= Constants.DefaultBoardSize; i++)
                board.TryApplyMove(i, i, Constants.PlayerX);
            Assert.True(board.IsDiagonalWinner(Constants.PlayerX));
        }

        [Fact]
        public void IsInverseDiagonalWinner_WhenInverseDiagonalHasSameSymbol_ReturnsTrue()
        {
            var boardSize = Constants.DefaultBoardSize;
            var board = BoardHelper.CreateBoard(boardSize);
            for (int i = 0; i < boardSize; i++)
                board.TryApplyMove(i + 1, boardSize - i, Constants.PlayerX);
            Assert.True(board.IsInverseDiagonalWinner(Constants.PlayerX));
        }

        [Fact]
        public void IsRowWinner_WhenRowHasDifferentSymbols_ReturnsFalse()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            board.TryApplyMove(1, 1, 'X');
            board.TryApplyMove(1, 2, 'O');
            board.TryApplyMove(1, 3, 'X');
            Assert.False(board.IsRowWinner(0, 'X'));
        }
    }
}
