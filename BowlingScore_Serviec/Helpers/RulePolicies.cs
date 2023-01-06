using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingScore_Serviec.Helpers
{
    public static class RulePolicies
    {
        public static bool IsSpare(this int currentRoll, int previousRoll)
        {

            if (previousRoll + currentRoll == 10)
                return true;

            return false;
        }

        public static bool IsSpare(this int rollNumber, int[] rolls)
        {

            if (rolls[rollNumber] + rolls[rollNumber + 1] == 10)
                return true;

            return false;
        }

        public static bool IsStrike(this int rollNumber, int[] rolls)
        {
            if (rolls[rollNumber] == 10)
                return true;

            return false;
        }

        public static bool IsStrike(this int score)
        {
            if (score == 10)
                return true;

            return false;
        }
    }
}
