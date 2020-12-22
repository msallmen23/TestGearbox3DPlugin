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
    public partial class E0_MaterialList_UserControl : CustomizablePanel
    {
		const int Delay = 0;

		public E0_MaterialList_UserControl()
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
		}

		/* Fill Material Profile buttons */
		private async void SelectUltem9085_OnClick(object sender, RoutedEventArgs e)
        {
			SendGUICommand(GUICommand.ModeSelect_Auto, "true"); //ensure HMI is set to auto mode so MCODE can be executed
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanelE0");
			SendGUICommand(GUICommand.SendGCode, "M602");	//MCODE to begin heating up machine to match Ultem 9085 temp profile (pre-extrusion process)
			await PutTaskDelay(Delay);	//delay to allow first MCode to complete
			//SendGUICommand(GUICommand.SendGCode, "M201");	//MCODE to begin loading(extruding) material from selected reel up to printhead
		}

		private async void SelectESD_Ultem1010_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanelE0");
			SendGUICommand(GUICommand.SendGCode, "M606");
			await PutTaskDelay(Delay);
			//SendGUICommand(GUICommand.SendGCode, "M201");
		}

		private async void SelectUltem1010_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanelE0");
			SendGUICommand(GUICommand.SendGCode, "M604");
			await PutTaskDelay(Delay); // wait some time before executing next MCode
			//SendGUICommand(GUICommand.SendGCode, "M201");
		}

		private async void SelectGF_Ultem1010_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanelE0");
			SendGUICommand(GUICommand.SendGCode, "M610");
			await PutTaskDelay(Delay); 
			//SendGUICommand(GUICommand.SendGCode, "M201");
		}

		private async void SelectCFPEEK_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanelE0");
			SendGUICommand(GUICommand.SendGCode, "M608");
			await PutTaskDelay(Delay);
			//SendGUICommand(GUICommand.SendGCode, "M201");
		}

		private async void SelectABS_OnClick(object sender, RoutedEventArgs e)
		{
			SendGUICommand(GUICommand.ModeSelect_Auto, "true");
			SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanelE0");
			SendGUICommand(GUICommand.SendGCode, "M612"); //MCODE to begin heating up machine to match ABS temp profile (pre-extrusion process)
			await PutTaskDelay(Delay); //delay to allow first MCode to complete
			//SendGUICommand(GUICommand.SendGCode, "M201"); //MCODE to begin loading(extruding) material from selected reel
		}
		
		async Task PutTaskDelay(int time)
        {
			SendGUICommand(GUICommand.SendGCode, "M201"); //MCODE to begin loading(extruding) material from selected reel in left bay
			await Task.Delay(time);
		}

		/* //Confirm selection on button press (running M201) */
		//private void Confirm_OnClick(object sender, RoutedEventArgs e)
		//{
		//	SendGUICommand(GUICommand.ModeSelect_Auto, "true");
		//	SendGUICommand(GUICommand.SendGCode, "M201");
		//	SendGUICommand(GUICommand.OpenFloatingZone, "PurgePanel");
		//}
		//Delay method to allow Compass some time to send MCodes
	}
}
