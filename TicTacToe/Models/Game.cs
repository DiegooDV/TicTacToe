using TicTacToe.Common;
using TicTacToe.Validators;
using TicTacToe.Interfaces;

namespace TicTacToe.Models
{
    public class Game
    {
        private readonly Board _board;
        private readonly IConsole _console;
        private int _totalMoves = 0;
        private bool _isGameOver = false;
        public Game(Board board, IConsole console)
        {
            _board = board;
            _console = console;
        }

        public void StartGame()
        {
            _board.PrintBoard();
            while (!_isGameOver)
            {
                PlayTurn();
            }
        }

        private void CheckDraw()
        {
            if (!_isGameOver && _totalMoves == _board.TotalCells)
            {
                _isGameOver = true;
                _console.WriteLine(Constants.DrawMessage);
            }
        }

        private void CheckWinner(int row, int column, int currentPlayer)
        {
            char playerSymbol = _board.GetCellValue(row - 1, column - 1);

            if(_board.IsRowWinner(row - 1, playerSymbol) ||
                   _board.IsColumnWinner(column - 1, playerSymbol) ||
                   _board.IsDiagonalWinner(playerSymbol) ||
                   _board.IsInverseDiagonalWinner(playerSymbol))
            {
                _isGameOver = true;
                _console.WriteLine(String.Format(Constants.PlayerWinMessageFormat, currentPlayer));
            }
        }

        private void PlayTurn()
        {
            var currentPlayer = (_totalMoves % 2) + 1;
            var playerSymbol = currentPlayer == 1 ? Constants.PlayerX : Constants.PlayerO;

            _console.WriteLine(String.Format(Constants.PlayerMovePromptFormat, currentPlayer));
            while (true)
            {
                var coordinatesInput = _console.ReadLine();
                var inputResult = GameValidator.ValidateInput(coordinatesInput, _board.BoardSize);
                if (!inputResult.IsValid)
                {
                    _console.WriteLine(inputResult.Message);
                    continue;
                }

                var coordinateParts = coordinatesInput.Split(',');
                int row = int.Parse(coordinateParts[0]);
                int column = int.Parse(coordinateParts[1]);

                var moveResult = _board.TryApplyMove(row, column, playerSymbol);
                if (!moveResult.Success)
                {
                    _console.WriteLine(moveResult.Message);
                    continue;
                }
                _totalMoves++;
                _board.PrintBoard();

                CheckWinner(row, column, currentPlayer);
                CheckDraw();
                break;
            }
        }
    }
}
