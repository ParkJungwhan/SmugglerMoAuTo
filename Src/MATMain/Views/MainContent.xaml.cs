using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Tools;
using HandyControl.Tools.Extension;
using MAT_Splash.Models;

namespace MATMain.Views;

/// <summary>
/// MainContent.xaml에 대한 상호 작용 논리
/// </summary>
public partial class MainContent : IRecipient<ChangeBoolMessage>
{
    private bool _isFull;

    public MainContent()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<ChangeBoolMessage>(this);

        //FullSwitch(false);
    }

    public void Receive(ChangeBoolMessage message) => FullSwitch(message.Value);

    private void FullSwitch(bool isFull)
    {
        if (_isFull == isFull) return;

        _isFull = isFull;

        if (_isFull)
        {
            BorderRootEffect.Show();
            BorderEffect.Collapse();
            BorderTitle.Collapse();
            GridMain.HorizontalAlignment = HorizontalAlignment.Stretch;
            GridMain.VerticalAlignment = VerticalAlignment.Stretch;
            PresenterMain.Margin = new Thickness();
            BorderRoot.CornerRadius = new CornerRadius(10);
            BorderRoot.Style = ResourceHelper.GetResource<Style>("BorderClip");
        }
        else
        {
            BorderRootEffect.Collapse();
            BorderEffect.Show();
            BorderTitle.Show();
            GridMain.HorizontalAlignment = HorizontalAlignment.Center;
            GridMain.VerticalAlignment = VerticalAlignment.Center;
            PresenterMain.Margin = new Thickness(0, 0, 0, 10);
            BorderRoot.CornerRadius = new CornerRadius();
            BorderRoot.Style = null;
        }
    }
}