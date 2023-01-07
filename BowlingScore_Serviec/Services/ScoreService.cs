using BowlingScore_Serviec.Helpers;
using System.Collections.Generic;
using System.Linq;
using BowlingScore_Serviec.Services;
using BowlingScore_Serviec.ViewModelsOrModels;

namespace BowlingScore_Serviec.Services
{
    public class ScoreService
    {
        private readonly GameService _gameService;

        public ScoreService()
        {
            frames.Add(new Frame { FrameNumber = 1, Rolls = new List<Roll>() });
            _gameService = new GameService();
        }

        internal List<int> lastFrameScores = new List<int>();
        internal List<int> previousRolls = new List<int>();
        internal List<Frame> frames = new List<Frame>();
        internal int frameNumber = 0; //FRAME
        internal int[] rolls = new int[21];
        internal int totalRollCount = 0; //ROLL COUNT
        internal int rollCount = 0; //ROLL COUNT
        internal int currentScore = 0;
        internal bool isSpareWait = false;
        internal bool isStrikeWait = false;
        internal int roll = 1;

        public int[] Rolls // Encapsulation
        {
            get { return rolls; }
            set { rolls = value; }
        }

        //Live Calculator
        public List<Frame> Roll(int score)
        {
            _gameService.FrameCount(ref frameNumber, totalRollCount);
            _gameService.TotalRollCount(ref totalRollCount, score); //Refrence variable
            _gameService.FrameRollCount(ref rollCount, ref frameNumber);

            _gameService.AddFrameDetails(ref frames, frameNumber, currentScore, score);

            if (rollCount >= 20)
            {
                lastFrameScores.Add(score);
            }

            if (rollCount == 22)
            {
                if (lastFrameScores.Count > 3)
                {
                    previousRolls.RemoveAt(0);
                }
                currentScore += lastFrameScores.Sum();
                return frames; // SHOULD BE HANDELED
            }



            if (previousRolls.Any())
            {
                if (!isSpareWait)
                    isSpareWait = score.IsSpare(previousRolls.Last());
            }

            previousRolls.Add(score);

            if (previousRolls.Count > 1)
            {

                // Strike
                if (isStrikeWait)
                {
                    if (previousRolls.Count == 3)
                    {

                        currentScore += previousRolls.Sum();
                        _gameService.AddFrameDetails(ref frames, frameNumber, currentScore);
                        previousRolls.RemoveAt(0);

                        if (isSpareWait)
                        {
                        }
                        var isSpare = previousRolls.Sum() == 10;
                        if (!isSpare && previousRolls.Sum() < 10)
                        {
                            currentScore += previousRolls.Sum();
                            _gameService.AddFrameDetails(ref frames, frameNumber, currentScore);
                            previousRolls.Clear();
                        }
                        isSpareWait = isSpare;

                    }
                }

                //Spare
                else if (isSpareWait)
                {
                    if (previousRolls.Count == 3)
                    {
                        if (previousRolls.First() == 10)
                        {
                            currentScore += previousRolls.Sum(); //Last X Score
                            _gameService.AddFrameDetails(ref frames, frameNumber, currentScore);
                            isStrikeWait = false;
                        }
                        currentScore += previousRolls.Sum();
                        _gameService.AddFrameDetails(ref frames, frameNumber, currentScore);
                        isSpareWait = false;
                        if (previousRolls.First() == 10)
                            previousRolls.Clear();
                        else previousRolls.RemoveRange(0, 2);
                    }
                }

                // Normal
                else
                {
                    if (previousRolls.First() == 10)
                    {
                        currentScore += previousRolls.Sum(); // Last Score
                        _gameService.AddFrameDetails(ref frames, frameNumber, currentScore);
                        previousRolls.RemoveAt(0);
                        currentScore += previousRolls.Sum();
                        _gameService.AddFrameDetails(ref frames, frameNumber, currentScore);
                        previousRolls.Clear();
                    }
                    else
                    {
                        currentScore += previousRolls.Sum();
                        _gameService.AddFrameDetails(ref frames, frameNumber, currentScore);
                        previousRolls.Clear();
                    }
                }
            }


            if (previousRolls.Any() && previousRolls.First() == 10)
            {
                isStrikeWait = true;
            }
            else
            {
                isStrikeWait = (!previousRolls.Any() && score.IsStrike());

            }

            return frames;


        }

        public int CalculateScore()
        {
            int frameIndex = 0;
            int score = 0;

            for (int i = 0; i < 10; i++) //looping throw frames
            {
                if (frameIndex.IsSpare(rolls))
                {
                    score += rolls[frameIndex] + rolls[frameIndex + 1] + rolls[frameIndex + 2];
                    frameIndex += 2;
                }
                else if (frameIndex.IsStrike(rolls))
                {
                    score += rolls[frameIndex] + rolls[frameIndex + 1] + rolls[frameIndex + 2];
                    frameIndex++;
                }
                else
                {
                    score += rolls[frameIndex] + rolls[frameIndex + 1];
                    frameIndex += 2;
                }

            }

            return score;
        }

    }
}
