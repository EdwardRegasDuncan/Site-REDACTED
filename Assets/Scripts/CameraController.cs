using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject cameraPanTriggerLeft;
    [SerializeField] GameObject cameraPanTriggerRight;
    [SerializeField] GameObject debugMenu;

    //hardcoded camera angles
    Quaternion[] cameraPositions = {
        Quaternion.Euler(0, 280, 0), //facing door
        Quaternion.Euler(0, 80, 0) //facing window
    };

    float cameraTurnSpeed = 100f;
    int cameraPositionValue = 1;
    bool cameraInMotion;

    Coroutine panCamera;

    private void Update()
    {

    }

    public void BeginPanCameraToPosition(int positionChange)
    {
        if (!cameraInMotion) 
        { 
            cameraPositionValue += positionChange;
            cameraPositionValue = Mathf.Clamp(cameraPositionValue, 0, 1);
            panCamera = StartCoroutine("rotateCamera", cameraPositions[cameraPositionValue]);
        }
        
    }
    public void CancelPanCameraToPosition()
    {
        StopCoroutine(panCamera);
        cameraInMotion = false;
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
