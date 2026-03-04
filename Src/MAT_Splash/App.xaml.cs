using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using MainAppSplash.ViewModels;
using MainAppSplash.Views;
using MAT_Splash.Models;
using MAT_Splash.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MainAppSplash;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application, IRecipient<ProcessRunMessage>
{
    public static IHost AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                //config.AddJsonFile("appsettings.json", optional: true);
                var env = context.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                                    optional: true,
                                    reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<AppInitServices>();
                //services.AddTransient<AppInitServices>();

                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .Build();

        WeakReferenceMessenger.Default.Register<ProcessRunMessage>(this);
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();

        var services = AppHost.Services.GetRequiredService<AppInitServices>();

        var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        var viewmodel = AppHost.Services.GetRequiredService<MainViewModel>();
        viewmodel.SetDispatcher(mainWindow);

        mainWindow.DataContext = AppHost.Services.GetRequiredService<MainViewModel>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        AppHost.Dispose();

        base.OnExit(e);
    }

    public void Receive(ProcessRunMessage message)
    {
        MainLauncher.StartMainAndExit(message.Value, new[] { "--launchedBySplash" });
    }
}