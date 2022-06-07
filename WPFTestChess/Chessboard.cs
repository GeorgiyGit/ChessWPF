using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFTestChess
{
    internal class Chessboard
    {
        public const int CELL_COUNT = 8;

        public Board board;


        public static bool IsWhiteButton { get; set; }
        public MainWindow window;


        public PieceSelectionCreate pSelCreate;
        public GameOver gameOver;
        public Chessboard(MainWindow window, MouseClickD execute, IsMouseClickD canExecute, PieceSelectionCreate pSelCreate, GameOver gameOver)
        {
            this.window = window;

            this.gameOver = gameOver;
            IsWhiteButton = true;

            board = new Board(window, this, execute.Invoke, canExecute.Invoke);

            SelectedColor = PieceColors.White;

            this.pSelCreate = pSelCreate;
        }
        public static PieceColors SelectedColor { get; set; }
        public static void ChangeColor()
        {
            if(SelectedColor == PieceColors.White) SelectedColor = PieceColors.Black;
            else SelectedColor = PieceColors.White;
        }

        private bool isCanSelected = true;
        public void MovePiece(ref BoardCell selectedBC, ref BoardCell bc)
        {
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    board.board[y, x].BackCell.IsCircle = false;
                    board.board[y, x].BackCell.IsPressedFalse = false;
                }
            }


            if (bc.Piece != null && isCanSelected && bc.Piece.Color== SelectedColor)
            {
                isCanSelected = false;
                selectedBC = bc;
                selectedBC.Piece.Move(Converter.BoardCellsToPieces(board.board));

                foreach (var p in selectedBC.Piece.movePointInts)
                {
                    board.board[p.Y, p.X].BackCell.IsCircle = true;
                    board.board[p.Y, p.X].BackCell.IsAttackCircle = false;
                }

                foreach (var p in selectedBC.Piece.attackPointInts)
                {
                    board.board[p.Y, p.X].BackCell.IsCircle = true;
                    board.board[p.Y, p.X].BackCell.IsAttackCircle = true;
                }

                selectedBC.BackCell.IsPressedTrue = true;
            }
            else if (selectedBC != null)
            {
                board.MovePiece(selectedBC.Point, bc.Point,ref isCanSelected);
                if (!PawnInLastCell()) isCanSelected = true;
            }
            else isCanSelected = true;
            if (selectedBC?.Piece?.Color == bc?.Piece?.Color)
            {
                if (selectedBC?.Piece?.Type == PieceType.King &&
                    bc?.Piece?.Type == PieceType.Rook)
                {
                    board.Roque(selectedBC.Point, bc.Point, ref isCanSelected);
                }
                else if (selectedBC?.Piece?.Type == PieceType.Rook &&
                    bc?.Piece?.Type == PieceType.King)
                {
                    board.Roque(bc.Point, selectedBC.Point, ref isCanSelected);
                }
            }
        }

        Piece selectedPawn;
        public bool PawnInLastCell()
        {
            for(int x=0;x<CELL_COUNT;x++)
            {
                if(board.board[0,x]?.Piece?.Type==PieceType.Pawn)
                {
                    Point point = new Point(0, 0);
                    PieceColors color = board.board[0, x].Piece.Color;

                    selectedPawn = board.board[0, x].Piece;
                    pSelCreate.Invoke(color, point);
                    return true;
                }
                else if(board.board[7, x]?.Piece?.Type == PieceType.Pawn)
                {

                    Point point = new Point(0, 0);
                    PieceColors color = board.board[7, x].Piece.Color;
                    
                    selectedPawn = board.board[7, x].Piece;
                    pSelCreate.Invoke(color, point);
                    return true;
                }
            }
            return false;
        }
        public void ChangePawn(PieceType type)
        {
            if(selectedPawn!=null)
            {
                PieceColors color = selectedPawn.Color;
                PointInt point = selectedPawn.ChessboardPointInt;
                //point.Y = IsWhiteButton ? CELL_COUNT - point.Y - 1 : point.Y;
                switch (type)
                {
                    case PieceType.Bishop:
                        board.board[point.Y, point.X].Piece = color == PieceColors.White ? new Bishop(color, type, window, @"\Textures\Pieces\White\Bishop.png", this, point) :
                                                                                           new Bishop(color, type, window, @"\Textures\Pieces\Black\Bishop.png", this, point);
                        break;
                    case PieceType.Knight:
                        board.board[point.Y, point.X].Piece = color == PieceColors.White ? new Knight(color, type, window, @"\Textures\Pieces\White\Knight.png", this, point) :
                                                                                           new Knight(color, type, window, @"\Textures\Pieces\Black\Knight.png", this, point);
                        break;
                    case PieceType.Rook:
                        board.board[point.Y, point.X].Piece = color == PieceColors.White ? new Rook(color, type, window, @"\Textures\Pieces\White\Rook.png", this, point) :
                                                                                           new Rook(color, type, window, @"\Textures\Pieces\Black\Rook.png", this, point);
                        break;
                    case PieceType.Queen:
                        board.board[point.Y, point.X].Piece = color == PieceColors.White ? new Queen(color, type, window, @"\Textures\Pieces\White\Queen.png", this, point) :
                                                                                           new Queen(color, type, window, @"\Textures\Pieces\Black\Queen.png", this, point);
                        break;
                }

                ChangeColor();
                board.RegenOneCell(point);
                board.MoveUpdate(ref isCanSelected);

                //isCanSelected = true;
                selectedPawn = null;
            }
        }

        public void StartNewGame()
        {
            board.StartNewGame();
            SelectedColor = PieceColors.White;
            selectedPawn = null;
            isCanSelected = true;
        }

        public delegate void GameOver(GameState state);
        public delegate void MouseClickD(object obj);
        public delegate bool IsMouseClickD(object obj);
        public delegate void PieceSelectionCreate(PieceColors color, Point cPoint);
    }
}
