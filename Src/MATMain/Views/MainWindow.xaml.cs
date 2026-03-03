using CommunityToolkit.Mvvm.Messaging;
using MAT_Splash.Models;
using MATMain.Views;

namespace MATMain;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow() => InitializeComponent();

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);

        NonClientAreaContent = new NonClientAreaContent();
    }
}