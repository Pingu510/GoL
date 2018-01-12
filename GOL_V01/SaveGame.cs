using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GOL
{
    public class SaveGame
    {
        
        //två funktioner, en för att spara spel och en för att spara varje runda.
        Game _currentGame;
        int gridSize;
        int increaseRound;

        public void DoSaveGame(string SaveName)//spara spel
        {

            increaseRound = 0;
            using (var context = new DBContext())
            {
                var newSave = new Game();
                newSave.SaveName = SaveName;
                newSave.SaveDate = DateTime.Now;
                _currentGame = newSave;
                context.Games.Add(newSave);
                context.SaveChanges();
                
            }
        }

        public void DoSaveRounds(string CurrentGameRound, int GridSize)//save rounds
        {
            using (var context = new DBContext())
            {
                var newRound = new GameRound();
                newRound.SaveID = _currentGame;
                newRound.GridSize = GridSize;
                newRound.Round = increaseRound++;
                newRound.PlayingField = CurrentGameRound;

                context.Rounds.Add(newRound);
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
            return gridSize;
        }

        public string GetLoadGamePlayingfield(string LoadGameName)
        {
            string loadedGameFirstRound = "";
            using (var c = new DBContext())
            {
                Game gameFound = null;
                bool _continue = false;

                foreach (Game g in c.Games)
                {
                    if (g.SaveName == LoadGameName)
                    {
                        gameFound = g;
                        _continue = true;
                    }
                }

                if (_continue)
                {
                    foreach (GameRound gr in c.Rounds)
                    {
                        if (gr.SaveID == gameFound)
                        {
                            loadedGameFirstRound = gr.PlayingField;
                            gridSize = gr.GridSize;
                        }
                    }
                }
            }
            return loadedGameFirstRound;
        }
    }
}
