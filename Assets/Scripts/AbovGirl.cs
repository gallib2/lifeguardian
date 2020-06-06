using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbovGirl : MonoBehaviour
{
    public AudioClip AudioClip;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(AudioClip);
    }
}
