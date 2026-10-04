using System;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewTicketItemData", menuName = "Item System/Ticket Item Data")]
public class TicketItemData : ScriptableObject
{
    public string TicketName;
    [TextArea(2, 5)]
    public string description;

    public int coinsReward;
    
    public ToyData toyDataTemplate;
}

[Serializable]
public class TicketItem 
{
    public TicketItemData ticketData;

    public Toy currentToyState = new Toy();

    public void InitializeCurrentState()
    {
        if (ticketData != null && ticketData.toyDataTemplate != null)
        {
            currentToyState.InitializeCurrentState(ticketData.toyDataTemplate);
        }
    }


    public bool isComplete()
    {
        if (currentToyState == null || currentToyState.toyTemplateData == null)
        {
            Debug.LogWarning("Current toy state or toy template data is null.");
            return false;
        }

        foreach (var part in currentToyState.currentParts)
        {
            if (part.cleanliness < ToyData.CleanlinessThreshold || part.connected < ToyData.ReleaseThreshold)
            {
                Debug.Log($"Part {part.item.itemName} is not complete. Cleanliness: {part.cleanliness}, Connected: {part.connected}");
                return false;
            }
        }

        // need a fix bcuse its not a an arry an item can be in diffrent index or place  
        for (int i = 0; i < currentToyState.currentParts.Count; i++)
        {
            var part = currentToyState.currentParts[i];
            var templatePart = currentToyState.toyTemplateData.GetToyParts()[i];

            if (part.item != templatePart.item)
            {
                Debug.Log($"Part {part.item.itemName} does not match the template part {templatePart.item.itemName}.");
                return false;
            }
        }

        return true;
    }


}