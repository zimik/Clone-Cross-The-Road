using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSubjectsController<T> : MonoBehaviour, IReleasely where T:GameSubject
{

    public PoolsContainer PoolsContainer;

    public GameLine GameLine;

    public T[] SubjectPrefabs;

    protected List<T> SubjectInstances;

    private void Awake()
    {
        SubjectInstances = new List<T>();
    }

	// Update is called once per frame
	void Update () {
		
	}  

    public abstract void GenerateObjects();

    public virtual void Release()
    {
        while (SubjectInstances.Count > 0)
        {
            T gameSubject = SubjectInstances[0];
            SubjectInstances.RemoveAt(0);
            PoolsContainer.PushObjectByPrefab(gameSubject.gameObject, gameSubject.PrefabForPulling);
        }
    }
}
