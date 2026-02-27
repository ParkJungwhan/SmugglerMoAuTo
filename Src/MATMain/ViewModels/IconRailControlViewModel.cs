using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MATMain.ViewModels;

internal partial class IconRailControlViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<IconItem> items;

    [ObservableProperty]
    private IconItem? selected;

    [ObservableProperty]
    private RelayCommand<IconItem> itemClickCommand;

    [ObservableProperty]
    private RelayCommand addCommand;

    public IconRailControlViewModel()
    {
        ItemClickCommand = new RelayCommand<IconItem>(item =>
        {
            Selected = item;            // 선택 변경 (하이라이트)
            // 여기서 실행 로직 호출
            // e.g. Launch(item)
        });

        AddCommand = new RelayCommand(() =>
        {
            // + 눌렀을 때 실행
            // e.g. 아이템 추가 팝업 / 파일 선택 / 등록 UI
        });
    }
}

public class IconItem
{
    public ImageSource? Icon { get; set; }
    public string? Id { get; set; } // 선택/삭제 등에 쓰고 싶으면
}