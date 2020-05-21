﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SwipeDetector;

public class SwipeLogger : MonoBehaviour
{
    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        Debug.Log("Swipe in direction: " + data);
    }
}