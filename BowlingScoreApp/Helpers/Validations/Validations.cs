using System;

namespace BowlingScoreApp.Helpers.Validations
{
    public static class Validations
    {

        public static bool InputValidation(this string input)
        {
            while (!input.IsBetween(0, 10))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong Input! Only enter numbers between 0 to 10, Try again");
                Console.ForegroundColor = ConsoleColor.Yellow;
                return false;
            }

            return true;
        }

        public static bool SecondInputValidate(this string input, int previousScore)
        {
            if (int.Parse(input) + previousScore > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The sum of previous roll with the current roll should not be more than 10, try again:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                return false;
            }

            return true;
        }

        public static string FixText(this string text)
        {
            return text.Trim().ToLower().Replace(" ", "");
        }

        private static bool isItNumber(this string value)
        {
            try
            {
                var isNumber = Convert.ToInt32(value) is int;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsBetween(this string number, int from, int to)
        {
            if (!number.isItNumber())
                return false;

            if (Convert.ToInt32(number) >= from && Convert.ToInt32(number) <= to)
                return true;


            return false;
        }

    }
}
