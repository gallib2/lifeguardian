using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washer : MonoBehaviour
{
    public delegate void HandleItemClick(ItemObject item);
    public static event HandleItemClick OnWasherItemClicked;

    public ItemObject item;
    public HealthBar healthBar;

    public void OnMouseDown()
    {
        OnWasherItemClicked?.Invoke(item);
        //GetComponentInParent<KidPackController>()?.MoveToSafty();
        //GetComponent<CharacterMovement>().MoveToSafty(item);
        //GetComponentInChildren<HealthBar>().OnStopDownloadHealth();
        //healthBar?.OnStopDownloadHealth();
    }
}
