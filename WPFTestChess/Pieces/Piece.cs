using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFTestChess
{

    internal abstract class Piece:INotifyPropertyChanged
    {
        public PieceColors Color { get; set; }
        public PieceType Type { get; set; }

        public PointInt ChessboardPointInt;
        public MainWindow window;
        private Image texture;
        //public PointInt CanvasPointInt;
        private bool isMousedDown;

        public bool IsMousedDown
        {
            get { return isMousedDown; }
            set
            {
                isMousedDown = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Texture));
            }
        }

        public List<PointInt> movePointInts=new List<PointInt>();
        public List<PointInt> attackPointInts = new List<PointInt>();

        //public bool isMouseEnter;

        public Image Texture
        {
            get
            {
                return isMousedDown ? null : texture;
            }
            set
            {
                texture = value;
                OnPropertyChanged();
            }
        }

        public Piece() { }
        public Piece(PieceColors color, PieceType type, MainWindow window, string texturePath, Chessboard c, PointInt boardPointInt)
        {
            this.window = window;
            this.Color = color;
            this.Type = type;

            BitmapImage bitTexture = new BitmapImage();

            bitTexture.BeginInit();
            bitTexture.UriSource = new Uri(texturePath, UriKind.Relative);
            bitTexture.EndInit();

            texture = new Image();

            texture.Source = bitTexture;
            texture.Stretch = Stretch.UniformToFill;

            //chessboard = c;

            ChessboardPointInt = boardPointInt;
        }
        public abstract void Move(Piece[,] board);



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
