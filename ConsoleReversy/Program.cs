﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using ReversyEngine;

namespace ConsoleReversy
{

    public class Params : ReversyEngine.CoreInit
    {
        public string ColorPlayer1 => "#FFFFFF";

        public string ColorPlayer2 => "#000000";

        public ReversyEngine.SizeField Size => new ReversyEngine.SizeField() { X = 8, Y = 8 };

        public string StartPattern => @"00000000|
                                        00000000|
                                        00000000|
                                        00012000|
                                        00021000|
                                        00000000|
                                        00000000|
                                        00000000";
    }

    public class Program
    {
        public static ReversyEngine.Core _core;

        #region Application Properties
        static ConsoleColor background = ConsoleColor.Black;
        static ConsoleColor foreground = ConsoleColor.White;
        public static int WindowWidth = 0;
        public static int WindowHeight = 0;

        static char PatternLine = '#';
        #endregion

        #region Score Properties
        static ConsoleColor backgroundUnactivePlayer = ConsoleColor.Gray;
        static ConsoleColor foregroundUnactivePlayer = ConsoleColor.Black;
        static ConsoleColor backgroundActivePlayer = ConsoleColor.Blue;
        static ConsoleColor foregroundActivePlayer = ConsoleColor.White;
        static int MarginScore = 3;
        static int WhitespacesNickname = 2;

        static string PatternScore = "{0}: {1:000}";
        #endregion

        #region Field Properties
        static ConsoleColor backgroundField = ConsoleColor.Cyan;
        static ConsoleColor foregroundField = ConsoleColor.DarkGreen;
        static ConsoleColor foregroundFieldHeader = ConsoleColor.Black;
        static ConsoleColor[] foregroundPlayer = new ConsoleColor[]
        {
            ConsoleColor.White,
            ConsoleColor.Black
        };

        static int CellWidth = 3;
        static int FieldMargin = 8;

        static string PatternChip = " o ";
        static string PatternCell = " # ";
        static string PatternColumn = " {0} ";
        static string PatternRow = " {0:00} ";
        static string PatternDelta = "    ";
        #endregion

        #region GameOver Properties
        static ConsoleColor backgroundGameOver = ConsoleColor.Red;
        static ConsoleColor foregroundGameOver = ConsoleColor.Black;

        static string GameOverLabel = " *** GAME OVER *** ";
        #endregion


        static void Main(string[] args)
        {
            _core = new ReversyEngine.CoreComputerEnemy(new Params())
            {
                Finder = new ReversyEngine.LineFinder()
            };
            _core.MovingHandler = OnMoving;
            _core.StateChanged += OnStatusChanged;
            Console.SetWindowSize(100, 30);
            WindowWidth = Console.WindowWidth;
            PrintGameScreen();
            Game();
        }


        static void Game() 
        {
            SetActivePlayer(ReversyEngine.Player.Player1);
            SetUnactivePlayer(ReversyEngine.Player.Player2);
            while (_core.State != ReversyEngine.CoreState.GameOver) 
            {

                Console.SetCursorPosition(0, 23);
                Console.WriteLine(new string(' ',WindowWidth));
                Console.WriteLine(new string(' ',WindowWidth));
                try
                {
                    Console.SetCursorPosition(0, 23);
                    Console.Write("\tSelect current column (A-{0}): ", (char) ('A' + _core.CurrentSize.X - 1));
                    char col = char.Parse(Console.ReadLine());
                    Console.Write("\tSelect current row (0-{0}): ", _core.CurrentSize.Y - 1);
                    int row = int.Parse(Console.ReadLine());

                    Console.Write(new string(' ',WindowWidth));
                    Console.Write("\r");
                    if ((col - 'A') < 0 || (col - 'A') >= _core.CurrentSize.X || row < 0 || row >= _core.CurrentSize.Y)
                    {
                        Console.WriteLine("\tUnknown column or row, please input again");
                        continue;
                    }

                    if (!_core.IsCanClicked(new ReversyEngine.Position((col - 'A'), row)))
                    {
                        Console.WriteLine("\tCell ({0},{1}) is can't selected", col, row);
                        continue;
                    }
                    _core.ClickedCell(_core.Field[(col - 'A'), row]);
                    _core.NextState();
                    Console.WriteLine("\tSelected cell ({0},{1})", (col - 'A'), row);
                }
                catch 
                {
                    Console.WriteLine();
                    Console.WriteLine("\tUnknown column or row, please input again");
                }
            }
        }

        static void OnStatusChanged(ReversyEngine.Core core) 
        {
            switch (core.State) 
            {
                case ReversyEngine.CoreState.Waiting:
                    SetActiveScorePlayer(core.CurrentPlayer);
                    break;
                case ReversyEngine.CoreState.GameOver:
                    PrintGameOver();
                    break;
            }
        }

        static void OnMoving(ReversyEngine.Core core, IEnumerable<ReversyEngine.Cell> cells) 
        {
            foreach (var cell in cells) 
                SetChip(cell.Chip.Player, cell.Position);
            core.NextState();
        }

        public static void SetPositionField(ReversyEngine.Position position)
        {
            var delta = (Console.WindowWidth - _core.CurrentSize.X * 3 ) / 2;
            Console.SetCursorPosition(delta + position.X * 3, 6 + position.Y * 2);

        }

        public static void SetChip(ReversyEngine.Player player, ReversyEngine.Position position)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            var background = Console.BackgroundColor;
            var foreground = Console.ForegroundColor;

            SetPositionField(position);
            Console.BackgroundColor = backgroundField;
            Console.ForegroundColor = foregroundPlayer[(int)player - 1];
            Console.Write(PatternChip);

            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.SetCursorPosition(left, top);
        }


        public static void PrintGameScreen() 
        {
            Console.WriteLine();
            PrintLine();
            PrintScore();
            PrintLine();
            Console.WriteLine();
            PrintField();
        }

        public static void PrintLine() 
        {
            Console.WriteLine(new string(PatternLine,WindowWidth));
        }

        public static void PrintScore()
        {
            var player1 = GetPlayerScore(ReversyEngine.Player.Player1);
            var player2 = GetPlayerScore(ReversyEngine.Player.Player2);
            var width = WindowWidth - player1.Length - player2.Length - (MarginScore + WhitespacesNickname) * 2;
            var margin = new string(PatternLine, MarginScore);
            var center = new string(PatternLine, width);

            Console.WriteLine("{0} {2} {1} {3} {0}", margin, center, player1, player2);
        }

        public static void SetActiveScorePlayer(Player player) 
        {
            SetActivePlayer(player); SetUnactivePlayer(GetOtherPlayer(player));
        }

        public static Player GetOtherPlayer(Player player) => player == Player.Player1 ? Player.Player2 : Player.Player1;

        public static void SetActivePlayer(ReversyEngine.Player player) => PrintScorePlayer(player, backgroundActivePlayer, foregroundActivePlayer);
        public static void SetUnactivePlayer(ReversyEngine.Player player) =>  PrintScorePlayer(player, backgroundUnactivePlayer, foregroundUnactivePlayer);


        static void PrintScorePlayer(ReversyEngine.Player player, ConsoleColor background, ConsoleColor foreground) 
        {
            var oldBackground = Console.BackgroundColor;
            var oldForeground = Console.ForegroundColor;
            var left = Console.CursorLeft;
            var top = Console.CursorTop;

            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            switch (player) 
            {
                case ReversyEngine.Player.Player1:
                    Console.SetCursorPosition(3, 2);
                    Console.Write(" {0} ", GetPlayerScore(player));
                    break;
                case ReversyEngine.Player.Player2:
                    var player2 = GetPlayerScore(ReversyEngine.Player.Player2);
                    var delta =WindowWidth - player2.Length - 5;
                    Console.SetCursorPosition(delta, 2);
                    Console.Write(" {0} ", player2);
                    break;
            }
            Console.BackgroundColor = oldBackground;
            Console.ForegroundColor = oldForeground;
            Console.SetCursorPosition(left, top);
        }

        static string GetPlayerScore(ReversyEngine.Player player) 
        {
            return string.Format(PatternScore, player, _core.CountChips[player]);
        }

        static void PrintGameOver()
        {
            Console.SetCursorPosition(0, 4 + _core.CurrentSize.Y );
            Console.BackgroundColor = backgroundGameOver;
            Console.ForegroundColor = foregroundGameOver;
            PrintLine();
            var delta = (Console.WindowWidth - GameOverLabel.Length) / 2;
            Console.WriteLine("{0}{1}{0}", new string(PatternLine, delta), GameOverLabel);
            PrintLine();
        }

        public static void PrintField()
        {
            var delta = new string(' ', (WindowWidth - _core.CurrentSize.X * 3 - 8) / 2);
            Console.Write(delta);
            SetFieldHeaderColor();
            Console.Write(PatternDelta);

            for (int x = 0; x < _core.CurrentSize.X; x++) 
                Console.Write(PatternColumn, (char)('A' + x));

            Console.WriteLine(PatternDelta);
            for (int y = 0; y < _core.CurrentSize.Y; y++)
            {
                SetDefaultColor();
                Console.Write(delta);
                SetFieldHeaderColor();
                Console.Write(PatternRow, y);

                for (int x = 0; x < _core.CurrentSize.X; x++)
                    if (_core.Field[x, y].Chip is null)
                    {
                        SetFieldColor();
                        Console.Write(PatternCell);
                    }
                    else 
                    {
                        Console.ForegroundColor = foregroundPlayer[(int)_core.Field[x, y].Chip.Player - 1];
                        Console.Write(PatternChip);
                    }

                Console.WriteLine(PatternDelta);
                SetDefaultColor();
                Console.Write(delta);
                SetFieldColor();
                Console.WriteLine(new string(' ', _core.CurrentSize.X * 3 + 8));
            }
            SetDefaultColor();
        }

        static void SetFieldHeaderColor()
        {
            Console.BackgroundColor = backgroundField;
            Console.ForegroundColor = foregroundFieldHeader;
        }

        static void SetFieldColor()
        {
            Console.BackgroundColor = backgroundField;
            Console.ForegroundColor = foregroundField;
        }

        static void SetDefaultColor()
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }
    }
}
