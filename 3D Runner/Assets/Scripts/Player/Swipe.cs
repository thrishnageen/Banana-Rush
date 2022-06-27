using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool isHolding = false;
    private Vector2 startTouch, swipeDelta;
    public static bool swipeLeft, swipeRight, swipeUp, swipeDown, tap;
    private void Update()
    {
        swipeLeft = swipeRight = swipeUp = swipeDown = tap = false;
        
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isHolding = true;
            startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0))
        {
            isHolding = false;
            Reset();
        }

        swipeDelta = Vector2.zero;
        if (isHolding)
        {
            if (Input.touches.Length < 0)
            {
                Debug.Log("Touch");
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if (swipeDelta.magnitude > 100)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isHolding = false;

    }

}
