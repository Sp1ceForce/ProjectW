using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class TestClass : MonoBehaviour, ITestSubscriber
{
    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
    public void DoDebug()
    {
        Debug.Log("TestMessage");
    }
}
