   M S B A M L   `   `   `  ����M  IFlightSimulatorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:FlightSimulatorApp.ControlsFlightSimulatorApp.Controls  5   	   ]3clr-namespace:FlightSimulatorApp.Controls.Steerings%FlightSimulatorApp.Controls.Steerings  5   	   [2clr-namespace:FlightSimulatorApp.Controls.Joystick$FlightSimulatorApp.Controls.Joystick  5   	   Q-clr-namespace:FlightSimulatorApp.Controls.MapFlightSimulatorApp.Controls.Map  5	   	   7 clr-namespace:FlightSimulatorAppFlightSimulatorApp  5
   	   #    FlightSimulatorApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >controls)clr-namespace:FlightSimulatorApp.Controls     5   	   I	steerings3clr-namespace:FlightSimulatorApp.Controls.Steerings     5   	   Gjoystick2clr-namespace:FlightSimulatorApp.Controls.Joystick     5   	   =map-clr-namespace:FlightSimulatorApp.Controls.Map     5	   	   2local clr-namespace:FlightSimulatorApp     5
   	     � Title$  
MainWindow��5   	   $	��750��6   $
��1333��6)   .��5      � ��5   
   �� 5       �� ImageSource$ design/BG/skyBG.jpg��6    � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��1.5* �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5!   
   (� $��1��6   $��1��6"   $��2��6/   $��40.404,10,45.256,10q�6@   .��5"      )   #FlightSimulatorApp.Controls.Map.Map  -   5          Name$	 map��5"      $��389.122��6#   $��626.094��64   
���   �$��4q�6X   5#      (� 5%   
   $��0��6   $��2��6   $��1��63   $	��100��6C   .��5&      (   "FlightSimulatorApp.Controls.Client  -   5          Name$ c��5&      
�������$��2q�6=   $��340.325��6Q   5'      (� 5(   
   $��2��6   $��1��6"   .��5)      5   /FlightSimulatorApp.Controls.Steerings.Steerings  -   5          Name$ 	steerings��5)   "   $��722.338��65   $��325.444��6E   
���   �$��4q�6j   5*      (� 5+   
   $��1��6   $��2��6"   $��732.544,2,-3.865,0q�6/   $��3��6K   .��5,      7   1FlightSimulatorApp.Controls.Joystick.MainJoystick  -   5          Name$ joystick��5,   $   $��654.322��66   5-      50      51      