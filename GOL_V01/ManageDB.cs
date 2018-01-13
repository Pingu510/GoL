using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL
{
    public class ManageDB
    {
        
        int _currentGame;
        
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
                context.Games.Add(newSave);
                context.SaveChanges();
                _currentGame = newSave.GameID;
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
        
        public void DeleteGame(string GameToDelete)
        {
            //Game i detta fall kommer kanske bara vara en variabel och inte ett Game objekt
            //Delete all rounds connected to this g
            //cascade delete?
        }

        public int GetSavedGridSize()
        {
            return gridSize;
        }

        public void RenameGame(string OldName, string NewName)
        {
            using (var c = new DBContext())
            {
                Game g = GetGame(OldName);
                var o = c.Games.Find(g.GameID);
                o.SaveName = NewName;
                c.SaveChanges();
            }
        }

        public Game GetGame(string GameName)
        {
            using (var c = new DBContext())
            {
                Game gameFound = null;

                foreach (Game g in c.Games)
                {
                    if (g.SaveName == GameName)
                    {
                        gameFound = g;
                        _currentGame = g.GameID; // this ok?
                        break;
                    }
                }
                return gameFound;                
            }
        }

        public string GetPlayingfield(Game GameToLoad)
        {
            string loadedFirstRound = "";
            using (var c = new DBContext())
            {
                foreach (GameRound gr in c.Rounds)
                {
                    if (gr.SaveID == GameToLoad.GameID)
                    {
                        loadedFirstRound = gr.PlayingField;
                        gridSize = gr.GridSize;
                        break;
                    }
                }
                return loadedFirstRound;
            }            
        }
    }
}
