using TicTacToe.Validators;
using TicTacToe.Common;

namespace TicTacToe.Tests.Helpers
{
    public class GameValidatorTests
    {
        [Theory]
        [InlineData("", false, Constants.InputEmptyMessage)]
        [InlineData("1", false, Constants.InvalidMoveFormatMessage)]
        [InlineData("a,b", false, Constants.InvalidMoveNumberMessage)]
        [InlineData("1,1", true, "")]
        [InlineData("3,3", true, "")]
        public void ValidateInput_WithVariousInputs_ReturnsExpectedResult(string input, bool expectedValid, string expectedMessage)
        {
            int boardSize = Constants.DefaultBoardSize;
            var result = GameValidator.ValidateInput(input, boardSize);

            Assert.Equal(expectedValid, result.IsValid);
            if (!result.IsValid && !string.IsNullOrEmpty(expectedMessage))
                Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void ValidateMove_WhenMoveIsOutOfBounds_ReturnsInvalid()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            var result = GameValidator.ValidateMove(0, 1, board);
            Assert.False(result.IsValid);
            Assert.Equal("Both x and y must be between 1 and 3", result.Message);
        }

        [Fact]
        public void ValidateMove_WhenCellIsOccupied_ReturnsInvalid()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            board.TryApplyMove(1, 1, 'X');
            var result = GameValidator.ValidateMove(1, 1, board);
            Assert.False(result.IsValid);
            Assert.Equal(Constants.CellOccupiedMessage, result.Message);
        }

        [Fact]
        public void ValidateMove_WhenCellIsEmptyAndInBounds_ReturnsValid()
        {
            var board = BoardHelper.CreateBoard(Constants.DefaultBoardSize);
            var result = GameValidator.ValidateMove(2, 2, board);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateSpaceAvailable_WhenCellIsOccupied_ReturnsInvalid()
        {
            var result = GameValidator.ValidateSpaceAvailable('X');
            Assert.False(result.IsValid);
            Assert.Equal(Constants.CellOccupiedMessage, result.Message);
        }

        [Fact]
        public void ValidateSpaceAvailable_WhenCellIsEmpty_ReturnsValid()
        {
            var result = GameValidator.ValidateSpaceAvailable(Constants.EmptyCharValue);
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("2", false)]
        [InlineData("3", true)]
        [InlineData("4", true)]
        [InlineData("5", true)]
        [InlineData("abc", false)]
        [InlineData("0", false)]
        public void ValidateBoardSize_WithVariousInputs_ReturnsExpectedResult(string input, bool expectedValid)
        {
            var result = GameValidator.ValidateBoardSize(input);
            Assert.Equal(expectedValid, result.IsValid);
        }
    }
}
