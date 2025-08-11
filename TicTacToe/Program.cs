using TicTacToe.Common;
using TicTacToe.Models;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _console = new SystemConsole();
            _console.WriteLine(Constants.WelcomeMessage);
            do
            {
                var boardSize = GameSetup.GetBoardSize(_console);
                var board = new Board(boardSize, _console);
                Game game = new Game(board, _console);
                game.StartGame();

                _console.WriteLine(Constants.PlayAgainPrompt);
                var key = _console.ReadKey(true).Key;
                if (key != ConsoleKey.Y)
                    break;

                _console.Clear();
            }
            while (true);
        }
    }
}
