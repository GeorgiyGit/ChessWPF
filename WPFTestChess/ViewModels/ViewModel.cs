using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFTestChess.ViewModels
{
    internal class ViewModel : INotifyPropertyChanged
    {

        #region Commands

        private RelayCommand click;
        public ICommand Click => click;

        private RelayCommand startNewGameCmd;
        public ICommand StartNewGameCmd => startNewGameCmd;

        #endregion

        #region Properties

        private double chessboardHeight;
        public double ChessboardHeight
        {
            get
            {
                return chessboardHeight <= chessboardWidth ? chessboardHeight : chessboardWidth;
            }
            set
            {
                chessboardHeight = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PieceLength));
                OnPropertyChanged(nameof(ChessboardLength));
                OnPropertyChanged(nameof(ChessboardWidth));
            }
        }

        private double chessboardWidth;
        public double ChessboardWidth
        {
            get
            {
                return chessboardHeight <= chessboardWidth ? chessboardHeight : chessboardWidth;
            }
            set
            {
                chessboardWidth = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PieceLength));
                OnPropertyChanged(nameof(ChessboardLength));
                OnPropertyChanged(nameof(ChessboardHeight));
            }
        }


        //private int chessboardlength;
        public double ChessboardLength
        {
            get
            {
                return ChessboardHeight <= ChessboardWidth ? ChessboardHeight : ChessboardWidth;
            }
        }

        public double PieceLength
        {
            get
            {
                return ChessboardLength / Chessboard.CELL_COUNT;
            }
        }

        public bool isClickOnImage
        {
            get
            {
                return isClickOnImage;
            }
            set
            {
                isClickOnImage = value;

            }
        }

        private bool isMouseDown;
        public bool IsMouseDown
        {
            get { return isMouseDown; }
            set
            {
                isMouseDown = value;
                OnPropertyChanged();
            }
        }

        private Image selectedPiece;
        public Image SelectedPiece
        {
            get { return selectedPiece; }
            set
            {
                selectedPiece = value;
                OnPropertyChanged();
            }
        }

        private Point mousePoint;
        public Point MousePoint
        {
            get
            {
                Point newPoint = mousePoint;

                newPoint.X -= ChessboardLength / Chessboard.CELL_COUNT / 2;
                newPoint.Y -= ChessboardLength / Chessboard.CELL_COUNT / 2;

                return newPoint;
            }
            set
            {
                mousePoint = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region ViewModels


        private PieceSelectionViewModel piece_Selection_View_Model;
        public PieceSelectionViewModel Piece_Selection_View_Model
        {
            get
            {
                return piece_Selection_View_Model;
            }
            set
            {
                piece_Selection_View_Model = value;
                OnPropertyChanged();
            }
        }

        private GameOverViewModel gameOverViewModel;
        public GameOverViewModel Game_Over_View_Model
        {
            get
            {
                return gameOverViewModel;
            }
            set
            {
                gameOverViewModel = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public Chessboard chessboard { get; set; }

        MainWindow window;
        public ViewModel(MainWindow window)
        {
            //Color = Colors.Black;
            //AddRect();
            this.window = window;
            chessboard = new Chessboard(window, MouseClick, IsMouseCLick, PieceSelectionCreate, GameOver);
            backCellsList = Converter.TwoDemensionalArrayToCollection(chessboard.board.board);

            chessboard.board.ClearPress();

            click = new RelayCommand(MouseClick, null);

            startNewGameCmd = new RelayCommand((t) => StartNewGame(), null);
        }

        private ObservableCollection<BoardCell> backCellsList = new ObservableCollection<BoardCell>();
        public IEnumerable<BoardCell> BackCells => backCellsList;

        public BoardCell selectedBC;
        public void ChessboardCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            MousePoint = e.GetPosition((Canvas)sender);
        }

        public void MouseClick(object obj)
        {
            BoardCell bc = obj as BoardCell;

            chessboard.MovePiece(ref selectedBC, ref bc);
        }
        public bool IsMouseCLick(object obj)
        {
            BoardCell bc = obj as BoardCell;
            return bc.BackCell.IsPressedFalse == true;
        }

        public void PieceSelectionCreate(PieceColors color, Point cPoint)
        {
            Piece_Selection_View_Model = new PieceSelectionViewModel(color, cPoint, SelectedTypeInPieceSelection);
        }

        public void SelectedTypeInPieceSelection(PieceType pieceType)
        {
            chessboard.ChangePawn(pieceType);
            Piece_Selection_View_Model = null;
        }

        public void GameOver(GameState state)
        {
            Game_Over_View_Model = new GameOverViewModel(state, StartNewGame);
            IsStartNewButtonEnabled = false;
        }
        private bool isStartNewButtonEnabled = true;
        public bool IsStartNewButtonEnabled
        {
            get { return isStartNewButtonEnabled; }
            set
            {
                isStartNewButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        public void StartNewGame()
        {
            chessboard.StartNewGame();
            Game_Over_View_Model = null;
            selectedBC = null;
            SelectedPiece = null;
            IsStartNewButtonEnabled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
