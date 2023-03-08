using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemTrigger : AfterInteract
{

    //[SerializeField] private GameObject trigger;
    public GameObject fieldsForSpawn;
    public GameObject SpawnedItem;
    public int amountItems = 1;
    public float sizeField = 5f;
    private SpawnItemHandler spawnItemHandler;

    // Start is called before the first frame update
    void Start()
    {
        spawnItemHandler = gameObject.AddComponent<SpawnItemHandler>();
        spawnItemHandler.CreateListOfFields(fieldsForSpawn);
        spawnItemHandler.SetSizeField(sizeField);
    }

    public override void AfterInteractLogic()
    {
        spawnItemHandler.SpawnItemsInRandomPalaceAndField(SpawnedItem, amountItems);
    }

}

