using System;
using TMPro;
using UnityEngine;

public class TicketItemUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI NameText;
    private Action<TicketItem> action;

    private TicketItem ticket;
    public void init(TicketItem item, Action<TicketItem> action)
    {
        ticket = item;
        descriptionText.text = item.ticketData.description;
        NameText.text = item.ticketData.TicketName;
        this.action = action;
    }

    public void OnClick()
    {
        action?.Invoke(ticket);
    }

}