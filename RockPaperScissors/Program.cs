using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RockPaperScissors
{
    internal static class Program
    {
        private const string title = "###\nRock Paper Scissors!\n###";

        // main menu options
        public static string selectionOptionString = "Select Option:";

        public static string newGameString = "n: New Game";
        public static string createPlayerString = "p: Create New Player";
        public static string loadGameString = "l: Load Game";
        public static string saveGameString = "s: Save Game";
        public static string viewStatsString = "v: View Stats";
        public static string viewGameRecordsString = "r: View Game Records";
        public static string quitGameString = "q: Quit";

        public static string mainMenuString =
            selectionOptionString + "\n" +
            newGameString + "\n" +
            createPlayerString + "\n" +
            loadGameString + "\n" +
            saveGameString + "\n" +
            viewStatsString + "\n" +
            viewGameRecordsString + "\n" +
            quitGameString;

        // game menu options
        public static string selectHandString = "Select Hand:";

        public static string selectPaperString = "p: Paper";
        public static string selectScissorsString = "s: Scissors";
        public static string selectRockString = "r: Rock";

        public static string roundString =
            selectHandString + "\n" +
            selectPaperString + "\n" +
            selectScissorsString + "\n" +
            selectRockString;

        // stats
        private static int gamesCounter = 0;

        public static int playerPaperCounter = 0;
        public static int playerScissorsCounter = 0;
        public static int playerRockCounter = 0;

        public static int compPaperCounter = 0;
        public static int compScissorsCounter = 0;
        public static int compRockCounter = 0;

        public static int winCounter = 0;
        public static int loseCounter = 0;
        public static int drawCounter = 0;

        // list of game records
        public static List<GameRecord> gameRecords = new List<GameRecord>();

        private const string gameRecordsFilePath = @"C:\Users\bunchmatt\source\repos\RockPaperScissorsCSharp\RockPaperScissors\gamerecords.txt";
        private const string gameStatisticsFilePath = @"C:\Users\bunchmatt\source\repos\RockPaperScissorsCSharp\RockPaperScissors\gamestatistics.txt";


        private static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Console.WriteLine(title);
            while (true)
            {
                ShowMainMenu();
            }
        }

        private static void ShowMainMenu()
        {
            Console.WriteLine(mainMenuString);

            const bool incorrectInput = true;
            while (incorrectInput)
            {
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "n")
                {
                    PlayGame();
                }
                else if (userInput == "l")
                {
                    LoadGame();
                }
                else if (userInput == "s")
                {
                    SaveGame();
                }
                else if (userInput == "v")
                {
                    PrintStats();
                }
                else if (userInput == "r")
                {
                    PrintGameRecords();
                }
                else if (userInput == "q")
                {
                    QuitGame();
                }
                else
                {
                    PrintIncorrectCommand();
                }

            }
        }

        private static void PlayGame()
        {
            PrintLineBreak();

            gamesCounter++;
            Console.WriteLine(roundString);

            Hand userHand = new Hand();
            bool incorrectInput = true;
            while (incorrectInput)
            {
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "p")
                {
                    playerPaperCounter++;
                    userHand = new Paper();
                    incorrectInput = false;
                }
                else if (userInput == "s")
                {
                    playerScissorsCounter++;
                    userHand = new Scissors();
                    incorrectInput = false;
                }
                else if (userInput == "r")
                {
                    playerRockCounter++;
                    userHand = new Rock();
                    incorrectInput = false;
                }
                else
                {
                    PrintIncorrectCommand();
                }
            }

            Console.WriteLine("Player selected hand: " + FirstLetterToUpper(userHand.value));

            Hand compHand = ReturnRandomHand();

            Console.WriteLine("Computer selected hand: " + FirstLetterToUpper(compHand.value));

            Hand winningHand = CompareTwoHands(userHand, compHand);

            string winningValue;

            if (winningHand == null)
            {
                drawCounter++;
                Console.WriteLine("Draw!");
            }
            else if (winningHand.value == userHand.value && winningHand.value != compHand.value)
            {
                winningValue = FirstLetterToUpper(winningHand.value);
                winCounter++;
                Console.WriteLine("Winning value: " + winningValue);
                Console.WriteLine("You won!");
            }
            else if (winningHand.value == compHand.value && winningHand.value != userHand.value)
            {
                winningValue = FirstLetterToUpper(winningHand.value);
                loseCounter++;
                Console.WriteLine("Winning value: " + winningValue);
                Console.WriteLine("You lost!");
            }
            else
            {
                Console.WriteLine("Error!");
            }

            new GameRecord(userHand, compHand);

            PrintLineBreak();

            ShowMainMenu();
        }
        private static Hand ReturnRandomHand()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 4);
            switch (num)
            {
                case 1:
                    compPaperCounter++;
                    return new Paper();

                case 2:
                    compScissorsCounter++;
                    return new Scissors();

                case 3:
                    compRockCounter++;
                    return new Rock();

                default:
                    return null;
            }
        }

        private static Hand CompareTwoHands(Hand hand1, Hand hand2)
        {
            if (hand1.value == "rock" && hand2.value == "rock")
            {
                return null;
            }
            else if (hand1.value == "rock" && hand2.value == "scissors")
            {
                return hand1;
            }
            else if (hand1.value == "rock" && hand2.value == "paper")
            {
                return hand2;
            }
            else if (hand1.value == "paper" && hand2.value == "paper")
            {
                return null;
            }
            else if (hand1.value == "paper" && hand2.value == "rock")
            {
                return hand1;
            }
            else if (hand1.value == "paper" && hand2.value == "scissors")
            {
                return hand2;
            }
            else if (hand1.value == "scissors" && hand2.value == "scissors")
            {
                return null;
            }
            else if (hand1.value == "scissors" && hand2.value == "paper")
            {
                return hand1;
            }
            else if (hand1.value == "scissors" && hand2.value == "rock")
            {
                return hand2;
            }
            else
            {
                Console.WriteLine("this shouldn't be hit!");
                return null;
            }
        }

        private static void LoadGame()
        {
            LoadGameRecords();

            LoadStatistics();

            PrintLineBreak();
            ShowMainMenu();
        }

        private static void LoadGameRecords()
        {
            _ = new List<string>();

            List<string> lines = File.ReadAllLines(gameRecordsFilePath).ToList();

            foreach (string line in lines)
            {
                string[] gameRecord = line.Split(' ');
                Hand playerHand = StringToHand(gameRecord[0]);
                Hand compHand = StringToHand(gameRecord[1]);
                string date = gameRecord[2] + " " + gameRecord[3] + " " + gameRecord[4];

                new GameRecord(playerHand, compHand, date);
            }

            Console.WriteLine(lines.Count + " records added!");
        }

        private static void LoadStatistics()
        {
            PrintLineBreak();

            List<string> lines = File.ReadAllLines(gameStatisticsFilePath).ToList();
            List<int> intLines = StringListToIntList(lines);

            gamesCounter = intLines[0];

            playerPaperCounter = intLines[1];
            playerScissorsCounter = intLines[2];
            playerRockCounter = intLines[3];

            compPaperCounter = intLines[4];
            compScissorsCounter = intLines[5];
            compRockCounter = intLines[6];

            winCounter = intLines[7];
            loseCounter = intLines[8];
            drawCounter = intLines[9];

            Console.WriteLine("Statistics loaded.");

        }

        private static List<int> StringListToIntList(List<string> input)
        {
            List<int> output = new List<int>();

            foreach (string line in input)
                output.Add(int.Parse(line));

            return output;
        }

        private static void SaveGame()
        {
            SaveGameRecords();

            SaveGameStatistics();

            ShowMainMenu();
        }

        private static void SaveGameRecords()
        {
            List<string> lines = new List<string>();
            foreach (GameRecord gameRecord in gameRecords)
                lines.Add(gameRecord.ToString());

            File.WriteAllLines(gameRecordsFilePath, lines);

            Console.WriteLine(lines.Count + " records saved!");
        }

        private static void SaveGameStatistics()
        {
            List<string> lines = new List<string>
            {
                gamesCounter.ToString(),

                playerPaperCounter.ToString(),
                playerScissorsCounter.ToString(),
                playerRockCounter.ToString(),

                compPaperCounter.ToString(),
                compScissorsCounter.ToString(),
                compRockCounter.ToString(),

                winCounter.ToString(),
                loseCounter.ToString(),
                drawCounter.ToString()
            };

            File.WriteAllLines(gameStatisticsFilePath, lines);

            Console.WriteLine("Statistics saved.");
        }

        private static Hand StringToHand(string input)
        {
            return input switch
            {
                "Paper" => new Paper(),
                "Scissors" => new Scissors(),
                "Rock" => new Rock(),
                _ => null,
            };
        }

        private static void PrintStats()
        {
            PrintLineBreak();

            // print stats
            Console.WriteLine("Games Played: " + gamesCounter);
            Console.WriteLine("Wins: " + winCounter);
            Console.WriteLine("Losses: " + loseCounter);
            Console.WriteLine("Draws: " + drawCounter);
            Console.WriteLine("Player played Paper: " + playerPaperCounter);
            Console.WriteLine("Player played Scissors: " + playerScissorsCounter);
            Console.WriteLine("Player played Rock: " + playerRockCounter);
            Console.WriteLine("Player's favorite hand: " + ReturnFavoriteHand(playerPaperCounter, playerScissorsCounter, playerRockCounter));
            Console.WriteLine("Comp played Paper: " + compPaperCounter);
            Console.WriteLine("Comp played Scissors: " + compScissorsCounter);
            Console.WriteLine("Comp played Rock: " + compRockCounter);
            Console.WriteLine("Comp's favorite hand: " + ReturnFavoriteHand(compPaperCounter, compScissorsCounter, compRockCounter));

            PrintLineBreak();
            ShowMainMenu();
        }

        private static string ReturnFavoriteHand(int paperCounter, int scissorsCounter, int rockCounter)
        {
            if (paperCounter > scissorsCounter && paperCounter > rockCounter)
            {
                return "Paper";
            }
            else if (scissorsCounter > paperCounter && scissorsCounter > rockCounter)
            {
                return "Scissors";
            }
            else if (rockCounter > paperCounter && rockCounter > scissorsCounter)
            {
                return "Rock";
            }
            else if (paperCounter == scissorsCounter && paperCounter > rockCounter)
            {
                return "Paper and Scissors";
            }
            else if (paperCounter == rockCounter && paperCounter > scissorsCounter)
            {
                return "Paper and Rock";
            }
            else if (scissorsCounter == rockCounter && scissorsCounter > paperCounter)
            {
                return "Scissors and Rock";
            }
            else if (paperCounter == scissorsCounter && paperCounter == rockCounter)
            {
                return "All three are tied!";
            }
            return "";
        }

        private static void PrintGameRecords()
        {
            PrintLineBreak();

            if (gameRecords.Count == 0)
            {
                Console.WriteLine("No games played");
            }
            else
            {
                foreach (GameRecord gr in gameRecords)
                {
                    Console.WriteLine(gr.ToString());
                }
            }

            PrintLineBreak();

            ShowMainMenu();
        }

        private static void QuitGame()
        {
            Console.WriteLine("Goodbye.");
            System.Environment.Exit(1);
        }

        public static string FirstLetterToUpper(String input)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }

        private static void PrintLineBreak()
        {
            Console.WriteLine("\n");
        }

        private static void PrintIncorrectCommand()
        {
            Console.WriteLine("Invalid Command. Please Try Again.");
        }
    }
}