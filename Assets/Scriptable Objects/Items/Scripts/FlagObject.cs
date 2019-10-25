using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flag Object", menuName = "Inventory System/Items/Flag")]
public class FlagObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Flag;
    }
}
