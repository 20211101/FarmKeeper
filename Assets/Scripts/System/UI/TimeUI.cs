using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    Timer timer;
    [SerializeField]
    TextMeshProUGUI timeText;

    private void Update()
    {
        if (timer.IsDay)
            timeText.text = $"[DAY {timer.DAY_CNT + 1}]\nMORNIG {timer.MORNING_TIME - timer.CUR_DAY_TIME:F0}";
        else
            timeText.text = $"[DAY {timer.DAY_CNT + 1}]\nNIGHT {timer.DAY_FULL_TIME - timer.CUR_DAY_TIME:F0}";

    }
}
