using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ResourceOrigin
{
    [SerializeField]
    private GameObject treeResourcePrefab; // 나무 자원 프리팹
    [SerializeField]
    private Transform spawnPoint; // 나무 자원 생성 위치

    public override void Damaged(PlayerInventory playerInventory)
    {
        Debug.Log("나무를 때렸습니다.");
        DropWoodResource(); // 나무 자원 드롭
        StartCoroutine(nameof(ScaleMove));
    }

    private void DropWoodResource()
    {
        if (treeResourcePrefab != null && spawnPoint != null)
        {
            Instantiate(treeResourcePrefab, spawnPoint.position, Quaternion.identity); // 자원 생성
            Debug.Log("나무 자원이 드롭되었습니다.");
        }
        else
        {
            Debug.LogError("treeResourcePrefab 또는 spawnPoint가 설정되지 않았습니다.");
        }
    }

    IEnumerator ScaleMove()
    {
        while (transform.localScale.x < 1.2f)
        {
            transform.localScale += new Vector3(0.01f, 0, 0);
            yield return null;
        }
        while (transform.localScale.x > 1f)
        {
            transform.localScale -= new Vector3(0.01f, 0, 0);
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
    }
}
