using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MATMain.ViewModels.DetailViewModels
{
    public partial class CaptureViewModel : ObservableObject
    {
        [ObservableProperty]
        private RelayCommand cmdPaste;

        [ObservableProperty]
        private BitmapSource copyImg;

        [ObservableProperty]
        public Stretch selectedStretch;

        public CaptureViewModel()
        {
            cmdPaste = new(CopyClipboardImage);
            selectedStretch = Stretch.Uniform;
        }

        private void CopyClipboardImage()
        {
            var image = Clipboard.GetImage();
            if (image != null)
            {
                // Assign to an Image control in XAML
                //MyImageControl.Source = image;
                copyImg = image;
            }
        }
    }
}