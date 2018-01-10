using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GOL
{
    public partial class Form1 : Form
    {
        Point location;
        Settings s;
        MoveLogic ml;
        SaveGame save = new SaveGame();
        GUI_Options GUI = new GUI_Options();
        Button[,] gamebuttongridarray;
        List <Game> ListOfSavedGames = new List<Game>();

        public Form1()
        {
            InitializeComponent();

            //MouseClick += mouseClick;

            s = new Settings(GameGrid_Panel);
            gamebuttongridarray = new Button[s.GridSize, s.GridSize];
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
                    Point templocation = new Point(xpos, ypos);

                    Button b = new Button
                    {
                        Parent = GameGrid_Panel,
                        Size = new Size(s.ButtonSize, s.ButtonSize),
                        Location = templocation,
                        FlatStyle = FlatStyle.Flat,
                    };
                    b.FlatAppearance.BorderSize = 1;
                    b.FlatAppearance.BorderColor = Color.LightGray;
                    b.Click += Grid_btn_Click;

            
                    s.PastGameTurn[x, y] = 0;
                    gamebuttongridarray[x, y] = b;

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
                    if (gamebuttongridarray[x, y] == btn) // (or maybe 'object.ReferenceEqual')
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
                s.NewGameTurn[found_x, found_y] = 1;
                s.PastGameTurn[found_x, found_y] = 1;
            }
            else
            {
                btn.BackColor = s.DeadCellColor;
                s.NewGameTurn[found_x, found_y] = 0;
                s.PastGameTurn[found_x, found_y] = 0;
            }
            UpdateGrid();
        }

        private void PlayRound()
        {
            ml.PlayRound(s);
            s.PastGameTurn = (int[,])s.NewGameTurn.Clone();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            for (int y = 0; y < s.GridSize;)
            {
                for (int x = 0; x < s.GridSize;)
                {
                    if (s.NewGameTurn[x, y] == 1)
                    {
                        gamebuttongridarray[x, y].BackColor = s.AliveCellColor;
                    }
                    else
                        gamebuttongridarray[x, y].BackColor = s.DeadCellColor;

                    x++;
                }
                y++;
            }
        }
        private void SaveGame(string SaveName)
        {
            save.DoSaveGame("X");
        }
        
        private void SaveRound(Game game)
        {
            string currentround = "";
            foreach( var position in s.PastGameTurn)
            {
                currentround += position + ",";
            }
            save.DoSaveRounds(currentround, s.GridSize);
        }

        /// <summary>
        /// Takes the loaded string and makes it into the GameArrays
        /// </summary>
        /// <param name="gameround"></param>
        /// <param name="gridsize"></param>
        private void MakeSavedRoundArray(string gameround, int gridsize)
        {
            s.GridSize = gridsize;
            string[] temparr = gameround.Split(',');
            int n = 0;
            for (int y = 0; y < gridsize; y++)
            {
                for (int x = 0; x < gridsize; x++)
                {
                    int i = Int32.Parse(temparr[n]);


                    //s.NewGameTurn[] = i;




                    s.NewGameTurn[x, y] = i;
                    s.PastGameTurn[x, y] = i;
                    n++;
                }
            }
        }

        private void LoadRound(Game g)
        {
            //select game in load click
            //update s.gridsize from loadedgame?
            string loadedgameround = save.LoadGame(g);
            MakeSavedRoundArray(loadedgameround, s.GridSize);
        }

        /// <summary>
        /// Detta är en testfunk, flytta och ändra som ni vill
        /// </summary>
        
        private void LightButtonTest() // Tänk att origo är i övre vänstra hörnet [x,y], (y går nedåt fast positiv) 
        {
            s.PastGameTurn[0, 0] = 1;
            s.PastGameTurn[0, 1] = 1;
            s.PastGameTurn[1, 1] = 1;
            s.PastGameTurn[0, 2] = 1;
            s.PastGameTurn[1, 2] = 1;

           // s.PastGameTurn[] = 1;


            s.NewGameTurn[0, 0] = 1;
            s.NewGameTurn[0, 1] = 1;
            s.NewGameTurn[1, 1] = 1;
            s.NewGameTurn[0, 2] = 1;
            s.NewGameTurn[1, 2] = 1;

            // Test

            //using (var context = new DBContext())
            //{
            //    var newsave = new Game();
            //    newsave.SaveName = "BestGame";
            //    newsave.SaveDate = System.DateTime.Now;

            //    var newround = new GameRound();

            //    string currentroundstring = "";
            //    foreach (var position in s.PastGameTurn)
            //    {
            //        currentroundstring = currentroundstring + "," + position;
            //    }
            //    newround.GridSize = s.GridSize;
            //    newround.PlayingField = currentroundstring;
            //    newround.Round = 4;

            //    newround.SaveID = newsave;
            //    context.Games.Add(newsave);
            //    context.Rounds.Add(newround);
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            GUI.StopGame();
            //lstBxSavedGames.Items.Add(g);
            //ListOfSavedGames.Add(g);
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            int LoadIndexNr = 0;
            if (lstBxSavedGames.SelectedItem != null)
                LoadIndexNr = lstBxSavedGames.SelectedIndex - 1;


            GUI.LoadGame(LoadIndexNr);
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
    }
}

