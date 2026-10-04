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

    public bool IsClean => cleanliness >= ToyData.CleanlinessThreshold;
    public bool IsConnected => connected >= ToyData.ConnectedThreshold;
    public bool IsComplete => IsClean && IsConnected;
    public bool CanRelease => connected < ToyData.ReleaseThreshold;

    public Part Clone()
    {
        return new Part { item = item, cleanliness = cleanliness, connected = connected };
    }
}



public interface IPart
{
    PartData Data { get; }
    void Initialize(Part part);
}