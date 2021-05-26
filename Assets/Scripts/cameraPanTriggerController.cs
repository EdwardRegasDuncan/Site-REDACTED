using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cameraPanTriggerController : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject _camera;
    [SerializeField] int positionValue;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _camera.GetComponent<CameraController>().panCameraToPosition(positionValue);
    }
}
