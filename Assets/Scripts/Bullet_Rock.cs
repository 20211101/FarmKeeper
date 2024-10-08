using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Rock : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigid;
    int bulletDamage = 2;
    private void OnEnable()
    {
        rigid.velocity = (transform.forward * 40);
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            other.GetComponent<Monster>().Damaged(bulletDamage);
            gameObject.SetActive(false);
        }
    }


}
