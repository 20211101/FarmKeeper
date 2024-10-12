using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHPUI : MonoBehaviour
{
    private static PlayerHPUI _instance;
    public static PlayerHPUI instance { get => _instance; set { } }

    [SerializeField]
    GameObject[] lifeImg;
    public int LifeCnt
    {
        set
        {
            for (int i = 0; i < lifeImg.Length; ++i)
                lifeImg[i].SetActive(false);
            for (int i = 0; i < value; ++i)
                lifeImg[i].SetActive(true);
        }
    }
    public void ChangeHPUIInfo(PlayerHealth p)
    {
        sliderFill.fillAmount = p.Hp / (float)p.MaxHp;
        hpTxt.text = $"{p.Hp}/{p.MaxHp}";
    }

    [SerializeField]
    Image sliderFill;
    [SerializeField]
    TextMeshProUGUI hpTxt;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }
}
