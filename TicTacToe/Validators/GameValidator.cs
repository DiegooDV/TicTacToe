using TicTacToe.Common;
using TicTacToe.Models;

namespace TicTacToe.Validators
{
    public class GameValidator
    {
        public static ValidationResult ValidateInput(string input, int boardSize)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult(false, Constants.InputEmptyMessage);

            var inputParts = input.Split(',');
            if (inputParts.Length != 2)
                return new ValidationResult(false, Constants.InvalidMoveFormatMessage);

            if (!int.TryParse(inputParts[0], out int row) || !int.TryParse(inputParts[1], out int column))
                return new ValidationResult(false, Constants.InvalidMoveNumberMessage);

            return new ValidationResult(true);
        }

        public static ValidationResult ValidateMove(int row, int column, Board board)
        {
            if (row < 1 || row > board.BoardSize || column < 1 || column > board.BoardSize)
                return new ValidationResult(false, String.Format(Constants.InvalidMoveRangeMessageFormat, board.BoardSize));

            if (board.GetCellValue(row - 1, column -1) != Constants.EmptyCharValue)
                return new ValidationResult(false, Constants.CellOccupiedMessage);

            return new ValidationResult(true);
        }

        public static ValidationResult ValidateSpaceAvailable(char cellValue)
        {
            if (cellValue != Constants.EmptyCharValue)
                return new ValidationResult(false, Constants.CellOccupiedMessage);

            return new ValidationResult(true);
        }


        public static ValidationResult ValidateBoardSize(string boardSizeInput)
        {
            if (!int.TryParse(boardSizeInput, out int boardSize) || boardSize < Constants.DefaultBoardSize)
                return new ValidationResult(false, Constants.InvalidBoardSizeMessage);
            return new ValidationResult(true);
        }
    }
}
