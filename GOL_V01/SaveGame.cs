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
        Game _currentgame;

        public void DoSaveGame(string SaveName)//spara spel
        {
            using (var context = new DBContext())
            {
                var newsave = new Game();
                newsave.SaveName = SaveName;
                newsave.SaveDate = System.DateTime.Now;
                context.Games.Add(newsave);
                _currentgame = newsave; // får se om detta funkar
                context.SaveChanges();
            }
            //return; onödig då det är void
        }

        public void DoSaveRounds(string CurrentGameRound, int GridSize)//save rounds
        {
            using (var context = new DBContext())
            {
                var newround = new GameRound();
                newround.SaveID = _currentgame; // Funkar detta?
                newround.GridSize = GridSize;
                newround.PlayingField = CurrentGameRound;
                newround.Round = 1; // Måste ökas på ngt vis

                context.Rounds.Add(newround);
                context.SaveChanges();
            }
        }

        public void DeleteGame(Game g)
        {
            //Delete all rounds connected to this g
        }

        public string LoadGame(Game g)
        {
            string loadedgamefirstround = "";


            return loadedgamefirstround;
        }        
    }
}
