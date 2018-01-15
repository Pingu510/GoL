using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL
{
    public class ManageDB
    {        
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
                context.Games.Add(newSave);
                context.SaveChanges();
                _currentGame = newSave;
            }         
        }

        public void DoSaveRounds(string CurrentGameRound, int GridSize)//save rounds
        {         
            using (var context = new DBContext())
            {
                var newRound = new GameRound();
                //newRound.SaveID = _currentGame.GameID;
                
                context.Games.Attach(_currentGame);
                newRound.Game = _currentGame;
                newRound.GridSize = GridSize;
                newRound.Round = increaseRound++;
                newRound.PlayingField = CurrentGameRound;

                context.Rounds.Add(newRound);
                context.SaveChanges();
            }
        }
        
        public void DeleteGame(string GameName)
        {
            using (var context = new DBContext())
            {
                Game g = GetGame(GameName);
                if(g != null)
                {
                    context.Games.Attach(g);
                    context.Games.Remove(g);
                    context.SaveChanges();
                }                
            }
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
                c.Games.Attach(g);
                g.SaveName = NewName;
                c.SaveChanges();
            }
        }

        public Game GetGame(string GameName)
        {
            Game gameFound = null;
            using (var c = new DBContext())
            {
                var theGames = c.Games.ToList();
                foreach (Game g in theGames)
                {
                    if (g.SaveName == GameName)
                    {
                        gameFound = g;
                        _currentGame = g; // this ok?
                        break;
                    }
                }
            }
            return gameFound;                
        }

        public string GetPlayingfield(string GameName)
        {
            string loadedFirstRound = "";
            using (var c = new DBContext())
            {
                Game g = GetGame(GameName);
                c.Games.Attach(g);
                foreach (GameRound gr in c.Rounds)
                {
                    if (gr.Game == g)
                    {
                        loadedFirstRound = gr.PlayingField;
                        gridSize = gr.GridSize;
                        break;
                    }
                }
            }            
            return loadedFirstRound;
        }
    }
}
