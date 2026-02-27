using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MATMain.ViewModels;

internal partial class LeftMainContentModel : ObservableObject
{
    [ObservableProperty]
    private string addTitle;

    [ObservableProperty]
    private RelayCommand addItem;

    [ObservableProperty]
    private RelayCommand<SelectionChangedEventArgs> switchitemCmd;

    [ObservableProperty]
    private ObservableCollection<SubItemInfo> itemList;

    [ObservableProperty]
    private int selectedIndex;

    public LeftMainContentModel()
    {
        addTitle = "Add Item";

        ItemList = new ObservableCollection<SubItemInfo>();

        AddItem = new RelayCommand(CmdAddItem);
        SwitchitemCmd = new RelayCommand<SelectionChangedEventArgs>(CmdSwitchItem);
    }

    private void CmdSwitchItem(SelectionChangedEventArgs? args)
    {
        if (args is not null)
        {
            if (args.AddedItems[0] is SubItemInfo)
            {
                var selectedItem = args.AddedItems[0] as SubItemInfo;
                //selectedIndex = args.
            }
        }
    }

    private void CmdAddItem()
    {
        ItemList.Add(new SubItemInfo() { Name = $"Sub Item {ItemList.Count + 1}" });
    }
}

public class SubItemInfo
{
    public string Name { get; set; } = "-";
    public bool IsNew { get; set; }
    public string QueriesText { get; set; }
    public bool IsVisible { get; set; } = true;
}