using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PartDataUI : MonoBehaviour
{
    [SerializeField] protected Part item;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private Image itemSpriteImage;

    public void init(Part item)
    {
        this.item = item;
        itemNameText.text = item.item.itemName;
        itemQuantityText.text = item.item.defaultQuantity.ToString();
        itemPriceText.text = item.item.price.ToString();
        itemSpriteImage.sprite = item.item.sprite;
    }
}