using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XController : MonoBehaviour {

    public bool rightStick;
    public bool leftStick;
    public bool xyabButtons;
    public bool directionalPad;
    public bool bumpers;
    public bool triggers;
    public bool mainButtons;
    public bool sticks;

    // Update is called once per frame
    void Update () {

        if(rightStick)
        {
            if (Input.GetAxis("Mouse X") != 0) Debug.Log("Mouse X");
            if (Input.GetAxis("Mouse Y") != 0) Debug.Log("Mouse Y");
        }
       
        if(leftStick)
        {
            if (Input.GetAxis("Horizontal") != 0) Debug.Log("Horizontal");
            if (Input.GetAxis("Vertical") != 0) Debug.Log("Vertical");
        }

        if(sticks)
        {
            if (Input.GetButtonDown("LeftStick")) Debug.Log("Left Stick");
            if (Input.GetButtonDown("RightStick")) Debug.Log("Right Stick");
        }

        if(xyabButtons)
        {
           // Input.GetKey
            if (Input.GetButtonDown("Fire1")) Debug.Log("A");
            if (Input.GetButtonDown("Fire2")) Debug.Log("B");
            if (Input.GetButtonDown("Fire3")) Debug.Log("X");
            if (Input.GetButtonDown("Jump")) Debug.Log("Y");
        }

        if (bumpers)
        {
            if (Input.GetButtonDown("bumperLeft")) Debug.Log("Bumper left");
            if (Input.GetButtonDown("bumperRight")) Debug.Log("Bumper right");
        }
        if (mainButtons)
        {
            if (Input.GetButtonDown("Back")) Debug.Log("Back");
            if (Input.GetButtonDown("Start")) Debug.Log("Start");
        }
        if (directionalPad)
        {
            if (Input.GetAxis("DPadX") != 0) Debug.Log("DPad X");
            if (Input.GetAxis("DPadY") != 0) Debug.Log("DPad Y");
        }
      
        if (triggers)
        {
            if (Input.GetAxis("LeftTrigger") != 0) Debug.Log("LeftTrigger");
            if (Input.GetAxis("RightTrigger") != 0) Debug.Log("RightTrigger");
        }
       


    }
}
