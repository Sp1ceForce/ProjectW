using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public interface IWitchToSaveData : IGlobalSubscriber
{
    void ToSaveData();
}
