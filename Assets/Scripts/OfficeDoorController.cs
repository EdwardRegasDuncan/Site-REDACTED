using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeDoorController : MonoBehaviour
{
    [SerializeField] GameObject _door;
    public bool _doorState = false; //false = door open
    [SerializeField] Transform _doorClosedPos;
    [SerializeField] Transform _doorOpenPos;
    bool buttonEnabled = true;

    public void changeDoorPos()
    {
        if (buttonEnabled)
        {
            buttonEnabled = false;
            Transform targetPos = _doorState ? _doorOpenPos : _doorClosedPos;
            StartCoroutine(moveDoor(targetPos));
            _doorState = _doorState ? false : true;
        }
    }

    private IEnumerator moveDoor(Transform target)
    {
        while (Vector3.Distance(_door.transform.position, target.position) > 0.1)
        {
            _door.transform.position = Vector3.Lerp(_door.transform.position, target.position, 0.1f);
            yield return null;
        }
        buttonEnabled = true;
        
    }

}
