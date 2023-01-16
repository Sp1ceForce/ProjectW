using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public interface IMoveDataToMoveCntr : IGlobalSubscriber
{
    void DataToMoveController();
}
