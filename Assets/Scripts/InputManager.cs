﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {


    //Move Right Event
    public delegate void OnRightScreenTouchStay();
    public event OnRightScreenTouchStay OnRightScreenTouchStationary;
    //Move Left Event
    public delegate void OnLeftScreenTouchStay();
    public event OnLeftScreenTouchStay OnLeftScreenTouchStationary;
    //Jump Event
    public delegate void OnJumpTouch();
    public event OnJumpTouch OnJumpTouched;


    //Variables for keyboard doubletap
    private float delay = 0.5f;
    private int tapCount = 0;
    // Update is called once per frame
    void FixedUpdate () {

        #if UNITY_EDITOR

        if (Input.GetKeyUp(KeyCode.RightArrow)||Input.GetKeyUp(KeyCode.LeftArrow)) {
            if(delay > 0f && tapCount == 1) {
                if (OnJumpTouched != null) {
                    OnJumpTouched();
                }
            } else {
                delay = 0.5f;
                tapCount += 1;
            }
        }

        if(delay > 0) {
            delay -= 1 * Time.deltaTime;
        } else {
            tapCount = 0;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (OnRightScreenTouchStationary != null)
            {
                OnRightScreenTouchStationary();
            }
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (OnLeftScreenTouchStationary != null)
            {
                OnLeftScreenTouchStationary();
            }
        }
        #endif

        #if UNITY_ANDROID
        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).tapCount == 2) {
                if (OnJumpTouched != null) {
                    OnJumpTouched();
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                if(Input.GetTouch(0).position.x >= Screen.width / 2)
                {
                    if (OnRightScreenTouchStationary != null)
                    {
                        OnRightScreenTouchStationary();
                    }
                }
                else
                {
                    if (OnLeftScreenTouchStationary != null)
                    {
                        OnLeftScreenTouchStationary();
                    }
                }
            }
        }
        #endif
    }
}