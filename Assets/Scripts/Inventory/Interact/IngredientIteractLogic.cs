using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class IngredientIteractLogic : AfterInteract
{
    public override void AfterInteractLogic()
    {
        Destroy(this.gameObject);
    }

}
