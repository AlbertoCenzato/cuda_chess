using System;

using Chess.Utils;
using Chess.Pieces;
using Chess.Renderer;

namespace Chess
{
    static class InputManager {

        public static Cell getPosition() {
            int x = 0, y = 0;
            bool done = false;
            do
            {
                var key1 = Console.ReadKey(true).KeyChar;
                if((key1 >= 65 && key1 <= 72) || (key1 >= 97 && key1 <= 104)) {
                    Console.Write(key1.ToString().ToUpper());
                    x = key1 > 72 ? key1-97 : key1-65;
                    done = true;
                }
            }while(!done);

            done = false;
            do
            {
                var key2 = Console.ReadKey(true).KeyChar;
                if(key2 >= 49 && key2 <= 56) {
                    Console.Write(key2);
                    y = key2-49;
                    done = true;
                }
            }while(!done);

            return new Cell(x, y);
        }

        public static Piece getSubstitutivePiece(Chessboard board, Cell pos, Team squad) {
            Console.WriteLine("\nCon che pezzo vuoi sostituire il pedone?");
            byte line = 4;
            ConsoleColor col0, col1, col2, col3 = ConsoleColor.Black;
            bool done = false;
            Console.CursorTop = Console.CursorTop + 4;

            while (!done) {
                Console.CursorTop = Console.CursorTop - 4;
                col0 = ConsoleColor.Black;
                col1 = ConsoleColor.Black;
                col2 = ConsoleColor.Black;
                col3 = ConsoleColor.Black;
                switch (line % 4) {
                    case 0: col0 = ConsoleColor.DarkRed; break;
                    case 1: col1 = ConsoleColor.DarkRed; break;
                    case 2: col2 = ConsoleColor.DarkRed; break;
                    case 3: col3 = ConsoleColor.DarkRed; break;
                }
                GameRenderer.Write("Regina\n",  ConsoleColor.White, col0);
                GameRenderer.Write("Torre\n",   ConsoleColor.White, col1);
                GameRenderer.Write("Cavallo\n", ConsoleColor.White, col2);
                GameRenderer.Write("Alfiere\n", ConsoleColor.White, col3);

                var key = Console.ReadKey(true).Key;
                if(key == ConsoleKey.Enter) break;
                
                if(key == ConsoleKey.UpArrow) line--;
                else if(key == ConsoleKey.DownArrow) line++;
            }
            System.Diagnostics.Debug.Print(">>>{0}", line % 4);
            switch (line % 4) {
                case 0:  return new Queen (squad);
                case 1:  return new Rook  (squad);
                case 2:  return new Knight(squad);
                default: return new Bishop(squad);
            }
        }
    }
}

