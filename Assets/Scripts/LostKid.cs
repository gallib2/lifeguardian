using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostKid : MonoBehaviour
{
    public static event Action OnLostKidFound;

    private void OnMouseDown()
    {
        OnLostKidFound?.Invoke();
    }
}
