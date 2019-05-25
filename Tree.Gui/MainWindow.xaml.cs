using System;
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
using Tree.Gui.Models;
using Tree.Gui.Services;

namespace Tree.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// Services
        private readonly LoadCommentsService _loadComments;
        private readonly LoadMemberService _loadMembers;
        private readonly OutputService _outputService;
        private readonly BuildCommentsService _buildComments;

        ///  State
        private IEnumerable<Comment> _heads;

        public MainWindow(
            LoadCommentsService loadComments,
            LoadMemberService loadMembers,
            OutputService outputService,
            BuildCommentsService buildComments)
        {
            _loadComments = loadComments;
            _loadMembers = loadMembers;
            _outputService = outputService;
            _buildComments = buildComments;

            var tempPath = @"C:\Users\jack\source\repos\Tree\Tree\bin\Debug\netcoreapp2.1\names.csv";
            _heads = _loadMembers.LoadMembers(tempPath);

            InitializeComponent();
        }

        private string filePath;

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV Files (*.csv)|*.csv";

            var res = dialog.ShowDialog();

            if (res == true)
            {
                FilePath.Text = $"File Loaded: {dialog.FileName}";
                filePath = dialog.FileName;
                Calc.IsEnabled = true;
            }
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "HTML Files (*.html)|*.html"
            };

            if (dialog.ShowDialog() == true)
            {
                var comments = _loadComments.LoadComments(filePath);
                _buildComments.BuildComments(_heads, comments);

                Stream fileStream;
                if ((fileStream = dialog.OpenFile()) != null)
                {
                    _outputService.Print(_heads, fileStream, OutputService.PrintFormats.Html);
                    Browser.Source = new Uri(@"file:///" + dialog.FileName);
                    FilePath.Text = $"Data Saved: {dialog.FileName}";
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
