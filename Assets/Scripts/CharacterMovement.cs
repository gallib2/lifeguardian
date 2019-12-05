using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static event Action OnMoveToDanger;

    public Transform groundDetection;
    public float speed = 0.5f;
    public float distance;

    private bool movingRight = true;

    //public Vector3 InitPosition { get; set; }
    //public bool ArrivedPatrolPosition { get; set; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "flagsZone_right")
        {
            GetComponentInParent<KidPackController>().IsInDangerZone = true;
            OnMoveToDanger?.Invoke();
            GetComponent<Item>().healthBar.OnStartDownloadHealth();
            //healthBar.OnStartDownloadHealth();
            //GetComponentInChildren<HealthBar>().OnStartDownloadHealth();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "flagsZone_right")
        {
            KidPackController kidPackController = GetComponentInParent<KidPackController>();
            if (kidPackController)
            {
                kidPackController.IsInDangerZone = false;
                kidPackController.MoveToDangerPerXSeconds = UnityEngine.Random.Range(5, 15);
            }
        }
    }

    public void Patrol()
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
        //Destroy(gameObject);
        DestroyImmediate(gameObject);
    }

}
