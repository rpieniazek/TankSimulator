using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

    [Header("Movement")]
    public bool CanMove = true;
    #region Movement vars
    public float speed = 8f;
    public float SpeedRun = 12f;
    public float JumpSpeed = 20f;
    public float RotateSpeed = 10f;
    public float gravity = 20f;
    #endregion

    // Kierunek przedni
    public Vector3 direction = Vector3.zero;
    // Poruszanie względem Y
    public Vector3 yDirection = Vector3.zero;
    // kamera
    public Camera cam;

    // Szybkość poruszania 
    float speed_ver;
    float speed_hor;
    // Kierunek boczny
    Vector3 dir_hor;
    CharacterController controller;

    [Header("Camera")]
    [SerializeField] Transform cameraTransform;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;

    public float MinimumX = -90F;
    public float MaximumX = 90F;

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;


    // Use this for initialization
    void Start () {

        m_CharacterTargetRot = transform.localRotation;
        m_CameraTargetRot = cameraTransform.localRotation;
        controller = GetComponent<CharacterController>();
    }
	
    void Update()
    {
     

        RotateView();
        if (CanMove)
        {
            // Poruszanie WSAD
            //    direction = new Vector3(-(cam.transform.position.x - transform.position.x), 0, -(cam.transform.position.z - transform.position.z));
            direction = transform.forward;
            direction.y = 0;
            direction.Normalize();
            float speed_ver = speed * Input.GetAxis("Vertical");
            float speed_hor = speed * Input.GetAxis("Horizontal");

            dir_hor = new Vector3(direction.z, 0, -direction.x);
            dir_hor.Normalize();

            // Szybkość poruszania się jest ograniczona do wartości Speed
            Vector3 moveDirection = direction * speed_ver + dir_hor * speed_hor;
            if (moveDirection.magnitude > speed)
            {
                moveDirection.Normalize();
                moveDirection *= speed;
            }
            // Poruszanie względem Y
            if (controller.isGrounded)
            {
                yDirection.y = 0;
                if (Input.GetButton("Jump"))
                {
                    yDirection.y = JumpSpeed;
                }
            }
            else
                yDirection.y -= gravity * Time.deltaTime;

            if ((controller.collisionFlags & CollisionFlags.Above) != 0)
            {
                if (yDirection.y > 0)
                    yDirection.y = 0;
            }
            controller.Move((moveDirection + yDirection) * Time.deltaTime);

        }

    }

    void OnDrawGizmos()
    {
        direction = new Vector3(-(cam.transform.position.x - transform.position.x), 0, -(cam.transform.position.z - transform.position.z));
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

    private void RotateView()
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        transform.localRotation = m_CharacterTargetRot;
        cameraTransform.localRotation = m_CameraTargetRot;
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
}
