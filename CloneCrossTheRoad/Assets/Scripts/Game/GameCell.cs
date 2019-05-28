using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCell
{
    public enum CellState
    {
        Free,
        Busy,
        Dangerous,
        Movable
    }

    private CellState _state;

    public CellState State
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;

        }
    }

}
