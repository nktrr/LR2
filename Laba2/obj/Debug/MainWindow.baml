   M S B A M L   `   `   `  ����@  <Laba2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Laba2Laba2  5   	       Laba2.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Laba2     5   	     � Title$  
MainWindow��5   	   $
��1080��6   $
��1024��6*   ���	#FF121212.��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��7��5       $	��Top=�5!      $��81��6-    J� Effect 5"      3  -System.Windows.Media.Effects.DropShadowEffect  5#      ��5%      �� 5&       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�5'   &   .��5(      �  � Color$ Black��6#    � Offset$ 1Q�5)   #   � 5*      $ 	#FF6974DC��6#   5+      .+�5-      �� 
������� �� Padding$ 2q�5.      $��Center=�5/      $T��NБанк данных угроз безопасности информации��50      $��22(�51      	 �� TextAlignment$	 Center=�52      �� 53      
 �� Orientation$
 
Horizontal=�6   $��6q�54      $��Center=�55      .+�56      �� -   5         ��Name$ First��56      $��4q�57      $��<< В начало��58      $��6q�59      5:   .   �� 5;      -   5        $ 	Backwards��5;      $��4q�5<      $��< Назад��5=      $��6q�5>      5?   2   � 5@      $��6q�6   $��Center=�5A      $��Center=�5B      .+�5C      �� -   5         ��Name$ PageInfo��5C      
�������5D      5E      � 5F      $��Center=�6   $��6q�5G      $��Center=�5H      .+�5I      �� -   5         ��Name$ NumberOfRecords��5I       �� SelectedIndex$ 0��5J      5K      5L      �� 5M      -   5        $ Forward��5M      $��4q�5N      $��> Вперёд��5O      $��6q�5P      5Q   0   �� 5R      -   5        $
 Last��5R      $��4q�5S      $��>> В конец��5T      $��6q�5U      5V   -   �� 5W      -   5        $ Update��5W      $��Обновить��5X      $��4q�5Y      $��6q�5Z      5[      �� 5\      -   5        $ ExportButton��5\      $��Экспорт��6/   $��6q�6A   $��4q�6L   6X   5]      5^      � 5_   
   $��7��5`      $��7��5a      $��	10,86,0,0q�6$   $y�Hidden=�6I   &   System.Windows.Controls.DataGrid  5b   
   -	   5          Name$ 	dgThreats��5b         AutoGenerateColumns .     
IsReadOnly . 
���   �$��7��6g   $��0,83,0,0q�6{   $��8��6�      Columns 5c      0  *System.Windows.Controls.DataGridTextColumn  5d      ,  &System.Windows.Controls.DataGridColumn   Header$  Идентификатор��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *Identificator+  5e      $+ %Наименование угрозы��6%    6B   �� *Name+   RowDetailsTemplate 5g      �� 5h      .R�5i      ]� 
�������.+�5k      � $
��0,10q�6    5l      �� 5m   "   $
��Auto �63   �� 5n   "   $��* �63    5p      �� 5q   "   $
��Auto �60   �� 5r   "   $
��Auto �60   �� 5s   "   $
��Auto �60   �� 5t   "   $
��Auto �60   .+�5v      �� $%��Описание угрозы: ��6(   $
��Bold"�6A   $��0��6S   
�������6`   �� 5w      $��1��6E   $��0��6U   
���������6(   �� *Description+6b   �� 5y      $&�� Источники угрозы:��6(   $
��Bold"�6A   $��1��6S   �� 5z      $��1��6F   $��1��6V   ��6(   �� *ThreatSource+6V   �� 5|      $+��%Объект воздействия: ��6(   $
��Bold"�6D   $��2��6V   
�������6c   �� 5}      $��1��6@   $��2��6P   
���������6(   �� *Target+6]   �� 5      $@��:Последствия реализации угрозы: ��6(   $
��Bold"�6O   $��3��6a   �� 5�      $��1��6J   $��3��6Z   ��6(   �� *BreachesForTable+6Z   5�      5�      5�      5�      5�      5�      