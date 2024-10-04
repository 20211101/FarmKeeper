using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 정해준 타겟 하나를 잡고 이동.
// 타겟으로 이동했는데, 저장고가 아니면, 저장고로 이동
// 저장고로 이동하면 공격
public class MonsterMovement : MonoBehaviour
{
    protected enum MonsterSpeed
    {
        MOUSE = 5,
        MALL = 6,
        RABBIT = 7
    };
    public Monster monster;
    public Transform[] targetArr;
    public Transform target;
    protected bool isMoving = false;

    protected void Awake()
    {
        monster = GetComponent<Monster>();
    }

    public virtual void SetTarget(Transform[] t)
    {
        targetArr = t;
        target = t[0];
    }

    public void StartMoving()
    {
        isMoving = true;
    }
    public void StopMoving()
    {
        isMoving = false;
    }
}
