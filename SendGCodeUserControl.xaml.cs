using IYaskawaCNCPlugin;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Yaskawa.WPFControls;

//var filecontent = string.empty;
//var filepath = string.empty;

//using (openfiledialog openfiledialog = new openfiledialog())
//{
//    openfiledialog.initialdirectory = "c:\\";
//    openfiledialog.filter = "txt files (*.txt)|*.txt|all files (*.*)|*.*";
//    openfiledialog.filterindex = 2;
//    openfiledialog.restoredirectory = true;

//    if (openfiledialog.showdialog() == dialogresult.ok)
//    {
//        //get the path of specified file
//        filepath = openfiledialog.filename;

//        //read the contents of the file into a stream
//        var filestream = openfiledialog.openfile();

//        using (streamreader reader = new streamreader(filestream))
//        {
//            filecontent = reader.readtoend();
//        }
//    }
//}

//messagebox.show(filecontent, "file content at path: " + filepath, messageboxbuttons.ok);

namespace Gearbox3D_Plugin
{
    /// <summary>
    /// Interaction logic for SendGCodeUserControl.xaml
    /// </summary>
    /// 

    //Public Class: SomeDataModel 
    // follow link: https://stackoverflow.com/questions/5929710/dynamically-add-multiple-buttons-to-wpf-window
    /* public partial class SomeDataModel : CustomizablePanel
  {
      public new string Content { get; set; }

      public ICommand Command { get; set; }

      public SomeDataModel(string content, ICommand command)
      {
          Content = content;
          Command = command;
      }
  }*/
    //private readonly ObservableCollection<SomeDataModel> _MyData = new ObservableCollection<SomeDataModel>();
    //public ObservableCollection<SomeDataModel> MyData { get { return _MyData; } }

    /* 
     * The following class performs the logic to start a print:
     * Step 1. The user will select a file (by pressing "Select File" button and choose a GCode/MCode file to print from).
     * Step 2. The user will select "Start Print" button to execute this process. 
     */
    public partial class SendGCodeUserControl : CustomizablePanel
    {
        //private string UsbDrive = "";
        //public string filePath_ = "";
        //string SelectedGcodeFile;
        //string sSelectedFolder;
        //string[] GcodeFiles;
        static public string filePath_ = "";

        //The following constructor is needed to construct an instance of the SendGCodeUserControl panel
        public SendGCodeUserControl()
        {
            InitializeComponent();
            //List<FileNames> filenames = new List<FileNames>(); //this list declaration is needed to locally populate list versus using microsofts api
            //filenames.Add(new FileNames() { FileName = "testing.gcode" });
            //filenames.Add(new FileNames() { FileName = "test.gcode" });
            //filenames.Add(new FileNames() { FileName = "testing.gcode" });
            //FileMenu.ItemsSource = filenames.ToString();
            //this.DataContext = this;
        }

        private void CustomizablePanel_Loaded(object sender, RoutedEventArgs e)
        {
            //string DrivePath; 
            //PopulateListBox(fileMenu, @"D:\", "*.GCODE"); //calling the following method to populate listBox with GCODE files from USB drive(D:\) when the panel first loads.
            // MPiec Global variable declarations for printMenuPanel
            GlobalVariables.AddVariable("@GlobalVariables.MyMachine.Control.SendGUICommand");
            GlobalVariables.AddVariable("@GlobalVariables.MyMachine.CoordinateSystem.Selected");
            GlobalVariables.AddVariable("@GlobalVariables.MyCounter");
            GlobalVariables.AddVariable("@GlobalVariables.MyMachine.LocalZeroOffset");
            GlobalVariables.AddVariable("@GlobalVariables.MyPath.StreamStatus.G_GroupStatus1");
            //GlobalVariables.AddVariable("@GlobalVariables.MyMachine.Control.CycleStart");
            PopulateListBox(fileMenu, @"E:\", "*.GCODE");
            //PopulateListBox(fileMenu, @"D\", "*.GCODE"); //You need to implement the logic for when a different drive path is selected
        }

        public void PopulateListBox(ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType); 
            foreach (FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);

            }
        }
        /*//private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        {
        //    //code for adding a filter is below in closed comment block (see https://stackoverflow.com/questions/24449988/how-to-get-file-path-from-openfiledialog-and-folderbrowserdialog)
        //    //CommonFileDialogStandardFilters.TextFiles.ShowExtensions = true;

        //    //CommonFileDialogFilter rtfFilter = new CommonFileDialogFilter("GCode Files", ".rtf");
        //    //rtfFilter.ShowExtensions = true;

        //    //obj.Filters.Add(CommonFileDialogStandardFilters.TextFiles);
        //    //obj.Filters.Add(rtfFilter);
        //    //OpenFileDialog openFileDialog = new OpenFileDialog();
        //    //openFileDialog.InitialDirectory = @"D:\";
        //    //openFileDialog.Filter = "GCODE files (*.GCODE)|*.GCODE";
        //    //openFileDialog.FilterIndex = 1;

        //    //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.);
        //    //if (openFileDialog.ShowDialog() == true)
        //    //{
        //    //    string filePath = openFileDialog.FileName;
        //    //    filePath_ = Path.GetFullPath(filePath).Replace(Name,filePath);
        //    //} 
            PopulateListBox(fileMenu, @"D:\", "*.GCODE");
        }*/

        //The following method implements the logic when the "Start Print" button is pressed.
        private void SendButton_OnClick(object sender, RoutedEventArgs e)
        {
            myButton.Content = fileMenu.SelectedItem; //get rid of this (just use it for testing)
            SendGUICommand(GUICommand.SendGCode, (string)(fileMenu.SelectedItem)); //you need to test this
           // SendGUICommand(GUICommand.SendGCode, File.ReadAllText((string)fileMenu.SelectedItem)); //get rid of ReadAllText()
            //GlobalVariables.WriteVariable("@GlobalVariables.MyMachine.Control.CycleStart", true);
            Window win = Window.GetWindow(this); //Close the popup. This can only be tested when connected to printer. 
            win.Close();
        }

        // The method below is the working print menu using the file explorer as the file selection menu.
        //private void OpenButton_OnClick(object sender, RoutedEventArgs e)
        //{
        //    CommonOpenFileDialog obj = new CommonOpenFileDialog();
        //    string temp = "";
        //    //add code within this if statment
        //    if (obj.ShowDialog() == CommonFileDialogResult.Ok)
        //    {
        //         = obj.FileName;             // may return shortened path, so return value needs to be checked for its full path
        //        filePath_ = Path.GetFullPath(filePath);     // will throw PathTooLongException if full path exceeds limits
        //    }
        //    //fileNameDisplayText.Text = filePath_;
        //    SendGUICommand(GUICommand.ModeSelect_Auto, "true");
        //}

    }
    //void ChangeBackground(object sender, RoutedEventArgs e)
    //{
    //    if (btn.Background == Brushes.Red)
    //    {
    //        btn.Background = new LinearGradientBrush(Colors.LightBlue, Colors.SlateBlue, 90);
    //        btn.Content = "Control background changes from red to a blue gradient.";
    //    }
    //    else
    //    {
    //        btn.Background = Brushes.Red;
    //        btn.Content = "Background";
    //    }
    //}

    //this directory public class contains the filepath that is accessible and needed for generating a list "manually" versus using microsofts api
    /*public class FileNames
     {
        public string FileName { get; set; }

     }*/

    

    //private void SendButton_OnClick(object sender, RoutedEventArgs e)
    //{
    //    SendGUICommand(GUICommand.SendGCode, File.ReadAllText(filePath_));
    //    GlobalVariables.WriteVariable("@GlobalVariables.MyMachine.Control.CycleStart", true);
    //}
}
        
    //public sealed class OpenFileDialog : System.Windows.Forms.FileDialog
    //{

    //}




