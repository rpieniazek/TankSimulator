using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankCameras : MonoBehaviour {

    public MouseOrbitImproved mainCamera;
    public Camera driver;
    public Camera barrel;


    public int activeCamera;

    public void SwitchCamera()
    {
        if (activeCamera == -1) return;
        if(activeCamera == 0)
        {
            activeCamera++;
            SetCamerasActive(false, true, false);
        }
        else if (activeCamera == 1)
        {
            activeCamera++;
            SetCamerasActive(false, false, true);
        }
        else if (activeCamera == 2)
        {
            activeCamera = 0;
            SetCamerasActive(true, false, false);
        }
    }

    public void SwitchOff()
    {
        activeCamera = -1;
        SetCamerasActive(false, false, false);
    }

    public void SwitchOn()
    {
        activeCamera = 0;
        mainCamera.CanMove = true;

        SetCamerasActive(true, false, false);

    }

    private void SetCamerasActive(bool main, bool drive, bool barr)
    {
        mainCamera.CanMove = main;
        mainCamera.gameObject.SetActive(main);
        driver.gameObject.SetActive(drive);
        barrel.gameObject.SetActive(barr);
    }

}
