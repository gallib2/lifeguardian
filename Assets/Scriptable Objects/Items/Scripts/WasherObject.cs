using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Washer Object", menuName = "Inventory System/Items/Washer")]
public class WasherObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Washer;
        timeToFinishWork = 10;
    }
}
