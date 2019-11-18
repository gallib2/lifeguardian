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
    public HealthBar healthBar;

    public void OnMouseDown()
    {
        OnItemClicked?.Invoke(item);
        GetComponent<CharacterMovement>().MoveToSafty(item);
        //GetComponentInChildren<HealthBar>().OnStopDownloadHealth();
        healthBar.OnStopDownloadHealth();
    }

    // todo detect if pass allowed zone OR in random time move forward to danger position
}
