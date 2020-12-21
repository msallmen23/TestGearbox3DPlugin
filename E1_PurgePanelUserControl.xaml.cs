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
    /// Interaction logic for E0_PurgePanelUserControl.xaml
    /// </summary>
    public partial class E1_PurgePanelUserControl : CustomizablePanel
    {
        const int Delay = 250;
        public E1_PurgePanelUserControl()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            SendGUICommand(GUICommand.OpenFloatingZone, "MaterialBayPanel");
        }

        private async void NoButton_Click(object sender, RoutedEventArgs e)
        {
            SendGUICommand(GUICommand.PostNotification, "Check printer to verify purging is complete!");
            await Task.Delay(Delay); // test with this method and if this doesn't work then use PutTaskDelay(Delay);
            SendGUICommand(GUICommand.PostNotification, "Is purging complete?");
        }

        public async Task PutTaskDelay(int time)
        {
            SendGUICommand(GUICommand.SendGCode, "M205"); // perform purge and wipe on E0, you are waiting on Jared for this. 
            await Task.Delay(time);
        }
    }
}
