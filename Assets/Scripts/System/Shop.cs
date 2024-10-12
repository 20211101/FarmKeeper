using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    PlayerInventory inventory;


    private bool CanBuy(Resource[] cost)
    {
        foreach (Resource i in cost)
        {
            if (inventory.resources[(int)i.type].cnt < i.cnt)
            {
                return false;
            }
        }
        return true;
    }
}
