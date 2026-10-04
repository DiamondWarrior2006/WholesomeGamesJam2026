using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SupplyPartsSystem : MonoBehaviour
{
    [SerializeField]
    private List<Part> supplyData = new List<Part>();

    public UnityEvent<Part> OnOrderFailed;
    public UnityEvent<List<Part>> OnOrdersUpdated;
    public UnityEvent<Part> OnItemBought;

    public UnityEvent<Part> OnBuyItem;


    // add new order on run time
    public void AddOrder(Part item)
    {
        supplyData.Add(item);
        OnOrdersUpdated?.Invoke(supplyData);
    }

    // remove order on run time
    public void RemoveOrder(Part item)
    {
        if (!supplyData.Contains(item))
        {
            OnOrderFailed?.Invoke(item);
            return;
        }

        supplyData.Remove(item);
        OnOrdersUpdated?.Invoke(supplyData);

    }

    public void orderItem(int currentAmountGold, Part item)
    {
        if (currentAmountGold < item.item.price)
        {
            OnOrderFailed?.Invoke(item);
            return;
        }

        OnItemBought?.Invoke(item);
    }

}