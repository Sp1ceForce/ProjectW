using System;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class TestInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventBus.RaiseEvent<ITestSubscriber>(h => h.DoDebug());
        }
    }
}