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
    /// YOU NEED TO FIGURE OUT HOW TO CLOSE A PANEL. 
    /// THIS IS NOW AN ISSUE DUE TO PURGE PANELS NEED TO BE ABLE TO OPEN MATERIAL BAY PANEL WHEN IT IS ALREADY OPEN.
    /// (WORSE CASE YOU CAN CREATE ANOTHER MATERIAL BAY PANEL CONTROL, BUT THAT WOULD BE REDUNDANT). 
    /// 
    /// <summary("Read Me!")>
    /// Interaction logic for MaterialBaySelection.xaml
    /// This panel allows the user to select a Reel within one of the respective bays for loading or unloading material.
    /// Selecting a reel will tie the respective feeder motor to the extruder for a given bay. For example: When the user 
    /// selects(presses) "Reel 1"(located in Left Bay) the feeder(switchTo1a) for that reel will be tied to extruder E0. 
    /// Pressing the "Reel 1" button will take the user to next page(zone) on the UI where user will chose to either load or unload
    /// material in "Reel 1". This process is triggered by pressing any of the four reel buttons, 
    /// whereby SendGUI commands are called that send the proper MCode and load the next zone.
    /// 
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

        //The following constructor is needed to create an instance of the MaterialBaySelection panel on the UI. 
        //If this constructor isn't created then Buttons/TextBlocks will "NOT" be populated on the UI panel.
        public MaterialBaySelection()
        {
            InitializeComponent();
        }

        ///public static int whichSelected = -1;

        //public object Counter { get; private set; }

        private void CustomizablePanel_Loaded(object sender, RoutedEventArgs e)
        {
            //Adding MPiec global variables so that they are accessible to plugin for reading/writing 
            //GlobalVariables.AddVariable("@GlobalVariables.MyMachine.Control.SendGUICommand");
            //GlobalVariables.AddVariable("@GlobalVariables.MyMachine.CoordinateSystem.Selected");
            //GlobalVariables.AddVariable("@GlobalVariables.MyCounter");
            //GlobalVariables.AddVariable("@GlobalVariables.MyMachine.LocalZeroOffset");
            //GlobalVariables.AddVariable("@GlobalVariables.MyPath.StreamStatus.G_GroupStatus1");
            //GlobalVariables.AddVariable("@GlobalVariables.MyPath.Buffer.ExecutedStatement");
            GlobalVariables.AddVariable("@GlobalVariables.SwitchTo1a");
            GlobalVariables.AddVariable("@GlobalVariables.SwitchTo1b");
            GlobalVariables.AddVariable("@GlobalVariables.SwitchTo1a_E1");
            GlobalVariables.AddVariable("@GlobalVariables.SwitchTo1b_E1");
            //Reading global variables
            GlobalVariables.ReadVariable("@GlobalVariables.SwitchTo1a");
            GlobalVariables.ReadVariable("@GlobalVariables.SwitchTo1b");
            GlobalVariables.ReadVariable("@GlobalVariables.SwitchTo1a_E1");
            GlobalVariables.ReadVariable("@GlobalVariables.SwitchTo1b_E1");
            SendGUICommand(GUICommand.ModeSelect_Auto, "true");
        }
        //TEST FOLLOWING METHOD ON PRINTER AND IT SHOULD WORK!
        private void Reel1_Button_Click(object sender, RoutedEventArgs e)
        {
                GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a", true);
                GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a_E1", false);
                GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b", false);
                GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b_E1", false);
                SendGUICommand(GUICommand.OpenFloatingZone, "MaterialPanel");
        }

        private void Reel2_Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b", true);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a_E1", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b_E1", false);
            SendGUICommand(GUICommand.OpenFloatingZone, "MaterialPanel");
        }

        private void Reel3_Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a_E1", true);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b_E1", false);
            SendGUICommand(GUICommand.OpenFloatingZone, "MaterialPanel_");
        }

        private void Reel4_Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1a_E1", false);
            GlobalVariables.WriteVariable("@GlobalVariables.SwitchTo1b_E1", true);
            SendGUICommand(GUICommand.OpenFloatingZone, "MaterialPanel_");
        }
        
        //Add code in each of the methods below(your going to need to change this based on a button being pressed!)
        public void TextBlock_TextInput(object sender, TextCompositionEventArgs e)
        {
            //Insert code here to display material type that the user selects.
            //Also you could implement a check here to ensure that the user is loading the same material
            // in each reel otherwise notify the user to change material so that material in each reel matches.
        }

        public void TextBlock_TextInput_1(object sender, TextCompositionEventArgs e)
        {
            //Insert code here to display material type that the user selects.
            //Also you could implement a check here to ensure that the user is loading the same material
            // in each reel otherwise notify the user to change material so that material in each reel matches.
        }


    }
}
