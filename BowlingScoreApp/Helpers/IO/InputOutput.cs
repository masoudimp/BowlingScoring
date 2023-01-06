using BowlingScoreApp.Helpers.Validations;
using System;
using TextToAsciiArt;

namespace BowlingScoreApp.Helpers.IO
{
    public class InputOutput
    {

        public string InputOutputHandler(int roll, int frame)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n \n ************* --> Roll " + roll.ToString() + " of Frame " + frame.ToString() + " <-- **************");


            Console.ForegroundColor = ConsoleColor.Yellow;
            var score = Console.ReadLine().FixText();
            Console.ForegroundColor = ConsoleColor.Green;

            return score;
        }

        public void ShowLogo(string logo)
        {
            IArtWriter writer = new ArtWriter();

            var setting = new ArtSetting
            {
                ConsoleSpeed = 30,
                IsBreakSpace = false,
                Text = "▓",
                BgText = " ",
                SpaceWidth = 2,
            };

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" \n ------------------------------------------------------------------------------------------------------ \n");
            Console.ForegroundColor = ConsoleColor.Cyan;

            writer.WriteConsole(logo, setting);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" \n ------------------------------------------------------------------------------------------------------ \n");
            Console.ResetColor();
        }

        public void ShowFinish()
        {
            IArtWriter writer = new ArtWriter();

            var setting = new ArtSetting
            {
                ConsoleSpeed = 30,
                IsBreakSpace = false,
                Text = "▓",
                BgText = " ",
                SpaceWidth = 2,
            };

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");
            writer.WriteConsole(" Game Finished", setting);
            Console.WriteLine("\n\n\n\n");
            Console.ResetColor();
        }

        public void ShowTip()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n *Tip: Only enter numbers between 0 to 10 \n \n");
        }

    }
}
