using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public delegate void HandleItemClick(ItemObject item);
    public static event HandleItemClick OnItemClicked;

    public ItemObject item;

    public void OnMouseDown()
    {
        OnItemClicked?.Invoke(item);
    }
}
