using Moq;
using TicTacToe.Models;
using TicTacToe.Common;
using TicTacToe.Interfaces;
using TicTacToe.Tests.Helpers;

namespace TicTacToe.Tests.Models
{
    public class GameTests
    {
        [Fact]
        public void StartGame_WhenBoardIsFilledAndNoWinner_EndsWithDraw()
        {
            var mockConsole = new Mock<IConsole>();
            // Simulate moves for a 3x3 board with no winner
            var moves = new Queue<string>(new[]
            {
                "1,1", "1,2", "1,3",
                "2,1", "2,3", "2,2",
                "3,1", "3,3", "3,2"
            });
            mockConsole.Setup(c => c.ReadLine()).Returns(() => moves.Dequeue());
            mockConsole.Setup(c => c.WriteLine(It.IsAny<string>()));

            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize, mockConsole);
            var game = new Game(board, mockConsole.Object);
            game.StartGame();

            mockConsole.Verify(c => c.WriteLine(Constants.DrawMessage), Times.Once);
        }

        [Fact]
        public void StartGame_WhenPlayer1Wins_AnnouncesWinner()
        {
            var mockConsole = new Mock<IConsole>();

            // Simulate moves for Player 1 to win on first row
            var moves = new Queue<string>(new[]
            {
                "1,1", "2,1",
                "1,2", "2,2",
                "1,3"
            });
            mockConsole.Setup(c => c.ReadLine()).Returns(() => moves.Dequeue());
            mockConsole.Setup(c => c.WriteLine(It.IsAny<string>()));

            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize, mockConsole);
            var game = new Game(board, mockConsole.Object);
            game.StartGame();

            mockConsole.Verify(c => c.WriteLine(It.Is<string>(msg => msg.Equals(String.Format(Constants.PlayerWinMessageFormat, 1)))), Times.Once);
        }

        [Fact]
        public void StartGame_WhenPlayer2Wins_AnnouncesWinner()
        {
            var mockConsole = new Mock<IConsole>();

            // Simulate moves for Player 2 to win on first row
            var moves = new Queue<string>(new[]
            {
                "2,1", "1,1",
                "2,2", "1,2",
                "3,3", "1,3"
            });
            mockConsole.Setup(c => c.ReadLine()).Returns(() => moves.Dequeue());
            mockConsole.Setup(c => c.WriteLine(It.IsAny<string>()));

            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize, mockConsole);
            var game = new Game(board, mockConsole.Object);
            game.StartGame();

            mockConsole.Verify(c => c.WriteLine(It.Is<string>(msg => msg.Equals(String.Format(Constants.PlayerWinMessageFormat, 2)))), Times.Once);
        }

        [Fact]
        public void PlayTurn_WhenInputIsInvalid_ShowsErrorAndPromptsAgain()
        {
            var mockConsole = new Mock<IConsole>();
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("invalid") // Invalid format
                .Returns("1,1");    // Valid move

            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize, mockConsole);
            var game = new Game(board, mockConsole.Object);

            var method = typeof(Game).GetMethod("PlayTurn", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(game, null);

            mockConsole.Verify(c => c.WriteLine(Constants.InvalidMoveFormatMessage), Times.Once);
        }

        [Fact]
        public void PlayTurn_WhenCellIsOccupied_ShowsErrorAndPromptsAgain()
        {
            var mockConsole = new Mock<IConsole>();
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("1,1") // First move
                .Returns("1,1") // Try to move again to same cell
                .Returns("1,2"); // Valid move

            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize, mockConsole);
            var game = new Game(board, mockConsole.Object);

            // First move
            var method = typeof(Game).GetMethod("PlayTurn", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(game, null);

            // Second move (should show error and prompt again)
            method.Invoke(game, null);

            mockConsole.Verify(c => c.WriteLine(Constants.CellOccupiedMessage), Times.Once);
        }
    }
}

