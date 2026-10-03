using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TicketSystem : MonoBehaviour
{
    [SerializeField] private List<TicketItem> currentTickets = new List<TicketItem>();
    public UnityEvent<TicketItem> OnTicketAccepted;
    public UnityEvent<TicketItem> OnTicketsUpdate;
    public void AddTicketItem(TicketItem item)
    {
        currentTickets.Add(item);
        OnTicketsUpdate?.Invoke(item);
    }

    public void RemoveTicketItem(TicketItem item)
    {
        if (currentTickets.Contains(item))
        {
            currentTickets.Remove(item);
            OnTicketsUpdate?.Invoke(item);
        }
    }

    public void AcceptTicket(TicketItem item)
    {
        OnTicketAccepted?.Invoke(item);
        RemoveTicketItem(item);
    }

}