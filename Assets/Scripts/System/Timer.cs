using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO : 현재 낮/밤인지와 시간의 흐름 UI로 띄우기
public class Timer : MonoBehaviour
{
    private static Timer _instance = null;
    public static Timer instance => _instance;

    [SerializeField] Light sun;
    
    public float MORNING_TIME = 12.0f;
    
    public float NIGHT_TIME = 120.0f;

    public float DAY_FULL_TIME => MORNING_TIME + NIGHT_TIME;
    public int DAY_CNT => (int)absTime / (int)DAY_FULL_TIME;
    public float CUR_DAY_TIME => absTime % DAY_FULL_TIME;

    public delegate void DayStart();
    public delegate void NightStart();
    public event DayStart dayStart;
    public event NightStart nightStart;

    private bool isStartGame = false;
    private bool isDay = true;
    public bool IsDay { get => isDay; private set { } }
    private float absTime;
    Material skyboxMornig;
    [SerializeField]
    Material skyboxNight;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        skyboxMornig = RenderSettings.skybox;
    }

    public void StartInGameTime()
    {
        absTime = 0;

        isStartGame = true;

        isDay = true;
    }

    public void ResetTimer()
    {
        isStartGame = false;
        isDay = true;
        absTime = MORNING_TIME;

        dayStart = null;
        nightStart = null;
    }

    void Update()
    {
        if (isStartGame == false) return;

        absTime += Time.deltaTime;

        // 돌리는 데, 하루 시간 => 365도로 치환
        // CURTIME * (365 / DAYTIME)
        // Lerp 갈기면 되지 않을까..
        sun.transform.rotation = Quaternion.Euler(Mathf.Lerp(0f, 365f, (CUR_DAY_TIME / DAY_FULL_TIME))
            , sun.transform.rotation.y, sun.transform.rotation.z);
        CheckDayOrNight();
    }

    private void CheckDayOrNight()
    {
        if (CUR_DAY_TIME > MORNING_TIME)
        {
            if (isDay != false)
            {
                RenderSettings.skybox = skyboxNight;
                RenderSettings.fogDensity = 0.03f;
                isDay = false;
                nightStart();
            }
        }
        else
        {
            if (isDay == false)
            {
                RenderSettings.skybox = skyboxMornig;
                RenderSettings.fogDensity = 0f;
                isDay = true;
                dayStart();
            }
        }
    }
}
