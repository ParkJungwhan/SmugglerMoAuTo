using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using MAT_Splash.Models;

namespace MainAppSplash.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;

        this.Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new ProcessBroadcastMessage(ProcessState.Initializing));
    }

    private void MainWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        // 더블클릭 같은 동작 필요 없으면 이대로 OK
        if (e.ButtonState == MouseButtonState.Pressed)
            DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}