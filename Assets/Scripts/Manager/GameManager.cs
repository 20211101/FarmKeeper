using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject Level;
    Timer timer;

    public delegate void GameClear();
    public event GameClear gameClear;

    private void Start()
    {
        timer = Timer.instance;
        GameStart();
    }

    private void GameStart()
    {
        timer.StartInGameTime();
    }

    private void GameOver()
    {
        timer.ResetTimer();
        gameClear();
    }

}
