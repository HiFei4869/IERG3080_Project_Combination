   M S B A M L   `   `   `  ����B  >WpfApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  5   	       WpfApp2.Window1   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp2     5   	     � Title$  Window1��5   	   $	��540��6   $	��960��6&    � DataContext 5	   	   ��  �� RelativeSource �� *Self+.��5
      � .+�5   
   �� -   5         ��Name$ 
GameCanvas��5      .+�5      ��  6   ��  �� *FindAncestor+ �� AncestorType" �.@�5      �� ��6   �� *DataContext.Balls+��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property �@��Left �� Value 68   �� *BallX+�� 5       �@��Top 67   �� *BallY+5      ��5      �� 5      .R�5      A� ��6"   �� *	BallColor+��6=   �� *
BallRadius+��6Z   �� *
BallRadius+ �� Left 5      �� *BallX+
	 �� Top	 64   �� *BallY+5       ��5"      �� 5#      .R�5$      �� 
 K� IsItemsHost$

 True��6!   5%      5'      5(      5)      5*      5+      