using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStabilization : MonoBehaviour
{
    public float Kp = 350;
    public float Ki = 5;
    public float Kd = 0.1f;
    private float P, I, D;
    private float prevError;
    public Transform turret;

    private Vector3 targetVector;

    private bool aimMode = false;

    void Start()
    {
    }

    void Update()
    {
        if (!aimMode)
        {
            turret.localRotation *= Quaternion.Euler(0, 0, Input.GetAxis("Yaw"));
            float angle = Vector3.Angle(transform.forward, turret.right);
            targetVector = turret.right.normalized;
        }
        else
        {
            float dt = Time.deltaTime;
            float targetAngle = Vector3.Angle(targetVector, Vector3.right);
            float currentAngle = Vector3.Angle(turret.right.normalized, Vector3.right);
            float angleSide = Vector3.Dot(turret.right.normalized, Vector3.forward);
            float angleError = targetAngle - currentAngle;
            if (angleError < 5 && angleError > -5)
            {
                turret.Rotate(0, 0, 0);
            }
            else
            {
                if(angleSide > 0)
                {
                    turret.Rotate(0, 0, -Pid(angleError, dt) * dt * dt);
                }
                else
                {
                    turret.Rotate(0, 0, Pid(angleError, dt) * dt * dt);
                }

            }
        }


        if (Input.GetKeyUp(KeyCode.Y))
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
