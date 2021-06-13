using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldAIJumpScareController : MonoBehaviour
{
    RawImage oldAI;
    void Start()
    {
        oldAI = GetComponent<RawImage>();
        StartCoroutine(OldAIJumpscareTimer());
    }

    public IEnumerator OldAIJumpscareTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            int chance = Random.Range(0, 100);
            Debug.Log($"OldAI rolled a {chance}");
            if (chance <= 50)
            {
                oldAI.enabled = true;
                yield return new WaitForSeconds(0.5f);
                oldAI.enabled = false;
            }
            
        }
    } 
}
