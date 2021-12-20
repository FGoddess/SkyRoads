using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    public static ObjectPool Instance => _instance;

    [SerializeField] private GameObject _objToPool;
    [SerializeField] private int _amountToPool;

    private List<GameObject> _pooledObjects;

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if(!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }

        return null;
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _pooledObjects = new List<GameObject>();
        for(int i = 0; i < _amountToPool; i++)
        {
            var temp = Instantiate(_objToPool, transform);
            temp.SetActive(false);
            _pooledObjects.Add(temp);
        }
    }
}
