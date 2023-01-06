using BowlingScoreApp.Helpers.IO;
using BowlingScoreApp.Helpers.Validations;
using System;
using BowlingScore_Serviec.Services;

namespace BowlingScoreApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Input Output

            var io = new InputOutput();
            var scoreService = new ScoreService();

            io.ShowLogo("  LOGO HERE  ");

            io.ShowTip();

            #endregion

            int frame = 0;
            int roll = 0;
            bool isGameFinished = false;
            int[] hittedPins = new int[23];
            int tempFirstFrame10Score = 0;
            int previousScore = 0;
            int score = 0;
            int currentScore = 0;
            int previousCurrentScore = 0;


            for (int i = 0; i < 23; i++)
            {
                previousCurrentScore = currentScore;

                #region Roll and Frame Numbers


                if (i == 0)
                    roll = 1;   // It means the first roll is always roll number 1
                else
                    roll = (frame == 10 && roll == 2) ? 3 : (roll == 2) ? 1 : (frame == 10 && roll == 2) ? 3 : 2;

                if (roll == 1 && frame != 10)
                    frame++;

                #endregion


                #region Input Output

                var input = io.InputOutputHandler(roll, frame);

                //Input Validation
                while (!input.InputValidation())
                    input = Console.ReadLine().FixText();
                

                if(frame != 10)
                {
                    while (!input.SecondInputValidate(previousScore))
                        input = Console.ReadLine().FixText();
                }

                score = Convert.ToInt32(input);
                previousScore = score;

                #endregion


                #region Third roll rule

                if (roll == 1 || roll == 3)
                {
                    hittedPins[i] = score;
                }
                else
                {
                    if (score != 0)
                        hittedPins[i] = score;
                }

                if (frame != 10)
                {
                    if (score == 10)
                    {
                        roll = 2;
                    }
                }

                #endregion


                #region 10th frame rule

                if (frame == 10) //If it is the 10th frame
                {

                    if (roll == 1)
                    {
                        tempFirstFrame10Score = score;
                    }

                    if (roll == 2)
                    {
                        if (tempFirstFrame10Score + score < 10)
                            isGameFinished = true;
                    }

                    if (roll == 3)
                        isGameFinished = true;
                }

                #endregion


                currentScore = scoreService.Roll(score);

                if(previousCurrentScore != currentScore)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Latest Score: " + currentScore);
                    Console.ResetColor();
                }
                else
                {
                    //Console.ForegroundColor = ConsoleColor.White;
                    //Console.WriteLine("Score depends on the next row...");
                    //Console.ResetColor();
                }


                if (roll == 2)
                    previousScore = 0;

                if (isGameFinished)
                    break;
            }



            io.ShowFinish();

            scoreService.Rolls = hittedPins;
            var finalScore = scoreService.CalculateScore();

            Console.WriteLine(finalScore.ToString());
            io.ShowLogo(currentScore.ToString());

            Console.ReadKey();
        }
    }
}
