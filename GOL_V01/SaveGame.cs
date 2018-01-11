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
        int gridsize;
        int increaseround;

        public void DoSaveGame(string SaveName)//spara spel
        {
            increaseround = 0;
            using (var context = new DBContext())
            {
                var newsave = new Game();
                newsave.SaveName = SaveName; 
                newsave.SaveDate = DateTime.Now;                
                _currentgame = newsave;
                context.Games.Add(newsave);
                context.SaveChanges();
            }         
        }

        public void DoSaveRounds(string CurrentGameRound, int GridSize)//save rounds
        {         
            using (var context = new DBContext())
            {
                var newround = new GameRound();
                newround.SaveID = _currentgame;
                newround.GridSize = GridSize;
                newround.Round = increaseround++;
                newround.PlayingField = CurrentGameRound;

                context.Rounds.Add(newround);
                context.SaveChanges();
            }
        }
        
        public void DeleteGame(Game g)
        {
            //Delete all rounds connected to this g
            //cascade delete?
        }

        public int GetSavedGridSize()
        {
            return gridsize;
        }

        public string GetLoadGamePlayingfield(string LoadGameName)
        {
            string loadedgamefirstround = "";
            using (var c = new DBContext())
            {
                Game gamefound = null;
                bool _continue = false;

                foreach (Game g in c.Games)
                {
                    if(g.SaveName == LoadGameName)
                    {
                        gamefound = g;
                        _continue = true;
                    }
                }

                if (_continue)
                {
                    foreach (GameRound gr in c.Rounds)
                    {
                        if (gr.SaveID == gamefound)
                        {
                            loadedgamefirstround = gr.PlayingField;
                            gridsize = gr.GridSize;
                        }
                    }
                }
            }
            return loadedgamefirstround;
        }
    }
}
