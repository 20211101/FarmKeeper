using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Club_HitBox : MonoBehaviour
{
    PlayerInventory playerInventory;
    int damage = 100;
    private void Awake()
    {
        playerInventory = GetComponentInParent<PlayerInventory>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("¶§·È´ç");
        if(other.tag == "Monster")
        {
            Monster m = other.GetComponent<Monster>();
            m.Damaged(damage);
        }
        if(other.tag == "ResourceOrigin")
        {
            ResourceOrigin r = other.GetComponent<ResourceOrigin>();
            r.Damaged(playerInventory);
        }
    }
}
