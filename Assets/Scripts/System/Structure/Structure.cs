using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 구조물(집, 터렛)
public class Structure : Damagable
{
    public BuildArea builtedArea;
    public override void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            if (builtedArea != null)
                builtedArea.state = BuildArea.State.emety;
            gameObject.SetActive(false);
        }
    }
}
