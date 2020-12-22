using IYaskawaCNCPlugin;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Yaskawa.WPFControls;

namespace Gearbox3D_Plugin
{
    /// <summary>
    /// Interaction logic for CommonToAllUserControls.xaml
    /// </summary>
    public partial class CommonToAllUserControls : CustomizablePanel
    {
        public CommonToAllUserControls()
        {
            InitializeComponent();
           
        }

        public void CustomizablePanel_Loaded(object sender, RoutedEventArgs e)
        {
            // MPiec Global variable declarations for printMenuPanel, MaterialStatusPanel, ...,
            GlobalVariables.AddVariable("@GlobalVariables.MyMachine.Control.SendGUICommand");
            GlobalVariables.AddVariable("@GlobalVariables.MyMachine.CoordinateSystem.Selected");
            GlobalVariables.AddVariable("@GlobalVariables.MyCounter");
            GlobalVariables.AddVariable("@GlobalVariables.MyMachine.LocalZeroOffset");
            GlobalVariables.AddVariable("@GlobalVariables.MyPath.StreamStatus.G_GroupStatus1");
            GlobalVariables.AddVariable("@GlobalVariables.BEGIN_LEVEL");
            // The following commands will execute the common processes that are needed to get the printer in a ready state
            SendGUICommand(GUICommand.ServoPowerOn, "true"); // enabling the servos
            SendGUICommand(GUICommand.ModeSelect_Auto, "true"); // switching to auto mode
            GlobalVariables.WriteVariable("@GlobalVariables.BEGIN_LEVEL", "true"); // starting to execute bed leveling
            SendGUICommand(GUICommand.PostNotification, "Is bed-leveling complete?");
        }

        public void BedLevel_ConfirmButton(object sender, RoutedEventArgs e)
        {
            //SendGUICommand(GUICommand.ModeSelect_Manual, "true");
            Window win = Window.GetWindow(this); //Close the popup
            win.Close();
        }

        public void BedLevel_NoConfirmButton(object sender, RoutedEventArgs e)
        {
            GlobalVariables.WriteVariable("@GlobalVariables.BEGIN_LEVEL", "true");
        }


    }
}
