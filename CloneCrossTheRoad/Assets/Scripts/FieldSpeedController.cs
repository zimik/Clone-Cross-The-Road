using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSpeedController : MonoBehaviour {

    public FloatUnityEvent ChangeSpeedEvent;

    public void ChangeScoresHandler(int scores)
    {
        ChangeSpeedEvent.Invoke((float)scores / 1000+0.001f);
    }
	
}
