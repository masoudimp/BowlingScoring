
using BowlingScore_Serviec.Helpers;
using BowlingScore_Serviec.ViewModelsOrModels;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScore_Serviec.Services
{
    internal class GameService
    {
        private bool isFirstRollZero = false;

        public void TotalRollCount(ref int rollCount, int currentScore)
        {

            if (currentScore.IsStrike() && rollCount < 19 && !isFirstRollZero/*previousRolls.Count != 0*/)
                rollCount += 2;
            else
                rollCount++;

            isFirstRollZero = currentScore == 0;
        }

        public void FrameCount(ref int frame, int rollCount)
        {
            if (rollCount % 2 == 0 && frame != 10)
                frame++;
        }

        public void FrameRollCount(ref int rollCount, ref int frameNumber)
        {
            if (rollCount == 0)
                rollCount = 1;   // It means the first roll is always roll number 1
            else
                rollCount = (frameNumber == 10 && rollCount == 2) ? 3 : (rollCount == 2) ? 1 : (frameNumber == 10 && rollCount == 2) ? 3 : 2;
        }

        public void AddFrameDetails(ref List<Frame> framesList, int frameNumber, int totalScore = 0, int? score = 0)
        {
            bool isTotalScoreChanged = false;
            var lastFrame = framesList.Last();
            int previousScore = framesList.Where(_ => _.Score != 0).Select(_ => _.Score).LastOrDefault();
            isTotalScoreChanged = previousScore != totalScore;


            if (isTotalScoreChanged)
            {
                var lastNotScoreCalculated = framesList.Where(_ => _.Score == 0 && !_.IsNoScore).FirstOrDefault();

                if (lastNotScoreCalculated == null)
                {
                    framesList.Last().Score = totalScore;
                    return;
                }

                lastNotScoreCalculated.Score = totalScore;
                return;
            }

            if (lastFrame.FrameNumber == frameNumber) //Only Roll Should be added to the current Frame
            {
                if (lastFrame.Rolls.Count < 2 || frameNumber == 10)
                {
                    lastFrame.Rolls.Add(new Roll { RollNumber = lastFrame.Rolls.Count + 1, Score = score.Value });
                    var lastRoll = lastFrame.Rolls.First().Score;
                    lastFrame.IsSpare = lastRoll + score == 10;
                    lastFrame.IsNoScore = lastRoll + score == 0;
                }
            }
            else // A new frame will be added
            {
                framesList.Add(new Frame { FrameNumber = frameNumber, IsStrike = (score == 10), Rolls = new List<Roll>() { new Roll { RollNumber = 1, Score = score.Value } } });
            }


        }

    }
}
