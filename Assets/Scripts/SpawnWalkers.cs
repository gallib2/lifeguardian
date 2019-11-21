using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWalkers : MonoBehaviour
{
    public GameObject prefub_walker;
    public GameObject currentWalker;
    public int timeBetweenSpawn;
    public int MAX_WalkersOnAction;

    private TimerHelper timer;
    private int numOfWalkersOnAction;

    private void OnEnable()
    {
        BeachWalker.OnBeachWalkerOut += BeachWalkerOut;
    }

    private void OnDisable()
    {
        BeachWalker.OnBeachWalkerOut -= BeachWalkerOut;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWalker = Instantiate(prefub_walker, transform);
        timer = new TimerHelper();
        MAX_WalkersOnAction = 1;
        numOfWalkersOnAction++;
        timeBetweenSpawn = 10;
    }

    // Update is called once per frame
    void Update()
    {
        bool toCreateWalker = (int)timer.Get() > 0 && (int)timer.Get() % timeBetweenSpawn == 0;
        bool isMaxWalkersOnAction = numOfWalkersOnAction < MAX_WalkersOnAction;

        if (isMaxWalkersOnAction && toCreateWalker)
        {
            numOfWalkersOnAction++;
            currentWalker = Instantiate(prefub_walker, transform);
        }
    }

    private void BeachWalkerOut()
    {
        numOfWalkersOnAction--;
        Destroy(currentWalker);
        timer.Reset();
    }
}
