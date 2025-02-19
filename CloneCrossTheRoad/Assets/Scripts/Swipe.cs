﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {

            isDraging = true;
            startTouch = Input.mousePosition;
        }else if (Input.GetMouseButtonUp(0))
        {
            tap = isDraging;
            Reset();
            
        }
#elif UNITY_ANDROID
        if (Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if( Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                tap = isDraging;
                Reset();
            }
        }
#endif
        swipeDelta = Vector2.zero;
        if(isDraging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

        }

        if (swipeDelta.magnitude>100)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                }else
                {
                    swipeRight = true;
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }
            Reset();
        }

    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

}
