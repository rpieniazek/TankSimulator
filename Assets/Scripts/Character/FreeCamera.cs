using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour {

    public float flySpeed = 10f;

    [Header("Movement")]
    public bool CanMove = true;

    // Kierunek przedni
    public Vector3 direction = Vector3.zero;
    // Poruszanie względem Y
    public Vector3 yDirection = Vector3.zero;

    Vector3 rot;

    [Header("Camera")]
    [SerializeField]
    Transform cameraTransform;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;

    public float MinimumX = -90F;
    public float MaximumX = 90F;

    //private Quaternion m_CharacterTargetRot;
   // private Quaternion m_CameraTargetRot;

    private void Start()
    {
     //   m_CharacterTargetRot = transform.localRotation;
    //    m_CameraTargetRot = cameraTransform.localRotation;
        rot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update () {

        if(CanMove)
        {
            RotateView();

            transform.Translate(Vector3.forward * flySpeed * Input.GetAxis("Vertical") * Time.deltaTime);
            transform.Translate(Vector3.right * flySpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
        }
       
    }


    private void RotateView()
    {

        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        rot.y += yRot;
        rot.x -= xRot;

        if (rot.x < -90)
        {
            rot.x = -90;
        }
       
        if (rot.x > 90)
        {
            rot.x = 90;
        }

        transform.eulerAngles = rot;
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    void OnDrawGizmos()
    {
        direction = new Vector3(-(cameraTransform.position.x - transform.position.x), 0, -(cameraTransform.position.z - transform.position.z));
        direction.Normalize();
        Vector3 dir_hor = new Vector3(direction.z, 0, -direction.x);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.up * 20);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 20);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, 5 * direction);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, 5 * dir_hor);
    }
}
