using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAir : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigid;
    int bulletDamage = 1;
    public GameObject maker;
    private void OnEnable()
    {
        rigid.velocity = (transform.forward  * 30);
        StartCoroutine(nameof(ReturnPool));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster m = other.GetComponent<Monster>();
            m.Damaged(new DamageInfo(bulletDamage, maker));
            Vector3 dir = (other.transform.position - maker.transform.position).normalized;
            Vector3 pow = new Vector3(dir.x, 0, dir.y) * 15;
            m.Push(pow);
            gameObject.SetActive(false);
        }
    }
    void OnDisable()
    {
        BulletPool.instance.GetBullet(gameObject, BulletType.Air);
        StopCoroutine(nameof(ReturnPool));
    }
    IEnumerator ReturnPool()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
    }
}
