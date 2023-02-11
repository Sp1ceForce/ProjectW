using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public interface IRefreshCraft : IGlobalSubscriber
{
    void RefreshCraft();
}
