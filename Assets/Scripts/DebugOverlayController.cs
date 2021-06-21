using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugOverlayController : MonoBehaviour
{
    [SerializeField] GameObject UiElements;

    [SerializeField] GameObject scp173;
    [SerializeField] Text scp173State;
    [SerializeField] Text scp173Location;
    [SerializeField] Text scp173Anger;
    [SerializeField] Text scp173Difficulty;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            UiElements.SetActive(UiElements.activeInHierarchy ? false : true);
        }

        if (UiElements.activeInHierarchy)
        {
            SCP173Controller script = scp173.GetComponent<SCP173Controller>();
            scp173State.text = $"SCP 173 State: {script.state.ToString()}";
            scp173Location.text = $"SCP 173 Location: {script._currentPosition.name}";
            scp173Anger.text = $"SCP 173 Anger: {script.scp173Anger}/{script.scp173AngerCap}";
            scp173Difficulty.text = $"SCP 173 Difficulty: {script.AiDifficulty}/20";
        }
        
    }
}
