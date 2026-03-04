using System.Diagnostics;
using System.IO;

namespace MAT_Splash.Services;

public static class MainLauncher
{
    public static void StartMainAndExit(string mainExeName, string[] args = null)
    {
        var dir = AppContext.BaseDirectory;                 // splash.exe가 있는 폴더
        var mainPath = Path.Combine(dir, mainExeName);      // 같은 폴더의 main.exe

        if (!File.Exists(mainPath))
            throw new FileNotFoundException("Main exe not found", mainPath);

        var psi = new ProcessStartInfo
        {
            FileName = mainPath,
            WorkingDirectory = dir,                          // 중요: 상대경로 리소스/설정 안정화
            UseShellExecute = true,
            Arguments = args is { Length: > 0 } ? string.Join(" ", args.Select(Quote)) : ""
        };

        Process.Start(psi);

        // 스플래시 종료 (WPF)
        System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            System.Windows.Application.Current.Shutdown();
        }));
        // 또는 Environment.Exit(0);
    }

    private static string Quote(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return "\"\"";
        return s.Contains(' ') ? $"\"{s.Replace("\"", "\\\"")}\"" : s;
    }
}