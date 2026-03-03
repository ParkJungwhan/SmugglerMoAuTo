using CommunityToolkit.Mvvm.Messaging.Messages;
using MATMain.ViewModels;

namespace MATMain.Models;

public class ChangeItemMessage : ValueChangedMessage<SubItemInfo>
{
    public ChangeItemMessage(SubItemInfo item) : base(item)
    {
    }
}