# ChessWPF
Chess in WPF
In this project we can see chess program and play chess.
Files tree with descriptions:

Help Elems - Elements which help us.
  Converter.cs - Converter which converting one type to outher.
  Enums.cs - All enums in this project.
  PointInt.cs - Point of type "Integer".
  RelayCommand.cs - Better version of command which we can use in ViewModel.
  
Pieces - All type of pieces in chess.
  Piece.cs - Main abstract class.

PushBinding - We have one problem, in WPF we can`t binding properties with only get. And this files help us with it.

Textures - Textures in our project

View Models - all view models
  GameOverViewModel.cs - View model with game over window(but it`s UserControl).
  PieceSelectionViewModel.cs - If Pawn move to last cell, we can replace it with another figure.
  ViewModel.cs - Global, main view model
  
 AttackBoard.cs - We have to arrays with two demension - BlackBoard and WhiteBoard. Elems in this arrays is AttackCell
  AttackCell.cs - i`ts cell which have pieces, color and property IsAttack(true - it`s attack AttackCell, false - it`s move AttackCell).
 AttackBoard cells have 3 states
  pieces.Count==0 - None Pieces move or attack on this cell.
  pieces.Count>0 and IsAttack true - One or More than one pieces have attack on this cell.
  pieces.Count>0 and IsAttack  false - One or More than one pieces have move on this cell.
  Also, in each cell we have list of Piece that can attack or move on it.
  
  ChessBoard.cs - contain board and outher properties such us SelectedColor(color which go in this course).
  
  Board.cs - have board with pieces and metods which work with piece and board
  
  BoardCell.cs - Board have arr[,] of BoardCell. BoardCell have Piece and BackCell. Piece it`s piece.
    BackCell.cs - It`s Square. All we no what it is. in this class we have many properties such us color, isSelected...
    
    
MainWindow.xaml - Main window with chessboard and outher elems.

GameOverView.xaml - game over window. It`s UserControl which contain button and label

PieceSelection.xaml - UserControl. It shows when pawn move to last cell and we can choose on of 4 pieces. It contain for button.
  IconSouece - file which help us with images in buttons in PieceSelection.

  
  
