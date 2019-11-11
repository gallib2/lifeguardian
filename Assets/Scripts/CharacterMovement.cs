using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static event Action OnMoveToDanger;

    public List<AudioClip> audioOnSafePlace;
    public List<AudioClip> audioOnDangerPlace;
    public Transform groundDetection;
    public float speed;
    public float distance;
    public int moveToDangerPerXSeconds;

    private AudioSource audioSource;
    private CharacterObject kidItem;
    private TimerHelper timer;
    private bool movingRight = true;
    private bool moveToDanger = false;
    private bool continueCheckTime = true;
    private Vector3 target;
    private Vector3 initPosition;
    private bool isInDangerZone = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        kidItem = GetComponent<Item>().item as CharacterObject;
        target = new Vector3(2f, 0, 0); //kidItem.dangerZone.transform.position;
        timer = new TimerHelper();
        initPosition = transform.position;
        moveToDangerPerXSeconds = UnityEngine.Random.Range(5, 15);
        Debug.Log("moveToDangerPerXSeconds: " + moveToDangerPerXSeconds);
    }

    private void Update()
    {
        moveToDanger = continueCheckTime && (int)timer.Get() > 0 && ((int)timer.Get() % moveToDangerPerXSeconds) == 0;

        if(!continueCheckTime || moveToDanger)
        {
            float step = speed * Time.deltaTime;
            continueCheckTime = false;
            transform.position = Vector2.MoveTowards(transform.position, target, step); 
        } 
        else
        {
            Patrol();
        }
    }

    public void MoveToSafty(ItemObject item)
    {
        // TODO the time that takes to return to safty is the time of the ui OR remove the ui for now
        if (isInDangerZone)
        {
            int clipsSize = audioOnDangerPlace.Count;
            int soundToPlay = UnityEngine.Random.Range(0, clipsSize);

            audioSource.PlayOneShot(audioOnDangerPlace[soundToPlay]);
            continueCheckTime = true;
            transform.position = initPosition;
        }
        else
        {
            int clipsSize = audioOnSafePlace.Count;
            int soundToPlay = UnityEngine.Random.Range(0, clipsSize);

            audioSource.PlayOneShot(audioOnSafePlace[soundToPlay]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "flagsZone_right")
        {
            isInDangerZone = true;
            OnMoveToDanger?.Invoke();
            GetComponentInChildren<HealthBar>().OnStartDownloadHealth();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "flagsZone_right")
        {
            isInDangerZone = false;
        }
    }

    private void Patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    public void OnCharcterDead()
    {
        Destroy(gameObject);
    }

}
