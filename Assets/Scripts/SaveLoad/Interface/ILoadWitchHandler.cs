using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public interface ILoadWitchHandler : IGlobalSubscriber
{
    void LoadWitch();
}
