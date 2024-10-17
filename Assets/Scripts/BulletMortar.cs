using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMortar : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] float radious = 10f;
    [SerializeField] GameObject explodeEffect;
    int damage = 10;
    public GameObject maker;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        startPos = transform.position;
    }
    private void Update()
    {
        rigid.velocity += new Vector3(0, -10, 0) * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Terrain"))
        {
            SoundPlayer.instance.PlayExplosionSound();
            Instantiate(explodeEffect, transform.position + new Vector3(0,1,0), Quaternion.identity);
            Collider[] colliders = Physics.OverlapSphere(transform.position, radious, 1<<9);
            foreach(Collider c in colliders)
            {
                c.GetComponent<Damagable>().Damaged(new DamageInfo(damage, maker));
            }
            Destroy(gameObject);
        }
    }
}
