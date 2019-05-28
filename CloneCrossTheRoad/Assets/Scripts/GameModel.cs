using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour
{

    public IntUnityEvent ChangeScoreEvent;

    private int _scores;

    public void IncreaseScores()
    {
        Scores++;
    }

    public void Reset()
    {
        Scores = 0;
    }

    public int Scores
    {
        private set
        {
            _scores = value;
            ChangeScoreEvent.Invoke(Scores);
        }
        get
        {
            return _scores;
        }
    }
}
