using System;
//Fridas

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    class MoveLogic // Move to game or Cell?
    {        
        Settings s;
        private int _gridsize { get; set; }

        public MoveLogic(Settings settings)
        {
            s = settings;
            _gridsize = s.GridSize;
        }

        public void PlayRound(Settings s)
        {
            bool cellstatealive;
            for (int y = 0; y < _gridsize; y++)
            {
                for (int x = 0; x < _gridsize; x++)
                {
                    int neighboursalive = CheckCellNeighbourhood(s.PastGameTurn, x, y);
                    cellstatealive = DoRulesMath(neighboursalive, CheckCellState(s.PastGameTurn[x, y]));

                    if (cellstatealive == true)
                        s.NewGameTurn[x, y] = 1;
                    else
                        s.NewGameTurn[x, y] = 0;
                }
            }
        }

        /// <summary>
        /// Checks if the surrounding cells are alive
        /// </summary>
        private int CheckCellNeighbourhood(int[,] IntArray, int PosX, int PosY)
        {
            int boundary = _gridsize -1;
            int closneighboursPosX = PosX - 1;
            int closneighboursPosY = PosY - 1;
            int aliveneighbours = 0;
            bool positionlive;
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (PosX == closneighboursPosX && PosY == closneighboursPosY) { }
                        //Do nothing
                    else if ((closneighboursPosX <= boundary && closneighboursPosX >= 0) && (closneighboursPosY <= boundary && closneighboursPosY >= 0)) // If it's WITHIN the bondary/grid
                    {
                        positionlive = CheckCellState(IntArray[closneighboursPosX, closneighboursPosY]);
                        if (positionlive == true)
                            aliveneighbours++;
                    }
                    else
                    {
                        positionlive = false;
                    }
                    closneighboursPosX++;
                }

                closneighboursPosX = PosX - 1;
                closneighboursPosY++;
            }
            return aliveneighbours;
        }

        /// <summary>
        /// Applies the rules of the game
        /// </summary>
        private bool DoRulesMath(int AliveNeighbours, bool IsAlive)
        {
            if (AliveNeighbours < 2)
                return false;

            else if (IsAlive && AliveNeighbours == 2)
                return true;

            else if (AliveNeighbours == 3)
                return true;

            else
                return false;
        }

        /// <summary>
        /// Checks if the cell is alive
        /// </summary>
        private bool CheckCellState(int cell)
        {
            if (cell == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Sets the cell to 1 if bool is true
        /// </summary>
        private void SetCellAliveState(bool alive, int cell)
        {
            if (alive)
                cell = 1;
            else
                cell = 0;
        }
        
        /// <summary>
        /// Formatted string of the rules of the game
        /// </summary>
        public string GetRules()
        {
            string rules = "Game of Life: Rules: \n" +
                "Any live cell with fewer than two live neighbours dies, as if caused by underpopulation. \n" +
                "Any live cell with two or three live neighbours lives on to the next generation. \n" +
                "Any live cell with more than three live neighbours dies, as if by overpopulation. \n" +
                "Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.\n\n" +
                "All of these rules will be applied simultaneously";
            return rules;
        }
    }
}
