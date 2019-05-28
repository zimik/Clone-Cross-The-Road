
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool:MonoBehaviour{

    public GameObject ContaterForPullingObjects;

    public GameObject Prefab;

    public List<GameObject> _objects;

    private void Awake()
    {
        _objects = new List<GameObject>();
    }
    
    public GameObject GetGameObjectFromPull()
    {
        if (_objects.Count>0)
        {
            GameObject gm = _objects[0];
            _objects.RemoveAt(0);
            return gm;
        }

        return Instantiate(Prefab);
    }

    public void PushGameObjectToPull(GameObject pushingObject)
    {
        if (_objects.IndexOf(pushingObject)==-1)
        {
            _objects.Add(pushingObject);
            pushingObject.SetActive(false);
            pushingObject.transform.parent = ContaterForPullingObjects.transform;
        }
    }
}
