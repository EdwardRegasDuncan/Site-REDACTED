using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyController : MonoBehaviour
{

    

    [SerializeField] float _movementOpportunityDelay = 5f; //time before next movement desision
    [SerializeField] GameObject _currentPosition;
    [SerializeField] bool canMove = true;
    [SerializeField] int moveWeight = 0;
    int AiDifficulty = 5; //0 = inactive, 20 = always moves
    public bool shutDownAi = false;

    [SerializeField] GameObject originPosition; //test variable to reset 173 after he reaches the Security Room

    //state machine
    string[] _states =
    {
        "Wander",
        "Attack",
        "Blocked"
    };
    string _currentState;

    // Objects that block movement
    public GameObject C0_Door;

    void Update()
    {
        



        if (shutDownAi)
        {
            StopAllCoroutines();
        }
    }

    private void Start()
    {
        _currentState = _states[0];


        StartCoroutine("movementCheck");
    }

    private bool canMoveToLocation(Transform location)
    {
        switch (location.name)
        {
            case "C0":
                {
                    // if _doorState is true then door is closed
                    return C0_Door.GetComponent<OfficeDoorController>()._doorState ? false : true;
                }
            default:
                {
                    return false;
                }
        }
    }

    private IEnumerator movementCheck()
    {
        while (true)
        {
            if (canMove)
            {
                int random = Random.Range(1, 21) + moveWeight;
                if (random < AiDifficulty)
                {
                    _currentPosition = _currentPosition.GetComponent<PositionRelation>().determineNextPosition();
                    transform.position = _currentPosition.GetComponent<Transform>().position;
                    transform.rotation = _currentPosition.GetComponent<Transform>().rotation;
                    Debug.Log($"Moved to {_currentPosition.name} {random} < {AiDifficulty} ({moveWeight})");
                    moveWeight = 0;
                    if(_currentPosition.name == "C0")
                    {
                        canMove = false;
                        Debug.Log("SCP-173 kills player");
                    }
                }
                else
                {
                    moveWeight--;
                    Debug.Log($"Decided not to move {random} < {AiDifficulty} ({moveWeight + 1})");
                }
            }
            else
            {
                transform.position = originPosition.GetComponent<Transform>().position;
                transform.rotation = originPosition.GetComponent<Transform>().rotation;
                _currentPosition = originPosition;
                canMove = true;
            }
            yield return new WaitForSeconds(_movementOpportunityDelay);
        }
    }
}
