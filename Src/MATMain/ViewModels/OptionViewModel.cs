using CommunityToolkit.Mvvm.ComponentModel;

namespace MATMain.ViewModels;

internal partial class OptionViewModel : ObservableObject
{
    [ObservableProperty]
    private string googleTitle;

    public OptionViewModel()
    {
        googleTitle = "Google Games Play";
    }
}