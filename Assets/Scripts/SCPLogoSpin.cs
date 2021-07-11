using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCPLogoSpin : MonoBehaviour
{
    float speed = 100f;

    // Start is called before the first frame update
    private void Update()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }
}
