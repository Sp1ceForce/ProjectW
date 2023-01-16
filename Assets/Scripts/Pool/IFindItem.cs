using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public interface IFindItem : IGlobalSubscriber
{
    void FindItem(GameObject gameObject);
    public void ReleaseItem(GameObject gameObject);
}
