using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GOL
{
    public partial class Form1 : Form
    {
        //Point location;
        Settings s;
        MoveLogic ml;
        SaveGame saveOrLoad = new SaveGame();
        GUI_Options GUI = new GUI_Options();
        Button[,] gameButtonGridArray;
        List <Game> ListOfSavedGames = new List<Game>();

        public Form1()
        {
            InitializeComponent();

            //MouseClick += mouseClick;

            s = new Settings(GameGrid_Panel);
            gameButtonGridArray = new Button[s.GridSize, s.GridSize];
            ml = new MoveLogic(s);
            CreateGrid();
            LightButtonTest(); // Test
            UpdateGrid();
            UpdateLoadListBox(); // Test
        }



        //private void mouseClick(object sender, MouseEventArgs e)
        //{
        //    if ( == ButtonState.Pushed)
        //    {
        //        cellstatealive = true;
        //    }
        //}

        /// <summary>
        /// Creates the Grid with new buttons
        /// </summary>
        private void CreateGrid()
        {
            int xpos = 0;
            int ypos = 0;

            for (int y = 0; y < s.GridSize;)
            {
                for (int x = 0; x < s.GridSize;)
                {
                    Point tempLocation = new Point(xpos, ypos);

                    Button b = new Button
                    {
                        Parent = GameGrid_Panel,
                        Size = new Size(s.ButtonSize, s.ButtonSize),
                        Location = tempLocation,
                        FlatStyle = FlatStyle.Flat,
                    };
                    b.FlatAppearance.BorderSize = 1;
                    b.FlatAppearance.BorderColor = Color.LightGray;
                    b.Click += Grid_btn_Click;

            
                    s.PastGameTurnArray[x, y] = 0;
                    gameButtonGridArray[x, y] = b;

                    xpos += s.ButtonSize;
                    x++;
                }
                xpos = 0;
                ypos += s.ButtonSize;
                y++;
            }
            UpdateGrid();
        }

        public void Grid_btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int found_x = -1;
            int found_y = -1;

            for (int x = 0; x < s.GridSize && found_x < 0; ++x)
            {
                for (int y = 0; y < s.GridSize; ++y)
                {
                    if (gameButtonGridArray[x, y] == btn) // (or maybe 'object.ReferenceEqual')
                    {
                        found_x = x;
                        found_y = y;
                        break;
                    }
                }
            }

            // Remove this after testing
            lstBxSavedGames.Items.Add(found_x + "   ---  " + found_y);

            if (btn.BackColor != s.AliveCellColor)
            {
                btn.BackColor = s.AliveCellColor;
                s.NewGameTurnArray[found_x, found_y] = 1;
                s.PastGameTurnArray[found_x, found_y] = 1;
            }
            else
            {
                btn.BackColor = s.DeadCellColor;
                s.NewGameTurnArray[found_x, found_y] = 0;
                s.PastGameTurnArray[found_x, found_y] = 0;
            }
            UpdateGrid();
        }

        private void PlayRound()
        {
            ml.PlayRound(s);
            s.PastGameTurnArray = (int[,])s.NewGameTurnArray.Clone();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            for (int y = 0; y < s.GridSize;)
            {
                for (int x = 0; x < s.GridSize;)
                {
                    if (s.NewGameTurnArray[x, y] == 1)
                    {
                        gameButtonGridArray[x, y].BackColor = s.AliveCellColor;
                    }
                    else
                        gameButtonGridArray[x, y].BackColor = s.DeadCellColor;

                    x++;
                }
                y++;
            }
        }

        private void SaveGame(string SaveName)
        {
            saveOrLoad.DoSaveGame(SaveName);
        }
        
        private void SaveRound()
        {
            string currentRound = "";
            foreach( var position in s.PastGameTurnArray)
            {
                currentRound += position + ",";
            }
            saveOrLoad.DoSaveRounds(currentRound, s.GridSize);
        }


        
        /// <summary>
        /// Takes the loaded string and makes it into the GameArrays
        /// </summary>
        private void MakeLoadedRoundToAnArray(string gameRound)
        {
            string[] temparr = gameRound.Split(',');
            int n = 0;
            for (int y = 0; y < s.GridSize; y++)
            {
                for (int x = 0; x < s.GridSize; x++)
                {
                    int i = Int32.Parse(temparr[n]);
                    
                    s.NewGameTurnArray[x, y] = i;
                    s.PastGameTurnArray[x, y] = i;
                    n++;
                }
            }
        }


        /// <summary>
        /// Detta är en testfunk, flytta och ändra som ni vill
        /// </summary>

        private void LightButtonTest() // Tänk att origo är i övre vänstra hörnet [x,y], (y går nedåt fast positiv) 
        {
            s.PastGameTurnArray[0, 0] = 1;
            s.PastGameTurnArray[0, 1] = 1;
            s.PastGameTurnArray[1, 1] = 1;
            s.PastGameTurnArray[0, 2] = 1;
            s.PastGameTurnArray[1, 2] = 1;

           // s.PastGameTurn[] = 1;


            s.NewGameTurnArray[0, 0] = 1;
            s.NewGameTurnArray[0, 1] = 1;
            s.NewGameTurnArray[1, 1] = 1;
            s.NewGameTurnArray[0, 2] = 1;
            s.NewGameTurnArray[1, 2] = 1;

            // Test

            //using (var context = new DBContext())
            //{
            //    var newSave = new Game();
            //    newSave.SaveName = "BestGame";
            //    newSave.SaveDate = System.DateTime.Now;

            //    var newRound = new GameRound();

            //    string currentRoundString = "";
            //    foreach (var position in s.PastGameTurnArray)
            //    {
            //        currentRoundString = currentRoundString + "," + position;
            //    }
            //    newRound.GridSize = s.GridSize;
            //    newRound.PlayingField = currentRoundString;
            //    newRound.Round = 4;

            //    newRound.SaveID = newSave;
            //    context.Games.Add(newSave);
            //    context.Rounds.Add(newRound);
            //    context.SaveChanges();
            //}

        }

        private void UpdateLoadListBox()
        {
            lstBxSavedGames.Items.Clear();
            using (var context = new DBContext())
            {
                var saves = context.Games;

                foreach (var save in saves)
                {
                    lstBxSavedGames.Items.Add(save.SaveName);
                }                
            }
        }



        private void btnStart_Click(object sender, EventArgs e)
        {
            PlayRound();
            
            //if (txbNameOfTheGame.Text != null)
            //g = new Game (txbNameOfTheGame.Text);

            //GUI.StartGame();
        }

        //private void btnStop_Click(object sender, EventArgs e)
        //{
        //    GUI.StopGame();
        //    lstBxSavedGames.Items.Add(g);
        //    ListOfSavedGames.Add(g);
        //}


        private void btnLoad_Click(object sender, EventArgs e)
        {
            string gameName = lstBxSavedGames.SelectedItem.ToString();
            //send the selected gamename or game 
            // FIX THIS
            string loadedGameRound = saveOrLoad.GetLoadGamePlayingfield(gameName);
            s.GridSize = saveOrLoad.GetSavedGridSize();
            MakeLoadedRoundToAnArray(loadedGameRound);
            //int LoadIndexNr = 0;
            //if (lstBxSavedGames.SelectedItem != null)
            //    LoadIndexNr = lstBxSavedGames.SelectedIndex - 1;


            //GUI.LoadGame(LoadIndexNr);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            int indexNr;

            if (lstBxSavedGames.SelectedIndex == -1)
            {
                indexNr = lstBxSavedGames.SelectedIndex - 1;
            }
            else
            {
                indexNr = lstBxSavedGames.SelectedIndex;
               
                ListOfSavedGames.RemoveAt(indexNr);
               
            }
            
            //GUI.DeleteGame();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {

        }

        private void btnStopSave_Click(object sender, EventArgs e)
        {
            SaveGame("Testus");
            SaveRound();
        }
    }
}

