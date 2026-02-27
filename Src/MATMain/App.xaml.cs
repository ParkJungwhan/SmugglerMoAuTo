using System.Diagnostics;
using System.Windows;
using MATMain.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MATMain;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                                    optional: true,
                                    reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                //services.AddSingleton<SubItems>();
                //services.AddSingleton<SubItemModel>();

                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowModel>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();

        //var subitems = AppHost.Services.GetRequiredService<SubItems>();
        //var viewmodel = AppHost.Services.GetRequiredService<SubItemModel>();
        //subitems.DataContext = viewmodel;

        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        mainWindow.DataContext = AppHost.Services.GetRequiredService<MainWindowModel>();
        //        mainWindow.SetSubControl(subitems);
        try
        {
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            Debug.Assert(false, ex.Message);
        }

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        AppHost.Dispose();

        base.OnExit(e);
    }
}