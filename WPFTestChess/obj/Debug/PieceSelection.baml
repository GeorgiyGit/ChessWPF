   M S B A M L   `   `   `  ����G  CWPFTestChess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:WPFTestChess.ViewModelsWPFTestChess.ViewModels  5      !    WPFTestChess.PieceSelection   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:WPFTestChess.ViewModels     5        � 	Resources  5
       	  Piece �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius
 � Tag �� Triggers P� Property	 P� Value��%�  &	        �� 5   
   " ��6   �5      �� 5       � �� 60   �� *BorderBrush+�� 5       � �� 6/   �� *	BackBrush+�� 5       � ��$ 10,10,10,10q�6+   �� 5       � ��$ 1��6-   �� 5       � �� 5      �� 5      " ��6*   .R�5      � .+�5   "   �� $ 10��6)   #��z��5   %   #��z��5   %   #��z��5   %   #��z��5   %   #��z��5   %   ._�5   &   �� #��z 6,   5   #   5      5      5        5"      P� 5#       �@J�IsMouseOver	 . . �5$      ��  � ��$ 20q�68   5%      P� 5&       �@J�IsMouseOver	 .  . �5'      ��  � ��$ 3q�68   5(      5+      5,      .��5-      � 
 � ColumnDefinitions
 5.   
   �� 5/      $��1* �6   �� 50      $��1* �6   �� 51      $��1* �6   �� 52      $��1* �6    � RowDefinitions 54   
   �� 55      $��3* �6   �� 56      $��2* �6   �� 57      $��3* �6   .+�5:   
   �� #��[  6   $��0��5;      $��1��5<       5=      �� *BishopSource+��5>      �� *	BishopCmd+5?      �� 5A   
   #��[  6   $��1��5B      $��1��5C       5D      �� *KnightSource+��5E      �� *	KnightCmd+5F      �� 5H   
   #��[  6   $��2��5I      $��1��5J       5K      �� *
RookSource+��5L      �� *	RookCmd+5M      �� 5O   
   #��[  6   $��3��5P      $��1��5Q       5R      �� *QueenSource+��5S      �� *
QueenCmd+5T      5U      5V      