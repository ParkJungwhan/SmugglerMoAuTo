using System.Windows;
using System.Windows.Controls;
using HandyControl.Data;

namespace MATMain.Views;

/// <summary>
/// LeftMainContent.xaml에 대한 상호 작용 논리
/// </summary>
public partial class LeftMainContent
{
    //private string _searchKey;

    public LeftMainContent() => InitializeComponent();

    private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void ButtonAscending_OnClick(object sender, RoutedEventArgs e)
    {
    }

    private void SearchBar_OnSearchStarted(object sender, FunctionEventArgs<string> e)
    {
    }

    private void FilterItems()
    {
    }

    //private void GroupItems(TabControl tabControl, DemoInfoModel demoInfo)
    //{
    //}
}