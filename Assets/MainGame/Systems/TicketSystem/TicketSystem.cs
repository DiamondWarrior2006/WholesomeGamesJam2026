using System.Collections.Generic;
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

}