﻿using System;
using System.Globalization;
using System.Linq;
using TicTacToe.Common;
using TicTacToe.Common.Entities;
using TicTacToe.Common.Factories;
using TicTacToe.Common.Repositories;

namespace TicTacToe.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var game = new GameManager(new PlayerRepository(), new BoardFactory());

            game.BoardUpdated += Game_Changed;
            game.Ended += Game_Ended;

            bool play = true;
            while (play)
            {
                System.Console.WriteLine("Spela? j eller n:");
                var answer = System.Console.ReadLine();
                if (answer != null) 
                    play = answer.Equals("j", StringComparison.InvariantCultureIgnoreCase);
                
                if (play)
                    game.Play();
            }
        }

        static void Game_Ended(object sender, EventArgs e)
        {
            System.Console.WriteLine("Spelet avslutades oavgjort!");
        }

        static void Game_Changed(object sender, BoardEventArgs e)
        {
            PrintBoard(e.CurrentBoard);

            if (!string.IsNullOrEmpty(e.Message))
            {
                System.Console.WriteLine(e.Message);
            }
        }

        private static void PrintBoard(Board board)
        {
            System.Console.WriteLine("---------------------");

            int rows = board.BoundaryY;
            for (int i = 1; i <= rows; i++)
            {
                //Hämta alla discs på denna rad
                var discsOnRow = board.DiscsOnBoard.Where(d => d.Y == i);

                var rowString = string.Empty;

                for (int j = 1; j <= board.BoundaryX; j++)
                {
                    //Om platsen är kryssad
                    var disc = discsOnRow.FirstOrDefault(d => d.X == j);
                    rowString += (disc != null ? disc.PlayerName[0].ToString(CultureInfo.InvariantCulture) : "-") + " ";
                }

                System.Console.WriteLine(rowString);

            }

        }
    }
}
