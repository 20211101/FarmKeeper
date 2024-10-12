using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damagable
{
    MeshRenderer[] mesh;
    int MAXLife = 5;
    Animator anim;
    int curLife;

    private void Awake()
    {
        curLife = MAXLife;
        anim = GetComponent<Animator>();
    }


    bool isIntentity = false;
    public bool isFallen = false;
    public override void Damaged(DamageInfo damage)
    {
        if (isIntentity == true) return;

        hp -= damage.damage;
        PlayerHPUI.instance.ChangeHPUIInfo(this);
        if(hp <= 0)
        {
            isIntentity = true;
            --curLife;
            PlayerHPUI.instance.LifeCnt = curLife;
            if (curLife <= 0)
            {
                GameManager.instance.GameOver();
            }
            else
            {
                isFallen = true;
                anim.SetLayerWeight(5, 1);
                anim.SetTrigger("Dead");
                anim.SetTrigger("GetUp");
            }

        }
    }

    public void WakeUp()
    {
        hp = MaxHp;
        PlayerHPUI.instance.ChangeHPUIInfo(this);
        isFallen = false;
        isIntentity = false;
        anim.SetLayerWeight(5, 0);
    }

}
