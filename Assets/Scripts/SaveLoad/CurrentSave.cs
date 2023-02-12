using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
[System.Serializable]
public class SaveData
{
    private Vector3 _positon;
    public Vector3 Position { get => _positon; set => _positon = value; }
    private Quaternion _rotation;
    public Quaternion Rotation { get => _rotation; set => _rotation = value; }
    int hp = 0;
    int curced = 0;
}
[CreateAssetMenu(fileName = "CurrentSave", menuName = "ProjectW/CurrentSave")]
public class CurrentSave : ScriptableObject, IWitchToSaveData, ISaveDataToWitch
{
    private static CurrentSave _instance;
    public static CurrentSave Instance => _instance == null ? LoadData() : _instance; // Instance { get => _instance; set => _instance = value; }
    private SaveData _saveData;
    public SaveData SaveData { get => _saveData; set => _saveData = value; }
    private MoveControllerData _moveControllerData;
    public MoveControllerData MoveControllerData => _moveControllerData;
    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }
    public void ToSaveData()
    {
        SaveData.Rotation = MoveControllerData.Rotation;
        SaveData.Position = MoveControllerData.Position;
    }
    public void DataToWitch()
    {
        MoveControllerData.Rotation = SaveData.Rotation;
        MoveControllerData.Position = SaveData.Position;
        EventBus.RaiseEvent<IMoveDataToMoveCntr>(h => h.DataToMoveController());
    }
    public static CurrentSave LoadData()
    {
        Debug.Log("CurrentSave is load!");
        return _instance = Resources.Load<CurrentSave>("CurrentSave");

    }


}


