using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Common;
using TicTacToe.Interfaces;
using TicTacToe.Models;

namespace TicTacToe.Tests.Models
{
    public class GameSetupTests
    {
        [Fact]
        public void GetBoardSize_WhenInputIsValidOnFirstTry_ReturnsValidSize()
        {
            var mockConsole = new Mock<IConsole>();
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("3"); // Valid input

            int result = GameSetup.GetBoardSize(mockConsole.Object);

            Assert.Equal(3, result);
            mockConsole.Verify(c => c.WriteLine(Constants.EnterBoardSizePrompt), Times.Once);
        }

        [Fact]
        public void GetBoardSize_WhenInputIsInvalid_PromptsUntilValid()
        {
            var mockConsole = new Mock<IConsole>();
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("abc")   // Invalid
                .Returns("0")     // Invalid
                .Returns("4");    // Valid

            int result = GameSetup.GetBoardSize(mockConsole.Object);

            Assert.Equal(4, result);
            mockConsole.Verify(c => c.WriteLine(Constants.EnterBoardSizePrompt), Times.Exactly(3));
            mockConsole.Verify(c => c.WriteLine(It.Is<string>(msg => !string.IsNullOrEmpty(msg))), Times.AtLeast(2));
        }
    }
}
