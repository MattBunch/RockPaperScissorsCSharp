using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{
    internal class Player
    {
        public string name;
        public Hand hand;

        public Player(string name)
        {
            this.name = name;
            this.hand = null;
        }
    }
}
