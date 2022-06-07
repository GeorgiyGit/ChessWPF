﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFTestChess
{
    internal class Knight : Piece
    {
        public Knight(PieceColors color, PieceType type, MainWindow window, string texturePath, Chessboard chessboard, PointInt boardPointInt) :
            base(color, type, window, texturePath, chessboard, boardPointInt)
        { }

        public override void Move(Piece[,] board)
        {
            movePointInts.Clear();
            attackPointInts.Clear();

            AddNumb(board,(int)ChessboardPointInt.X - 1, (int)ChessboardPointInt.Y + 2);
            AddNumb(board, (int)ChessboardPointInt.X + 1, (int)ChessboardPointInt.Y + 2);

            AddNumb(board, (int)ChessboardPointInt.X -1, (int)ChessboardPointInt.Y - 2);
            AddNumb(board, (int)ChessboardPointInt.X +1, (int)ChessboardPointInt.Y - 2);

            AddNumb(board, (int)ChessboardPointInt.X +2, (int)ChessboardPointInt.Y + 1);
            AddNumb(board, (int)ChessboardPointInt.X +2, (int)ChessboardPointInt.Y - 1);

            AddNumb(board, (int)ChessboardPointInt.X - 2, (int)ChessboardPointInt.Y + 1);
            AddNumb(board, (int)ChessboardPointInt.X - 2, (int)ChessboardPointInt.Y - 1);
        }
        public void AddNumb(Piece[,] board,int x, int y)
        {
            if (x >= 0 && x < Chessboard.CELL_COUNT && y >= 0 && y < Chessboard.CELL_COUNT)
            {
                if (board[y, x] != null)
                {
                    if (board[y, x].Color != Color) attackPointInts.Add(new PointInt(x, y));
                }
                else movePointInts.Add(new PointInt(x, y));
            }
        }
    }
}
