using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using HandyControl.Tools;
using HandyControl.Tools.Extension;

namespace MATMain.Views;

/// <summary>
/// MainWindowContent.xaml에 대한 상호 작용 논리
/// </summary>
public partial class MainWindowContent : Border
{
    private GridLength _columnDefinitionWidth;

    public MainWindowContent() => InitializeComponent();

    private void OnLeftMainContentShiftOut(object sender, RoutedEventArgs e)
    {
        ButtonShiftOut.Collapse();
        GridSplitter.IsEnabled = false;

        double targetValue = -ColumnDefinitionLeft.MaxWidth;
        _columnDefinitionWidth = ColumnDefinitionLeft.Width;
        DoubleAnimation animation = AnimationHelper.CreateAnimation(targetValue, milliseconds: 100);
        animation.FillBehavior = FillBehavior.Stop;
        animation.Completed += OnAnimationCompleted;
        LeftMainContent.RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);

        void OnAnimationCompleted(object obj, EventArgs args)
        {
            animation.Completed -= OnAnimationCompleted;
            LeftMainContent.RenderTransform.SetCurrentValue(TranslateTransform.XProperty, targetValue);

            Grid.SetColumn(MainContent, 0);
            Grid.SetColumnSpan(MainContent, 2);

            ColumnDefinitionLeft.MinWidth = 0;
            ColumnDefinitionLeft.Width = new GridLength();
            ButtonShiftIn.Show();
        }
    }

    private void OnLeftMainContentShiftIn(object sender, RoutedEventArgs e)
    {
        ButtonShiftIn.Collapse();
        GridSplitter.IsEnabled = true;

        double targetValue = ColumnDefinitionLeft.Width.Value;

        DoubleAnimation animation = AnimationHelper.CreateAnimation(targetValue, milliseconds: 100);
        animation.FillBehavior = FillBehavior.Stop;
        animation.Completed += OnAnimationCompleted;
        LeftMainContent.RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);

        void OnAnimationCompleted(object obj, EventArgs args)
        {
            animation.Completed -= OnAnimationCompleted;
            LeftMainContent.RenderTransform.SetCurrentValue(TranslateTransform.XProperty, targetValue);

            Grid.SetColumn(MainContent, 1);
            Grid.SetColumnSpan(MainContent, 1);

            ColumnDefinitionLeft.MinWidth = 240;
            ColumnDefinitionLeft.Width = _columnDefinitionWidth;
            ButtonShiftOut.Show();
        }
    }

    private void DrawerCode_OnOpened(object sender, RoutedEventArgs e)
    {
    }

    private void DrawerCode_Closed(object sender, RoutedEventArgs e)
    {
        // 설정이 완료됐으니 반영하라. IsDirty를 확인하여 진행
    }
}