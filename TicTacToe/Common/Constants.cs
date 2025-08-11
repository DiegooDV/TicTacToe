namespace TicTacToe.Common
{
    public static class Constants
    {
        // Game constants
        public const char PlayerX = 'X';
        public const char PlayerO = 'O';
        public const char EmptyCharValue = '\0';
        public const int DefaultBoardSize = 3;
        public const string EmptyCellDisplayPlaceholder = " . ";

        // Game prompts
        public const string PlayAgainPrompt = "Play again? Y/N";
        public const string PlayerMovePromptFormat = "Player {0}, enter your move, row and column in format 'x,y':";
        public const string EnterBoardSizePrompt = "Enter board size (e.g. 3 for 3x3):";

        // Game messages
        public const string WelcomeMessage = "Welcome to Tic Tac Toe!";
        public const string DrawMessage = "It's a draw!";
        public const string PlayerWinMessageFormat = "Player {0} wins!";

        //Game validation messages
        public const string InvalidBoardSizeMessage = "Invalid size. Please enter a board size greater than or equal to 3";
        public const string InvalidMoveFormatMessage = "Input must be in the format 'x,y'";
        public const string InvalidMoveNumberMessage = "Both x and y must be valid numbers";
        public const string InvalidMoveRangeMessageFormat = "Both x and y must be between 1 and {0}";
        public const string CellOccupiedMessage = "Cell is already occupied. Please choose another cell";
        public const string InputEmptyMessage = "Input cannot be empty";
    }
}
