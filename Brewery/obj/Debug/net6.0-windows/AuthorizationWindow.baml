   M S B A M L   `   `   `  ����B  >Brewery, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:BreweryBrewery  5   	   !    Brewery.AuthorizationWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QScottPlot.WPF, Version=4.1.63.0, Culture=neutral, PublicKeyToken=e53b06131e34a3aae aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Brewery     5   	   -   5          � Title$  Brewery Authorization��5   	   $	��400��6(   $	��300��65   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��Authorization��6   $
��16pt(�6)   $��Consolas)�69   $��Center=�6O   $��30��6l   $��Center=�6x   $��0��6�   $��1��6�   �� 5   
   $��Login��6   $
��14pt(�6!   $��Consolas)�61   $��Bottom=�6G   $	��100��6b   $
��Left=�6n   $��40,0,0,0q�6�   $��2��6�   �� 5   
   -   5        $��tbxLogin��5      $��+7(911)911-91-91��6"    J� Opacity$	 0.8Q�6:   $��3��6H   $	��220��6U   $��25��6a   �� 5   
   $��Password��6   $
��14pt(�6$   $��Consolas)�64   $��Bottom=�6J   $	��100��6e   $
��Left=�6q   $��40,0,0,0q�6�   $��4��6�   �� 5   
   -   5        $��tbxPassword��5      $��string��6%   $	 0.8Q�63   $��5��6A   $	��220��6N   $��25��6Z   �� 5   
   -   5         ��Name$ btAuth��5      $	 0.8Q�6!   $��6��6/   $
��Auth��6<   $	��100��6K   $��30��6W   $��Center=�6c   
������� � 	Resources 5       �� 
TargetType �� Property �� Value��%^   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 7��65   5      5      5      �� 5    
   -   5        $��Regist��5       $��6��6!   $��20, 0, 70, 0q�6.   $��Center=�6D   $��Right=�6_   �� 5!   
   -   5        $��tbError��5!      $�� ��6#   $��8��6+   $
��Wrap=�68   $	��300��6L    �� TextAlignment$ Center=�6X   5"      5#      