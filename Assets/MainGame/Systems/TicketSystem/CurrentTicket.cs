using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CurrentTicket : MonoBehaviour
{
    [SerializeField] private List<TicketItem> currentTickets = new List<TicketItem>();

    public UnityEvent<int> OnTicketCompleted;

    public UnityEvent<List<TicketItem>> OnTicketUpdated;

    public void AddTicketItem(TicketItem item)
    {
        currentTickets.Add(item);
        OnTicketUpdated.Invoke(currentTickets);
    }

    public void RemoveTicketItem(TicketItem item)
    {
        if (currentTickets.Contains(item))
        {
            currentTickets.Remove(item);
            OnTicketUpdated.Invoke(currentTickets);
        }
    }

    public void CompleteTicket(TicketItem item)
    {

        OnTicketCompleted?.Invoke(item.item.price);
        RemoveTicketItem(item);

    }

}