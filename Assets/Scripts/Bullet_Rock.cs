using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Rock : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigid;
    int bulletDamage = 2;
    public GameObject maker;
    private void OnEnable()
    {
        rigid.velocity = (transform.forward * 60);
        StartCoroutine(nameof(ReturnPool));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            other.GetComponent<Monster>().Damaged(new DamageInfo(bulletDamage, maker));
            gameObject.SetActive(false);
        }
    }


    IEnumerator ReturnPool()
    {
        yield return new WaitForSeconds(2);
        BulletPool.instance.GetBullet(gameObject, BulletType.Rock);
        gameObject.SetActive(false);
    }

}
