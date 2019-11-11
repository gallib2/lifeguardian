using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Character,
    Flag
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefub;
    public ItemType type;
    public int timeToFinishWork;
    public GameObject allowedZone;
    public string itemName;

    [TextArea(15, 20)]
    public string description;
}
