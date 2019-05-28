using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableSubjectsController : BaseSubjectsController<GameSubject> {
   
    public override void GenerateObjects()
    {
        float gs2 = GameLine.Size / 2;
        SubjectInstances = new List<GameSubject>();
        for (int i= 0; i< GameLine.Size; i++)
        {
            GameSubject prefab = SubjectPrefabs[Random.Range(0, SubjectPrefabs.Length)];
           
            bool needGenerate = Random.Range(0, gs2 - Mathf.Abs(gs2-i)) < 1;
            if (needGenerate)
            {
//                GameSubject gameSubject = Instantiate(prefab, GameLine.transform);
                GameSubject gameSubject = PoolsContainer.PullObjectByPrefab<GameSubject>(prefab.gameObject, GameLine.transform);
                gameSubject.PrefabForPulling = prefab.gameObject;
                gameSubject.transform.localPosition = new Vector3(i - GameLine.Size / 2, 0, 0);
                SubjectInstances.Add(gameSubject);

                GameLine.SetCellState(i, GameCell.CellState.Busy);
            }
        }
    }
}
