using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLine : MonoBehaviour, IReleasely {

    public UnityEvent LineInitialized;
    public UnityEvent LineReleased;

    public GameObject Background;

    public GameObject PrefabForPulling;

    private GameLine _nextLine;

    private List<GameCell> _cells;

    private int _size;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetNextLine(GameLine nextLine)
    {
        _nextLine = nextLine;
    }

    public void InitialiLine(int size)
    {
        _size = size;
        Background.transform.localScale = new Vector3(size, 1, 1);

        _cells = new List<GameCell>();
        for (int i = 0; i<_size; i++)
        {
            _cells.Add(new GameCell());
        }

        LineInitialized.Invoke();
    }

    public int Size
    {
        get
        {
            return _size;
        }
    }

    public GameLine NextLine
    {
        get
        {
            return _nextLine;
        }
    }

    public bool IsCanMoveToPosition(int position)
    {
        if(position<0 || position >= _size)
        {
            return false;
        }

        if (_cells[position].State == GameCell.CellState.Busy)
        {
            return false;
        }
        return true;
    }

    public void SetCellState(int position, GameCell.CellState state)
    {
        if (position >= 0 && position < _size)
        {
            _cells[position].State = state;
        }
            
    }

    public void SetCellState(int position, int count, GameCell.CellState state)
    {
        for (int i = position; i< (position+count); i++)
        {
            SetCellState(i, state);
        }
    }

    public GameCell.CellState GetCellState(int position)
    {
        return _cells[position].State;
    }

    public void Release()
    {
        LineReleased.Invoke();
    }
}
