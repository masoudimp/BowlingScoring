using BowlingScore_Serviec.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScore_Serviec.Services
{
    public class ScoreService
    {
        private List<int> lastFrameScores = new List<int>();
        private List<int> previousRolls = new List<int>();
        private int[] rolls = new int[21];
        private int rollCount = 0;
        private int currentScore = 0;
        private bool isSpareWait = false;
        private bool isStrikeWait = false;

        public int[] Rolls // Encapsulation
        {
            get { return rolls; }
            set { rolls = value; }
        }

        //Live Calculator
        public int Roll(int score)
        {

            if (score.IsStrike() && rollCount < 19)
                rollCount += 2;
            else
                rollCount++;

            if (rollCount >= 20)
            {
                lastFrameScores.Add(score);
            }

            if (rollCount == 22)
            {
                currentScore += lastFrameScores.Sum();
                return currentScore;
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
                        previousRolls.RemoveAt(0);

                        if (isSpareWait)
                        {
                        }
                        var isSpare = previousRolls.Sum() == 10;
                        if (!isSpare && previousRolls.Sum() < 10)
                        {
                            currentScore += previousRolls.Sum();
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
                            isStrikeWait = false;
                        }
                        currentScore += previousRolls.Sum();
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
                        previousRolls.RemoveAt(0);
                        currentScore += previousRolls.Sum();
                        previousRolls.Clear();
                    }
                    else
                    {
                        currentScore += previousRolls.Sum();
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

            return currentScore;


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
