using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    DayTimer dayTimer;
    public GameObject fieldsForSpawn;
    public GameObject SpawnedItem;
    public int amountItems = 1;
    public float sizeField = 5f;
    public float timePointForSpawn = 0.25f;
    private bool isTodaySpawned = false; 
    private SpawnItemHandler spawnItemHandler = new SpawnItemHandler();

    void Awake() {
        dayTimer = GameObject.Find("DayTimer").GetComponent<DayTimer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnItemHandler.CreateListOfFields(fieldsForSpawn);
        spawnItemHandler.SetSizeField(sizeField);
    }


    void SpawnItem(){
        spawnItemHandler.SpawnItemsInRandomPalaceAndField(SpawnedItem, amountItems);
    }

    // Update is called once per frame
    void Update()
    {
        if ((dayTimer.currentTimeOfDay < 0.25) && (isTodaySpawned)){
            isTodaySpawned = false;
        } 
        if ((dayTimer.currentTimeOfDay > timePointForSpawn) && (!isTodaySpawned)){
            SpawnItem();
            isTodaySpawned = true;
        }
    }
}
