using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{
    internal class GameRecord
    {
        public string Winner { get; set; }
        public Hand WinningHand { get; }
        public Hand LosingHand { get; }
        public string TimeOfGame { get; }
        public GameRecord(string winner, Hand WinningHand, Hand LosingHand)
        {
            this.WinningHand = WinningHand;
            this.LosingHand = LosingHand;
            this.Winner = winner;

            //DateTime now = DateTime.Now;
            //string date = now.GetDateTimeFormats('d')[0];
            //string time = now.GetDateTimeFormats('t')[0];
            this.TimeOfGame = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");

            Program.gameRecords.Add(this);

            Program.InsertRecord(this);
        }

        public GameRecord(string Winner, Hand WinningHand, Hand LosingHand, string TimeOfGame)
        {
            this.Winner = Winner;
            this.WinningHand = WinningHand;
            this.LosingHand = LosingHand;
            this.TimeOfGame = TimeOfGame;

            Program.gameRecords.Add(this);
        }

        public override string ToString()
        {
            return this.Winner + " " + Program.FirstLetterToUpper(this.WinningHand.value) + " " + Program.FirstLetterToUpper(this.LosingHand.value) + " " + this.TimeOfGame;
        }
    }
}
