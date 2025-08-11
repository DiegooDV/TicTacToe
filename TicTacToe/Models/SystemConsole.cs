using TicTacToe.Interfaces;

namespace TicTacToe.Models
{
    public class SystemConsole : IConsole
    {
        public void Write(string message) => Console.Write(message);
        public void WriteLine(string message) => Console.WriteLine(message);
        public string ReadLine() => Console.ReadLine();
        public ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);
        public void Clear() => Console.Clear();
    }
}
