using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{

    Vector2 startTouchPos, currenTouchtPos, endTouchPos;
    bool stopTouch = false;

    public float swipeRange;
    public float tapRange;
    public static bool swipeLeft, swipeRight, swipeUp, swipeDown, tap;

    void Update()
    {
        swipeLeft = swipeRight = swipeUp = swipeDown = tap = false;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currenTouchtPos = Input.GetTouch(0).position;
            Vector2 Distance = currenTouchtPos - startTouchPos;

            if (!stopTouch)
            {
                if (Distance.x < -swipeRange)
                {
                    swipeLeft = true;
                    stopTouch = true;

                } else if (Distance.x > swipeRange)
                {
                    swipeRight = true;
                    stopTouch = true;

                } else if (Distance.y > swipeRange)
                {
                    swipeUp = true;
                    stopTouch = true;

                } else if (Distance.y < -swipeRange)
                {
                    swipeDown = true;
                    stopTouch = true;
                }

            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPos = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPos - startTouchPos;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
                tap = true;
        }


    }

}
