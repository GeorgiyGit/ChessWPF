using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFTestChess.ViewModels
{
    internal class GameOverViewModel : INotifyPropertyChanged
    {
        RelayCommand startNewGameCmd;
        public ICommand StartNewGameCmd => startNewGameCmd;

        private GameState gameState;
        public string Text
        {
            get
            {
                if (gameState == GameState.WhiteWin)
                {
                    return "WHITE WIN!!!";
                }
                else return "BLACK WIN!!!";
            }
        }
        public GameOverViewModel(GameState state, StartNewGameEx startNewGameEx)
        {
            gameState = state;
            startNewGameCmd = new RelayCommand((t) => startNewGameEx(), null);
        }


        public delegate void StartNewGameEx();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
