using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickUp : MonoBehaviour
{
    [SerializeField]
    public Resource resource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null && resource != null)
            {
                playerInventory.GetResource(resource); // 자원 인벤토리에 추가
                Debug.Log($"{resource.type} 자원이 인벤토리에 추가되었습니다.");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("자원 또는 플레이어 인벤토리가 null입니다.");
            }
        }
    }
}
