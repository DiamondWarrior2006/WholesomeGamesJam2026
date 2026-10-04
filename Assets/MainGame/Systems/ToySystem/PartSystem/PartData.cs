using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPartData", menuName = "Item System/Base Part Data")]
public class PartData : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int defaultQuantity = 1;
    public int price;
}

[Serializable]
public class Part
{
    public PartData item;
    [Range(0f, 1f)]
    public float cleanliness = 1f;
    [Range(0f, 1f)]
    public float connected = 0f;
}