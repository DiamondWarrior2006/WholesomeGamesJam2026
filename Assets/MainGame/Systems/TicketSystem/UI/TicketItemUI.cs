using System;
using TMPro;
using UnityEngine;

public class TicketItemUI : ItemDataUI
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    private Action<ticketItem> acceptTicket;
    private Action<ticketItem> complainTicket;
    public void init(ticketItem item, Action<ticketItem> acceptTicket, Action<ticketItem> complainTicket)
    {
        base.init(item);
        this.item = item;
        descriptionText.text = item.ticketData.description;
        this.acceptTicket = acceptTicket;
        this.complainTicket = complainTicket;
    }

    public void OnAccept()
    {
        acceptTicket?.Invoke(item as ticketItem);
    }

    public void ComplainTicket()
    {
        complainTicket?.Invoke(item as ticketItem);
    }

}