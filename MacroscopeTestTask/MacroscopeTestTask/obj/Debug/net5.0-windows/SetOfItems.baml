   M S B A M L   `   `   `  ����M  IMacroscopeTestTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MacroscopeTestTaskMacroscopeTestTask  5      M+clr-namespace:MacroscopeTestTask.ConvertersMacroscopeTestTask.Converters  6=   K*clr-namespace:MacroscopeTestTask.ViewModelMacroscopeTestTask.ViewModel  6|   #    MacroscopeTestTask.SetOfItems   5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:MacroscopeTestTask     5      B
converters+clr-namespace:MacroscopeTestTask.Converters     6=   @	viewmodel*clr-namespace:MacroscopeTestTask.ViewModel     6|   .��5
      �   � RowDefinitions  5   
   �� 5      $	��120 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6    � ColumnDefinitions 5   
   �� 5      $	��120 �6   .+�5   
   �� $
��Auto��6)   $
��Auto��66   $��0��6D   ��6   �� *Image+ J� InputBindings 5      d� 5       d� MouseAction$ LeftDoubleClicke�6    �� Command 6=   �� *ShowBigImage+5      �� 5   
   $��5q�6L   $��1��6W   ��6   �� *Url+ �� UpdateSourceTrigger$ PropertyChanged=�6W   �� 5   
   $��Start��6   $��5q�6;   $��2��6F   ��6!   �� *Start+{�5      `� 5       `� Mode$ OneWay=�6      BoolMultivalueConverter `� 	Converter# [  6-   1�5      �� 5        �� Path$ DownloadStatus�6"    & "DownloadStatusToIsEnabledConverter	 �� 	Converter#	 [ 68   �� 5!      $ 
IsUrlValid�6"   5#      5%      �� 5&   
   $��Cancel��6   $��5q�6�   $��3��6�   ��6"   �� *Cancel+{�6=   �� *DownloadStatus+ - )DownloadStatusToIsEnabledConverter_Cancel#	 [ 6�   � 5'   
   -   5        $��pbStatus��5+   2   
 � Minimum$
 0Q�5'      $��5q�6"    � Maximum$	 100Q�5(   2   $��4��5+   B   ��5)   2   �� *DownloadStatus+ " DownloadStatusToColorConverter#	 [  � Value 5*   2   �� *
Progress+5+   B   �� 5,   
   $��4��6   $��Center=�5-   )   $��Center=�5.   )   ��5,   !   ��  �� ElementName$ pbStatus��$ Value� �� StringFormat$ {0:0}%��5.   )   5/      51      