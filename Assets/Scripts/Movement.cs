using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speedIngagement = 1;
    public void Slow()
    {
        StopCoroutine(nameof(UnSlow));
        speedIngagement = 0.5f;
        StartCoroutine("UnSlow");
    }

    IEnumerator UnSlow()
    {
        yield return new WaitForSeconds(0.5f);
        speedIngagement = 1;
    }

}
