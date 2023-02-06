using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using EventBusSystem;
using System.Xml.Serialization;

[CreateAssetMenu(fileName = "SaveLoadManager", menuName = "ProjectW/SaveLoadManager")]

public class SaveLoadManager : ScriptableObject, ISaveWitchHandler, ISaveLocationHandler, ILoadWitchHandler, ILoadLocationHandler
{


    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
    private static SaveLoadManager _instance;
    public static SaveLoadManager Instance => _instance == null ? LoadData() : _instance;
    public static SaveLoadManager LoadData()
    {
        Debug.Log("SaveLoadManager is load!");

        return _instance = Resources.Load<SaveLoadManager>("SaveLoadManager");

    }




    public void LoadLocation()
    {
        Debug.Log("Load Location");
    }
    public void SaveLocation()
    {
        Debug.Log("Save Location");
    }

    public void SaveWitch()
    {
        Debug.Log(message: "Saving Witch...");

        EventBus.RaiseEvent<IMoveDataToSaveData>(h => h.MoveDataToSaveData());
        EventBus.RaiseEvent<IWitchToSaveData>(h => h.ToSaveData());

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));
        //Application.persistentDataPath это строка; выведите ее в логах и вы увидите расположение файла сохранений
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.txt");
        xmlSerializer.Serialize(file, CurrentSave.Instance.SaveData);
        file.Close();
        // Debug.Log(Application.persistentDataPath + "/savedGames.txt");

        Debug.Log(Application.persistentDataPath + "/savedGames.txt");
        Debug.Log(" Witch Save!");

    }
    public void LoadWitch()
    {
        Debug.Log("Loading Witch...");
        if (File.Exists(Application.persistentDataPath + "/savedGames.txt"))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));

            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.txt", FileMode.Open);
            CurrentSave.Instance.SaveData = (SaveData)xmlSerializer.Deserialize(file);
            file.Close();

            EventBus.RaiseEvent<ISaveDataToWitch>(h => h.DataToWitch());

            // Debug.Log(Application.persistentDataPath + "/savedGames.txt");
            Debug.Log("Witch Load!");
        }
        else
        {
            Debug.Log("File dosn't exist");
        }

    }
}