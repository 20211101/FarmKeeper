using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Damagable
{
    public enum MonsterType
    {
        MOUSE,
        MOLE,
        RABBIT
    }

    public enum MonsterHP
    {
        MOUSE = 10,
        MOLE = 20,
        RABBIT = 30
    }
    public enum MonsterMeatAmount
    {
        MOUSE = 1,
        MOLE = 1,
        RABBIT = 1
    }
    public enum MonsterLeatherAmount
    {
        MOUSE = 2,
        MOLE = 2,
        RABBIT = 2
    }

    public SpawnInfo_Spawner spawnInfo;
    public MonsterType type;
    public delegate void ResetDel();
    Spawner parentSpawner;

    protected bool isDying = false;
    protected int meatAmount = 3;



    public virtual void BringLife(Transform[] paths)
    {
    }

    public override void Damaged(DamageInfo damage)
    {
        throw new NotImplementedException();
    }


    protected virtual void Dead()
    {

    }
    protected virtual void Move()
    {

    }

    public virtual void ResetSelf(Transform[] paths)
    {
        switch(type)
        {
            default:
                hp = 100;
                break;
        }
        isDying = false;
    }

    public virtual void RunAway()
    {
        throw new NotImplementedException();
    }

    public virtual void Push(Vector3 dir)
    {

    }
}
