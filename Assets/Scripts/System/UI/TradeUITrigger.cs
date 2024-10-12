using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeUITrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject TradeUI;

    private void OnTriggerEnter(Collider other)
    {
        TradeUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        TradeUI.SetActive(false);
        
    }
}
