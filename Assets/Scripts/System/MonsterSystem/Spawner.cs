using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wave Controller에게서 만들 몬스터 정보 가져와서 몬스터 만들어줌
public class Spawner : MonoBehaviour
{
    private bool isClosed = false;
    public bool IsClosed { get => isClosed; }
    public void Close()
    {
        isClosed = true;
        GetComponent<MeshRenderer>().enabled = false;
    }
    [SerializeField]
    // 스폰 하고서 이동 할 경로(중앙)
    Transform[] path1 = new Transform[2];
    [SerializeField]
    // 스폰 하고서 이동 할 경로(바깥)
    Transform[] path2 = new Transform[2];

    [SerializeField]
    Transform spawnPos;

    // 몬스터 만들어주는 녀석
    MonsterPool pool;
    // 생성된 몬스터
    Stack<GameObject> spawnedMonster = new Stack<GameObject>();
    // 생성된 몬스터(타입)
    Stack<SpawnInfo_Spawner> typeStack = new Stack<SpawnInfo_Spawner>();

    private void Start()
    {
        pool = MonsterPool.instance;
    }

    public void StartSpawn(Stack<SpawnInfo_Spawner> spawnStack)
    {
        if(pool == null) pool = MonsterPool.instance;

        while (spawnStack.Count > 0)
        {
            typeStack.Push(spawnStack.Pop());
            GameObject monster = pool.GetMonster(typeStack.Peek().type);
            monster.GetComponent<Monster>().spawnInfo = typeStack.Peek();
            spawnedMonster.Push(monster);
        }
        StartCoroutine(nameof(DelaySpawn));
    }
    IEnumerator DelaySpawn()
    {
        foreach(GameObject m in spawnedMonster)
        {
            m.SetActive(true);
            m.transform.position = spawnPos.position;

            Monster mon = m.GetComponent<Monster>();
            if(mon.spawnInfo.movingRoot == 0)
                mon.BringLife(path2);
            if(mon.spawnInfo.movingRoot == 1)
                mon.BringLife(path1);
            yield return new WaitForSeconds (mon.spawnInfo.delay);
        }

    }
    internal void ReturnAllMonsters()
    {
        foreach (GameObject m in spawnedMonster)
        {
            m.GetComponent<Monster>().RunAway(); ;
        }
        StartCoroutine(nameof(BackToPool));
    }

    IEnumerator BackToPool()
    {
        yield return new WaitForSeconds(5);
        foreach (GameObject m in spawnedMonster)
        {
            m.SetActive(false);
        }
        for (int i = 0; i < spawnedMonster.Count; ++i)
        {
            pool.CollectMonster(spawnedMonster.Pop(), typeStack.Pop().type);
        }
    }
}
