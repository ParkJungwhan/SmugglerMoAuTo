using System.Collections.ObjectModel;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MATMain.Models;

namespace MATMain.ViewModels;

internal partial class LeftMainContentModel : ObservableObject
{
    [ObservableProperty]
    private string addTitle;

    [ObservableProperty]
    private RelayCommand addItem;

    [ObservableProperty]
    private RelayCommand changeItem;

    [ObservableProperty]
    private RelayCommand<SelectionChangedEventArgs> switchitemCmd;

    [ObservableProperty]
    private ObservableCollection<SubItemInfo> itemList;

    [ObservableProperty]
    private int selectedIndex;

    private SubItemInfo SelectInfo;

    public LeftMainContentModel()
    {
        addTitle = "Add Item";

        ItemList = new ObservableCollection<SubItemInfo>();

        AddItem = new RelayCommand(CmdAddItem);
        ChangeItem = new RelayCommand(CmdChangeName);
        SwitchitemCmd = new RelayCommand<SelectionChangedEventArgs>(CmdSwitchItem);
    }

    private void CmdChangeName()
    {
        var sss = SelectInfo.Name;
    }

    private void CmdSwitchItem(SelectionChangedEventArgs? args)
    {
        if (args is not null)
        {
            if (args.AddedItems[0] is SubItemInfo)
            {
                SelectInfo = args.AddedItems[0] as SubItemInfo;
                if (SelectInfo is null) return;

                // 메시지로 maincontent에 선택한 항목을 보여주는걸로 던져야 함
                WeakReferenceMessenger.Default.Send<ChangeItemMessage>(new ChangeItemMessage(SelectInfo));
            }
        }
    }

    private void CmdAddItem()
    {
        var subitem = new SubItemInfo()
        {
            Name = $"Sub Item {ItemList.Count + 1}",
        };
        subitem.SetTicker();

        ItemList.Add(subitem);
    }
}

public class SubItemInfo
{
    public string Name { get; set; } = "-";
    public bool IsNew { get; set; }
    public bool IsVisible { get; set; } = true;
    public string TimeTicker { get; private set; }

    //public string QueriesText { get; set; }
    public void SetTicker()
    {
        TimeTicker = $"{Name.Replace(' ', '_')}{TimeTicker}";
    }
}