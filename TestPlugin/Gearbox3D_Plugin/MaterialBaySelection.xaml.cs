using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IYaskawaCNCPlugin;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using Yaskawa.WPFControls;
// using System.Windows.Forms;



namespace Gearbox3D_Plugin
{
    /// <summary>
    /// Interaction logic for MaterialBaySelection.xaml
    /// </summary>

    public partial class MaterialBaySelection : CustomizablePanel
    {
        //Public Class: SomeDataModel 
        // follow link: https://stackoverflow.com/questions/5929710/dynamically-add-multiple-buttons-to-wpf-window
        /* public class SomeDataModel
         {
             public string Content { get; set; }

             public ICommand Command { get; set; }

             public SomeDataModel(string content, ICommand command)
             {
                 Content = content;
                 Command = command;
             }
         }

         private readonly ObservableCollection<SomeDataModel> _MyData = new ObservableCollection<SomeDataModel>();
         public ObservableCollection<SomeDataModel> MyData { get { return _MyData; } }
        */
        public MaterialBaySelection()
        {
            InitializeComponent();

        }

        private void CustomizablePanel_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalVariables.AddVariable("@GlobalVariables.MyMachine.Control.SendGUICommand");
            GlobalVariables.AddVariable("@GlobalVariables.MyMachine.CoordinateSystem.Selected");
            GlobalVariables.AddVariable("@GlobalVariables.MyCounter");
            GlobalVariables.AddVariable("@GlobalVariables.MyMachine.LocalZeroOffset");
            GlobalVariables.AddVariable("@GlobalVariables.MyPath.StreamStatus.G_GroupStatus1");
            GlobalVariables.AddVariable("@GlobalVariables.MyPath.Buffer.ExecutedStatement");
            GlobalVariables.AddVariable("@GlobalVariables.SwitchTo1a");
            GlobalVariables.AddVariable("@GlobalVariables.SwitchTo1b");
            GlobalVariables.AddVariable("@GlobalVariables.SwitchTo1a_E1");
            GlobalVariables.AddVariable("@GlobalVariables.SwitchTo1b_E1");
            GlobalVariables.AddVariable("@GlobalVariables.Feeder1_Active_E0");
            GlobalVariables.AddVariable("@GlobalVariables.Feeder2_Active_E0");
            GlobalVariables.AddVariable("@GlobalVariables.Feeder1_Active_E1");
            GlobalVariables.AddVariable("@GlobalVariables.Feeder2_Active_E1");
            //GlobalVariables.AddVariable("@GlobalVariables.Load");
            GlobalVariables.ReadVariable("@GlobalVariables.SwitchTo1a");
            GlobalVariables.ReadVariable("@GlobalVariables.SwitchTo1b");
            GlobalVariables.ReadVariable("@GlobalVariables.SwitchTo1a_E1");
            GlobalVariables.ReadVariable("@GlobalVariables.SwitchTo1b_E1");
            GlobalVariables.ReadVariable("@GlobalVariables.Feeder1_Active_E0");
            GlobalVariables.ReadVariable("@GlobalVariables.Feeder2_Active_E0");
            GlobalVariables.ReadVariable("@GlobalVariables.Feeder1_Active_E1");
            GlobalVariables.ReadVariable("@GlobalVariables.Feeder2_Active_E1");

            SendGUICommand(GUICommand.PostNotification, "Feeder Variables are added!"); //checking to ensure variables are added when panel is loaded
        }

        private void Reel1_Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a", true);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a_E1", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b_E1", false);
            if ((bool)GlobalVariables.ReadVariable("@GlobalVariables.SwitchTo1a") == true)
            {
                SendGUICommand(GUICommand.OpenFloatingZone, "YDefaultPlugins.Plugins.GridPanel.MaterialListPanel");
            }
            SendGUICommand(GUICommand.OpenFloatingZone, "@YDefaultPlugins.Plugins.GridPanel.MaterialListPanel");
        }

        private void Reel2_Button_Click(object sender, RoutedEventArgs e)
        {
            //Window win2 = new Window();
           // win2.Show();
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b", true);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a_E1", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b_E1", false);
            SendGUICommand(GUICommand.OpenFloatingZone, "@YDefaultPlugins.Plugins.GridPanel.MaterialPanel");

        }

        private void Reel3_Button_Click(object sender, RoutedEventArgs e)
        {
            //Window win3 = new Window();
            //win3.Show();
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a_E1", true);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b_E1", false);
            SendGUICommand(GUICommand.OpenFloatingZone, "@YDefaultPlugins.Plugins.GridPanel.MaterialPanel");


        }

        private void Reel4_Button_Click(object sender, RoutedEventArgs e)
        {
            //Window win4 = new Window();
            //win4.Show();
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a_E1", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b_E1", true);
            SendGUICommand(GUICommand.OpenFloatingZone, "@YDefaultPlugins.Plugins.GridPanel.MaterialPanel");
        }

    }
}
