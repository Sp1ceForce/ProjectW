using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public interface IAddItem : IGlobalSubscriber
{
    void AddItem(Item item, int amount = 1);
}
