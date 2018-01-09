using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    public class Settings // Later we could have table for settings in db so user can set it and save
    {
        //public Button[,] NewGameTurn;
        //public Button[,] PastGameTurn;
        public int[,] NewGameTurn;
        public int[,] PastGameTurn;

        private int gamesize;
        private int gridsize;
        private Color _aliveColor;
        private Color _deadColor;
                

        public int ButtonSize { get; private set; }

        public Color AliveCellColor
        {
            get { return _aliveColor; }
            set { _aliveColor = value; }
        }

        public Color DeadCellColor
        {
            get { return _deadColor; }
            set { _deadColor = value; }
        }

        /// <summary>
        /// Controls the playfield/panel size
        /// </summary>
        public int GameSize
        {
            get { return gamesize; }
            set { gamesize = value;
                UpdateSettings();
            }
        }

        // OBS! Ändrar man gridsize så ändras arayens storlek, vilket förmodigen krashar hela programmet
        /// <summary>
        /// Sets amount of Columns/Rows
        /// </summary>
        public int GridSize
        {
            get { return gridsize; }
            set { gridsize = value;
                //PastGameTurn = null; This solves that^ but will erase current game and other games saved in other size will error
                UpdateSettings();
            }
        }
                
        /// <summary>
        /// Updates values depending on new sets
        /// </summary>
        private void UpdateSettings()
        {
            ButtonSize = gamesize / gridsize;
            NewGameTurn = new int[gridsize, gridsize]; //Måste matcha det man laddar in
            if (PastGameTurn == null) // Om spelplanen(arrayen) är tom/null så skapas ny
                PastGameTurn = new int[gridsize, gridsize];
            //NewGameTurn = new Button[gridsize, gridsize]; //Måste matcha det man laddar in
            //if (PastGameTurn == null) // Om spelplanen(arrayen) är tom/null så skapas ny
            //    PastGameTurn = new Button[gridsize, gridsize];
        }


        public Settings(Panel GamePanel)
        {
            _aliveColor = Color.Green;
            _deadColor = Color.DarkOrange;
            GamePanel.Height = GamePanel.Width; // ensures a square panel
            gamesize = GamePanel.Width;
            gridsize = 10; // Number of Rows/Columns

            UpdateSettings();
        }
    }
}
