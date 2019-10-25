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

    [TextArea(15, 20)]
    public string description;
}
