using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class ObeliscIteractLogic : AfterInteract
{
    private List<GameObject> Campfire = new List<GameObject>();
    private bool active = false;
    private void Start()
    {
        var trn = transform;
        for (int i = 0; i < trn.childCount; i++)
        {
            Campfire.Add(trn.GetChild(i).gameObject);
            Campfire[i].SetActive(active);
        }
    }
    public override void AfterInteractLogic()
    {
        active = !active;
        foreach (GameObject item in Campfire)
        {
            item.SetActive(active);
        }
    }

}
