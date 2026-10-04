using UnityEngine;
using UnityEngine.Events;

public class TicketSystem : TicketList
{
    public UnityEvent<TicketItem> OnTicketAccepted;
    public UnityEvent<TicketItem> OnTicketCompleted;

    public void AcceptTicket(TicketItem item)
    {
        OnTicketAccepted?.Invoke(item);
        RemoveTicketItem(item);
    }

    public bool TryCompleteTicket(TicketItem item)
    {
        if (item == null || !item.IsComplete()) return false;

        OnTicketCompleted?.Invoke(item);
        return true;
    }
}
