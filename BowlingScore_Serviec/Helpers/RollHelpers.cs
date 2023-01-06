using System.Collections.Generic;

namespace BowlingScore_Serviec.Helpers
{
    public static class RollHelpers
    {
        public static bool IsPreviousRollsStrike(this List<int> rolls)
        {
            foreach (int roll in rolls)
            {
                if (roll != 10)
                    return false;
            }

            return true;
        }
    }
}
