using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public Slider sliderTimer;

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    Dictionary<InventorySlot, GameObject> itemDisplay = new Dictionary<InventorySlot, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            InventorySlot inventorySlot = inventory.container[i];
            if(inventorySlot.timeToFinishWork > 0)
            {
                var obj = itemDisplay[inventorySlot];
                inventorySlot.timeToFinishWork = inventorySlot.item.timeToFinishWork - (int)inventorySlot.timerHelper.Get();
                obj.GetComponentInChildren<Slider>().value = inventorySlot.timeToFinishWork;
            }
            else
            {
                DestroyImmediate(itemDisplay[inventorySlot]);
                itemDisplay.Remove(inventorySlot);
                inventory.container.Remove(inventorySlot);
            }
        }
    }


    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            var obj = Instantiate(inventory.container[i].item.prefub, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<Slider>().maxValue = inventory.container[i].timeToFinishWork;
            itemDisplay.Add(inventory.container[i], obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN), Y_START +(-Y_SPACE_BETWEEN_ITEM * (i/NUMBER_OF_COLUMN)), 0f);
    }

    private void UpdateDisplay()
    {
        int lastIndex = inventory.container.Count - 1;
        //CreateDisplay();
        var obj = Instantiate(inventory.container[lastIndex].item.prefub, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(lastIndex);
        obj.GetComponentInChildren<Slider>().maxValue = inventory.container[lastIndex].timeToFinishWork;
        itemDisplay.Add(inventory.container[lastIndex], obj);
    }


    private void OnEnable()
    {
        Lifeguard.OnItemAddToInventory += UpdateDisplay;
    }

    private void OnDisable()
    {
        Lifeguard.OnItemAddToInventory -= UpdateDisplay;
    }
}
