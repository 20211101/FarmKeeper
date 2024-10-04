using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Transform[] path1 = new Transform[2];
    [SerializeField]
    Transform[] path2 = new Transform[2];

    [SerializeField]
    Transform spawnPos;

    MonsterPool pool;
    Stack<GameObject> spawnedMonster = new Stack<GameObject>();
    Stack<Monster.MonsterType> typeStack = new Stack<Monster.MonsterType>();

    private void Start()
    {
        pool = MonsterPool.instance;
    }

    public void StartSpawn(Stack<Monster.MonsterType> spawnStack)
    {
        if(pool == null) pool = MonsterPool.instance;

        while (spawnStack.Count > 0)
        {
            typeStack.Push(spawnStack.Pop());
            GameObject monster = pool.GetMonster(typeStack.Peek());
           
            monster.GetComponent<Monster>().Setting(this);
           
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
            if(UnityEngine.Random.Range(0, 2) == 0)
                m.GetComponent<Monster>().BringLife(path1);
            else
                m.GetComponent<Monster>().BringLife(path2);
            yield return new WaitForSeconds (0.5f);
        }

    }
    internal void ReturnAllMonsters()
    {
        for(int i = 0; i < spawnedMonster.Count; ++i)
        {
            pool.CollectMonster(spawnedMonster.Pop(), typeStack.Pop());
        }
    }
}
