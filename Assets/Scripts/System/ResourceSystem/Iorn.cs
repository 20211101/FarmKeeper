using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iorn : ResourceOrigin
{
    [SerializeField]
    private GameObject steelResourcePrefab; // 철 자원 프리팹
    [SerializeField]
    private Transform spawnPoint; // 자원 생성 위치
    [SerializeField]
    private MeshRenderer[] renderers; // 자원 생성 위치

    public override void Damaged(PlayerInventory playerInventory)
    {
        Debug.Log("돌을 때렸습니다.");
            DropSteelResource();

        StartCoroutine(nameof(ScaleMove));
    }


    private void DropSteelResource()
    {
        if (steelResourcePrefab != null && spawnPoint != null)
        {
            Instantiate(steelResourcePrefab, spawnPoint.position, Quaternion.identity); // 철 자원 생성
        }
        else
        {
            Debug.LogError("steelResourcePrefab 또는 spawnPoint가 설정되지 않았습니다.");
        }
    }

    IEnumerator ScaleMove()
    {
        // 고철 크기 조정 애니메이션
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
        Diactivate();
    }

    [SerializeField]
    GameObject respawnCanvas;
    public override void Respone()
    {
        foreach (MeshRenderer m in renderers)
            m.enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
    public override void Diactivate()
    {
        respawnCanvas.SetActive(true);
        foreach (MeshRenderer m in renderers)
            m.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
