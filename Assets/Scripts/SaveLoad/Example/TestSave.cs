using System;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
public class TestSave : MonoBehaviour
{
    [SerializeField] private CurrentSave CurrentSave;
    [SerializeField] private SaveLoadManager SaveLoadManager;

    private void Update()
    {
        //Просто в нужный момент вызываем ивент с сохранением/загрузкой нужной сущности
        //Загрузка локаций будет реализована похожим образом, но позже
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventBus.RaiseEvent<ISaveWitchHandler>(h => h.SaveWitch());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            EventBus.RaiseEvent<ILoadWitchHandler>(h => h.LoadWitch());
        }
    }
}