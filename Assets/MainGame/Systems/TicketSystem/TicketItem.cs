using System;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewTicketItemData", menuName = "Item System/Ticket Item Data")]
public class TicketItemData : ScriptableObject
{
    public string TicketName;
    [TextArea(2, 5)]
    public string description;
    public ToyData toyDataTemplate;
}

[Serializable]
public class TicketItem 
{
    public TicketItemData ticketData;
    public bool isCompleted = false;

    public Toy currentToyState = new Toy();

    public void InitializeCurrentState()
    {
        if (ticketData != null && ticketData.toyDataTemplate != null)
        {
            currentToyState.InitializeCurrentState(ticketData.toyDataTemplate);
        }
    }
}