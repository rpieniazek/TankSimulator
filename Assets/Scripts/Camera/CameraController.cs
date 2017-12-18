using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    [SerializeField] List<Camera> cameras = new List<Camera>();

    [Space(10)]
    [SerializeField]
    Vector2 cameraPreviewSize = new Vector2(0.2f, 0.3f);
    private bool showPreviews = true;
    private int activeCameraIndex = 0;

    [Header("Nowy system kamer")]
    public TankCameras tankCameras;
    public FreeCamera freeCamera;
    public FirstPersonController fpsCamera;
    RearWheelDrive rearWheel;

    private int currentCamera = 1; 

    private void Start()
    {

        cameras.RemoveAll(x => x == null);
        if(cameras.Count != 0 )
        {
            cameras[0].GetComponent<CameraOrbitDrag>().CanMove = true;
            DisplayCamera(0);
            return;
        }


        rearWheel = tankCameras.gameObject.GetComponent<RearWheelDrive>();


        currentCamera = 2;
        SwitchMainCamera();
    }

    void Update()
    {
        if(Input.GetButtonDown("Switch1") && cameras.Count == 0)
        {
            SwitchTankCamera();
        }
        if (Input.GetButtonDown("Switch2") && cameras.Count == 0)
        {
            SwitchMainCamera();
        }

        if(cameras.Count == 4)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DisplayCamera(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                DisplayCamera(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                DisplayCamera(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                DisplayCamera(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                showPreviews = !showPreviews;
                DisplayCamera(activeCameraIndex);
            }
        }

    }

    private void SwitchTankCamera()
    {
        tankCameras.SwitchCamera();
    }
    private void SwitchMainCamera()
    {

        if(currentCamera == 0)
        {
            currentCamera++;
            tankCameras.SwitchOff();
            

            freeCamera.CanMove = true;
            freeCamera.gameObject.SetActive(true);

            fpsCamera.CanMove = false;
            fpsCamera.gameObject.SetActive(false);

            rearWheel.CanMove = true;
        }
        else if(currentCamera == 1)
        {
            currentCamera++;
            tankCameras.SwitchOff();

            freeCamera.CanMove = false;
            freeCamera.gameObject.SetActive(false);

            fpsCamera.CanMove = true;
            fpsCamera.gameObject.SetActive(true);

            rearWheel.CanMove = false;
        }
        else if(currentCamera == 2)
        {
            currentCamera = 0;
            tankCameras.SwitchOn();

            freeCamera.CanMove = false;
            freeCamera.gameObject.SetActive(false);

            fpsCamera.CanMove = false;
            fpsCamera.gameObject.SetActive(false);

            rearWheel.CanMove = true;
        }
    }


    /// <summary>
    /// Uaktywnia określoną kamerę i podglądy
    /// </summary>
    private void DisplayCamera(int cameraIndex)
    {
        if (cameraIndex >= cameras.Count) return;

        activeCameraIndex = cameraIndex;

        int previews = 0; 


        for(int i = 0; i < cameras.Count; i++)
        {
            bool main = cameraIndex == i ? true : false;
            bool active = main || showPreviews ? true : false;
            if (main)
            {
                cameras[i].rect = new Rect(Vector2.zero, Vector2.one);
                cameras[i].depth = 0;
            }
            else if (showPreviews)
            {
                if (cameras[i] == null) continue;

                cameras[i].rect = new Rect(new Vector2(0, cameraPreviewSize.y * previews), cameraPreviewSize);
                previews++;
                cameras[i].depth = previews;
            }
            cameras[i].gameObject.SetActive(active);
        }
    }

}


