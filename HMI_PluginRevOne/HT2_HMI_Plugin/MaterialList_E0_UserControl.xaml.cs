using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IYaskawaCNCPlugin;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using Yaskawa.WPFControls;

namespace HMI_PluginRevOne
{
	/// <summary>
	/// Interaction logic for MaterialList_E0_UserControl.xaml
	/// </summary>
	public partial class MaterialList_E0_UserControl : CustomizablePanel
	{
		//private void CustomizablePanel_Loaded(object sender, RoutedEventArgs e)
		//{
		//	//Initializing global variables in PLC
		//	GlobalVariables.AddVariable("@GlobalVariables.MyMachine.Control.SendGUICommand");
		//	GlobalVariables.AddVariable("@GlobalVariables.MyMachine.CoordinateSystem.Selected");
		//	GlobalVariables.AddVariable("@GlobalVariables.MyCounter");
		//	GlobalVariables.AddVariable("@GlobalVariables.MyMachine.LocalZeroOffset");
		//	GlobalVariables.AddVariable("@GlobalVariables.MyPath.StreamStatus.G_GroupStatus1");
		//	//GlobalVariables.AddVariable("@GlobalVariables.MyPath.Buffer.ExecutedStatement");
		//	// add logic here to close out of pop up zone using Unloaded
		//}

		/* Material Profile buttons */
		private void SelectUltem9085_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.SendGCode, "M602");
		}

		private void SelectESD_Ultem9085_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.SendGCode, "M606");
		}

		private void SelectUltem1010_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.SendGCode, "M604");
		}

		private void SelectGF_Ultem1010_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.SendGCode, "M610");
		}

		private void SelectCFPEEK_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.SendGCode, "M608");
		}

		private void SelectABS_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.SendGCode, "M612");
		}

		/*Confirm selection on button press (running M201) */
		private void Confirm_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.SendGCode, "M201");
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanel");
		}
	}
}
