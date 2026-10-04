using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewToyData", menuName = "Item System/Toy Data")]
public class ToyData: ScriptableObject
{
    public const float CleanlinessThreshold = 0.9f;
    public const float ConnectedThreshold = 0.9f;
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

    public Part GetPart(PartData data, int occurrence = 0)
    {
        int count = 0;
        foreach (var part in currentParts)
        {
            if (part.item != data) continue;
            if (count == occurrence) return part;
            count++;
        }
        return null;
    }

    public bool IsPartComplete(PartData data)
    {
        bool found = false;
        foreach (var part in currentParts)
        {
            if (part.item != data) continue;
            found = true;
            if (!part.IsComplete) return false;
        }
        return found;
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
            if (!part.IsClean) return false;
        }
        return true;
    }


    public bool IsComplete()
    {
        if (toyTemplateData == null) return false;

        var templateParts = toyTemplateData.GetToyParts();
        if (currentParts.Count != templateParts.Count) return false;

        foreach (var part in currentParts)
        {
            if (!part.IsComplete) return false;
        }

        // count parts per type so order doesnt matter
        var needed = new Dictionary<PartData, int>();
        foreach (var part in templateParts)
        {
            needed.TryGetValue(part.item, out int n);
            needed[part.item] = n + 1;
        }

        foreach (var part in currentParts)
        {
            if (part.item == null || !needed.TryGetValue(part.item, out int n) || n == 0) return false;
            needed[part.item] = n - 1;
        }

        return true;
    }
}