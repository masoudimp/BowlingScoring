using Xunit;
using BowlingScore_Serviec.Services;

namespace BowlingScoreAppTests
{
    public class BowlingScoreTest
    {
        private readonly ScoreService _scoreService;

        public BowlingScoreTest()
        {
            _scoreService = new ScoreService();
        }
       

        [Fact]
        public void LastFrameTwoRolls()
        {
            _scoreService.Rolls = new int[] { 1, 3, 7, 3, 10, 1, 7, 5, 2, 5, 3, 8, 2, 8, 2, 10, 9, 0 };
            Assert.Equal(131, _scoreService.CalculateScore());
        }

        [Fact]
        public void LastFrameThreeRolls()
        {
            _scoreService.Rolls = new int[] { 10, 9, 1, 10, 10, 10, 10, 7, 3, 10, 9, 1, 10, 10, 10 };
            Assert.Equal(237, _scoreService.CalculateScore());
        }

    }
}