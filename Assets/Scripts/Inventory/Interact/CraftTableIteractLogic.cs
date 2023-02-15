using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class CraftTableIteractLogic : AfterInteract
{
    [SerializeField] public GameObject panelUI;
    private bool active = false;
    public override void AfterInteractLogic()
    {
        if (active != true)
        {
            active = true;
            panelUI.SetActive(active);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (active != false)
        {
            active = false;
            panelUI.SetActive(active);
        }
    }

}
