using TicTacToe.Common;
using TicTacToe.Validators;
using TicTacToe.Interfaces;

namespace TicTacToe.Models
{
    public static class GameSetup
    {
        public static int GetBoardSize(IConsole console)
        {
            while (true)
            {
                console.WriteLine(Constants.EnterBoardSizePrompt);
                var boardSizeInput = console.ReadLine();
                var boardValidation = GameValidator.ValidateBoardSize(boardSizeInput);
                if (boardValidation.IsValid)
                {
                    return int.Parse(boardSizeInput);
                }
                console.WriteLine(boardValidation.Message);
            }
        }
    }
}
