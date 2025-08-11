using TicTacToe.Common;
using TicTacToe.Validators;
using TicTacToe.Interfaces;

namespace TicTacToe.Models
{
    public class Board
    {
        private readonly IConsole _console;
        private char[,] Grid { get; set; }
        public int BoardSize => Grid.GetLength(0);
        public int TotalCells => Grid.Length;

        public Board(int boardSize, IConsole console)
        {
            Grid = new char[boardSize, boardSize];
            _console = console;

        }

        public char GetCellValue(int row, int column) => Grid[row, column];

        public void PrintBoard()
        {
            _console.WriteLine(String.Empty);
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Grid[i, j] == Constants.EmptyCharValue)
                    {
                        _console.Write(Constants.EmptyCellDisplayPlaceholder);
                    }
                    else
                    {
                        _console.Write($" {Grid[i, j].ToString()} ");
                    }
                }
                _console.WriteLine(String.Empty);
            }
            _console.WriteLine(String.Empty);
        }

        public MoveResult TryApplyMove(int row, int column, char playerSymbol)
        {
            var validation = GameValidator.ValidateMove(row, column, this);
            if (!validation.IsValid)
                return new MoveResult(false, validation.Message);

            Grid[row - 1, column - 1] = playerSymbol;
            return new MoveResult(true);
        }

        public bool IsRowWinner(int row, char symbol)
        {
            for (int c = 0; c < BoardSize; c++)
                if (Grid[row, c] != symbol) return false;
            return true;
        }

        public bool IsColumnWinner(int column, char symbol)
        {
            for (int r = 0; r < BoardSize; r++)
                if (Grid[r, column] != symbol) return false;
            return true;
        }

        public bool IsDiagonalWinner(char symbol)
        {
            for (int i = 0; i < BoardSize; i++)
                if (Grid[i, i] != symbol) return false;
            return true;
        }

        public bool IsInverseDiagonalWinner(char symbol)
        {
            for (int i = 0; i < BoardSize; i++)
                if (Grid[BoardSize - i - 1, i] != symbol) return false;
            return true;
        }
    }
}
