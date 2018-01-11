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
        public int[,] NewGameTurnArray;
        public int[,] PastGameTurnArray;

        private int gameSize;
        private int gridSize;
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
            get { return gameSize; }
            set { gameSize = value;
                UpdateSettings();
            }
        }

        // OBS! Ändrar man gridsize så ändras arayens storlek, vilket förmodigen krashar hela programmet
        /// <summary>
        /// Sets amount of Columns/Rows
        /// </summary>
        public int GridSize
        {
            get { return gridSize; }
            set { gridSize = value;
                //PastGameTurn = null; This solves that^ but will erase current game and other games saved in other size will error
                UpdateSettings();
            }
        }

        /// <summary>
        /// Updates values depending on new sets
        /// </summary>
        private void UpdateSettings()
        {
            ButtonSize = gameSize / gridSize;
            NewGameTurnArray = new int[gridSize, gridSize]; //Måste matcha det man laddar in
            if (PastGameTurnArray == null) // Om spelplanen(arrayen) är tom/null så skapas ny
                PastGameTurnArray = new int[gridSize, gridSize];
        }


        public Settings(Panel GamePanel)
        {
            _aliveColor = Color.Black;
            _deadColor = Color.Transparent;
            GamePanel.Height = GamePanel.Width; // ensures a square panel
            gameSize = GamePanel.Width;
            gridSize = 3; // Number of Rows/Columns

            UpdateSettings();
        }
    }
}
