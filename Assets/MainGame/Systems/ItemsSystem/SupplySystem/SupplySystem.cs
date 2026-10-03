using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SupplySystem : MonoBehaviour
{
    [SerializeField]
    private List<Item> supplyData = new List<Item>();

    public UnityEvent<Item> OnOrderFailed;
    public UnityEvent<List<Item>> OnOrdersUpdated;
    public UnityEvent<Item> OnItemBought;

    public UnityEvent<Item> OnBuyItem;


    // add new order on run time
    public void AddOrder(Item item)
    {
        supplyData.Add(item);
        OnOrdersUpdated?.Invoke(supplyData);
    }

    // remove order on run time
    public void RemoveOrder(Item item)
    {
        if (!supplyData.Contains(item))
        {
            OnOrderFailed?.Invoke(item);
            return;
        }

        supplyData.Remove(item);
        OnOrdersUpdated?.Invoke(supplyData);

    }

    public void orderItem(int currentAmountGold, Item item)
    {
        if (currentAmountGold < item.item.price)
        {
            OnOrderFailed?.Invoke(item);
            return;
        }

        OnItemBought?.Invoke(item);
    }

}