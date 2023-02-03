using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class IngredientAfterIteractLogic : AfterInteract
{
    public override void AfterInteractLogic()
    {
        Destroy(this.gameObject);
    }

}
