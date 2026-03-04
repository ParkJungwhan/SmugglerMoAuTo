using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MATMain.Models;

namespace MATMain.ViewModels;

internal partial class MainContentModel : ObservableObject, IRecipient<ChangeItemMessage>
{
    [ObservableProperty]
    private string contentTitle;

    [ObservableProperty]
    private RelayCommand cmdPlayMacro;

    [ObservableProperty]
    private RelayCommand cmdAddTab;

    [ObservableProperty]
    private ObservableCollection<SubTabItem> subTabs;

    [ObservableProperty]
    private int currentIndex;

    [ObservableProperty]
    private RelayCommand cmdProjectOption;

    [ObservableProperty]
    private RelayCommand cmdChangeTitle;

    [ObservableProperty]
    private bool isOptionOpen;

    public MainContentModel()
    {
        IsOptionOpen = false;
        ContentTitle = "-";
        cmdPlayMacro = new(PlayMacroCmd);
        cmdAddTab = new(AddTabCmd);
        cmdProjectOption = new(ProjectOptionCmd);
        cmdChangeTitle = new(ChangeTitleCmd);

        subTabs = new ObservableCollection<SubTabItem>();

        WeakReferenceMessenger.Default.Register<ChangeItemMessage>(this);
    }

    private void ChangeTitleCmd()
    {
        // 여기서 상위 뷰모델의 데이터를 변경해야함.
    }

    private void ProjectOptionCmd()
    {
        //option창을 띄워서
        IsOptionOpen = true;
    }

    private void AddTabCmd()
    {
        SubTabs.Add(new SubTabItem() { TabHeaderName = $"Header{SubTabs.Count() + 1}" });
        CurrentIndex = SubTabs.Count();
    }

    private void PlayMacroCmd()
    {
        // 저장된 매크로를 실행한다
    }

    public void Receive(ChangeItemMessage message)
    {
        // IsCodeOpened = true;
        // 메시지를 받으면 데이터를 뒤져서 Sub Tab 들을 다시 구성해서 화면을 보여준다
        // 그동안 progress를 보여주고 그동안 데이터를 구성한다.

        var subInfo = message.Value;
        ContentTitle = subInfo.Name;

        // 첫번째 화면의 탭을 보여준다

        SubTabs.Clear();
        // sample
        for (int i = 0; i < Random.Shared.Next(2, 5); i++)
        {
            SubTabs.Add(new SubTabItem() { TabHeaderName = $"Header{i + 1}" });
        }
        CurrentIndex = SubTabs.Count();
    }
}

public class SubTabItem
{
    public string TabHeaderName { get; set; }
}