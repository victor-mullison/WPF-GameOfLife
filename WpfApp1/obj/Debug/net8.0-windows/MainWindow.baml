   M S B A M L   `   `   `  ����B  >WPFLife, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WPFLifeWPFLife  5   	       WPFLife.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )wpflifeclr-namespace:WPFLife     5   	     � Title$   Conway's Game of Life - VM��5   	    � 
ResizeMode$ NoResize=�5	   	   $	��815��5
   	   $	��479��5   	   .��5      � ���	#FF1F2840.+�5   
      WPFLife.LifeCanvas  -   5          Name$ 
LifeCanvas��5      $��20,22,20,362q�61   $	��400��6G   $	��400��6S   �� 5   
   -   5         ��Name$ RandomButton��5      $��4q�6C   $��70,466,309,262q�6W   ���	#FF464F69���	#FF181D2B J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5      V� 5      .~�5      �� �� 5       �� AngleX$	 -10Q�6$   �� 5      T� 5      5      .��5      �� $��Images/dice.png��6   $ 0.5,0.5&�6-   z�5      V� 5      .~�5      �� �� 5      $ 10Q�6(   �� 5       T� 5!      5"      5$      5%      �� 5'   
   -   5        $��190,466,189,262q�5'      $��4q�6D   ���	#FF464F69$ 0.5,0.5&�6o   ���	#FF181D2Bz�5(      V� 5)      .~�5*      �� �� 5+      $	 -10Q�6$   �� 5,      T� 5-      5.      .��50      �� -   5         ��Name$ PlayPauseIcon��50      $��Images/pause.png��6+   $ 0.5,0.5&�6E   z�51      V� 52      .~�53      �� �� 54      $ 10Q�6(   �� 55      T� 56      57      59      5:      �� 5<   
   -   5        $��309,466,70,262q�5<      $��4q�6D   ���	#FF464F69$ 0.5,0.5&�6o   ���	#FF181D2B � BindingGroup 5=      &   System.Windows.Data.BindingGroup  5>      z�5@      V� 5A      .~�5B      �� �� 5C      $	 -10Q�6$   �� 5D      T� 5E      5F      .��5H      �� $��Images/reset.png��6   $ 0.5,0.5&�6.   z�5I      V� 5J      .~�5K      �� �� 5L      $ 10Q�6(   �� 5M      T� 5N      5O      5Q      5R      �� 5U   
   -   5         ��Name$ GameSizeSlider��5U      	 � Value$	 3Q�6P   
 � Minimum$
 1Q�6Z    � Maximum$ 5Q�6f    �� TickFrequency$ 1Q�6r    �� IsSnapToTickEnabled$
 True��6�   $��40,604,39,136q�6�   �� 5V   
   $
��Wrap=�6   $��	Game Size��6(   $��63,570,216,165q�69   $��18(�6Q   
�������6_   �� 5W   
   -   5         ��Name$ GridSizeView��5W      $
��Wrap=�6*   $��40x40��6>   $��240,570,39,165q�6K    �� TextAlignment$ Right=�6c   $��18(�6y   
�������6�   �� 5Y   
   -   5        $ SimSpeedSlider��5Y      $	 3Q�6O   $
 1Q�6Y   $ 5Q�6f   $ 1Q�6r   $
 True��6�   $��40,702,39,10q�6�   �� 5Z   
   $
��Wrap=�6   $��Simulation Speed��6(   $��63,668,216,71q�6@   
�������$��18(�6j   �� 5[   
   -	   5        $ SimSpeedView��5[      $
��Wrap=�6*   $��1x��6>   $��240,668,39,71q�6H   $ Right=�6_   
�������$��18(�6�   5\      5]      