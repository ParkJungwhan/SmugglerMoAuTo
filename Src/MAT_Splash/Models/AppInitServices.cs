using System.Diagnostics;
using System.IO;
using CommunityToolkit.Mvvm.Messaging;

namespace MAT_Splash.Models;

public class AppInitServices : IRecipient<ProcessBroadcastMessage>
{
    public AppInitServices()
    {
        WeakReferenceMessenger.Default.Register<ProcessBroadcastMessage>(this);
    }

    public void Receive(ProcessBroadcastMessage message)
    {
        if (message.Value == ProcessState.Initializing)
        {
            Task.Factory.StartNew(() =>
            {
                Debug.WriteLine($"{DateTime.Now}\tStart Initial...");

                GetDataFromService();
            });
        }
    }

    public void GetDataFromService()
    {
        SendMessage("Initializing...");

        Thread.Sleep(2000);
        SendMessage("result1");

        Thread.Sleep(1000);
        SendMessage("result2");

        Thread.Sleep(1000);
        SendMessage("result3");

        Thread.Sleep(2000);

        SendMessage("complete!!!");

        Debug.WriteLine($"{DateTime.Now}\tFinish Initial");

        WeakReferenceMessenger.Default.Send(new ProcessRunMessage("MATMain.exe"));
    }

    private void SendMessage(string msg)
    {
        WeakReferenceMessenger.Default.Send(new BroadcastMessage(msg));
    }

    //public void LaunchMain(string mainExeRelativePath, string[] args = null)
    //{
    //    // splash.exe 기준이 아니라 "설치/배포 루트" 기준으로 잡는 게 안전함
    //    // 보통 splash와 main이 같은 폴더면 AppContext.BaseDirectory가 가장 편함
    //    var baseDir = AppContext.BaseDirectory;
    //    var mainExePath = Path.GetFullPath(Path.Combine(baseDir, mainExeRelativePath));

    //    if (!File.Exists(mainExePath))
    //        throw new FileNotFoundException("Main executable not found", mainExePath);

    //    var psi = new ProcessStartInfo
    //    {
    //        FileName = mainExePath,
    //        WorkingDirectory = Path.GetDirectoryName(mainExePath)!,
    //        UseShellExecute = true, // 보통 WPF 앱 실행은 이게 더 무난
    //        Arguments = args is { Length: > 0 } ? string.Join(" ", args.Select(QuoteIfNeeded)) : ""
    //    };

    //    Process.Start(psi);
    //}

    //private string QuoteIfNeeded(string s)
    //{
    //    if (string.IsNullOrWhiteSpace(s)) return "\"\"";
    //    return s.Contains(' ') ? $"\"{s.Replace("\"", "\\\"")}\"" : s;
    //}
}