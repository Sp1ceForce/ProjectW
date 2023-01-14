using System;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class TestSave : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventBus.RaiseEvent<ISaveWitchHandler>(h => h.SaveWitch());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            EventBus.RaiseEvent<ILoadWitchHandler>(h => h.LoadWitch());
        }
    }
}