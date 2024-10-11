using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArrow : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigid;
    int bulletDamage = 2;
    public GameObject maker;
    private void OnEnable()
    {
        rigid.velocity = (transform.forward * 40);
        StartCoroutine(nameof(ReturnPool));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Monster>().Damaged(new DamageInfo(bulletDamage, maker));
        }
        else if(other.CompareTag("Terrain"))
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator ReturnPool()
    {
        yield return new WaitForSeconds(2);
        BulletPool.instance.GetBullet(gameObject, BulletType.Arrow);
        gameObject.SetActive(false);
    }
}
