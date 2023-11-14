using UnityEngine;
using System.Collections.Generic;



public class ObjectPool
{

    private GameObject prefab;

    private List<GameObject> pool;

    private GameObject poolParent;
    public ObjectPool(GameObject prefab, int initialSize, string poolParentName)
    {
        this.prefab = prefab;
        poolParent = new GameObject(poolParentName);
        poolParent.transform.position = Vector3.zero;
        poolParent.transform.rotation = Quaternion.identity;
        this.pool = new List<GameObject>();
        for (int i = 0; i < initialSize; i++)
        {
            AllocateInstance();
        }
    }

    public GameObject GetInstance()
    {
        if (pool.Count == 0)
        {
            AllocateInstance();
        }

        int lastIndex = pool.Count - 1;
        GameObject instance = pool[lastIndex];
        pool.RemoveAt(lastIndex);

        instance.SetActive(true);
        return instance;
    }

    public void ReturnInstance(GameObject instance)
    {
        instance.SetActive(false);
        pool.Add(instance);
    }

    protected virtual GameObject AllocateInstance()
    {
        if (poolParent == null)
            return null;

        GameObject instance = (GameObject)GameObject.Instantiate(prefab, poolParent.transform);
        instance.SetActive(false);
        pool.Add(instance);

        return instance;
    }

}
