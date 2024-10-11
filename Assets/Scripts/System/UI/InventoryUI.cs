using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    PlayerInventory playerInventory;
    [SerializeField]
    TextMeshProUGUI woodTxt;
    [SerializeField]
    TextMeshProUGUI stoneTxt;
    [SerializeField]
    TextMeshProUGUI stealTxt;
    [SerializeField]
    TextMeshProUGUI meatTxt;
    [SerializeField]
    TextMeshProUGUI leatherTxt;
    [SerializeField]
    TextMeshProUGUI coinTxt;
    [SerializeField]
    TextMeshProUGUI TXTPowderTxt;

    void Update()
    {
        if (playerInventory == null)
        {
            Debug.Log("플레이어 인벤토리 넣어야 함");
            return;
        }

        woodTxt.text = $"{playerInventory.resources     [(int)Resource.EType.Wood].cnt}";
        stoneTxt.text = $"{playerInventory.resources    [(int)Resource.EType.Stone].cnt}";
        stealTxt.text = $"{playerInventory.resources    [(int)Resource.EType.Steal].cnt}";
        meatTxt.text = $"{playerInventory.resources     [(int)Resource.EType.Meat].cnt}";
        leatherTxt.text = $"{playerInventory.resources  [(int)Resource.EType.Leather].cnt}";
        coinTxt.text = $"{playerInventory.resources  [(int)Resource.EType.Coin].cnt}";
        TXTPowderTxt.text = $"{playerInventory.resources  [(int)Resource.EType.TNTPowder].cnt}";
    }
}
