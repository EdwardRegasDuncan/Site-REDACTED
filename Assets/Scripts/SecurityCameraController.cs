using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraController : MonoBehaviour
{

    [SerializeField] GameObject[] _cameras;

    private void Start()
    {
        foreach(GameObject cam in _cameras)
        {
            cam.SetActive(false);
        }
    }

    public void switchCamera(int camera)
    {
        foreach (GameObject cam in _cameras)
        {
            cam.SetActive(false);
        }

        _cameras[camera].SetActive(true);
    }

}
