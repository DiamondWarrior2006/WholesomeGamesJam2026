using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SupplySystem : MonoBehaviour
{
    [SerializeField]
    private List<Item> supplyData = new List<Item>();

    public UnityEvent<Item> onOrder;

    public UnityEvent<Item> OnOrderFailedEvent;


    public UnityEvent<List<Item>> OnUpdateOrdersList;


    public UnityEvent<Item> OnBuyItem;


    // add new order on run time
    public void AddOrder(Item item)
    {
        supplyData.Add(item);
        OnUpdateOrdersList?.Invoke(supplyData);
    }

    // remove order on run time
    public void RemoveOrder(Item item)
    {
        if (!supplyData.Contains(item))
        {
            OnOrderFailedEvent?.Invoke(item);
            return;
        }

        supplyData.Remove(item);
        OnUpdateOrdersList?.Invoke(supplyData);

    }

    public void orderItem(int currentAmountGold, Item item)
    {
        if (currentAmountGold < item.item.price)
        {
            OnOrderFailedEvent?.Invoke(item);
            return;
        }

        OnBuyItem?.Invoke(item);
    }

}