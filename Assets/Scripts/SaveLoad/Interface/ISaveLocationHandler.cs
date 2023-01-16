using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public interface ISaveLocationHandler : IGlobalSubscriber
{
    void SaveLocation();
}
