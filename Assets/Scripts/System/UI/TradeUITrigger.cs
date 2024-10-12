using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeUITrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject TradeUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false) return;
        TradeUI.SetActive(true);

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnTriggerExit(Collider other)
    {
        TradeUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
