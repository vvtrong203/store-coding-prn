   M S B A M L   `   `   `  ����D  @Question2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Question2	Question2  5   	       Question2.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Question2     5   	     � Title$  
MainWindow��5   	   $	��800��6   .��5      � 
����� �.+�5   
   ]� $	��Top=�6   $��10q�6,   .+�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $��* �6$   �� 5      $��4* �6$   �� 5      $��* �6$   .+�5      �� $��0��6   
����ح� �� Orientation$ Vertical=�6A   $
��Left=�6X   $	��400��6s   .+�5      �� -   5        $��lbTitle��5      
���  ��$��DemiBold"�6<   $��20(�6R   $��Car Information��6`   �� 5      -   5        $��lbCarId��5      $��Car Id��6+   �� 5      -   5        $��txtCarId��5      $��Stretch=�6.   $��25��6L   ��6X   ��  �� Path$ CarId� �� Mode$ OneWay=� � DataContext 6�   ��  �� ElementName$ lvCars��$ SelectedItem��� 5      -   5        $��	lbCarName��5      $��Car Name��6-   �� 5      -   5        $��
txtCarName��5      $��Stretch=�60   $��25��6N   ��6Z   �� $ CarName�$ OneWay=� 6�   �� $ lvCars��$ SelectedItem��� 5      -   5        $��lbManufacturer��5      $��Manufacturer��62   �� 5      -   5        $��txtManufacturer��5      $��Stretch=�65   $��25��6S   ��6_   �� $ Manufacturer�$ OneWay=� 6�   �� $ lvCars��$ SelectedItem��� 5!      -   5        $��lbPrice��5!      $��Price��6+   �� 5"      -	   5        $��txtPrice��5"      $��Stretch=�6.   $��25��6L   ��6X   �� $ Price� �� StringFormat$ {0:N3}��$ OneWay=� 6�   �� $ lvCars��$ SelectedItem��� 5$      -
   5        $��lbReleasedYear��5$      $��ReleaseYear��62   �� 5%      -   5        $��txtReleasedYear��5%      $��Stretch=�65   $��25��6S   ��6_   �� $ ReleasedYear�$ OneWay=� 6�   �� $ lvCars��$ SelectedItem�5&      �� 53      $��1��6   $ 
Horizontal=�6*   $
��Left=�6C   .+�54      �� -   5         ��Name$ btnLoad��54      $��10q�6.   $��80��6:   $
��Load��6F   6U   �� 55      -   5        $ 	btnInsert��55      $��10q�60   $��80��6<   $��Insert��6G   6X   �� 56      -   5        $ 	btnUpdate��56      $��10q�60   $��80��6<   $��Update��6G   6X   �� 57      -   5        $ 	btnDelete��57      $��10q�60   $��80��6<   $��Delete��6G   6X   58      �� 5:      -   5        $��lvCars��5:   (   $��2��6   $
��Auto��66   $
��Auto��6C   	 �� View	 5;      �� 5<      .P�5=      �� $��Car Id��6-   
 �� Width$	
 100��6=    �� DisplayMemberBinding 6I   �� $ CarId��� 5>      $��Car Name��6-   $	
 200��6?    6K   �� $ CarName��� 5?      $��Manufacturer��6-   $	
 200��6C    6O   �� $ Manufacturer��� 5@      $��Price��6-   $	
 100��6<    6H   �� $ Price�$ {0:N3}���� 5A      $��ReleasedYear��6-   $	
 100��6C    6O   �� $ ReleasedYear�5B      5D      �� 5E      -   5        $ btnClose��5E   &   $��3��6   $��590,10,0,10q�68   $
��Left=�6M   $��80��6h   $��Close��6s   6�   5F      5G      5H      5J      