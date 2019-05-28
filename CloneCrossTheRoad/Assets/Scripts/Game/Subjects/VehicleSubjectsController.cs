using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSubjectsController : BaseSubjectsController<VehicleGameSubject> {

    public float Speed = 0.01f;

    public override void GenerateObjects()
    {
        SubjectInstances = new List<VehicleGameSubject>();
        for (int i = 0; i < GameLine.Size; i++)
        {
            VehicleGameSubject prefab = SubjectPrefabs[Random.Range(0, SubjectPrefabs.Length)];
            bool needGenerate = Random.Range(0, 10) < 1;
            if (needGenerate)
            {
                VehicleGameSubject gameSubject = PoolsContainer.PullObjectByPrefab<VehicleGameSubject>(prefab.gameObject, GameLine.transform);
                gameSubject.PrefabForPulling = prefab.gameObject;

                gameSubject.transform.localPosition = new Vector3(i - GameLine.Size / 2, 0, 0);
                SubjectInstances.Add(gameSubject);

                GameLine.SetCellState(i, gameSubject.Size, GameCell.CellState.Dangerous);
                i += (gameSubject.Size);
            }
        }
    }

    private void FixedUpdate()
    {
        GameLine.SetCellState(0, GameLine.Size, GameCell.CellState.Free);
        for (int i = 0; i < SubjectInstances.Count; i++)
        {
            VehicleGameSubject gameSubject = SubjectInstances[i];
            gameSubject.transform.Translate(Speed,0,0);
            int newPosition = Mathf.CeilToInt(SubjectInstances[i].transform.position.x+ GameLine.Size / 2);
            if(newPosition>= GameLine.Size)
            {
                gameSubject.transform.localPosition = new Vector3(-GameLine.Size / 2, 0);
            }
            else
            {
                GameLine.SetCellState(newPosition, gameSubject.Size, GameCell.CellState.Dangerous);
            }

        }
    }

    void OnDisable()
    {
        for (int i = 0; i < SubjectInstances.Count; i++)
        {
            SubjectInstances[i].gameObject.SetActive(false);
        }
    }
}
