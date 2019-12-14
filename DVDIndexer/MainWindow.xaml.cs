using Newtonsoft.Json;
using System.Collections.Generic;
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

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            var filePath = txtInput.Text ?? @"..\";

            var outputPath = txtOutput.Text ?? @"..\";

            FileList filelist = await GetFiles(filePath, outputPath);

            await Task.Run(async () =>
            {
                var json = JsonConvert.SerializeObject(filelist);

                await File.WriteAllTextAsync(outputPath, json);
            });
            btnStart.IsEnabled = true;
        }

        private static async Task<FileList> GetFiles(string filePath, string outputPath)
        {

            return await Task.Run(async () =>
             {
                 List<FileInfo> allfiles = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).Select(x => new FileInfo(x)).ToList();

                 FileList filelist = File.Exists(outputPath) ? JsonConvert.DeserializeObject<FileList>(await File.ReadAllTextAsync(outputPath)) : new FileList();
                 foreach (var file in allfiles)
                 {
                     filelist.Add(new FileInfoSerializable(file));
                 }

                 return filelist;
             });
        }
    }
}
