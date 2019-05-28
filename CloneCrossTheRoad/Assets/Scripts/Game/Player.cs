using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    public UnityEvent PlayerForwardMovedEvent;
    public UnityEvent PlayerDiedEvent;

    private int _currentPosition;

    private GameLine _currentLine;


    public int CurrentPosition
    {
        get
        {
            return _currentPosition;
        }

        set
        {
            _currentPosition = value;
        }
    }

    public GameLine CurrentLine
    {
        get
        {
            return _currentLine;
        }

        set
        {
            _currentLine = value;
            transform.parent = _currentLine.transform;
            UpdatePlayerPosition();
        }
    }

    public void TryMoveForward()
    {
        if(CurrentLine.NextLine == null  || !CurrentLine.NextLine.IsCanMoveToPosition(CurrentPosition))
        {
            return;
        }

        CurrentLine = CurrentLine.NextLine;
        PlayerForwardMovedEvent.Invoke();
    }

    public void TryMoveLeft()
    {
        TryMoveOnSide(CurrentPosition-1);
    }

    public void TryMoveRight()
    {
        TryMoveOnSide(CurrentPosition + 1);
    }

    public void Die()
    {
        PlayerDiedEvent.Invoke();
        gameObject.SetActive(false);
    }

    private void TryMoveOnSide(int newPosition)
    {
        if (CurrentLine.IsCanMoveToPosition(newPosition))
        {
            CurrentPosition = newPosition;
            UpdatePlayerPosition();
        }
    }

    private void UpdatePlayerPosition()
    {
        transform.localPosition = new Vector3(CurrentPosition - CurrentLine.Size / 2, 0, 0);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentLine.GetCellState(CurrentPosition) == GameCell.CellState.Dangerous)
        {
            Die();
        }
    }

}
