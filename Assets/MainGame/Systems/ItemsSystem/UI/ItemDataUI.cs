using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemDataUI : MonoBehaviour
{
    [SerializeField] protected Item item;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private Image itemSpriteImage;

    public void init(Item item)
    {
        this.item = item;
        itemNameText.text = item.item.name;
        itemQuantityText.text = item.item.defaultQuantity.ToString();
        itemPriceText.text = item.item.price.ToString();
        itemSpriteImage.sprite = item.item.sprite;
    }
}