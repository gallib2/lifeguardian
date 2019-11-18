using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacters : MonoBehaviour
{
    public GameObject characterPrefub;
    public List<CharacterSettings> characters;
    public float speed;

    public List<int> timesInTheWater;
    public List<int> timesOutsideTheWater;
    public List<Vector3> positions;
    public bool ArrivedWaterPosition { get; set; }
    public bool ArrivedOutsidePosition { get; set; }

    public int maxPeopleInTheWater;
    private int howManyInTheWater;

    private bool continueCheckTimeToExit = true;
    private bool continueCheckTimeToEnter = true;

    private TimerHelper timer;
    private Vector3 lowerLeftPosition = new Vector3(-3f, -5f, 0);
    private Vector3 lowerRightPosition = new Vector3(2.5f, -5f, 0);

    void Start()
    {
        timer = new TimerHelper();
        int arrSize = timesInTheWater.Count;
        characters = new List<CharacterSettings>(arrSize);
        //Instantiate(characterPrefub, transform);
        for (int i = 0; i < arrSize; i++)
        {
            int timeInTheWater = timesInTheWater[i];
            int timeInOutsideWater = timesOutsideTheWater[i];
            Vector3 position = positions[i];
            GameObject chararcter = Instantiate(characterPrefub,lowerLeftPosition, Quaternion.identity ,transform);
            CharacterSettings characterSettings = new CharacterSettings(timeInTheWater, timeInOutsideWater, position, lowerLeftPosition, chararcter);
            characters.Add(characterSettings);
        }
    }

    void Update()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            bool needToEnterWater = continueCheckTimeToEnter && (int)timer.Get() % characters[i].timeInOutsideWater == 0;

            if (!characters[i].isInTheWater)
            {
                bool canEnterWater = (howManyInTheWater < maxPeopleInTheWater) && (!continueCheckTimeToEnter || needToEnterWater);

                if (canEnterWater)
                {
                    // Enter the water
                    Vector3 target = characters[i].position;
                    continueCheckTimeToEnter = false;
                    ArrivedWaterPosition = MoveObject(target, characters[i]);

                    if (ArrivedWaterPosition)
                    {
                        howManyInTheWater++;
                        continueCheckTimeToExit = true;
                    }
                }

            }
            else
            {
                bool needToExitWater = continueCheckTimeToExit && (int)timer.Get() % characters[i].timeInTheWater == 0;

                if (!continueCheckTimeToExit || needToExitWater)
                {
                    // Exit form water
                    continueCheckTimeToExit = false;
                    float step = speed * Time.deltaTime;
                    Vector3 target = characters[i].initPosition;

                    ArrivedOutsidePosition = MoveObject(target, characters[i]);

                    if (ArrivedOutsidePosition)
                    {
                       
                        howManyInTheWater--;
                        continueCheckTimeToEnter = true;
                    }
                }
            }
        }
    }

    public bool MoveObject(Vector3 target, CharacterSettings character)
    {
        float step = speed * Time.deltaTime;
        bool arrivePosition = false;
        ArrivedOutsidePosition = false;
        ArrivedWaterPosition = false;

        if (character.characterPrefub)
        {
            character.characterPrefub.transform.position = Vector2.MoveTowards(character.characterPrefub.transform.position, target, step);
            character.characterPrefub.GetComponentInChildren<CharacterMovement>().ArrivedPatrolPosition = false;
            arrivePosition = character.characterPrefub.transform.position == target;

            if (arrivePosition)
            {
                character.isInTheWater = !character.isInTheWater;
                character.characterPrefub.GetComponentInChildren<CharacterMovement>().ArrivedPatrolPosition = true;
            }

            return arrivePosition;
        }

        return arrivePosition;
    }
}

public class CharacterSettings 
{
    public int timeInTheWater;
    public int timeInOutsideWater;
    public bool isInTheWater;
    public Vector3 position;
    public Vector3 initPosition;
    public GameObject characterPrefub;

    public CharacterSettings(int _timeInTheWater, int _timeInOutsideWater, Vector3 _position, Vector3 _initPosition , GameObject _characterPrefub)
    {
        characterPrefub = _characterPrefub;
        timeInTheWater = _timeInTheWater;
        timeInOutsideWater = _timeInOutsideWater;
        position = _position;
        initPosition = _initPosition;
        isInTheWater = false;
        
        characterPrefub.transform.position = _initPosition;
        characterPrefub.GetComponentInChildren<CharacterMovement>().InitPosition = position;
    }
}
