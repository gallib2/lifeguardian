using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //public static event Action OnLifeOver;

    public float initialHealth;
    public Slider lifeSlider;
    public Transform target;
    public float offsetYSliderPosition = 0.5f;
    //public GameObject wholeObjectToDestroy;

    private bool toStartDwonloadHelth;
    private bool toStopDwonloadHelth;
    private TimerHelper timer;

    public float InitialHealth
    {
        get
        {
            return initialHealth;
        }
        set
        {
            initialHealth = value; 
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        timer = new TimerHelper();
        lifeSlider = GetComponent<Slider>();

        Debug.Log("InitialHealth: " + InitialHealth);
        Debug.Log("initialHealth: " + initialHealth);

        lifeSlider.maxValue = InitialHealth;
    }

    void Update()
    {
        lifeSlider.transform.position = new Vector3(target.position.x, target.position.y + offsetYSliderPosition, lifeSlider.transform.position.z);
        //lifeSlider.transform.position = new Vector3(target.position.x, lifeSlider.transform.position.y, lifeSlider.transform.position.z);

        if(toStartDwonloadHelth && (int)timer.Get() > 0)
        {
            lifeSlider.value = InitialHealth - (int)timer.Get();
            if(lifeSlider.value <= 0)
            {
                //OnLifeOver?.Invoke();
                //Destroy(wholeObjectToDestroy);
                //character.OnCharcterDead();
                //Destroy(gameObject);
            }
        }
        else if(toStopDwonloadHelth)
        {
            lifeSlider.value = InitialHealth;
            //StopDownloadHelth();
        }
    }

    public void OnStartDownloadHealth()
    {
        toStartDwonloadHelth = true;
        toStopDwonloadHelth = false;
        timer.Reset();
    }

    public void OnStopDownloadHealth()
    {
        toStartDwonloadHelth = false;
        toStopDwonloadHelth = true;
    }

    private void StartDwonloadHelth()
    {
        //localScale.x -= healthDownloadPerSecond;
        //transform.localScale = localScale;
    }

    private void StopDownloadHelth()
    {

    }
}
