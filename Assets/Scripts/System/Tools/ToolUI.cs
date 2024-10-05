using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ToolUI : MonoBehaviour
{
    public static Color selected = Color.green;
    public static Color unSelected = Color.black;

    [SerializeField]
    Image backGroundImg;
    [SerializeField]
    Image toolImg;
    [SerializeField]
    TextMeshProUGUI cntTxt;

    public Image BackGroundImg {get => backGroundImg; private set { } }
    public Image ToolImg { get => toolImg; private set { } }
    public TextMeshProUGUI CntTxt { get => cntTxt; private set { } }

    public void ChangeUnSelected()
    {
        StopCoroutine(nameof(ReactRed));
        backGroundImg.color = unSelected;
    }
    public void ChangeSelected()
    {
        backGroundImg.color = selected;
    }

    public void CantChangeReaction()
    {
        StopCoroutine(nameof(ReactRed));
        StartCoroutine(nameof(ReactRed));
    }

    IEnumerator ReactRed()
    {
        backGroundImg.color = Color.red;

        for(int i = 255; i > 0; i--)
        {
            backGroundImg.color = new Color(i/(float)255, 0, 0);
            yield return null;
        }

        backGroundImg.color = Color.black;
    }
}
