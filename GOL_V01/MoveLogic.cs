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
        public Color _alivecellcolor { get; set; }
        private Color _deadcellcolor { get; set; }
        Settings s;

        public MoveLogic(Settings settings)
        {
            s = settings;
            //_alivecellcolor = s.AliveCellColor;
            //_deadcellcolor = s.DeadCellColor;
        }

        public void PlayRound(Settings s)
        {
            bool cellstatealive;
            for (int y = 0; y < s.GridSize; y++) // -1?
            {
                for (int x = 0; x < s.GridSize; x++)
                {
                    int neighboursalive = CheckCellArea(s.PastGameTurn, x, y);
                    cellstatealive = DoRulesMath(neighboursalive, CheckIfCellAlive(s.PastGameTurn[x, y]));

                    if (cellstatealive == true)
                        s.NewGameTurn[x, y] = 1;
                    else
                        s.NewGameTurn[x, y] = 0;

                    
                }
            }
        }

        private int CheckCellArea(int[,] IntArray, int PosX, int PosY)
        {
            int boundary = (int)Math.Sqrt(IntArray.Length) - 1;
            int newPosX = PosX - 1;
            int newPosY = PosY - 1;
            int livecount = 0;
            bool positionlive;


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (PosX == newPosX && PosY == newPosY) { }
                    //Do nothing

                    else if ((newPosX <= boundary && newPosX >= 0) && (newPosY <= boundary && newPosY >= 0)) // If it's WITHIN the bondary
                    {
                        positionlive = CheckIfCellAlive(IntArray[newPosX, newPosY]);
                        if (positionlive == true)
                            livecount++;
                    }
                    else
                    {
                        positionlive = false;
                    }
                    newPosX++;
                }

                newPosX = PosX - 1;
                newPosY++;
            }
            return livecount;
        }

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

        private bool CheckIfCellAlive(int cell)
        {
            if (cell == 1)
                return true;

            else
                return false;
        }

        public void SetCellAliveState(bool alive, int cell)
        {
            if (alive)
                cell = 1;
            else
                cell = 0;
        }

        
        public string Rules()
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
