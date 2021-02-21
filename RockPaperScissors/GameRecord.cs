using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{
    internal class GameRecord
    {
        public Hand WinningHand { get; }
        public Hand LosingHand { get; }
        public string TimeOfGame { get; }
        public GameRecord(Hand winningHand, Hand losingHand)
        {
            this.WinningHand = winningHand;
            this.LosingHand = losingHand;

            DateTime now = DateTime.Now;
            string date = now.GetDateTimeFormats('d')[0];
            string time = now.GetDateTimeFormats('t')[0];
            TimeOfGame = date + " " + time;

            Program.gameRecords.Add(this);
        }

        public override string ToString()
        {
            return Program.FirstLetterToUpper(this.WinningHand.value) + " " + Program.FirstLetterToUpper(this.LosingHand.value) + " " + this.TimeOfGame;
        }
    }
}
