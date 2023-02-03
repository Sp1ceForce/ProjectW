using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class PotionIteractLogic : AfterInteract
{
    [SerializeField] public GameObject potionUI;
    private bool active = false;
    public override void AfterInteractLogic()
    {
        if (active != true)
        {
            active = true;
            potionUI.SetActive(active);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active != false)
        {
            active = false;
            potionUI.SetActive(active);
        }
    }

}
