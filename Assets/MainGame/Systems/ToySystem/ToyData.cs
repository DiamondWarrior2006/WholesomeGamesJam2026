using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewToyData", menuName = "Item System/Toy Data")]
public class ToyData
{
    public const float CleanlinessThreshold = 0.9f;
    public const float ReleaseThreshold = 0.1f;

    [SerializeField] private List<Part> toyItems = new List<Part>();
    public List<Part> GetToyParts()
    {
        return toyItems;
    }
}


[Serializable]
public class Toy
{
    public ToyData toyTemplateData;
    public List<Part> currentParts = new List<Part>(); 

    public void InitializeCurrentState(ToyData newToyTemplateData)
    {
        toyTemplateData = newToyTemplateData;
        currentParts.Clear();
        foreach (var part in toyTemplateData.GetToyParts())
        {
            Part newPart = new Part
            {
                item = part.item,
                cleanliness = part.cleanliness,
                connected = part.connected
            };
            currentParts.Add(newPart);
        }
    }

    public void CreateRandomizedCurrentState(ToyData newToyTemplateData)
    {
        toyTemplateData = newToyTemplateData;
        currentParts.Clear();
        foreach (var part in toyTemplateData.GetToyParts())
        {
            Part newPart = new Part
            {
                item = part.item,
                cleanliness = UnityEngine.Random.Range(0f, 1f),
                connected = UnityEngine.Random.Range(0f, 1f)
            };
            currentParts.Add(newPart);
        }
    }

    public void AddToyPart(Part part)
    {
        currentParts.Add(part);
    }

    public void RemoveToyPart(Part part)
    {
        if (currentParts.Contains(part) && part.connected < ToyData.ReleaseThreshold)
        {
            currentParts.Remove(part);
        }
    }

    public bool IsClean()
    {
        foreach (var part in currentParts)
        {
            if (part.cleanliness <= ToyData.CleanlinessThreshold)
            {
                return false;
            }
        }
        return true;
    }
}