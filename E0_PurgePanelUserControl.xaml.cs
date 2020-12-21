using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IYaskawaCNCPlugin;
using System.Windows.Media;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading.Tasks;

namespace Gearbox3D_Plugin
{
    /// <summary>
    /// Interaction logic for E0_PurgePanelUserControl.xaml
    /// </summary>
    /// 
    public partial class E0_PurgePanelUserControl : Yaskawa.WPFControls.CustomizablePanel, System.Windows.Markup.IComponentConnector
    {
        const int Delay = 250;

        //public UserControl ParentControl { get; set; }  //This is a getter/setter that can access 

        public E0_PurgePanelUserControl()
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
            await Task.Delay(Delay);
            SendGUICommand(GUICommand.PostNotification, "Is purging complete?");
        }

        public async Task PutTaskDelay(int time)
        {
            SendGUICommand(GUICommand.SendGCode, "M200"); // perform purge and wipe on E0
            await Task.Delay(time);
        }

    }

    // try to close the panel using this class
    //public partial class CloseWindowUserControl: System.Windows.Controls.UserControl
    //{
    //   //E0_PurgePanelUserControl obj = new E0_PurgePanelUserControl();   
    //}
}
