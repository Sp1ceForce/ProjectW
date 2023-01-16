using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour, ISaveWitchHandler, ISaveLocationHandler, ILoadWitchHandler, ILoadLocationHandler
{
    public void LoadLocation()
    {
        Debug.Log("Load Location");
    }

    public void LoadWitch()
    {
        Debug.Log("Load Witch");
    }

    public void SaveLocation()
    {
        Debug.Log("Save Location");
    }

    public void SaveWitch()
    {
        Debug.Log("Save Witch");
    }
}