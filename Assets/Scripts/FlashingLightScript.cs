using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLightScript : MonoBehaviour
{
    [SerializeField] GameObject _light;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("flash");
    }

    private IEnumerator flash()
    {
        bool state = true;
        while (true)
        {
            state = state ? false : true;
            _light.SetActive(state);
            float randomTime = Random.Range(0.1f, 3f);
            yield return new WaitForSeconds(randomTime);
        }
    }
}
