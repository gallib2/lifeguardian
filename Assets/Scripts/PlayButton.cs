using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private AudioSource audioSource;
    public List<AudioClip> audioClips;
    int lastPlayedIndex;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPlayButtonClick()
    {
        //Speak();
        SceneManager.LoadScene((int)SceneEnumConfig.Game);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    private void Speak()
    {
        int index = UnityEngine.Random.Range(0, audioClips.Count);
        if (index == lastPlayedIndex)
        {
            index = (index + 1) % audioClips.Count;
        }
        audioSource.PlayOneShot(audioClips[index]);
        lastPlayedIndex = index;
    }
}
