using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using EventBusSystem;

[CreateAssetMenu(fileName = "PoolObjectData", menuName = "ProjectW/Pool/PoolObjectData")]
public class PoolObjectData : ScriptableObject
{
    [SerializeField] private int _defaultCapacity = 10;
    public int DefaultCapacity { get => _defaultCapacity; set => _defaultCapacity = value; }
    [SerializeField] private int _maxPoolSize = 1000;
    public int MaxPoolSize { get => _maxPoolSize; set => _maxPoolSize = value; }
    [SerializeField] private bool _respawn = true;
    public bool Respawn { get => _respawn; set => _respawn = value; }
    [SerializeField] private float _timeToRespawn = 2f;
    public float TimeToRespawn { get => _timeToRespawn; set => _timeToRespawn = value; }
    [SerializeField] private string _objectName = "Object";
    public string ObjectName { get => _objectName; set => _objectName = value; }
    [SerializeField] private string _objectTag = "TestItem";
    public string ObjectTag { get => _objectTag; set => _objectTag = value; }
    [SerializeField] private GameObject _objectPrefab;
    public GameObject ObjectPrefab { get => _objectPrefab; set => _objectPrefab = value; }
}