using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DVDIndexer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedPath = string.Empty;
            selectedPath = GetFolderPath(selectedPath);
            txtInput.Text = selectedPath;
        }

        private static string GetFolderPath(string selectedPath)
        {
            var folderBrowser = new FolderBrowserDialog();
            var result = folderBrowser.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                selectedPath = folderBrowser.SelectedPath;
            }

            return selectedPath;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedPath = string.Empty;
            selectedPath = GetFolderPath(selectedPath);
            txtOutput.Text = System.IO.Path.Combine(selectedPath, "result.json");
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;

            if (!string.IsNullOrEmpty(txtOutput_Copy.Text.Trim()))
            {
                var filePath = txtInput.Text ?? @"..\";

                var outputPath = txtOutput.Text ?? @"..\";

                var uniqueId = txtOutput_Copy.Text.Trim();

                List<FileList> filelist = await GetFiles(filePath, outputPath, uniqueId);


                await Task.Run(async () =>
                {
                    var json = JsonConvert.SerializeObject(filelist);

                    await File.WriteAllTextAsync(outputPath, json);
                });
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please provide unique ID.");
            }
            btnStart.IsEnabled = true;
        }

        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            txtOutput_Copy.Text = Guid.NewGuid().ToString();
        }

        private static async Task<List<FileList>> GetFiles(string filePath, string outputPath, string uniqueId)
        {

            return await Task.Run(async () =>
             {
                 List<FileInfo> allfiles = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).Select(x => new FileInfo(x)).ToList();

                 List<FileList> filelists = File.Exists(outputPath) ? JsonConvert.DeserializeObject<List<FileList>>(await File.ReadAllTextAsync(outputPath)) : new List<FileList>();
                 var fileList = new FileList();

                 var drive = DriveInfo.GetDrives().Where(i => System.IO.Path.GetFullPath(filePath).Contains(i.RootDirectory.FullName)).FirstOrDefault();
                 fileList.Source = drive.DriveType;
                 fileList.SourceName = !string.IsNullOrEmpty(drive.VolumeLabel) ? drive.VolumeLabel : DriveType.Unknown.ToString();
                 fileList.Id = uniqueId;

                 foreach (var file in allfiles)
                 {
                     fileList.Add(new FileInfoSerializable(file));
                 }

                 filelists.Add(fileList);
                 return filelists;
             });
        }
    }
}
