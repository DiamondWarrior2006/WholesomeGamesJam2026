using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TicketSystem : MonoBehaviour
{
    [SerializeField] private List<ticketItem> currentTicket = new List<ticketItem>();
    public UnityEvent<ticketItem> OnTicketAccepted;
    public UnityEvent<ticketItem> OnTicketsUpdate;
    public void AddTicketItem(ticketItem item)
    {
        currentTicket.Add(item);
        OnTicketsUpdate?.Invoke(item);
    }

    public void RemoveTicketItem(ticketItem item)
    {
        if (currentTicket.Contains(item))
        {
            currentTicket.Remove(item);
            OnTicketsUpdate?.Invoke(item);
        }
    }

    public void AcceptTicket(ticketItem item)
    {
        OnTicketAccepted?.Invoke(item);
        RemoveTicketItem(item);
    }

}