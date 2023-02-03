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
            if (other.tag == "Player")
            {
                item.currentTimeToPickUp += Time.deltaTime;
                if (item.currentTimeToPickUp >= item.timeToPickUp)
                {
                    item.currentTimeToPickUp = 0;
                    if (item.itSelectedItem)
                        EventBus.RaiseEvent<IAddItem>(h => h.AddItem(item, amount));
                    gameObject.GetComponent<AfterInteract>().AfterInteractLogic();
                    // Destroy(this.gameObject);
                }
            }

        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            item.currentTimeToPickUp = 0;
        }
    }
}
