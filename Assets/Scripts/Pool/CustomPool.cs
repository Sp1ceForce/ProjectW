using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using EventBusSystem;

public class PullObjectData : ScriptableObject
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
public class CustomPool : MonoBehaviour, IFindItem
{
    private ObjectPool<GameObject> _pool;
    private bool _findObject = true;
    [SerializeField] private PullObjectData _data;
    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }
    private void Start()
    {
        _pool = new ObjectPool<GameObject>
        (createFunc: () =>
        {
            if (_findObject)
            {
                var objs = GameObject.FindGameObjectsWithTag(_data.ObjectTag);
                foreach (GameObject ojb in objs)
                {
                    ojb.transform.SetParent(this.gameObject.transform);
                    ojb.name = _data.ObjectName;
                }
                _findObject = false;
                if (objs.Length != 0)
                {
                    return objs[0];
                }
                else
                    return null;
            }
            else
            {
                return Instantiate(_data.ObjectPrefab);
            }
        },
        actionOnGet: (obj) => obj.SetActive(true),
        actionOnRelease: (obj) => obj.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: false,
        defaultCapacity: _data.DefaultCapacity,
        maxSize: _data.MaxPoolSize);
        _pool.Get();
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }


    //Для разных пулов написать разные интерфейсы-ключи?
    public void FindItem(GameObject gameObject)
    {
        StartCoroutine(On(gameObject));
    }
    public void ReleaseItem(GameObject gameObject)
    {
        _pool.Release(gameObject);
        // StartCoroutine(Off(gameObject));
    }
    IEnumerator On(GameObject obj)
    {
        yield return new WaitForSeconds(_data.TimeToRespawn);
        obj.SetActive(true);
        if (_data.Respawn)
            obj.GetComponent<PoolItem>().Respawn();
    }
}