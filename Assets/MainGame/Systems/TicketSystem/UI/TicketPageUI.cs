using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TicketPageUI : MonoBehaviour
{
    [SerializeField] private TicketItemUI ticketItemPrefab;
    [SerializeField] private Transform ticketItemContainer; 


    public UnityEvent<TicketItem> OnTicketItemComplete;

    public UnityEvent<TicketItem> OnError;

    public void CreateTicketItemUI(TicketItem item, Action<TicketItem> onClickAction)
    {
        TicketItemUI ticketItemUI = Instantiate(ticketItemPrefab, ticketItemContainer);
        ticketItemUI.init(item, onClickAction);
    }

    public void CompleteTicketItemUI(TicketItem item)
    {
        if (item.isComplete())
        {
            OnTicketItemComplete?.Invoke(item);
            //give coins and remove the ticket from the list
        }
        OnError?.Invoke(item); // need to add what the error or why its not end
    }

    public void ClearTicketItemUI()
    {
        foreach (Transform child in ticketItemContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void UpdatesTickets(List<TicketItem> tickets, Action<TicketItem> onClickAction)
    {
        ClearTicketItemUI();
        foreach (var ticket in tickets)
        {
            CreateTicketItemUI(ticket, onClickAction);
        }
    }

}