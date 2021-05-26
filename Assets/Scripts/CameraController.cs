using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject cameraPanTriggerLeft;
    [SerializeField] GameObject cameraPanTriggerRight;

    //hardcoded camera angles
    Quaternion[] cameraPositions = {
        Quaternion.Euler(0, 280, 0), //facing door
        Quaternion.Euler(0, 0, 0), //facing desk
        Quaternion.Euler(0, 80, 0) //facing window
    };

    float cameraTurnSpeed = 150f;
    int cameraPositionValue = 1;
    bool cameraInMotion;

    private void Update()
    {
        if (cameraInMotion)
        {
            cameraPanTriggerLeft.SetActive(false);
            cameraPanTriggerRight.SetActive(false);
        }
        else
        {
            cameraPanTriggerLeft.SetActive(cameraPositionValue == 0 ? false : true);
            cameraPanTriggerRight.SetActive(cameraPositionValue == 2 ? false : true);
        }
    }

    public void panCameraToPosition(int positionChange)
    {
        cameraPositionValue += positionChange;
        cameraPositionValue = Mathf.Clamp(cameraPositionValue, 0, 2);
        StartCoroutine("rotateCamera", cameraPositions[cameraPositionValue]);
    }
    public IEnumerator rotateCamera(Quaternion targetPosition)
    {
        cameraInMotion = true;
        while(Quaternion.Angle(transform.rotation, targetPosition) > 0.1)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetPosition, cameraTurnSpeed * Time.deltaTime);
            yield return null;
        }
        cameraInMotion = false;
    }

    public void endGame()
    {
        Application.Quit();
    }
}
