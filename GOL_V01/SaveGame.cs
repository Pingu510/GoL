using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL
{
    public class SaveGame
    {
        //två funktioner, en för att spara spel och en för att spara varje runda.
        Settings s;

        public void IfSaveGame(string SaveName)//spara spel
        {
            using (var context = new DBContext())
            {
                var newsave = new Game();
                newsave.SaveName = "BestGame";
                newsave.SaveDate = System.DateTime.Now;
                context.Games.Add(newsave);
            }
            return;
        }

        public void IfSaveRounds(string currentroundstring)//save rounds
        {
            using (var context = new DBContext())
            {
                var newround = new GameRound();
                
                newround.GridSize = s.GridSize;
                newround.PlayingField = currentroundstring;
                newround.Round = 1;
           
                context.Rounds.Add(newround);
                context.SaveChanges();
            }
        
        }

    }
}
