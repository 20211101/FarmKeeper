using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// 게임의 시작과 끝을 관리
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { get => _instance; private set { } }
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject Level;
    [SerializeField]
    GameObject GameoverPannel;
    [SerializeField]
    GameObject WinPannel;
    Timer timer;

    public delegate void GameClear();
    public event GameClear gameClear;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        timer = Timer.instance;
        Player.SetActive(false);
    }

    public void GameStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        timer.StartInGameTime();
        Player.SetActive(true);
    }

    public void GameOver()
    {
        Player.SetActive(false);
        timer.ResetTimer();
        GameoverPannel.SetActive(true);

        Cursor.lockState = CursorLockMode.Confined;
    }
    public void Win()
    {
        Player.SetActive(false);
        timer.ResetTimer();
        WinPannel.SetActive(true);
        if(gameClear != null)
            gameClear();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
