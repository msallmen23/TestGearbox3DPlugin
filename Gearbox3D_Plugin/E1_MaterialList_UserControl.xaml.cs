using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IYaskawaCNCPlugin;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using Yaskawa.WPFControls;
using System.Threading.Tasks;

namespace Gearbox3D_Plugin
{
	/// <summary>
	/// Interaction logic for MaterialList_E0_UserControl.xaml
	/// </summary>
	public partial class E1_MaterialList_UserControl : CustomizablePanel
	{
		const int Delay = 250;

        public object FrameWorkElement { get; }

        public E1_MaterialList_UserControl()
		{
			InitializeComponent();
		}
		private void CustomizablePanel_Loaded(object sender, RoutedEventArgs e)
		{
			//Initializing global variables in PLC
			GlobalVariables.AddVariable("@GlobalVariables.MyMachine.Control.SendGUICommand");
			GlobalVariables.AddVariable("@GlobalVariables.MyMachine.CoordinateSystem.Selected");
			GlobalVariables.AddVariable("@GlobalVariables.MyCounter");
			GlobalVariables.AddVariable("@GlobalVariables.MyMachine.LocalZeroOffset");
			GlobalVariables.AddVariable("@GlobalVariables.MyPath.StreamStatus.G_GroupStatus1");
			// add logic here to close out of pop up zone using Unloaded
		}

		/* Support Material Profile buttons */
		private async void SelectSUP6_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true"); //ensure HMI is set to auto mode so MCODE can be executed
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanelE1");
			SendGUICommand(GUICommand.SendGCode, "M615");   //MCODE to begin heating up machine to match SUP6 temp profile (pre-extrusion process)
			await PutTaskDelay(Delay);   //delay to allow first MCode to complete prior to starting to execute M203
			SendGUICommand(GUICommand.PostNotification, "M203 is Complete!");           
			//SendGUICommand(GUICommand.SendGCode, "M203");  
		}

		private async void SelectSUP7_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanelE1");
			SendGUICommand(GUICommand.SendGCode, "M617");
			await PutTaskDelay(Delay);
			SendGUICommand(GUICommand.PostNotification, "M203 is Complete!");           
			//SendGUICommand(GUICommand.SendGCode, "M203");
		}

		private async void SelectSUP4_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanelE1");
			SendGUICommand(GUICommand.SendGCode, "M619");
			//SendGUICommand(GUICommand.PostNotification, "before delay");
			await PutTaskDelay(Delay);
			SendGUICommand(GUICommand.PostNotification, "M203 is Complete!");
			//SendGUICommand(GUICommand.SendGCode, "M203");
		}


		/* //Confirm selection on button press (running M203) */
		//private void Confirm_OnClick(object sender, RoutedEventArgs e)
		//{
		//	SendGUICommand(GUICommand.ModeSelect_Auto, "true");
		//	SendGUICommand(GUICommand.SendGCode, "M203");
		//	SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanel");
		//}

		async Task PutTaskDelay(int time)
		{
			SendGUICommand(GUICommand.SendGCode, "M203"); //MCODE to begin loading(extruding) support material from selected reel to E1
			await Task.Delay(Delay);
		}
	}
}