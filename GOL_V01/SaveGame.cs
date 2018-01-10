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
        Settings s;

        public void DoSaveGame(string SaveName)//spara spel
        {
            using (var context = new DBContext())
            {
                var newsave = new Game();
                newsave.SaveName = "testgame"; //test för att se om det funkar..
                newsave.SaveDate = System.DateTime.Now;
                
                _currentgame = newsave; // får se om detta funkar
                context.Games.Add(newsave);
                context.SaveChanges();
            }         
        }

        public void DoSaveRounds(string CurrentGameRound, int GridSize)//save rounds
        {
         
            using (var context = new DBContext())
            {
                var newround = new GameRound();
                newround.SaveID = _currentgame; // Funkar detta?
                newround.GridSize = s.GridSize;
                //string currentroundstring = "";//
                //  foreach (var position in s.PastGameTurn)
                //   {
                //       currentroundstring = currentroundstring + "," + position;
                //   }
               // newround.PlayingField = Currentroundstring; 
                newround.Round = 8; // Måste ökas på ngt vis

                context.Rounds.Add(newround);
                context.SaveChanges();
            }
        }

//              using (var context = new DBContext())
//            {
//                var newsave = new Game();
//                newsave.SaveName = "BestGame";
//                newsave.SaveDate = System.DateTime.Now;

//                var newround = new GameRound();
//                 newround.GridSize = s.GridSize;
//                newround.PlayingField = currentroundstring;
//                newround.Round = 4;

//                newround.SaveID = newsave;
//                context.Games.Add(newsave);
//                context.Rounds.Add(newround);
//                context.SaveChanges();
//            }

        public void DeleteGame(Game g)
        {
            //Delete all rounds connected to this g
            //cascade delete?
        }

        public string GetLoadGamePlayingfield(string LoadGameName)
        {
            string loadedgamefirstround = "";
            using (var c = new DBContext())
            {
                Game gamefound = c.Games.Find(LoadGameName); // works?
                var savefound = c.Rounds;
                foreach (var item in savefound)
                {
                    if (item.SaveID == gamefound)
                    {
                        loadedgamefirstround = item.PlayingField;
                    }
                }
            }
            return loadedgamefirstround;
        }
    }
}
