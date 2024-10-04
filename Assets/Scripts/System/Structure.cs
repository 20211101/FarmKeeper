using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    int hp = 100;

    public void Damaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            gameObject.SetActive(false);
    }
}
