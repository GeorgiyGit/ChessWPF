using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace WPFTestChess
{
    internal class BackChessboard
    {
        BackCell[,] backCells = new BackCell[Chessboard.CELL_COUNT, Chessboard.CELL_COUNT];

        MainWindow window;
        Chessboard chessboard;

        public ObservableCollection<BackCell> backCellsList = new ObservableCollection<BackCell>();

        public IEnumerable<BackCell> BackCells => backCellsList;
        public BackChessboard(MainWindow window, Chessboard chessboard)
        {
            this.window = window;
            this.chessboard = chessboard;

            DrawRectangle();
        }
        public void DrawRectangle()
        {
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    bool isWhite = y % 2 == 0 ? x % 2 == 0 ? false : true : x % 2 == 0 ? true : false;

                    backCells[y, x] = new BackCell(isWhite, false, window);
                    backCellsList.Add(backCells[y, x]);
                }
            }
        }
        public void DrawMoveCircles()
        {
            ClearBackCellCircles();

            /*if (chessboard.SelectedMovePeace != null)
            {
                foreach (var PointInt in chessboard.SelectedMovePeace.movePointInts)
                {
                    if (chessboard.IsWhiteButton)
                    {
                        backCells[Chessboard.CELL_COUNT - (int)PointInt.Y - 1, (int)PointInt.X].DrawMoveCircle();
                    }
                    else
                    {
                        backCells[(int)PointInt.Y, (int)PointInt.X].DrawMoveCircle();
                    }
                }
            }*/
        }
        public void ClearBackCellCircles()
        {
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    //backCells[y, x].ClearCircle();
                }
            }
        }
        public void ClearBackSellSelection()
        {
            for (int y = 0; y < Chessboard.CELL_COUNT; y++)
            {
                for (int x = 0; x < Chessboard.CELL_COUNT; x++)
                {
                    //backCells[y, x].ChangeToNormalCell();
                }
            }
        }

        //public Rectangle GetCellRectangle(int x, int y)
        //{
          //  return GetCellRectangle(new PointInt(x, y));
        //}
        //public Rectangle GetCellRectangle(PointInt PointInt)
        //{
            //if (PointInt.X >= 0 && PointInt.Y >= 0 && PointInt.X < Chessboard.CELL_COUNT && PointInt.Y < Chessboard.CELL_COUNT)
               // return backCells[(int)PointInt.Y, (int)PointInt.X].rectangle;
            //return null;
        //}

        //public bool IsPointIntInCell(int x, int y, PointInt PointInt)
        //{
            //return IsPointIntInCell(new PointInt(x, y), PointInt);
        //}
        //public bool IsPointIntInCell(PointInt CellPointInt, PointInt PointInt)
        //{
            //if (CellPointInt.X >= 0 && CellPointInt.Y >= 0 && CellPointInt.X < Chessboard.CELL_COUNT && CellPointInt.Y < Chessboard.CELL_COUNT)
                //return backCells[(int)CellPointInt.Y, (int)CellPointInt.X].IsPointIntInCell(PointInt);
            //return false;
        //}

        //public void PointIntInCell(int x, int y, PointInt PointInt)
        //{
            //PointIntInCell(new PointInt(x, y), PointInt);
        //}
        //public void PointIntInCell(PointInt CellPointInt, PointInt PointInt)
        //{
            //if (CellPointInt.X >= 0 && CellPointInt.Y >= 0 && CellPointInt.X < Chessboard.CELL_COUNT && CellPointInt.Y < Chessboard.CELL_COUNT)
                //backCells[(int)CellPointInt.Y, (int)CellPointInt.X].PointIntInCell(PointInt);
        //}

        public void ChangeToNormalRectangle(int x, int y)
        {
            ChangeToNormalRectangle(new PointInt(x, y));
        }
        public void ChangeToNormalRectangle(PointInt PointInt)
        {
            if (PointInt.X >= 0 && PointInt.Y >= 0 && PointInt.X < Chessboard.CELL_COUNT && PointInt.Y < Chessboard.CELL_COUNT) ;
                //backCells[(int)PointInt.Y, (int)PointInt.X].ChangeToNormalCell();
        }

        public void ChangeToSelectedRectangle(int x, int y)
        {
            ChangeToSelectedRectangle(new PointInt(x, y));
        }
        public void ChangeToSelectedRectangle(PointInt PointInt)
        {
            if (PointInt.X >= 0 && PointInt.Y >= 0 && PointInt.X < Chessboard.CELL_COUNT && PointInt.Y < Chessboard.CELL_COUNT) ;
                //backCells[(int)PointInt.Y, (int)PointInt.X].ChangeToSelectedCell();
        }
    }
}
