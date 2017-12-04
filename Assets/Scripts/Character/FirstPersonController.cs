using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

    [Header("Movement")]
    public float speed = 5f;

    private CharacterController characterController;
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
    //    characterController = GetComponent<CharacterController>();

        m_CharacterTargetRot = transform.localRotation;
        m_CameraTargetRot = cameraTransform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {

        RotateView();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

       

        Vector3 desiredMove = transform.forward * vertical + transform.right * horizontal;
        Vector3 moveDir = Vector3.zero;
        moveDir.x = desiredMove.x * speed;
        moveDir.z = desiredMove.z * speed;

        transform.Translate(horizontal * speed * Time.deltaTime, 0 , vertical * speed * Time.deltaTime);

       // characterController.Move(m_MoveDir * Time.deltaTime);

       
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
