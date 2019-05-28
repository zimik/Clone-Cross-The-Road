using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsContainer : MonoBehaviour
{

    public GameObjectsPool GameObjectsPool;

    public Dictionary<GameObject, GameObjectsPool> _pools;

    private void Awake()
    {
        _pools = new Dictionary<GameObject, GameObjectsPool>();
    }

    public GameObject PullObjectByPrefab(GameObject prefab)
    {
        GameObject gm = GetPullByPrefab(prefab).GetGameObjectFromPull();
        gm.SetActive(true);
        return gm;
    }

    public GameObject PullObjectByPrefab(GameObject prefab, Transform parent)
    {
        GameObject gm = PullObjectByPrefab(prefab);
        gm.transform.parent = parent;
        return gm;
    }

    public T PullObjectByPrefab<T>(GameObject prefab)
    {
        GameObject gm = PullObjectByPrefab(prefab);
        return gm.GetComponent<T>();
    }

    public T PullObjectByPrefab<T>(GameObject prefab, Transform parent)
    {
        return PullObjectByPrefab(prefab, parent).GetComponent<T>();
    }

    public void PushObjectByPrefab(GameObject pushingObject,GameObject prefab)
    {
        GetPullByPrefab(prefab).PushGameObjectToPull(pushingObject);
    }

    private GameObjectsPool GetPullByPrefab(GameObject prefab)
    {
        if (!_pools.ContainsKey(prefab))
        {
            GameObjectsPool pool = Instantiate(GameObjectsPool);
            pool.Prefab = prefab;
            _pools.Add(prefab, pool);
        }

        return _pools[prefab];
    }

}
