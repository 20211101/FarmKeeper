using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 구조물(집, 터렛)
public class Structure : Damagable
{
    public BuildArea builtedArea;
    public override void Damaged(DamageInfo damage)
    {
        hp -= damage.damage;
        if (hp <= 0)
        {
            Destroying();
        }
    }

    public virtual void Destroying()
    {
        if (builtedArea != null)
            builtedArea.state = BuildArea.State.emety;
        gameObject.SetActive(false);
    }
}
