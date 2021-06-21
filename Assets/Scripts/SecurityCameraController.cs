using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraController : MonoBehaviour
{

    [SerializeField] GameObject[] _cameras;
    public GameObject activeCamera;

    private void Start()
    {
        foreach(GameObject cam in _cameras)
        {
            cam.SetActive(false);
        }
        switchCamera(1);
    }

    public void switchCamera(int camera)
    {
        foreach (GameObject cam in _cameras)
        {
            cam.SetActive(false);
        }

        activeCamera = _cameras[camera];
        activeCamera.SetActive(true);
    }

}
