using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> FieldsForSpawn = new List<GameObject>();
    private float fieldSize = 5f;
    private float highSpawn = 1f;

    public void CreateListOfFields(GameObject fields){
        var trn = fields.transform;
        for (int i = 0; i < trn.childCount; i++)
        {
            FieldsForSpawn.Add(trn.GetChild(i).gameObject);
        }
    }

    public void ActivateFields(){
        // foreach (GameObject item in FieldsForSpawn)
        // {
        //     item.SetActive(true);
        // }
    }

    public void SpawnItemInCentre(GameObject item){
        foreach (GameObject field in FieldsForSpawn){
            Vector3 spawnOffset = new Vector3 (0.0f, highSpawn, 0.0f);
            Instantiate(item, field.transform.position + spawnOffset ,field.transform.rotation);
        }
    }

    public void SpawnOneItemInRandomPalace(GameObject item){
        foreach (GameObject field in FieldsForSpawn){
            float xSpawn = Random.value * fieldSize - fieldSize * 0.5f;
            float zSpawn = Random.value * fieldSize - fieldSize * 0.5f;
            Vector3 spawnOffset = new Vector3 (xSpawn, highSpawn, zSpawn);
            Instantiate(item, field.transform.position + spawnOffset ,field.transform.rotation);
        }
    }

    public void SpawnItemsInRandomPalaceAndField(GameObject item, int ItemCount){
        //foreach (GameObject field in FieldsForSpawn){
        while (ItemCount != 0) {
            int fieldRandNumber = Random.Range(0, FieldsForSpawn.Count);
            float xSpawn = Random.value * fieldSize - fieldSize * 0.5f;
            float zSpawn = Random.value * fieldSize - fieldSize * 0.5f;
            Vector3 spawnOffset = new Vector3 (xSpawn, highSpawn, zSpawn);
            Instantiate(item, FieldsForSpawn[fieldRandNumber].transform.position + spawnOffset ,FieldsForSpawn[fieldRandNumber].transform.rotation);
            ItemCount -= 1;
        }
    }

    public void SetSizeField(float sizeField){
        fieldSize = sizeField;
    }


}
