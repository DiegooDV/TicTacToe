namespace TicTacToe.Models
{
    public class MoveResult
    {
        public bool Success { get; }
        public string Message { get; }

        public MoveResult(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
