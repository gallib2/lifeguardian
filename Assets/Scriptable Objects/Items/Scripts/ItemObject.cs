using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Character,
    Flag,
    Washer
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefub;
    public ItemType type;
    public int timeToFinishWork;
    public GameObject allowedZone;
    public string itemName;
    public SpriteRenderer spriteRenderer;

    [TextArea(15, 20)]
    public string description;
}
