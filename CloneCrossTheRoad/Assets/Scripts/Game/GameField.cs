using System;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour {

    public enum MoveDirection
    {
        Forward,
        Left,
        Right
    }

    public PoolsContainer PoolsContainer;
    public GameFieldSettings GameFieldSettings;
    public GameObject Container;

    private readonly List<GameLine> _lines = new List<GameLine>();

    private float _containerShift;
    private float _shift;

    private Player _player;

    public void StartPlay()
    {
        ClearGameField();
        GenerateGameField();
        StartGameFieldMove();
        InitializePlayer();
    }

    public void StopPlay()
    {
        StopGameFieldMove();
    }

    public void TryMoveForward()
    {
        _player.TryMoveForward();
    }

    public void TryMoveLeft()
    {
        _player.TryMoveLeft();
    }

    public void TryMoveRight()
    {
        _player.TryMoveRight();
    }

    public void SetFieldMoveSpeed(float speed)
    {
        _shift = speed;
    }

    private void TryMove(MoveDirection moveDirection)
    {
        Debug.Log(moveDirection);
    }

    private void InitializePlayer()
    {
        _player.CurrentPosition = GameFieldSettings.StartPosition.x;
        _player.CurrentLine = _lines[GameFieldSettings.StartPosition.y];
        _player.gameObject.SetActive(true);
    }

    private void ClearGameField()
    {
        while (_lines.Count > 0)
        {
            GameLine line = _lines[0];
            line.Release();
            PoolsContainer.PushObjectByPrefab(line.gameObject, line.PrefabForPulling);
            _lines.RemoveAt(0);
        }
    }

    private void GenerateGameField()
    {
        for(int linesIndex = 0; linesIndex< GameFieldSettings.FieldSize.y; linesIndex++)
        {
            if (linesIndex!= GameFieldSettings.StartPosition.y)
            {
                AddLineByPrefab(GameFieldSettings.GetRandomPrefab());
            }
            else
            {
                AddLineByPrefab(GameFieldSettings.StartLinePrefab);
            }
        }
        SetLinesPositions();
    }

    private void AddLineByPrefab(GameLine prefab)
    {
        GameLine line = PoolsContainer.PullObjectByPrefab<GameLine>(prefab.gameObject, Container.transform);
        line.PrefabForPulling = prefab.gameObject;
        line.InitialiLine(GameFieldSettings.FieldSize.x);
        if (_lines.Count > 0)
        {
            _lines[_lines.Count - 1].SetNextLine(line);
        }
        _lines.Add(line);

    }

    private void RegenerateLine()
    {
        GameLine line = _lines[0];
        if(line == _player.CurrentLine)
        {
            _player.Die();
        }
        line.Release();
        PoolsContainer.PushObjectByPrefab(line.gameObject, line.PrefabForPulling);
        _lines.RemoveAt(0);


        AddLineByPrefab(GameFieldSettings.GetRandomPrefab());
        SetLinesPositions();

    }

    private void StartGameFieldMove()
    {
//        _shift = _speedOfFieldMove;
    }

    public void StopGameFieldMove()
    {
        _shift = 0;
    }

    private void SetLinesPositions()
    {
        for (int linesIndex = 0; linesIndex < _lines.Count; linesIndex++)
        {
            _lines[linesIndex].transform.localPosition = new Vector3(0,0,linesIndex);
        }
    }
    

    // Use this for initialization
    void Start () {
        _player = Instantiate( GameFieldSettings.PlayerPrefab);
        _player.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _containerShift = Math.Max(-1, _containerShift - _shift);
        if (_containerShift == -1)
        {
            RegenerateLine();
            _containerShift = 0;
        }
        Container.transform.position = new Vector3(0, 0, _containerShift);
        
    }
}

[System.Serializable]
public class GameFieldSettings
{
    public Vector2Int FieldSize;
    public Vector2Int StartPosition;

    public GameLine StartLinePrefab;

    public GameLine[] GeneratedLinesPrefab;

    public Player PlayerPrefab;

    public GameLine GetRandomPrefab()
    {
        return GeneratedLinesPrefab[UnityEngine.Random.Range(0, GeneratedLinesPrefab.Length)];
    }

}
