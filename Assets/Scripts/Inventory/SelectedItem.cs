using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class SelectedItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int amount = 1;

    private void OnTriggerStay(Collider other)
    {
        if (!item) return;
        if (Input.GetKey(KeyCode.F))
        {
            item.currentTimeToPickUp += Time.deltaTime;
            if (item.currentTimeToPickUp >= item.timeToPickUp)
            {
                item.currentTimeToPickUp = 0;
                EventBus.RaiseEvent<IAddItem>(h => h.AddItem(item, amount));
                Destroy(this.gameObject);
            }

        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            item.currentTimeToPickUp = 0;
        }


    }
}
