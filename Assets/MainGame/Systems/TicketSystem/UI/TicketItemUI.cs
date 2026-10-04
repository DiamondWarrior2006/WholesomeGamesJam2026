using System;
using TMPro;
using UnityEngine;

public class TicketItemUI : PartDataUI
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    private Action<TicketItem> action;
    public void init(TicketItem item, Action<TicketItem> action)
    {
        base.init(item);
        descriptionText.text = item.ticketData.description;
        this.action = action;
    }

    public void OnClick()
    {
        action?.Invoke(item as TicketItem);
    }

}