using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCP079JumpScareController : MonoBehaviour
{
    RawImage oldAI;
    void Start()
    {
        oldAI = GetComponent<RawImage>();
        StartCoroutine(JumpscareTimer());
    }

    public IEnumerator JumpscareTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            int chance = Random.Range(0, 100);
            if (chance <= 15)
            {
                oldAI.enabled = true;
                yield return new WaitForSeconds(0.5f);
                oldAI.enabled = false;
            }
        }
    } 
}
