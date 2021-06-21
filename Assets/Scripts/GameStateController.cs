using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] GameObject SCP173;
    [SerializeField] GameObject deathScreenUI;
    GameObject causeOfDeath;


    enum GameState
    {
        PreGame,
        MainGame,
        EndGame,
        PlayerDeath
    }

    GameState state;

    private void Start()
    {
        state = GameState.MainGame;
        SCP173.SetActive(true);
        deathScreenUI.SetActive(false);
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.PlayerDeath:
                SCP173.SetActive(false);
                deathScreenUI.SetActive(true);
                break;
        }
    }

    public void PlayerDeath(GameObject cause)
    {
        causeOfDeath = cause;
        state = GameState.PlayerDeath;
    }
}
