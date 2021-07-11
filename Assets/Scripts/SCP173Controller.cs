using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP173Controller : MonoBehaviour
{

    [SerializeField] GameObject gameStateController;
    public GameObject securityCameras;
    [SerializeField] float _movementOpportunityDelay = 5f; //time before next movement desision
    public GameObject _currentPosition;
    [SerializeField] bool canMove = false;
    [SerializeField] int moveWeight = 0; //increases chance of movement each time movement check is failed
    [SerializeField] public float scp173AngerCap = 50; //set when anger mechanic triggers attack state
    [SerializeField] public float scp173Anger = 0; //current anger value
    [SerializeField] float scp173AngerRate = 10; //how much anger increases per second while 173 is being watched
    [SerializeField] public int AiDifficulty = 5; //0 = inactive, 20 = always moves

    [SerializeField] float waitTime = 20f;

    public GameObject originPosition;
    [SerializeField] bool watchLock;
    [SerializeField] bool attackmode;

    [SerializeField] float currentPlayerCameraPosition;

    //state machine
    public enum _states
    {
        Idle,
        Attacking,
        InOffice,
        Captured,
        Waiting,
        Watched
    }

    public _states state;
    _states casheState;

    // Objects that block movement
    public GameObject C0_Door;

    private void Start()
    {
        casheState = state = _states.Waiting;
        StartCoroutine(MovementOppotunity());
        StartCoroutine(WaitTimer());
    }

    void Update()
    {
        state = Check173IsVisibleToPlayer(state);

        if (scp173Anger == scp173AngerCap && state != _states.Watched)
        {
            state = _states.Attacking;
        }

        switch (state)
        {
            case _states.Idle:
                if (canMove && !watchLock)
                {
                    if (MoveCheck())
                    {
                        Move();
                    }

                    canMove = false;
                }
                break;
            case _states.Attacking:
                if (canMove && !watchLock)
                {
                    AttackMove();
                    canMove = false;
                }
                break;
            case _states.InOffice:
                break;
            case _states.Captured:
                break;
            case _states.Waiting:
                break;
            case _states.Watched:
                if (!watchLock)
                {
                    StartCoroutine(WatchedLock());
                }
                scp173Anger += scp173AngerRate * Time.deltaTime;
                scp173Anger = Mathf.Clamp(scp173Anger, 0, scp173AngerCap);
                break;
        }
    }

    private void Move()
    {
        _currentPosition.GetComponent<PositionRelation>().RemoveSCPFromThisLocation(GetComponent<GameObject>());

        moveWeight = 0;

        _currentPosition = _currentPosition.GetComponent<PositionRelation>().determineNextPosition();
        transform.position = _currentPosition.GetComponent<Transform>().position;
        transform.rotation = _currentPosition.GetComponent<Transform>().rotation;

        Debug.Log($"At Postion {_currentPosition.name}");

        _currentPosition.GetComponent<PositionRelation>().AddSCPToThisLocation(GetComponent<GameObject>());
    }

    private IEnumerator MovementOppotunity()
    {
        while (true)
        {
            yield return new WaitForSeconds(_movementOpportunityDelay);
            canMove = true;
        }
    }

    private bool MoveCheck()
    {
        int random = Random.Range(1, 21) + moveWeight;
        if (random < AiDifficulty)
        {
            Debug.Log($"Can Move {random} < {AiDifficulty} ({moveWeight})");
            return true;
        }
        else
        {
            moveWeight--;
            Debug.Log($"Decided not to move {random} < {AiDifficulty} ({moveWeight + 1})");
            return false;
        }
    }
    private IEnumerator WatchedLock()
    {
        watchLock = true;
        yield return new WaitForSeconds(2);
        watchLock = false;
    }

    private void AttackMove()
    {
        GameObject nextposition = _currentPosition.GetComponent<PositionRelation>()._directionToPlayer;

        if (_currentPosition.name == "C1")
        {
            //check if security door is open.
            bool securityDoorOpen = C0_Door.GetComponent<OfficeDoorController>()._doorState;
            Debug.Log($"Door is {securityDoorOpen}");
            if (!securityDoorOpen)
            {
                transform.position = nextposition.GetComponent<Transform>().position;
                transform.rotation = nextposition.GetComponent<Transform>().rotation;
                gameStateController.GetComponent<GameStateController>().PlayerDeath(GetComponent<GameObject>());
            }
            else
            {
                transform.position = originPosition.GetComponent<Transform>().position;
                transform.rotation = originPosition.GetComponent<Transform>().rotation;
                _currentPosition = originPosition;
                Debug.Log("Returned to idle");
                scp173Anger = 0;
                state = _states.Idle;
            }
        }
        else
        {
            transform.position = nextposition.GetComponent<Transform>().position;
            transform.rotation = nextposition.GetComponent<Transform>().rotation;
            _currentPosition = nextposition;
        }
    }

    private _states Check173IsVisibleToPlayer(_states currentState)
    {
        
        if (currentState != _states.Watched)
        {
            casheState = state;
        }

        //check if 173 is on current camera and player can see monitor
        string activeCameraName = securityCameras.GetComponent<SecurityCameraController>().activeCamera.name;
        currentPlayerCameraPosition = Camera.main.transform.localEulerAngles.y;
        bool playerLookingAtMonitor = (currentPlayerCameraPosition > 285 || currentPlayerCameraPosition < 75) ? true : false;
        if (activeCameraName == _currentPosition.name)
        {
            if (playerLookingAtMonitor)
            {
                return _states.Watched;
            }
        }

        //check if 173 is watched directly

        return casheState;
    }

    private IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(waitTime);
        state = _states.Idle;
    }

}
 