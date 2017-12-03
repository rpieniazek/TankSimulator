using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStabilization : MonoBehaviour {
    public float Kp = 200;
    public float Ki = 1;
    public float Kd = 0.1f;
    private float P, I, D;
    private float prevError;
    private float targetAngle;
    public Transform turret;
    private bool aimMode = false;
    private Vector3 targetVector;
    private Vector3 targetVector2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(!aimMode)
        {
            turret.localRotation *= Quaternion.Euler(0, 0, Input.GetAxis("Yaw"));
            targetVector = Vector3.ProjectOnPlane(turret.up, Vector3.right);
            targetVector2 = Vector3.ProjectOnPlane(turret.up, Vector3.forward);
            Debug.DrawLine(Vector3.zero, targetVector, Color.red);
            Debug.DrawLine(Vector3.zero, targetVector2, Color.red);


            //float angle = Vector3.Angle(turret.up, Vector3.forward);
            //float sign = Mathf.Sign(Vector3.Dot(turret.right, Vector3.forward));
            //float finalAngle = sign * angle;
            //targetAngle = finalAngle + 180;
            //print(targetAngle);
        }
        else
        {
            Vector3 diffrenceVector = Vector3.ProjectOnPlane(turret.up, Vector3.right) - targetVector;
            float angleDiff = Vector3.Angle(diffrenceVector, targetVector);
            print(angleDiff);
        }


        if (Input.GetKeyUp(KeyCode.T))
        {
            aimMode = !aimMode;
        }

    }

    public float Pid(float currentError, float deltaTime)
    {
        P = currentError;
        I += P * deltaTime;
        D = (P - prevError) / deltaTime;
        prevError = currentError;

        return P * Kp + I * Ki + D * Kd;
    }

}
