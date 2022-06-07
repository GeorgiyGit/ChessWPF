using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTestChess
{
    public enum GameState
    {
        WhiteWin,
        BlackWin,
        WhiteCheck,
        BlackCheck,
        NoWiners
    }

    enum PieceColors
    {
        White = 1,
        Black
    }
    enum PieceType
    {
        Pawn = 1,//пішка
        Knight,//Кінь
        Bishop,//Слон
        Rook,//Тура
        Queen,//Ферзь
        King,
        None
    }

    enum KingState
    {
        Normal,
        Check,//Шаг
        CheckMate,//Мат
        Error
    }
}
