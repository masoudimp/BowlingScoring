using Xunit;
using BowlingScore_Serviec.Services;

namespace BowlingScoreAppTests
{
    public class BowlingScoreTest
    {
        private readonly ScoreService scoreService;

        public BowlingScoreTest()
        {
            scoreService = new ScoreService();
        }
       

        [Fact]
        public void LastFrameTwoRolls()
        {
            scoreService.Rolls = new int[] { 1, 3, 7, 3, 10, 1, 7, 5, 2, 5, 3, 8, 2, 8, 2, 10, 9, 0 };
            Assert.Equal(131, scoreService.CalculateScore());
        }

        [Fact]
        public void LastFrameThreeRolls()
        {
            scoreService.Rolls = new int[] { 10, 9, 1, 10, 10, 10, 10, 7, 3, 10, 9, 1, 10, 10, 10 };
            Assert.Equal(237, scoreService.CalculateScore());
        }

    }
}