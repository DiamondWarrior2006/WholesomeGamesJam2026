using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrentTicket : MonoBehaviour
{
    [SerializeField] private List<ticketItem> currentTicket = new List<ticketItem>();

    public UnityEvent<int> OnTicketCompletedEvent;

    public UnityEvent<List<ticketItem>> OnTicketUpdatedEvent;

    public void AddTicketItem(ticketItem item)
    {
        currentTicket.Add(item);
        OnTicketUpdatedEvent.Invoke(currentTicket);
    }

    public void RemoveTicketItem(ticketItem item)
    {
        if (currentTicket.Contains(item))
        {
            currentTicket.Remove(item);
            OnTicketUpdatedEvent.Invoke(currentTicket);
        }
    }


    public void CompleteTicket(ticketItem item)
    {

        OnTicketCompletedEvent?.Invoke(item.item.price);
        RemoveTicketItem(item);

    }

}