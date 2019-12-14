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
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            var filePath = txtInput.Text ?? @"..\";

            var outputPath = txtOutput.Text ?? @"..\";

            List<FileInfo> allfiles = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).Select(x => new FileInfo(x)).ToList();

            FileList filelist = File.Exists(outputPath) ? JsonConvert.DeserializeObject<FileList>(File.ReadAllText(outputPath)) : new FileList();

            foreach (var file in allfiles)
            {
                filelist.Add(new FileInfoSerializable(file));
            }

            var json = JsonConvert.SerializeObject(filelist);

            File.WriteAllText(outputPath, json);
            btnStart.IsEnabled = true;
        }
    }
}
