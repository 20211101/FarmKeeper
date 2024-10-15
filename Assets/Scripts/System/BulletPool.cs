using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Rock,
    Arrow,
    Air
}
public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    public static BulletPool instance { get => _instance; set { } }

    [SerializeField]
    GameObject bullet_arrow;
    [SerializeField]
    GameObject bullet_rock;
    [SerializeField]
    GameObject bullet_air;

    Vector3 tempPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

    int MAX_SIZE= 200;
    Stack<GameObject> arrowPool = new Stack<GameObject>(100);
    Stack<GameObject> rockPool = new Stack<GameObject>(100);
    Stack<GameObject> airPool = new Stack<GameObject>(1000);

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        for(int i  = 0; i < MAX_SIZE; i++)
        {
            GameObject temp = Instantiate(bullet_arrow, tempPos, Quaternion.identity);
            temp.SetActive(false);
            arrowPool.Push(temp);
            temp = Instantiate(bullet_rock, tempPos, Quaternion.identity);
            temp.SetActive(false);
            rockPool.Push(temp);
            temp = Instantiate(bullet_air, tempPos, Quaternion.identity);
            temp.SetActive(false);
            airPool.Push(temp);
            temp = Instantiate(bullet_air, tempPos, Quaternion.identity);
            temp.SetActive(false);
            airPool.Push(temp);
        }
    }

    public GameObject SendBullet(BulletType t)
    {
        switch (t)
        {
            case BulletType.Rock:
                if (rockPool.Count < 1)
                {
                    GameObject temp = Instantiate(bullet_rock, tempPos, Quaternion.identity);
                    temp.SetActive(false);
                    rockPool.Push(temp);
                    return rockPool.Pop();
                }
                else
                    return rockPool.Pop();
                
            case BulletType.Arrow:
                if (arrowPool.Count < 1)
                {
                    GameObject temp = Instantiate(bullet_arrow, tempPos, Quaternion.identity);
                    temp.SetActive(false);
                    arrowPool.Push(temp); 
                    return arrowPool.Pop();
                }
                else
                    return arrowPool.Pop();
            case BulletType.Air:
                if (airPool.Count < 1)
                {
                    GameObject temp = Instantiate(bullet_air, tempPos, Quaternion.identity);
                    temp.SetActive(false);
                    airPool.Push(temp);
                    return airPool.Pop();
                }
                else
                {
                    Debug.Log("POP");
                    return airPool.Pop();
                }

                
        }
        return null;
    }
    public void GetBullet(GameObject g, BulletType t)
    {
        switch (t)
        {
            case BulletType.Rock:
                rockPool.Push(g);
                break;
            case BulletType.Arrow:
                arrowPool.Push(g);
                break;
            case BulletType.Air:
                airPool.Push(g);
                break;
        }
    }
}