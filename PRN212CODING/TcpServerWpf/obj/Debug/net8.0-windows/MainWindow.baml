   M S B A M L   `   `   `  ����  TcpServerWpf    TcpServerWpf.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
TCP Server��5   	   $	��500��6   $	��800��6)   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,10q�6.   .+�5      �� $��Host:��6   $��Center=�6%   $��0,0,5,0q�6@   �� 5      -   5         ��Name$ 	HostInput��5      $	��150��6)   $��	127.0.0.1��65   $��0,0,10,0q�6F   �� 5      $��Port:��6   $��Center=�6%   $��0,0,5,0q�6@   �� 5      -   5        $ 	PortInput��5      $	��100��6)   $��13000��65   �� 5      -   5        $��Start Server��5      $��10,0,0,0q�6,   6>   �� 5      -   5        $��Stop Server��5      $��5,0,0,0q�6+   6<   5      �� 5   
   -   5        $ 	LogOutput��5      $��1��6%    �� 
IsReadOnly$
 True��62   " �� VerticalScrollBarVisibility$
 Auto=�5       �� TextWrapping$
 Wrap=�65    �� AcceptsReturn$
 True��5      5      5      