using TMPro;
using UnityEngine;

public class TicketItemUI : ItemDataUI
{
    [SerializeField] private TicketSystem ticketSystem;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void init(ticketItem item)
    {
        base.init(item);
        this.item = item;
        descriptionText.text = item.ticketData.description;
    }

    public void OnAcceptButtonClicked()
    {
        ticketSystem.AcceptTicket(item as ticketItem);
    }

}