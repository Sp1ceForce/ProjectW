using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public interface IAddToCraft : IGlobalSubscriber
{
    void AddToCraft();
}
