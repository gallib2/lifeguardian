using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacters : MonoBehaviour
{
    public GameObject characterPrefub;
    public List<GameObject> characters;

    public List<int> timesInTheWater;
    public List<int> timesOutsideTheWater;
    public List<Vector3> positions;
    public List<Vector3> dangerTargets;

    public int maxPeopleInTheWater;
    private int howManyInTheWater;

    public int MAX_NumberOfCharacters;
    private int currentNumberOfCharacters;
    private bool toCreateCharacters;


    //private TimerHelper timer;
    private Vector3 lowerLeftPosition = new Vector3(-3f, -5f, 0);
    private Vector3 lowerRightPosition = new Vector3(0.6f, -5f, 0);
    private Vector3 defaultStartPosition = new Vector3(-3f, -5f, 0);

    private void OnEnable()
    {
        KidPackController.OnLifeOver += OnCharacterDead;
    }

    private void OnDisable()
    {
        KidPackController.OnLifeOver -= OnCharacterDead;
    }

    void Start()
    {
        // -1.3
        // -2.7

        MAX_NumberOfCharacters = 2;
        CreateCharacters();
        //int arrSize = timesInTheWater.Count;
        //characters = new List<GameObject>(arrSize);

        ////Instantiate(characterPrefub, transform);
        //for (int i = 0; i < arrSize; i++)
        //{
        //    float x_startPosition = Random.Range(lowerLeftPosition.x, lowerRightPosition.x);
        //    defaultStartPosition.x = x_startPosition;
        //    GameObject chararcter = Instantiate(characterPrefub,lowerLeftPosition, Quaternion.identity ,transform);
        //    KidPackController kidPackController = chararcter.GetComponent<KidPackController>();
        //    chararcter.transform.position = defaultStartPosition;
        //    kidPackController.InitPosition = defaultStartPosition;
        //    kidPackController.TargetPosition = positions[i];
        //    kidPackController.TimeInTheWater = timesInTheWater[i];
        //    kidPackController.TimeInOutsideWater = timesOutsideTheWater[i];
        //    kidPackController.DangerTarget = dangerTargets[i];

        //    // new Vector3(2f, 0, 0)

        //    //CharacterSettings characterSettings = new CharacterSettings(timeInTheWater, timeInOutsideWater, position, lowerLeftPosition, chararcter);
        //    //characters.Add(chararcter);
        //}
    }

    private void Update()
    {

    }

    private void OnCharacterDead()
    {
        currentNumberOfCharacters--;
        Debug.Log("OnCharacterDead" + currentNumberOfCharacters);

        if (currentNumberOfCharacters == 0)
        {
            Debug.Log("inside if");
            CreateCharacters();
        }

    }

    private void CreateCharacters()
    {
        int arrSize = timesInTheWater.Count;
        //characters = new List<GameObject>(arrSize);
        Debug.Log("CreateCharacters");

        for (int i = 0; i < arrSize; i++)
        {
            currentNumberOfCharacters++;
            float x_startPosition = Random.Range(lowerLeftPosition.x, lowerRightPosition.x);
            defaultStartPosition.x = x_startPosition;
            GameObject chararcter = Instantiate(characterPrefub, lowerLeftPosition, Quaternion.identity, transform);
            KidPackController kidPackController = chararcter.GetComponent<KidPackController>();
            chararcter.transform.position = defaultStartPosition;
            kidPackController.InitPosition = defaultStartPosition;
            kidPackController.TargetPosition = positions[i];
            kidPackController.TimeInTheWater = timesInTheWater[i];
            kidPackController.TimeInOutsideWater = timesOutsideTheWater[i];
            kidPackController.DangerTarget = dangerTargets[i];

            // new Vector3(2f, 0, 0)

            //CharacterSettings characterSettings = new CharacterSettings(timeInTheWater, timeInOutsideWater, position, lowerLeftPosition, chararcter);
            //characters.Add(chararcter);
        }
    }

}
