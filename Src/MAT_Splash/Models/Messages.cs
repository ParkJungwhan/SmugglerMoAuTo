using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MAT_Splash.Models;

public enum ProcessState
{
    Initializing,
    Running,
    Completed,
    Failed
}

public class BroadcastMessage : ValueChangedMessage<string>
{
    public BroadcastMessage(string msg) : base(msg)
    {
    }
}

public class ProcessBroadcastMessage : ValueChangedMessage<ProcessState>
{
    public ProcessBroadcastMessage(ProcessState ps) : base(ps)
    {
    }
}

public class ChangeBoolMessage : ValueChangedMessage<bool>
{
    public ChangeBoolMessage(bool ps) : base(ps)
    {
    }
}

public class ProcessRunMessage : ValueChangedMessage<string>
{
    public ProcessRunMessage(string ps) : base(ps)
    {
    }
}