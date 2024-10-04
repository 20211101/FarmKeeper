using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트 풀링(몬스터)
public class MonsterPool : MonoBehaviour
{
    private static MonsterPool _instance;
    public static MonsterPool instance { get { return _instance; } private set { } }

    const int MAXIMUM_POOL = 100;

    [SerializeField] GameObject mouse;
    [SerializeField] GameObject mall;
    [SerializeField] GameObject rabbit;

    Stack<GameObject> mouseStack = new Stack<GameObject>(MAXIMUM_POOL);
    Stack<GameObject> mallStack = new Stack<GameObject>(MAXIMUM_POOL);
    Stack<GameObject> rabbitStack = new Stack<GameObject>(MAXIMUM_POOL);

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        int i = 0;
        GameObject temp;
        for (i = 0; i < MAXIMUM_POOL; i++)
        {
            temp = Instantiate(mouse);
            temp.SetActive(false);
            mouseStack.Push(temp);
        }
        for (i = 0; i < MAXIMUM_POOL; i++)
        {
            temp = Instantiate(mall);
            temp.SetActive(false);
            mallStack.Push(temp);
        }
        for (i = 0; i < MAXIMUM_POOL; i++)
        {
            temp = Instantiate(rabbit);
            temp.SetActive(false);
            rabbitStack.Push(temp);
        }
        temp = null;
    }

    public GameObject GetMonster(Monster.MonsterType t)
    {
        GameObject temp = null;

        switch(t)
        {
            case Monster.MonsterType.MOUSE:
                temp = mouseStack.Pop();
                break;
            case Monster.MonsterType.MALL:
                temp = mallStack.Pop();
                break;
            case Monster.MonsterType.RABBIT:
                temp = rabbitStack.Pop();
                break;
            default:
                Debug.Log("GetMonster 타입정의 오류");
                break;
        }


        return temp;
    }
    public void CollectMonster(GameObject m, Monster.MonsterType t)
    {
        switch(t)
        {
            case Monster.MonsterType.MOUSE:
                mouseStack.Push(m);
                break;
            case Monster.MonsterType.MALL:
                mallStack.Push(m);
                break;
            case Monster.MonsterType.RABBIT:
                rabbitStack.Push(m);
                break;
            default:
                Debug.Log("CollectMonster 타입정의 오류");
                break;
        }
    }
}
