using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IYaskawaCNCPlugin;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using Yaskawa.WPFControls;
using System.Windows.Data;

namespace Gearbox3D_Plugin
{
    /// <summary>
    /// Interaction logic for E0_PurgePanelUserControl.xaml
    /// </summary>
    public partial class LoadOrUnloadUserControl : CustomizablePanel
    {
        public LoadOrUnloadUserControl()
        {
            InitializeComponent();
        }

        private void CustomizablePanel_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalVariables.AddVariable("@GlobalVariables.E0_MaterialIndex");
            SendGUICommand(GUICommand.ModeSelect_Auto, "true");
        }
        // Add code for the two following methods to close the current zone 
        private void LoadOn_ButtonClick(object sender, RoutedEventArgs e)
        {
            if(GlobalVariables.ReadVariable("@GlobalVariables.E0_MaterialIndex").ToString()=="0")
            {
                SendGUICommand(GUICommand.OpenFloatingZone, "MaterialListPanel"); // Load button opens MaterialListPanel(User will select Fill Material for loading E0)
            }
            else
            {
                SendGUICommand(GUICommand.OpenFloatingZone, "MaterialBayPanel");
            }
        }

        private void UnloadOn_ButtonClick(object sender, RoutedEventArgs e)
        {
            SendGUICommand(GUICommand.OpenFloatingZone, "MaterialBayPanel");
            SendGUICommand(GUICommand.SendGCode, "M202"); //M202 is sent to PLC, which executes the unloading of the fill material from E0
        }
    }
}
