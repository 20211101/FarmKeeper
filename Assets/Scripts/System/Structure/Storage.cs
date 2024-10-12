using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Structure
{
    public override void Damaged(DamageInfo damage)
    {
        base.Damaged(damage);
        StorageHPUI.instance.UpdateUI();
    }

    public override void Destroying()
    {
        base.Destroying();
        StorageHPUI.instance.UpdateUI();
        GameManager.instance.GameOver();
    }
    void Start()
    {
        MAX_HP = 200;
        hp = MAX_HP;
    }
}
