using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBull : MonoBehaviour
{
    private TimerHelper timer;
    private bool continueCheckTime;
    private int lastPlayedIndex;
    private AudioSource audioSource;
    public List<AudioClip> audioClips;
    public int timeBetweenSounds;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = new TimerHelper();
    }

    // Update is called once per frame
    void Update()
    {
        bool needToPlaySound = continueCheckTime && timer.Get() > 1 && ((int)timer.Get() % timeBetweenSounds == 0);

        if(needToPlaySound)
        {
            continueCheckTime = false;
            Speak();
        }

        if(!audioSource.isPlaying)
        {
            continueCheckTime = true;
        }
    }

    private void Speak()
    {
        Debug.Log("needToPlaySound");
        int index = UnityEngine.Random.Range(0, audioClips.Count);
        if(index == lastPlayedIndex)
        {
            index = (index + 1) % audioClips.Count;
        }
        audioSource.PlayOneShot(audioClips[index]);
        lastPlayedIndex = index;
    }



}
