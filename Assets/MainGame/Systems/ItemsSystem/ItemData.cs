using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Item System/Base Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int defaultQuantity = 1;
    public int price;
}

[Serializable]
public class Item
{
    public ItemData item;
}