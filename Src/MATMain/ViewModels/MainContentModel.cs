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

    public MainContentModel()
    {
        ContentTitle = "-";
        cmdPlayMacro = new(PlayMacroCmd);

        WeakReferenceMessenger.Default.Register<ChangeItemMessage>(this);
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
    }
}