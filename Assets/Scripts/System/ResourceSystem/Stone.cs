using System.Collections;
using UnityEngine;

public class Stone : ResourceOrigin
{
    [SerializeField]
    private GameObject stoneResourcePrefab; // 돌 자원 프리팹
    [SerializeField]
    private GameObject steelResourcePrefab; // 철 자원 프리팹
    [SerializeField]
    private Transform spawnPoint; // 자원 생성 위치
    [SerializeField]
    [Range(0, 100)]
    private int steelDropPercent = 50; // 철 자원 드롭 확률

    public override void Damaged(PlayerInventory playerInventory)
    {
        Debug.Log("돌을 때렸습니다.");
        DropStoneResource(); // 돌 자원 드롭

        // 50% 확률로 철 자원 드롭
        if (Random.Range(0, 100) < steelDropPercent)
        {
            DropSteelResource();
        }

        StartCoroutine(nameof(ScaleMove));
    }

    private void DropStoneResource()
    {
        if (stoneResourcePrefab != null && spawnPoint != null)
        {
            Instantiate(stoneResourcePrefab, spawnPoint.position, Quaternion.identity); // 돌 자원 생성
            Debug.Log("돌 자원이 드롭되었습니다.");
        }
        else
        {
            Debug.LogError("stoneResourcePrefab 또는 spawnPoint가 설정되지 않았습니다.");
        }
    }

    private void DropSteelResource()
    {
        if (steelResourcePrefab != null && spawnPoint != null)
        {
            Instantiate(steelResourcePrefab, spawnPoint.position, Quaternion.identity); // 철 자원 생성
            Debug.Log("철 자원이 드롭되었습니다.");
        }
        else
        {
            Debug.LogError("steelResourcePrefab 또는 spawnPoint가 설정되지 않았습니다.");
        }
    }

    IEnumerator ScaleMove()
    {
        // 돌 크기 조정 애니메이션
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
