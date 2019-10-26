using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Character Object", menuName = "Inventory System/Items/Character")]
public class CharacterObject : ItemObject
{
    public int lifeTimeValue;
    private void Awake()
    {
        type = ItemType.Character;
        timeToFinishWork = 5;
    }
}
