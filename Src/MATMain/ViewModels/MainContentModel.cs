using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MATMain.ViewModels;

internal partial class MainContentModel : ObservableObject
{
    [ObservableProperty]
    private string addTitle;

    [ObservableProperty]
    private string contentTitle;

    [ObservableProperty]
    private RelayCommand cmdPlayMacro;

    public MainContentModel()
    {
        AddTitle = "add title(test)";
        ContentTitle = "TitleContente";
        cmdPlayMacro = new(CmdPlaymacro);
    }

    private void CmdPlaymacro()
    {
    }
}