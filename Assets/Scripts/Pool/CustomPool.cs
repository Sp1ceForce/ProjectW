using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using EventBusSystem;


public class CustomPool : MonoBehaviour, IFindItem
{
    private ObjectPool<GameObject> _pool;
    private bool _findObject = true;
    [SerializeField] private PoolObjectData _data;
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