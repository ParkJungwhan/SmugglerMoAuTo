using System.Diagnostics;
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
    }

    private void SendMessage(string msg)
    {
        WeakReferenceMessenger.Default.Send(new BroadcastMessage(msg));
    }
}