using System.Collections.ObjectModel;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MainAppSplash.Views;
using MAT_Splash.Models;

namespace MainAppSplash.ViewModels;

public partial class MainViewModel : ObservableObject, IRecipient<BroadcastMessage>
{
    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private string versionText;

    [ObservableProperty]
    public ObservableCollection<string> logs;

    private Dispatcher ui;

    public MainViewModel()
    {
        Title = "MAT";
        versionText = "ver. 0.0.000";
        logs = new ObservableCollection<string>();

        WeakReferenceMessenger.Default.Register<BroadcastMessage>(this);
    }

    public void AddLog(string text)
    {
        if (ui.CheckAccess())
        {
            Logs.Add(text);
            // 너무 길어지면 자르기(선택)
            if (Logs.Count > 200) Logs.RemoveAt(0);
        }
        else
        {
            ui.BeginInvoke(() =>
            {
                Logs.Add(text);
                if (Logs.Count > 200) Logs.RemoveAt(0);
            }, DispatcherPriority.Background);
        }
    }

    public void Receive(BroadcastMessage message) => AddLog(message.Value);

    internal void SetDispatcher(MainWindow mainWindow) => this.ui = mainWindow.Dispatcher;
}