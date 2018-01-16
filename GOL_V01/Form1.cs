using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GOL
{
    public partial class Form1 : Form
    {
        Settings s;
        MoveLogic ml;
        ManageDB manageDB = new ManageDB();
        Button[,] gameButtonGridArray;
        List<Game> ListOfSavedGames = new List<Game>();
        Random random = new Random(15);
        bool savegame = false;

        public Form1()
        {
            InitializeComponent();
            s = new Settings(GameGrid_Panel);
            gameButtonGridArray = new Button[s.GridSize, s.GridSize];
            ml = new MoveLogic(s);
            CreateGrid();
            UpdateGrid();
            UpdateLoadListBox();
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (savegame)
                SaveRound();
            if (s.PastGameTurnArray == s.NewGameTurnArray) // If the cells doesnt change anymore
            {
                PlayTimer.Stop();
                btnPause.Enabled = false;
            }
            PlayRound();
        }

        private void PlayRound()
        {
            ml.PlayRound(s);            
            s.PastGameTurnArray = (int[,])s.NewGameTurnArray.Clone();
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
                    Point tempLocation = new Point(xpos, ypos);

                    Button button = CreateButton(tempLocation);
                    button.Click += Grid_btn_Click;

                    s.PastGameTurnArray[x, y] = 0;
                    gameButtonGridArray[x, y] = button;

                    xpos += s.ButtonSize;
                    x++;
                }
                xpos = 0;
                ypos += s.ButtonSize;
                y++;
            }
        }

        /// <summary>
        /// Skapar rutornas utseende.
        /// </summary>
        /// <param name="tmpLocation"></param>
        /// <returns></returns>
        private Button CreateButton(Point tmpLocation)
        {
            Button b = new Button
            {
                Parent = GameGrid_Panel,
                Size = new Size(s.ButtonSize, s.ButtonSize),
                Location = tmpLocation,
                FlatStyle = FlatStyle.Flat,
            };
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.BorderColor = Color.LightGray;
            return b;
        }

        /// <summary>
        /// Uppdaterar spelplanen.
        /// </summary>
        private void UpdateGrid()
        {
            for (int y = 0; y < s.GridSize;)
            {
                for (int x = 0; x < s.GridSize;)
                {
                    if (s.PastGameTurnArray[x, y] == 1)
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

        /// <summary>
        /// Använder random för att slumpa fram nya tal i array
        /// </summary>
        private void CreateRandomGrid()
        {
            for (int i = 0; i < s.GridSize;)
            {
                for (int j = 0; j < s.GridSize;)
                {
                    s.PastGameTurnArray[i, j] = random.Next(2);
                    j++;
                }
                i++;
            }

            UpdateGrid();
        }

        /// <summary>
        /// Spara spel i databasen.
        /// </summary>
        /// <param name="GameName"></param>
        private void SaveGame(string GameName)
        {
            manageDB.DoSaveGame(GameName);
        }

        /// <summary>
        /// Spara rundor i databasen.
        /// </summary>
        private void SaveRound()
        {
            string currentRound = "";
            foreach (var position in s.PastGameTurnArray)
            {
                currentRound += position + ",";
            }
            manageDB.DoSaveRounds(currentRound, s.GridSize);
        }
        

        private void ResetGame()
        {
            btnStart.Enabled = true;
            s.ClearArray();
            UpdateLoadListBox();
            UpdateGrid();
        }


        /// <summary>
        /// Takes the loaded string and makes it into the GameArrays
        /// </summary>
        private void MakeLoadedRoundToAnArray(string gameRound)
        {
            string[] temparr = gameRound.Split(',');
            int n = 0;
            for (int x = 0; x < s.GridSize; x++)
            {
                for (int y = 0; y < s.GridSize; y++)
                {
                    int i = Int32.Parse(temparr[n]);


                    s.PastGameTurnArray[x, y] = i;
                    n++;
                }
            }
        }

        /// <summary>
        /// Uppdaterar listboxen med de sparade spelen
        /// </summary>
        private void UpdateLoadListBox()
        {
            lstBxSavedGames.Items.Clear();
            using (var context = new DBContext())
            {
                var saves = context.Games;

                foreach (var savedgame in saves)
                {
                    lstBxSavedGames.Items.Add(savedgame.SaveName);
                }
            }
        }

        /// <summary>
        /// För att starta spelet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Run randomiser if not using loaded
            SaveGame("DefaultGameName");
            savegame = true;
            btnPause.Enabled = true;
            btnStart.Enabled = false;
            PlayTimer.Start();
        }

        /// <summary>
        /// För att ladda ett redan sparat spel och spela upp det.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (lstBxSavedGames.SelectedItem != null)
            {
                string gameName = lstBxSavedGames.SelectedItem.ToString();                
                string loadedGameRound = manageDB.GetPlayingfield(gameName);
                if (loadedGameRound != "")
                {
                    s.GridSize = manageDB.GetSavedGridSize();
                    MakeLoadedRoundToAnArray(loadedGameRound);
                    UpdateGrid();
                    btnStart.Enabled = false;
                    savegame = false;
                    PlayTimer.Start();
                }
            }
        }

        /// <summary>
        /// Ta bort spel i db.
        /// </summary>
        /// <param name="GameName"></param>
        public void DeleteGame(string GameName)
        {
            manageDB.DeleteGame(GameName);
        }

        /// <summary>
        /// Knapp för att ta bort spel ur listboxen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (lstBxSavedGames.SelectedItem != null)
            {
                DeleteGame(lstBxSavedGames.SelectedItem.ToString());                
            }
            UpdateLoadListBox();
        }

        /// <summary>
        /// Knapp för att pausa spelet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Pause")
            {
                PlayTimer.Stop();
                btnPause.Text = "Continue";
            }
            else
            {
                btnPause.Text = "Pause";
                PlayTimer.Start();
            }
        }

        /// <summary>
        /// Knapp för att stoppa/spara spelet.
        /// Om man inte skrivit in namn så får den namnet DefaultGameName.
        /// Annars tas den bort.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopSave_Click(object sender, EventArgs e)
        {
            PlayTimer.Stop();

            if (txbNameOfTheGame.Text == "")
                DeleteGame("DefaultGameName");
            else
            {
                manageDB.RenameGame("DefaultGameName", txbNameOfTheGame.Text);
                txbNameOfTheGame.Text = "";
            }
            btnPause.Enabled = false;
            ResetGame();
        }

        /// <summary>
        /// Stänga applicationen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Man kan välja vilka rutor man vill på brädet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Grid_btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int found_x = -1;
            int found_y = -1;

            for (int x = 0; x < s.GridSize && found_x < 0; ++x)
            {
                for (int y = 0; y < s.GridSize; ++y)
                {
                    if (gameButtonGridArray[x, y] == btn)
                    {
                        found_x = x;
                        found_y = y;
                        break;
                    }
                }
            }

            if (btn.BackColor != s.AliveCellColor)
            {
                btn.BackColor = s.AliveCellColor;
                s.PastGameTurnArray[found_x, found_y] = 1;
            }
            else
            {
                btn.BackColor = s.DeadCellColor;
                s.PastGameTurnArray[found_x, found_y] = 0;
            }
            UpdateGrid();
        }

        /// <summary>
        /// Knapp för random
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRandom_Click(object sender, EventArgs e)
        {
            CreateRandomGrid();
        }
    }
}

