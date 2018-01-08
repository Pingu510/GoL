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
        public int Generation { get; set; }

        public int SaveID { get; set; }
        public SaveGame Save { get; set; }
    }
}
