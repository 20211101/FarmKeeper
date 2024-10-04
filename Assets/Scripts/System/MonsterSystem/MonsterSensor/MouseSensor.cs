using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSensor : MonoBehaviour
{
    Mouse mouse;
    MonsterMovement mouseMovement;
    private void Awake()
    {
        mouse = GetComponentInParent<Mouse>();
        mouseMovement = GetComponentInParent<MouseMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Turret" || other.tag == "Storage")
        {
            mouseMovement.StopMoving();
            mouse.StartAttack(other.GetComponent<Structure>());
        }
    }
}
