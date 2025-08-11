namespace TicTacToe.Interfaces
{
    public interface IConsole
    {
        void Write(string message);
        void WriteLine(string message);
        string ReadLine();
        ConsoleKeyInfo ReadKey(bool intercept);
        void Clear();
    }
}
