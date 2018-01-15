using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL
{
    public class Game
    {        
        public Game()
        {
            GameRounds = new List<GameRound>();
        }
        public int GameID { get; set; }
        public string SaveName { get; set; }
        public DateTime? SaveDate { get; set; }   
        
        public virtual ICollection<GameRound> GameRounds { get; set; }
    }
}
