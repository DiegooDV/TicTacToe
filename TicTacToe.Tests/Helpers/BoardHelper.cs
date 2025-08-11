using Moq;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Tests.Helpers
{
    public static class BoardHelper
    {
        public static Board CreateBoard(int size, Mock<IConsole>? mockConsole = null)
        {
            if (mockConsole == null)
            {
                mockConsole = new Mock<IConsole>();
            }
            return new Board(size, mockConsole.Object);
        }
    }
}
