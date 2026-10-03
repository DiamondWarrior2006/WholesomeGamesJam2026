using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTicketItemData", menuName = "Item System/Ticket Item Data")]
public class TicketItemData : ItemData
{
    [TextArea(2, 5)]
    public string description;
}

[Serializable]
public class ticketItem : Item
{
    public TicketItemData ticketData => item as TicketItemData;
}