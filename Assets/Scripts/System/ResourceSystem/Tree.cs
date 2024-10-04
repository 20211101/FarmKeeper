using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ResourceOrigin
{
    Resource r = new Resource(Resource.EType.Wood, 5);
    public override void Damaged(PlayerInventory playerInventory)
    {
        Debug.Log("¾Æ¾æ");
        playerInventory.GetResource(r);
        StartCoroutine(nameof(ScaleMove));
    }

    IEnumerator ScaleMove()
    {
        while(transform.localScale.x < 1.2f)
        {
            transform.localScale += new Vector3(0.01f, 0, 0);
            yield return null;
        }
        while(transform.localScale.x > 1f)
        {
            transform.localScale -= new Vector3(0.01f, 0, 0);
            yield return null;
        }
            transform.localScale = new Vector3(1, 1, 1);

    }
}
