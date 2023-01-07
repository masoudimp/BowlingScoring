

using System.Collections.Generic;

namespace BowlingScore_Serviec.ViewModelsOrModels
{
    public class Frame
    {
        public int FrameNumber { get; set; }
        public List<Roll> Rolls { get; set; }
        public int Score { get; set; }
        public bool IsStrike { get; set; }
        public bool IsSpare { get; set; }
        public bool IsNoScore { get; set; }
    }
}
