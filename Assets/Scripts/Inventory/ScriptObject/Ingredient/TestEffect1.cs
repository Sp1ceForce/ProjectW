
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestEffect1", menuName = "ProjectW/Items/Effect/TestEffect1")]

public class TestEffect1 : AbstractEffect
{
    private void Start()
    {
        bombTag1 = "test";
    }
}
