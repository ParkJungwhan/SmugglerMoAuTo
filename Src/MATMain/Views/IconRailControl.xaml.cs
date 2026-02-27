using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;

namespace MATMain.Views;

/// <summary>
/// IconRailControl.xaml에 대한 상호 작용 논리
/// </summary>
public partial class IconRailControl : UserControl
{
    private INotifyCollectionChanged? _incc;

    public IconRailControl()
    {
        InitializeComponent();
    }

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(IconRailControl),
            new PropertyMetadata(null, OnItemsSourceChanged));

    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(object), typeof(IconRailControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (d, e) => ((IconRailControl)d).RefreshSelectedIcon()));

    public RelayCommand? ItemClickCommand
    {
        get => (RelayCommand?)GetValue(ItemClickCommandProperty);
        set => SetValue(ItemClickCommandProperty, value);
    }

    public static readonly DependencyProperty ItemClickCommandProperty =
        DependencyProperty.Register(nameof(ItemClickCommand), typeof(RelayCommand), typeof(IconRailControl));

    public RelayCommand? AddCommand
    {
        get => (RelayCommand?)GetValue(AddCommandProperty);
        set => SetValue(AddCommandProperty, value);
    }

    public static readonly DependencyProperty AddCommandProperty =
        DependencyProperty.Register(nameof(AddCommand), typeof(RelayCommand), typeof(IconRailControl));

    // 아이템 유무 (0개면 선택 슬롯 숨김)
    public bool HasItems
    {
        get => (bool)GetValue(HasItemsProperty);
        private set => SetValue(HasItemsPropertyKey, value);
    }

    private static readonly DependencyPropertyKey HasItemsPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(HasItems), typeof(bool), typeof(IconRailControl),
            new PropertyMetadata(false));

    public static readonly DependencyProperty HasItemsProperty = HasItemsPropertyKey.DependencyProperty;

    // 선택 아이콘 소스
    public ImageSource? SelectedIconSource
    {
        get => (ImageSource?)GetValue(SelectedIconSourceProperty);
        private set => SetValue(SelectedIconSourcePropertyKey, value);
    }

    private static readonly DependencyPropertyKey SelectedIconSourcePropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(SelectedIconSource), typeof(ImageSource), typeof(IconRailControl),
            new PropertyMetadata(null));

    public static readonly DependencyProperty SelectedIconSourceProperty = SelectedIconSourcePropertyKey.DependencyProperty;

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var ctl = (IconRailControl)d;

        if (ctl._incc != null)
            ctl._incc.CollectionChanged -= ctl.OnCollectionChanged;

        ctl._incc = e.NewValue as INotifyCollectionChanged;
        if (ctl._incc != null)
            ctl._incc.CollectionChanged += ctl.OnCollectionChanged;

        ctl.SyncSelectionAndFlags();
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        SyncSelectionAndFlags();
    }

    private void SyncSelectionAndFlags()
    {
        var list = ItemsSource?.Cast<object>().ToList() ?? new();
        HasItems = list.Count > 0;

        if (!HasItems)
        {
            SelectedItem = null;
            SelectedIconSource = null;
            return;
        }

        // SelectedItem이 없거나, 삭제되어 목록에 없으면 첫 항목 선택
        if (SelectedItem == null || !list.Any(x => ReferenceEquals(x, SelectedItem) || (x?.Equals(SelectedItem) ?? false)))
            SelectedItem = list[0];

        RefreshSelectedIcon();
    }

    private void RefreshSelectedIcon()
    {
        if (SelectedItem == null)
        {
            SelectedIconSource = null;
            return;
        }

        // SelectedItem.Icon (ImageSource) 사용 가정
        var prop = SelectedItem.GetType().GetProperty("Icon");
        SelectedIconSource = prop?.GetValue(SelectedItem) as ImageSource;
    }
}