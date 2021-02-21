using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{
    internal class Hand
    {
        public string value;
    }

    internal class Rock : Hand
    {
        public Rock()
        {
            this.value = "rock";
        }
    }

    internal class Paper : Hand
    {
        public Paper()
        {
            this.value = "paper";
        }
    }

    internal class Scissors : Hand
    {
        public Scissors()
        {
            this.value = "scissors";
        }
    }
}
