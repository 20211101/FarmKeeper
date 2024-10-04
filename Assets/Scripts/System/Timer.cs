using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : ÇöÀç ³·/¹ãÀÎÁö¿Í ½Ã°£ÀÇ Èå¸§ UI·Î ¶ç¿ì±â
public class Timer : MonoBehaviour
{
    private static Timer _instance = null;
    public static Timer instance => _instance;


    [SerializeField]
    const float MORNING_TIME = 120.0f;
    [SerializeField]
    const float NIGHT_TIME = 120.0f;

    public delegate void DayStart();
    public delegate void NightStart();
    public event DayStart dayStart;
    public event NightStart nightStart;

    private bool isStartGame = false;
    private bool isDay = true;
    private float curTime;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public void StartInGameTime()
    {
        curTime = MORNING_TIME;

        isStartGame = true;

        isDay = true;
    }

    public void ResetTimer()
    {
        isStartGame = false;
        isDay = true;
        curTime = MORNING_TIME;

        dayStart = null;
        nightStart = null;
    }

    void Update()
    {
        if (isStartGame == false) return;

        curTime -= Time.deltaTime;
        if(curTime <= 0)
        {
            ChangeDayOrNight();
        }
    }

    private void ChangeDayOrNight()
    {
        if (isDay)
        {
            isDay = false;
            curTime = NIGHT_TIME;
            nightStart();
        }
        else
        {
            isDay = true;
            curTime = MORNING_TIME;
            dayStart();
        }
    }
}
