using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpUI : MonoBehaviour
{
    [SerializeField]
    Image hpBar;
    
    [SerializeField]
    Monster monster;

    private void Update()
    {
        hpBar.fillAmount = (float)monster.Hp / (float)monster.MaxHp;
    }
}
