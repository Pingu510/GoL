using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL
{
    public class GameRound
    {
        public int GameRoundID { get; set; }
        public int Round { get; set; }
        public string PlayingField { get; set; }
        public int GridSize { get; set; }

        //public int SaveID { get; set; }
        public Game SaveID { get; set; }
    }
}
