using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Tools;
using MATMain.Models;

namespace MATMain.ViewModels;

internal partial class MainWindowModel : ObservableObject//, IRecipient<ChangeItemMessage>
{
    [ObservableProperty]
    private LeftMainContentModel leftModel;

    [ObservableProperty]
    private MainContentModel mainModel;

    [ObservableProperty]
    private string appTitle;

    [ObservableProperty]
    private OptionViewModel optionModel;

    [ObservableProperty]
    private bool isCodeOpened;

    public MainWindowModel()
    {
        leftModel = new LeftMainContentModel();
        mainModel = new MainContentModel();
        optionModel = new OptionViewModel();

        appTitle = "MAT";

#if DEBUG
        appTitle += " (DEBUG MODE)";
#endif

        GlobalShortcut.Init(new List<KeyBinding>
        {
            new(this.CmdSaveProject, Key.F5, ModifierKeys.None),
            new(this.CmdOption, Key.F12, ModifierKeys.None)
        });

        //WeakReferenceMessenger.Default.Register<ChangeItemMessage>(this);
    }

    private RelayCommand CmdSaveProject => new(() =>
    {
        // 프로젝트 저장 로직
        // ui 변경 msg 호출

        Debug.WriteLine($"{DateTime.Now}\t Call Save Projects ");
    });

    private RelayCommand CmdOption => new(() =>
    {
        IsCodeOpened = true;
        //WeakReferenceMessenger.Default.Send<ChangeItemMessage>(new ChangeItemMessage(null));
        // 옵션 화면 출력
        // ui 변경 msg 호출

        Debug.WriteLine($"{DateTime.Now}\t Show Option View");
    });

    //public void Receive(ChangeItemMessage message)
    //{
    //    IsCodeOpened = true;
    //    // 메시지를 받으면 데이터를 뒤져서 Sub Tab 들을 다시 구성해서 화면을 보여준다
    //    // 그동안 progress를 보여주고 그동안 데이터를 구성한다.
    //}
}