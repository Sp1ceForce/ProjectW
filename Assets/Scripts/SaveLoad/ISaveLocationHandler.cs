using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSustem;
public interface ISaveLocationHandler : IGlobalSubscriber
{
    void SaveLocation();
}
