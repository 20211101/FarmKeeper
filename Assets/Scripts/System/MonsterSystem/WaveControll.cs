using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnInfo
{
    public SpawnInfo(int idx, Monster.MonsterType t, int amount)
    {
        spawnerIdx = idx;
        type = t;
        spawnAmount = amount;
    }
    public int spawnerIdx;
    public Monster.MonsterType type;
    public int spawnAmount;
}

public class WaveControll : MonoBehaviour
{
    [SerializeField]
    Spawner[] spawners = new Spawner[2];

    private int waveCnt = 1;

    private void Start()
    {
        Timer.instance.dayStart += GetAllMonstersBack;
        Timer.instance.dayStart += () => { ++waveCnt; Debug.Log("웨이브 카운트 +1"); };

        Timer.instance.nightStart += SpawnMonster;
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        Debug.Log("SpawnMonster 실행");

        SpawnInfo[] info = GetSpawnInfoByWave();
        Stack<Monster.MonsterType> spawnStack1 = new Stack<Monster.MonsterType>();
        Stack<Monster.MonsterType> spawnStack2 = new Stack<Monster.MonsterType>();
        
        foreach(SpawnInfo i in info)
        {
            Stack<Monster.MonsterType> targetStack;
            switch(i.spawnerIdx)
            {
                case 0:
                    targetStack = spawnStack1;
                    break;
                case 1:
                    targetStack = spawnStack2;
                    break;
                default:
                    Debug.LogError("스포너 인덱스 지정 오류!");
                    return;
            }


            for (int j = 0; j < i.spawnAmount; j++)
                targetStack.Push(i.type);
        }

        if (spawnStack1.Count > 0)
            spawners[0].StartSpawn(spawnStack1);
        if (spawnStack2.Count > 0)
            spawners[1].StartSpawn(spawnStack2);
    }

    private SpawnInfo[] GetSpawnInfoByWave()
    {
        SpawnInfo[] info = new SpawnInfo[2];
        info[0] = new SpawnInfo(0, Monster.MonsterType.MALL, 2);
        info[1] = new SpawnInfo(1, Monster.MonsterType.RABBIT, 2);
        return info;
    }

    private void GetAllMonstersBack()
    {
        Debug.Log("GetAllMonstersBack 실행");

        foreach (Spawner i in spawners)
            i.ReturnAllMonsters();
    }
}
