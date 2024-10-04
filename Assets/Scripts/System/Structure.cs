using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 구조물(집, 터렛)
public class Structure : MonoBehaviour
{
    int hp = 100;

    public virtual void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            gameObject.SetActive(false);
    }
}
