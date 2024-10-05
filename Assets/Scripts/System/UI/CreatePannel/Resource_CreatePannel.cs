using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Resource_CreatePannel : MonoBehaviour
{
    [SerializeField]
    Resource.EType type;
    [SerializeField]
    TextMeshProUGUI cntTxt;

    public void ChangeCnt(int cnt)
    {
        cntTxt.text = cnt.ToString();
    }
}
