using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHangman {
    class Transform {
        int posX;
        public int posY;

        public int PosX { get { return posX; } set { posX = value; } }

        public bool Metodo () {
            Console.WriteLine (posX);
            return false;
        }

        public Transform () {

        }
    }
}
