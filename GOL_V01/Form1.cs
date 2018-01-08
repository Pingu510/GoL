using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GOL
{
    public partial class Form1 : Form
    {
        Settings s;
        Game g;
        MoveLogic ml;
        GUI_Options GUI = new GUI_Options();
        Button[,] gamebuttongridarray;
        List <Game> ListOfSavedGames = new List<Game>();

        public Form1()
        {
            InitializeComponent();

            s = new Settings(GameGrid_Panel);
            gamebuttongridarray = new Button[s.GridSize, s.GridSize];
            ml = new MoveLogic(s);
            CreateGrid();
            LightButtonTest();
            UpdateGrid();
        }
               
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
                        Location = templocation
                    };
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
            s.NewGameTurn[0, 0] = 1;
            s.NewGameTurn[0, 1] = 1;
            s.NewGameTurn[1, 1] = 1;
            s.NewGameTurn[0, 2] = 1;
            s.NewGameTurn[1, 2] = 1;

            // Test
            using (var context = new DBContext())
            {
                var newsave = new SaveGame();
                newsave.SaveName = "Spel2";
                context.SaveGames.Add(newsave);
                context.SaveChanges();
            }
            using (var context = new DBContext())
            {
                var newsave = new SaveGame();
                newsave.SaveName = "BestGame";
                var gameRound = new GameRound();
                gameRound.GridSize = s.GridSize;
                gameRound.PlayingField = s.PastGameTurn.ToString();
                gameRound.Round = 1;
                
                gameRound.SaveID = newsave;
                context.SaveGames.Add(newsave);
                context.GameRounds.Add(gameRound);
                context.SaveChanges();
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
            lstBxSavedGames.Items.Add(g);
            ListOfSavedGames.Add(g);
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
    }
}

