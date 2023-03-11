using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class SelectedItem : MonoBehaviour
{
    [SerializeField] public Item item;
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
                    {
                        if (this.gameObject.TryGetComponent<BombHandler>(out BombHandler bombHandler))
                        {
                            EventBus.RaiseEvent<IAddItem>(h =>
                            {
                                GameObject iconGameObject = h.AddItem(bombHandler.bombItem, amount);
                                iconGameObject.AddComponent<BombHandler>().InitFromAnotherHandler(bombHandler);
                            });
                        }
                        else if (this.gameObject.TryGetComponent<PotionHandler>(out PotionHandler potionHandler))
                        {
                            EventBus.RaiseEvent<IAddItem>(h =>
                            {
                                GameObject iconGameObject = h.AddItem(potionHandler.potionItem, amount);
                                iconGameObject.AddComponent<PotionHandler>().InitFromAnotherHandler(potionHandler);
                            });
                        }
                        else EventBus.RaiseEvent<IAddItem>(h => h.AddItem(item, amount));//почему это работает правильно? на сцене же минимум 4 инвентаря...
                    }
                    gameObject.GetComponent<AfterInteract>().AfterInteractLogic();
                }
            }

        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            item.currentTimeToPickUp = 0;
        }
    }
}
