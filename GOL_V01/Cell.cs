using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    class Cell
    {
        public Button Position { get; private set; }
        public bool IsAlive { get; set; }

        public Cell(Button postition)
        {
            Position = postition;
            IsAlive = false;

        }

        //public void Update (MouseState mouseState)
        //{

        //}
    }
}
