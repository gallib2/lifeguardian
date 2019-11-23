using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWalkers : MonoBehaviour
{
    public CharacterType characterType;

    public GameObject prefub_walker;
    public GameObject currentWalker;
    public int timeBetweenSpawn = 10;
    public int MAX_WalkersOnAction = 1;

    private TimerHelper timer;
    private int numOfWalkersOnAction;

    private void OnEnable()
    {
        BeachWalker.OnBeachWalkerOut += BeachWalkerOut;
        //BeachWalker.OnLifeOver += LifeOver;
    }

    private void OnDisable()
    {
        BeachWalker.OnBeachWalkerOut -= BeachWalkerOut;
        //BeachWalker.OnLifeOver -= LifeOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWalker = Instantiate(prefub_walker, transform);
        timer = new TimerHelper();
        //MAX_WalkersOnAction = 1;
        numOfWalkersOnAction++;
        //timeBetweenSpawn = 10;
    }

    // Update is called once per frame
    void Update()
    {
        bool isMaxWalkersOnAction = numOfWalkersOnAction < MAX_WalkersOnAction;
        bool toCreateWalker = isMaxWalkersOnAction && (int)timer.Get() > 0 && (int)timer.Get() % timeBetweenSpawn == 0;

        if (isMaxWalkersOnAction && toCreateWalker)
        {
            numOfWalkersOnAction++;
            currentWalker = Instantiate(prefub_walker, transform);
        }
    }

    private void CreateCharacter()
    {
        numOfWalkersOnAction--;
        Destroy(currentWalker);
        timer.Reset();
    }

    private void BeachWalkerOut()
    {
        if(characterType == CharacterType.beach_start_with)
        {
            CreateCharacter();
        }
    }

    public void LifeOver()
    {
        if (characterType == CharacterType.Sea_deep_water || characterType == CharacterType.Sea_shallow_water)
        {
            CreateCharacter();
        }
    }
}
