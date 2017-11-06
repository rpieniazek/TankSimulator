using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    [SerializeField] List<Camera> cameras = new List<Camera>();

    [Space(10)]
    [SerializeField] Vector2 cameraPreviewSize = new Vector2(0.2f, 0.3f);

    private bool showPreviews = true;
    private int activeCameraIndex = 0;

    private void Start()
    {
        DisplayCamera(0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
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
            showPreviews = !showPreviews;
            DisplayCamera(activeCameraIndex);
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
                cameras[i].rect = new Rect(new Vector2(0, cameraPreviewSize.y * previews), cameraPreviewSize);
                previews++;
                cameras[i].depth = previews;
            }
            cameras[i].gameObject.SetActive(active);
        }
    }

}


