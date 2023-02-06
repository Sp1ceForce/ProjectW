using System;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
using System.Collections;
/*
Автор: Дима
Для чего это нужно: для pool сущностей наследуем PoolItem.
Как использовать: назначаем для сущности tag, что совпадает тегом из PullObjectData. 
Объект добавиться в пул,
*/
public class TestItem : PoolItem
{
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;


    public override void Respawn()
    {

    }
    //How to use pool
    private void Start()
    {
        EventBus.RaiseEvent<IFindItem>(h => { h.ReleaseItem(this.gameObject); h.FindItem(this.gameObject); });
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;
    }

    //Example

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventBus.RaiseEvent<IFindItem>(h =>
            {
                h.FindItem(this.gameObject);
                h.ReleaseItem(this.gameObject);
            });
        }
    }
    //Example
}