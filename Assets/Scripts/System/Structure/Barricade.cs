using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : Structure
{
    private void Awake()
    {
        MAX_HP = 200;
        hp = MAX_HP;
    }
}
