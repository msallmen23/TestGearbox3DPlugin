using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IYaskawaCNCPlugin;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using Yaskawa.WPFControls;

namespace Gearbox3D_Plugin
{
    /// <summary>
    /// Interaction logic for LoadOrUnload_E1_UserControl
    /// 
    /// This is the Loading/Unloading support material panel for E1
    /// If user presses load button and there is no support material loaded, then the MaterialListPanel for E1 opens
    /// If user already has material selected for loading E1 then user will not allowed to be able to get to the MaterialListPanel_E1 UI screen
    /// 
    /// </summary>
    public partial class LoadOrUnload_E1_UserControl : CustomizablePanel
    {
        public LoadOrUnload_E1_UserControl()
        {
            InitializeComponent();
        }

        private void CustomizablePanel_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalVariables.AddVariable("@GlobalVariables.E1_MaterialIndex");
            SendGUICommand(GUICommand.ModeSelect_Auto, "true");
        }
        //The following "Event handler" method for the Load Button is "triggered" when the user presses the Load Button (a button "Event")
        private void LoadOn_ButtonClick(object sender, RoutedEventArgs e)
        {
            //This works, but also try using a Double to ensure that it can handle cases like 0.0
            if (GlobalVariables.ReadVariable("@GlobalVariables.E1_MaterialIndex").ToString() == "0")
            {
                SendGUICommand(GUICommand.OpenFloatingZone, "MaterialListPanel_E1"); // Load button opens MaterialListPanel_E1(User will select support Material for loading E1)
            }
            else
            {
                SendGUICommand(GUICommand.OpenFloatingZone, "MaterialBayPanel"); //Return to MaterialBayPanel, where user can select another reel to load or unload material from
            }
        }

        private void UnloadOn_ButtonClick(object sender, RoutedEventArgs e)
        {
            SendGUICommand(GUICommand.OpenFloatingZone, "MaterialBayPanel"); //Return to MaterialBayPanel, where user can select another reel to load or unload material from
            SendGUICommand(GUICommand.SendGCode, "M204"); //M204 is sent to PLC, which executes the unloading of the support material from E1 to the respective reel(3 or 4) in Right Bay
        }
    }
}
