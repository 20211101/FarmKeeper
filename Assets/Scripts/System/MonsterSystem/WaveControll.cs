using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnInfo 
{
    public SpawnInfo(int idx, int root, Monster.MonsterType t, int amount, float d)
    {
        infoSpawner.movingRoot = root;
        infoSpawner.type = t;
        infoSpawner.delay = d;
        // 어느 포탈에서 소환하는지
        spawnerIdx = idx;
        // 소환 할 몬스터 수
        spawnAmount = amount;
    }
    public int spawnerIdx;
    public int spawnAmount;
    public SpawnInfo_Spawner infoSpawner;
}

[System.Serializable]
public struct SpawnInfo_Spawner
{
    public SpawnInfo_Spawner( int root, Monster.MonsterType t, float d)
    {
        // 어느 포탈에서 소환하는지
        movingRoot = root;
        // 소환 할 몬스터 타입
        type = t;
        // 소환 할 몬스터 수
        delay = d;
    }
    public int movingRoot;
    public Monster.MonsterType type;
    public float delay;
}


// 밤 되면 타이머 델리게이트로 알아서 몬스터 만들어줌
public class WaveControll : MonoBehaviour
{
    #region
    public SpawnInfo[] spawnInfo1 ;
    public SpawnInfo[] spawnInfo2 ;
    public SpawnInfo[] spawnInfo3 ;
    public SpawnInfo[] spawnInfo4 ;
    public SpawnInfo[] spawnInfo5 ;
    public SpawnInfo[] spawnInfo6 ;
    public SpawnInfo[] spawnInfo7 ;
    public SpawnInfo[] spawnInfo8 ;
    public SpawnInfo[] spawnInfo9 ;
    public SpawnInfo[] spawnInfo10;
    public SpawnInfo[] spawnInfo11;
    public SpawnInfo[] spawnInfo12;
    public SpawnInfo[] spawnInfo13;
    public SpawnInfo[] spawnInfo14;
    public SpawnInfo[] spawnInfo15;
    #endregion
    public List<SpawnInfo[]> spawnInfos = new List<SpawnInfo[]>();
    [SerializeField]
    Spawner[] spawners = new Spawner[2];

    private int waveCnt = 1;

    private void Awake()
    {
        #region
        spawnInfos.Add(spawnInfo1 );
        spawnInfos.Add(spawnInfo2 );
        spawnInfos.Add(spawnInfo3 );
        spawnInfos.Add(spawnInfo4 );
        spawnInfos.Add(spawnInfo5 );
        spawnInfos.Add(spawnInfo6 );
        spawnInfos.Add(spawnInfo7 );
        spawnInfos.Add(spawnInfo8 );
        spawnInfos.Add(spawnInfo9 );
        spawnInfos.Add(spawnInfo10);
        spawnInfos.Add(spawnInfo11);
        spawnInfos.Add(spawnInfo12);
        spawnInfos.Add(spawnInfo13);
        spawnInfos.Add(spawnInfo14);
        spawnInfos.Add(spawnInfo15);
        #endregion
    }

    private void Start()
    {
        Timer.instance.dayStart += GetAllMonstersBack;
        Timer.instance.dayStart += () => { ++waveCnt; Debug.Log("웨이브 카운트 +1"); };

        Timer.instance.nightStart += SpawnMonster;
    }

    private void SpawnMonster()
    {
        Debug.Log("SpawnMonster 실행");

        // 소환 정보 받아오기
        SpawnInfo[] info = GetSpawnInfoByWave(); 
        // 포탈에 명령 할 명령 데이터
        Stack<SpawnInfo_Spawner> spawnStack1 = new Stack<SpawnInfo_Spawner>();
        Stack<SpawnInfo_Spawner> spawnStack2 = new Stack<SpawnInfo_Spawner>();
        
        foreach(SpawnInfo i in info)
        {
            Stack<SpawnInfo_Spawner> targetStack;
            // 어떤 포탈에 몬스터 추가할 지 분기문
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
                targetStack.Push(i.infoSpawner);
        }

        if (spawnStack1.Count > 0)
        {
            if(spawners[0].IsClosed != true)
                spawners[0].StartSpawn(spawnStack1);
        }
        if (spawnStack2.Count > 0)
            if (spawners[1].IsClosed != true)
                spawners[1].StartSpawn(spawnStack2);
    }

    private SpawnInfo[] GetSpawnInfoByWave()
    {
        return spawnInfos[Timer.instance.DAY_CNT];
    }

    private void GetAllMonstersBack()
    {
        Debug.Log("GetAllMonstersBack 실행");

        foreach (Spawner i in spawners)
            i.ReturnAllMonsters();
    }
}
