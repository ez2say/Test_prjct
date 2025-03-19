using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool = new List<T>();
    public Transform _poolContainer;

    public ObjectPool(string poolName)
    {
        GameObject container = new GameObject(poolName);
        _poolContainer = container.transform;
    }

    public void AddObject(T obj)
    {
        obj.transform.SetParent(_poolContainer);
        _pool.Add(obj);
    }

    public T GetObject()
    {
        foreach (T obj in _pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    public void RemoveObject(T obj)
    {
        _pool.Remove(obj);
    }
}