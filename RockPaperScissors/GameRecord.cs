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
        public GameRecord(Hand WinningHand, Hand LosingHand)
        {
            this.WinningHand = WinningHand;
            this.LosingHand = LosingHand;

            DateTime now = DateTime.Now;
            string date = now.GetDateTimeFormats('d')[0];
            string time = now.GetDateTimeFormats('t')[0];
            TimeOfGame = date + " " + time;

            Program.gameRecords.Add(this);
        }

        public GameRecord(Hand WinningHand, Hand LosingHand, string TimeOfGame)
        {
            this.WinningHand = WinningHand;
            this.LosingHand = LosingHand;
            this.TimeOfGame = TimeOfGame;

            Program.gameRecords.Add(this);
        }

        public override string ToString()
        {
            return Program.FirstLetterToUpper(this.WinningHand.value) + " " + Program.FirstLetterToUpper(this.LosingHand.value) + " " + this.TimeOfGame;
        }
    }
}
