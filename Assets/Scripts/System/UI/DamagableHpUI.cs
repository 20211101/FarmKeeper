using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamagableHpUI : MonoBehaviour
{
    [SerializeField]
    Image hpBar;

    [SerializeField]
    Damagable monster;

    private void Update()
    {
        hpBar.fillAmount = (float)monster.Hp / (float)monster.MaxHp;
    }
}
