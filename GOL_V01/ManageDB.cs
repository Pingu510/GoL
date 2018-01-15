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

        /// <summary>
        /// Save games to database by name
        /// </summary>
        public void DoSaveGame(string SaveName)
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

        /// <summary>
        /// Save every round/iteration of the game grid to DB as a string
        /// </summary>
        public void DoSaveRounds(string CurrentGameRound, int GridSize)//save rounds
        {         
            using (var context = new DBContext())
            {
                var newRound = new GameRound();
                                
                context.Games.Attach(_currentGame);
                newRound.Game = _currentGame;
                newRound.GridSize = GridSize;
                newRound.Round = increaseRound++;
                newRound.PlayingField = CurrentGameRound;

                context.Rounds.Add(newRound);
                context.SaveChanges();
            }
        }
        
        /// <summary>
        /// Deletes a game from DB and it's game rounds
        /// </summary>
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

        /// <summary>
        /// Gets the set grid size from the settings class via the GameRounds class
        /// </summary>
        public int GetSavedGridSize()
        {
            return gridSize;
        }

        /// <summary>
        /// Rename the deafult game name "DefaultGameName" at the time you save, to whatever is typed in the text box. 
        /// </summary>
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

        /// <summary>
        ///Gets the selected game from DB 
        /// </summary>
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

        /// <summary>
        /// Get a saved game round from DB as a string
        /// </summary>
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
