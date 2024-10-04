using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    PlayerInventory inventory;

    ToolRecipy[] recipies = new ToolRecipy[]
    {
        new ToolRecipy("¸ùµÕÀÌ", Tool.type.Club, new Resource[]{new Resource(Resource.EType.Wood,10) })
    };

    public void Buy(Tool.type t)
    {
        if (CanBuy(recipies[(int)t].cost) == false) return;

        foreach (Resource i in recipies[(int)t].cost)
        {
            inventory.resources[(int)i.type].cnt -= i.cnt;
        }

        inventory.MakeTool(t);
    }

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
