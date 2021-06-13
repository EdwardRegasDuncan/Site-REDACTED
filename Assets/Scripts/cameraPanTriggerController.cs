using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cameraPanTriggerController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject _camera;
    [SerializeField] int positionValue;
    Coroutine pan;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _camera.GetComponent<CameraController>().BeginPanCameraToPosition(positionValue);

    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        _camera.GetComponent<CameraController>().CancelPanCameraToPosition();
    }
}
