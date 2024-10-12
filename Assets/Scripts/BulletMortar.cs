using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMortar : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] float radious = 10f;
    int damage = 10;
    public GameObject maker;

    private void Awake()
    {
        startPos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Terrain"))
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, radious, 1<<9);
            foreach(Collider c in colliders)
            {
                c.GetComponent<Damagable>().Damaged(new DamageInfo(damage, maker));
            }
            Destroy(gameObject);
        }
    }
}
