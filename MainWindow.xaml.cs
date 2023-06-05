using Microsoft.Win32;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Picheres1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapConverter bc = new BitmapConverter();
        ADBService ADBService = new ADBService();
        private string storageDirPath;
        public MainWindow()
        {
            InitializeComponent();

            UpdateImageList();
            

        }
        private void UpdateImageList()
        {
            ImageLB.ItemsSource = ADBService.PicherImage();
        }
        private void ShowImage(Image image)
        {
            ImageView.Source = bc.ConvertByteArrayToBitmapImage(image.Data);
        }
        private void ShowImage(string path)
        {
            ImageView.Source = new BitmapImage(new Uri(path));
        }
        private void AddPichersBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                
                var bitmapImage = new BitmapImage(new Uri(op.FileName));
                Image ne_w = new Image() {Name = op.FileName , Data = bc.ConvertBitmapImageToByteArray(bitmapImage) };
                ADBService.AddPcihers( ne_w );
                ShowImage( ne_w );
                UpdateImageList();
            }
            else
            {
                MessageBox.Show("Image not selected");
            }
        }

        private void RemovePichersBTN_Click(object sender, RoutedEventArgs e)
        {
            if(ImageLB.SelectedItem != null) 
            ADBService.RemovePichers(ImageLB.SelectedItem.ToString());
            UpdateImageList();
        }

        private void RenamePichersBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ImageLB.SelectedItem != null)
            {
                RenameWindow widow = new RenameWindow();
                widow.ShowDialog();
                if (widow.DialogResult == true)
                {
                    ADBService.RenamePichers(ImageLB.SelectedItem.ToString(), widow.RenameTB.Text);
                    UpdateImageList();
                }
            }
        }

        private void ImageLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if( ImageLB.SelectedItem != null )
            {
                ImageView.Source = bc.ConvertByteArrayToBitmapImage(ADBService.SearchImage(ImageLB.SelectedItem.ToString()));
            }
        }
    }
}
