using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    public Swipe Swipe;

    public UnityEvent MoveForward;
    public UnityEvent MoveLeft;
    public UnityEvent MoveRight;



    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Swipe.SwipeLeft)
        {
            MoveLeft.Invoke();
        }
        if (Swipe.SwipeRight)
        {
            MoveRight.Invoke();
        }
        if (Swipe.Tap)
        {
            MoveForward.Invoke();
        }
    }
}
