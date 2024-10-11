using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyFloor : Structure
{
    private void Awake()
    {
        Destroy(gameObject, 20);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster") == false) return;
        else
            other.GetComponent<Movement>().Slow();
    }
}
