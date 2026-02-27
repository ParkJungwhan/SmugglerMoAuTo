using CommunityToolkit.Mvvm.ComponentModel;

namespace MATMain.ViewModels;

internal partial class MainWindowModel : ObservableObject
{
    //[ObservableProperty]
    //private IconRailControlViewModel subModel;

    [ObservableProperty]
    private LeftMainContentModel leftModel;

    public MainWindowModel()
    {
        //subModel = new IconRailControlViewModel();
        leftModel = new LeftMainContentModel();
    }
}