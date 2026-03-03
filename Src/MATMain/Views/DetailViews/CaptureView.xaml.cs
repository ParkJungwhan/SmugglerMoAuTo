using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MATMain.ViewModels.DetailViewModels;

namespace MATMain.Views.DetailViews;

/// <summary>
/// CaptureView.xaml에 대한 상호 작용 논리
/// </summary>
public partial class CaptureView : UserControl
{
    private CaptureViewModel ViewModel;

    public CaptureView()
    {
        InitializeComponent();

        ViewModel = new CaptureViewModel();
        DataContext = ViewModel;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        // Ctrl+V가 여기서 먹도록 포커스 확보
        Focus();
    }

    private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        {
            PasteClipboardImage();
            e.Handled = true;
        }
    }

    private void PasteClipboardImage()
    {
        try
        {
            if (!Clipboard.ContainsImage())
                return;

            BitmapSource img = Clipboard.GetImage();
            if (img == null) return;

            // 클립보드 객체가 잠길 수 있어서 Freeze 권장
            if (img.CanFreeze) img.Freeze();

            // VM에 바인딩되어 있다면 VM 프로퍼티에 넣기
            //if (DataContext is IClipboardImageHost vm)
            //    vm.ClipboardImage = img;
            ViewModel.CopyImg = img;
        }
        catch (Exception)
        {
            // 클립보드 접근은 가끔 다른 프로세스 때문에 예외가 날 수 있음.
            // 필요하면 HandyControl Growl로 토스트 띄우면 좋음.
        }
    }
}

// VM 계약(선택)
//public interface IClipboardImageHost
//{
//    BitmapSource? ClipboardImage { get; set; }
//}