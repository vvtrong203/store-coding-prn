   M S B A M L   `   `   `  ����  WpfTcpChatClient!    WpfTcpChatClient.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  TCP Chat Client��5   	   $	��450��6!   $	��650��6.   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��0,0,0,10q�6;   .+�5      �� -   5         ��Name$ ServerInput��5      $	��150��6+   $��0,0,10,0q�67   $��	127.0.0.1��6I   �� 5      -   5        $ 	PortInput��5      $��70��6)   $��0,0,10,0q�64   $��13000��6F   �� 5      -   5        $��Connect��5      $��80��6'   $��0,0,5,0q�62   6C   �� 5      -   5        $��
Disconnect��5      $��80��6*   65   5      �� 5   
   $��1��6   $
��Auto=�6$   .��5      �� -   5         ��Name$ ChatLog��5      $
��Wrap=�6)   $	��Top=�6=   $��Consolas)�6U   $��14(�6k   5      �� 5   
   $ 
Horizontal=�6   $��2��6.   $��0,10,0,0q�6;   .+�5      �� -   5        $ MessageInput��5      $	��450��6,   $��0,0,10,0q�68   �� 5      -   5        $
��Send��5      $��80��6$   6/   5      5      5      