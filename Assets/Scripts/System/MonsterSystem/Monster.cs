using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Damagable
{
    public enum MonsterType
    {
        MOUSE,
        MALL,
        RABBIT
    }

    public enum MonsterHP
    {
        MOUSE = 10,
        MALL = 20,
        RABBIT = 30
    }
    public enum MonsterMeatAmount
    {
        MOUSE = 10,
        MALL = 12,
        RABBIT = 15
    }

    public enum MonsterState
    {
        Move,
        Attack,
        Die
    }
    
    public MonsterType type;
    public delegate void ResetDel();
    public ResetDel resetDel;
    Spawner parentSpawner;

    protected bool isDying = false;
    protected bool isAttacking = false;
    protected bool canAttack = false;
    protected int meatAmount = 3;

    protected MonsterMovement movement;

    private void Awake()
    {
        movement = GetComponent<MonsterMovement>();
    }
    protected void BaseAwake()
    {
        movement = GetComponent<MonsterMovement>();
    }

    public void BringLife(Transform[] paths)
    {
        if(movement == null)
            movement = GetComponent<MonsterMovement>();
        movement.SetTarget(paths);
        movement.StartMoving();
    }

    public override void Damaged(int damage)
    {
        throw new NotImplementedException();
    }


    public void Setting(Spawner spawner)
    {
        parentSpawner = spawner;
    }
    protected virtual void Dead()
    {

    }
    protected virtual void Move()
    {

    }
    protected virtual void Attack()
    {

    }

    public virtual void ResetSelf()
    {
        switch(type)
        {
            default:
                hp = 100;
                break;
        }
        resetDel();
        isAttacking = false;
        canAttack = false;
        isDying = false;
    }
}
