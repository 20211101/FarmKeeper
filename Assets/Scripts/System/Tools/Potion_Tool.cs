using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_Tool : Tool
{
    [SerializeField] GameObject healEffect;
    int healAmount = 20;
    private void Start()
    {
        toolCnt = 100;
    }

    public override void Action()
    {
        StopCoroutine("EffectOff");
        GetComponent<PlayerHealth>().Heal(healAmount);
        healEffect.SetActive(true);
        StartCoroutine("EffectOff");
        toolCnt--;
        if (toolCnt < 1)
        {
            PlayerHand.instance.ChangeTool(type.Club);
        }
    }

    IEnumerator EffectOff()
    {
        yield return new WaitForSeconds(0.5f);
        healEffect.SetActive(false);
    }

}
