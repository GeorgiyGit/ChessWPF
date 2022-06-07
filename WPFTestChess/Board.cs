using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFTestChess
{


    internal class Board
    {
        public BoardCell[,] board = new BoardCell[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];

        private BoardCell[,] checkBoard = new BoardCell[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];

        Chessboard chessboard;

        AttackBoard attackBoard;

        MainWindow window;

        public Board(MainWindow window, Chessboard chessboard, MouseClickD execute, IsMouseClickD canExecute)
        {
            this.window = window;
            this.chessboard = chessboard;

            Initialization(board, execute, canExecute);
            Initialization(checkBoard, execute, canExecute);

            gameState = GameState.NoWiners;
            attackBoard = new AttackBoard(chessboard);
        }
        private void Initialization(BoardCell[,] initialBoard, MouseClickD execute, IsMouseClickD canExecute)
        {
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    bool isWhite = y % 2 == 0 ? x % 2 == 0 ? false : true : x % 2 == 0 ? true : false;

                    initialBoard[y, x] = new BoardCell(new BackCell(isWhite, false, window), execute.Invoke, canExecute.Invoke, new PointInt(x, y));
                }
            }

            InitializePiece(initialBoard);

        }
        private void InitializePiece(BoardCell[,] initialBoard)
        {
            for (int i = 0; i < Chessboard.CELL_COUNT; i++)
            {
                initialBoard[1, i].Piece = new Pawn(PieceColors.White, PieceType.Pawn, window, @"Textures\Pieces\White\Pawn.png", chessboard, new PointInt(i, 1));
                initialBoard[6, i].Piece = new Pawn(PieceColors.Black, PieceType.Pawn, window, @"Textures\Pieces\Black\Pawn.png", chessboard, new PointInt(i, 6));

                (initialBoard[1, i].Piece as Pawn).isFirstMove = true;
                (initialBoard[6, i].Piece as Pawn).isFirstMove = true;
            }


            initialBoard[0, 0].Piece = new Rook(PieceColors.White, PieceType.Rook, window, @"Textures\Pieces\White\Rook.png", chessboard, new PointInt(0, 0));
            initialBoard[0, 7].Piece = new Rook(PieceColors.White, PieceType.Rook, window, @"Textures\Pieces\White\Rook.png", chessboard, new PointInt(7, 0));

            initialBoard[7, 0].Piece = new Rook(PieceColors.Black, PieceType.Rook, window, @"Textures\Pieces\Black\Rook.png", chessboard, new PointInt(0, 7));
            initialBoard[7, 7].Piece = new Rook(PieceColors.Black, PieceType.Rook, window, @"Textures\Pieces\Black\Rook.png", chessboard, new PointInt(7, 7));


            initialBoard[0, 1].Piece = new Knight(PieceColors.White, PieceType.Knight, window, @"Textures\Pieces\White\Knight.png", chessboard, new PointInt(1, 0));
            initialBoard[0, 6].Piece = new Knight(PieceColors.White, PieceType.Knight, window, @"Textures\Pieces\White\Knight.png", chessboard, new PointInt(6, 0));

            initialBoard[7, 1].Piece = new Knight(PieceColors.Black, PieceType.Knight, window, @"Textures\Pieces\Black\Knight.png", chessboard, new PointInt(1, 7));
            initialBoard[7, 6].Piece = new Knight(PieceColors.Black, PieceType.Knight, window, @"Textures\Pieces\Black\Knight.png", chessboard, new PointInt(6, 7));


            initialBoard[0, 2].Piece = new Bishop(PieceColors.White, PieceType.Bishop, window, @"\Textures\Pieces\White\Bishop.png", chessboard, new PointInt(2, 0));
            initialBoard[0, 5].Piece = new Bishop(PieceColors.White, PieceType.Bishop, window, @"Textures\Pieces\White\Bishop.png", chessboard, new PointInt(5, 0));

            initialBoard[7, 2].Piece = new Bishop(PieceColors.Black, PieceType.Bishop, window, @"Textures\Pieces\Black\Bishop.png", chessboard, new PointInt(2, 7));
            initialBoard[7, 5].Piece = new Bishop(PieceColors.Black, PieceType.Bishop, window, @"Textures\Pieces\Black\Bishop.png", chessboard, new PointInt(5, 7));


            initialBoard[0, 3].Piece = new Queen(PieceColors.White, PieceType.Queen, window, @"Textures\Pieces\White\Queen.png", chessboard, new PointInt(3, 0));
            initialBoard[7, 3].Piece = new Queen(PieceColors.Black, PieceType.Queen, window, @"Textures\Pieces\Black\Queen.png", chessboard, new PointInt(3, 7));


            initialBoard[0, 4].Piece = new King(PieceColors.White, PieceType.King, window, @"Textures\Pieces\White\King.png", chessboard, new PointInt(4, 0));
            initialBoard[7, 4].Piece = new King(PieceColors.Black, PieceType.King, window, @"Textures\Pieces\Black\King.png", chessboard, new PointInt(4, 7));
        }


        public GameState gameState { get; set; }

        public void ClearPress()
        {
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    board[y, x].BackCell.IsPressedFalse = false;
                }
            }
        }
        public void MovePiece(PointInt currentPoint, PointInt nextPoint, ref bool isCanSelected)
        {
            if (checkBoard[currentPoint.Y, currentPoint.X].Piece != null)
            {
                checkBoard[currentPoint.Y, currentPoint.X].Piece.Move(Converter.BoardCellsToPieces(checkBoard));

                if (checkBoard[currentPoint.Y, currentPoint.X].Piece.movePointInts.Contains(nextPoint) ||
                   checkBoard[currentPoint.Y, currentPoint.X].Piece.attackPointInts.Contains(nextPoint))
                {
                    RelocatePiece(checkBoard, currentPoint, nextPoint);

                    if (gameState == GameState.NoWiners)
                    {
                        if (board[currentPoint.Y, currentPoint.X].Piece.Type == PieceType.Pawn)
                            ((Pawn)board[currentPoint.Y, currentPoint.X].Piece).isFirstMove = false;

                        else if (board[currentPoint.Y, currentPoint.X].Piece.Type == PieceType.Rook)
                            ((Rook)board[currentPoint.Y, currentPoint.X].Piece).isFirstMove = false;

                        else if (board[currentPoint.Y, currentPoint.X].Piece.Type == PieceType.King)
                            ((King)board[currentPoint.Y, currentPoint.X].Piece).isFirstMove = false;

                        RelocatePiece(board, currentPoint, nextPoint);

                        attackBoard.PointsCalculation(checkBoard);

                        //ChangeSelectionColor();
                        Chessboard.ChangeColor();

                        PointInt kingPoint = GetPiecesPoint(board, PieceType.King, Chessboard.SelectedColor)[0];
                        KingState state = attackBoard.GetKingState(checkBoard, Chessboard.SelectedColor);


                        if (state == KingState.Normal)
                        {
                            gameState = GameState.NoWiners;

                            isCanSelected = true;
                        }
                        else if (state == KingState.Check)
                        {
                            List<Piece> attackPieces = Chessboard.SelectedColor == PieceColors.White ? attackBoard.Blackboard[kingPoint.Y, kingPoint.X]?.pieces :
                                                                                                       attackBoard.Whiteboard[kingPoint.Y, kingPoint.X]?.pieces;

                            bool flag = isCheckMate(attackPieces);

                            if (flag == false)
                            {
                                isCanSelected = true;

                                gameState = Chessboard.SelectedColor == PieceColors.White ? GameState.WhiteCheck : GameState.BlackCheck;
                            }
                            else
                            {
                                gameState = Chessboard.SelectedColor == PieceColors.White ? GameState.BlackWin : GameState.WhiteWin;
                                chessboard.gameOver(gameState);
                            }
                        }

                    }
                    else if (gameState == GameState.BlackCheck || gameState == GameState.WhiteCheck)
                    {
                        attackBoard.PointsCalculation(checkBoard);

                        //ChangeSelectionColor();

                        //PointInt kingPoint = GetPiecesPoint(checkBoard, PieceType.King, Chessboard.SelectedColor)[0];
                        KingState state = attackBoard.GetKingState(checkBoard, Chessboard.SelectedColor);

                        if (state == KingState.Normal)
                        {
                            if (board[currentPoint.Y, currentPoint.X].Piece.Type == PieceType.Pawn)
                                ((Pawn)board[currentPoint.Y, currentPoint.X].Piece).isFirstMove = false;

                            RelocatePiece(board, currentPoint, nextPoint);

                            gameState = GameState.NoWiners;

                            isCanSelected = true;
                            Chessboard.ChangeColor();
                        }
                        else
                        {
                            RegeneratePiece(nextPoint, currentPoint);

                            isCanSelected = true;
                        }
                    }
                }
            }
        }

        public void MoveUpdate(ref bool isCanSelected)
        {
            attackBoard.PointsCalculation(checkBoard);

            //ChangeSelectionColor();
            Chessboard.ChangeColor();

            PointInt kingPoint = GetPiecesPoint(board, PieceType.King, Chessboard.SelectedColor)[0];
            KingState state = attackBoard.GetKingState(checkBoard, Chessboard.SelectedColor);


            if (state == KingState.Normal)
            {
                gameState = GameState.NoWiners;

                isCanSelected = true;
            }
            else if (state == KingState.Check)
            {
                List<Piece> attackPieces = Chessboard.SelectedColor == PieceColors.White ? attackBoard.Blackboard[kingPoint.Y, kingPoint.X]?.pieces :
                                                                                           attackBoard.Whiteboard[kingPoint.Y, kingPoint.X]?.pieces;

                bool flag = isCheckMate(attackPieces);

                if (flag == false)
                {
                    isCanSelected = true;

                    gameState = Chessboard.SelectedColor == PieceColors.White ? GameState.WhiteCheck : GameState.BlackCheck;
                }
                else
                {
                    gameState = Chessboard.SelectedColor == PieceColors.White ? GameState.BlackWin : GameState.WhiteWin;
                    MessageBox.Show(gameState + "");
                }
            }
        }

        public bool isCheckMate(List<Piece> pieces)
        {
            foreach (var piece in pieces)
            {
                if (piece.Type != PieceType.King)
                {
                    for (int mP = 0; mP < piece.movePointInts.Count; mP++)
                    {
                        var movePoint = piece.movePointInts[mP];
                        bool flag = isDefense(movePoint);

                        if (flag) return false;
                    }

                    var point = piece.ChessboardPointInt;

                    if (isDefense(point)) return false;
                }
                return KingDefense();
            }
            return true;
        }
        public bool KingDefense()
        {
            PointInt kingPoint = GetPiecesPoint(board, PieceType.King, Chessboard.SelectedColor)[0];

            checkBoard[kingPoint.Y, kingPoint.X].Piece.Move(Converter.BoardCellsToPieces(checkBoard));

            for(int i =0;i< checkBoard[kingPoint.Y, kingPoint.X].Piece.movePointInts.Count;i++)
            {
                var p = checkBoard[kingPoint.Y, kingPoint.X].Piece.movePointInts[i];

                var flag = IsRelocateKing(kingPoint, p);
                if (flag) return false;
            }


            for (int i = 0; i < checkBoard[kingPoint.Y, kingPoint.X].Piece.attackPointInts.Count; i++)
            {
                var p = checkBoard[kingPoint.Y, kingPoint.X].Piece.attackPointInts[i];

                var flag = IsRelocateKing(kingPoint, p);
                if (flag) return false;
            }
            return true;
        }
        private bool IsRelocateKing(PointInt kingPoint, PointInt nextPoint)
        {
            RelocatePiece(checkBoard, kingPoint, nextPoint);

            attackBoard.PointsCalculation(checkBoard);

            KingState stateRes = attackBoard.GetKingState(checkBoard, Chessboard.SelectedColor);

            RegeneratePiece(nextPoint, kingPoint);

            attackBoard.PointsCalculation(checkBoard);

            if (stateRes == KingState.Normal) return true;
            else attackBoard.PointsCalculation(board);
            return false;
        }
        public bool isDefense(PointInt point)
        {
            attackBoard.PointsCalculation(checkBoard);

            if ((Chessboard.SelectedColor == PieceColors.White &&
                        attackBoard.Whiteboard[point.Y, point.X]?.pieces?.Count > 0)
                     || (Chessboard.SelectedColor == PieceColors.Black &&
                        attackBoard.Blackboard[point.Y, point.X]?.pieces?.Count > 0))
            {
                Piece defensePiece = Chessboard.SelectedColor == PieceColors.White ? attackBoard.Whiteboard[point.Y, point.X]?.pieces?[0] :
                                                                        attackBoard.Blackboard[point.Y, point.X]?.pieces?[0];

                if (defensePiece != null)
                {
                    PointInt point2 = defensePiece.ChessboardPointInt;
                    RelocatePiece(checkBoard, point2, point);

                    attackBoard.PointsCalculation(checkBoard);

                    KingState stateRes = attackBoard.GetKingState(checkBoard, Chessboard.SelectedColor);

                    RegeneratePiece(point, point2);

                    attackBoard.PointsCalculation(checkBoard);

                    if (stateRes == KingState.Normal) return true;
                    else attackBoard.PointsCalculation(board);
                }
            }
            return false;
        }

        public List<PointInt> GetPiecesPoint(Piece[,] board, PieceType type, PieceColors color)
        {
            List<PointInt> points = new List<PointInt>();
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    if (board[y, x] != null)
                    {
                        if (board[y, x].Type == type && board[y, x].Color == color) points.Add(new PointInt(x, y));
                    }
                }
            }
            return points;
        }

        public List<PointInt> GetPiecesPoint(BoardCell[,] board, PieceType type, PieceColors color)
        {
            return GetPiecesPoint(Converter.BoardCellsToPieces(board), type, color);
        }

        public void RelocatePiece(BoardCell[,] selectedBoard, PointInt currentPoint, PointInt nextPoint)
        {
            selectedBoard[nextPoint.Y, nextPoint.X].Piece = selectedBoard[currentPoint.Y, currentPoint.X].Piece;
            selectedBoard[nextPoint.Y, nextPoint.X].Piece.ChessboardPointInt = nextPoint;
            selectedBoard[currentPoint.Y, currentPoint.X].Piece = null;
        }
        public void RegeneratePiece(PointInt currentPoint, PointInt nextPoint)
        {
            RelocatePiece(checkBoard, currentPoint, nextPoint);
            checkBoard[currentPoint.Y, currentPoint.X].Piece = board[currentPoint.Y, currentPoint.X].Piece;
        }
        public void RegenOneCell(PointInt point)
        {
            checkBoard[point.Y, point.X].Piece = board[point.Y, point.X].Piece;
        }

        public bool Roque(PointInt currentPoint, PointInt nextPoint, ref bool isCanSelected)
        {
            attackBoard.PointsCalculation(board);

            if (((King)board[currentPoint.Y, currentPoint.X].Piece).isFirstMove == false ||
               ((Rook)board[nextPoint.Y, nextPoint.X].Piece).isFirstMove == false)
            {
                return false;
            }

            int x = currentPoint.X > nextPoint.X ? nextPoint.X + 1 : currentPoint.X + 1;
            int max = currentPoint.X > nextPoint.X ? currentPoint.X : nextPoint.X;
            for (; x < max; x++)
            {
                if (board[currentPoint.Y, x].Piece != null)
                {
                    return false;
                }
            }
            int moveVector = currentPoint.X > nextPoint.X ? -2 : 2;
            int moveVector2 = currentPoint.X > nextPoint.X ? -1 : 1;
            if (board[currentPoint.Y, currentPoint.X].Piece.Color == PieceColors.White)
            {
                if (attackBoard.Blackboard[currentPoint.Y, currentPoint.X + moveVector].pieces.Count > 0 ||
                    attackBoard.Blackboard[currentPoint.Y, currentPoint.X + moveVector2].pieces.Count > 0)
                {
                    return false;
                }
            }
            else
            {
                if (attackBoard.Whiteboard[currentPoint.Y, currentPoint.X + moveVector].pieces.Count > 0 ||
                    attackBoard.Whiteboard[currentPoint.Y, currentPoint.X + moveVector2].pieces.Count > 0)
                {
                    return false;
                }
            }

            RelocatePiece(board, currentPoint, new PointInt(currentPoint.X + moveVector, currentPoint.Y));
            RelocatePiece(board, nextPoint, new PointInt(currentPoint.X + moveVector2, currentPoint.Y));

            RelocatePiece(checkBoard, currentPoint, new PointInt(currentPoint.X + moveVector, currentPoint.Y));
            RelocatePiece(checkBoard, nextPoint, new PointInt(currentPoint.X + moveVector2, currentPoint.Y));

            MoveUpdate(ref isCanSelected);

            ((King)board[currentPoint.Y, currentPoint.X + moveVector].Piece).isFirstMove = false;
            ((Rook)board[nextPoint.Y, currentPoint.X + moveVector2].Piece).isFirstMove = false;
            return true;
        }

        public void StartNewGame()
        {
            for(int x=0;x<Chessboard.CELL_COUNT;x++)
            {
                for(int y=0;y<Chessboard.CELL_COUNT;y++)
                {
                    board[x, y].Piece = null;
                    checkBoard[x, y].Piece = null;
                }
            }
            InitializePiece(board);
            InitializePiece(checkBoard);
            gameState = GameState.NoWiners;
        }


        public delegate void MouseClickD(object obj);
        public delegate bool IsMouseClickD(object obj);
    }
}
