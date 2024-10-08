using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHpCanvas : MonoBehaviour
{
    Transform target;
    private void OnEnable()
    {
        target = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(transform.position + (transform.position - target.position));
    }
}
